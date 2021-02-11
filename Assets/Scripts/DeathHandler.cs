using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using DentedPixel;

public class DeathHandler : MonoBehaviour
{
    public bool gameOver = false;
    public GameObject controller;
    [SerializeField] FirstPersonController fpsController;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] Button playAgainBtn;
    [SerializeField] GameObject gunReticle;
    Camera fpsCamera;
    float fadeInDuration = 4f;
    bool alignToCenter = false;

    void Start()
    {
        fpsCamera = transform.GetChild(0).GetComponent<Camera>();
        playerAnimator.enabled = false;
    }

    void Update()
    {
        if(alignToCenter)
        {
            // Align player angle direction to center
            Vector3 targetPos = new Vector3(0f, fpsCamera.transform.eulerAngles.y, fpsCamera.transform.eulerAngles.z);
            Quaternion targetRotation = Quaternion.Euler(targetPos);
            fpsCamera.transform.rotation = Quaternion.Slerp(fpsCamera.transform.rotation, targetRotation, Time.deltaTime * 5f);
            // Run post align functions after aligning to center
            if(fpsCamera.transform.eulerAngles.x < 10f || fpsCamera.transform.eulerAngles.x > 349f){ PostAlign(); }
        }
    }

    public void HandleDeath()
    {
        gameOver = true;
        gunReticle.SetActive(false);
        weaponAnimator.enabled = false;
        if(FindObjectOfType<WeaponSwitcher>().currentWeapon == 0)
        {
            // Set default FOV if zoomed in
            BroadcastMessage("SetDefaultZoom");
        }
        ProcessMouseInput();
        alignToCenter = true;
    }

    private void PostAlign()
    {
        alignToCenter = false;
        // Display the Game Over menu overlay
        gameOverCanvas.SetActive(true);
        FadeInOverlay();
        ProcessDeathAnimation();
    }

    private void FadeInOverlay()
    {
        // Tween UI fade in for game over overlay
        CanvasGroup gameOverCG = gameOverCanvas.GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(gameOverCG, 1f, fadeInDuration);
    }

    private void ProcessMouseInput()
    {
        // Disable the FPSController mouse lock functions and prevent mouse from moving camera, finally make mouse visible
        fpsController.gameOver = true; // Let fps script know that game is over and disable some things
        fpsController.m_MouseLook.lockCursor = false;
        fpsController.m_MouseLook.m_cursorIsLocked = false;
    }

    private void ProcessDeathAnimation()
    {
        // Enable animator instead of call trigger event to prevent interferce with fpscontroller script
        playerAnimator.enabled = true;
        Invoke("StopTime", 4f);
    }

    private void StopTime()
    {
        Time.timeScale = 0;
        // Disable weapon switching when time stops
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playAgainBtn.Select();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class DeathHandler : MonoBehaviour
{
    public GameObject controller;
    public bool gameOver = false;
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Button playAgainBtn;
    [SerializeField] GameObject gunReticle;
    float gameOverFadeIn = 4f;

    void Start()
    {
        playerAnimator.enabled = false;
    }

    public void HandleDeath()
    {
        gameOver = true;
        gunReticle.SetActive(false);
        // Display the Game Over menu overlay
        gameOverCanvas.SetActive(true);
        FadeInOverlay();
        ProcessMouseInput();
        ProcessDeathAnimation();
        
        
    }

    private void FadeInOverlay()
    {
        // Tween UI fade in for game over overlay
        CanvasGroup gameOverCG = gameOverCanvas.GetComponent<CanvasGroup>();
        LeanTween.alphaCanvas(gameOverCG, 1f, gameOverFadeIn);
    }

    private void ProcessMouseInput()
    {
        // Disable the FPSController mouse lock functions and prevent mouse from moving camera, finally make mouse visible
        var fpsController = controller.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playAgainBtn.Select();
    }
}

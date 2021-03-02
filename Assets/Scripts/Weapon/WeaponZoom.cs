using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] FirstPersonController fpsController;
    [SerializeField] Camera mainCamera;
    float zoomInFOV = 15f;
    float zoomOutFOV = 60f;
    float zoomInSensitivity = 0.1875f;
    float zoomOutSensitivity = 0.75f;
    bool isZoomed = false;
    bool canZoom = true;

    void Update()
    {
        // Mouse Input
        if(CrossPlatformInputManager.GetButtonDown("Zoom"))
        {
            CheckInput();
            SetZoom();
        }

        // Gamepad Input
        if(CrossPlatformInputManager.GetAxis("Zoom") == -1 && canZoom)
        {
            // Only zoom once when gamepad button pressed
            canZoom = false;
            CheckInput();
            SetZoom(); 
        }

        if(CrossPlatformInputManager.GetAxis("Zoom") != -1)
        {
            canZoom = true;
        }

    }

    private void CheckInput()
    {
        // Check if gamepad is connected and if so update sensitivity, otherwise use default
        if(Input.GetJoystickNames().Length > 0)
        {
            zoomInSensitivity = 0.75f;
            zoomOutSensitivity = 3f;
        }
        else
        {
            zoomInSensitivity = 0.1875f;
            zoomOutSensitivity = 0.75f;
        }
    }

    private void SetZoom()
    {
        // Toggle camera fov and mouse sensitivity
        if(isZoomed)
        {
            ChangeFPSettings(zoomOutFOV, zoomOutSensitivity);
        }
        else
        {
            ChangeFPSettings(zoomInFOV, zoomInSensitivity);
        }
        isZoomed = !isZoomed;
    }

    public void SetDefaultZoom()
    {
        isZoomed = false;
        ChangeFPSettings(zoomOutFOV, zoomOutSensitivity);
    }

    private void ChangeFPSettings(float fov, float sensitivity)
    {
        mainCamera.fieldOfView = fov;
        fpsController.m_MouseLook.XSensitivity = sensitivity;
        fpsController.m_MouseLook.YSensitivity = sensitivity;
    }

}

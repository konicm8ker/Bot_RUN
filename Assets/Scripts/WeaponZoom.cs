using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] FirstPersonController fpsController;
    [SerializeField] Camera mainCamera;
    float zoomInFOV = 15f;
    float zoomOutFOV = 60f;
    float zoomInSensitivity = 0.5f;
    float zoomOutSensitivity = 2f;
    bool isZoomed = false;

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
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
    }

    private void SetDefaultZoom()
    {
        ChangeFPSettings(zoomOutFOV, zoomOutSensitivity);
    }

    private void ChangeFPSettings(float fov, float sensitivity)
    {
        mainCamera.fieldOfView = fov;
        fpsController.m_MouseLook.XSensitivity = sensitivity;
        fpsController.m_MouseLook.YSensitivity = sensitivity;
    }

}

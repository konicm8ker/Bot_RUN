using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using DentedPixel;
using TMPro;

[Serializable]
public class MouseLook
{
    public float XSensitivity = 0.75f;
    public float YSensitivity = 0.75f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;
    public string joystickType;
    bool inputChecked = false;
    bool inverted = false;
    string[] joysticks;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    public bool m_cursorIsLocked = true;

    public void Init(Transform character, Transform camera)
    {
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = camera.localRotation;
    }

    public string CheckInput()
    {
        // Only check once when called at startup
        inputChecked = true;

        joysticks = Input.GetJoystickNames();
        var statusText = "";
        // Check if there is a gamepad connected and if so update sensitivity, otherwise use default
        if(joysticks.Length > 0)
        {
            GetJoystickType();
            XSensitivity = 3f;
            YSensitivity = 3f;
            return statusText = joystickType + " controller found!\nUsing gamepad sensitivity.";
        }
        else
        {
            XSensitivity = 0.75f;
            YSensitivity = 0.75f;
            return statusText = "No controller found!\nUsing mouse sensitivity.";
        }
    }

    private void GetJoystickType()
    {
        joystickType = "";
        if(joysticks[0].ToLower().Contains("microsoft") || joysticks[0].ToLower().Contains("xbox"))
        {
            joystickType = "Xbox One";
        }
        else if(joysticks[0].ToLower().Contains("sony") || joysticks[0].ToLower().Contains("ps4"))
        {
            joystickType = "PS4";
        }
        else if(joysticks[0].ToLower().Contains("wireless"))
        {
            joystickType = "Wireless";
        }
        if(joystickType == ""){ joystickType = "Unrecognized"; }
    }

    public void LookRotation(Transform character, Transform camera, bool isFirefox)
    {
        float mouseX = CrossPlatformInputManager.GetAxis("Mouse X");
        float mouseY = CrossPlatformInputManager.GetAxis("Mouse Y");
        // Toggle y-axis invert when key/button pressed
        if(isFirefox)
        {
            if(joystickType == "PS4")
            {
                if(CrossPlatformInputManager.GetButtonDown("PS4 Invert")){ inverted = !inverted; }
            }
            else
            {
                if(CrossPlatformInputManager.GetButtonDown("Firefox Invert")){ inverted = !inverted; }
            }
        }
        else
        {
            if(CrossPlatformInputManager.GetButtonDown("Invert")){ inverted = !inverted; }
        }
        if(inverted){ mouseY = -mouseY; }else{ mouseY = +mouseY; }
        float yRot = mouseX * XSensitivity;
        float xRot = mouseY * YSensitivity;
        
        m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

        if(clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

        if(smooth)
        {
            character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            character.localRotation = m_CharacterTargetRot;
            camera.localRotation = m_CameraTargetRot;
        }

        // Check if gamepad is connected on first look around movement
        if(mouseX != 0 || mouseY != 0)
        {
            if(inputChecked == false)
            {
                var statusText = CheckInput();
                GameObject.FindWithTag("Player").GetComponent<FirstPersonController>().FadeInDeviceStatus(statusText);
            }
        }
        // Debug.Log("XSen:" + XSensitivity + " | YSen: " + YSensitivity);

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if(!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}

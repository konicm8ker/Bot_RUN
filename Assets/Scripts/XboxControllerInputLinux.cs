using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class XboxControllerInputLinux : MonoBehaviour
{

    // Xbox One X Controller Mapping for Linux

    [Header("Xbox One X Controller - Linux")]
    public float xboxHorizontalL; // Joystick X Axis
    public float xboxVerticalL; // Joystick Y Axis - Inverted
    public float xboxHorizontalR; // Joystick 4th Axis
    public float xboxVerticalR; // Joystick 5th Axis - Inverted
    public float xboxDpadHorizontal; // Joystick 7th Axis
    public float xboxDpadVertical; // Joystick 8th Axis - Inverted
    public float xboxLT; // Joystick 3rd Axis
    public float xboxRT; // Joystick 6th Axis
    public bool xboxA; // Joystick Button 0
    public bool xboxB; // Joystick Button 1
    public bool xboxX; // Joystick Button 2
    public bool xboxY; // Joystick Button 3
    public bool xboxLB; // Joystick Button 4
    public bool xboxRB; // Joystick Button 5
    public bool xboxBack; // Joystick Button 6
    public bool xboxMenu; // Joystick Button 7
    public bool xboxGuide; // Joystick Button 8
    public bool xboxLS; // Joystick Button 9
    public bool xboxRS; // Joystick Button 10

    void Update()
    {
        xboxHorizontalL = CrossPlatformInputManager.GetAxis("Xbox Horizontal L - Linux");
        xboxVerticalL = CrossPlatformInputManager.GetAxis("Xbox Vertical L - Linux");
        xboxHorizontalR = CrossPlatformInputManager.GetAxis("Xbox Horizontal R - Linux");
        xboxVerticalR = CrossPlatformInputManager.GetAxis("Xbox Vertical R - Linux");
        xboxDpadHorizontal = CrossPlatformInputManager.GetAxis("Xbox Dpad Horizontal - Linux");
        xboxDpadVertical = CrossPlatformInputManager.GetAxis("Xbox Dpad Vertical - Linux");
        xboxLT = CrossPlatformInputManager.GetAxis("Xbox LT - Linux");
        xboxRT = CrossPlatformInputManager.GetAxis("Xbox RT - Linux");
        xboxA = CrossPlatformInputManager.GetButton("Xbox A - Linux");
        xboxB = CrossPlatformInputManager.GetButton("Xbox B - Linux");
        xboxX = CrossPlatformInputManager.GetButton("Xbox X - Linux");
        xboxY = CrossPlatformInputManager.GetButton("Xbox Y - Linux");
        xboxLB = CrossPlatformInputManager.GetButton("Xbox LB - Linux");
        xboxRB = CrossPlatformInputManager.GetButton("Xbox RB - Linux");
        xboxBack = CrossPlatformInputManager.GetButton("Xbox Back - Linux");
        xboxMenu = CrossPlatformInputManager.GetButton("Xbox Menu - Linux");
        xboxGuide = CrossPlatformInputManager.GetButton("Xbox Guide - Linux");
        xboxLS = CrossPlatformInputManager.GetButton("Xbox LS - Linux");
        xboxRS = CrossPlatformInputManager.GetButton("Xbox RS - Linux");
    }

}

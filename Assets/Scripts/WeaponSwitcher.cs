using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public int currentWeapon = 0;
    [SerializeField] float timer = 0f;
    [SerializeField] float timerDuration = 0.25f;
    [SerializeField] string switchState = "";

    void Start()
    {
        SetActiveWeapon();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;
        ProcessKeyInput();
        ProcessScrollWheel();

        if(previousWeapon != currentWeapon)
        {
            SetActiveWeapon();
        }
    }

    private void SetActiveWeapon()
    {
        int weaponIndex = 0;
        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
        
    }

    private void ProcessKeyInput()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void ProcessScrollWheel()
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                switchState = "up";
            }
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(currentWeapon <= 0)
            {
                currentWeapon = 0;
            }
            else
            {
                switchState = "down";
            }
        }
        WeaponSwitchDelay();

    }

    private void WeaponSwitchDelay()
    {

        if(switchState == "up")
        { 
            timer += Time.deltaTime;
            if(timer >= timerDuration){
                currentWeapon++;
                timer = 0f;
                switchState = "";
            }
        }
        if(switchState == "down")
        {
            timer += Time.deltaTime;
            if(timer >= timerDuration){
                currentWeapon--;
                timer = 0f;
                switchState = "";
            }
        }

    }

}

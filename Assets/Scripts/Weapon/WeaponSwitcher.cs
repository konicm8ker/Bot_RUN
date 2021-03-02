using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class WeaponSwitcher : MonoBehaviour
{
    public int currentWeapon = 0;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] float timer = 0f;
    [SerializeField] float timerDuration = 0.25f;
    [SerializeField] string scrollState = "";
    public int weaponToBeSwitched = 0;
    bool inputLock = false;

    void Start()
    {
        SetActiveWeapon();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;
        if(inputLock == false)
        {
            // Weapons loop index both ways when switching
            ProcessKeyInput();
            ProcessScrollWheel();
            ProcessGamepadInput();
        }
    }

    private void SetActiveWeapon()
    {
        // Loop through each child object and enable/disable weapon
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
            if(currentWeapon != 0)
            {
                inputLock = true;
                weaponToBeSwitched = 0;
                StartWeaponSwitch();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(currentWeapon != 1)
            {
                inputLock = true;
                ResetZoom();
                weaponToBeSwitched = 1;
                StartWeaponSwitch();
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(currentWeapon != 2)
            {
                inputLock = true;
                ResetZoom();
                weaponToBeSwitched = 2;
                StartWeaponSwitch();
            }
        }
    }

    private void ProcessScrollWheel()
    {

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(currentWeapon > transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                scrollState = "up";
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(currentWeapon < 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                scrollState = "down";
            }
        }
        WeaponSwitchDelay();

    }

    private void ProcessGamepadInput()
    {
        if(CrossPlatformInputManager.GetButtonDown("Weapon Switch Left"))
        {
            if(currentWeapon < 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            else
            {
                scrollState = "down";
            }
        }

        if(CrossPlatformInputManager.GetButtonDown("Weapon Switch Right"))
        {
            if(currentWeapon > transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            else
            {
                scrollState = "up";
            }
        }
        WeaponSwitchDelay();
    }

    private void StartWeaponSwitch()
    {
        // Play weapon switch animation
        weaponAnimator.SetTrigger("WeaponSwitchStart");
        StartCoroutine(WaitForWeaponSwitchStart());
    }

    IEnumerator WaitForWeaponSwitchStart()
    {
        yield return new WaitForSeconds(.5f);
        SwitchWeapon();
    }

    public void SwitchWeapon()
    {
        currentWeapon = weaponToBeSwitched;
        SetActiveWeapon();
        EndWeaponSwitch();
    }

    private void EndWeaponSwitch()
    {
        weaponAnimator.SetTrigger("WeaponSwitchEnd");
        StartCoroutine(WaitForWeaponSwitchEnd());
    }

    IEnumerator WaitForWeaponSwitchEnd()
    {
        yield return new WaitForSeconds(.5f);
        weaponAnimator.SetTrigger("Idle");
        inputLock = false;
    }

    private void WeaponSwitchDelay()
    {
        // Only switch weapons from scroll wheel at smooth rate
        if(scrollState == "up")
        {
            ResetZoom();
            timer += Time.deltaTime;
            if(timer >= timerDuration){
                inputLock = true;
                weaponToBeSwitched++;
                if(weaponToBeSwitched > transform.childCount - 1)
                {
                    weaponToBeSwitched = 0;
                }
                StartWeaponSwitch();
                timer = 0f;
                scrollState = "";
            }
        }
        if(scrollState == "down")
        {
            ResetZoom();
            timer += Time.deltaTime;
            if(timer >= timerDuration){
                inputLock = true;
                weaponToBeSwitched--;
                if(weaponToBeSwitched < 0)
                {
                    weaponToBeSwitched = transform.childCount - 1;
                }
                StartWeaponSwitch();
                timer = 0f;
                scrollState = "";
            }
        }

    }

    private void ResetZoom()
    {
        // If weapon is zoomed in reset zoom before switching to non zoom weapon
        if(weaponToBeSwitched == 0)
        {
            FindObjectOfType<WeaponZoom>().SetDefaultZoom();
        }
    }

}

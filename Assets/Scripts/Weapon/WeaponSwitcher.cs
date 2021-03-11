using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    Transform ammoDisplay;
    GameObject bulletsIcon;
    GameObject shellsIcon;
    GameObject slugsIcon;
    TextMeshProUGUI ammoAmountText;
    TextMeshProUGUI weaponNameText;
    TextMeshProUGUI ammoNameText;

    AmmoType ammoType;
    Ammo ammoAmount;
    AmmoType bullets;
    AmmoType shells;
    AmmoType slugs;


    void Start()
    {
        GetAmmoInfo();
        GetAmmoDisplayElements();
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
            UpdateAmmoDisplay();
        }
    }

    private void GetAmmoInfo()
    {
        ammoAmount = FindObjectOfType<Ammo>();
        bullets = gameObject.transform.GetChild(0).GetComponent<Weapon>().ammoType;
        shells = gameObject.transform.GetChild(1).GetComponent<Weapon>().ammoType;
        slugs = gameObject.transform.GetChild(2).GetComponent<Weapon>().ammoType;
    }

    private void GetAmmoDisplayElements()
    {
        ammoDisplay = GameObject.FindWithTag("Ammo Display").transform;
        bulletsIcon = ammoDisplay.Find("Bullets Icon").gameObject;
        shellsIcon = ammoDisplay.Find("Shells Icon").gameObject;
        slugsIcon = ammoDisplay.Find("Slugs Icon").gameObject;
        ammoAmountText = ammoDisplay.Find("Ammo Amount").GetComponent<TextMeshProUGUI>();
        weaponNameText = ammoDisplay.Find("Weapon Name").GetComponent<TextMeshProUGUI>();
        ammoNameText = ammoDisplay.Find("Ammo Name").GetComponent<TextMeshProUGUI>();
    }

    private void UpdateAmmoDisplay()
    {
        // Update the ammo display ui based on current active weapon
        if(currentWeapon == 0)
        {
            UpdateAmmoIcons(bulletsIcon, shellsIcon, slugsIcon);
            UpdateAmmoText(bullets, "Plasma Rifle");
        }
        else if(currentWeapon == 1)
        {
            UpdateAmmoIcons(shellsIcon, bulletsIcon, slugsIcon);
            UpdateAmmoText(shells, "Automatic");
        }
        else if(currentWeapon == 2)
        {
            UpdateAmmoIcons(slugsIcon, bulletsIcon, shellsIcon);
            UpdateAmmoText(slugs, "Shotgun");
        }
    }

    private void UpdateAmmoIcons(GameObject icon1, GameObject icon2, GameObject icon3)
    {
        icon1.SetActive(true);
        icon2.SetActive(false);
        icon3.SetActive(false);
    }

    private void UpdateAmmoText(AmmoType type, string weapon)
    {
        ammoAmountText.text = ammoAmount.RequestAmmoAmount(type).ToString();
        weaponNameText.text = weapon;
        ammoNameText.text = type.ToString();
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
        // Only switch weapons with smooth a transition delay
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

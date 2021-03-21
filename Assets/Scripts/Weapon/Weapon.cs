using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Weapon : MonoBehaviour
{
    [SerializeField] Ammo ammoSlot;
    public AmmoType ammoType;
    [SerializeField] Camera FPSCamera;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject weaponHitVFX;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 20;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] bool isAuto = true;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] FirstPersonController fpsController;
    public bool canShoot = true;
    int currentWeapon;

    void OnEnable()
    {
        // Makes sure weapons can shoot when switched back and forth
        canShoot = true;
        currentWeapon = FindObjectOfType<WeaponSwitcher>().currentWeapon;
    }

    void Update()
    {
        if(fpsController.isPaused){ return; } // Don't process weapon fire when game is paused
        if(CrossPlatformInputManager.GetAxis("Fire1") == 1 && canShoot && isAuto)
        {
            // Handle firing for automatic weapon
            StartCoroutine(FireWeapon());
        }
        else
        {
            weaponAnimator.SetTrigger("Idle");
        }
        if(CrossPlatformInputManager.GetAxis("Fire1") == 1 && canShoot)
        {
            StartCoroutine(FireWeapon());
        }

    }

    IEnumerator FireWeapon()
    {
        canShoot = false;
        // Only fire if enough ammo
        if(ammoSlot.RequestAmmoAmount(ammoType) > 0)
        {
            PlayWeaponFireAnimation();
            PlayFiringVFX();
            ProcessRaycast();
            ammoSlot.ReduceAmmoAmount(ammoType);
        }
        else { print("OUT OF AMMO."); }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayWeaponFireAnimation()
    {
        // Play weapon fire animation depending on weapon active
        if(currentWeapon == 0)
        {
            weaponAnimator.SetTrigger("WeaponFire1");
        }
        else if(currentWeapon == 1)
        {
            weaponAnimator.SetTrigger("WeaponFire2");
        }
        else if(currentWeapon == 2)
        {
            weaponAnimator.SetTrigger("WeaponFire3");
        }
        if(isAuto){ return; } // If automatic, set idle after active firing is finished
        weaponAnimator.SetTrigger("Idle");
    }

    private void PlayFiringVFX()
    {
        // Play muzzle flash vfx
        muzzleFlashVFX.Play();
    }

    private void ProcessRaycast()
    {
        // Send raycast and get info to check if enemy is hit or not
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range);
        if(raycastHit)
        {
            ProcessWeaponHitVFX(hit);
            var target = hit.transform.gameObject;
            
            // Make sure raycast hit is on child collider of enemy objects
            if(target.tag == "Enemy - DroneBot" || target.tag == "Enemy - SpiderBot"){
                target.GetComponentInParent<EnemyHealth>().TakeDamage(damage);
            } else { return; }
        }
        else
        {
            return;
        }
    }

    private void ProcessWeaponHitVFX(RaycastHit hit)
    {
        // Add some hit effect for visual confirmation
        GameObject weaponHit = Instantiate(weaponHitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(weaponHit, 0.1f);
    }
}

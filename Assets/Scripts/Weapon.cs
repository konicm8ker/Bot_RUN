using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] Camera FPSCamera;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject weaponHitVFX;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 20;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] bool isAuto = true;
    public bool canShoot = true;

    void OnEnable()
    {
        // Makes sure weapons can shoot when switched back and forth
        canShoot = true;    
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && canShoot && isAuto)
        {
            // Handle firing for automatic weapon
            StartCoroutine(FireWeapon());
        }
        else if(Input.GetMouseButtonDown(0) && canShoot)
        {
            // Handle firing for other weapons
            StartCoroutine(FireWeapon());   
        }

    }

    IEnumerator FireWeapon()
    {
        canShoot = false;
        // Only fire if enough ammo
        if(ammoSlot.RequestAmmoAmount(ammoType) > 0)
        {
            PlayFiringVFX();
            ProcessRaycast();
            ammoSlot.ReduceAmmoAmount(ammoType);
        }
        else { print("OUT OF AMMO."); }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
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
            if(target.tag == "Enemy"){
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

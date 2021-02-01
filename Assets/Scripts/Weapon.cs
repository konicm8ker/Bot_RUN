using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject weaponHitVFX;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 20;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        PlayFiringVFX();
        ProcessRaycast();
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
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target){ target.TakeDamage(damage); } else { return; }
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

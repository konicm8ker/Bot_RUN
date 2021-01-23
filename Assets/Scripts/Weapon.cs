using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range = 100f;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            FireWeapon();
        }
    }

    private void FireWeapon()
    {
        RaycastHit hit;
        bool raycastHit = Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range);
        var objectHitString = "";
        if(raycastHit)
        {
            if(hit.transform.parent)
            {
                objectHitString = hit.transform.parent.name + " > " + hit.transform.name; 
            }
            else
            {
                objectHitString = hit.transform.name;
            }
            print("You've hit something! [" + objectHitString + "]");
        }
        else
        {
            print("No object was hurt in the playing of this game.");
        }
    }
}

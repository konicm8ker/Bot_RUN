using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType;
    [SerializeField] int ammoAmount = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("You got " + ammoType + "!");
            FindObjectOfType<Ammo>().IncreaseAmmoAmount(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}

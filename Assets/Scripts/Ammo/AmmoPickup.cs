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
            // print("You got " + ammoType + "!");
            FindObjectOfType<Ammo>().IncreaseAmmoAmount(ammoType, ammoAmount);
            // Activate PickupStatus to show/hide status text
            gameObject.GetComponentInParent<PickupStatus>().ammoType = ammoType;
            gameObject.GetComponentInParent<PickupStatus>().ammoAmount = ammoAmount;
            gameObject.GetComponentInParent<PickupStatus>().showStatusText = true;
            Destroy(gameObject);
        }
    }

}

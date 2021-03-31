using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthAmount;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FindObjectOfType<PlayerHealth>().IncreaseHealth(healthAmount);
            gameObject.GetComponentInParent<PickupStatus>().isHealth = true;
            gameObject.GetComponentInParent<PickupStatus>().showStatusText = true;
            Destroy(gameObject);
        }
    }
}

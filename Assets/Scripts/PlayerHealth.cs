using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            // Process post death methods
            GetComponent<DeathHandler>().HandleDeath();
        }
        
    }
}

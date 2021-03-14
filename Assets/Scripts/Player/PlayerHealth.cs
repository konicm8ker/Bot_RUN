using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public HealthBar healthBar;
    int lastHealth;
    int targetHealth;
    bool canDrain = false;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        DrainHealth();
    }

    public void TakeDamage(int damage)
    {
        lastHealth = health;
        targetHealth = health - damage;
        canDrain = true;
        health -= damage;
        // healthBar.SetHealth(health);
        if(health <= 0)
        {
            // Process post death methods
            GetComponent<DeathHandler>().HandleDeath();
        }
        
    }

    private void DrainHealth()
    {
        if(canDrain == false){ return; }
        if(lastHealth > targetHealth)
        {
            lastHealth--;
            healthBar.SetHealth(lastHealth);
        }
        else
        {
            canDrain = false;
            Debug.Log(lastHealth);
        }
    }

}

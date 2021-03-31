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
    bool canAdd = false;

    void Start()
    {
        healthBar.SetMaxHealth(health);
    }

    void Update()
    {
        AddHealth();
        DrainHealth();
    }

    public void IncreaseHealth(int healthAmount)
    {
        lastHealth = health;
        targetHealth = health + healthAmount;
        if(targetHealth > 100){ targetHealth = 100; } // Set max target health if over increased
        canAdd = true;
        health += healthAmount;
        if(health > 100){ health = 100; } // Set max health if over increased
    }

    public void TakeDamage(int damage)
    {
        lastHealth = health;
        targetHealth = health - damage;
        canDrain = true;
        health -= damage;
        if(health <= 0)
        {
            // Process post death methods
            GetComponent<DeathHandler>().HandleDeath();
        }
        
    }

    private void AddHealth()
    {
        if(canAdd == false){ return; }
        if(lastHealth < targetHealth)
        {
            // Debug.Log("Increasing health");
            lastHealth++;
            healthBar.SetHealth(lastHealth);
        }
        else
        {
            canAdd = false;
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
        }
    }

}

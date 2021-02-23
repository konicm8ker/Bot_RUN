using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHP = 100;
    bool enemyDeathActive;

    public void TakeDamage(int damage)
    {
        BroadcastMessage("OnDamageTaken");
        enemyHP -= damage;
        if(enemyHP <= 0)
        {
            // Once dead, play death anim once then disable enemy
            if(enemyDeathActive == false)
            {
                enemyDeathActive = true;
                SendMessage("EnemyDeath");
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int enemyHP = 100;

    public void TakeDamage(int damage)
    {
        enemyHP -= damage;
        if(enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}

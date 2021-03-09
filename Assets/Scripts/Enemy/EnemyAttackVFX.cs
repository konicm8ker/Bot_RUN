using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackVFX : MonoBehaviour
{
    [SerializeField] GameObject enemyAttack;

    public void StartAttackVFX()
    {
        if(enemyAttack.activeSelf == true)
        { enemyAttack.SetActive(false); }
        enemyAttack.SetActive(true);
    }

    public void EndAttackVFX()
    {
        enemyAttack.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] GameObject playerHitCanvas;
    [SerializeField] GameObject spiderBotAttack;
    [SerializeField] int damage = 10;
    PlayerHealth target;
    bool hitStarted = false;
    
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void StartSpawnMessage()
    {
        BroadcastMessage("StartAttackVFX");
    }

    public void EndSpawnMessage()
    {
        BroadcastMessage("EndAttackVFX");
    }

    public void AttackHitEvent()
    {
        if(!target){ return; }
        target.TakeDamage(damage);
        if(hitStarted == false){ DisplayPlayerHit(); }
    }

    private void DisplayPlayerHit()
    {
        hitStarted = true;
        playerHitCanvas.SetActive(true);
        Invoke("HidePlayerHit", .5f);
    }

    private void HidePlayerHit()
    {
        if(target.health <= 0){ return; } 
        playerHitCanvas.SetActive(false);
        hitStarted = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float targetRange = 5f;
    NavMeshAgent navMeshAgent;
    Animator enemyAnimator;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        // Disable navmesh on start to prevent interference with idle animation
        navMeshAgent.enabled = false;
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= targetRange)
        {
            isProvoked = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, targetRange);
    }

    private void EngageTarget()
    {
        
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        // Enable navmesh agent when moving and set enemy state to move
        navMeshAgent.enabled = true;
        enemyAnimator.SetBool("attack", false);
        enemyAnimator.SetTrigger("move");

        // Have AI agent move towards players current position
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        // Disable navmesh to prevent interference of attack animation, enable attack animation
        navMeshAgent.enabled = false;
        enemyAnimator.SetBool("attack", true);

    }
}

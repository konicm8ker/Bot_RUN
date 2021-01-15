using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float targetRange = 5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        // Have AI agent move towards players current position
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        // Insert attack functionality here
        print("DIE HUMAN! *stab*");
    }
}

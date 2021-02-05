using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float targetRange = 5f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] DeathHandler deathHandler;
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

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            if(deathHandler.gameOver == false){ AttackTarget(); }
            else { isProvoked = false; enemyAnimator.enabled = false; }
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
        FaceTarget();
        enemyAnimator.SetBool("attack", true);

    }

    public void FaceTarget()
    {
        // Get new enemy direction vector without magnitude and also enemy look rotation
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));

        // Set new direction and rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
    }

}

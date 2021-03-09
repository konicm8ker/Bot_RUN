using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    FirstPersonController fpsController;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] Transform target;
    [SerializeField] DeathHandler deathHandler;
    EnemyHealth enemyHealth;
    NavMeshAgent navMeshAgent;
    NavMeshObstacle navMeshObstacle;
    Animator enemyAnimator;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        fpsController = FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshObstacle = GetComponent<NavMeshObstacle>();
        enemyAnimator = GetComponent<Animator>();
        // Disable navmesh on start to prevent interference with idle animation
        navMeshAgent.enabled = false;
    }

    void LateUpdate()
    {

        distanceToTarget = Vector3.Distance(transform.position, target.position);
        if(isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= fpsController.enemyRadius)
        {
            isProvoked = true;
        }
    }

    void OnDrawGizmosSelected()
    {
        fpsController = FindObjectOfType<FirstPersonController>().GetComponent<FirstPersonController>();
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fpsController.enemyRadius);
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }
    private void EnemyDeath()
    {
        // Disable drone fans if drone bot
        if(gameObject.tag == "Enemy - DroneBot")
        {
            BroadcastMessage("DisableDroneFan");
        }
        // Disable enemy movement and attacks and play death anim
        navMeshAgent.enabled = false;
        enabled = false;
        enemyAnimator.SetTrigger("death");
    }

    public void DisableEnemyAnimator()
    {
        // Disable enemy animator after death anim is finished
        enemyAnimator.enabled = false;
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
        navMeshObstacle.enabled = false;
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
        navMeshObstacle.enabled = true; // Have moving enemies avoid attacking ones
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CactusAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float patrolspeed = 2f;

    public float chargespeed = 10f;

    //Patroling
    private Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    // public GameObject projectile;
    private bool currentRolling;
    public float chargeTime;

    //States
    public float sightRange;
    public bool playerInSightRange;

    // Animator
    public Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolspeed;

    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (playerInSightRange)
        {
            animator.SetBool("chargingUp", true);
            StartCoroutine(Charge());
        }
        else
        {
            Patroling();
        }
    }

    private void Patroling()
    {
        animator.SetBool("endRoll", true);
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.speed = patrolspeed;
            agent.acceleration = patrolspeed;
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    IEnumerator Charge()
    {
        Vector3 oldpos = player.position;
        agent.speed = chargespeed;
        agent.acceleration = chargespeed;
        currentRolling = true;
        agent.SetDestination(transform.position);

        yield return new WaitForSeconds(chargeTime);

        ChasePlayer(oldpos);

        animator.SetBool("chargingUp", false);
        
    }
    
    private void ChasePlayer(Vector3 oldpos)
    {
        // Debug.Log(currentRolling);

        if (!currentRolling) 
        {

        }

        if (currentRolling)
        {
            
            agent.SetDestination(oldpos);
        }

        Vector3 distToPlayerPoint = transform.position - oldpos;

        if (distToPlayerPoint.magnitude < 2f)   
        {
            currentRolling = false;
            return;
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void DestroyEnemy(GameObject gameObject)
    {
        DestroyEnemy(gameObject);
    }

}

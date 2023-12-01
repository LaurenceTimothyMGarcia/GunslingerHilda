using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeleAttack : SkeleState
{
    public bool AttackDone;

    public NavMeshAgent enemy;

    public SkeleChase skeleChase;

    private GameObject Player;

    public float timeBetweenAttacks;

    public Transform playerTransform;

    public GameObject projectile;
    public override SkeleState RunCurrentState()
    {
        if (AttackDone)
        {
            AttackDone = false;
            return skeleChase;
        }
        else
        {
            if (!AttackDone)
            {

                enemy.SetDestination(transform.position);

                transform.LookAt(playerTransform);
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                AttackDone = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

            }
            return this;
        }
    }

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = Player.transform;
    }
    private void ResetAttack()
    {
        AttackDone = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeleChase : SkeleState
{
    public NavMeshAgent enemy;

    private GameObject Player;

    public float DetectionRange;

    public LayerMask whatIsPlayer;

    public Transform playerTransform;

    public bool attackRange;


    public SkeleAttack skeleAttack;
    public override SkeleState RunCurrentState()
    {
        if (attackRange)
        {
            attackRange = false;
;            return skeleAttack;
        }
        else {
            enemy.SetDestination(playerTransform.position);
            attackRange = Physics.CheckSphere(transform.position, DetectionRange, whatIsPlayer);
            

            return this;

        }
    }

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = Player.transform;
    }

   
}

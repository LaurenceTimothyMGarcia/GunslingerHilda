using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeleChase : SkeleState
{
    public NavMeshAgent enemy;
    private GameObject Player;
    public Transform playerTransform;

    public bool attackRange;
    public SkeleAttack skeleAttack;
    public override SkeleState RunCurrentState()
    {
        if (attackRange)
        {
            attackRange = false;
            return skeleAttack;
        }
        else {
            enemy.SetDestination(playerTransform.position);
            return this;
        }
    }

    public void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = Player.transform;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.LogWarning("True");
            attackRange = true;
        }
    }
}

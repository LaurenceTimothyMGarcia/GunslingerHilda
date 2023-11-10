using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeleChase : SkeleState
{
    public NavMeshAgent enemy;
    public Transform Player;

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
            enemy.SetDestination(Player.position);
            return this;
        }
    }
}

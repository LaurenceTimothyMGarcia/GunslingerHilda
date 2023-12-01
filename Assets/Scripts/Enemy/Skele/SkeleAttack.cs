using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleAttack : SkeleState
{
    public bool AttackDone;
    public SkeleChase skeleChase;

    public float timeBetweenAttacks;

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
                //Lovingly taken from SerpentAI
                Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);

                AttackDone = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);

            }
            return this;
        }
    }

    private void ResetAttack()
    {
        AttackDone = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleIdle : SkeleState
{
    public bool canSeePlayer;
    public SkeleChase skeleChase;
    public override SkeleState RunCurrentState()
    {
        if (canSeePlayer)
        {
            return skeleChase;
        }
        else
        {
            return this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("True");
        if (other.gameObject.tag == "Player")
        {
            canSeePlayer = true;
        }
    }
}

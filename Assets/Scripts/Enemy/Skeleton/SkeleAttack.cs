using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleAttack : SkeleState
{
    public bool AnimationIsDone;
    public SkeleChase skeleChase;

    public GameObject HitDetector;
    public override SkeleState RunCurrentState()
    {
        if (AnimationIsDone)
        {
            AnimationIsDone = false;
            return skeleChase;
        }
        else
        {
            HitDetector.SetActive(true);
            // Play Animation 
            // Finish animation set AnimationIsDone true
            // If player is in triggger they take damage
            // HitDetector.SetActive(false);
            Debug.Log("I attackted");
            return this;
        }
    }
}

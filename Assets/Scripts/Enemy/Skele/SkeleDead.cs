using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeleDead : SkeleState
{
    public bool IsAnimationDone;
    public override SkeleState RunCurrentState()
    {
        // Play Death Animation
        // Destroy object
        return this;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkeleState : MonoBehaviour
{
    public abstract SkeleState RunCurrentState();
}

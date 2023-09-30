using System.Collections;
using System.Collections.Generic;
using PlayerInput;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletPowers/FasterProjectileSpeed")]
public class FasterProjectileSpeed : BulletPower
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Gun>().shootForce += amount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBullet : Bullet
{
    public int slowFactor;
    public override void Start()
    {
        this.bulletLifeTime *= 5;
        this.defaultForce /= slowFactor;
        base.Start();
    }
}

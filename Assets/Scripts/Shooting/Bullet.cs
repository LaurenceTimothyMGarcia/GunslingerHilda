using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPowerProps activePowers;

    public void OnCollisionEnter(Collision col)
    {

        // Insert different scripts here
        if (activePowers.fireBullets)
        {
            FireBullets();
        }

        if (activePowers.homingBullets)
        {
            HomingBullets();
        }

        if (activePowers.piercingShots)
        {
            PiercingShots();
        }

        if (activePowers.stunBullets)
        {
            StunBullets();
        }

        if (activePowers.slowBullets)
        {
            SlowBullets();
        }

        if (activePowers.knockbackBullets)
        {
            KnockbackBullets();
        }

        if (activePowers.explosiveBullets)
        {
            ExplosiveBullets();
        }
    }

    public void FireBullets()
    {
        // Insert code here
    }

    public void HomingBullets()
    {
        // Insert code here
    }

    public void PiercingShots()
    {
        // Insert code here
    }

    public void StunBullets()
    {
        // Insert code here
    }

    public void SlowBullets()
    {
        // Insert code here
    }

    public void KnockbackBullets()
    {
        // Insert code here
    }

    public void ExplosiveBullets()
    {
        // Insert code here
    }
}

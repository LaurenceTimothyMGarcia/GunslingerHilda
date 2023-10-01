using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPowerProps activePowers;

    public float bulletLifetime = 5f;

    private void Start()
    {
        Destroy(this.gameObject, bulletLifetime);
    }

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

        // On Collide
        if (!col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                // Deal Damage to enemy
            }

            Destroy(this.gameObject);
        }
    }

    public void FireBullets()
    {
        // Insert code here
        Debug.Log("Fire Bullet");
    }

    public void HomingBullets()
    {
        // Insert code here
        Debug.Log("Homing");
    }

    public void PiercingShots()
    {
        // Insert code here
        Debug.Log("Pierce");
    }

    public void StunBullets()
    {
        // Insert code here
        Debug.Log("Stun");
    }

    public void SlowBullets()
    {
        // Insert code here
        Debug.Log("Slow");
    }

    public void KnockbackBullets()
    {
        // Insert code here
        Debug.Log("Knockback");
    }

    public void ExplosiveBullets()
    {
        // Insert code here
        Debug.Log("Explosive");
    }
}

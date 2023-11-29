using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    public GameObject explosion;
    public int explosionDamage;
    public float explosionRange;
    public float explosionForce;

    public void Explode()
    {
        //create explosion
        if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);

        //check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, 1 << enemyLayer);
        for (int i = 0; i < enemies.Length; i++)
        {
            Rigidbody enemyRb = enemies[i].GetComponent<Rigidbody>();
            Vector3 knockbackDirection = enemyRb.gameObject.transform.position - transform.position;
            enemyRb.AddForce(knockbackDirection * explosionForce, ForceMode.Impulse);
        }
        Destroy(gameObject);
    }

    public override void OnCollisionEnter(Collision col)
    {
        Explode();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}

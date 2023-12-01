using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : Bullet
{
    public GameObject explosion;
    public float explosionDamage;
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
            Collider currentEnemy = enemies[i];
            Rigidbody enemyRb = currentEnemy.gameObject.GetComponent<Rigidbody>();
            Vector3 knockbackDirection = enemyRb.gameObject.transform.position - transform.position;
            enemyRb.AddForce(knockbackDirection * explosionForce, ForceMode.Impulse);

            // take damge from explosion
            currentEnemy.gameObject.GetComponent<DummyHealth>().TakeDamage(explosionDamage);
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

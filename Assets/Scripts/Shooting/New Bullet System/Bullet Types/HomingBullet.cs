using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public override void SetBehavior()
    {
        Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, 20f, enemyLayer); //Detect nearby enemies
        if (detectedEnemies.Length > 0)
        {
            defaultDirection = detectedEnemies[0].gameObject.transform.position - transform.position; //Override default direction to the direction of the first detected nearest enemy
            GetComponent<Rigidbody>().AddForce(defaultDirection * defaultForce, ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(defaultDirection * defaultForce, ForceMode.Impulse);
        }
    }

    public override void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            Debug.Log("Hit Enemy");
        }
        Destroy(this.gameObject);
    }
}

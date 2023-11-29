using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    public override void Start() {
        Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, 20f, 1 << enemyLayer); //Detect nearby enemies
        if (detectedEnemies.Length > 0)
        {
            defaultDirection = detectedEnemies[0].gameObject.transform.position - transform.position; //Override default direction to the direction of the first detected nearest enemy
            GetComponent<Rigidbody>().AddForce(defaultDirection * defaultForce, ForceMode.Impulse);
        }
        else
        {
            base.Start();
        }
    }
}

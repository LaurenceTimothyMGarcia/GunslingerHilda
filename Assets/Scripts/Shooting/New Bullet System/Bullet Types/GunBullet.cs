using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet 
{
    public override void SetBehavior() 
    {
        GetComponent<Rigidbody>().AddForce(defaultDirection * defaultForce, ForceMode.Impulse);
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
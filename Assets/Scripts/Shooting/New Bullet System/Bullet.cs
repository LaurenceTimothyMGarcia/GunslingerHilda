using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //General bullet properties
    public float bulletLifetime;
    public int enemyLayer;

    //Default props passed by the gun object on instantiontion - look inside Shoot()
    public Vector3 defaultDirection;
    public float defaultForce;

    // Start is called before the first frame update
    public void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy"); // Set enemy layer
        SetBehavior();
        Destroy(this.gameObject, bulletLifetime); //Destroy bullet after lifetime
    }
    
    //Must override
    public abstract void SetBehavior(); //Sets bullet behavior on instantiation
    public abstract void OnCollisionEnter(Collision col); //Set bullet behavior on collision
}

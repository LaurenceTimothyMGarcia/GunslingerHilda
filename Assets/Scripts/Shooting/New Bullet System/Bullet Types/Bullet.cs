using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //General bullet properties
    public float bulletLifeTime;
    public LayerMask enemyLayer;

    //Default props passed by the gun object on instantiontion - look inside Shoot()
    public Vector3 defaultDirection;
    public float defaultForce;

    //Set enemy layer, bullet life time, can override this
    public void Awake() {
        bulletLifeTime = 5f;
        enemyLayer = 9;
    }

    //Default behavior
    public virtual void Start()
    {
        GetComponent<Rigidbody>().AddForce(defaultDirection * defaultForce, ForceMode.Impulse);
        Destroy(this.gameObject, bulletLifeTime); //Destroy bullet after lifetime
    }
    
    //Default collision behavior
    public virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            //Apply damage
            Debug.Log("Hit player");
        }

        // Debug.Log("Destrooyed");
        Destroy(this.gameObject);
    }
}

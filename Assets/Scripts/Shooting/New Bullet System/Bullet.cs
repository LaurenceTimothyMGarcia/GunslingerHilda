using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    //General bullet properties
    public float bulletLifetime = 3f; //default lifetime
    public int enemyLayer; //enemy layer
    public Vector3 defaultDirection { get; set; }
    public float defaultForce { get; set; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy"); // Set enemy layer
        Destroy(this.gameObject, bulletLifetime); //Destroy bullet after lifetime
    }

    public abstract void SetBehavior();
    public abstract void OnHit();
}

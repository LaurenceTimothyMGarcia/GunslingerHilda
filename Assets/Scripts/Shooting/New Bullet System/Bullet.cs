using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //General bullet properties
    public float bulletLifetime = 5f; //lifetime
    public LayerMask enemyLayer; //enemy layer

    // Start is called before the first frame update
    public virtual void Start()
    {
        Destroy(this.gameObject, bulletLifetime); //Destroy bullet after lifetime
    }
}

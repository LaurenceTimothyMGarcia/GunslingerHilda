using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : Bullet
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == enemyLayer)
        {
            //Apply damage
        }
        else
        {
            Destroy(gameObject); //Destroy if hits something else than enemy
        }
    }
}

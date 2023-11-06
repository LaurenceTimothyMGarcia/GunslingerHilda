using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("ENTER");
            Debug.Log("ENTER"); 
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("STAY");
        }
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            print("EXIT");
        }
    }
}

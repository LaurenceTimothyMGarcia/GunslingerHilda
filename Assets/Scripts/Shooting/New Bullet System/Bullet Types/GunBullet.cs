using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour {

    public void Start() {
        SetBehavior();
        Destroy(this.gameObject, 5f);
    }
    public void SetBehavior() {
        Debug.Log("Hello from GunBullet");
    }
}
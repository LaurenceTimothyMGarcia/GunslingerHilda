using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject currentBulletType;
    //Bullet types
    public GameObject gunBullet; //0
    public GameObject homingBullet; //1
    public GameObject explosiveBullet; //2
    public GameObject piercingBullet; //3
    
    public Dictionary<int, GameObject> bulletTypesMap; //Maps string names to bullet types to make switching easy


    // Start is called before the first frame update
    void Start()
    {
        //Set up the dictionary
        // bulletTypesMap[0] = gunBullet;
        // bulletTypesMap[1] = homingBullet;
        // bulletTypesMap[2] = explosiveBullet;
        // bulletTypesMap[3] = piercingBullet;
    }

    public void Shoot(Transform gunAttackPoint) {
        Instantiate(currentBulletType);
    }

    public void SetCurrentBulletType(int bulletType) {
        currentBulletType = bulletTypesMap[bulletType];
    }
}

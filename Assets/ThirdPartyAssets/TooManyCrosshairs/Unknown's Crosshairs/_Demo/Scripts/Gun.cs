using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnknownsCrosshairs
{
    public class Gun : MonoBehaviour
    {

        public Crosshair crosshair; //put the UI crosshair object into this field in the inspector
        public float gunRecoil;
        public float settleSpeed;
        public float shotsPerSecond; //how fast this gun shoots


        //used to set up how often the gun shoots as set in shotsPerSecond 
        float shotRate;
        float nextShotTime;

        void Start()
        {

            crosshair.SetShrinkSpeed(settleSpeed);

            //set up the gunshooting speed in this script
            shotRate = 1.0f / shotsPerSecond;
        }

        void Update()
        {
            if (Input.GetButton("Fire1")) // press the mouse1 / left control / controller button 1 to simulate shooting with the given recoil
                Shoot();
        }

        void Shoot() //shoot the gun based on the fire rate set by setting shotsPerSecond
        {
            if (nextShotTime < Time.time)
            {
                crosshair.Expand(gunRecoil);
                nextShotTime = Time.time + shotRate;
            }

        }
    }
}

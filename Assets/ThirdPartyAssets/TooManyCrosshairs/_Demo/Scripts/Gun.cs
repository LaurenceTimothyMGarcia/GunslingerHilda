using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooManyCrosshairs
{
    public class Gun : MonoBehaviour
    {

        public Crosshair crosshair; //put the UI crosshair object into this field in the inspector
        public float gunRecoil;
        public float reloadSpeed;
        public float settleSpeed;
        public float maxCrossHairSize;
        public float shotsPerSecond; //how fast this gun shoots

        public Color specialColor;

        //used to set up how often the gun shoots as set in shotsPerSecond 
        float shotRate;
        float nextShotTime;

        void Start()
        {

            //sets behaviour of the crosshair to match the way the gun shoots
            crosshair.SetShrinkSpeed(settleSpeed);
            crosshair.SetReloadSpeed(reloadSpeed);
            crosshair.SetMaxScale(maxCrossHairSize);

            //set up the gunshooting speed in this script
            shotRate = 1.0f / shotsPerSecond;
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.R))  // press the 'R' key to call the reload function in the crosshair script
                crosshair.DoReload();

            if (Input.GetButton("Fire1")) // press the mouse1 / left control / controller button 1 to simulate shooting with the given recoil
                Shoot();


            if (Input.GetKey(KeyCode.E)) // hold 'E' to show alternate textures
                ShowAlts();
            else
                HideAlts();

            if (Input.GetKey(KeyCode.Space)) //hold 'Space Bar' to display selected color/tint 
                EnableTint();
            else
                DisableTint();
        }

        void Shoot() //shoot the gun based on the fire rate set by setting shotsPerSecond
        {
            if (nextShotTime < Time.time)
            {
                crosshair.ExpandCrosshair(gunRecoil);
                nextShotTime = Time.time + shotRate;
            }

        }

        void ShowAlts() // tells the crosshair to show the alternate textures
        {
            crosshair.ShowAlternates();
        }

        void HideAlts() // tells the crosshair to show the default textures
        {
            crosshair.HideAlternates();
        }

        void EnableTint() // tells the crosshair to show the alternate color
        {
            crosshair.EnableTint(specialColor);
        }

        void DisableTint() // tells the crosshair to show the default color
        {
            crosshair.DisableTint();
        }
    }
}

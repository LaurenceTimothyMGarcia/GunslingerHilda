using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInput
{
    public class Gun : MonoBehaviour
    {
        // Bulet force
        public float shootForce;
        public float upwardForce;

        // Gun Stats
        public float timeBetweenShooting;
        public float spread;
        public float reloadTime;
        public float timeBetweenShots;

        [Tooltip("Max amount of bullets")]
        public int magazineSize;
        public int bulletsPerTap;

        public bool allowButtonHold;

        // Current amount of bullets
        int bulletsLeft;
        int bulletsShot;

        // State machine for the gun
        public GunState state;
        public enum GunState
        {
            shooting,
            readyToShoot,
            reloading
        }

        // References
        public Camera cam;
        public Transform attackPoint;

        // Bug fixing
        public bool allowInvoke = true;

        public GameObject currentBulletType;
        //Bullet types
        public GameObject bullet; //Default bullet
        public GameObject homingBullet; //Homing bullet
        public GameObject explosiveBullet; //Explosive bullet
        public GameObject piercingBullet; //Piercing bullet
        public GameObject slowBullet; //Slow bullet


        private void Awake()
        {
            // Ensure clip is full
            bulletsLeft = magazineSize;

            state = GunState.readyToShoot;  

            cam = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            GunInput();
        }

        private void GunInput()
        {
            // Reload button pressed or if player shoots but is out of ammo
            if ((PlayerInputManager.Instance.reloadPressed() && bulletsLeft < magazineSize && state != GunState.reloading) ||
                (state == GunState.readyToShoot && PlayerInputManager.Instance.firePressed() && bulletsLeft <= 0))
            {
                Reload();
            }

            // Shooting
            if (state == GunState.readyToShoot
                && PlayerInputManager.Instance.firePressed()
                && bulletsLeft > 0)
            {
                // Set bullet shots to 0
                bulletsShot = 0;

                Shoot();
            }
        }

        private void Shoot()
        {
            state = GunState.shooting;

            // Find position to hit with a raycast
            // Raycast to the middle of the screen
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            // Check if ray hits something
            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag != "Player")
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75); // random point far away from the player
            }

            // calculate direction
            Vector3 directionNoSpread = targetPoint - attackPoint.position;

            // Calc Spread
            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 directionSpread = directionNoSpread + new Vector3(x, y, 0);

//----------------------------------------------------------------------------------------------------------------------------------//

            // Instanciate the bullet
            GameObject currentBullet = Instantiate(currentBulletType, attackPoint.position, Quaternion.identity);

            //Pass default bullet props
            currentBullet.GetComponent<Bullet>().defaultForce = shootForce;
            currentBullet.GetComponent<Bullet>().defaultDirection = directionNoSpread;

            // Upward force Only necessary if bombs or grenades
            // currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

//----------------------------------------------------------------------------------------------------------------------------------//

            bulletsLeft--;
            bulletsShot++;

            // Reset the shot
            if (allowInvoke)
            {
                // Function calls after timebetweenshooting seconds
                Invoke("ResetShot", timeBetweenShooting);
                allowInvoke = false;
            }

            // If more than one bullet per tap like a shotgun
            if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            {
                Invoke("Shoot", timeBetweenShots);
            }
        }


        private void ResetShot()
        {
            state = GunState.readyToShoot;
            allowInvoke = true;
        }

        private void Reload()
        {
            state = GunState.reloading;
            Invoke("ReloadFinished", reloadTime);
        }

        private void ReloadFinished()
        {
            bulletsLeft = magazineSize;
            state = GunState.readyToShoot;
        }

        public void SetCurrentBulletType(GameObject bulletType) {
            currentBulletType = bulletType;
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(attackPoint.position, cam.transform.forward * 100f);
        }
    }
}

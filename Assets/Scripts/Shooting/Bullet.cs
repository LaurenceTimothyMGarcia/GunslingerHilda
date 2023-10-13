using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletPowerProps activePowers;

    public float bulletLifetime = 5f;
    
    public string testVariable;

    public GameObject testEnemy;

    // Temporary fix
    public LayerMask layer;

    private void Awake()
    {
        //Moved homing bullets to Awake() as they need to detect the nearest enemy.
        if (activePowers.homingBullets)
        {
            HomingBullets();
        }
    }

    private void Start()
    {
        Destroy(this.gameObject, bulletLifetime);
    }

    public void OnCollisionEnter(Collision col)
    {

        // Insert different scripts here
        if (activePowers.fireBullets)
        {
            FireBullets();
        }

        //if (activePowers.homingBullets)
        //{
        //    HomingBullets();
        //}

        if (activePowers.piercingShots)
        {
            PiercingShots();
        }

        if (activePowers.stunBullets)
        {
            StunBullets();
        }

        if (activePowers.slowBullets)
        {
            SlowBullets();
        }

        if (activePowers.knockbackBullets)
        {
            KnockbackBullets();
        }

        if (activePowers.explosiveBullets)
        {
            ExplosiveBullets();
        }

        // On Collide
        if (!col.gameObject.CompareTag("Player"))
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                // Deal Damage to enemy
            }

            Destroy(this.gameObject);
        }
    }

    public void FireBullets()
    {
        // Insert code here
        Debug.Log("Fire Bullet");
    }

    public void HomingBullets()
    {
            // Insert code here
            Collider[] detectedEnemies = Physics.OverlapSphere(transform.position, 20f, layer);

            if (detectedEnemies.Length > 0)
            {
                Transform attackPoint = GameObject.FindWithTag("Gun").GetComponent<PlayerInput.Gun>().attackPoint;

                Camera cam = GameObject.FindWithTag("Gun").GetComponent<PlayerInput.Gun>().cam;
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
                
                Vector3 directionNoSpread = targetPoint - attackPoint.position;

                // Calc Spread
                float spread = GameObject.FindWithTag("Gun").GetComponent<PlayerInput.Gun>().spread;
                float x = Random.Range(-spread, spread);
                float y = Random.Range(-spread, spread);

                Vector3 directionSpread = directionNoSpread + new Vector3(x, y, 0);
                gameObject.GetComponent<Rigidbody>().AddForce(-directionSpread.normalized * GameObject.FindWithTag("Gun").GetComponent<PlayerInput.Gun>().shootForce, ForceMode.Impulse);

                transform.forward = (detectedEnemies[0].transform.position - transform.position).normalized;
                gameObject.GetComponent<Rigidbody>().AddForce((detectedEnemies[0].transform.position - transform.position).normalized * GameObject.FindWithTag("Gun").GetComponent<PlayerInput.Gun>().shootForce, ForceMode.Impulse);
            }        
        Debug.Log("Homing");
    }

    public void PiercingShots()
    {
        // Insert code here
        Debug.Log("Pierce");
    }

    public void StunBullets()
    {
        // Insert code here
        Debug.Log("Stun");
    }

    public void SlowBullets()
    {
        // Insert code here
        Debug.Log("Slow");
    }

    public void KnockbackBullets()
    {
        // Insert code here
        Debug.Log("Knockback");
    }

    public void ExplosiveBullets()
    {
        // Insert code here
        Debug.Log("Explosive");
    }
}

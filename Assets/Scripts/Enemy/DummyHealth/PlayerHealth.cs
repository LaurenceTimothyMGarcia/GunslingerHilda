using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public EnemyHealth healthBar;

    public float maxHealth = 100;
    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.UpdateHealthBar(health, maxHealth);
        if (health <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Player Collide");
        if (col.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(col.gameObject.GetComponent<Damage>().damage);
        }
    }
}

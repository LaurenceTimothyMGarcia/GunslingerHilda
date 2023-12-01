using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public EnemyHealth healthBar;

    public float maxHealth = 100;
    public float health = 100;

    private void Awake()
    {
        // healthBar = GetComponentInChildren<EnemyHealth>();
    }

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
        if (col.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(col.gameObject.GetComponent<Damage>().damage);
        }
    }
}

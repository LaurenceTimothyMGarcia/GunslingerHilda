using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Cobbled together from seeing some things from the Healthbar tutorial and other things and intuition.
// Learned that SerializeField makes it available AND editable in Unity!
public class DummyHealth : MonoBehaviour
{
    [SerializeField] EnemyHealth healthBar;

    [SerializeField] float maxHealth = 100;
    [SerializeField] float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private void Awake()
    {
        healthBar = GetComponentInChildren<EnemyHealth>();
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
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(this.gameObject);
    }

    // The Toiler flashbacks (BUT BETTER BECAUSE THE DAMAGE IS CONTAINED WITHIN THE ENEMY SCRIPT, NOT THE HITBOX OF THE BULLET WOOOOOOO)
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bullet"))
        {
           TakeDamage(col.gameObject.GetComponent<Damage>().damage);
        }
        
    }
}

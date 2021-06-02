using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] TheSceneManager theSceneManager;
    //public GameObject deathEffect;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    { 
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("enemyCrate"))
        {
            IncreaseHP();
            Destroy(collision.gameObject);
        }
    }
   
        
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
    public void IncreaseHP()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        theSceneManager.LoadLoseScreen();
    }
}
    


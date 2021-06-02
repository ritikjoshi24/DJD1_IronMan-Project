using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject[] spawnes;
    [SerializeField] private int health = 100;
    int randomInt;

    public void takeDamage (int damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Score.scoreValue += 1;
        
        SpawnRandom();
        Destroy(gameObject);
    }
   
    void SpawnRandom()
    {
        randomInt = Random.Range(0, spawnes.Length);
        Instantiate(spawnes[randomInt], transform.position, Quaternion.identity);
    }
}

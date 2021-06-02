using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private int damage = 40;
    public Rigidbody2D rb;
   // public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity =  speed * transform.right;  
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
       EnemyHealth enemy = hitInfo.GetComponent<EnemyHealth>();
        if (enemy!= null)
        {
            enemy.takeDamage(damage);
        }
       // Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

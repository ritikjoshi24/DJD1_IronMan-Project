using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;

    Rigidbody2D enemy_rb;
    // Start is called before the first frame update
    void Start()
    {
        enemy_rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAi();
    }

    private void EnemyAi()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        if (distToPlayer < agroRange)
        {
            ChasePlayer();
        }
        else
        {
            StopChasingPlayer();
        }
        
    }

    private void StopChasingPlayer()
    {
        enemy_rb.velocity = new Vector2(0, 0);
    }

    private void ChasePlayer()
    {
        //enemy is on the left side of the player so move right
        if (transform.position.x < player.position.x) 
        {
            enemy_rb.velocity = new Vector2(moveSpeed, 0);
        }
        //enemy is on the right side of the player so move left
        else 
        {
            enemy_rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }
}

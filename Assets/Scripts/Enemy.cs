using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float agroRange;
    [SerializeField] float moveSpeed;
    [SerializeField] bool movingRight;
    [SerializeField] bool playerDetected;
    [SerializeField] float rayDistance;


    [SerializeField]Transform groundDetection;
    


    public Rigidbody2D enemy_rb;
    
    void Start()
    {
        
        enemy_rb = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
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
        Patrol();
    }

    private void ChasePlayer()
    {
        //enemy is on the left side of the player so move right
        if (transform.position.x < player.position.x) 
        {
            enemy_rb.velocity = new Vector2(moveSpeed, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //enemy is on the right side of the player so move left
        else 
        {
            enemy_rb.velocity = new Vector2(-moveSpeed, 0);
            
            transform.localRotation = Quaternion.Euler(0, -180, 0);
            
        }
    }

    private void Patrol() 
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        RaycastHit2D groundinfo = Physics2D.Raycast(groundDetection.position, Vector2.down, rayDistance);
        
        if (groundinfo.collider == false )
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyFollow1 : MonoBehaviour
{
    public float walkSpeed, range, timebtwShots, shootSpeed;
    private float distToPlayer;
    public Animator animator;
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn, canShoot;

    public Rigidbody2D rb;
    public Transform groundCheckPos;
    public LayerMask groundLayer;
    public Collider2D bodyCollider;
    public Transform player, shootPos;
    public GameObject bullet;
    Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        mustPatrol = true;
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (mustPatrol)
        {
            
            Patrol();
        }
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= range )
        {
            
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                
                Flip();
                shootPos.transform.Rotate(0, 180, 0);
                
            }

            mustPatrol = false;
            if(groundCheckPos)
            {
                rb.velocity = new Vector2(0, 0);
                /* rb.velocity = new Vector2(3 * walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
                 if (distToPlayer <= shootingRange)
                 {
                     rb.velocity = new Vector2(0, rb.velocity.y);
                 }*/
                if (canShoot == true)
                {
                    animator.SetBool("Shooting", true);
                    StartCoroutine(shoot());
                  //  
                }      
            }
           
        }
        else
        {
             mustPatrol = true;
            animator.SetBool("Shooting", false);
        }
        
    }
    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 1.0f, groundLayer);
        }
    }
    void Patrol()
    {
       
        if (mustTurn || bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
       
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    IEnumerator shoot()
    {
        canShoot = false;

        yield return new WaitForSeconds(timebtwShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, shootPos.rotation);
        moveDirection = (player.position - transform.position).normalized * shootSpeed * Time.fixedDeltaTime;
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection.x, moveDirection.y);
        
        canShoot = true;
        
    }

    private void StopChasingPlayer()
    {
        mustPatrol = true;
    }

    private void ChasePlayer()
    {
        //enemy is on the left side of the player so move right
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(walkSpeed, 0);
        }
        //enemy is on the right side of the player so move left
        else
        {
            rb.velocity = new Vector2(-walkSpeed, 0);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        
    }
}
   

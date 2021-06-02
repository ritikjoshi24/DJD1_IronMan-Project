using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 20f;

    float horizontalMove = 0f;
    bool jump = false;
    
    private float runboost = 10f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("Jumping", true);
            
        }
    }

    public void OnLanding()
    {
        animator.SetBool("Jumping", false);
        animator.SetInteger("Flight Trigger", 0);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove* Time.fixedDeltaTime, jump);
        jump = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("runUpgrade"))
        {
            runSpeed = 40f;
            StartCoroutine(runUpgrade());
            Destroy(collision.gameObject);
        }
    }

    IEnumerator runUpgrade()
    {
        yield return new WaitForSeconds(runboost);
        runSpeed = 20f;
        
    }

}

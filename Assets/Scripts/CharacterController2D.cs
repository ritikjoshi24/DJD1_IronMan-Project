using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 700f;							// Amount of force added when the player jumps.
	//[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	
    [SerializeField] int flightTrigger;
    [SerializeField] private float flyspeed = 35;
    public Animator animator;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
    [SerializeField] private Playermovement run;
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	public Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	[SerializeField] float energy;
	[SerializeField] float maxEnergy = 100f;
    private float flyboost = 0f;
	EnergyBar energyBar;


	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
    
    

    private void Awake()
	{
		energyBar = FindObjectOfType<EnergyBar>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        run = GetComponent<Playermovement>();
        energyBar.SetMaxEnergy(maxEnergy);
        if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		energyBar.SetEnergy(energy);
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
                flightTrigger = 0;
				Regen(0.5f);


				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump)
	{
		

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (flightTrigger == 1)
            {
                Flight();
            }
            else if (flightTrigger > 1)
            {
                flightTrigger = 0;
                //Changes back to the normal 2d movement rigidbody
                
            }

            if (jump)
            {
                flightTrigger++;


            }
            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            
        }

       
        
    }

    private void Flight()
    {
        
        //Horizontal and Vertical input references
        float moveHorizontally = Input.GetAxis("Horizontal");
        float moveVertically = Input.GetAxis("Vertical");
        //The vector that uses them multiplied by the playerSpeed
        Vector3 movement = new Vector3(moveHorizontally, moveVertically, 0) * (run.runSpeed - flyspeed);
        m_Rigidbody2D.velocity = movement;
        animator.SetInteger("Flight Trigger", flightTrigger);
        StartCoroutine(upgrade());
        

    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
	}

	private void Regen(float energytoadd)
	{
		energy += energytoadd % 2;
		ReachMaxEnergy();
	}

	private void ReachMaxEnergy()
	{

		if (energy > maxEnergy)
		{
			energy = maxEnergy;
		}
	}
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("flyUpgrade"))
        {
            energy = maxEnergy;
            energyBar.SetMaxEnergy(maxEnergy);
            flyboost = 2f;
            Destroy(collision.gameObject);
        }
    }

    IEnumerator upgrade()
    {
        yield return new WaitForSeconds(flyboost);
        flyboost = 0f;
        energy--;
        if (energy <= 0)
        {
            flightTrigger = 0;
          
        }
    }
}

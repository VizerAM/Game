using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private LayerMask m_WhatIsPlatform;
    [SerializeField] private LayerMask m_WhatIsLadders;
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_Clinb;
    private LayerMask GroundMask;


    const float k_CeilingRadius = .1f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

    private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

    public void Update()
    {
        
    }

    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        GroundMask = 0;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround + m_WhatIsPlatform + m_WhatIsLadders);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{

                GroundMask = colliders[i].gameObject.layer;


				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, float moveV , bool jump)
	{

        m_Animator.SetFloat("horizontalSpeed", Mathf.Abs(move));
        m_Animator.SetFloat("verticalSpeed", m_Rigidbody2D.velocity.y);
        m_Animator.SetBool("OnGround", m_Grounded);

        bool crouch = false;
        bool climb = false;



        if(m_Clinb)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsLadders) && Physics2D.OverlapCircle(m_GroundCheck.position, k_CeilingRadius, m_WhatIsLadders))
            {
                m_Clinb = true;

            }
            else
            {
                m_Clinb = false;
            }
        }
        




        if(moveV != 0)
        {
            if(Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsLadders) && moveV > 0 || Physics2D.OverlapCircle(m_GroundCheck.position, k_CeilingRadius, m_WhatIsLadders ) && moveV < 0)
            {
                m_Clinb = true;
                climb = true;
                moveV *= 10f;
                
            }
            else if(moveV < 0)
            {
                crouch = true;
                moveV = m_Rigidbody2D.velocity.y;
            }
            else
            {

               moveV = m_Rigidbody2D.velocity.y;
            }

        }
        else
        {
            if(!m_Clinb)
            {
                moveV = m_Rigidbody2D.velocity.y;
            }
            else
            {
                moveV = 0;
            }

            
        }

        if(m_Clinb)
        {
            m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        m_Animator.SetBool("Climb", m_Clinb);

        
        

        // If crouching, check to see if the character can stand up
        if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}



			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, moveV);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

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
        if (m_Grounded && jump && !(GroundMask == LayerMask.NameToLayer("Platform") && crouch))
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
        m_Animator.SetBool("Crouch", crouch);
    }





	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{

    [SerializeField] private float m_JumpForce = 400f;                         
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private LayerMask m_WhatIsGround;                          
    [SerializeField] private LayerMask m_WhatIsPlatform;
    [SerializeField] private LayerMask m_WhatIsLadders;
    [SerializeField] private Transform m_GroundCheck;							
    [SerializeField] private Transform m_CeilingCheck;                          
    [SerializeField] private Collider2D m_CrouchDisableCollider;




    private bool doubleJump = false;
    private float JumpColdown = 0.1f;
    private float LasJunpTime;
    private  int JupCount;
    

    const float k_GroundedRadius = .1f; 
    private bool m_Grounded;            
    private bool m_Clinb;
    private LayerMask GroundMask;


    const float k_CeilingRadius = .1f;
    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private bool m_FacingRight = true;  
    private Vector3 m_Velocity = Vector3.zero;


    private bool m_wasCrouching = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
    }

    public void Update()
    {

    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        GroundMask = 0;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround + m_WhatIsPlatform + m_WhatIsLadders);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {

                GroundMask = colliders[i].gameObject.layer;


                m_Grounded = true;
                JupCount = 0;
            }
        }


    }


    public void Move(float Horisontal, float Vertical, bool jump)
    {

        m_Animator.SetFloat("horizontalSpeed", Mathf.Abs(Horisontal));
        m_Animator.SetFloat("verticalSpeed", m_Rigidbody2D.velocity.y);
        m_Animator.SetBool("OnGround", m_Grounded);

        bool crouch = false;


        bool LaddersUp = Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsLadders);
        bool LaddersDown = GroundMask == LayerMask.NameToLayer("Ladders");


        if(m_Clinb)
        {
            m_Clinb = LaddersDown && LaddersUp;
        }

        if (Vertical != 0)
        {
            if (LaddersUp && Vertical > 0 ||  LaddersDown && Vertical < 0)
            {
                m_Clinb = true;
                Vertical *= 10f;

            }
            else if (Vertical < 0)
            {
                crouch = true;
                Vertical = m_Rigidbody2D.velocity.y;
            }
            else
            {

                Vertical = m_Rigidbody2D.velocity.y;
            }

        }
        else
        {
            if (!m_Clinb)
            {
                Vertical = m_Rigidbody2D.velocity.y;
            }
            else
            {
                Vertical = 0;
            }


        }

        if (m_Clinb)
        {
            m_Rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        m_Animator.SetBool("Climb", m_Clinb);




        if (!crouch)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        if (m_Grounded || m_AirControl)
        {

            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                }
                Horisontal *= m_CrouchSpeed;
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                }
            }


            Vector3 targetVelocity = new Vector2(Horisontal * 10f, Vertical);
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            if (Horisontal > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (Horisontal < 0 && m_FacingRight)
            {
                Flip();
            }
        }



        if (m_Grounded && jump && !(GroundMask == LayerMask.NameToLayer("Platform") && crouch))
        {
            Jump();
            
        }
        else if(jump && doubleJump && JupCount == 0 && JumpColdown < Time.time - LasJunpTime && !(GroundMask == LayerMask.NameToLayer("Platform")))
        {
            Jump();
            JupCount++;
        }
        m_Animator.SetBool("Crouch", crouch);
    }



    public void Jump()
    {
        m_Grounded = false;
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
        m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        LasJunpTime = Time.time;
    }

    public void DoubleJumpActive(float DoblrJumpTime)
    {
        StartCoroutine(DoubleJump(DoblrJumpTime));
    }

    IEnumerator DoubleJump(float DoblrJumpTime)
    {
        doubleJump = true;


        yield return new WaitForSeconds(DoblrJumpTime);

        doubleJump = false;
    }



    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

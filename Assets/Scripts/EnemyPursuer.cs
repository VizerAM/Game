using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPursuer : MonoBehaviour
{

    public float JumpForse;
    public float MoveSpead;
    public Animator animator;

    private Rigidbody2D rb;
    [SerializeField] private LayerMask Target;


    private Vector3 target;

    private bool FacingRight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        FacingRight = true;

    }

    private void Flip()
    {
        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



    public void Jump()
    {

        Collider2D TargetObject = Physics2D.OverlapCircle(transform.position, 5.0f, Target);
        if(TargetObject != null)
        {
            if (transform.position.x - TargetObject.transform.position.x > 0 && !FacingRight)
            {
                Flip();
            }
            else if (transform.position.x - TargetObject.transform.position.x < 0 && FacingRight)
            {
                Flip();
            }

            rb.AddForce(new Vector2(MoveSpead * -transform.localScale.x, JumpForse));

        }
        
    }

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        animator.SetFloat("verticalSpeed", rb.velocity.y);
    }

}

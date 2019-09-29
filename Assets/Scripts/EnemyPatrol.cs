using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public float speed;

    private Rigidbody2D rb;
    private Vector3 target;

    private bool FacingRight;

    private void Awake()
    {
        target = PointA.position;
        rb = GetComponent<Rigidbody2D>();

        if(transform.position.x - target.x > 0)
        {
            FacingRight = false;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void FixedUpdate()
    {
        if( Vector3.Distance(new Vector3(transform.position.x,0), new Vector3(target.x,0)) < 0.1) 
        {
            if(target == PointA.position)
            {
                target = PointB.position;
            }
            else
            {
                target = PointA.position;
            }

            Flip();

        }
        else
        {
            
            if(FacingRight)
            {
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime * -1, rb.velocity.y);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public List<Transform> route;
    public float speed;
    private int tatargetIndex;
    private Vector3 target;

    public bool flip = false;
    private bool FacingRight = false;




    // Update is called once per frame

    private void FixedUpdate()
    {

        if (transform.position != route[tatargetIndex].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, route[tatargetIndex].position, speed * Time.fixedDeltaTime);
        }
        else
        {

            if (tatargetIndex == route.Count - 1)
            {
                tatargetIndex = 0;
            }
            else
            {
                tatargetIndex++;
                
            }

            if (flip)
            {
                if (transform.position.x - route[tatargetIndex].position.x > 0 && !FacingRight)
                {
                    Flip();
                }
                else if (transform.position.x - route[tatargetIndex].position.x < 0 && FacingRight)
                {
                    Flip();
                }
            }

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

}

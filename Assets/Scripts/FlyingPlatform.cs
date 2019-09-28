using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPlatform : MonoBehaviour
{
    public List<Transform> route;
    public float speed;
    private int tatargetIndex;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    public void FixedUpdate()
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
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }


}

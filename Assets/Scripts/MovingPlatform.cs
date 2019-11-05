using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }


    public void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Vector3 Direction;
    public float speed = 1;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Character>().Hurt();
        }

        Destroy(gameObject);
    }


    private void Start()
    {
        //Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction , speed * Time.fixedDeltaTime);
    }
}

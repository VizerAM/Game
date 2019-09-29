using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Vector3 Direction;
    public float speed = 1;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + Direction , speed * Time.fixedDeltaTime);
    }
}

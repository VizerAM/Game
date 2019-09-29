using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сherry : MonoBehaviour
{
    public GameObject PickUpEfect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(PickUpEfect).transform.position = transform.position;
        Destroy(gameObject);
    }

}

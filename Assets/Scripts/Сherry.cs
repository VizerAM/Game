using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сherry : MonoBehaviour
{
    public GameObject PickUpEfect;
    public float DoobleJumpTime = 30;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<CharacterController2D>().DoubleJumpActive(DoobleJumpTime) ;
            Instantiate(PickUpEfect).transform.position = transform.position;
            Destroy(gameObject);
        }

    }

}

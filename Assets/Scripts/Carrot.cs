using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    public GameObject PickUpEfect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Character>().AddHealthPoint();
            if(PickUpEfect != null)
                Instantiate(PickUpEfect).transform.position = transform.position;
            Destroy(gameObject);
        }
    }

}

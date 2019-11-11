using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public GameObject DieEfect;

    const float radius = .2f;

    public void Hurt()
    {
        Instantiate(DieEfect).transform.position = transform.position;
        Destroy(gameObject);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            List<ContactPoint2D> ContactPoints = new List<ContactPoint2D>();
            collision.GetContacts(ContactPoints);

            foreach(ContactPoint2D point in ContactPoints)
            {

                if (point.normal.y >= -0.5f)
                {
                    
                    collision.gameObject.GetComponent<Character>().Hurt();
                    break;

                }
                else
                {
                    collision.gameObject.GetComponent<CharacterController2D>().Jump();
                    Hurt();
                }
            }

            

        }
    }
    


    private void FixedUpdate()
    {


    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

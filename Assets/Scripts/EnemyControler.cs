using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public GameObject DieEfect;
    public Transform TopPoint;

    public LayerMask WatIsPlayer;

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
            collision.gameObject.GetComponent<CharacterController2D>();
            Debug.Log("PaerHit");

        }
    }
    


    private void FixedUpdate()
    {
        Collider2D colliders = Physics2D.OverlapCircle(TopPoint.position, radius, WatIsPlayer);

        if(colliders != null)
        {
            CharacterController2D Player = colliders.gameObject.GetComponent<CharacterController2D>();
            Player.EnemyAddForse();
            Hurt();
        }

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

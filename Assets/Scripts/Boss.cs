
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    private int stage = 0;

    [SerializeField] private float InvulnerabilityTime = 5f;
    private Animator animator;
    private bool invulnerability = false;



    public List<Transform> route;
    private float speed;
    private int tatargetIndex = 1;
    private Vector3 target;

    public bool flip = false;
    private bool FacingRight = false;

    public GameObject DieEfect;

    const float radius = .2f;

    public void Death(Collider2D collider)
    {
        if (DieEfect != null)
            Instantiate(DieEfect).transform.position = transform.position;
        Destroy(gameObject);

        GetComponent<EndPoint>().EnGame(collider);

    }


    public void Hurt(Collider2D collider)
    {
        stage++;

        
        if(stage == 3)
        {

            Death(collider);
        }else
        {
            StartCoroutine(Invulnerability(InvulnerabilityTime));
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            List<ContactPoint2D> ContactPoints = new List<ContactPoint2D>();
            collision.GetContacts(ContactPoints);

            foreach (ContactPoint2D point in ContactPoints)
            {

                if (point.normal.y >= -0.5f)
                {

                    collision.gameObject.GetComponent<Character>().Hurt();
                    break;

                }
                else
                {
                    collision.gameObject.GetComponent<CharacterController2D>().Jump();
                    Hurt(collision.collider);
                }
            }



        }
    }

    IEnumerator Invulnerability(float InvulnerabilityTime)
    {

        invulnerability = true;

        Vector3 Target;

        int Player = LayerMask.NameToLayer("Player");
        int Enemy = LayerMask.NameToLayer("Enemy");


        Physics2D.IgnoreLayerCollision(Enemy, Player);

        animator.SetLayerWeight(1, 1);

        //foreach (Collider2D collider2D in colliders)
        //{
        //    collider2D.enabled = false;
        //    collider2D.enabled = true;
        //}

        yield return new WaitForSeconds(InvulnerabilityTime);

        animator.SetLayerWeight(1, 0);

        Physics2D.IgnoreLayerCollision(Enemy, Player, false);




        invulnerability = false;

    }

    private void FixedUpdate()
    {


        if(!invulnerability)
        {
            if (transform.position != target)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.fixedDeltaTime);
            }
            else
            {

                NextTarget();

                if (flip)
                {
                    if (transform.position.x - target.x > 0 && !FacingRight)
                    {
                        Flip();
                    }
                    else if (transform.position.x - target.x < 0 && FacingRight)
                    {
                        Flip();
                    }
                }
            }

        }

    }

    public void NextTarget()
    {



        if(stage == 0)
        {
            speed = 0;
            target = route[0].position;
        }else if(stage == 1)
        {
            speed = 5;
            
            if(Random.Range(1,6) == 6)
            {
                tatargetIndex = 6;
            }

            target = route[tatargetIndex].position;

            tatargetIndex++;
            if(tatargetIndex == route.Count)
            {
                tatargetIndex = 1;
            }
        }
        else
        {
            speed = 10;
            Random random = new Random();
            tatargetIndex = Random.Range(1, route.Count);
            target = route[tatargetIndex].position;
        }

        if(tatargetIndex == 6)
        {
            speed = 20;
        }
    }

    public void Start()
    {    
        animator = GetComponent<Animator>();

    }

    private void Flip()
    {

        FacingRight = !FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }



}

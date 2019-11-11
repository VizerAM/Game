using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [SerializeField] private int HealthPoints = 3;
    [SerializeField] private float InvulnerabilityTime = 5f;
    [SerializeField] private Text HealthpPointText;
    [SerializeField] private Text NumbeOfStarsText;
    [SerializeField] private EndGameMenu EndGame;


    public bool noHurt = true;

    private int Stars = 0;
    private Animator animator;
    private bool invulnerability = false;
    private Collider2D[] colliders; 

    public void Start()
    {
        HealthpPointText.text = HealthPoints.ToString();
        NumbeOfStarsText.text = Stars.ToString() + "/3";
        animator = GetComponent<Animator>();
        colliders = GetComponents<Collider2D>();
        LayerMask.GetMask();
       
    }

    public int GetStars()
    {
        return Stars;
    }

    public void PicUpStar()
    {   
        if(Stars < 3)
            Stars++;
        NumbeOfStarsText.text = Stars.ToString() + "/3";
    }

    public void AddHealthPoint()
    {
        HealthPoints++;
        HealthpPointText.text = HealthPoints.ToString();
    }

    public void Hurt()
    {
        //Debug.Log(invulnerability);
        if(!invulnerability)
        {
            if (HealthPoints > 0)
            {
                noHurt = false;
                StartCoroutine(Invulnerability(InvulnerabilityTime));
                HealthPoints--;
                HealthpPointText.text = HealthPoints.ToString();
                
            }
            else
            {
                EndGame.EndGame();
            }
        }
        



    }

    IEnumerator Invulnerability(float InvulnerabilityTime)
    {

        invulnerability = true;



        int Player = LayerMask.NameToLayer("Player");
        int Enemy = LayerMask.NameToLayer("Enemy");


        Physics2D.IgnoreLayerCollision(Enemy,Player);

        animator.SetLayerWeight(1, 1);

        foreach (Collider2D collider2D in colliders)
        {
            collider2D.enabled = false;
            collider2D.enabled = true;
        }

        yield return new WaitForSeconds(InvulnerabilityTime);

        animator.SetLayerWeight(1, 0);

        Physics2D.IgnoreLayerCollision(Enemy, Player,false);


        

        invulnerability = false;

    }


}

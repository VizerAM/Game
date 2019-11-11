using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D characterController;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    private bool speedUp = false;

    private bool jump = false;
    //private bool crouch = false;



    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;




        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        



    }


    public void SpeedUpActive(float SpeadUpTime)
    {
        if(!speedUp)
        {
            StartCoroutine(SpeedUp(SpeadUpTime));
        }
    }

    IEnumerator SpeedUp(float SpeadUpTime)
    {
        float defoltSpead = runSpeed;
        runSpeed *= 2;
        speedUp = true;
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(SpeadUpTime * 0.5f );

        runSpeed = defoltSpead;
        speedUp = false;
        Time.timeScale = 1;
    }


    private void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, jump);

        

        jump = false;
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D characterController;

    public float runSpeed = 40f;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;


    private bool jump = false;
    private bool crouch = false;

    private bool onGround = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;




        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        //if(Input.GetButtonDown("Crouch"))
        //{
        //    crouch = true;
        //}
        //if(Input.GetButtonUp("Crouch"))
        //{
        //    crouch = false;
        //}

        



    }


    private void FixedUpdate()
    {
        characterController.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime, jump);

        

        jump = false;
        
    }

}

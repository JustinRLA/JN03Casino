using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    //Mouvement
    public bool canMove = true;
    public float speed = 6.0f;
    public float jumpSpeed = 5.0f;
    public float slowSpeed = 4.0f;
    public float jumpForce = 8.0f;
    public Vector3 moveDirection = Vector3.zero;

    //Player Control Inputs
    public string horizontalMove = "";
    public string verticalMove = "";
    public string jump = "";
    public string activate = "";

    // Private variables.  These will not be accessible from any other class.
    public bool isGrounded = false;

    private CharacterController controller;


    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            Movement(controller);
        }
    }

    void Movement(CharacterController controller)
    {
        //Get input
        float xSpeed = Input.GetAxis(horizontalMove);
        float zSpeed = Input.GetAxis(verticalMove);

        //Get axis
        //Axis de mouvement
        moveDirection = new Vector3(xSpeed * Time.deltaTime * 50, 0, zSpeed * Time.deltaTime * 50);
        moveDirection = transform.TransformDirection(moveDirection);

        //On ground and using power
        if (controller.isGrounded && Input.GetButton(activate))
        {
            //Slower speed for channeling
            moveDirection *= slowSpeed;

            //ANIMATION TRIGGER: ACTIVATING AND ACTIVE IDLE

            //Listen for jump
            if (Input.GetButtonDown(jump))
            {
                moveDirection.y = jumpForce;
                //ANIMATION TRIGGER: JUMPING
            }
        }
        //Normal movement
        else if (controller.isGrounded)
        {
            //Normal speed
            moveDirection *= speed;

            //ANIMATION TRIGGER: WALK AND IDLE

            //Listen for jump
            if (Input.GetButtonDown(jump))
            {
                moveDirection.y = jumpForce;
                //ANIMATION TRIGGER: JUMPING
            }
        }
        //In air
        else
        {
            moveDirection *= jumpSpeed;
        }


        // Get the movement vector
        Vector3 velocityAxis = new Vector3(xSpeed, 0, zSpeed);
        // Rotate the movement vector based on the camera
        velocityAxis = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * velocityAxis;

        // Rotate the player's model to show direction
        if (velocityAxis.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(velocityAxis);
        }

        //Gravity
        moveDirection.y -= Physics.gravity.y * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

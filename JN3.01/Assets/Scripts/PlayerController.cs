using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    //Movement Params
    public float acceleration = 50f;
    public float maxSpeed = 20f;
    public float jumpStrength = 500f;

    //Player Control Inputs
    public string horizontalMove = "";
    public string verticalMove = "";
    public string jump = "";
    public string power = "";

    //Grounding Check
    private bool _onGround = false;

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        // Get the player's input axes
        float xSpeed = Input.GetAxis(horizontalMove);
        float zSpeed = Input.GetAxis(verticalMove);

        // Get the movement vector
        Vector3 velocityAxis = new Vector3(xSpeed, 0, zSpeed);

        // Rotate the movement vector based on the camera
        velocityAxis = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * velocityAxis;

        // Rotate the player's model to show direction
        if (velocityAxis.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(velocityAxis);
        }

        // Move the player
        GetComponent<Rigidbody>().AddForce(velocityAxis.normalized * acceleration);

        // Check the player's input
        if (Input.GetButtonDown(jump))
        {
            Jump();
        }
        if (Input.GetButton(power))
        {
            ActivateTrigger(false);
        }

        LimitVelocity();
    }

    void FixedUpdate()
    {
    }

    //Limit velocity to maxSpeed
    private void LimitVelocity()
    {
        Vector2 xzVel = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z);
        if (xzVel.magnitude > maxSpeed)
        {
            xzVel = xzVel.normalized * maxSpeed;
            GetComponent<Rigidbody>().velocity = new Vector3(xzVel.x, GetComponent<Rigidbody>().velocity.y, xzVel.y);
        }
    }

    //Jumping
    private void Jump()
    {
        if (_onGround)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpStrength, 0));
        }
    }

    //Animation Controller for power use (Assumes we can use power while not on pad, but it does a fail animation; if not, just do in OnTrigger functions)
    private void ActivateTrigger(bool onPowerPad)
    {
        //Can activate power
        if (_onGround && onPowerPad)
        {
            //ANIMATION TRIGGER: ACTIVATING PAD
        }
        //Can activate, but does not work
        else if (_onGround)
        {
            //ANIMATION TRIGGER: ACTIVATING NOTHING
        }
    }

    //Ground checks
    void OnCollisionEnter(Collision col)
    {
        //Use layer "Ground" as check to verify collision
        if (col.gameObject.layer == 8)
        {
            //print("landed");
            _onGround = true;
        }
    }

    //Deactivate grounding when jumping
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            //print("airborne");
            _onGround = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PowerPad")
        {
            col.GetComponent<PowerPadTrigger>().Activate(true);
            ActivateTrigger(true);
        }
        else if (col.gameObject.tag == "Kill")
        {
            Lose();
        }
        else if (col.gameObject.tag == "Win")
        {
            Win();
        }
    }

    //Trigger zone detection
    void OnTriggerStay(Collider col)
    {
        //When in the zone of a Power Pad, check for power use to trigger pad's effects
        if (col.gameObject.tag == "PowerPad")
        {
            col.GetComponent<PowerPadTrigger>().Activate(false);
            ActivateTrigger(true);
        }
    }
    
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PowerPad")
        {
            col.GetComponent<PowerPadTrigger>().Deactivate();
        }
    }

    void Lose()
    {
        //print ("you lose");
        //Set menu to Lose Screen
        ApplicationModel.menuState = 2;
        SceneManager.LoadScene(0);
    }

    void Win()
    {
        //print ("you win");
        //Set menu to Win Screen
        ApplicationModel.menuState = 1;
        SceneManager.LoadScene(0);
    }

    void MainMenu()
    {
        //Set menu to Main Menu Screen
        ApplicationModel.menuState = 0;
        SceneManager.LoadScene(0);
    }
}

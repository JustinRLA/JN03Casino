using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Public variables that will show up in the Editor
    public float Acceleration = 50f;
    public float MaxSpeed = 20f;
    public float JumpStrength = 500f;

    //Player Control Inputs
    public string horizontalMove = "";
    public string verticalMove = "";
    public string jump = "";
    public string power = "";

    // Private variables.  These will not be accessible from any other class.
    private bool _onGround = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
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
        GetComponent<Rigidbody>().AddForce(velocityAxis.normalized * Acceleration);

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

    /// <summary>
    /// Keeps the player's velocity limited so it will not go too fast.
    /// </summary>
    private void LimitVelocity()
    {
        Vector2 xzVel = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.z);
        if (xzVel.magnitude > MaxSpeed)
        {
            xzVel = xzVel.normalized * MaxSpeed;
            GetComponent<Rigidbody>().velocity = new Vector3(xzVel.x, GetComponent<Rigidbody>().velocity.y, xzVel.y);
        }
    }

    /// <summary>
    /// Applies force to the player's rigidbody to make him jump.
    /// </summary>
    private void Jump()
    {
        if (_onGround)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, JumpStrength, 0));
        }
    }

    private void ActivateTrigger(bool onPowerPad)
    {
        if (_onGround && onPowerPad)
        {
            //ANIMATION TRIGGER: ACTIVATING PAD
        }
        else if (_onGround)
        {
            //ANIMATION TRIGGER: ACTIVATING NOTHING
        }
    }

    //Ground checks
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            print("landed");
            _onGround = true;
            GetComponent<Rigidbody>().AddForce(0,0,0);
        }
    }

    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 8)
        {
            print("airborne");
            _onGround = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "PowerPad")
        {
            if (Input.GetButtonDown(power) || Input.GetButton(power))
            {
                col.GetComponent<PowerPadTrigger>().Activate();
                ActivateTrigger(true);
            }
            else if (Input.GetButtonUp(power))
            {
                col.GetComponent<PowerPadTrigger>().Deactivate();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PowerPad")
        {
            col.GetComponent<PowerPadTrigger>().Deactivate();
        }
    }
}

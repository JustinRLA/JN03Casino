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

    //Right character for platform check
    public int myID = 0;
	
    public CharacterAnimator rig;

    //Respawn system stuff
    public GameObject otherPlayer;
    public int currentStage = 0;

    public GameObject[] respawnPoint;
    public GameObject[] dangerZone;

    bool ruinsDangerStarted = false;
    bool heavenDangerStarted = false;

    public GameObject[] platforms;


    // Use this for initialization
    void Start()
    {
        WhoAmI();
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
			
			if(_onGround)
            {
			    rig.Run();
			}
            transform.rotation = Quaternion.LookRotation(velocityAxis);
        }
        else
        {
			if(_onGround)
            {
			    rig.Idle();
			}
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
            //ActivateTrigger(false);
            Die();
        }

        LimitVelocity();
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
			rig.Jump();
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpStrength, 0));
        }
    }

    private void WhoAmI()
    {
        if (gameObject.tag == "Player1")
        {
            myID = 1;
        }
        else if (gameObject.tag == "Player2")
        {
            myID = 2;
        }
        else
        {
            print("am I even real?");
        }
    }

    //Animation Controller for power use (Assumes we can use power while not on pad, but it does a fail animation; if not, just do in OnTrigger functions)
    private void ActivateTrigger(bool onPowerPad, bool isTheRightCharacter)
    {
        //Can activate power
        if (_onGround && onPowerPad && isTheRightCharacter)
        {
            //ANIMATION TRIGGER: ACTIVATING PAD
        }
        else if (_onGround && onPowerPad && !isTheRightCharacter)
        {
            //ANIMATION TRIGGER: FAILING TO ACTIVATE PAD (WRONG PLAYER)
        }
        //Can activate, but does not work
        else if (_onGround)
        {
            //ANIMATION TRIGGER: ACTIVATING NOTHING (NOT ON PAD)
        }
    }

    //Ground checks
    void OnCollisionEnter(Collision col)
    {
        //Use layer "Ground" as check to verify collision
        if (col.gameObject.layer == 8)
        {
            //print("landed");
			rig.Land();
            _onGround = true;
        }
    }

    //Ground checks
    void OnCollisionStay(Collision col)
    {
        //Use layer "Ground" as check to verify collision
        if (col.gameObject.layer == 8 && !_onGround)
        {
            //print("landed");
            rig.Land();
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
            if(myID == col.GetComponent<PowerPadTrigger>().usableByPlayer || col.GetComponent<PowerPadTrigger>().usableByPlayer == 0)
            {
                col.GetComponent<PowerPadTrigger>().Activate(true);
                ActivateTrigger(true, true);
            }
            else
            {
                ActivateTrigger(true, false);
            }
        }
        if (col.gameObject.tag == "Respawn0")
        {
            //Set player respawn point and value
            currentStage = 0;
        }
        else if (col.gameObject.tag == "Respawn1")
        {
            //Set player respawn point and value
            currentStage = 1;
            if (!ruinsDangerStarted)
            {
                dangerZone[currentStage].SetActive(true);
                ruinsDangerStarted = true;
            }
        }
        else if (col.gameObject.tag == "Respawn2")
        {
            //Set player respawn point and value
            currentStage = 2;
            if (!heavenDangerStarted)
            {
                dangerZone[currentStage].SetActive(true);
                heavenDangerStarted = true;
            }
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
            if (myID == col.GetComponent<PowerPadTrigger>().usableByPlayer || col.GetComponent<PowerPadTrigger>().usableByPlayer == 0)
            {
                col.GetComponent<PowerPadTrigger>().Activate(false);
                ActivateTrigger(true, true);
            }
            else
            {
                ActivateTrigger(true, false);
            }
        }
        if (col.gameObject.tag == "Respawn0")
        {
            //Set player respawn point and value
            currentStage = 0;
        }
        else if (col.gameObject.tag == "Respawn1")
        {
            //Set player respawn point and value
            currentStage = 1;
        }
        else if (col.gameObject.tag == "Respawn2")
        {
            //Set player respawn point and value
            currentStage = 2;
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
    
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PowerPad")
        {
            if(myID == col.GetComponent<PowerPadTrigger>().usableByPlayer || col.GetComponent<PowerPadTrigger>().usableByPlayer == 0)
            {
                col.GetComponent<PowerPadTrigger>().Deactivate();
            }
        }
    }

    public void Die()
    {
        transform.position = respawnPoint[currentStage].transform.position;
        otherPlayer.transform.position = otherPlayer.GetComponent<PlayerController>().respawnPoint[currentStage].transform.position;
        dangerZone[currentStage].GetComponent<KillController>().BackToDefault();
        platforms = GameObject.FindGameObjectsWithTag("PowerPad");
        foreach (GameObject platform in platforms)
        {
            platform.GetComponent<PowerPadTrigger>().BackToDefaultTrigger();
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

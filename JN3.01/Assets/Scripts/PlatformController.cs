using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public string platformType = "Standard";
	public AnimationClip animations;

    public Material[] blockMaterials;
    public Material[] litBlockMaterials;
    public int usableByPlayer;

    public float collapseTime = 1.0f;

    bool isActive = false;

    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activated(bool initialActivationPlatform)
    {
        if(platformType == "Standard")
        {
            //Activate when you step in
            if (initialActivationPlatform)
            {
                GetComponent<Animation>().CrossFade("PressurePlateRise");
            }
            //Stays active as long as you're in
            //GetComponent<BoxCollider>().enabled = true;
            GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
        }
        else if (platformType == "Solidify")
        {
            //Only triggers once
            if (!isActive)
            {
                if (initialActivationPlatform)
                {
                    GetComponent<Animation>().CrossFade("PressurePlateRise");
                    //GetComponent<BoxCollider>().enabled = true;
                    GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
                }
                isActive = true;
            }
                //Don't do anything special
        }
        else if (platformType == "Collapsing")
        {
            //Only triggers once
            if (!isActive)
            {
                if (initialActivationPlatform)
                {
                    isActive = true;
                    GetComponent<Animation>().CrossFade("PressurePlateRise");
                    GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
                    //start countdown then collapses
                    StartCoroutine(countdownToCollapse(collapseTime));
                }
            }
        }
    }

    public void Deactivated()
    {
        if (platformType == "Standard")
        {
            //Deactivate
            //GetComponent<BoxCollider>().enabled = false;
            GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
            GetComponent<Animation>().CrossFade("PressurePlateLower");
        }
        else if (platformType == "Solidify")
        {
            //Stays in place
        }
        else if (platformType == "Collapsing")
        {
            //Allowed to be activated again
            isActive = false;
        }
    }

    public void BackToDefault()
    {
        GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
        GetComponent<Animation>().CrossFade("PressurePlateLower");
        isActive = false;
    }

    IEnumerator countdownToCollapse(float timer)
    {
        yield return new WaitForSeconds(timer);

        //At end of timer, collapses
        GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
        GetComponent<Animation>().CrossFade("PressurePlateLower");
    }
}

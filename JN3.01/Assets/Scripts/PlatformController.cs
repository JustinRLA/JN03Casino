using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public string platformType = "Standard";
	public AnimationClip animations;

    public Material[] blockMaterials;
    public Material[] litBlockMaterials;
    public int usableByPlayer;

    bool isActive = false;

    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider>().enabled = false;
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
            GetComponent<BoxCollider>().enabled = true;
            GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
        }
        if (platformType == "Solidify")
        {
            //Only triggers once
            if (!isActive)
            {
                if (initialActivationPlatform)
                {
                    GetComponent<Animation>().CrossFade("PressurePlateRise");
                    GetComponent<BoxCollider>().enabled = true;
                    GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
                }
                isActive = true;
            }
                //Don't do anything special
            }
    }

    public void Deactivated()
    {
        if (platformType == "Standard")
        {
            //Deactivate
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
            GetComponent<Animation>().CrossFade("PressurePlateLower");
        }
        if (platformType == "Solidify")
        {
            //Stays in place
        }
    }
}

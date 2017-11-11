using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public string platformType = "";
	public AnimationClip animations;

    public Material[] blockMaterials;
    public Material[] litBlockMaterials;
    public int usableByPlayer;

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
        //Prevent repeat calling of animation
        if (initialActivationPlatform)
        {
            GetComponent<Animation>().CrossFade("PressurePlateRise");
        }
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
        
		
        if(platformType == "Solidify")
        {
            
        }
    }

    public void Deactivated()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
        GetComponent<Animation>().CrossFade("PressurePlateLower");

    }
}

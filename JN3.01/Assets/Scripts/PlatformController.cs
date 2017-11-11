using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public string platformType = "";
	public AnimationClip animations;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activated()
    {
		
	GetComponent<Animation>().CrossFade("PressurePlateRise");
		
        if(platformType == "Solidify")
        {
            
        }
    }

    public void Deactivated()
    {
    GetComponent<Animation>().CrossFade("PressurePlateLower");

    }
}

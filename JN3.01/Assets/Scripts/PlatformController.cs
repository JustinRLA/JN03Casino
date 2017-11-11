using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    public string platformType = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activated()
    {
        if(platformType == "Solidify")
        {
            
        }
    }

    public void Deactivated()
    {
        //ANIMATION: DISSAPEAR

    }
}

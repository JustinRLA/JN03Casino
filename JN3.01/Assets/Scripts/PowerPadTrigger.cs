using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPadTrigger : MonoBehaviour {

    public int platformCount = 0;

    
    public GameObject[] platforms;

    // Use this for initialization
    void Start () {
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<BoxCollider>().enabled = false;
            gO.GetComponent<MeshRenderer>().enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Activate()
    {
        print("platforms activated");
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<BoxCollider>().enabled = true;
            gO.GetComponent<MeshRenderer>().enabled = true;
            gO.GetComponent<PlatformController>().Activated();
        }
    }
    public void Deactivate()
    {
        print("platforms activated");
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<PlatformController>().Deactivated();
            gO.GetComponent<BoxCollider>().enabled = false;
            gO.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}

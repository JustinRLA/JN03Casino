using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPadTrigger : MonoBehaviour {
    
    public GameObject[] platforms;
	public GameObject plateMesh;
	public Material[] pressurePlateMaterials;
	public Material[] litPressurePlateMaterials;
	public Material[] blockMaterials;
	public Material[] litBlockMaterials;
	public int usableByPlayer; // 0 = both, 1 = p1, 2 = p2
	public AudioClip pressed;
	public AudioClip depressed;
	Renderer skin;
	
    void Start () {
		
		skin = plateMesh.GetComponent<Renderer>();
		skin.material = pressurePlateMaterials[usableByPlayer];

        foreach (GameObject gO in platforms)
        {
		gO.GetComponent<BoxCollider>().enabled = false;
		gO.GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
        }
		
    }

    public void Activate()
    {
		
        GetComponent<AudioSource>().PlayOneShot(pressed,1.0f);
		skin.material = litPressurePlateMaterials[usableByPlayer];
		
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<BoxCollider>().enabled = true;
			gO.GetComponent<Renderer>().material = litBlockMaterials[usableByPlayer];
            gO.GetComponent<PlatformController>().Activated();
        }
    }
    public void Deactivate()
    {
        GetComponent<AudioSource>().PlayOneShot(depressed,1.0f);
		skin.material = pressurePlateMaterials[usableByPlayer];
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<BoxCollider>().enabled = false;
			gO.GetComponent<Renderer>().material = blockMaterials[usableByPlayer];
            gO.GetComponent<PlatformController>().Deactivated();


        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPadTrigger : MonoBehaviour {
    
    public GameObject[] platforms;
	public GameObject plateMesh;
	public Material[] pressurePlateMaterials;
	public Material[] litPressurePlateMaterials;
	public int usableByPlayer; // 0 = both, 1 = p1, 2 = p2
	public AudioClip[] Pressed;
	Renderer skin;
	
    void Start () {
		//PowerPad initialisations
		skin = plateMesh.GetComponent<Renderer>();
		skin.material = pressurePlateMaterials[usableByPlayer];

        //Platform Initialisations
        int i = 0;
        foreach (GameObject gO in platforms) {
            if (gO.CompareTag("Platform"))
            {
                platforms[i] = gO;
                i++;
            }
        }
		
    }

    public void Activate(bool initialActivation)
    {
        //Press PowerPad
        if (initialActivation)
        {
            //Only trigger sound at start
            GetComponent<AudioSource>().PlayOneShot(Pressed[Random.Range(0,6)], 1f);
        }
        skin.material = litPressurePlateMaterials[usableByPlayer];

        //Activate platforms
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<PlatformController>().Activated(initialActivation);
        }
    }

    public void Deactivate()
    {
        //Depress PowerPad
        GetComponent<AudioSource>().PlayOneShot(Pressed[Random.Range(0,6)],1f);
		skin.material = pressurePlateMaterials[usableByPlayer];

        //Deactivate platforms
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<PlatformController>().Deactivated();
        }
    }

    public void BackToDefaultTrigger()
    {
        foreach (GameObject gO in platforms)
        {
            gO.GetComponent<PlatformController>().BackToDefault();
        }
    }
}

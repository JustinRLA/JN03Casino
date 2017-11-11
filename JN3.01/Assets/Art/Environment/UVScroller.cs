using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UVScroller : MonoBehaviour {

 public float scrollSpeed;
 Renderer rend;

 
	void Start () {
		rend = GetComponent<Renderer>();
		//InvokeRepeating("Tile",0f,scrollSpeed);
	}
	

	void Update () {
		
		rend.material.mainTextureOffset = new Vector2(0, -0.05f* Time.timeSinceLevelLoad);
	}
}

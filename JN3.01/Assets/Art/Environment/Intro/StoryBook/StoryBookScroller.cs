using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryBookScroller : MonoBehaviour {

	int index;
	public Texture[] Boards;
	public string[] Lines;
	public GUIText Text;
	public GUITexture Texture;
	
	
	void Start () {
		index = -1;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("P1_Power")){
			index++;
			Text.text = Lines[index];
			Texture.texture = Boards[index];
			
			
			if(Text.text == " "){
				StartCoroutine("LoadGame");
			}
			
			
		}
	}
	
	IEnumerator LoadGame(){
	
	yield return new WaitForSeconds(2);
	SceneManager.LoadScene("Level2");
}
	
	
}



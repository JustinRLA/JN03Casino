using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    //Input
    public string horizontalBtn = "Horizontal";
    public string verticalBtn = "Vertical";

    public string startBtn = "Submit";
    public string quitBtn = "Quit";

    //State Variables

    // Use this for initialization
    void Start () {
		//Set starting status variables
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetButtonUp(quitBtn))
        {
            Lose();
        }

        if (Input.GetButtonUp(startBtn))
        {
            Win();
        }

        if (Input.GetKeyUp("space"))
        {
            MainMenu();
        }
    }

	void Lose(){
		//print ("you lose");
		//Set menu to Lose Screen
		ApplicationModel.menuState = 2;
		SceneManager.LoadScene(0);
	}

	void Win(){
		//print ("you win");
		//Set menu to Win Screen
		ApplicationModel.menuState = 1;
		SceneManager.LoadScene(0);
	}

    void MainMenu()
    {
        //Set menu to Main Menu Screen
        ApplicationModel.menuState = 0;
        SceneManager.LoadScene(0);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{

    public GameObject playBtn;
//    public GameObject music;

    void Awake()
    {
//        DontDestroyOnLoad(music);
    }
    

    public void LoadByIndex(int sceneIndex)
    {
        //AkSoundEngine.PostEvent("SFX_Button_Start_Retro", playBtn);
        //AkSoundEngine.PostEvent("Play_Music", music);
        SceneManager.LoadScene(sceneIndex);
    }
}

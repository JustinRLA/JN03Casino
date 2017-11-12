using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

public GameObject KillTrigger;
public GameObject WinTrigger;
public GameObject PsychoPlayer;
public Transform Player;
float TotalDistance;
float KillDistance;
float WinDistance;
float PsychoDistance;
	
	void Start () {
		TotalDistance = Vector3.Distance(KillTrigger.transform.position, WinTrigger.transform.position);
		
	}
	
	void Update () {
		KillDistance = Vector3.Distance(KillTrigger.transform.position, Player.position);
		WinDistance = Vector3.Distance(WinTrigger.transform.position, Player.position);
		
		KillTrigger.GetComponent<AudioSource>().volume = (KillDistance / TotalDistance) * 1;
		WinTrigger.GetComponent<AudioSource>().volume = (WinDistance / TotalDistance) * 1;
		PsychoPlayer.GetComponent<AudioSource>().volume = -KillDistance + 5;
	
		
	}
}

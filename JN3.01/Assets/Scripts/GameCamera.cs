using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	public GameObject player1;
	public GameObject player2;


	void Update () {
		
		transform.position =  new Vector3(0,((player1.transform.position.y + player2.transform.position.y)/2)+3.53f,((player1.transform.position.z + player2.transform.position.z)/2)-4.31f);
		
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlay : MonoBehaviour {

public float timer;

	void Start () {

Wait();
	
	}
	
	// Update is called once per frame
 IEnumerator Wait() {
			yield return new WaitForSeconds(timer);
	GetComponent<Animation>().Play();
	}
}

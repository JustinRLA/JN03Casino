using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlay : MonoBehaviour {

public float timer;
public AnimationClip fuckyou;

	void Start () {

StartCoroutine("Wait");
	
	}
	
	// Update is called once per frame
 IEnumerator Wait() {
			yield return new WaitForSeconds(timer);
	GetComponent<Animation>().CrossFade(fuckyou.name,0.5f);
	}
}

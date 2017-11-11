using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour {

public Animation anim;


	public void Run () {
		anim.CrossFade("Run",0.15f);
	}
	
	public void Idle () {
		anim.CrossFade("Idle",0.15f);
	}
	
	public void Jump () {
		anim.CrossFade("Jump",0.15f);
	}
	
	public void Land () {
		anim.CrossFade("Land",0.15f);
	}
	
}

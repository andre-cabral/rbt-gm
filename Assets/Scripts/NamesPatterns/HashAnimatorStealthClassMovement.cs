using UnityEngine;
using System.Collections;

public class HashAnimatorStealthClassMovement : MonoBehaviour {

	public int dodging;
	public int run;
	
	void Awake() {
		dodging = Animator.StringToHash("Dodging");
		run = Animator.StringToHash("Run");
	}
}

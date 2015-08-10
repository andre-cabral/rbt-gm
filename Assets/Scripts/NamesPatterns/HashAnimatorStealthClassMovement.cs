using UnityEngine;
using System.Collections;

public class HashAnimatorStealthClassMovement : MonoBehaviour {

	public int dodging;
	
	void Awake() {
		dodging = Animator.StringToHash("Dodging");
	}
}

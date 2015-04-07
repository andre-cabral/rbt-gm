using UnityEngine;
using System.Collections;

public class HashAnimatorPowerClassMovement : MonoBehaviour {

	public int punch;
	
	void Awake() {
		punch = Animator.StringToHash("Punch");
	}
}

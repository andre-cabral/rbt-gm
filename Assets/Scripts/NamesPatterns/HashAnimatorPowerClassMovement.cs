using UnityEngine;
using System.Collections;

public class HashAnimatorPowerClassMovement : MonoBehaviour {

	public int punch;
	public int blocking;
	
	void Awake() {
		punch = Animator.StringToHash("Punch");
		blocking = Animator.StringToHash("Blocking");
	}
}

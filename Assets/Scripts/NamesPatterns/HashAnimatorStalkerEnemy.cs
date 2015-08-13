using UnityEngine;
using System.Collections;

public class HashAnimatorStalkerEnemy : MonoBehaviour {

	public int attacking;
	
	void Awake() {
		attacking = Animator.StringToHash("Attacking");
	}
}

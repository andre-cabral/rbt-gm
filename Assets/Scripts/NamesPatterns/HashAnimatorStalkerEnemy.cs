using UnityEngine;
using System.Collections;

public class HashAnimatorStalkerEnemy : MonoBehaviour {

	public int velocity;
	public int attacking;


	void Awake() {
		velocity = Animator.StringToHash("Velocity");
		attacking = Animator.StringToHash("Attacking");
	}
}

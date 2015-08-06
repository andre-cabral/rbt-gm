using UnityEngine;
using System.Collections;

public class HashAnimatorRangedClassMovement : MonoBehaviour {

	public int shooting;
	
	void Awake() {
		shooting = Animator.StringToHash("Shooting");
	}
}

using UnityEngine;
using System.Collections;

public class HashAnimatorDefaultMovement : MonoBehaviour {

	public int valueX;
	public int valueZ;
	
	void Awake() {
		valueX = Animator.StringToHash("ValueX");
		valueZ = Animator.StringToHash("ValueZ");
	}
}

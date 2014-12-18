using UnityEngine;
using System.Collections;

public class HashAnimatorDefaultMovement : MonoBehaviour {

	private Animator animator;
	public int valueX;
	public int valueZ;
	public int changeClass;
	
	void Awake() {
		valueX = Animator.StringToHash("ValueX");
		valueZ = Animator.StringToHash("ValueZ");
		changeClass = Animator.StringToHash("ChangeClass");
	}
}

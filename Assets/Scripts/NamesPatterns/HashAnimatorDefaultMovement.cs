using UnityEngine;
using System.Collections;

public class HashAnimatorDefaultMovement : MonoBehaviour {

	public int valueX;
	public int valueZ;
	public int changeClass;
	public int classToGo;
	
	void Awake() {
		valueX = Animator.StringToHash("ValueX");
		valueZ = Animator.StringToHash("ValueZ");
		changeClass = Animator.StringToHash("ChangeClass");
		classToGo = Animator.StringToHash("ClassToGo");
	}
}

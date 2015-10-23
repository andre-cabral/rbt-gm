using UnityEngine;
using System.Collections;

public class HashAnimatorDefaultMovement : MonoBehaviour {

	public int valueX;
	public int valueZ;
	public int changeClass;
	public int classToGo;
	public int grounded;
	public int verticalSpeed;
	public int jumpStart;
	public int use;
	
	void Awake() {
		valueX = Animator.StringToHash("ValueX");
		valueZ = Animator.StringToHash("ValueZ");
		changeClass = Animator.StringToHash("ChangeClass");
		classToGo = Animator.StringToHash("ClassToGo");
		grounded = Animator.StringToHash("Grounded");
		verticalSpeed = Animator.StringToHash("VerticalSpeed");
		jumpStart = Animator.StringToHash("JumpStart");
		use = Animator.StringToHash("Use");
	}
}

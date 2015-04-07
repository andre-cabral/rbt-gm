using UnityEngine;
using System.Collections;

public class PowerClassMovement : MonoBehaviour {

	public DefaultMovement defaultMovementScript;
	public GameObject punchCollider;
	private Animator animator;
	private HashAnimatorPowerClassMovement hashPower;

	void Awake(){
		animator = GetComponent<Animator>();
		hashPower = GetComponent<HashAnimatorPowerClassMovement>();
	}

	void Update () {
		if(!defaultMovementScript.getIsDead() && !defaultMovementScript.getStoppedOnAnimation()){
			if(Input.GetButtonDown(Buttons.power0) ){
				Punch();
			}
			if(Input.GetButtonDown(Buttons.power1) ){
				
			}
		}
	}

	void Punch(){
		if(defaultMovementScript.getGrounded()){
			defaultMovementScript.setStoppedOnAnimation(true);
			animator.SetBool(hashPower.punch, true);
		}
	}

	void PunchAnimationHitStart(){
		punchCollider.SetActive(true);
	}

	void PunchAnimationEnd(){
		punchCollider.SetActive(false);
		defaultMovementScript.setStoppedOnAnimation(false);
	}

}

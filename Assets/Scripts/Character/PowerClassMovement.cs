using UnityEngine;
using System.Collections;

public class PowerClassMovement : MonoBehaviour {

	public DefaultMovement defaultMovementScript;
	public GameObject punchCollider;
	public GameObject blockingCollider;
	private bool isBlocking = false;
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
				BlockingStart();
			}
			if(Input.GetButtonUp(Buttons.power1) ){
				BlockingEnd();
			}
		}
	}

//########Punch START
//###########################################
	void Punch(){
		//if(defaultMovementScript.getGrounded()){
			//defaultMovementScript.setCanJump(false);
			animator.SetBool(hashPower.punch, true);
		//}
	}

	void PunchAnimationHitStart(){
		punchCollider.SetActive(true);

	}

	void PunchAnimationEnd(){
		punchCollider.SetActive(false);
		//defaultMovementScript.setCanJump(true);
	}
//########Punch END
//###########################################


//########Blocking START
//###########################################
	void BlockingStart(){
		blockingCollider.SetActive(true);
		animator.SetBool(hashPower.blocking, true);
		defaultMovementScript.setCanWalk(false);
	}
	void BlockingEnd(){
		blockingCollider.SetActive(false);
		animator.SetBool(hashPower.blocking, false);
		defaultMovementScript.setCanWalk(true);
	}
//########Blocking END
//###########################################
}

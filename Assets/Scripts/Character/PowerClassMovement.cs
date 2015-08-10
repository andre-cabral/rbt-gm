﻿using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BlockingCollider))]

public class PowerClassMovement : MonoBehaviour {

	public GameObject punchCollider;
	public GameObject blockingCollider;

	private bool isPunching = false;
	private bool isBlocking = false;

	private DefaultMovement defaultMovementScript;
	private Animator animator;
	private HashAnimatorPowerClassMovement hashPower;
	private BlockingCollider blockingColliderScript;

	void Awake(){
		defaultMovementScript = GetComponent<DefaultMovement>();
		animator = GetComponent<Animator>();
		hashPower = GetComponent<HashAnimatorPowerClassMovement>();

		blockingColliderScript = blockingCollider.GetComponent<BlockingCollider>();
	}

	void Update () {
		if(!defaultMovementScript.getIsDead() && !defaultMovementScript.getStoppedOnAnimation()){
			if(Input.GetButtonDown(Buttons.power0) && !isPunching){
				Punch();
			}

			if(Input.GetButton(Buttons.power1) && !isBlocking && !isPunching && defaultMovementScript.getGrounded() ){
				BlockingStart();
			}
		}
		if(!defaultMovementScript.getIsDead() && isBlocking
		   && (!Input.GetButton(Buttons.power1) || defaultMovementScript.getStoppedOnAnimation() || isPunching || !defaultMovementScript.getGrounded() ) ){
			BlockingEnd();
		}
	}

//########Punch START
//###########################################
	void Punch(){
		//if(defaultMovementScript.getGrounded()){
			//defaultMovementScript.setCanJump(false);
			animator.SetBool(hashPower.punch, true);
			isPunching = true;
			if(isBlocking){
				BlockingEnd();
			}
		//}
	}

	void PunchAnimationHitStart(){
		punchCollider.SetActive(true);

	}

	void PunchAnimationEnd(){
		punchCollider.SetActive(false);
		isPunching = false;
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
		isBlocking = true;
	}
	void BlockingEnd(){
		blockingCollider.SetActive(false);
		animator.SetBool(hashPower.blocking, false);
		defaultMovementScript.setCanWalk(true);
		isBlocking = false;
	}
//########Blocking END
//###########################################

	public bool getIsBlocking(){
		return isBlocking;
	}

}

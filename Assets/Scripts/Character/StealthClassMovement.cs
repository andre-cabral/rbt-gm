using UnityEngine;
using System.Collections;
[RequireComponent(typeof(DodgingCollider))]

public class StealthClassMovement : MonoBehaviour {

	public GameObject dodgingCollider;

	public float runningSpeed = 0.075f;
	public float walkingSpeed = 0.025f;
	bool running = false;

	private bool isDodging = false;

	private DefaultMovement defaultMovementScript;
	private Animator animator;
	private HashAnimatorStealthClassMovement hashStealth;

	public float dodgeDelayTime = 0.5f;
	private float dodgeDelayTimePassed = 0f;
	private bool onDelayTime = false;

	void Awake(){
		defaultMovementScript = GetComponent<DefaultMovement>();
		animator = GetComponent<Animator>();
		hashStealth = GetComponent<HashAnimatorStealthClassMovement>();
	}

	void Update () {
		if(!defaultMovementScript.getIsDead() && !defaultMovementScript.getStoppedOnAnimation()){
			if(Input.GetButtonDown(Buttons.power1) && !isDodging && defaultMovementScript.getGrounded() && !onDelayTime ){
				DodgingStart();
			}

			if(Input.GetButton(Buttons.power0)){
				StartRunning();
			}else{
				EndRunning();
			}
		}
		/*
		if(!defaultMovementScript.getIsDead() && isDodging
		   && (!Input.GetButton(Buttons.power1) || defaultMovementScript.getStoppedOnAnimation() || !defaultMovementScript.getGrounded() ) ){
			DodgingEnd();
		}
		*/

		if(onDelayTime){
			dodgeDelayTimePassed += Time.deltaTime;

			if(dodgeDelayTimePassed >= dodgeDelayTime){
				dodgeDelayTimePassed = 0f;
				onDelayTime = false;
			}
		}
	}

//########Dodging START
//###########################################
	void DodgingStart(){
		dodgingCollider.SetActive(true);
		animator.SetBool(hashStealth.dodging, true);
		defaultMovementScript.setCanWalk(false);
		isDodging = true;
	}
	void DodgingEnd(){
		dodgingCollider.SetActive(false);
		animator.SetBool(hashStealth.dodging, false);
		defaultMovementScript.setCanWalk(true);
		isDodging = false;

		onDelayTime = true;
	}
//########Dodging END
//###########################################


//########Running START
//###########################################
	void StartRunning(){
		if(!running){
			gameObject.name = ClassesObjectsNames.stealthRunning;
			defaultMovementScript.setMovementSpeed(runningSpeed);
			animator.SetBool(hashStealth.run, true);
			running = true;
		}
	}

	void EndRunning(){
		if(running){
			gameObject.name = ClassesObjectsNames.stealth;
			defaultMovementScript.setMovementSpeed(walkingSpeed);
			animator.SetBool(hashStealth.run, false);
			running = false;
		}
	}
//########Running END
//###########################################



	public bool getIsDodging(){
		return isDodging;
	}

}

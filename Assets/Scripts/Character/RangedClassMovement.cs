using UnityEngine;
using System.Collections;

public class RangedClassMovement : MonoBehaviour {

	public GameObject arrowPrefab;
	public GameObject arrowPositionObject;
	public float arrowForce = 1000f;

	private GameObject latestArrow;
	private Rigidbody latestArrowRigidBody;

	private bool isShooting = false;
	private bool isArrowFollowingPositionObject = false;

	private DefaultMovement defaultMovementScript;
	private Animator animator;
	private HashAnimatorRangedClassMovement hashRanged;
	
	void Awake(){
		defaultMovementScript = GetComponent<DefaultMovement>();
		animator = GetComponent<Animator>();
		hashRanged = GetComponent<HashAnimatorRangedClassMovement>();
	}
	
	void Update () {

		if(latestArrow != null){
			Debug.DrawRay(latestArrow.transform.position,latestArrow.transform.forward*50);
		}

		if(!defaultMovementScript.getIsDead() && !defaultMovementScript.getStoppedOnAnimation()){
			if(Input.GetButtonDown(Buttons.power0) && !isShooting){
				Shooting();
			}
		}

		if(!isArrowFollowingPositionObject && !isShooting){
			NewArrow();
		}

		if(isArrowFollowingPositionObject){
			//ArrowPosition();
		}
	}
	
	//########Shooting START
	//###########################################
	void Shooting(){
		animator.SetBool(hashRanged.shooting, true);
		isShooting = true;
	}

	void ShootingLaunchArrowAnimationStart(){
		isArrowFollowingPositionObject = false;
		arrowPositionObject.transform.DetachChildren();
		latestArrowRigidBody.isKinematic = false;
		latestArrowRigidBody.AddForce( arrowForce * latestArrow.transform.forward );
	}

	void ShootingAnimationEnd(){
		isShooting = false;
	}

	void NewArrow(){
		isArrowFollowingPositionObject = true;
		latestArrow = Instantiate(arrowPrefab);
		latestArrowRigidBody = latestArrow.GetComponent<Rigidbody>();
		latestArrow.transform.SetParent(arrowPositionObject.transform);
		latestArrow.transform.localPosition = new Vector3(0f,0f,0f);
		latestArrow.transform.localRotation = Quaternion.Euler(new Vector3 (0f,0f,0f));

		//ArrowPosition();
	}
	void ArrowPosition(){
		latestArrow.transform.position = arrowPositionObject.transform.position;
		//latestArrow.transform.position = new Vector3( arrowPositionObject.transform.position.x,arrowPositionObject.transform.position.y,arrowPositionObject.transform.position.z);

		latestArrow.transform.rotation = arrowPositionObject.transform.rotation;
		//latestArrow.transform.rotation = new Vector3(90f,0f,0f).;
		
		//latestArrowRigidBody.velocity = new Vector3(0f,0f,0f);
		//latestArrowRigidBody.angularVelocity = new Vector3(0f,0f,0f);
		//latestArrow.transform
	}
	//########Shooting END
	//###########################################
}

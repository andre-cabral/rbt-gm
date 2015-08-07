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

			Vector3 origin = latestArrow.transform.position;
			Vector3 destiny = transform.forward*50;

			Debug.DrawRay(origin, destiny);
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

		//latestArrow.transform.localPosition = new Vector3(transform.localPosition.x, latestArrow.transform.localPosition.y, latestArrow.transform.localPosition.z);
		//Debug.Log(new Vector3(transform.localPosition.x, latestArrow.transform.localPosition.y, latestArrow.transform.localPosition.z));
		latestArrow.transform.rotation = transform.rotation;
		//latestArrowRigidBody.angularVelocity = Vector3.zero;
		latestArrowRigidBody.isKinematic = false;

		arrowPositionObject.transform.DetachChildren();

		latestArrowRigidBody.AddForce( arrowForce * /*latestArrow.*/transform.forward );
		latestArrow.GetComponent<Collider>().enabled = true;
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
	}
	//########Shooting END
	//###########################################
}

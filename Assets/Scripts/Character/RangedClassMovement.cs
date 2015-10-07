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
		if(!isArrowFollowingPositionObject && !isShooting){
			NewArrow();
		}

		if(!defaultMovementScript.getIsDead() && !DefaultMovement.isPaused && !defaultMovementScript.getStoppedOnAnimation() && !defaultMovementScript.getIsChangingClass()){
			if(Input.GetButtonDown(Buttons.power0) && !isShooting){
				Shooting();
			}
		}
		if(isShooting && defaultMovementScript.getIsChangingClass()){
			ShootingAnimationEnd();
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

		latestArrow.transform.rotation = transform.rotation;

		arrowPositionObject.transform.DetachChildren();

		latestArrowRigidBody.isKinematic = false;
		latestArrowRigidBody.AddForce( arrowForce * transform.forward );
		latestArrow.GetComponent<Collider>().enabled = true;
	}

	void ShootingAnimationEnd(){
		isShooting = false;
	}

	void NewArrow(){
		isArrowFollowingPositionObject = true;
		//latestArrow = new GameObject();
		latestArrow = Instantiate(arrowPrefab);
		latestArrowRigidBody = latestArrow.GetComponent<Rigidbody>();
		latestArrow.transform.SetParent(arrowPositionObject.transform);
		latestArrow.transform.localPosition = new Vector3(0f,0f,0f);
		latestArrow.transform.localRotation = Quaternion.Euler(new Vector3 (0f,0f,0f));
	}
	//########Shooting END
	//###########################################
}

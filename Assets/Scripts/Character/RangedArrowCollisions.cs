using UnityEngine;
using System.Collections;

public class RangedArrowCollisions : MonoBehaviour {

	Rigidbody arrowRigidBody;
	Collider arrowCollider;
	public float timeToDestroy = 0.2f;
	private float timePassedToDestroy = 0f;
	private bool startCountingToDestroy = false;

	void Awake(){
		arrowRigidBody = GetComponent<Rigidbody>();
		arrowCollider = GetComponent<Collider>();
	}

	void Update(){
		if(startCountingToDestroy){
			if(timePassedToDestroy < timeToDestroy){
				timePassedToDestroy += Time.deltaTime;
			}else{
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == Tags.enemy || collider.tag == Tags.wall){
			ArrowStopOnTarget(collider);
			startCountingToDestroy = true;
		}
	}

	//method that stops the arrow on the target and make it stick to the target.
	//this method is not used now, but can be useful in the future, in explosive arrows for example
	void ArrowStopOnTarget(Collider collidedTarget){
		arrowCollider.enabled = false;
		
		arrowRigidBody.velocity = Vector3.zero;
		arrowRigidBody.angularVelocity = Vector3.zero;
		arrowRigidBody.isKinematic = true;
		arrowCollider.isTrigger = false;
		//arrowRigidBody.useGravity = true;
		
		transform.SetParent(collidedTarget.gameObject.transform);
	}
}
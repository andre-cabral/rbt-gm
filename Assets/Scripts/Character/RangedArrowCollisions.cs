using UnityEngine;
using System.Collections;

public class RangedArrowCollisions : MonoBehaviour {

	Rigidbody arrowRigidBody;
	Collider arrowCollider;

	void Awake(){
		arrowRigidBody = GetComponent<Rigidbody>();
		arrowCollider = GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider collider){
		Destroy(gameObject);
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
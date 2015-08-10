using UnityEngine;
using System.Collections;

public class RangedArrowCollisions : MonoBehaviour {

	Rigidbody arrowRigidBody;
	Collider arrowCollider;

	void Awake(){
		arrowRigidBody = GetComponent<Rigidbody>();
		arrowCollider = GetComponent<Collider>();
	}

	void OnCollisionEnter(Collision collision){
		arrowCollider.enabled = false;

		arrowRigidBody.velocity = Vector3.zero;
		arrowRigidBody.angularVelocity = Vector3.zero;
		arrowRigidBody.isKinematic = true;
		//arrowRigidBody.useGravity = true;
	}
}
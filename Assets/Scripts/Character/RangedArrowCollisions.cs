using UnityEngine;
using System.Collections;

public class RangedArrowCollisions : MonoBehaviour {

	Rigidbody arrowRigidBody;

	void Awake(){
		arrowRigidBody = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision collision){
		arrowRigidBody.velocity = Vector3.zero;
		arrowRigidBody.angularVelocity = Vector3.zero;
		arrowRigidBody.useGravity = true;
	}
}
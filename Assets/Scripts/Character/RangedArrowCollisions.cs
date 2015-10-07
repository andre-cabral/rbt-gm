using UnityEngine;
using System.Collections;

public class RangedArrowCollisions : MonoBehaviour {

	public AudioClip hitOnWallSound;
	public AudioClip hitOnEnemySound;
	Rigidbody arrowRigidBody;
	Collider arrowCollider;
	public float timeToDestroy = 0.2f;

	void Awake(){
		arrowRigidBody = GetComponent<Rigidbody>();
		arrowCollider = GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == Tags.enemy){
			AudioSource.PlayClipAtPoint(hitOnEnemySound, transform.position);
		}
		if(collider.tag == Tags.wall){
			AudioSource.PlayClipAtPoint(hitOnWallSound, transform.position);
		}
		if(collider.tag == Tags.enemy || collider.tag == Tags.wall || collider.tag == Tags.bell){
			ArrowStopOnTarget(collider);
			Destroy(this.gameObject, timeToDestroy);
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

		transform.SetParent(collidedTarget.gameObject.transform);
	}
}
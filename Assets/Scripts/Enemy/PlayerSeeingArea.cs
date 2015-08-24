using UnityEngine;
using System.Collections;

public class PlayerSeeingArea : MonoBehaviour {

	public Transform enemyTransform;
	public PatrolAndStalkMovement stalkerNavMeshScript;
	public bool canSeeStealthPlayer = false;
	public bool needLineOfSightToGetPlayer = false;

	void OnTriggerStay(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.characterClass){
			if( (canSeeStealthPlayer || collidedObject.name != ClassesObjectsNames.stealth) 
			   && stalkerNavMeshScript.getLastPlayerSeen() != collidedObject.transform.position){

				bool canSeePlayer = true;
				if(needLineOfSightToGetPlayer){
					canSeePlayer = HasLineOfSight(enemyTransform.position, collidedObject.transform.position);
				}

				if(canSeePlayer){
					stalkerNavMeshScript.setLastPlayerSeen(collidedObject.transform.position);
					if(!stalkerNavMeshScript.getIsSeeingPlayer() ){
						stalkerNavMeshScript.setIsSeeingPlayer(true);
					}
				}

			}
		}
	}

	void OnTriggerExit(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.characterClass){
			stalkerNavMeshScript.setIsSeeingPlayer(false);
		}
	}

	//if there is another tag to hide the player, add it here in the foreach
	bool HasLineOfSight(Vector3 origin, Vector3 target){
		bool hasLOS = true;

		Vector3 direction = target - origin;
		float distance = (target - origin).magnitude;

		RaycastHit[] allHits = Physics.RaycastAll(origin, direction, distance);

		foreach(RaycastHit hit in allHits){
			if(hit.collider.gameObject.tag == Tags.wall){
				hasLOS = false;
			}
		}

		return hasLOS;
	}

}

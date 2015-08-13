using UnityEngine;
using System.Collections;

public class PlayerSeeingArea : MonoBehaviour {

	public SelectMovementSeeingPlayer selectMovementScript;
	public bool canSeeStealthPlayer = false;

	void OnTriggerStay(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.characterClass){
			if( (canSeeStealthPlayer || collidedObject.name != ClassesObjectsNames.stealth) 
			   && !selectMovementScript.getNavMeshMovementActive() ){
				selectMovementScript.SelectNavMeshMovement();
			}
		}
	}

	void OnTriggerExit(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.characterClass){
			if( !selectMovementScript.getPointsMovementActive() ){
				selectMovementScript.SelectPointsMovement();
			}
		}
	}

}

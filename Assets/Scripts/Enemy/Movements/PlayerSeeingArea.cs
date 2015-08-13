using UnityEngine;
using System.Collections;

public class PlayerSeeingArea : MonoBehaviour {

	public StalkerNavMesh stalkerNavMeshScript;
	public bool canSeeStealthPlayer = false;

	void OnTriggerStay(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.characterClass){
			if( (canSeeStealthPlayer || collidedObject.name != ClassesObjectsNames.stealth) 
			   && stalkerNavMeshScript.getLastPlayerSeen() != collidedObject.transform.position){

				stalkerNavMeshScript.setLastPlayerSeen(collidedObject.transform.position);
				if(!stalkerNavMeshScript.getIsSeeingPlayer() ){
					stalkerNavMeshScript.setIsSeeingPlayer(true);
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

}

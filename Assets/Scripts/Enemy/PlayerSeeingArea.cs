using UnityEngine;
using System.Collections;

public class PlayerSeeingArea : MonoBehaviour {
	
	public Transform enemyTransform;
	public PatrolAndStalkMovement stalkerNavMeshScript;
	public bool canSeeStealthPlayer = false;
	public bool needLineOfSightToGetPlayer = false;
	public bool canHearPlayer = false;
	Bell lastBellHeard;

	void Update(){
		if(lastBellHeard != null){
			if(!lastBellHeard.getRinging()){
				lastBellHeard = null;
				stalkerNavMeshScript.resetLastPlayerSeen();
				stalkerNavMeshScript.setIsSeeingPlayer(false);
			}
		}
	}

	void OnTriggerStay(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		if(collidedObject.tag == Tags.characterClass){
			if(stalkerNavMeshScript.getLastPlayerSeen() != collidedObject.transform.position){
				
				bool canSeePlayer = true;
				//check if stealth is running
				if(canSeePlayer && !canSeeStealthPlayer && collidedObject.name == ClassesObjectsNames.stealth){
					canSeePlayer = collidedObject.GetComponent<StealthClassMovement>().getRunning();
				}
				//check if need LOS or if its a hearing area
				if(canSeePlayer && needLineOfSightToGetPlayer){
					canSeePlayer = HasLineOfSight(enemyTransform.position, collidedObject.transform.position);
				}
				
				if(canSeePlayer){
					stalkerNavMeshScript.setLastPlayerSeen(collidedObject.transform.position);
					if(!stalkerNavMeshScript.getIsSeeingPlayer() ){
						SetIcon();
						stalkerNavMeshScript.setIsSeeingPlayer(true);
					}
				}
				
			}
		}
		if(collidedObject.tag == Tags.bellRinging){
			if(stalkerNavMeshScript.getLastPlayerSeen() == stalkerNavMeshScript.getLastPlayerSeenResetPosition()){
				lastBellHeard = collidedObject.GetComponent<Bell>();
				stalkerNavMeshScript.setLastPlayerSeen(collidedObject.transform.position);
				if(!stalkerNavMeshScript.getIsSeeingPlayer() ){
					SetHearIcon();
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

	void SetIcon(){
		stalkerNavMeshScript.DeactivateAllIcons();
		if(canHearPlayer){
			stalkerNavMeshScript.ActivatePlayerHeard();
		}else{
			stalkerNavMeshScript.ActivatePlayerSeen();
		}
	}

	void SetHearIcon(){
		stalkerNavMeshScript.DeactivateAllIcons();
		stalkerNavMeshScript.ActivatePlayerHeard();
	}
	
}

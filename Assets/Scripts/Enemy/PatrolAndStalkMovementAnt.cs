using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HashAnimatorStalkerEnemy))]
public class PatrolAndStalkMovementAnt : PatrolAndStalkMovement {
	public static float distanceToCallAnts = 20f;
	public static Vector3 lastPlayerSeenAnt = new Vector3(9999f,9999f,9999f);
	public static bool anyAntCanSeePlayer = false;
	public static Transform lastAntWhoSeenPlayer = null;
	
	public override void Awake(){
		base.Awake();
	}
	
	public override void Update(){
		base.Update();
		if(lastAntWhoSeenPlayer == transform && !getIsSeeingPlayer()){
			lastAntWhoSeenPlayer = null;
		}
		if(lastAntWhoSeenPlayer == null){
			lastPlayerSeenAnt = getLastPlayerSeenResetPosition();
			if(getIsSeeingPlayer()){
				lastPlayerSeenAnt = getLastPlayerSeen();
				lastAntWhoSeenPlayer = transform;
			}
		}else{
			if(getIsSeeingPlayer()){
				lastPlayerSeenAnt = getLastPlayerSeen();
				lastAntWhoSeenPlayer = transform;
			}else{
				if( Vector3.Distance(transform.position, lastAntWhoSeenPlayer.position) <= distanceToCallAnts ){ 
					setLastPlayerSeen(lastPlayerSeenAnt);
				}
			}
		}
	}
}

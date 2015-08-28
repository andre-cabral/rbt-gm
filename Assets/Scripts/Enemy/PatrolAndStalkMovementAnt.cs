﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HashAnimatorStalkerEnemy))]
public class PatrolAndStalkMovementAnt : PatrolAndStalkMovement {
	public static Vector3 lastPlayerSeenAnt = new Vector3(9999f,9999f,9999f);
	public static bool anyAntCanSeePlayer = false;
	static int totalAnts = 0;
	static int antsSeeingPlayer = 0;
	bool addedThisSeeingPlayer = false;
	
	public override void Awake(){
		base.Awake();
		totalAnts++;
	}
	
	public override void Update(){
		base.Update();
		if(!addedThisSeeingPlayer && getIsSeeingPlayer()){
			antsSeeingPlayer++;
			addedThisSeeingPlayer = true;
		}
		if(addedThisSeeingPlayer && !getIsSeeingPlayer()){
			antsSeeingPlayer--;
			addedThisSeeingPlayer = false;
		}
		if(antsSeeingPlayer == 0){
			lastPlayerSeenAnt = getLastPlayerSeenResetPosition();
		}else{
			if(getIsSeeingPlayer()){
				lastPlayerSeenAnt = getLastPlayerSeen();
			}else{
				setLastPlayerSeen(lastPlayerSeenAnt);
			}
		}
	}
	
	void OnDestroy() {
		totalAnts --;
		if(getIsSeeingPlayer()){
			antsSeeingPlayer--;
		}
	}
}

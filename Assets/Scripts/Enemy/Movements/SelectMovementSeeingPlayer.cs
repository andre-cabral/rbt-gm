using UnityEngine;
using System.Collections;

public class SelectMovementSeeingPlayer : MonoBehaviour {

	private bool pointsMovementActive = true;
	private bool navMeshMovementActive = false;

	WorldPointsMovement worldPointsMovementScript;
	StalkerNavMesh stalkerNavMeshScript;

	void Awake () {
		worldPointsMovementScript = GetComponent<WorldPointsMovement>();
		stalkerNavMeshScript = GetComponent<StalkerNavMesh>();
	}

	public void SelectPointsMovement(){
		stalkerNavMeshScript.enabled = false;
		worldPointsMovementScript.enabled = true;

		navMeshMovementActive = false;
		pointsMovementActive = true;
	}

	public void SelectNavMeshMovement(){
		worldPointsMovementScript.enabled = false;
		stalkerNavMeshScript.enabled = true;

		pointsMovementActive = false;
		navMeshMovementActive = true;
	}

	public bool getPointsMovementActive(){
		return pointsMovementActive;
	}

	public bool getNavMeshMovementActive(){
		return navMeshMovementActive;
	}

}

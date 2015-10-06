using UnityEngine;
using System.Collections;

public class EnableAndDisableColliders : MonoBehaviour {

	public Collider[] colliders;

	public void EnableTheColliders(){
		foreach(Collider col in colliders){
			col.enabled = true;
		}
	}

	public void DisableTheColliders(){
		foreach(Collider col in colliders){
			col.enabled = false;
		}
	}
}

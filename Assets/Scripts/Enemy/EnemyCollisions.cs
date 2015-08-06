using UnityEngine;
using System.Collections;

public class EnemyCollisions : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		if(collider.tag == Tags.playerAttackCollider){
			Destroy(gameObject);
		}
	}
}

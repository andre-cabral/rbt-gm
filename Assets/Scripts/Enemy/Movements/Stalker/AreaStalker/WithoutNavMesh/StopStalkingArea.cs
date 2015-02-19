using UnityEngine;
using System.Collections;

public class StopStalkingArea : MonoBehaviour {

	public GameObject enemy;
	private StalkerMovementArea stalkerMovement;
	
	void Awake () {
		stalkerMovement = enemy.GetComponent<StalkerMovementArea>();
	}
	
	void OnTriggerExit(Collider collision){
		if(collision.gameObject.tag == Tags.characterClass){
			stalkerMovement.setIsStalking(false);
		}
	}
}

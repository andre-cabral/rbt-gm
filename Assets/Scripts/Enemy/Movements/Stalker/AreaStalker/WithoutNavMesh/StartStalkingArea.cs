using UnityEngine;
using System.Collections;

public class StartStalkingArea : MonoBehaviour {
	public GameObject enemy;
	private StalkerMovementArea stalkerMovement;

	void Awake () {
		stalkerMovement = enemy.GetComponent<StalkerMovementArea>();
	}

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == Tags.characterClass){
			stalkerMovement.setIsStalking(true);
		}
	}
}

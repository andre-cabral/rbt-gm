using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public int lifeToGain = 1;
	public AudioClip healthGotSound;
	private LifeManager lifeManager;
	
	void Awake(){
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		lifeManager = gameController.GetComponent<LifeManager>();
	}
	
	void OnTriggerStay(Collider col){
		if(col.gameObject.tag == Tags.characterClass){
			CollidedWithPlayer();
		}
	}

	void CollidedWithPlayer(){
		if(lifeManager.GetLife() < lifeManager.maxLife){
			lifeManager.LifeGain(lifeToGain);
			if(healthGotSound != null){
				AudioSource.PlayClipAtPoint(healthGotSound, transform.position);
			}
			Destroy(gameObject);
		}
	}
}

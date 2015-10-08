using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

	public int lifeToGain = 1;
	public AudioNames healthGotSound;
	private LifeManager lifeManager;
	CameraEffects cameraEffects;
	
	void Awake(){
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		lifeManager = gameController.GetComponent<LifeManager>();

		cameraEffects = Camera.main.GetComponent<CameraEffects>();
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
				AudioManager.PlaySound(healthGotSound.ToString(), transform.position);
			}

			cameraEffects.StartVignetteChange();
			Destroy(gameObject);
		}
	}
}

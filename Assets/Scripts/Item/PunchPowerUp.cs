using UnityEngine;
using System.Collections;

public class PunchPowerUp : MonoBehaviour {

	public GameObject[] objectsToShowWhilePoweredUp;
	public GameObject[] objectsToHideWhilePoweredUp;
	public int damageToAdd = 1;
	public float timePowerUp = 60f;
	public AudioNames punchPowerUpGotSound;
	float timePassed = 0f;
	PlayerAttackCollider punchCollider;
	bool collected = false;
	CameraEffects cameraEffects;

	void Awake(){
		cameraEffects = Camera.main.GetComponent<CameraEffects>();
	}

	void Start(){
		foreach(GameObject characterClass in GameObject.FindGameObjectWithTag(Tags.characterClassesContainer).GetComponent<ChangeClass>().GetClassesObjects()){
			if(characterClass.name == ClassesObjectsNames.power){
				punchCollider = characterClass.GetComponent<PowerClassMovement>().punchCollider.GetComponent<PlayerAttackCollider>();
			}
		}
	}

	void Update () {
		if(collected){
			if(timePassed <= timePowerUp){
				timePassed += Time.deltaTime;
			}else{
				punchCollider.SubtractDamageAlteration(damageToAdd);
				foreach(GameObject objectToShow in objectsToShowWhilePoweredUp){
					objectToShow.SetActive(false);
				}
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.characterClass){
			if(!collected){
				collected = true;

				cameraEffects.StartVignetteChange();
				AudioManager.PlaySound(punchPowerUpGotSound.ToString(), transform.position);

				punchCollider.AddDamageAlteration(damageToAdd);

				foreach(GameObject objectToShow in objectsToShowWhilePoweredUp){
					objectToShow.SetActive(true);
				}
				foreach(GameObject objectToHide in objectsToHideWhilePoweredUp){
					objectToHide.SetActive(false);
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class ExitManager : MonoBehaviour {

	public string messageIfHasGreenKey;
	public string messageIfDontHaveGreenKey;
	public GameObject endingFadeObject;
	private ScreenMessageManager screenMessageManager;
	private InventoryManager inventory;

	public GameObject doorToClose;
	Animator animator;
	HashAnimatorLadderDoor hashAnimatorLadderDoor;

	
	void Awake(){
		inventory = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<InventoryManager>();
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		screenMessageManager = gameController.GetComponent<ScreenMessageManager>();

		animator = doorToClose.GetComponent<Animator>();
		hashAnimatorLadderDoor = doorToClose.GetComponent<HashAnimatorLadderDoor>();
	}
	
	void OnTriggerEnter(Collider col){
		if( col.gameObject.tag == Tags.characterClass ){
			if( inventory.getHasGreenKey() ){
				FinishLevel(col.gameObject);
			}else{
				DontHaveTheKeyMessage();
			}
		}
	}

	void DontHaveTheKeyMessage(){
		screenMessageManager.NewMessage(messageIfDontHaveGreenKey);
	}

	void FinishLevel(GameObject playerObject){
		endingFadeObject.SetActive(true);
		playerObject.GetComponent<DefaultMovement>().setStoppedOnAnimation(true);
		animator.SetBool(hashAnimatorLadderDoor.opened, false);
	}
}

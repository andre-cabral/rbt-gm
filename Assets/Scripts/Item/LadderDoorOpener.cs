using UnityEngine;
using System.Collections;

public class LadderDoorOpener : MonoBehaviour {

	public KeysNames keyNeeded;
	public string messageIfHasKey;
	public string messageIfDontHaveKey;
	public GameObject doorToOpen;
	Animator animator;
	HashAnimatorLadderDoor hashAnimatorLadderDoor;
	private ScreenMessageManager screenMessageManager;
	private InventoryManager inventory;
	
	
	void Awake(){
		inventory = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<InventoryManager>();
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		screenMessageManager = gameController.GetComponent<ScreenMessageManager>();
		animator = doorToOpen.GetComponent<Animator>();
		hashAnimatorLadderDoor = doorToOpen.GetComponent<HashAnimatorLadderDoor>();
	}
	
	void OnTriggerEnter(Collider col){
		if( col.gameObject.tag == Tags.characterClass ){
			if(keyNeeded == KeysNames.greenKey){
				if( inventory.getHasGreenKey() ){
					Open();
				}else{
					DontHaveTheKeyMessage();
				}
			}else if(keyNeeded == KeysNames.noKey){
				Open();
			}
		}
	}

	void OnTriggerExit(Collider col){
		if( col.gameObject.tag == Tags.characterClass ){
			Close();
		}
	}
	
	void DontHaveTheKeyMessage(){
		screenMessageManager.NewMessage(messageIfDontHaveKey);
	}
	
	void Open(){
		if(messageIfHasKey != ""){
			screenMessageManager.NewMessage(messageIfHasKey);
		}
		animator.SetBool(hashAnimatorLadderDoor.opened, true);
	}

	void Close(){
		animator.SetBool(hashAnimatorLadderDoor.opened, false);
	}

}

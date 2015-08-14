using UnityEngine;
using System.Collections;

public class ExitManager : MonoBehaviour {

	public string messageIfHasGreenKey;
	public string messageIfDontHaveGreenKey;
	private ScreenMessageManager screenMessageManager;
	private InventoryManager inventory;

	
	void Awake(){
		inventory = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<InventoryManager>();
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		screenMessageManager = gameController.GetComponent<ScreenMessageManager>();
	}
	
	void OnTriggerEnter(Collider col){
		if( col.gameObject.tag == Tags.characterClass ){
			if( inventory.getHasGreenKey() ){
				FinishLevel();
			}else{
				DontHaveTheKeyMessage();
			}
		}
	}

	void DontHaveTheKeyMessage(){
		screenMessageManager.NewMessage(messageIfDontHaveGreenKey);
	}

	void FinishLevel(){
		screenMessageManager.NewMessage(messageIfHasGreenKey);
		Time.timeScale = 0f;
	}
}

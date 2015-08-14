using UnityEngine;
using System.Collections;

public class ScreenMessageChangeOnTrigger : MonoBehaviour {

	public string message;
	private ScreenMessageManager screenMessageManager;

	void Awake(){
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		screenMessageManager = gameController.GetComponent<ScreenMessageManager>();
	}
	
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.characterClass){
			screenMessageManager.NewMessage(message);
		}
	}
}

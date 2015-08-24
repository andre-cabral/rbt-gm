﻿using UnityEngine;
using System.Collections;

public class GetGreenKey : MonoBehaviour {

	public string message;
	private ScreenMessageManager screenMessageManager;
	private InventoryManager inventory;

	void Awake(){
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		inventory = gameController.GetComponent<InventoryManager>();
		screenMessageManager = gameController.GetComponent<ScreenMessageManager>();
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.characterClass){
			inventory.setHasGreenKey(true);
			screenMessageManager.NewMessage(message);
			Destroy(gameObject);
		}
	}

}
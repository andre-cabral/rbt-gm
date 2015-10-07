﻿using UnityEngine;
using System.Collections;

public class GetGreenKey : MonoBehaviour {

	public string message;
	public AudioNames gotKeySound;
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
			AudioManager.PlaySound(gotKeySound.ToString(), transform.position);
			Destroy(gameObject);
		}
	}

}

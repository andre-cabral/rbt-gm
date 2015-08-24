﻿using UnityEngine;
using System.Collections;

public class EnemyAttackCollider : MonoBehaviour {

	public int damage = 1;
	public bool isBlockable = true;
	private bool isBlocked = false;
	public bool isDodgeable = true;
	private bool isDodged = false;
	private int damageReduction = 0;

	private GameObject characterClassesContainerObject;
	private GameObject playerPower;
	private PowerClassMovement powerClassScript;
	private GameObject playerStealth;
	private StealthClassMovement stealthClassScript;

	void Awake(){
		//Used transform.Find on the active parent object "CharacterClassesContainer"
		//because GameObject.FindGameObjectsWithTag cant find inactive Game Objects
		characterClassesContainerObject = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer);

		playerPower = characterClassesContainerObject.transform.Find(ClassesObjectsNames.power).gameObject;
		powerClassScript = playerPower.GetComponent<PowerClassMovement>();

		playerStealth = characterClassesContainerObject.transform.Find(ClassesObjectsNames.stealth).gameObject;
		stealthClassScript = playerStealth.GetComponent<StealthClassMovement>();

	}


	void Update(){
		if(playerPower.activeSelf && !powerClassScript.getIsBlocking() ){
			BlockEnd();
		}
		if(playerStealth.activeSelf && !stealthClassScript.getIsDodging() ){
			DodgeEnd();
		}
	}

	void OnTriggerEnter(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		
		if(collidedObject.tag == Tags.blockingCollider){
			BlockStart(collidedObject);
		}
		if(collidedObject.tag == Tags.dodgeCollider){
			DodgeStart(collidedObject);
		}		
	}

	void BlockStart(GameObject collidedObject){
		isBlocked = true;
		damageReduction = collidedObject.GetComponent<BlockingCollider>().getDamageReduction();
	}
	void DodgeStart(GameObject collidedObject){
		isDodged = true;
		damageReduction = collidedObject.GetComponent<DodgingCollider>().getDamageReduction();
	}
	void BlockEnd(){
		isBlocked = false;
		damageReduction = 0;
	}
	void DodgeEnd(){
		isDodged = false;
		damageReduction = 0;
	}

	public void ResetDamageReduction(){
		damageReduction = 0;
	}

	public int DamageDealt(){
		if( (playerPower.activeSelf && !isBlockable) || (playerStealth.activeSelf && !isDodgeable) 
		   || (playerPower.activeSelf && !isBlocked) || (playerStealth.activeSelf && !isDodged) ){
			return damage;
		}else{
			if(damage > damageReduction ){
				return damage - damageReduction;
			}else{
				return 0;
			}
		}
	}

}

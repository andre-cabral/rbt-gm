using UnityEngine;
using System.Collections;

public class SpecificClassMovementTemplate : MonoBehaviour {

	public DefaultMovement defaultMovementScript;
	
	void Update () {
		if(!defaultMovementScript.getIsDead() && !defaultMovementScript.getStoppedOnAnimation()){
			if(Input.GetButtonDown(Buttons.power0) ){
				
			}
			if(Input.GetButtonDown(Buttons.power1) ){
				
			}
		}
	}
}

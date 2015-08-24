using UnityEngine;
using System.Collections;

public class FlickerMeshWhenDamaged : MonoBehaviour {

	
	public MeshRenderer[] meshRenderersToFlicker;
	public GameObject[] gameObjectsToFlicker;
	public float timeToDisappear = 0.05f;
	public float timeToAppear = 0.05f;
	public float timeToFlicker = 1f;
	private float actualTime = 0f;
	public float totalTimeFlicker = 0f;
	private bool areTheObjectsOn = true;
	public bool flicker = false;
	
	void Update () {
		
		if(flicker){
			if(timeToFlicker >= totalTimeFlicker){
				//flicker on
				if(areTheObjectsOn && actualTime <= timeToAppear){
					actualTime += Time.deltaTime;
				}
				if(areTheObjectsOn && actualTime > timeToAppear){
					actualTime = 0f;
					TurnObjectsOff();
				}
				
				//flicker off
				if(!areTheObjectsOn && actualTime <= timeToDisappear){
					actualTime += Time.deltaTime;
				}
				if(!areTheObjectsOn && actualTime > timeToDisappear){
					actualTime = 0f;
					TurnObjectsOn();
				}
				totalTimeFlicker += Time.deltaTime;
			}else{
				totalTimeFlicker = 0f;
				flicker = false;
				
				TurnObjectsOn();
			}
		}
		
	}
	
	void TurnObjectsOn(){
		if(meshRenderersToFlicker.Length>0){
			foreach(MeshRenderer objectToTurnOn in meshRenderersToFlicker){
				objectToTurnOn.enabled = true;
			}
		}else{
			foreach(GameObject objectToTurnOn in gameObjectsToFlicker){
				objectToTurnOn.SetActive(true);
			}
		}
		areTheObjectsOn = true;
	}
	void TurnObjectsOff(){
		if(meshRenderersToFlicker.Length>0){
			foreach(MeshRenderer objectToTurnOn in meshRenderersToFlicker){
				objectToTurnOn.enabled = false;
			}
		}else{
			foreach(GameObject objectToTurnOn in gameObjectsToFlicker){
				objectToTurnOn.SetActive(false);
			}
		}
		areTheObjectsOn = false;
	}
	
	public void startFlickering(){
		this.flicker = true;
		TurnObjectsOff();
	}
	public bool getFlicker(){
		return flicker;
	}

}

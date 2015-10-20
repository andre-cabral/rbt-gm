using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMovement : MonoBehaviour {
	public GameObject characterClassesContainer;
	//public float height = 15.5f;
	public float distance = 10f;
	public GameObject audioListenerObject;
	//public float audioListenerDistance = 10f;
	public float yAdjust = 0.1f;
	public float minZoomDistance = 6f;
	public float maxZoomDistance = 15f;
	public float zoomSpeed = 1f;
	public float zoomSpeedMouseScrollZoomAxis = 100f;
	public float smoothVelocity = 7f;
	public bool followMouse = true;
	public float mouseFollowXLimit = 3f;
	public float mouseFollowYLimit = 3f;
	Vector3 mouseFollowPosition = Vector3.zero;

	private List<GameObject> classesObjects;

	void Awake(){
		classesObjects = characterClassesContainer.GetComponent<ChangeClass>().GetClassesObjects();
	}

	void Update(){
		if(Input.GetAxis(Buttons.zoom) != 0){
			float zoomToAdd = Input.GetAxis(Buttons.zoom) * zoomSpeedMouseScrollZoomAxis * Time.deltaTime * -1;
			if(distance + zoomToAdd >= minZoomDistance && distance + zoomToAdd <= maxZoomDistance){
				distance += zoomToAdd;
			}
		}
	}


	void FixedUpdate () {
		foreach(GameObject classObject in classesObjects){
			if(classObject.activeSelf){
				//transform.LookAt(classObject.transform.position);
				//Vector3 targetPosition = new Vector3(classObject.transform.position.x, height, classObject.transform.position.z-distance);

				if(followMouse){
					mouseFollowPosition = Input.mousePosition;
					mouseFollowPosition = new Vector3( ((mouseFollowPosition.x/Screen.width)-0.5f)*2*mouseFollowXLimit, ((mouseFollowPosition.y/Screen.height)-0.5f)*2*mouseFollowYLimit, 0f);
				}

				Vector3 targetPosition = new Vector3(classObject.transform.position.x+mouseFollowPosition.x, distance + (yAdjust * distance), classObject.transform.position.z-distance+mouseFollowPosition.y);
				transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothVelocity);

				audioListenerObject.transform.position = classObject.transform.position;
			}
		}
	}

}

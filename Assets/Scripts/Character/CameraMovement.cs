using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject characterClassesContainer;
	//public float height = 15.5f;
	public float distance = 10f;
	public float minZoomDistance = 6f;
	public float maxZoomDistance = 15f;
	public float zoomSpeed = 1f;
	public float zoomSpeedMouseScrollZoomAxis = 100f;
	public float smoothVelocity = 7f;

	private ArrayList classesObjects;

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

				Vector3 targetPosition = new Vector3(classObject.transform.position.x, distance, classObject.transform.position.z-distance);
				transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothVelocity);
			}
		}
	}

}

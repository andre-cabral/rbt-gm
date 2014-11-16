using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject characterClassesContainer;
	public float height = 15.5f;
	public float distance = 13f;
	public float smoothVelocity = 7f;

	private ArrayList classesObjects;

	void Awake(){
		classesObjects = characterClassesContainer.GetComponent<ChangeClass>().GetClassesObjects();
	}

	// Update is called once per frame
	void FixedUpdate () {
		foreach(GameObject classObject in classesObjects){
			if(classObject.activeSelf){
				//transform.LookAt(classObject.transform.position);
				Vector3 targetPosition = new Vector3(classObject.transform.position.x, height, classObject.transform.position.z-distance);
				transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothVelocity);
			}
		}
	}

}

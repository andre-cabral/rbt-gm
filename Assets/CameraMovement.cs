using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject characterClassesContainer;
	public float height;
	public float distance;

	private ArrayList classesObjects;

	void Awake(){
		classesObjects = characterClassesContainer.GetComponent<ChangeClass>().GetClassesObjects();
	}

	// Update is called once per frame
	void FixedUpdate () {
		foreach(GameObject classObject in classesObjects){
			if(classObject.activeSelf){
				//transform.LookAt(classObject.transform.position);
				transform.position = new Vector3(classObject.transform.position.x, height, classObject.transform.position.z-distance);
			}
		}
	}

}

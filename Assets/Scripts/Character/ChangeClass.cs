using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeClass : MonoBehaviour {

	private bool changingClass = false;
	private GameObject activeClass;
	public GameObject class0;
	public GameObject class1;
	public GameObject class2;
	private List<GameObject> classesObjects = new List<GameObject>();

	void Awake(){
		classesObjects.Add(class0);
		classesObjects.Add(class1);
		classesObjects.Add(class2);
		foreach(GameObject classObject in classesObjects){
			if(classObject.activeSelf){
				activeClass = classObject;
			}
		}
	}

	public void ActivateClass(GameObject classSelected){
		changingClass = true;

		foreach(GameObject classObject in classesObjects){

			if(classObject.activeSelf){
				classSelected.transform.position = classObject.transform.position;
				classSelected.transform.rotation = classObject.transform.rotation;
			}

			classObject.SetActive(false);
		}

		classSelected.SetActive(true);

		changingClass = false;

		activeClass = classSelected;
	}

	public List<GameObject> GetClassesObjects(){
		return classesObjects;
	}

	public bool GetChangingClass(){
		return changingClass;
	}

	public GameObject GetActiveClass(){
		return activeClass;
	}

}

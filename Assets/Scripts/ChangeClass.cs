using UnityEngine;
using System.Collections;

public class ChangeClass : MonoBehaviour {

	private bool changingClass = false;
	public GameObject class0;
	public GameObject class1;
	public GameObject class2;
	private ArrayList classesObjects = new ArrayList();

	void Awake(){
		classesObjects.Add(class0);
		classesObjects.Add(class1);
		classesObjects.Add(class2);
	}
	
	void Update () {

		if( Input.GetButtonDown(Buttons.class0) && !changingClass ){
			ActivateClass(class0);
		}
		if( Input.GetButtonDown(Buttons.class1) && !changingClass  ){
			ActivateClass(class1);
		}
		if( Input.GetButtonDown(Buttons.class2) && !changingClass  ){
			ActivateClass(class2);
		}

	}

	void ActivateClass(GameObject classSelected){
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
	}

}

using UnityEngine;
using System.Collections;

public class SelectMenuByClass : MonoBehaviour {

	public GameObject toughObject;
	public GameObject stealthObject;
	public GameObject rangedObject;

	ChangeClass changeClass;

	void Awake () {
		changeClass = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer).GetComponent<ChangeClass>();
	}

	void OnEnable() {
		if(changeClass.GetActiveClass() != null){
			DeactivateAll();
			switch(changeClass.GetActiveClass().name){
				case ClassesObjectsNames.power :
					toughObject.SetActive(true);
				break;
				case ClassesObjectsNames.stealth :
					stealthObject.SetActive(true);
				break;
				case ClassesObjectsNames.ranged :
					rangedObject.SetActive(true);
				break;
			}
		}
	}

	void DeactivateAll(){
		toughObject.SetActive(false);
		stealthObject.SetActive(false);
		rangedObject.SetActive(false);
	}
}

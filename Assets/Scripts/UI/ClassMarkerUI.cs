using UnityEngine;
using System.Collections;

public class ClassMarkerUI : MonoBehaviour {

	public float rotationClass0 = 120f;
	public float rotationClass1 = 240f;
	public float rotationClass2 = 0f;
	ChangeClass changeClassScript;
	Animator animator;
	HashAnimatorClassUI hashAnimatorClassUI;
	int actualClass = 0;
	int classToGo = 0;

	void Awake(){
		changeClassScript = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer).GetComponent<ChangeClass>();
		animator = GetComponent<Animator>();
		hashAnimatorClassUI = GetComponent<HashAnimatorClassUI>();
	}

	void Start(){
		float yRotation = 0f;
		if( changeClassScript.class0.Equals(changeClassScript.GetActiveClass()) ){
			actualClass = 0;
			yRotation = rotationClass0;
		}
		
		if( changeClassScript.class1.Equals(changeClassScript.GetActiveClass()) ){
			actualClass = 1;
			yRotation = rotationClass1;
		}
		
		if( changeClassScript.class2.Equals(changeClassScript.GetActiveClass()) ){
			actualClass = 2;
			yRotation = rotationClass2;
		}
		classToGo = actualClass;
		transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, yRotation, transform.localRotation.eulerAngles.z);
		RotateTo();
	}

	void RotateTo(){
		animator.SetInteger(hashAnimatorClassUI.classToGo, classToGo);
	}

	public int getActualClass(){
		return actualClass;
	}
	
	public void setActualClass(int actualClass){
		this.actualClass = actualClass;
	}

	public int getClassToGo(){
		return classToGo;
	}
	
	public void setClassToGo(int classToGo){
		this.classToGo = classToGo;
	}

	public void changingClassStart(int classToGo){
		this.classToGo = classToGo;
		RotateTo();
	}

	public void changingClassEnd(){
		actualClass = classToGo;
	}
}

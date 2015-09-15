using UnityEngine;
using System.Collections;

public class ClassMarkerUI : MonoBehaviour {

	ChangeClass changeClassScript;
	Animator animator;
	int actualClass = 0;
	int classToGo = 0;

	void Awake(){
		changeClassScript = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer).GetComponent<ChangeClass>();
		animator = GetComponent<Animator>();

		if( changeClassScript.class0.Equals(changeClassScript.GetActiveClass()) ){
			actualClass = 0;
		}
		
		if( changeClassScript.class1.Equals(changeClassScript.GetActiveClass()) ){
			actualClass = 1;
		}
		
		if( changeClassScript.class2.Equals(changeClassScript.GetActiveClass()) ){
			actualClass = 2;
		}
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
	}

	public void changingClassEnd(){
		actualClass = classToGo;
	}
}

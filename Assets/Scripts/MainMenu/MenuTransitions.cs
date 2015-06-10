using UnityEngine;
using System.Collections;

public class MenuTransitions : MonoBehaviour {
	Animator animatorObject;
	HashAnimatorMainMenu hashMainMenu;


	void Awake(){
		animatorObject = GetComponent<Animator>();
		hashMainMenu = GetComponent<HashAnimatorMainMenu>();
	}

	public void MainToOptions () {
		animatorObject.SetTrigger(hashMainMenu.mainToOptions);
	}

	public void OptionsToMain(){
		animatorObject.SetTrigger(hashMainMenu.optionsToMain);
	}
}

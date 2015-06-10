using UnityEngine;
using System.Collections;

public class HashAnimatorMainMenu : MonoBehaviour {

	public int mainToOptions;
	public int optionsToMain;
	
	void Awake() {
		mainToOptions = Animator.StringToHash("MainToOptions");
		optionsToMain = Animator.StringToHash("OptionsToMain");
	}
}

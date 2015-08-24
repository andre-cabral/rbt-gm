using UnityEngine;
using System.Collections;

public class HashAnimatorSpiderEnemy : MonoBehaviour {

	public int webLaunching;
	
	
	void Awake() {
		webLaunching = Animator.StringToHash("WebLaunching");
	}
}

using UnityEngine;
using System.Collections;

public class HashAnimatorClassUI : MonoBehaviour {

	public int classToGo;
	
	
	void Awake() {
		classToGo = Animator.StringToHash("ClassToGo");
	}
}

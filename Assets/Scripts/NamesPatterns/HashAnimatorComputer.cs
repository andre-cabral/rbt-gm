using UnityEngine;
using System.Collections;

public class HashAnimatorComputer : MonoBehaviour {

	public int open;
	
	void Awake() {
		open = Animator.StringToHash("Open");
	}
}

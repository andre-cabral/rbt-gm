using UnityEngine;
using System.Collections;

public class HashAnimatorLadderDoor : MonoBehaviour {

	public int opened;
	
	void Awake() {
		opened = Animator.StringToHash("Opened");
	}
}

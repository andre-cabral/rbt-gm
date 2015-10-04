using UnityEngine;
using System.Collections;

public class HashAnimatorWeb : MonoBehaviour {

	public int hitPlayer;
	
	void Awake() {
		hitPlayer = Animator.StringToHash("HitPlayer");
	}
}

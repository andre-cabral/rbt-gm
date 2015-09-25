using UnityEngine;
using System.Collections;

public class UnpauseOnAwake : MonoBehaviour {

	void Awake () {
		DefaultMovement.setPause(false);
	}
}

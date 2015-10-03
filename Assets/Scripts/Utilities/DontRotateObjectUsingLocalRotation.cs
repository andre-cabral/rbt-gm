using UnityEngine;
using System.Collections;

public class DontRotateObjectUsingLocalRotation : MonoBehaviour {
	
	Quaternion startRotation;
	
	void Awake () {
		startRotation = transform.localRotation;
	}
	
	void Update () {
		transform.rotation = startRotation;
	}
}

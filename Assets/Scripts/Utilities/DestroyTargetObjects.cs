using UnityEngine;
using System.Collections;

public class DestroyTargetObjects : MonoBehaviour {
	public GameObject[] targetObjects;

	public void DestroyTheTargetObjects(){
		foreach(GameObject targetObject in targetObjects){
			Destroy(targetObject);
		}
	}
}

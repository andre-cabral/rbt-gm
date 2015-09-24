using UnityEngine;
using System.Collections;

public class ExplosionTime : MonoBehaviour {

	public float timeExploding = 1f;
	float timeExplodingPassed = 0f;
	
	// Update is called once per frame
	void Update () {
		if(timeExplodingPassed < timeExploding){
			timeExplodingPassed += Time.deltaTime;
		}else{
			Destroy(gameObject);
		}
	}
}

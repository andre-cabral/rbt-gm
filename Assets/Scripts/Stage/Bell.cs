using UnityEngine;
using System.Collections;

public class Bell : MonoBehaviour {

	public float timeToRing = 3f;
	public bool ringOnlyOnce = true;
	bool ringed = false;
	bool ringing = false;
	float timeRinged = 0f;


	void Update () {
		if(ringing){
			if(timeRinged < timeToRing){
				timeRinged += Time.deltaTime;
			}else{
				ringing = false;
				timeRinged = 0f;
				gameObject.tag = Tags.bell;
			}
		}
	}

	void OnTriggerEnter(Collider collider){
		GameObject collidedObject = collider.gameObject;
		if( collidedObject.tag == Tags.playerAttackCollider && !ringing ){
			if(!(ringOnlyOnce && ringed)){
				ringing = true;
				ringed = true;
				gameObject.tag = Tags.bellRinging;
			}
		}
	}

	public bool getRinging(){
		return ringing;
	}
}

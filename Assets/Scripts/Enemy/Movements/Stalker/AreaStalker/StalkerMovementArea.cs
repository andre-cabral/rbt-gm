using UnityEngine;
using System.Collections;

public class StalkerMovementArea : MonoBehaviour {

	public bool MoveYAxis = false;
	public float velocity = 0.15f;
	public float timeIdleAfterHitPlayer = 1f;
	private GameObject stalkedPlayerObject;
	private bool isStalking = false;
	private bool playerHit = false;
	private float timeCountAfterHitPlayer = 0f;
	//EnemyCollisions enemyCollisions;
	
	// Use this for initialization
	void Awake () {
		stalkedPlayerObject = GameObject.FindGameObjectWithTag(Tags.characterClass);
		//enemyCollisions = gameObject.GetComponent<EnemyCollisions>();
	}
	
	
	void FixedUpdate () {
		//if(!enemyCollisions.getTakingDamage()){
			if(isStalking){

				Movement(new Vector3(stalkedPlayerObject.gameObject.transform.position.x, transform.localPosition.y, stalkedPlayerObject.gameObject.transform.position.z), velocity);
			}
		//}
	}
	
	
	void Movement(Vector3 end, float velocity){
		Flip (end);

		if(!playerHit){
			timeCountAfterHitPlayer = 0f;
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, velocity);
		}else{
			timeCountAfterHitPlayer += Time.deltaTime;
			if(timeCountAfterHitPlayer > timeIdleAfterHitPlayer){
				playerHit = false;
			}
		}
	}
	
	void Flip(Vector3 end){
		transform.LookAt(end);
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == Tags.characterClass){
			playerHit = true;
			stalkedPlayerObject = collision.gameObject;
		}
	}

	public void setIsStalking(bool isStalking){
		this.isStalking = isStalking;
	}
}

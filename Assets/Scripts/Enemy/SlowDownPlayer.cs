using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SlowDownPlayer : MonoBehaviour {
	public float speedToReduceFromPlayerSpeed = 0.01f;
	public float timeReducingSpeed = 2f;
	public float timeToDestroyOnWall = 0.5f;
	private Collider colliderComponent;
	private bool collidedWithPlayer = false;
	private ChangeClass changeClassScript;
	private Vector3 positionFromPlayer = new Vector3();
	private DefaultMovement[] allPlayersMovements;
	private static bool isSlowedDown = false;
	private static float maxSlowDownCollidedWithPlayer = 0f;
	private static List<float> slowDownCollidedWithPlayerList = new List<float>();
	private float timePassed = 0f;
	private Rigidbody rb;

	void Awake(){
		changeClassScript = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer).GetComponent<ChangeClass>();

		rb = GetComponent<Rigidbody>();
		colliderComponent = GetComponent<Collider>();
	}

	void Start(){
		GameObject[] characterClassObjects = changeClassScript.GetClassesObjects().ToArray();
		allPlayersMovements = new DefaultMovement[characterClassObjects.Length];
		for(int i=0; i<characterClassObjects.Length; i++){
			allPlayersMovements[i] = characterClassObjects[i].GetComponent<DefaultMovement>();
		}
	}

	void Update () {
		if(collidedWithPlayer){
			if(timePassed < timeReducingSpeed){
				timePassed += Time.deltaTime;
			}else{
				SlowDownEnd();
			}

			FollowPlayer();
		}

		if(!isSlowedDown && collidedWithPlayer){
			SlowDownEffectAfter();
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == Tags.wall && !collidedWithPlayer){
			Destroy(this.gameObject, timeToDestroyOnWall);
		}

		if(collider.tag == Tags.characterClass && !collidedWithPlayer){
			ObjectStopOnPlayer(collider);
			SlowDownEffect();
			//StartFollowingPlayer();
		}
	}

//########Follow Player START
//###########################################
	/*
	void StartFollowingPlayer(){
		positionFromPlayer = changeClassScript.GetActiveClass().transform.position - transform.position;
	}
	*/
	void FollowPlayer(){
		transform.position = changeClassScript.GetActiveClass().transform.position /*- positionFromPlayer*/;
	}

	void ObjectStopOnPlayer(Collider collidedTarget){
		colliderComponent.enabled = false;
		
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		rb.isKinematic = true;
	}
//########Follow Player END
//###########################################


//########Slow Down Effect START
//###########################################
	void SlowDownEffect(){
		collidedWithPlayer = true;
		slowDownCollidedWithPlayerList.Add(speedToReduceFromPlayerSpeed);
		if(maxSlowDownCollidedWithPlayer < speedToReduceFromPlayerSpeed){
			maxSlowDownCollidedWithPlayer = speedToReduceFromPlayerSpeed;
		}

		foreach(DefaultMovement defaultMovementScript in allPlayersMovements){
			defaultMovementScript.SpeedReduceOnce(speedToReduceFromPlayerSpeed);
		}
		isSlowedDown = true;
	}

	void SlowDownEffectAfter(){
		foreach(DefaultMovement defaultMovementScript in allPlayersMovements){
			defaultMovementScript.SpeedReduceOnce(maxSlowDownCollidedWithPlayer);
		}
		isSlowedDown = true;
	}

	void SlowDownEnd(){
		foreach(DefaultMovement defaultMovementScript in allPlayersMovements){
			defaultMovementScript.SpeedReset();
		}
		isSlowedDown = false;
		slowDownCollidedWithPlayerList.Remove(speedToReduceFromPlayerSpeed);
		maxSlowDownCollidedWithPlayer = 0f;
		foreach(float reduce in slowDownCollidedWithPlayerList){
			if(reduce > maxSlowDownCollidedWithPlayer){
				maxSlowDownCollidedWithPlayer = reduce;
			}
		}
		Destroy(gameObject);
	}
//########Slow Down Effect END
//###########################################
}
using UnityEngine;
using System.Collections;

public class StalkerNavMesh : MonoBehaviour {
	
	private Animator enemyAnimator;
	public GameObject attackHitBoxObject;
	private HashAnimatorStalkerEnemy hashAnimator;
	public float attackDelay = 1f;

	private GameObject stalkedPlayerObject;
	private Transform stalkedPlayerObjectTransform;
	NavMeshAgent agent;

	private GameObject classContainer;
	private ChangeClass changeClassScript;

	private EnemyLife enemyLifeScript;

	private bool attacking = false;
	private bool attackingDelayCount = false;
	private float attackingDelayPassed = 0f;

	public float lookAtPlayerSpeed = 3f;

	void Awake () {
		agent = gameObject.GetComponent<NavMeshAgent>();

		classContainer = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer);
		changeClassScript = classContainer.GetComponent<ChangeClass>();

		enemyLifeScript = GetComponent<EnemyLife>();

		enemyAnimator = GetComponent<Animator>();
		hashAnimator = GetComponent<HashAnimatorStalkerEnemy>();
	}

	void Start(){
		stalkedPlayerObject = changeClassScript.GetActiveClass();
		stalkedPlayerObjectTransform = stalkedPlayerObject.transform;
	}
	
	void Update () {
		if(!enemyLifeScript.getIsDead()){
			Stalk();
		}
		AttackDelayCounter ();
	}


//########Stalk Movement START
//###########################################
	void Stalk(){
		if( !stalkedPlayerObject.Equals(changeClassScript.GetActiveClass()) ){
			stalkedPlayerObject = changeClassScript.GetActiveClass();
			stalkedPlayerObjectTransform = stalkedPlayerObject.transform;
		}

		Vector3 destination = new Vector3(stalkedPlayerObjectTransform.position.x,transform.localPosition.y,stalkedPlayerObjectTransform.position.z);

		if(Vector3.Distance(transform.position, destination) > agent.stoppingDistance){
			agent.SetDestination(destination);
			//Flip(new Vector3(agent.destination.x, transform.position.y, agent.destination.z));
		}else{ 
			if(!attacking && !attackingDelayCount){
				AttackStart();
			}
			FlipWithSpeed(new Vector3(destination.x, transform.position.y, destination.z), lookAtPlayerSpeed);
		}

	}

	void FlipWithSpeed(Vector3 end, float speed){
		Quaternion finalRotation = Quaternion.LookRotation(end - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * speed);
	}
//########Stalk Movement END
//###########################################


//########Attacking START
//###########################################
	void AttackStart(){
		attacking = true;
		attackHitBoxObject.SetActive(true);
		enemyAnimator.SetBool(hashAnimator.attacking, true);
	}

	public void AttackFinish(){
		//function called with an event in the end of the attack animation
		attackingDelayCount = true;
		attackHitBoxObject.SetActive(false);
	}

	void AttackDelayFinish(){
		attacking = false;
		attackingDelayCount = false;
		attackingDelayPassed = 0f;
	}

	void AttackDelayCounter(){
		if(attackDelay >= attackingDelayPassed){
			attackingDelayPassed += Time.deltaTime;
		}else{
			AttackDelayFinish();
		}
	}
//########Attacking END
//###########################################

}

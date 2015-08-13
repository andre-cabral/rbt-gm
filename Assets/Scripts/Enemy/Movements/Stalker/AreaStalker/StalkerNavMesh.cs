using UnityEngine;
using System.Collections;

public class StalkerNavMesh : MonoBehaviour {

	public bool moveYAxis = false;

	public GameObject[] patrolWaypoints;
	public float distanceToChangeWaypoint = 0.5f;
	private Vector3[] points;
	private int vectorIndex = 1;

	private Vector3 lastPlayerSeenResetPosition = new Vector3(9999f,9999f,9999f);
	public Vector3 lastPlayerSeen = new Vector3(9999f,9999f,9999f);
	private bool isSeeingPlayer = false;

	private ChangeClass changeClassContainerScript;

	private Animator enemyAnimator;
	public GameObject attackHitBoxObject;
	private HashAnimatorStalkerEnemy hashAnimator;
	public float attackDelay = 1f;

	NavMeshAgent agent;

	private EnemyLife enemyLifeScript;

	private bool attacking = false;
	private bool attackingDelayCount = false;
	private float attackingDelayPassed = 0f;

	public float lookAtPlayerSpeed = 3f;

	void Awake () {
		agent = gameObject.GetComponent<NavMeshAgent>();

		//Patrol instance START
		points = new Vector3[patrolWaypoints.Length];
		
		for(int i=0; i<patrolWaypoints.Length; i++ ){
			if(!moveYAxis){
				points[i] = new Vector3(patrolWaypoints[i].transform.position.x, transform.position.y, patrolWaypoints[i].transform.position.z);
			}else{
				points[i] = new Vector3(patrolWaypoints[i].transform.position.x, patrolWaypoints[i].transform.position.y, patrolWaypoints[i].transform.position.z);
			}
		}

		if(points.Length == 1){
			vectorIndex = 0;
		}
		//Patrol instance END

		enemyLifeScript = GetComponent<EnemyLife>();
		changeClassContainerScript = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer).GetComponent<ChangeClass>();

		enemyAnimator = GetComponent<Animator>();
		hashAnimator = GetComponent<HashAnimatorStalkerEnemy>();
	}
		
	void Update () {
		if(!enemyLifeScript.getIsDead() && lastPlayerSeen == lastPlayerSeenResetPosition){
			Patrol();
		}
		if(!enemyLifeScript.getIsDead() && lastPlayerSeen != lastPlayerSeenResetPosition){
			Stalk();
		}
		AttackDelayCounter ();
	}

//########Patrol Movement START
//###########################################
	void Patrol(){
		Vector3 destination;
		if(!moveYAxis){
			destination = new Vector3(points[vectorIndex].x,transform.position.y,points[vectorIndex].z);
		}else{
			destination = new Vector3(points[vectorIndex].x,points[vectorIndex].y,points[vectorIndex].z);
		}

		if( Vector3.Distance(transform.position, destination) > distanceToChangeWaypoint ){
			if(agent.destination != destination){
				agent.SetDestination(destination);
			}
		}else{
			if(vectorIndex < points.Length-1){
				vectorIndex++;
			}else{
				vectorIndex = 0;
			}
		}
		
	}
//########Patrol Movement END
//###########################################

//########Stalk Movement START
//###########################################
	void Stalk(){

		Vector3 destination;
		if(!moveYAxis){
			destination = new Vector3(lastPlayerSeen.x,transform.position.y,lastPlayerSeen.z);
		}else{
			destination = new Vector3(lastPlayerSeen.x,lastPlayerSeen.y,lastPlayerSeen.z);
		}
		if(Vector3.Distance(transform.position, destination) > agent.stoppingDistance){
			Debug.Log(agent.destination);
			if(agent.destination != destination){
				agent.SetDestination(destination);
			}
		}else{ 
			if(isSeeingPlayer){
				if(!attacking && !attackingDelayCount){
					AttackStart();
				}
				FlipWithSpeed(new Vector3(destination.x, transform.position.y, destination.z), lookAtPlayerSpeed);
			}else{
				resetLastPlayerSeen();
			}
		}

	}

	void FlipWithSpeed(Vector3 end, float speed){
		Quaternion finalRotation = Quaternion.LookRotation(end - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * speed);
	}

	public void SeePlayer(){
		lastPlayerSeen = changeClassContainerScript.GetActiveClass().transform.position;
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

	//function called with an event in the end of the attack animation
	public void AttackFinish(){
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

	public Vector3 getLastPlayerSeen(){
		return lastPlayerSeen;
	}

	public void setLastPlayerSeen(Vector3 lastPlayerSeen){
		this.lastPlayerSeen = lastPlayerSeen;
	}

	public void resetLastPlayerSeen(){
		lastPlayerSeen = lastPlayerSeenResetPosition;
	}

	public bool getIsSeeingPlayer(){
		return isSeeingPlayer;
	}

	public void setIsSeeingPlayer(bool isSeeingPlayer){
		this.isSeeingPlayer = isSeeingPlayer;
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HashAnimatorStalkerEnemy))]
public class PatrolAndStalkMovement : MonoBehaviour {
	
	public bool moveYAxis = false;
	
	public GameObject[] patrolWaypoints;
	public float distanceToChangeWaypoint = 0.5f;
	public float timeOnWaypoint = 0f;
	float timePassedOnWaypoint = 0f;
	private Vector3[] points;
	private int vectorIndex = 1;
	
	private Vector3 lastPlayerSeenResetPosition = new Vector3(9999f,9999f,9999f);
	public Vector3 lastPlayerSeen = new Vector3(9999f,9999f,9999f);
	private bool isSeeingPlayer = false;
	private Vector3 destination;
	
	private ChangeClass changeClassContainerScript;
	
	private Animator enemyAnimator;
	private HashAnimatorStalkerEnemy hashAnimator;
	
	NavMeshAgent agent;
	
	private EnemyLife enemyLifeScript;
	
	private bool attacking = false;
	private bool patrolling = true;
	private bool stalking = false;
	
	public float lookAtPlayerSpeed = 3f;
	
	public virtual void Awake () {
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
	
	public virtual void Update () {
		if(!enemyLifeScript.getIsDead() && lastPlayerSeen == lastPlayerSeenResetPosition && !attacking){
			Patrol();
		}
		if(!enemyLifeScript.getIsDead() && lastPlayerSeen != lastPlayerSeenResetPosition && !attacking){
			Stalk();
		}
	}
	
	//########Patrol Movement START
	//###########################################
	void Patrol(){
		patrolling = true;
		stalking = false;
		
		if(!moveYAxis){
			destination = new Vector3(points[vectorIndex].x,transform.position.y,points[vectorIndex].z);
		}else{
			destination = new Vector3(points[vectorIndex].x,points[vectorIndex].y,points[vectorIndex].z);
		}
		
		if( Vector3.Distance(transform.position, destination) > distanceToChangeWaypoint ){
			if(agent.destination != destination){
				agent.SetDestination(destination);
				enemyAnimator.SetFloat(hashAnimator.velocity, agent.desiredVelocity.magnitude);
			}
		}else{
			if(timePassedOnWaypoint >= timeOnWaypoint){
				timePassedOnWaypoint = 0f;
				if(vectorIndex < points.Length-1){
					vectorIndex++;
				}else{
					vectorIndex = 0;
				}
			}else{
				timePassedOnWaypoint += Time.deltaTime;
			}
		}
		
	}
	//########Patrol Movement END
	//###########################################
	
	//########Stalk Movement START
	//###########################################
	void Stalk(){
		stalking = true;
		patrolling = false;

		timePassedOnWaypoint = 0f;

		if(!moveYAxis){
			destination = new Vector3(lastPlayerSeen.x,transform.position.y,lastPlayerSeen.z);
		}else{
			destination = new Vector3(lastPlayerSeen.x,lastPlayerSeen.y,lastPlayerSeen.z);
		}
		if(Vector3.Distance(transform.position, destination) > agent.stoppingDistance){
			if(agent.destination != destination){
				agent.SetDestination(destination);
				enemyAnimator.SetFloat(hashAnimator.velocity, agent.desiredVelocity.magnitude);
			}
		}else if(!isSeeingPlayer){
			resetLastPlayerSeen();
		}
		
	}
	
	public void FlipWithSpeed(Vector3 end, float speed){
		Quaternion finalRotation = Quaternion.LookRotation(end - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, Time.deltaTime * speed);
	}
	
	public void SeePlayer(){
		lastPlayerSeen = changeClassContainerScript.GetActiveClass().transform.position;
	}
	//########Stalk Movement END
	//###########################################
	
	
	public bool getPatrolling(){
		return patrolling;
	}
	
	public bool getStalking(){
		return stalking;
	}
	
	public bool getAttacking(){
		return attacking;
	}
	public void setAttacking(bool attacking){
		this.attacking = attacking;
	}
	
	public Vector3 getLastPlayerSeen(){
		return lastPlayerSeen;
	}
	
	public void setLastPlayerSeen(Vector3 lastPlayerSeen){
		this.lastPlayerSeen = lastPlayerSeen;
	}
	
	public void resetLastPlayerSeen(){
		lastPlayerSeen = lastPlayerSeenResetPosition;
	}
	
	public Vector3 getLastPlayerSeenResetPosition(){
		return lastPlayerSeenResetPosition;
	}
	
	public bool getIsSeeingPlayer(){
		return isSeeingPlayer;
	}
	
	public void setIsSeeingPlayer(bool isSeeingPlayer){
		this.isSeeingPlayer = isSeeingPlayer;
	}
	
	public Vector3 getDestination(){
		return destination;
	}
}

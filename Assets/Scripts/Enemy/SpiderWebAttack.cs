using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HashAnimatorSpiderEnemy))]
public class SpiderWebAttack : MonoBehaviour {

	public bool useOnlyOneWebAttack = true;
	private bool usedFirstWeb = false;
	public float webRange = 10f;
	public float attackDelay = 1f;

	public GameObject webPrefab;
	public Transform webPosition;
	public float webYPosition = 1f;
	public float webForce = 10f;
	public float angleFromPlayerToAttack = 10f;
	private GameObject lastWeb;
	private Animator enemyAnimator;
	private HashAnimatorStalkerEnemy hashAnimator;
	private HashAnimatorSpiderEnemy hashAnimatorSpider;
	
	private bool attackingDelayCount = false;
	private float attackingDelayPassed = 0f;
	NavMeshAgent agent;
	private PatrolAndStalkMovement patrolAndStalkMovementScript;
	
	void Awake(){
		agent = gameObject.GetComponent<NavMeshAgent>();
		patrolAndStalkMovementScript = GetComponent<PatrolAndStalkMovement>();
		
		enemyAnimator = GetComponent<Animator>();
		hashAnimator = GetComponent<HashAnimatorStalkerEnemy>();
		hashAnimatorSpider = GetComponent<HashAnimatorSpiderEnemy>();
	}
	
	void Update () {
		Vector3 destination = patrolAndStalkMovementScript.getDestination();
		if( !(Vector3.Distance(transform.position, destination) > webRange) && patrolAndStalkMovementScript.getStalking() ){
			enemyAnimator.SetFloat(hashAnimator.velocity, agent.desiredVelocity.magnitude);
			if( patrolAndStalkMovementScript.getIsSeeingPlayer() ){
				Vector3 destinationToLook = new Vector3(destination.x, transform.position.y, destination.z);
				if(!patrolAndStalkMovementScript.getAttacking() && !attackingDelayCount && Quaternion.Angle(transform.rotation, Quaternion.LookRotation(destinationToLook - transform.position)) <= angleFromPlayerToAttack  ){
					if(!useOnlyOneWebAttack || !usedFirstWeb){
						AttackStart();
					}
				}
				patrolAndStalkMovementScript.FlipWithSpeed(destinationToLook, patrolAndStalkMovementScript.lookAtPlayerSpeed);
			}
		}

		if(!useOnlyOneWebAttack && attackingDelayCount){
			AttackDelayCounter();
		}
	}
	
//########Attacking START
//###########################################
	void AttackStart(){
		patrolAndStalkMovementScript.setAttacking(true);
		usedFirstWeb = true;
		
		enemyAnimator.SetBool(hashAnimatorSpider.webLaunching, true);
		
		agent.SetDestination(transform.position);
	}
	
	//function called with an event in the attack animation, when the hitbox should start to cause damage
	public void AttackWebLaunch(){
		lastWeb = Instantiate(webPrefab);
		lastWeb.transform.position = new Vector3 (webPosition.transform.position.x, webYPosition, webPosition.transform.position.z);
		lastWeb.transform.rotation = transform.rotation;
		lastWeb.GetComponent<Rigidbody>().AddForce(transform.forward*webForce);
	}
	
	//function called with an event in the end of the attack animation
	public void AttackFinish(){
		attackingDelayCount = true;

		if(useOnlyOneWebAttack){
			patrolAndStalkMovementScript.setAttacking(false);
		}
	}
	
	void AttackDelayFinish(){
		patrolAndStalkMovementScript.setAttacking(false);
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

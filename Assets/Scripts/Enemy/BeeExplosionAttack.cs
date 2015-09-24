using UnityEngine;
using System.Collections;

public class BeeExplosionAttack : MonoBehaviour {
	
	public float attackDelay = 1f;
	private Animator enemyAnimator;
	private HashAnimatorStalkerEnemy hashAnimator;
	
	private bool attackingDelayCount = false;
	private float attackingDelayPassed = 0f;
	NavMeshAgent agent;
	private PatrolAndStalkMovement patrolAndStalkMovementScript;

	private EnemyLife enemyLife;
	
	void Awake(){
		agent = gameObject.GetComponent<NavMeshAgent>();
		patrolAndStalkMovementScript = GetComponent<PatrolAndStalkMovement>();
		
		enemyAnimator = GetComponent<Animator>();
		hashAnimator = GetComponent<HashAnimatorStalkerEnemy>();

		enemyLife = GetComponent<EnemyLife>();
	}
	
	void Update () {
		Vector3 destination = patrolAndStalkMovementScript.getDestination();
		if( !(Vector3.Distance(transform.position, destination) > agent.stoppingDistance) && patrolAndStalkMovementScript.getStalking() ){ 
			enemyAnimator.SetFloat(hashAnimator.velocity, agent.desiredVelocity.magnitude);
			if( patrolAndStalkMovementScript.getIsSeeingPlayer() ){
				if(!patrolAndStalkMovementScript.getAttacking() && !attackingDelayCount){
					AttackStart();
				}
				patrolAndStalkMovementScript.FlipWithSpeed(new Vector3(destination.x, transform.position.y, destination.z), patrolAndStalkMovementScript.lookAtPlayerSpeed);
			}
		}
		
		if(attackingDelayCount){
			AttackDelayCounter ();
		}
	}
	
	//########Attacking START
	//###########################################
	void AttackStart(){
		attackingDelayCount = true;
		patrolAndStalkMovementScript.setAttacking(true);
		
		enemyAnimator.SetBool(hashAnimator.attacking, true);
		
		agent.SetDestination(transform.position);
	}
	
	void AttackDelayFinish(){
		enemyLife.setLife(0);
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

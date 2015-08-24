using UnityEngine;
using System.Collections;

public class BasicAttackEnemy : MonoBehaviour {

	public float attackDelay = 1f;
	public GameObject[] attackHitBoxObjects;
	private EnemyAttackCollider[] enemyAttackCollidersScripts;
	private Animator enemyAnimator;
	private HashAnimatorStalkerEnemy hashAnimator;

	private bool attackingDelayCount = false;
	private float attackingDelayPassed = 0f;
	NavMeshAgent agent;
	private PatrolAndStalkMovement patrolAndStalkMovementScript;

	void Awake(){
		agent = gameObject.GetComponent<NavMeshAgent>();
		patrolAndStalkMovementScript = GetComponent<PatrolAndStalkMovement>();

		enemyAnimator = GetComponent<Animator>();
		hashAnimator = GetComponent<HashAnimatorStalkerEnemy>();

		enemyAttackCollidersScripts = new EnemyAttackCollider[attackHitBoxObjects.Length];
		for (int i=0; i<attackHitBoxObjects.Length; i++){
			enemyAttackCollidersScripts[i] = attackHitBoxObjects[i].GetComponent<EnemyAttackCollider>();
		}
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
		patrolAndStalkMovementScript.setAttacking(true);

		enemyAnimator.SetBool(hashAnimator.attacking, true);

		agent.SetDestination(transform.position);
	}

	//function called with an event in the attack animation, when the hitbox should start to cause damage
	public void AttackHitBoxStart(){
		foreach (GameObject attackHitBoxObject in attackHitBoxObjects){
			attackHitBoxObject.SetActive(true);
		}
	}
	
	//function called with an event in the end of the attack animation
	public void AttackFinish(){
		attackingDelayCount = true;
		for(int i=0; i< attackHitBoxObjects.Length; i++){
			attackHitBoxObjects[i].SetActive(false);
			enemyAttackCollidersScripts[i].ResetDamageReduction();
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

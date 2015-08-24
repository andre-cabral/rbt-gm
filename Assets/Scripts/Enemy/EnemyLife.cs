using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startingLife = 2;
	int life = 999;
	bool isDead = false;

	private FlickerMeshWhenDamaged flickerWhenDamage;
	private PatrolAndStalkMovement stalkerNavMeshScript;
	
	void Awake () {
		life = startingLife;
		flickerWhenDamage = GetComponent<FlickerMeshWhenDamaged>();
		stalkerNavMeshScript = GetComponent<PatrolAndStalkMovement>();
	}

	void Update(){
		CheckDeath();
	}
	
	void OnTriggerEnter(Collider collider){
		GameObject collidedObject = collider.gameObject;
		if( collidedObject.tag == Tags.playerAttackCollider && !flickerWhenDamage.getFlicker() ){
			int damage = collidedObject.GetComponent<PlayerAttackCollider>().damage;
			if(damage > 0){
				life -= damage;
				flickerWhenDamage.startFlickering();
				stalkerNavMeshScript.SeePlayer();
			}
		}
	}

	void CheckDeath(){
		if(life <= 0){
			isDead = true;
			Destroy(gameObject);
		}
	}

	public bool getIsDead(){
		return isDead;
	}
}

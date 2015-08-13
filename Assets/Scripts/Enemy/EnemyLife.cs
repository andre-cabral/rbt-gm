using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startingLife = 2;
	int life = 999;
	bool isDead = false;

	private FlickerMeshWhenDamaged flickerWhenDamage;
	
	void Awake () {
		life = startingLife;
		flickerWhenDamage = GetComponent<FlickerMeshWhenDamaged>();
	}

	void Update(){
		CheckDeath();
	}
	
	void OnTriggerEnter(Collider collider){
		GameObject collidedObject = collider.gameObject;
		if( collidedObject.tag == Tags.playerAttackCollider && !flickerWhenDamage.getFlicker() ){
			int damage = collidedObject.GetComponent<PlayerAttackCollider>().damage;
			life -= damage;
			flickerWhenDamage.startFlickering();
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

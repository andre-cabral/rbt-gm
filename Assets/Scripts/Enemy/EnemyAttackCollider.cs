using UnityEngine;
using System.Collections;

public class EnemyAttackCollider : MonoBehaviour {

	public int damage = 1;
	public bool isBlockable = true;
	private bool isBlocked = false;
	private int damageReduction = 0;

	void OnTriggerEnter(Collider collider) {
		GameObject collidedObject = collider.gameObject;
		
		if(collidedObject.tag == Tags.blockingCollider){
			isBlocked = true;
			damageReduction = collidedObject.GetComponent<BlockingCollider>().getDamageReduction();
		}
		
	}

	void OnTriggerExit(Collider collider) {
		GameObject collidedObject = collider.gameObject;

		if(collidedObject.tag == Tags.blockingCollider){
			isBlocked = false;
			damageReduction = 0;
		}
		
	}

	public int DamageDealt(){
		if(!isBlocked || !isBlockable){
			return damage;
		}else{
			if(damage < damageReduction ){
				return damage - damageReduction;
			}else{
				return 0;
			}
		}
	}

}

using UnityEngine;
using System.Collections;

public class PlayerAttackCollider : MonoBehaviour {

	public int damage = 1;
	int damageAlterations = 0;
	public AudioNames hitEnemySound;
	public bool useHitEnemySound = false;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.enemy){
			if(useHitEnemySound){
				AudioManager.PlaySound(hitEnemySound.ToString(), transform.position);
			}
		}
	}

	public void AddDamageAlteration(int numberToAdd){
		damageAlterations += numberToAdd;
	}

	public void SubtractDamageAlteration(int numberToAdd){
		damageAlterations -= numberToAdd;
	}

	public void ResetDamageAlteration(){
		damageAlterations = 0;
	}

	public int DamageToDeal(){
		return damage + damageAlterations;
	}
}

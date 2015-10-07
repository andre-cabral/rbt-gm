using UnityEngine;
using System.Collections;

public class PlayerAttackCollider : MonoBehaviour {

	public int damage = 1;
	public AudioNames hitEnemySound;
	public bool useHitEnemySound = false;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.enemy){
			if(useHitEnemySound){
				AudioManager.PlaySound(hitEnemySound.ToString(), transform.position);
			}
		}
	}
}

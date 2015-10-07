using UnityEngine;
using System.Collections;

public class PlayerAttackCollider : MonoBehaviour {

	public int damage = 1;
	public AudioSource hitEnemySound;

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.enemy){
			if(hitEnemySound != null){
				hitEnemySound.Play();
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class WebSlowDown : SlowDownPlayer {
	Animator animator;
	HashAnimatorWeb hashAnimatorWeb;

	public override void Awake (){
		base.Awake ();

		animator = GetComponent<Animator>();
		hashAnimatorWeb = GetComponent<HashAnimatorWeb>();
	}

	public override void OnTriggerEnter(Collider collider){
		if(collider.tag == Tags.characterClass && !getCollidedWithPlayer()){
			animator.SetBool(hashAnimatorWeb.hitPlayer, true);
		}

		base.OnTriggerEnter(collider);
	}
}

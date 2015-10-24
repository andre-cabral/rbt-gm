using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour {

	public int startingLife = 2;
	public GameObject[] dropWhenDead;
	public EnemyLifebar enemyLifebar;
	public GameObject hitIconPrefab;
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
			int damage = collidedObject.GetComponent<PlayerAttackCollider>().DamageToDeal();
			if(damage > 0){
				life -= damage;
				flickerWhenDamage.startFlickering();

				float lifeFloat = life;
				float startingLifeFloat = startingLife;
				enemyLifebar.changePercentage(lifeFloat/startingLifeFloat);

				Instantiate(hitIconPrefab, collider.transform.position, hitIconPrefab.transform.rotation);

				stalkerNavMeshScript.SeePlayer();
			}
		}
	}

	public void setLife(int life){
		this.life = life;
	}

	void CheckDeath(){
		if(life <= 0){
			isDead = true;
			if(dropWhenDead.Length >0){
				for(int i=0; i<dropWhenDead.Length; i++){
					GameObject objectInstatiated = (GameObject)Instantiate(dropWhenDead[i], transform.position, dropWhenDead[i].transform.rotation);
				}
			}
			Destroy(gameObject);
		}
	}

	public bool getIsDead(){
		return isDead;
	}
}

using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {
	public int maxLife = 3;
	public int initialLife = 3;
	public int lowLifeAlert = 1;
	HealthbarObjects healthBarObjects;
	GameObject[] lowLifeObjects;
	int lifePoints;

	void Awake () {
		//if there will be persistence on life, get the life here
		healthBarObjects = GameObject.FindGameObjectWithTag(Tags.healthBar).GetComponent<HealthbarObjects>();
		lowLifeObjects = GameObject.FindGameObjectsWithTag(Tags.lowLifeObject);
		SetLowLifeObjects(false);
		lifePoints = initialLife;
	}

	public void LifeGain(int gainedLife){
		if(gainedLife + lifePoints > maxLife){
			lifePoints = maxLife;
			changedLife();
		}else{
			lifePoints += gainedLife;
			changedLife();
		}
	}

	public void LifeLoss (int lifeToLose){
		lifePoints -= lifeToLose;
		changedLife();
	}

	void changedLife(){
		for(int i=0; i<healthBarObjects.healthObjectsInOrder.Length; i++){
			if( i < maxLife - lifePoints ){
				healthBarObjects.healthObjectsInOrder[i].SetActive(false);
			}else{
				healthBarObjects.healthObjectsInOrder[i].SetActive(true);
			}
		}

		if(lifePoints <= lowLifeAlert){
			SetLowLifeObjects(true);
		}else{
			SetLowLifeObjects(false);
		}
	}

	public int GetLife(){
		return lifePoints;
	}

	void SetLowLifeObjects(bool isLowLife){
		foreach(GameObject lowLifeObject in lowLifeObjects){
			lowLifeObject.SetActive(isLowLife);
		}
	}
}

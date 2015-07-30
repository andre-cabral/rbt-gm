using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {
	public int maxLife = 3;
	public int initialLife = 3;
	int lifePoints;

	void Awake () {
		//if there will be persistence on life, get the life here
		lifePoints = initialLife;
	}

	public void LifeGain(int gainedLife){
		if(gainedLife + lifePoints > maxLife){
			lifePoints = maxLife;
		}else{
			lifePoints += gainedLife;
		}
	}

	public void LifeLoss (int lifeToLose){
		lifePoints -= lifeToLose;
	}

	public int GetLife(){
		return lifePoints;
	}
}

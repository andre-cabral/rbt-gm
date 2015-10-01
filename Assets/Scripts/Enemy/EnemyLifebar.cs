using UnityEngine;
using System.Collections;

public class EnemyLifebar : MonoBehaviour {

	float percentage = 1f;
	Quaternion startRotation;
	SpriteRenderer spriteRenderer;
	public float distanceFromUnit = 1f;
	Transform parentTransform;
	float percentageXPosition = 0f;

	void Awake () {
		parentTransform = gameObject.transform.parent;
		startRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Quaternion.identity.y, Quaternion.identity.z);
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	void Update () {
		transform.rotation = startRotation;
		transform.position = new Vector3(parentTransform.position.x - percentageXPosition, transform.position.y, parentTransform.position.z + distanceFromUnit);
	}

	public void changePercentage(float newPercentage){
		percentage = newPercentage;
		percentageXPosition = (1f - percentage)/2;
		transform.localScale = new Vector3(percentage, transform.localScale.y, transform.localScale.z);
		if(percentage <= 0.75f && percentage > 0.5f){
			spriteRenderer.color = Color.yellow;
		}
		if(percentage <= 0.5f){
			spriteRenderer.color = Color.red;
		}
	}
}

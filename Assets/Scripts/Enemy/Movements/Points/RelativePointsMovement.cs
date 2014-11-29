using UnityEngine;
using System.Collections;


public class RelativePointsMovement : MonoBehaviour {

	public Vector3[] points;
	public bool moveYAxis = false;
	public float velocity = 0.15f;
	private Vector3 startPosition;
	private int vectorIndex = 0;
	//EnemyCollisions enemyCollisions;

	// Use this for initialization
	void Awake () {
		startPosition = transform.localPosition;
		//enemyCollisions = gameObject.GetComponent<EnemyCollisions>();

		for(int i=0; i<points.Length; i++ ){
			if(i == 0){
				if(!moveYAxis){
					points[i] = new Vector3(points[i].x + startPosition.x, transform.position.y, points[i].z + startPosition.z);
				}else{
					points[i] = new Vector3(points[i].x + startPosition.x, points[i].y + startPosition.y, points[i].z + startPosition.z);
				}
			}else{
				if(!moveYAxis){
					points[i] = new Vector3(points[i].x + points[i-1].x, transform.position.y, points[i].z + points[i-1].z);
				}else{
					points[i] = new Vector3(points[i].x + points[i-1].x, points[i].y + points[i-1].y, points[i].z + points[i-1].z);
				}
			}
		}
	}
	

	void FixedUpdate () {
		//if(!enemyCollisions.getTakingDamage()){
			if(vectorIndex < points.Length){
				Movement(points[vectorIndex], velocity);
			}else{
				Movement(startPosition, velocity);
			}
		//}
	}
	

	void Movement(Vector3 end, float velocity){

		Flip(end);

		transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, velocity);
		if(transform.localPosition.x == end.x && transform.localPosition.z == end.z){
			if(vectorIndex < points.Length){
				vectorIndex++;
			}else{
				vectorIndex = 0;
			}
		}
	}

	void Flip(Vector3 end){
		transform.LookAt(end);
	}
}

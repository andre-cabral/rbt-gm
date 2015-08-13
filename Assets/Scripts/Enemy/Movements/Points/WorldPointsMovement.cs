using UnityEngine;
using System.Collections;

public class WorldPointsMovement : MonoBehaviour {

	public GameObject[] waypoints;
	private Vector3[] points;
	public bool moveYAxis = false;
	public float velocity = 0.15f;
	private int vectorIndex = 0;
	//EnemyCollisions enemyCollisions;
	
	// Use this for initialization
	void Awake () {
		//startPosition = transform.localPosition;
		//enemyCollisions = gameObject.GetComponent<EnemyCollisions>();

		points = new Vector3[waypoints.Length];

		for(int i=0; i<waypoints.Length; i++ ){
			if(!moveYAxis){
				points[i] = new Vector3(waypoints[i].transform.position.x, transform.position.y, waypoints[i].transform.position.z);
			}else{
				points[i] = new Vector3(waypoints[i].transform.position.x, waypoints[i].transform.position.y, waypoints[i].transform.position.z);
			}
		}
	}
	
	
	void FixedUpdate () {
		//if(!enemyCollisions.getTakingDamage()){
			Movement(points[vectorIndex], velocity);
		
		//}
	}
	
	
	void Movement(Vector3 end, float velocity){
		
		Flip(end);
		
		transform.position = Vector3.MoveTowards(transform.position, end, velocity);
		if(transform.position.x == end.x && transform.position.z == end.z){
			if(vectorIndex < points.Length-1){
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

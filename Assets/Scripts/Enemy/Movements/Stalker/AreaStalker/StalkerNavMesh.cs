using UnityEngine;
using System.Collections;

public class StalkerNavMesh : MonoBehaviour {

	private GameObject stalkedPlayerObject;
	private Transform stalkedPlayerObjectTransform;
	NavMeshAgent agent;
	private GameObject classContainer;
	private ChangeClass changeClassScript;

	void Awake () {
		agent = gameObject.GetComponent<NavMeshAgent>();

		classContainer = GameObject.FindGameObjectWithTag(Tags.characterClassesContainer);
		changeClassScript = classContainer.GetComponent<ChangeClass>();
	}

	void Start(){
		stalkedPlayerObject = changeClassScript.GetActiveClass();
		stalkedPlayerObjectTransform = stalkedPlayerObject.transform;
	}
	
	void Update () {
		if( !stalkedPlayerObject.Equals(changeClassScript.GetActiveClass()) ){
			stalkedPlayerObject = changeClassScript.GetActiveClass();
			stalkedPlayerObjectTransform = stalkedPlayerObject.transform;
		}

		agent.SetDestination(new Vector3(stalkedPlayerObjectTransform.position.x,transform.localPosition.y,stalkedPlayerObjectTransform.position.z));
		//Flip(new Vector3(agent.destination.x, transform.position.y, agent.destination.z));
	}

	void Flip(Vector3 end){
		transform.LookAt(end);
	}
}

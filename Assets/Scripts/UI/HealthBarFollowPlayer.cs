using UnityEngine;
using System.Collections;

public class HealthBarFollowPlayer : MonoBehaviour {

	public ChangeClass characterClassesContainerChangeClass;

	void Update () {
		transform.position = characterClassesContainerChangeClass.GetActiveClass().transform.position;
	}
}

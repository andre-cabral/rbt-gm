using UnityEngine;
using System.Collections;

public class goToScene : MonoBehaviour {
	public string sceneName;

	public void selectScene (){
		Application.LoadLevel(sceneName);
	}
}

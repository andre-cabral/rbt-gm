using UnityEngine;
using System.Collections;

public class PauseAndUnpause : MonoBehaviour {

	void Awake () {
	
	}
	
	void Update () {
	
	}

	public void Pause(){
		DefaultMovement.setPause(true);
	}

	public void Unpause(){
		DefaultMovement.setPause(false);
	}

	public void TogglePause(){
		DefaultMovement.setPause(!DefaultMovement.isPaused);
	}
}

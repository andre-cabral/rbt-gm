using UnityEngine;
using System.Collections;

public class DestroyObjectAfterSoundEnd : MonoBehaviour {

	public AudioSource sound;

	void Update(){
		if(!sound.isPlaying){
			Destroy(gameObject);
		}
	}
}

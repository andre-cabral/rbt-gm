using UnityEngine;
using System.Collections;

public class MusicPlay : MonoBehaviour {

	public MusicNames musicToPlay;
	public bool playOnStart = true;

	void Start () {
		if(playOnStart){
			PlayTheMusic();
		}
	}
	
	public void PlayTheMusic () {
		AudioManager.PlaySound(musicToPlay.ToString(), transform.position);
	}
}

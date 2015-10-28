using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuSetEffectsVolume : MonoBehaviour {

	AudioManager audioManager;
	Slider slider;

	// Use this for initialization
	void Awake () {
		audioManager = GameObject.FindGameObjectWithTag(Tags.audioController).GetComponent<AudioManager>();
		slider = GetComponent<Slider>();
	}
	
	public void SetEffectsVolume(){
		if(slider != null && audioManager != null){
			audioManager.SetEffectsVolume(slider.value/slider.maxValue);
		}
	}
}

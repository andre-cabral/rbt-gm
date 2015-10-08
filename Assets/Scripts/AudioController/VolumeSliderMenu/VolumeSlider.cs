using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeSlider : MonoBehaviour {

	Slider slider;
	public enum EffectsOrMusic{effects, music};
	public EffectsOrMusic effectsOrMusic;

	void Start () {
		slider = GetComponent<Slider>();

		if(effectsOrMusic == EffectsOrMusic.effects){
			slider.value = AudioManager.getEffectsVolume() * 100;
		}

		if(effectsOrMusic == EffectsOrMusic.music){
			slider.value = AudioManager.getMusicVolume() * 100;
		}
	}



}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	static AudioManager audioManager;
	public static Dictionary<string, AudioSource> audioSounds;
	Dictionary<string, float> backgroundMusicStartingVolume;
	float musicVolume = 1f;
	float effectsVolume = 1f;

	void Awake () {
		if(audioManager == null){
			DontDestroyOnLoad(this);
			audioManager = this;
		}else{
			Destroy(gameObject);
		}
		if (audioSounds == null)
		{
			audioSounds = new Dictionary<string, AudioSource>();
			backgroundMusicStartingVolume = new Dictionary<string, float>();

			AudioSource[] sounds = gameObject.GetComponentsInChildren<AudioSource>();
			for (int i = 0; i < sounds.Length; i++)
			{
				if(sounds[i].tag == Tags.backgroundMusicObject){
					sounds[i].ignoreListenerVolume = true;
					backgroundMusicStartingVolume[sounds[i].name] = sounds[i].volume;
				}
				audioSounds[sounds[i].gameObject.name] = sounds[i];
				//DontDestroyOnLoad(sounds[i]);
			}
		}
	}

	public void SetEffectsVolume(float volume){
		volume = Mathf.Clamp(volume, 0.0f, 1.0f);
		AudioListener.volume = volume;
		//effectsVolume = volume;
	}

	public void SetMusicVolume(float volume){
		volume = Mathf.Clamp(volume, 0.0f, 1.0f);
		foreach(AudioSource soundToCheck in audioSounds.Values){
			if(soundToCheck.tag == Tags.backgroundMusicObject){
				soundToCheck.volume = backgroundMusicStartingVolume[soundToCheck.name] * volume;
			}
		}
		//musicVolume = volume;
	}

	public static void PlaySound(string soundName, Vector3 positionToPlay){
		audioSounds[soundName].transform.position = positionToPlay;
		audioSounds[soundName].PlayOneShot(audioSounds[soundName].clip);
	}

	public static GameObject CopySound(string soundName, Transform parentTransform){
		GameObject soundCopiedObject = Instantiate(audioSounds[soundName].gameObject);
		soundCopiedObject.transform.parent = parentTransform;
		soundCopiedObject.transform.localPosition = Vector3.zero;

		return soundCopiedObject;
	}
}

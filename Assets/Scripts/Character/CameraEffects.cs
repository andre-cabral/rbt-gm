using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class CameraEffects : MonoBehaviour {

	float vignetteStartValue;
	public float vignetteTargetValue = -5f;
	public float vignetteChangeTime = 0.1f;
	float vignetteChangeTimePassed = 0f;
	bool vignetteChanging = false;

	float chromaticAberrationStartValue;
	public float chromaticAberrationTargetValue = 20f;
	public float chromaticAberrationChangeTime = 0.1f;
	float chromaticAberrationChangeTimePassed = 0f;
	bool chromaticAberrationChanging = false;


	VignetteAndChromaticAberration vignetteAndChromaticAberration;



	// Use this for initialization
	void Awake () {
		vignetteAndChromaticAberration = GetComponent<VignetteAndChromaticAberration>();
		vignetteStartValue = vignetteAndChromaticAberration.intensity;
		chromaticAberrationStartValue = vignetteAndChromaticAberration.chromaticAberration;
	}

	void Update(){
		if(vignetteChanging){
			CheckVignetteChange();
		}
		if(chromaticAberrationChanging){
			CheckChromaticAberrationChange();
		}
	}

	public void StartVignetteChange(){
		vignetteChangeTimePassed = 0f;
		vignetteChanging = true;
		vignetteAndChromaticAberration.intensity = vignetteTargetValue;
	}

	void CheckVignetteChange(){
		if(vignetteChangeTimePassed < vignetteChangeTime){
			vignetteChangeTimePassed += Time.deltaTime;
		}else{
			vignetteChanging = false;
			vignetteAndChromaticAberration.intensity = vignetteStartValue;
		}
	}

	public void StartChromaticAberrationChange(){
		chromaticAberrationChangeTimePassed = 0f;
		chromaticAberrationChanging = true;
		vignetteAndChromaticAberration.chromaticAberration = chromaticAberrationTargetValue;
	}
	
	void CheckChromaticAberrationChange(){
		if(chromaticAberrationChangeTimePassed < chromaticAberrationChangeTime){
			chromaticAberrationChangeTimePassed += Time.deltaTime;
		}else{
			chromaticAberrationChanging = false;
			vignetteAndChromaticAberration.chromaticAberration = chromaticAberrationStartValue;
		}
	}

}

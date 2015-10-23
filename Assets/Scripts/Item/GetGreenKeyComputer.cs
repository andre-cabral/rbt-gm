using UnityEngine;
using System.Collections;

public class GetGreenKeyComputer : MonoBehaviour {

	public string message;
	public AudioNames gotKeySound;
	public Animator computerAnimator;
	public HashAnimatorComputer hashAnimatorComputer;
	private ScreenMessageManager screenMessageManager;
	private InventoryManager inventory;
	CameraEffects cameraEffects;

	void Awake(){
		GameObject gameController = GameObject.FindGameObjectWithTag(Tags.gameController);
		inventory = gameController.GetComponent<InventoryManager>();
		screenMessageManager = gameController.GetComponent<ScreenMessageManager>();

		cameraEffects = Camera.main.GetComponent<CameraEffects>();
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == Tags.characterClass){
			col.gameObject.GetComponent<DefaultMovement>().StartUse(transform.position);
			computerAnimator.SetBool(hashAnimatorComputer.open, true);

			inventory.setHasGreenKey(true);
			screenMessageManager.NewMessage(message);
			AudioManager.PlaySound(gotKeySound.ToString(), transform.position);

			cameraEffects.StartVignetteChange();

			Destroy(gameObject);
		}
	}

}

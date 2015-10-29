using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenMessageManager : MonoBehaviour {

	Text textObject;
	string defaultMessage = "";
	public AudioNames newMessageSound;

	void Awake(){
		textObject = GameObject.FindGameObjectWithTag(Tags.screenMessageText).GetComponent<Text>();
		if(textObject!= null){
			defaultMessage = textObject.text;
		}
	}

	public void NewMessage(string newMessage){
		if(textObject != null){
			textObject.text = newMessage.Replace("{newline}", "\n");
			AudioManager.PlaySound(newMessageSound.ToString(), transform.position);
		}
	}

	public void DefaultMessage(){
		if(textObject != null){
			textObject.text = defaultMessage.Replace("{newline}", "\n");
		}
	}
}

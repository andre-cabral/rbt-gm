using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenMessageManager : MonoBehaviour {

	Text textObject;

	void Awake(){
		textObject = GameObject.FindGameObjectWithTag(Tags.screenMessageText).GetComponent<Text>();
	}

	public void NewMessage(string newMessage){
		if(textObject != null){
			textObject.text = newMessage.Replace("{newline}", "\n");
		}
	}
}

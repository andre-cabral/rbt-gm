using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenMessageManager : MonoBehaviour {

	public Text textObject;

	public void NewMessage(string newMessage){
		textObject.text = newMessage.Replace("{newline}", "\n");
	}
}

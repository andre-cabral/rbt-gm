using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

	private bool hasGreenKey = false;

	public bool getHasGreenKey(){
		return hasGreenKey;
	}
	public void setHasGreenKey(bool hasGreenKey){
		this.hasGreenKey = hasGreenKey;
	}
}

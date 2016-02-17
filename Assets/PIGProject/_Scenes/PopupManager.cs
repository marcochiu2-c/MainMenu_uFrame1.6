using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour {

	public Popup CurrentPopup;

	public void Start(){
		ShowPopup (CurrentPopup);
	}

	public void ShowPopup(Popup popup){
		if (CurrentPopup != null)
			CurrentPopup.IsOpen = false;
//		CurrentPopup = Popup;
		CurrentPopup.IsOpen = true;
	}
}

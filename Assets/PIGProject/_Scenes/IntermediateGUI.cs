using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class IntermediateGUI : MonoBehaviour {

	public string username = "Enter username";
	public string password = "Enter password";
	private bool passwordInError = false;
	private string passwordErrorMessage = "<color=red>Password too short</color>";
	//private Rect rctWindow1;

	void CheckUserPasswordAndRegister()
	{
		if (password.Length < 6) {
			//If the password is not long enough, mark it in error and focus on the pasword field
			passwordInError = true;
			GUI.FocusControl ("PasswordField");
		} else {
			passwordInError = false;
			GUI.FocusControl ("");
			//Register User
		}
	}

	void DoMyWindow(int windowID)
	{
		GUI.Label (new Rect (25, 15, 100, 30), "Label");
		//Make the window draggable
		GUI.DragWindow();
	}
	
	void OnGUI()
	{
		//A tidy group for our fields and a box to decorate it
		GUI.BeginGroup (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 80, 150, 160));
		GUI.Box (new Rect (0, 0, 150, 160), "User registration form");
		GUI.SetNextControlName ("UsernameField");
		username = GUI.TextField (new Rect (10, 40, 130, 20), username);
		GUI.SetNextControlName ("PasswordField");
		password = GUI.PasswordField (new Rect (10, 70, 130, 20), password, '*');
		if (passwordInError) {
			GUI.Label (new Rect (10, 100, 200, 20), passwordErrorMessage);
		}
		if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl () == "PasswordFirld") {
			CheckUserPasswordAndRegister ();
		}
		if (GUI.Button (new Rect (80, 130, 65, 20), new GUIContent ("Register", "My Tooltip"))) {
			CheckUserPasswordAndRegister ();
		}
		GUI.Label (new Rect (10, 120, 65, 20), GUI.tooltip);
		GUI.EndGroup ();
		//Track the windows location
		//Rect rctWindow1;
		//rctWindow1 = GUI.ModalWindow (0, rctWindow1, DoMyWindow, "Modal Controls Window");
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

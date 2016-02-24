using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class GUIStyles : MonoBehaviour {

	public GUIStyle myGUIStyle;

	void OnGUI()
	{
		//Create a label using the GUIStyle property above
		GUI.Label (new Rect (25, 15, 100, 30), "Label", myGUIStyle);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

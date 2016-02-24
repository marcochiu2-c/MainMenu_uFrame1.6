using UnityEngine;
using System.Collections;

[ExecuteInEditMode]

public class GUISkins : MonoBehaviour {

	public GUISkin MySkin;

	void OnGUI()
	{
		GUI.skin = MySkin;
		GUI.Label (new Rect (25, 15, 100, 30), "Label");
		//Draw the rest of your controls
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

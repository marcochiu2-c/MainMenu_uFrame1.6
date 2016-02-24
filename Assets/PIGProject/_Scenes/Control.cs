using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {

	public Texture2D myTexture;
	public string textString1 = "Some text here";
	public string textString2 = "Some more text here";
	public string textString3 = "Even more text here";
	bool blnToggleState = false;
	private int toolbarInt;
	private string[] toolbarStrings = new string[] {"Toolbar1" , "Toolbar2" , "Toolbar3"};
	private int selectionGridInt;
	private string[] selectionStrings = new string[] {"Grid 1", "Grid2", "Grid 3", "Grid 4"};
	public float fltSliderValue = 5.0f;
	private Vector2 scrollPosition = Vector2.zero;
	public bool ShowVertical = false;
	public bool ShowHorizontal = false;
	string login = "Would you like to play a game?";

	// Use this for initialization
	void Start () {
		myTexture = new Texture2D (125, 15);
		toolbarInt = 0;


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		/*GUI.Label (new Rect (125, 15, 100, 30), myTexture);
		GUI.Label (new Rect (125, 15, 100, 30), "Text overlay");
		GUI.Label (new Rect (325, 15, 100, 30), myTexture);
		GUI.DrawTexture (new Rect (325, 35, 100, 100), myTexture,
		                 ScaleMode.ScaleToFit,true,0.5f);
		GUI.DrawTextureWithTexCoords (new Rect (125, 35, 100, 15), myTexture, new Rect (10, 10, 50, 5));
		if (GUI.Button (new Rect (25, 40, 120, 30), myTexture)) {
		}
		if (GUI.RepeatButton (new Rect (170, 40, 120, 30), "RepeatButton")) {
		}
		GUI.TextField (new Rect (25, 100, 100, 30), textString1);
		GUI.TextArea (new Rect (150, 100, 200, 75), textString2);
		GUI.PasswordField (new Rect (375, 100, 90, 30), textString3, '*');
		GUI.Box (new Rect (350, 350, 100, 130), "Settings");
		GUI.Label (new Rect (360, 370, 80, 30), "Label");
		textString1 = GUI.TextField (new Rect (360, 400, 80, 30), textString1);
		if (GUI.Button (new Rect (360, 440, 80, 30), "Button")) {
		}
		blnToggleState = GUI.Toggle (new Rect (25, 150, 250, 30), blnToggleState, "Toggle");
		toolbarInt = GUI.Toolbar (new Rect (25, 200, 200, 30), toolbarInt, toolbarStrings);
		selectionGridInt = GUI.SelectionGrid (new Rect (250, 200, 200, 60), selectionGridInt, selectionStrings, 2);
		fltSliderValue = GUI.HorizontalSlider (new Rect (25, 250, 100, 30), fltSliderValue, 0.0f, 10.0f);
		fltSliderValue = GUI.VerticalSlider (new Rect (150, 250, 25, 50), fltSliderValue, 10.0f, 0.0f);
		//scrollPosition = GUI.BeginScrollView (new Rect (25, 325, 300, 200), scrollPosition, new Rect (0, 0, 400, 400), ShowVertical, ShowHorizontal);
		//for (int i=0; i<20; i++) {
		//	addScrollViewListItem (i, "I'm listItem number " + i);
		//}
		//GUI.EndScrollView ();
		scrollPosition = GUI.BeginScrollView (new Rect (10, 10, 100, 50), scrollPosition, new Rect (0, 0, 220, 10), ShowVertical, ShowHorizontal);
		if (GUI.Button (new Rect (120, 0, 100, 20), "Go to Top Left"))
			GUI.ScrollTo (new Rect (0, 0, 100, 20));
		GUI.EndScrollView ();*/
		GUI.BeginGroup (new Rect (50, 50, 150, 160));
		GUI.Label (new Rect (10, 10, 100, 30), "Label in a Group");
		GUI.EndGroup ();
		GUI.SetNextControlName ("MyAwesomeField");
		login = GUI.TextField (new Rect (10, 10, 200, 20), login);
	}

	void addScrollViewListItem(int i, string strItemName){
		GUI.Label (new Rect (25, 25 + (i * 25), 150, 25), strItemName);
		blnToggleState = GUI.Toggle (new Rect (175, 25 + (i * 25), 100, 25), blnToggleState, "");
	}
}

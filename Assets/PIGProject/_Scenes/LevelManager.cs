using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;

	// Use this for initialization
	void Start () {
		if (autoLoadNextLevelAfter == 0) {
			Debug.Log ("Level auto load disabled");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);	
		}
	}
	public void LoadLevel(string name)
	{
		Debug.Log ("New Level Load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest()
	{
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel()
	{
		Application.LoadLevel (Application.loadedLevel + 1);
	}

	public void LoadPrevLevel()
	{
		Application.LoadLevel (Application.loadedLevel - 1);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

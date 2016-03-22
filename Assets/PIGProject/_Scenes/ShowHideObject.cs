using UnityEngine;
using System.Collections;

public class ShowHideObject : MonoBehaviour {

	public GameObject[] objects;

	void OnGUI()
	{
		foreach (GameObject go in objects) {
			bool active = GUILayout.Toggle (go.activeSelf, go.name);
			if (active != go.activeSelf)
				go.SetActive (active);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

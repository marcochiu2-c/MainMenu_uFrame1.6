using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KnowledgeOption : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnKnowledgeButtonClicked(Button btn){
		string name = btn.transform.GetChild(0).GetComponent<Text>().text;
		Debug.Log (name);
	}
}

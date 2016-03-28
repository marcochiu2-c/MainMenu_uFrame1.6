using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class StorageItem : MonoBehaviour {
	private GameObject itemImagePanel;
	private Text itemText;
	public static List<GameObject> sItem = new List<GameObject>();
	// Use this for initialization
	void Start () {
		itemImagePanel = transform.Find ("Image").gameObject;
		itemText = transform.Find ("ItemText").gameObject.GetComponent<Text>();
		Debug.Log (GetItemText());
		RectTransform imageRect = itemImagePanel.GetComponent<RectTransform> ();
		imageRect.rect.Set(0f,0f,533f, 150f);	
//		imageRect.sizeDelta.x = 142f;
//		imageRect.sizeDelta.y = 151.5f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetItemText(string text){
		itemText.text = text;
	}

	public string GetItemText(){
		return itemText.text;
	}
}

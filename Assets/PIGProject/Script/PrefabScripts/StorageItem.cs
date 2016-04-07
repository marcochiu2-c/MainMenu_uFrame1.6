using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class StorageItem : MonoBehaviour {
	private GameObject itemImagePanel;
	private Text itemText;
	public static List<StorageItem> WeaponSItem = new List<StorageItem>();
	public static List<StorageItem> ArmorSItem = new List<StorageItem>();
	public static List<StorageItem> ShieldSItem = new List<StorageItem>();
	public static List<StorageItem> MountSItem = new List<StorageItem>();

	public Sprite ItemPic { get { 
			Image img = transform.Find ("Image").gameObject.GetComponent<Image> ();
			return img.sprite;
			} 
			set { Image img = transform.Find ("Image").gameObject.GetComponent<Image> ();
			img.sprite = value;
		} 
	}

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

	public void SetImage(Sprite sp){
		Image img = transform.Find ("Image").gameObject.GetComponent<Image> ();
		img.sprite = sp;
	}

	public void SetText(string txt){
		Text itemText = transform.Find ("ItemText").gameObject.GetComponent<Text> ();
		itemText.text = txt;
	}
}

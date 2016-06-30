using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class StorageItem : MonoBehaviour {
	public GameObject itemImagePanel;
	public Text nameText;
	public Text itemText;
	public static List<StorageItem> WeaponSItem = new List<StorageItem>();
	public static List<StorageItem> ArmorSItem = new List<StorageItem>();
	public static List<StorageItem> ShieldSItem = new List<StorageItem>();
	public static List<StorageItem> MountSItem = new List<StorageItem>();

	#region Image
//	public Sprite ItemPic { get { 
//			Image img = transform.Find ("Image").gameObject.GetComponent<Image> ();
//			return img.sprite;
//			} 
//			set { Image img = transform.Find ("Image").gameObject.GetComponent<Image> ();
//			img.sprite = value;
//		} 
//	}
	#endregion

	// Use this for initialization
	void Start () {
//		itemImagePanel = transform.GetChild (0).gameObject;
//		Debug.Log (GetItemText());
//		RectTransform imageRect = itemImagePanel.GetComponent<RectTransform> ();
//		imageRect.rect.Set(0f,0f,533f, 150f);	
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
		Image img = itemImagePanel.GetComponent<Image> ();
		img.sprite = sp;
	}

	public void SetNameText(string name){
		nameText.text = name;
	}

	public void SetText(string txt){
		itemText.text = txt;
	}
}

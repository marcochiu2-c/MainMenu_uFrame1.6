using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using Utilities;
using System.Collections.Generic;

public class Store : MonoBehaviour
{
	Game game;
	WsClient wsc;
	public GameObject WeaponStorePanel;
	public GameObject ArmorStorePanel;
	public GameObject ShieldStorePanel;
	public GameObject MountStorePanel;
	//List<GameObject> StorageItem = new List<GameObject> ();
	public static int panelCounter=8;
	public Sprite test;

	float prefabWidth = 0f;
	float prefabHeight = 0f;
	public static void GetStorageInfoFromDB ()
	{
		var game = Game.Instance;
		var wsc = WsClient.Instance;
		JSONClass json = new JSONClass ();
		json.Add ("data", new JSONData (game.login.id));
		json ["table"] = "storage";
		json ["action"] = "GET";
		wsc.Send (json.ToString ());
	}

	// Use this for initialization
	void Start ()
	{
		CallStore ();
	}

	void CallStore ()
	{
		game = Game.Instance;
		wsc = WsClient.Instance;


		SetPanel ("Weapon", 10);
		SetPanel ("Armor", 8);
		SetPanel ("Shield", 5);
		SetPanel ("Mount", 2);

		StorageItem.WeaponSItem [0].ItemPic = test;
		StorageItem.WeaponSItem [0].SetText ("武器\n數量：30");
		//TODO set storage UI
	}

	public void SetPanel(string panel,int quantity){
		List<StorageItem> si=null;
		GameObject sp=null;
		if (panel == "Weapon") {
			si = StorageItem.WeaponSItem;
			sp = WeaponStorePanel;
		}else if (panel == "Armor") {
			si = StorageItem.ArmorSItem;
			sp = ArmorStorePanel;
		}else if (panel == "Shield") {
			si = StorageItem.ShieldSItem;
			sp = ShieldStorePanel;
		}else if (panel == "Mount") {
			si = StorageItem.MountSItem;
			sp = MountStorePanel;
		}

		for (var i = 0; i < quantity; i++) { 
			si.Add (Instantiate(Resources.Load("StorageItemPrefab") as GameObject).GetComponent<StorageItem>());
			si[i].transform.parent = sp.transform;
			RectTransform rTransform = si[i].GetComponent<RectTransform>();
			rTransform.localScale=Vector3.one;

		}

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void TestAddPanel(){
		StorageItem tmp = Instantiate (Resources.Load ("StorageItemPrefab") as GameObject).GetComponent<StorageItem>();
		tmp.transform.parent = WeaponStorePanel.transform;
		tmp.transform.localScale = Vector3.one;
		StorageItem.WeaponSItem.Add (tmp);

	}


}

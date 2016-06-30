#define TEST
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
	static GameObject WeaponStorePanel;
	static GameObject ArmorStorePanel;
	static GameObject ShieldStorePanel;
	static GameObject MountStorePanel;
	Transform PopupHolder;
	//List<GameObject> StorageItem = new List<GameObject> ();

	float prefabWidth = 0f;
	float prefabHeight = 0f;
	public static void GetStorageInfoFromDB ()
	{
		var game = Game.Instance;
		var wsc = WsClient.Instance;
//		if (game.login.id != 0) {
//			JSONClass json = new JSONClass ();
//			json.Add ("data", new JSONData (game.login.id));
//			json ["table"] = "storage";
//			json ["action"] = "GET";
//			wsc.Send (json.ToString ());
//		}
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
		AssignGameObjectVariable ();
		ProductDict p = new ProductDict ();
		int weaponCount = game.weapon.Count;
		int armorCount = game.armor.Count;
		int shieldCount = game.shield.Count;
		SetPanel ("Weapon", weaponCount);
		SetPanel ("Armor", armorCount);
		SetPanel ("Shield", shieldCount);
//		SetPanel ("Mount", 2);

//		StorageItem.WeaponSItem [0].ItemPic = test;
		for (int i = 0; i < weaponCount; i++) {
			SetItem("Weapon",i , p.products[ game.weapon[i].type].name,game.weapon[i].quantity);
		}
		for (int i = 0; i < armorCount; i++) {
			SetItem("Armor",i , p.products[ game.armor[i].type].name,game.armor[i].quantity);
		}
		for (int i = 0; i < shieldCount; i++) {
			SetItem("Shield",i , p.products[ game.shield[i].type].name,game.shield[i].quantity);
		}
//		for (int i = 0; i < mountCount; i++) {
//			SetItem("Mount",i , p.products[ game.mount[i].type].name,game.mount[i].quantity);
//		}
	}

	void AssignGameObjectVariable(){
		PopupHolder = transform.Find ("PopupHolder");
		WeaponStorePanel = GetGridLayoutPanel("WeaponGridList");
		ArmorStorePanel = GetGridLayoutPanel("ArmorGridList");
		ShieldStorePanel = GetGridLayoutPanel("ShieldGridList");
		MountStorePanel = GetGridLayoutPanel("MountsGridList");
	}

	GameObject GetGridLayoutPanel(string name){
		Transform gridList = PopupHolder.Find (name);
		return gridList.GetChild (0).GetChild (0).GetChild (0).gameObject;
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

	public void SetItem(string panel, int index, string name, int quantity){
		List<StorageItem> items = null;
		if (panel == "Weapon") {
			items = StorageItem.WeaponSItem;
		} else if (panel == "Armor") {
			items = StorageItem.ArmorSItem;
		} else if (panel == "Shield") {
			items = StorageItem.ShieldSItem;
		} //else if (panel == "Mount") {
//			items = StorageItem.MountSItem;
//		}
		items [index].SetNameText(name);
		items [index].SetItemText(quantity.ToString ());
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

#if TEST
	public void TestAddPanel(){
		StorageItem tmp = Instantiate (Resources.Load ("StorageItemPrefab") as GameObject).GetComponent<StorageItem>();
		tmp.transform.parent = WeaponStorePanel.transform;
		tmp.transform.localScale = Vector3.one;
		StorageItem.WeaponSItem.Add (tmp);
	}
#endif

}

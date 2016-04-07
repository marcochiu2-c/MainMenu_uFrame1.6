#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using Utilities;
using System;
using System.Collections.Generic;

public class ArtisanHolder : MonoBehaviour {
	Game game;
	WsClient wsc = WsClient.Instance;
	public GameObject ArtisanWeaponPanel;
	public GameObject ArtisanArmorPanel;
	public GameObject ArtisanShieldPanel;
	public GameObject DisablePopup;
	public GameObject ArtisanConfirmPopup;
	public GameObject NeedExtraResourcesPopup;
	public GameObject SpeedUpPopup;
	public GameObject EquipmentQHolder;
	Button BackButton;
	Button CloseButton;
	static ProductDict p  = new ProductDict();
	static int IdWeaponWhichProducing  = 0;
	static int IdArmorWhichProducing   = 0;
	static int IdShieldWhichProducing  = 0;
	public static int IdEquipmentToBeProduced = 0;
	static int NumberOfEquipmentToBeProduced=0;

	int latestEta = 0;
	// Use this for initialization
	void Start () {
		CallArtisanHolder ();
	}


	public void CallArtisanHolder(){
		game = Game.Instance;
		BackButton = transform.GetChild (1).GetComponent<Button> ();
		CloseButton = transform.GetChild (2).GetComponent<Button> ();
		AddButtonListener ();
		SetPanel (game.weapon);
		SetPanel (game.armor);
		SetPanel (game.shield);
		latestEta = GetLatestEta ();
		InvokeRepeating ("updateProductionEtaTimeText", 0, 1);
		SetItemButtonActivateWhenJobComplete ();
	}

	void AddButtonListener(){
		BackButton.onClick.AddListener (() => {
			ArtisanWeaponPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanArmorPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanShieldPanel.transform.parent.parent.gameObject.SetActive(false);
			transform.GetChild(0).gameObject.SetActive(true);
		});
		CloseButton.onClick.AddListener (() => {
			ArtisanWeaponPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanArmorPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanShieldPanel.transform.parent.parent.gameObject.SetActive(false);
			gameObject.SetActive(false);
		});
		EquipmentQHolder.transform.GetChild (2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {  //Confirm
			OnEquipmentQHolderConfirmed();
		});
		EquipmentQHolder.transform.GetChild (2).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {  //Cancel
			HidePanel(EquipmentQHolder);
			EquipmentQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text = "";
		});
		ArtisanConfirmPopup.transform.GetChild (2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {  //Confirm
			OnArtisanConfirmPopupConfirmed();
		});
		ArtisanConfirmPopup.transform.GetChild (2).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {  //Confirm
			HidePanel(ArtisanConfirmPopup);
		});
		NeedExtraResourcesPopup.transform.GetChild (2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {  //Confirm

		});
		NeedExtraResourcesPopup.transform.GetChild (2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {  //Confirm
			HidePanel(NeedExtraResourcesPopup);
		});
	}

	void OnEquipmentQHolderConfirmed(){
		HidePanel(EquipmentQHolder);
		NumberOfEquipmentToBeProduced = int.Parse(EquipmentQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text);
		EquipmentQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text = "";
		ShowPanel(ArtisanConfirmPopup);
		SetArtisanConfirmPopupText ();
	}

	void OnArtisanConfirmPopupConfirmed(){
		int cost = p.products [IdEquipmentToBeProduced].attributes ["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced;
		HidePanel(ArtisanConfirmPopup);
		if (cost > game.wealth [2].value) {
			ShowPanel (NeedExtraResourcesPopup);
			SetNeedExtraResourcesPopupText();
		} else {
			game.wealth[2].Deduct(cost);
			SetJob();
		}
	}

	void OnNeedExtraResourcesPopupConfirmed(){
		int cost = p.products [IdEquipmentToBeProduced].attributes ["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced;
		HidePanel(NeedExtraResourcesPopup);

	}

	void SetJob(){
		int type=0;
		if (IdEquipmentToBeProduced > 5000 && IdEquipmentToBeProduced < 6000) {
			type = 0;
		}else if (IdEquipmentToBeProduced > 6000 && IdEquipmentToBeProduced < 7000) {
			type = 1;
		}else if (IdEquipmentToBeProduced > 7000 && IdEquipmentToBeProduced < 8000) {
			type = 2;
		}
		game.artisans [type].targetId = IdEquipmentToBeProduced;
		game.artisans [type].resources = p.products [IdEquipmentToBeProduced].attributes ["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced;
		game.artisans [type].details = " ";
		game.artisans [type].quantity = NumberOfEquipmentToBeProduced;
		game.artisans [type].startTimestamp = DateTime.Now;
		game.artisans [type].etaTimestamp = DateTime.Now.Add (new TimeSpan(p.products [IdEquipmentToBeProduced].attributes ["ProductionTime"].AsInt * NumberOfEquipmentToBeProduced * 1000 * 10000));
		game.artisans [type].status = 1;
		game.artisans [type].UpdateObject ();
	}

	void SetArtisanConfirmPopupText(){
		ProductDict p = new ProductDict ();
		string msg = "製造equipment需要amount的資源，確定製造嗎？";
		msg = msg.Replace ("equipment", p.products [IdEquipmentToBeProduced].name);
		msg = msg.Replace ("amount", (p.products[IdEquipmentToBeProduced].attributes["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced).ToString()) ;
		ArtisanConfirmPopup.transform.GetChild (1).GetComponent<Text> ().text = msg;
	}

	void SetNeedExtraResourcesPopupText(){
		ProductDict p = new ProductDict ();
		int res = (p.products [IdEquipmentToBeProduced].attributes ["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced);
		string msg = "主公，資源不足，需要額外使用amount 星塵進行制作嗎？";
		msg = msg.Replace ("amount",Utilities.ExchangeRate.GetStardustFromResource(res-game.wealth[2].value).ToString());
		NeedExtraResourcesPopup.transform.GetChild (1).GetComponent<Text> ().text = msg;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetPanel<T>(List<T> weapon){
		List<WeaponMaking> wsi = null;
		WeaponMaking wobj = null;
		int quantity = weapon.Count;
		Products products;
		string panel = weapon[0].GetType ().ToString ();
		HighlightProceedingJobs ();
		for (var i = 0; i < quantity; i++) {
			wobj = Instantiate(Resources.Load("WeaponMakingPrefab") as GameObject).GetComponent<WeaponMaking>();
			if (panel == "Weapon") {
				wsi = WeaponMaking.Weapons;
				wobj.transform.parent = ArtisanWeaponPanel.transform;
				Weapon x = weapon[i] as Weapon;
				if (IdWeaponWhichProducing== x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[0].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
			}else if (panel == "Armor"){
				wsi = WeaponMaking.Armors;
				wobj.transform.parent = ArtisanArmorPanel.transform;
				Armor x = weapon[i] as Armor;
				if (IdArmorWhichProducing == x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[0].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
			}else if (panel == "Shield"){
				wsi = WeaponMaking.Armors;
				wobj.transform.parent = ArtisanShieldPanel.transform;
				Shield x = weapon[i] as Shield;
				if(IdShieldWhichProducing == x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[0].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
			}
			RectTransform rTransform = wobj.GetComponent<RectTransform>();
			wsi.Add (wobj);
			rTransform.localScale=Vector3.one;
		}

	}

	void HighlightProceedingJobs(){
		if (game.artisans [0].etaTimestamp > DateTime.Now) {
			IdWeaponWhichProducing = game.artisans[0].targetId;
		}else if (game.artisans [1].etaTimestamp > DateTime.Now) {
			IdArmorWhichProducing = game.artisans[1].targetId;
		}else if (game.artisans [2].etaTimestamp > DateTime.Now) {
		}
	}

	int GetLatestEta(){
		int last = 0;
		if (game.artisans [0].etaTimestamp < game.artisans [1].etaTimestamp) {
			last = 1;
			if (game.artisans [1].etaTimestamp < game.artisans [2].etaTimestamp) {
				last = 2;
			}
		} else if (game.artisans [0].etaTimestamp < game.artisans [2].etaTimestamp){
			last = 2;
		}
		return last;
	}

	void updateProductionEtaTimeText(){
		if (game.artisans [latestEta].etaTimestamp > DateTime.Now) {
			TimeSpan ts = game.artisans [latestEta].etaTimestamp.Subtract (DateTime.Now);
			transform.GetChild (0).GetChild (2).GetChild (3).GetComponent<Text> ().text = string.Format ("生產中 {0:D2}:{1:D2}:{2:D2} 後完成", ts.Hours, ts.Minutes, ts.Seconds);
		} else {
			transform.GetChild (0).GetChild (2).GetChild (3).GetComponent<Text> ().text = "生產中 00:00:00 後完成";
		}
	}

	void SetItemButtonInteractable(GameObject panel, bool value){
		Transform t = panel.transform.GetChild (0).GetChild (0);
		int count = t.childCount;
		for (int i = 0 ; i < count ; i++){
			t.GetChild(i).GetComponent<Button>().interactable = value;
		}
	}

	void OnWeaponJobComplete(){
		SetItemButtonInteractable (ArtisanWeaponPanel, true);
		game.artisans [0].status = 3;
	}

	void OnArmorJobComplete(){
		SetItemButtonInteractable (ArtisanArmorPanel, true);
		game.artisans [1].status = 3;
	}

	void OnShieldJobComplete(){
		SetItemButtonInteractable (ArtisanShieldPanel, true);
		game.artisans [2].status = 3;
	}

	void SetItemButtonActivateWhenJobComplete(){
		if (game.artisans [0].status == 1 || game.artisans [0].status == 4) {
			if (game.artisans [0].etaTimestamp > DateTime.Now){
				SetItemButtonInteractable (ArtisanWeaponPanel, false);
				Invoke ("OnWeaponJobComplete", (float)((game.artisans [0].etaTimestamp - DateTime.Now).TotalSeconds));
			}
		}
		if (game.artisans [1].status == 1 || game.artisans [1].status == 4) {
			if (game.artisans [1].etaTimestamp > DateTime.Now){
				SetItemButtonInteractable (ArtisanArmorPanel, false);
				Invoke ("OnArmorJobComplete", (float)((game.artisans [1].etaTimestamp - DateTime.Now).TotalSeconds));
			}
		}
		if (game.artisans [2].status == 1 || game.artisans [2].status == 4) {
			if (game.artisans [2].etaTimestamp > DateTime.Now){
				SetItemButtonInteractable (ArtisanShieldPanel, false);
				Invoke ("OnShieldJobComplete", (float)((game.artisans [2].etaTimestamp - DateTime.Now).TotalSeconds));
			}
		}
	}

	void ShowPanel(GameObject panel){
		DisablePopup.SetActive (true);
		panel.SetActive (true);
	}
	
	void HidePanel(GameObject panel){
		DisablePopup.SetActive (false);
		panel.SetActive (false);
	}

}

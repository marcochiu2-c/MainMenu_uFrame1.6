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
	public GameObject JobCancelPopup;
	public GameObject DetailPanel;
	Button BackButton;
	Button CloseButton;
	static ProductDict p  = ProductDict.Instance;
	static int IdWeaponWhichProducing  = 0;
	static int IdArmorWhichProducing   = 0;
	static int IdShieldWhichProducing  = 0;
	public static int OpenedHolder = 0;
	public static int IdEquipmentToBeProduced = 0;
	static int NumberOfEquipmentToBeProduced=0;
	public static int CancelType = 0;
	public static int CancelId   = 0;
	public static GameObject staticDisablePopup;
	public static GameObject staticSpeedUpPopup;
	public static GameObject staticJobCancelPopup;
	public static GameObject staticEquipmentQHolder;
	public static GameObject staticDetailPanel;

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

		latestEta = GetLatestEta ();

		SetItemButtonActivateWhenJobComplete ();

		staticDisablePopup = DisablePopup;
		staticSpeedUpPopup = SpeedUpPopup;
		staticJobCancelPopup = JobCancelPopup;
		staticEquipmentQHolder = EquipmentQHolder;
		staticDetailPanel = DetailPanel;
	}

	void OnEnable(){
		game = Game.Instance;
		SetPanel (game.weapon);
		SetPanel (game.armor);
		SetPanel (game.shield);
		InvokeRepeating ("updateProductionEtaTimeText", 0.5f, 1);
		InvokeRepeating ("OnArtisanJobsComplete", 0.5f, 1);
	}

	void OnDisable(){
		CancelInvoke ();
	}

	void OnArtisanJobsComplete (){
		Game game = Game.Instance;
		int id= 0;
		if (game.artisans [0].etaTimestamp <= DateTime.Now && (game.artisans[0].status == 1 || game.artisans[0].status==4)) {
			id = game.weapon.FindIndex (x => x.type == game.artisans[0].targetId);
			game.weapon[id].quantity += game.artisans[0].quantity;
			game.weapon[id].UpdateObject();
			game.artisans[0].status = 3;
			game.artisans[0].UpdateObject();
			WeaponMaking.Weapons.Find(x => x.id == game.weapon[id].type).UpdateRemainingTime();
		}
		if (game.artisans [1].etaTimestamp <= DateTime.Now && (game.artisans[1].status == 1 || game.artisans[1].status==4)) {
			id = game.armor.FindIndex (x => x.type == game.artisans[1].targetId);
			game.armor[id].quantity += game.artisans[1].quantity;
			game.armor[id].UpdateObject();
			game.artisans[1].status = 3;
			game.artisans[1].UpdateObject();
			WeaponMaking.Armors.Find(x => x.id == game.armor[id].type).UpdateRemainingTime();
		}
		if (game.artisans [2].etaTimestamp <= DateTime.Now && (game.artisans[2].status == 1 || game.artisans[2].status==4)) {
			id = game.shield.FindIndex (x => x.type == game.artisans[2].targetId);
			game.shield[id].quantity += game.artisans[2].quantity;
			game.shield[id].UpdateObject();
			game.artisans[2].status = 3;
			game.artisans[2].UpdateObject();
			WeaponMaking.Shields.Find(x => x.id == game.shield[id].type).UpdateRemainingTime();
		}
	}

	void CloseAllPanel(string buttonName){
		DisablePopup.SetActive (false);
		if (buttonName == "Cancel") {
			if (ArtisanConfirmPopup.activeSelf || NeedExtraResourcesPopup.activeSelf || SpeedUpPopup.activeSelf ||
				 EquipmentQHolder.activeSelf || JobCancelPopup.activeSelf|| DetailPanel.activeSelf){
				Debug.Log("CloseAllPanel()");
				ArtisanConfirmPopup.SetActive (false);
				NeedExtraResourcesPopup.SetActive (false);
				SpeedUpPopup.SetActive (false);
				EquipmentQHolder.SetActive (false);
				JobCancelPopup.SetActive (false);
				DetailPanel.SetActive (false);
			} else {
				ArtisanWeaponPanel.transform.parent.parent.gameObject.SetActive (false);
				ArtisanArmorPanel.transform.parent.parent.gameObject.SetActive (false);
				ArtisanShieldPanel.transform.parent.parent.gameObject.SetActive (false);
				OpenedHolder = 0;
			}
		} else {
			OpenedHolder = 0;
			ArtisanConfirmPopup.SetActive (false);
			NeedExtraResourcesPopup.SetActive (false);
			SpeedUpPopup.SetActive (false);
			EquipmentQHolder.SetActive (false);
			JobCancelPopup.SetActive (false);
			DetailPanel.SetActive (false);
			ArtisanWeaponPanel.transform.parent.parent.gameObject.SetActive (false);
			ArtisanArmorPanel.transform.parent.parent.gameObject.SetActive (false);
			ArtisanShieldPanel.transform.parent.parent.gameObject.SetActive (false);
			gameObject.SetActive(false);
		}
	}

	void DestroyPrefabObject(){
		var count = WeaponMaking.Weapons.Count;
		for (int i = 0 ; i < count ; i++){
			GameObject.DestroyImmediate( WeaponMaking.Weapons[i].gameObject);
		}
		count =WeaponMaking.Armors.Count;
		for (int i = 0 ; i < count ; i++){
			GameObject.DestroyImmediate( WeaponMaking.Armors[i].gameObject);
		}
		count =WeaponMaking.Shields.Count;
		for (int i = 0 ; i < count ; i++){
			GameObject.DestroyImmediate( WeaponMaking.Shields[i].gameObject);
		}
		WeaponMaking.Weapons = new List<WeaponMaking> ();
		WeaponMaking.Armors = new List<WeaponMaking> ();
		WeaponMaking.Shields = new List<WeaponMaking> ();
	}

	void AddButtonListener(){
		BackButton.onClick.AddListener (() => {
			CloseAllPanel("Cancel");
			transform.GetChild(0).gameObject.SetActive(true);
		});
		CloseButton.onClick.AddListener (() => {
			CloseAllPanel("Close");
			DestroyPrefabObject();
			Resources.UnloadUnusedAssets();
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
			OnNeedExtraResourcesPopupConfirmed();
		});
		NeedExtraResourcesPopup.transform.GetChild (2).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {  //Confirm
			HidePanel(NeedExtraResourcesPopup);
		});
		SpeedUpPopup.transform.GetChild (2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			OnSpeedUpPopupConfirmed();
		});
		SpeedUpPopup.transform.GetChild (2).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			HidePanel (SpeedUpPopup);
		});
		JobCancelPopup.transform.GetChild(2).GetChild(0).GetComponent<Button> ().onClick.AddListener (() => {
			OnCancelPopupConfirmed();
		});
		JobCancelPopup.transform.GetChild(2).GetChild(1).GetComponent<Button> ().onClick.AddListener (() => {
			HidePanel (JobCancelPopup);
		});
		DetailPanel.transform.GetChild(2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			HidePanel (DetailPanel);
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
		game.wealth [1].Deduct( Utilities.ExchangeRate.GetStardustFromResource (cost - game.wealth [2].value));
		game.wealth [2].Set (0);
		SetJob ();
	}

	void OnSpeedUpPopupConfirmed(){  // 10 Stardust for 1 hour
		DateTime time =DateTime.Now;
		int id = ArtisanHolder.IdEquipmentToBeProduced;
		int count = 0;
		if (id == game.artisans [0].targetId) {
			time = game.artisans[0].etaTimestamp;
			game.artisans[0].status = 3;
			game.artisans[0].etaTimestamp = DateTime.Now;
			count = game.weapon.Count;
			for (int i =0 ; i < count ; i++){
				if (game.weapon[i].type == id){
					game.weapon[i].SetQuantity(game.artisans [0].quantity + game.weapon[i].quantity);
				}
			}
			ResetItemAfterSpeedUpOrJobCancel(ArtisanWeaponPanel, id);
			game.artisans[0].UpdateObject();
		}else if (id == game.artisans [1].targetId) {
			time = game.artisans[1].etaTimestamp;
			game.artisans[1].status = 3;
			game.artisans[1].etaTimestamp = DateTime.Now;
			count = game.armor.Count;
			for (int i =0 ; i < count ; i++){
				if (game.armor[i].type == id){
					game.armor[i].SetQuantity(game.artisans [1].quantity + game.armor[i].quantity);
				}
			}
			ResetItemAfterSpeedUpOrJobCancel(ArtisanArmorPanel, id);
			game.artisans[1].UpdateObject();
		}else if (id == game.artisans [2].targetId) {
			time = game.artisans[2].etaTimestamp;
			game.artisans[2].status = 3;
			game.artisans[2].etaTimestamp = DateTime.Now;
			count = game.shield.Count;
			for (int i =0 ; i < count ; i++){
				if (game.shield[i].type == id){
					game.shield[i].SetQuantity(game.artisans [2].quantity + game.shield[i].quantity);
				}
			}
			ResetItemAfterSpeedUpOrJobCancel(ArtisanShieldPanel, id);
			game.artisans[2].UpdateObject();
		}
		TimeSpan tdiff = time - DateTime.Now;
		int cost = (int)(tdiff.TotalHours * 10);
		game.wealth [1].Deduct (cost);
		HidePanel (SpeedUpPopup);
	}

	void OnCancelPopupConfirmed(){
		int money = 0;
		money = p.products[CancelId].attributes["NumberOfProductionResources"].AsInt * game.artisans[CancelType].quantity;

		game.artisans [CancelType].status = 0;
		game.artisans [CancelType].etaTimestamp = DateTime.Now;
		game.artisans [CancelType].UpdateObject ();

		game.wealth [2].Add (money);
		CancelType = 0;
		if (CancelType == 0) {
			ResetItemAfterSpeedUpOrJobCancel (ArtisanWeaponPanel,CancelId);
		} else if (CancelType == 1) {
			ResetItemAfterSpeedUpOrJobCancel (ArtisanArmorPanel,CancelId);
		} else {
			ResetItemAfterSpeedUpOrJobCancel (ArtisanShieldPanel,CancelId);
		}

		HidePanel (JobCancelPopup);
	}

	void SetJob(){
		DateTime  eta = DateTime.Now.Add (new TimeSpan(0,0,Mathf.Abs(p.products [IdEquipmentToBeProduced].attributes ["ProductionTime"].AsInt * NumberOfEquipmentToBeProduced)));;
		int type=0;
		if (IdEquipmentToBeProduced > 5000 && IdEquipmentToBeProduced < 6000) {
			type = 0;
			WeaponMaking.Weapons.Find (x => x.id == IdEquipmentToBeProduced).eta = eta;
			WeaponMaking.Weapons.Find (x => x.id == IdEquipmentToBeProduced).SetAutoRun();
		}else if (IdEquipmentToBeProduced > 6000 && IdEquipmentToBeProduced < 7000) {
			type = 1;
			WeaponMaking.Armors.Find (x => x.id == IdEquipmentToBeProduced).eta = eta;
			WeaponMaking.Armors.Find (x => x.id == IdEquipmentToBeProduced).SetAutoRun();
		}else if (IdEquipmentToBeProduced > 7000 && IdEquipmentToBeProduced < 8000) {
			type = 2;
			WeaponMaking.Shields.Find (x => x.id == IdEquipmentToBeProduced).eta = eta;
			WeaponMaking.Shields.Find (x => x.id == IdEquipmentToBeProduced).SetAutoRun();
		}
		game.artisans [type].targetId = IdEquipmentToBeProduced;
		game.artisans [type].resources = p.products [IdEquipmentToBeProduced].attributes ["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced;
		game.artisans [type].details = " ";
		game.artisans [type].quantity = Mathf.Abs (NumberOfEquipmentToBeProduced);
		game.artisans [type].startTimestamp = DateTime.Now;
		Debug.Log ("p.products [IdEquipmentToBeProduced].attributes [\"ProductionTime\"].AsInt: " + p.products [IdEquipmentToBeProduced].attributes ["ProductionTime"].AsInt);
		Debug.Log ("NumberOfEquipmentToBeProduced: " + NumberOfEquipmentToBeProduced);
		game.artisans [type].etaTimestamp = DateTime.Now.Add (new TimeSpan(0,0,Mathf.Abs(p.products [IdEquipmentToBeProduced].attributes ["ProductionTime"].AsInt * NumberOfEquipmentToBeProduced)));
		game.artisans [type].status = 1;
		game.artisans [type].UpdateObject ();
		latestEta = GetLatestEta ();
	}

	void SetArtisanConfirmPopupText(){
		ProductDict p = ProductDict.Instance;
		string msg = "製造equipment需要amount的資源，確定製造嗎？";
		msg = msg.Replace ("equipment", p.products [IdEquipmentToBeProduced].name);
		msg = msg.Replace ("amount", (p.products[IdEquipmentToBeProduced].attributes["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced).ToString()) ;
		ArtisanConfirmPopup.transform.GetChild (1).GetComponent<Text> ().text = msg;

		#region ChangeTitle
		if (IdEquipmentToBeProduced > 5000 && IdEquipmentToBeProduced < 6000) {
			ArtisanConfirmPopup.transform.GetChild (0).GetComponent<Text>().text = "武器";
		}else if (IdEquipmentToBeProduced > 6000 && IdEquipmentToBeProduced < 7000) {
			ArtisanConfirmPopup.transform.GetChild (0).GetComponent<Text>().text = "防具";
		}else if (IdEquipmentToBeProduced > 7000 && IdEquipmentToBeProduced < 8000) {
			ArtisanConfirmPopup.transform.GetChild (0).GetComponent<Text>().text = "盾";
		}
		#endregion
	}

	void SetNeedExtraResourcesPopupText(){
		ProductDict p = ProductDict.Instance;
		int cost = (p.products [IdEquipmentToBeProduced].attributes ["NumberOfProductionResources"].AsInt * NumberOfEquipmentToBeProduced);
		string msg = "主公，資源不足，需要額外使用amount 星塵進行制作嗎？";
		msg = msg.Replace ("amount",Utilities.ExchangeRate.GetStardustFromResource(cost - game.wealth[2].value).ToString());
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
				WeaponMaking.Weapons.Add (wobj);
			}else if (panel == "Armor"){
				wsi = WeaponMaking.Armors;
				wobj.transform.parent = ArtisanArmorPanel.transform;
				Armor x = weapon[i] as Armor;
				if (IdArmorWhichProducing == x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[1].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
				WeaponMaking.Armors.Add (wobj);
			}else if (panel == "Shield"){
				wsi = WeaponMaking.Armors;
				wobj.transform.parent = ArtisanShieldPanel.transform;
				Shield x = weapon[i] as Shield;
				if(IdShieldWhichProducing == x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[2].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
				WeaponMaking.Shields.Add (wobj);
			}
			RectTransform rTransform = wobj.GetComponent<RectTransform>();
//			wsi.Add (wobj);
			rTransform.localScale=Vector3.one;
		}

	}

	void HighlightProceedingJobs(){
		if (game.artisans [0].etaTimestamp > DateTime.Now) {
			IdWeaponWhichProducing = game.artisans[0].targetId;
		}
		if (game.artisans [1].etaTimestamp > DateTime.Now) {
			IdArmorWhichProducing = game.artisans[1].targetId;
		}
		if (game.artisans [2].etaTimestamp > DateTime.Now) {
			IdShieldWhichProducing = game.artisans[2].targetId;
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
			transform.GetChild (0).GetChild (2).GetChild (3).GetComponent<Text> ().text = string.Format ("生產中 {0} 後完成", Utilities.TimeUpdate.Time(game.artisans [latestEta].etaTimestamp));
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
//		game.artisans [0].status = 3;
//		game.artisans [0].UpdateObject ();
	}

	void OnArmorJobComplete(){
		SetItemButtonInteractable (ArtisanArmorPanel, true);
//		game.artisans [1].status = 3;
//		game.artisans [1].UpdateObject ();
	}

	void OnShieldJobComplete(){
		SetItemButtonInteractable (ArtisanShieldPanel, true);
//		game.artisans [2].status = 3;
//		game.artisans [2].UpdateObject ();
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

	void ResetItemAfterSpeedUpOrJobCancel(GameObject panel, int idEnd){
		int count = panel.transform.childCount;
		for (int i =0; i< count; i++) {
			if(panel.transform.GetChild(i).GetComponent<WeaponMaking>().id == idEnd){
				panel.transform.GetChild(i).GetComponent<WeaponMaking>().eta = DateTime.Now;
				return;
			}
		}
	}
}

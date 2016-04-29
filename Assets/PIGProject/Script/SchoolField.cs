using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

public class SchoolField : MonoBehaviour {
	public GameObject DollPanel;
	public GameObject DataPanel;
	public GameObject DisablePanel;
	public GameObject NewSoldierPanel;
	public GameObject AssignNSPopup;
	public GameObject ButtonHolder;
	public GameObject ArmyListHolder;
	public GameObject ArmyQAHolder;
	public GameObject MountListHolder;
	public GameObject MountQAHolder;
	public GameObject ArmorListHolder;
	public GameObject ArmorQAHolder;
	public GameObject AdjustSoildersAttribute;
	public GameObject ShieldListHolder;
	public GameObject ShieldQAHolder;
	public GameObject TrainingQHolder;
	public GameObject TrainingEquHolder;
	public GameObject TrainingEquConfirmHolder;
	public GameObject TrainingQAHolder;
	public GameObject CannotTrainSoldierPopup;
	public GameObject ConfirmSpeedUpHolder;
	public Text TrainingQText;

	public static GameObject staticArmyQAHolder;
	public static GameObject staticArmorQAHolder;
	public static GameObject staticShieldQAHolder;
	public static GameObject staticTrainingEquHolder;
	public static GameObject staticDisablePanel;

	public static int AssigningWeaponId=0;
	public static int AssigningArmorId=0;
	public static int AssigningShieldId=0;
	public static int AssigningSoldier=1;  // 兵種
	public static int AssigningResources=0;
	public static int AssigningStarDust=0;
	public static DateTime AssigningTime;
	public static int AssigningQuantity = 0;
	public static int TotalTrainingTime=0;
	public static int refStarDust = 80;
	public static int refResource =160000;
//	public static int refFeather  = 400;
	Text SoldierSummary;
	static ProductDict p;

#region stringDefinition
	static string msgExistingSoldierQuantity= "現有兵數："; 
	static string msgSoldierQuantityWantToTrain = "請輸入欲訓練士兵數目";
	static string msgWarningSOldierQuantiyOverQuota = "<color=red>輸入兵數不能大於現有兵數</color>";
	static string msgExtraEquipmentRequired = "訓練%SQ%個士兵需要製造額外%EQ%的裝備，找工匠嗎？";
	static string msgEquipmentReplacement = "確定更改%Orig%為%New%嗎？";
	static string msgQuestionConfirmProductionTimeForExtraEquipment = "額外%EQ%的裝備需要%SU%物資和時間%TM%，立刻製作嗎？";
	static string msgQuestionConfirmTimeForTrainingSOldiers = "訓練%SQ%的士兵需時%TT%，確定訓練嗎？";


#endregion

	Game game;
	WsClient wsc;
	Draggable drag;
//	public bool hasTrainingQHolderShown = false;
	RectTransform rt;
	// Use this for initialization
	void Start () {
		CallSchoolField ();
	}

	void CallSchoolField(){

		game = Game.Instance;
		wsc = WsClient.Instance;
		SchoolField.AssigningSoldier = 1;
//		Debug.Log (game.counselor [0].toJSON ().ToString ());
//		Debug.Log (TotalSoldiersAvailable());
		InvokeRepeating ("ShowTotalSoldiersAvailableText", 0, 60);
		AddButtonListener ();
		staticArmyQAHolder = ArmyQAHolder;
		staticArmorQAHolder = ArmorQAHolder;
		staticShieldQAHolder = ShieldQAHolder;
		staticTrainingEquHolder = TrainingEquHolder;
		staticDisablePanel = DisablePanel;
		DisablePanel.SetActive (false);
		SoldierSummary = DollPanel.transform.GetChild (2).GetComponent<Text>();
		SetDataPanel ();
		SetAdjustSoldierValues ();
		p = ProductDict.Instance;
		InvokeRepeating ("UpdateSoldierSummaryPanel", 0, 1);
	}

	void OnEnable(){
		Invoke("SetDataPanel",0.5f);
	}


	// Update is called once per frame
	void Update () {

	}
	
	void AddButtonListener(){
//		TrainingQAHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { Debug.Log ("OnTrainingQAHolderConfirmed()");OnTrainingQAHolderConfirmed(); });
//		Debug.Log (NewSoldierPanel.transform.GetChild (1));
		NewSoldierPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
			if (game.soldiers[AssigningSoldier-1].attributes["ETATrainingTime"] == null){
				ShowPanel(TrainingQHolder);
			}
		});
		DollPanel.transform.GetChild (0).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			DisablePanel.SetActive(true);
			OnWeaponButtonClicked (); });
		DollPanel.transform.GetChild (1).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			DisablePanel.SetActive(true);
			OnArmorButtonClicked (); });
		DollPanel.transform.GetChild (1).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			DisablePanel.SetActive(true);
			OnShieldButtonClicked (); });
		DataPanel.transform.GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			if (CheckIfTrainingOngoing()){
				return;
			}
			DisablePanel.SetActive(true);
			SetAdjustSoldierValues();
		});
		#region ChooseSoldierType  
		//TODO assign action when changing Soldier Type
		ButtonHolder.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(5).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(6).GetComponent<Button>().onClick.AddListener(() => { });
		ButtonHolder.transform.GetChild(7).GetComponent<Button>().onClick.AddListener(() => { });
		#endregion

		#region ConfirmationReplaceArmedEquipment
		ArmyQAHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { OnArmedReplacement("weapon");});
		ArmorQAHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { OnArmedReplacement("armor");});
		ShieldQAHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { OnArmedReplacement("shield");});
		ArmyQAHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { HidePanel(ArmyQAHolder);});
		ArmorQAHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { HidePanel(ArmorQAHolder);});
		ShieldQAHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { HidePanel(ShieldQAHolder);});
		#endregion
		TrainingQHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => {  //confirm
			Debug.Log ("Soldier number assignment confirmed.");
			HidePanel(TrainingEquHolder);
			game.soldiers[AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = int.Parse(TrainingQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text);
			TrainingQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text = "";
		});
		TrainingQHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {  //cancel
			HidePanel(TrainingQHolder);
			TrainingQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text = "";
		});
		
		TrainingEquHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { OnGoArtisanButtonClicked();});
		TrainingEquHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { 
			HidePanel(TrainingEquHolder);
		});

		TrainingEquConfirmHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { OnAddEquipmentConfirmed();});
		TrainingEquConfirmHolder.transform.GetChild (2).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			OnSpeedUpProductionClicked();
		});

		AdjustSoildersAttribute.transform.GetChild (8).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			GetAdjustedSoldierValues();
		});
		AdjustSoildersAttribute.transform.GetChild (8).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			HidePanel(AdjustSoildersAttribute);

		});

		TrainingQAHolder.transform.GetChild (2).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			ConfirmedTraining();
		});
		TrainingQAHolder.transform.GetChild (2).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			CancelConfirmTraining();
		});
		Utilities.Panel.GetConfirmButton (AssignNSPopup).onClick.AddListener (() => {
			HidePanel(AssignNSPopup);
		});
		Utilities.Panel.GetConfirmButton (ConfirmSpeedUpHolder).onClick.AddListener (() => {
			OnSpeedUpProductionConfirmed();
		});
		Utilities.Panel.GetCancelButton (ConfirmSpeedUpHolder).onClick.AddListener (() => {
			HidePanel(ConfirmSpeedUpHolder);
		});

	}

	#region ShowSoldiersAvailable





	void ShowTotalSoldiersAvailableText(){
//		Text availSoldiersText = NewSoldierPanel.transform.GetChild (0).GetComponent<Text> ();
		Text availSoldiersText = NewSoldierPanel.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		var i = TotalSoldiersAvailable ();
		if (i > 99999) {
			availSoldiersText.fontSize -= 1;
		} else if (i > 999999) {
			availSoldiersText.fontSize -= 2;
		}
		availSoldiersText.text = msgExistingSoldierQuantity+"\n"+i;
	}

	int TotalSoldiersAvailable(){
		return TotalSoldierGenerated() - game.login.attributes["TotalDeductedSoldiers"].AsInt;
	}

	int TotalSoldierGenerated(){
		DateTime rt = game.login.registerTime;
		return (int)DateTime.Now.Subtract (rt).TotalMinutes;
	}
	#endregion

	public void OnSetNewSoldierNumber(){
		Text s = TrainingQText;
		string soldiers = Regex.Replace(s.text, "[^0-9]", "");
		if (soldiers != "") {
			int soldierQuantity = Int32.Parse (soldiers);
			if (soldierQuantity > TotalSoldiersAvailable ()) {
				ShowPanel(TrainingQHolder);
				TrainingQHolder.transform.GetChild(1).GetComponent<Text>().text=msgWarningSOldierQuantiyOverQuota+"\n"+msgSoldierQuantityWantToTrain;
				Debug.Log ("Number of new soldiers cannot be larger than available, inputed: " + soldierQuantity);
			} else {   //Valid number
				transform.GetChild(9).GetChild(1).GetComponent<Text>().text= "msgSoldierQuantityWantToTrain";
				transform.GetChild (9).gameObject.SetActive (false);
//				Debug.Log ("士兵數目："+s);
				game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldierQuantity;
				game.soldiers[SchoolField.AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = soldierQuantity;
				ShowTotalSoldiersAvailableText();
//				wsc.Send ("users","SET",game.login.toJSON());
				game.soldiers [SchoolField.AssigningSoldier - 1].UpdateObject();
				s.text = "";
				HidePanel( TrainingQHolder);
//				CheckArmedEquipmentAvailability();
				SetDataPanel();
			}
		} else {
			return;
		}
	}

	public static void CheckArmedEquipmentAvailability(){
		Game game = Game.Instance;
		string msg = msgExtraEquipmentRequired;
		var count = 0;
		if (AssigningWeaponId != 0) {
			count = game.weapon.Count;
//			AssigningArmorId = 0;
//			AssigningShieldId = 0;
			for (int i = 0 ; i< count ; i++){
				if(game.weapon[i].type == AssigningWeaponId){

					if (game.weapon[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt){
						msg = msg.Replace("%SQ%",game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"]);
						msg = msg.Replace("%EQ%",(game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt - game.weapon[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticShowPanel( staticTrainingEquHolder);
					}else{
						game.weapon[i].SetQuantity(game.weapon[i].quantity - game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt);
					}
				}
			}
		}
		if (AssigningArmorId != 0) {
			count = game.weapon.Count;
//			AssigningWeaponId = 0;
//			AssigningShieldId = 0;
			for (int i = 0 ; i< count ; i++){
				if(game.armor[i].type == AssigningArmorId){

					if (game.armor[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt){
						msg = msg.Replace("%SQ%",game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"]);
						msg = msg.Replace("%EQ%",(game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt - game.armor[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticShowPanel( staticTrainingEquHolder);
					}else{
						game.weapon[i].SetQuantity(game.armor[i].quantity - game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt);
					}
				}
			}
		}

		if (AssigningShieldId != 0) {
			count = game.shield.Count;
//			AssigningWeaponId = 0;
//			AssigningArmorId = 0;
			for (int i = 0 ; i< count ; i++){
				if(game.shield[i].type == AssigningShieldId){
					if (game.shield[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt){
						msg = msg.Replace("%SQ%",game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"]);
						msg = msg.Replace("%EQ%",(game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt - game.shield[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticShowPanel( staticTrainingEquHolder);
					}else{
						game.weapon[i].SetQuantity(game.shield[i].quantity - game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt);
					}
				}
			}
		}
	}

	public void ShowNewWeaponPanel(){
		DestroySchoolFieldWeaponPanel(ArmyListHolder.transform);
//		ProductDict p = ProductDict.Instance;
		var count = game.weapon.Count;
		Transform panel = ArmyListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.4f, 0.4f);
			obj.SetSchoolFieldWeapon (SchoolField.AssigningSoldier,
								game.weapon [i].type,
								p.products [game.weapon [i].type].name, 
		                        game.weapon [i].quantity.ToString (), 
		                        p.products [game.weapon [i].type].attributes ["Category"],
		                        "");

			obj.transform.SetParent (panel);
		}
		ShowPanel( ArmyListHolder);
	}

	public void ShowNewArmorPanel(){
		DestroySchoolFieldWeaponPanel(ArmorListHolder.transform);
//		ProductDict p = ProductDict.Instance;
		var count = game.armor.Count;
		Transform panel = ArmorListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.4f, 0.4f);
			obj.SetSchoolFieldWeapon (SchoolField.AssigningSoldier,
									  game.armor [i].type,
			                          p.products [game.armor [i].type].name, 
			                          game.armor [i].quantity.ToString (), 
			                          "",
			                          "");
			
			obj.transform.SetParent (panel);
			
		}
		ShowPanel( ArmorListHolder);
	}

	public void ShowNewShieldPanel(){
		DestroySchoolFieldWeaponPanel(ShieldListHolder.transform);
//		ProductDict p = ProductDict.Instance;
		var count = game.shield.Count;
		Transform panel = ShieldListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.4f, 0.4f);
			obj.SetSchoolFieldWeapon (SchoolField.AssigningSoldier,
			                          game.shield [i].type,
			                          p.products [game.shield [i].type].name, 
			                          game.shield [i].quantity.ToString (), 
			                          "",
			                          "");
			
			obj.transform.SetParent (panel);
			
		}
		ShowPanel( ShieldListHolder);
	}
	
	public void ShowIsNeedRecruitArtisanPanel(int soldiers){
		string txt = TrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text;
		txt = txt.Replace ("%SQ%", soldiers.ToString ());
		TrainingEquHolder.transform.GetChild (1).GetComponent<Text> ().text = txt;
		ShowPanel( TrainingEquHolder);
	}

	public void OnWeaponButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0) {
			ShowNewWeaponPanel ();
		} else {
			HidePanel(ArmyListHolder);
			ShowPanel(AssignNSPopup);
			Debug.Log("No soldier assigned");
		}
	}

	public void OnArmorButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0) {
			ShowNewArmorPanel ();
		} else {
			HidePanel(ArmorListHolder);
			ShowPanel( AssignNSPopup);
		}
	}

	public void OnShieldButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0) {
			ShowNewShieldPanel ();
		} else {
			HidePanel(ShieldListHolder);
			ShowPanel( AssignNSPopup );
		}
	}
	
	public void OnPanelClose(){
		#region Destroy List of Weapons
		DestroySchoolFieldWeaponPanel(ArmyListHolder.transform);
		DestroySchoolFieldWeaponPanel(ArmorListHolder.transform);
		DestroySchoolFieldWeaponPanel(ShieldListHolder.transform);
		#endregion
	}

	public void DestroySchoolFieldWeaponPanel(Transform panel){
		Transform t = panel.transform.GetChild (1).GetChild (1).GetChild (0);
		int count = t.childCount;
		for (int j = 0; j<count; j++) {
			GameObject.DestroyImmediate(t.GetChild(0).gameObject);
		}
	}

	public void SetDataPanel(){
		Transform dataHolder = DataPanel.transform.GetChild (0).GetChild (0);
		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		string[] valueList = new string[]{
			"Hit",				"Dodge",		"Strength",
			"Morale",			"AttackSpeed",	"Wand",
			"PiercingLongWeapon","Bows",		"HighEndBows",
			"HiddenWeapon",		"Knife",		"HackTypeLongWeapon",
			"Sword",			"HighEndSword",	"SpecialSword",
			"Axe",				"Hammer"
		};
		Debug.Log ("Soldier attributes: "+game.soldiers [AssigningSoldier - 1].attributes.ToString());
		if (j ["trainingSoldiers"].AsInt != 0) {
			for (var i = 0; i < 17; i++){
				dataHolder.GetChild(i*2+1).GetComponent<Text>().text = (Mathf.Round(j[valueList[i]].AsFloat*100)/100).ToString();
			}
		}
	}

	public static void OnReplaceItem(int armedType,string armedCategory){
		Dictionary<string,string> nameDict = new Dictionary<string, string> ();
		Dictionary<string,GameObject> panelDict = new Dictionary<string, GameObject> ();
		nameDict.Add ("weapon", "兵器");
		nameDict.Add ("armor", "防具");
		nameDict.Add ("shield", "盾");
		panelDict.Add ("weapon", SchoolField.staticArmyQAHolder);
		panelDict.Add ("armor", SchoolField.staticArmorQAHolder);
		panelDict.Add ("shield", SchoolField.staticShieldQAHolder);
		Game game = Game.Instance;
		string qText = "";
//		ProductDict p = ProductDict.Instance;
		staticShowPanel(panelDict [armedCategory]);

		if (armedCategory == "weapon"){
			qText = nameDict [armedCategory];
		}else if (armedCategory == "armor"){
			qText = nameDict [armedCategory];
		}else if (armedCategory == "shield"){
			qText =nameDict [armedCategory];
		}
		panelDict[armedCategory].transform.GetChild(0).GetComponent<Text>().text = qText;

		qText = msgEquipmentReplacement;
		if (armedCategory == "weapon") {
			if (game.soldiers[AssigningSoldier-1].attributes["weapon"].AsInt != AssigningWeaponId){
				qText = qText.Replace ("%Orig%", p.products [game.soldiers [SchoolField.AssigningSoldier - 1].attributes [armedCategory].AsInt].name);
				qText = qText.Replace ("%New%", p.products [armedType].name);
			}else{
				staticHidePanel( staticArmyQAHolder);
				return;
			}
		}else if (armedCategory == "armor"){
			if (game.soldiers[AssigningSoldier-1].attributes["armor"].AsInt != AssigningArmorId){
				qText = qText.Replace ("%Orig%", p.products [game.soldiers [SchoolField.AssigningSoldier - 1].attributes [armedCategory].AsInt].name);
				qText = qText.Replace ("%New%", p.products [armedType].name);
			}else{
				staticHidePanel( staticArmorQAHolder);
				return;
			}
		}else if (armedCategory == "shield"){
			if (game.soldiers[AssigningSoldier-1].attributes["shield"].AsInt != AssigningShieldId){
				qText = qText.Replace ("%Orig%", p.products [game.soldiers [SchoolField.AssigningSoldier - 1].attributes [armedCategory].AsInt].name);
				qText = qText.Replace ("%New%", p.products [armedType].name);
			}else{
				staticHidePanel( staticShieldQAHolder);
				return;
			}
		}
		panelDict[armedCategory].transform.GetChild(1).GetComponent<Text>().text = qText;
	}

	public void OnArmedReplacement(string category){
		Dictionary<string,GameObject> panelDict = new Dictionary<string, GameObject> ();
		panelDict.Add ("weapon", SchoolField.staticArmyQAHolder);
		panelDict.Add ("armor", SchoolField.staticArmorQAHolder);
		panelDict.Add ("shield", SchoolField.staticShieldQAHolder);
		if (category == "weapon") {
			game.soldiers[AssigningSoldier-1].attributes["weapon"].AsInt = AssigningWeaponId;
		}else if (category == "armor") {
			game.soldiers[AssigningSoldier-1].attributes["armor"].AsInt = AssigningArmorId;
		}else if (category == "shield") {
			game.soldiers[AssigningSoldier-1].attributes["shield"].AsInt = AssigningShieldId;
		}
		CheckArmedEquipmentAvailability ();
//		JSONClass json = new JSONClass ();
//		json.Add ("id", new JSONData (game.soldiers [AssigningSoldier - 1].id));
//		json.Add ("userId", new JSONData (game.login.id));
//		json.Add ("json", game.soldiers [AssigningSoldier - 1].attributes);
//		wsc.Send ("soldier", "SET", json);
		staticHidePanel(panelDict [category]);

	}

	void OnGoArtisanButtonClicked(){
//		ProductDict p = ProductDict.Instance;
		HidePanel( TrainingEquHolder);
//		ShowPanel( TrainingEquConfirmHolder);
		int count = 0;
		int quantity = 0;
		TimeSpan time = new TimeSpan();
		if (AssigningWeaponId != 0){
			if (game.artisans[0].etaTimestamp > DateTime.Now){
				HidePanel(TrainingEquHolder);
				ShowPanel( CannotTrainSoldierPopup);
				return;
			}
			count = game.weapon.Count;
			for (int i = 0 ; i< count ; i++){
				if(game.weapon[i].type == AssigningWeaponId){
					quantity = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt - game.weapon [i].quantity;
					AssigningResources = p.products[AssigningWeaponId].attributes["NumberOfProductionResources"].AsInt*quantity;
					time = new TimeSpan((long)p.products[AssigningWeaponId].attributes["ProductionTime"].AsFloat * quantity *10000*1000);
				}
			}
		}else if (AssigningArmorId != 0){ 
			if (game.artisans[1].etaTimestamp > DateTime.Now){
				HidePanel(TrainingEquHolder);
				ShowPanel(CannotTrainSoldierPopup);
				return;
			}
			count = game.armor.Count;
			for (int i = 0 ; i< count ; i++){
				if(game.armor[i].type == AssigningArmorId){
					quantity = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt - game.weapon [AssigningArmorId].quantity;
					AssigningResources = p.products[AssigningArmorId].attributes["NumberOfProductionResources"].AsInt*quantity;
					time = new TimeSpan((long)p.products[AssigningArmorId].attributes["ProductionTime"].AsFloat * quantity *10000*1000);
				}
			}
		}else if (AssigningShieldId != 0){ 
			if (game.artisans[2].etaTimestamp > DateTime.Now){
				HidePanel(TrainingEquHolder);
				ShowPanel(CannotTrainSoldierPopup);
				return;
			}
			count = game.shield.Count;
			for (int i = 0 ; i< count ; i++){
				if(game.shield[i].type == AssigningShieldId){
					quantity = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt - game.weapon [AssigningShieldId].quantity;
					AssigningResources = p.products[AssigningShieldId].attributes["NumberOfProductionResources"].AsInt*quantity;
					time = new TimeSpan((long)p.products[AssigningShieldId].attributes["ProductionTime"].AsFloat * quantity *10000*1000);
				}
			}
		}
		AssigningQuantity = quantity;
		AssigningTime = DateTime.Now.Add(time);
		string msg = msgQuestionConfirmProductionTimeForExtraEquipment;
		msg = msg.Replace ("%EQ%", quantity.ToString());
		msg = msg.Replace ("%SU%", AssigningResources.ToString());
//		Debug.Log (time.ToString ());
		if (time.TotalMinutes > 59) {
			msg = msg.Replace ("%TM%", Utilities.TimeUpdate.Time(time));
		} else {
			msg = msg.Replace ("%TM%", time.Minutes + ":" + time.Seconds);
		}
		TrainingEquConfirmHolder.transform.GetChild (1).GetComponent<Text> ().text = msg;
		ShowPanel( TrainingEquConfirmHolder);
	}

	void OnAddEquipmentConfirmed(){
		Text msgText = TrainingEquConfirmHolder.transform.GetChild (1).GetComponent<Text> ();
		if (msgText.text.Contains ("時之星塵不足")) {
			//TODO change this action of this condition to shop
			HidePanel(TrainingEquConfirmHolder);
		}
		if (!msgText.text.Contains("資源不足")&& !msgText.text.Contains ("時之星塵不足")){
		#region CheckResources
			if (game.wealth[2].value < AssigningResources){
				Debug.Log ("AssigningResources: "+AssigningResources);
				Debug.Log ("Resource own: "+game.wealth[2].value);
				string msg = "資源不足，要用%SD%時之星塵嗎？";
				AssigningStarDust = (int)(Mathf.Round((AssigningResources - game.wealth[2].value)*refStarDust/refResource));
				Debug.Log ("Need Stardust: "+AssigningStarDust);
					if (game.wealth[1].value < AssigningStarDust){
						msg = "時之星塵不足，請先購買時之星塵"; //TODO change the text to "Buy StarDust"
						TrainingEquConfirmHolder.transform.GetChild (1).GetComponent<Text> ().text = msg;
						return;
					}
				msg  = msg.Replace("%SD%",AssigningStarDust.ToString());
				TrainingEquConfirmHolder.transform.GetChild (1).GetComponent<Text> ().text = msg;
				ShowPanel( TrainingEquConfirmHolder);
				return;
			}
		}
		#endregion
		int type = 0;
		if (AssigningWeaponId != 0) {
			type = 1;
			game.artisans[0].targetId = AssigningWeaponId;
			game.soldiers [AssigningSoldier - 1].attributes.Add ("weaponProducing", new JSONData (true));
		} else if (AssigningArmorId != 0) {
			type = 2;
			game.artisans[1].targetId = AssigningArmorId;
			game.soldiers [AssigningSoldier - 1].attributes.Add ("armorProducing", new JSONData (true));
		} else if (AssigningShieldId != 0) {
			type = 3;
			game.artisans[2].targetId = AssigningShieldId;
			game.soldiers [AssigningSoldier - 1].attributes.Add ("shieldProducing", new JSONData (true));
		}
		game.artisans [type - 1].startTimestamp = DateTime.Now;
		game.artisans [type - 1].etaTimestamp = AssigningTime;
		game.artisans [type - 1].quantity = AssigningQuantity;
		game.artisans [type - 1].status = 4;

		TrainingEquConfirmHolder.transform.GetChild (1).GetComponent<Text> ().text = "";

		// TODO add the ETA time to soldiers if new one is end later
		game.soldiers [AssigningSoldier - 1].attributes.Add("eta_time",new JSONData(AssigningTime.ToString()));
		Utilities.ShowLog.Log (game.soldiers [AssigningSoldier - 1].attributes ["eta_time"]);
		if (game.soldiers [AssigningSoldier - 1].attributes ["eta_time"] != null) {
			if (Convert.ToDateTime(game.soldiers [AssigningSoldier - 1].attributes ["eta_time"]) > DateTime.Now){
				if (DateTime.Parse(game.soldiers [AssigningSoldier - 1].attributes ["eta_time"]) < new DateTime()/* TODO new ETA  */){
					game.soldiers [AssigningSoldier - 1].attributes.Add("eta_time",new JSONData(0)); // TODO change 0 to .NET DateTime in string format
				}
			}
		} else {
			game.soldiers [AssigningSoldier - 1].attributes.Add("eta_time",new JSONData(0)); // TODO change 0 to .NET DateTime in string format
		}
		game.artisans [type - 1].UpdateObject ();
		HidePanel(TrainingEquConfirmHolder);
		if (AssigningStarDust>0 ) {
			game.wealth [1].Deduct (AssigningStarDust);
			game.wealth [2].Set (0);
		} else {
			game.wealth [2].Deduct (AssigningResources);
		}


		AssigningResources = 0;
		AssigningStarDust = 0;
	}
	void OnSpeedUpProductionClicked(){
		HidePanel (TrainingEquConfirmHolder);
		TimeSpan ts = AssigningTime - DateTime.Now;
		var msg = "要用%sd%時之星塵加速製作嗎？";
		if (AssigningStarDust == 0) {
			AssigningStarDust = Utilities.ExchangeRate.GetStardustFromResource(AssigningResources)+(int)(ts.TotalHours * 10);
		} else {
			AssigningStarDust += (int)(ts.TotalHours * 10);
		}
		if (AssigningStarDust > game.wealth [1].value) {
			Debug.Log( "時之星塵: "+AssigningStarDust);
			msg = "時之星塵不足，請先購買";
			ConfirmSpeedUpHolder.transform.GetChild (1).GetComponent<Text> ().text = msg;
			ShowPanel (ConfirmSpeedUpHolder);
		}
		Debug.Log( "時之星塵: "+AssigningStarDust);
		msg = msg.Replace ("%sd%", AssigningStarDust.ToString ());
		ConfirmSpeedUpHolder.transform.GetChild (1).GetComponent<Text> ().text = msg;
		ShowPanel (ConfirmSpeedUpHolder);
	}

	void OnSpeedUpProductionConfirmed(){
		int index = 0;
		HidePanel (ConfirmSpeedUpHolder);
		if (AssigningStarDust > game.wealth [1].value) {
			return;
		}
		game.wealth[1].Deduct(AssigningStarDust);
		if (AssigningWeaponId > 0) {
			index = game.weapon.FindIndex (x => x.type == AssigningWeaponId);
			game.weapon [index].quantity += AssigningQuantity;
			game.weapon [index].UpdateObject ();
		} else if (AssigningArmorId > 0) {
			index = game.armor.FindIndex (x => x.type == AssigningArmorId);
			game.armor [index].quantity += AssigningQuantity;
			game.armor [index].UpdateObject ();
		} else if (AssigningShieldId > 0) {
			index = game.shield.FindIndex(x => x.type == AssigningShieldId);
			game.shield[index].quantity += AssigningQuantity;
			game.shield[index].UpdateObject();
		}
	}

	public static void ResetAssigningValue(){
		AssigningArmorId = 0;
		AssigningShieldId = 0;
		AssigningSoldier = 0;
		AssigningWeaponId = 0;
		AssigningResources = 0;
	}

	void SetAdjustSoldierValues(){
		Transform valueHolder		 = AdjustSoildersAttribute.transform.GetChild (1);
		Transform Hit				 = valueHolder.GetChild (0).GetChild (0).GetChild (0);
		Transform Dodge				 = valueHolder.GetChild (0).GetChild (0).GetChild (1);
		Transform Strength			 = valueHolder.GetChild (0).GetChild (0).GetChild (2);
		Transform AttackSpeed		 = valueHolder.GetChild (0).GetChild (0).GetChild (3);
		Transform Morale			 = valueHolder.GetChild (0).GetChild (0).GetChild (5);
		Transform Wand 				 = valueHolder.GetChild (0).GetChild (1).GetChild (0);
		Transform PiercingLongWeapon = valueHolder.GetChild (0).GetChild (1).GetChild (1);
		Transform Bows				 = valueHolder.GetChild (0).GetChild (1).GetChild (2);
		Transform HighEndBows		 = valueHolder.GetChild (0).GetChild (1).GetChild (3);
		Transform HiddenWeapon		 = valueHolder.GetChild (0).GetChild (1).GetChild (4);
		Transform Knife				 = valueHolder.GetChild (0).GetChild (1).GetChild (5);
		Transform HackTypeLongWeapon = valueHolder.GetChild (0).GetChild (2).GetChild (0);
		Transform Sword				 = valueHolder.GetChild (0).GetChild (2).GetChild (1);
		Transform HighEndSword		 = valueHolder.GetChild (0).GetChild (2).GetChild (2);
		Transform SpecialWeapon		 = valueHolder.GetChild (0).GetChild (2).GetChild (3);
		Transform Axe				 = valueHolder.GetChild (0).GetChild (2).GetChild (4);
		Transform Hammer			 = valueHolder.GetChild (0).GetChild (2).GetChild (5);

		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		Hit.GetChild (1).GetChild (1).GetComponent<Slider> ().value					= Mathf.Round(j ["Hit"].AsFloat*100)/100;
		Dodge.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Dodge"].AsFloat*100)/100;
		Strength.GetChild (1).GetChild (1).GetComponent<Slider> ().value			= Mathf.Round(j ["Strength"].AsFloat*100)/100;
		AttackSpeed.GetChild (1).GetChild (1).GetComponent<Slider> ().value			= Mathf.Round(j ["AttackSpeed"].AsFloat*100)/100;
		Morale.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Morale"].AsFloat*100)/100;
		Wand.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Wand"].AsFloat*100)/100;
		PiercingLongWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value	= Mathf.Round(j ["PiercingLongWeapon"].AsFloat*100)/100;
		Bows.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Bows"].AsFloat*100)/100;
		HighEndBows.GetChild (1).GetChild (1).GetComponent<Slider> ().value			= Mathf.Round(j ["HighEndBows"].AsFloat*100)/100;
		HiddenWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value		= Mathf.Round(j ["HiddenWeapon"].AsFloat*100)/100;
		Knife.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Knife"].AsFloat*100)/100;
		HackTypeLongWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value	= Mathf.Round(j ["HackTypeLongWeapon"].AsFloat*100)/100;
		Sword.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Sword"].AsFloat*100)/100;
		HighEndSword.GetChild (1).GetChild (1).GetComponent<Slider> ().value		= Mathf.Round(j ["HighEndSword"].AsFloat*100)/100;
		SpecialWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value		= Mathf.Round(j ["SpecialWeapon"].AsFloat*100)/100;
		Axe.GetChild (1).GetChild (1).GetComponent<Slider> ().value					= Mathf.Round(j ["Axe"].AsFloat*100)/100;
		Hammer.GetChild (1).GetChild (1).GetComponent<Slider> ().value				= Mathf.Round(j ["Hammer"].AsFloat*100)/100;
	}

	void GetAdjustedSoldierValues(){
		Game game = Game.Instance;
		Transform valueHolder		 = AdjustSoildersAttribute.transform.GetChild (1);
		Transform Hit				 = valueHolder.GetChild (0).GetChild (0).GetChild (0);
		Transform Dodge				 = valueHolder.GetChild (0).GetChild (0).GetChild (1);
		Transform Strength			 = valueHolder.GetChild (0).GetChild (0).GetChild (2);
		Transform AttackSpeed		 = valueHolder.GetChild (0).GetChild (0).GetChild (3);
		Transform Morale			 = valueHolder.GetChild (0).GetChild (0).GetChild (5);
		Transform Wand 				 = valueHolder.GetChild (0).GetChild (1).GetChild (0);
		Transform PiercingLongWeapon = valueHolder.GetChild (0).GetChild (1).GetChild (1);
		Transform Bows				 = valueHolder.GetChild (0).GetChild (1).GetChild (2);
		Transform HighEndBows		 = valueHolder.GetChild (0).GetChild (1).GetChild (3);
		Transform HiddenWeapon		 = valueHolder.GetChild (0).GetChild (1).GetChild (4);
		Transform Knife				 = valueHolder.GetChild (0).GetChild (1).GetChild (5);
		Transform HackTypeLongWeapon = valueHolder.GetChild (0).GetChild (2).GetChild (0);
		Transform Sword				 = valueHolder.GetChild (0).GetChild (2).GetChild (1);
		Transform HighEndSword		 = valueHolder.GetChild (0).GetChild (2).GetChild (2);
		Transform SpecialWeapon		 = valueHolder.GetChild (0).GetChild (2).GetChild (3);
		Transform Axe				 = valueHolder.GetChild (0).GetChild (2).GetChild (4);
		Transform Hammer			 = valueHolder.GetChild (0).GetChild (2).GetChild (5);

		int trainingSoldiers = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt;
		TotalTrainingTime = 0;
		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		TotalTrainingTime += CalculateSoldierTrainingTime("Hit",trainingSoldiers,Hit.GetChild (1).GetChild (1).GetComponent<Slider> ().value- Mathf.Round(j ["Hit"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Dodge",trainingSoldiers,Dodge.GetChild (1).GetChild (1).GetComponent<Slider> ().value	-Mathf.Round(j ["Dodge"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Strength",trainingSoldiers,Strength.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Strength"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("AttackSpeed",trainingSoldiers,AttackSpeed.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["AttackSpeed"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Morale",trainingSoldiers,Morale.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Morale"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Wand",trainingSoldiers,Wand.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Wand"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("PiercingLongWeapon",trainingSoldiers,PiercingLongWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["PiercingLongWeapon"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Bows",trainingSoldiers,Bows.GetChild (1).GetChild (1).GetComponent<Slider> ().value- Mathf.Round(j ["Bows"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("HighEndBows",trainingSoldiers,HighEndBows.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["HighEndBows"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("HiddenWeapon",trainingSoldiers,HiddenWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["HiddenWeapon"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Knife",trainingSoldiers,Knife.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Knife"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("HackTypeLongWeapon",trainingSoldiers,HackTypeLongWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["HackTypeLongWeapon"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Sword",trainingSoldiers,Sword.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Sword"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("HighEndSword",trainingSoldiers,HighEndSword.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["HighEndSword"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("SpecialWeapon",trainingSoldiers,SpecialWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["SpecialWeapon"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Axe",trainingSoldiers,Axe.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Axe"].AsFloat*100)/100);
		TotalTrainingTime += CalculateSoldierTrainingTime("Hammer",trainingSoldiers,Hammer.GetChild (1).GetChild (1).GetComponent<Slider> ().value - Mathf.Round(j ["Hammer"].AsFloat*100)/100);
		Debug.Log (TotalTrainingTime);
		if (TotalTrainingTime > 0) {
			HidePanel(AdjustSoildersAttribute);
			ShowConfirmTrainingPanel(trainingSoldiers);

		} else {
			HidePanel(AdjustSoildersAttribute);
		}

	}

	int CalculateSoldierTrainingTime(string type, int NumOfSoldiers, float AbilityToBeTrained){
		int session = Mathf.RoundToInt ((float)NumOfSoldiers/1000);
		int time = 0;
		Dictionary<string,float> trainParam= new Dictionary<string, float> ();
		trainParam.Add ("Hit", 0.8f);
		trainParam.Add ("Dodge", 0.7f);
		trainParam.Add ("Strength", 0.3f);
		trainParam.Add ("AttackSpeed", 0.05f);
		trainParam.Add ("Morale", 0.7f);
		trainParam.Add ("Wand", 0.95f);
		trainParam.Add ("PiercingLongWeapon", 0.7f);
		trainParam.Add ("Bows", 0.5f);
		trainParam.Add ("HighEndBows", 0.3f);
		trainParam.Add ("HiddenWeapon", 0.2f);
		trainParam.Add ("Knife", 0.75f);
		trainParam.Add ("HackTypeLongWeapon", 0.7f);
		trainParam.Add ("Sword", 0.75f);
		trainParam.Add ("HighEndSword", 0.5f);
		trainParam.Add ("SpecialWeapon", 0.01f);
		trainParam.Add ("Axe", 0.7f);
		trainParam.Add ("Hammer", 0.7f);;

		time = Mathf.RoundToInt (AbilityToBeTrained*session/trainParam[type]);
		return time*3600; //Return as number of secounds
	}

	void ShowConfirmTrainingPanel(int numOfSoldiers){
		string msg = msgQuestionConfirmTimeForTrainingSOldiers;
		TimeSpan time = new TimeSpan ((long)SchoolField.TotalTrainingTime *1000*10000);
		msg = msg.Replace ("%SQ%", numOfSoldiers.ToString ());
		Debug.Log (time.ToString ());
		msg = msg.Replace ("%TT%", Utilities.TimeUpdate.Time (time));
		TrainingQAHolder.transform.GetChild (1).GetComponent<Text> ().text = msg;
		ShowPanel(TrainingQAHolder);
	}

	void ConfirmedTraining(){
		Transform valueHolder		 = AdjustSoildersAttribute.transform.GetChild (1);
		Transform Hit				 = valueHolder.GetChild (0).GetChild (0).GetChild (0);
		Transform Dodge				 = valueHolder.GetChild (0).GetChild (0).GetChild (1);
		Transform Strength			 = valueHolder.GetChild (0).GetChild (0).GetChild (2);
		Transform AttackSpeed		 = valueHolder.GetChild (0).GetChild (0).GetChild (3);
		Transform Morale			 = valueHolder.GetChild (0).GetChild (0).GetChild (5);
		Transform Wand 				 = valueHolder.GetChild (0).GetChild (1).GetChild (0);
		Transform PiercingLongWeapon = valueHolder.GetChild (0).GetChild (1).GetChild (1);
		Transform Bows				 = valueHolder.GetChild (0).GetChild (1).GetChild (2);
		Transform HighEndBows		 = valueHolder.GetChild (0).GetChild (1).GetChild (3);
		Transform HiddenWeapon		 = valueHolder.GetChild (0).GetChild (1).GetChild (4);
		Transform Knife				 = valueHolder.GetChild (0).GetChild (1).GetChild (5);
		Transform HackTypeLongWeapon = valueHolder.GetChild (0).GetChild (2).GetChild (0);
		Transform Sword				 = valueHolder.GetChild (0).GetChild (2).GetChild (1);
		Transform HighEndSword		 = valueHolder.GetChild (0).GetChild (2).GetChild (2);
		Transform SpecialWeapon		 = valueHolder.GetChild (0).GetChild (2).GetChild (3);
		Transform Axe				 = valueHolder.GetChild (0).GetChild (2).GetChild (4);
		Transform Hammer			 = valueHolder.GetChild (0).GetChild (2).GetChild (5);
		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		j.Add ("TargetHit",new JSONData(Hit.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetDodge",new JSONData(Dodge.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetStrength",new JSONData(Strength.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetAttackSpeed",new JSONData(AttackSpeed.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetMorale",new JSONData(Morale.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetWand",new JSONData(Wand.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetPiercingLongWeapon",new JSONData(PiercingLongWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetBows",new JSONData(Bows.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetHighEndBows",new JSONData(HighEndBows.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetHiddenWeapon",new JSONData(HiddenWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetKnife",new JSONData(Knife.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetHackTypeLongWeapon",new JSONData(HackTypeLongWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetSword",new JSONData(Sword.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetHighEndSword",new JSONData(HighEndSword.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetSpecialWeapon",new JSONData(SpecialWeapon.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetAxe",new JSONData(Axe.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		j.Add ("TargetHammer",new JSONData(Hammer.GetChild (1).GetChild (1).GetComponent<Slider> ().value));
		DateTime d = DateTime.Now;
		j.Add ("ETATrainingTime", new JSONData (d.Add (new TimeSpan((long)TotalTrainingTime*1000*10000)).ToString()));

		game.soldiers [AssigningSoldier - 1].UpdateObject ();
		HidePanel(TrainingQAHolder);
	}

	void CancelConfirmTraining(){
		TotalTrainingTime = 0;
		SetAdjustSoldierValues ();
		HidePanel(TrainingQAHolder);
	}

	bool CheckIfTrainingOngoing(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"] != null) {
//			Debug.Log ("There is a training for this soldier type");
//			Debug.Log (Convert.ToDateTime( game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"]).ToString());
//			Debug.Log (DateTime.Now.ToString());
			if (Convert.ToDateTime (game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"]) < DateTime.Now) {
				return true;
			} else {
				return false;
			}
		} else {
			return false;
		}
	}

	void UpdateSoldierSummaryPanel(){
		string data = "";
		CheckIfTrainingOngoing ();
		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		data += "\n名稱： 兵種" + AssigningSoldier.ToString ();
		if (j ["weapon"].AsInt != 0) {
			data += "\n兵器： " + p.products [j ["weapon"].AsInt].name;
		} else {
			data += "\n兵器： 未指派";
		}
		if (j ["armor"].AsInt != 0) {
			data += "\n防具： " + p.products [j ["armor"].AsInt].name;
		} else {
			data += "\n防具： 未指派";
		}
		if (j ["shield"].AsInt != 0) {
			data += "\n 盾： " + p.products[ j["shield"].AsInt].name;
		} else {
			data += "\n 盾： 未指派";
		}
		data += "\n已訓練兵數： " + game.soldiers [AssigningSoldier - 1].quantity;
		data += "\n未訓練兵數： " + j["trainingSoldiers"];

		if (j ["ETATrainingTime"] != null) {
			if (Convert.ToDateTime( j ["ETATrainingTime"]) >= DateTime.Now) {  //Target time should be larger than now
				data += "\n訓練時間： " + Utilities.TimeUpdate.Time(Convert.ToDateTime( j ["ETATrainingTime"]));
				if (Convert.ToDateTime( j ["ETATrainingTime"]) == DateTime.Now){
					SetDataPanel();
				}
			}
		}
		SoldierSummary.text = data;
	}

	static void staticShowPanel(GameObject panel){
		staticDisablePanel.SetActive (true);
		panel.SetActive (true);
	}

	static void staticHidePanel(GameObject panel){
		staticDisablePanel.SetActive (false);
		panel.SetActive (false);
	}

	void ShowPanel(GameObject panel){
		DisablePanel.SetActive (true);
		Debug.Log ("ShowPanel");
		panel.SetActive (true);
	}
	
	void HidePanel(GameObject panel){
		DisablePanel.SetActive (false);
		Debug.Log ("HidePanel");
		panel.SetActive (false);
	}

	void ChangePanel(GameObject panel1, GameObject panel2){
		Debug.Log (panel1 + " panel is change to " + panel2);
		panel1.SetActive (false);
		panel2.SetActive (true);
	}

}

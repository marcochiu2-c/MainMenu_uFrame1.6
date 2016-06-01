using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using Utilities;
using uFrame.Kernel;

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
	public GameObject TrainingInProgressPopup;
	public GameObject ConfirmSpeedUpHolder;
	public GameObject ArtisanHolder;
	public GameObject UnmountWeaponPanel;
	public InputField TrainingQText;
	public Button UnmountConfirm;
	public Toggle UnmountAllToggle;
	public Toggle UnmountWeaponToggle;
	public Toggle UnmountArmorToggle;
	public Toggle UnmountShieldToggle;

	bool isSpeedUpQuestion = false;

	public static GameObject staticArmyQAHolder;
	public static GameObject staticArmorQAHolder;
	public static GameObject staticShieldQAHolder;
	public static GameObject staticTrainingEquHolder;
	public static GameObject staticDisablePanel;

	public static Toggle staticUnmountAllToggle;
	public static Toggle staticUnmountWeaponToggle;
	public static Toggle staticUnmountArmorToggle;
	public static Toggle staticUnmountShieldToggle;


	public static int AssigningWeaponId=0;
	public static int AssigningArmorId=0;
	public static int AssigningShieldId=0;
	public static int AssigningSoldier=1;  // 兵種
	public static int AssigningNewSoldiers = 0; // Number of new soldiers to be trained
	public static int AssigningResources=0;
	public static int AssigningStarDust=0;
	public static DateTime AssigningTime;
	public static int AssigningQuantity = 0;
	public static float TotalTrainingTime=0;
	public static int refStarDust = 80;
	public static int refResource =160000;
	
//	public static int refFeather  = 400;
	Text SoldierSummary;
	static ProductDict p;
	bool isNewSoldiers = false;

	#region stringDefinition
	static string msgExistingSoldierQuantity= "現有兵數："; 
	static string msgSoldierQuantityWantToTrain = "請輸入欲訓練士兵數目";
	static string msgWarningSOldierQuantiyOverQuota = "<color=red>輸入兵數不能大於現有兵數</color>";
	static string msgExtraEquipmentRequired = "訓練%SQ%個士兵需要製造額外%EQ%的裝備，找工匠嗎？";
	static string msgExtraEquipmentRequiredForNewSoldiers = "裝備不足，請先找工匠。";
	static string msgEquipmentReplacement = "確定更改%Orig%為%New%嗎？";
	static string msgQuestionConfirmProductionTimeForExtraEquipment = "額外%EQ%的裝備需要%SU%物資和時間%TM%，立刻製作嗎？";
	static string msgQuestionConfirmTimeForTrainingSOldiers = "訓練%SQ%的士兵需時%TT%，確定訓練嗎？";
	static string msgSpeedUpTrainingConfirmation ="軍師閣下，可以用%sd%時之星塵加速訓練，確認？";
	static string msgShowConfirmNewSoldierTraining = "軍師閣下，訓練 %soldier% 新兵需要 %time% 時間，確認？";
	static string headerShowConfirmNewSoldierTraining = "新兵訓練";
	static string headerTrainingQAHolder = "訓練";
	static string msgCannotTrainSoldier = "軍師閣下，工匠正在忙碌，未能製造裝備，致未能訓練新兵，請先等工匠空閒時再來！";
	static string headerCannotTrainSoldier = "未能訓練新兵";
	static string headerNotEnoughEquipementForNewSoldiers = "裝備不足";
	static string msgNotEnoughEquipementForNewSoldiers = "軍師閣下，下列裝備不足，\n請找工匠補充\n";
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
		AddButtonListener ();
		staticArmyQAHolder = ArmyQAHolder;
		staticArmorQAHolder = ArmorQAHolder;
		staticShieldQAHolder = ShieldQAHolder;
		staticTrainingEquHolder = TrainingEquHolder;
		staticDisablePanel = DisablePanel;

		staticUnmountAllToggle = UnmountAllToggle;
		staticUnmountWeaponToggle = UnmountWeaponToggle;
		staticUnmountArmorToggle = UnmountArmorToggle;
		staticUnmountShieldToggle = UnmountShieldToggle;

		DisablePanel.SetActive (false);
		SoldierSummary = DollPanel.transform.GetChild (2).GetComponent<Text>();
		SetDataPanel ();
		SetAdjustSoldierValues ();
		p = ProductDict.Instance;

	}

	void OnEnable(){
		InvokeRepeating ("ShowTotalSoldiersAvailableText", 0, 60);
		InvokeRepeating ("UpdateSoldierSummaryPanel", 0, 1);
		InvokeRepeating("SetDataPanel",0.5f,1);
	}
	
	void OnDisable(){
		CancelInvoke ();
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
				TrainingQText.text = "";
			}
		});
		// Weapon Button
		DollPanel.transform.GetChild (0).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			OnWeaponButtonClicked (); });
		// Speed Up Button for Training
		DollPanel.transform.GetChild (0).GetChild (2).GetComponent<Button> ().onClick.AddListener (() => {
			TimeSpan trainTime = Convert.ToDateTime (game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"]) - DateTime.Now;
			if (ExchangeRate.GetStardustFromTime (trainTime) <= game.wealth[1].value){
				OnSpeedUpTrainingClicked();
			}
			 });
		// Armor Button
		DollPanel.transform.GetChild (1).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			DisablePanel.SetActive(true);
			OnArmorButtonClicked (); });
		// Shield Button
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
		Panel.GetConfirmButton(TrainingQHolder).onClick.AddListener(() => {  //confirm
			Debug.Log ("Soldier number assignment confirmed.");
			HidePanel(TrainingQHolder);
//			TrainingQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text = "";
			OnSetNewSoldierNumber();
		});
		Panel.GetCancelButton(TrainingQHolder).onClick.AddListener(() => {  //cancel
			HidePanel(TrainingQHolder);
			TrainingQHolder.transform.GetChild(1).GetChild(0).GetComponent<InputField>().text = "";
		});
		
		TrainingEquHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { 
			Debug.Log("isNewSoldiers: "+isNewSoldiers);
//			if (!isNewSoldiers){
//				OnGoArtisanButtonClicked();
//			}else{
//				isNewSoldiers = false;
//				Panel.GetCancelButton(TrainingEquHolder).transform.GetChild(0).GetComponent<Text>().text = "變更裝備";
//				HidePanel(TrainingEquHolder);
//				var evt = new RequestMainMenuScreenCommand();
//				evt.ScreenType = typeof(ArtisanScreenViewModel);
//				var uFC = new uFrameComponent();
//				uFC.Publish(evt);
//			}
			isNewSoldiers = false;
			Panel.GetCancelButton(TrainingEquHolder).transform.GetChild(0).GetComponent<Text>().text = "變更裝備";
			HidePanel(TrainingEquHolder);
			var evt = new RequestMainMenuScreenCommand();
			evt.ScreenType = typeof(ArtisanScreenViewModel);
			var uFC = new uFrameComponent();
			uFC.Publish(evt);
		});
		TrainingEquHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
			Panel.GetCancelButton(TrainingEquHolder).transform.GetChild(0).GetComponent<Text>().text = "變更裝備";
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
		Utilities.Panel.GetConfirmButton (AssignNSPopup).onClick.AddListener (() => {
			HidePanel(AssignNSPopup);
		});
		Panel.GetConfirmButton(TrainingQAHolder).onClick.AddListener (() => {
			if (Panel.GetHeader(TrainingQAHolder).text == headerTrainingQAHolder){
				if (isSpeedUpQuestion){
					ConfirmedSpeedUpTraining();
				}else{
					OnConfirmedNewSoldierTraining();
				}
				isSpeedUpQuestion = false;
			}
		});
		Panel.GetCancelButton(TrainingQAHolder).onClick.AddListener (() => {
			if (Panel.GetHeader(TrainingQAHolder).text == headerTrainingQAHolder){
				if (isSpeedUpQuestion){
					HidePanel (TrainingQAHolder);
				}else{
					CancelConfirmTraining();
				}
				isSpeedUpQuestion = false;
			}
		});
		Panel.GetConfirmButton(CannotTrainSoldierPopup).onClick.AddListener(() =>{
			HidePanel(CannotTrainSoldierPopup);
			TrainingQText.text = "";
		});
		UnmountConfirm.onClick.AddListener (() => {
			if (UnmountAllToggle.isOn){
				game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt = 0;
				game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt = 0;
				game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt = 0;
			}else if (UnmountWeaponToggle.isOn){
				game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt = 0;
			}else if (UnmountArmorToggle.isOn){
				game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt = 0;
			}else if (UnmountShieldToggle.isOn){
				game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt = 0;
			}
			game.soldiers[AssigningSoldier - 1].UpdateObject();
			UnmountAllToggle.isOn=false;
			UnmountWeaponToggle.isOn=false;
			UnmountArmorToggle.isOn=false;
			UnmountShieldToggle.isOn=false;
			HidePanel(UnmountWeaponPanel);
		});
	}

	void ConfirmedSpeedUpTraining(){
		Debug.Log ("Speed up Training Confirmed");

		CompletingTrainingSoldiers (AssigningSoldier - 1);
		TimeSpan trainTime = Convert.ToDateTime (game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"]) - DateTime.Now;
		Debug.Log (AssigningStarDust);
		game.wealth [1].Deduct (AssigningStarDust);
		HidePanel (TrainingQAHolder);
	}

	void OnSpeedUpTrainingClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"] != null) {
			isSpeedUpQuestion = true;
			TimeSpan trainTime = Convert.ToDateTime (game.soldiers [AssigningSoldier - 1].attributes ["ETATrainingTime"]) - DateTime.Now;
			string msg = msgSpeedUpTrainingConfirmation;
			int cost = ExchangeRate.GetStardustFromTime (trainTime);
			ShowPanel (TrainingQAHolder);
			AssigningStarDust = cost;
			Panel.GetHeader (TrainingQAHolder).text = headerTrainingQAHolder;
			Panel.GetMessageText (TrainingQAHolder).text = msg.Replace ("%sd%", cost.ToString ());

			

		}
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
		return ((int)DateTime.Now.Subtract (rt).TotalMinutes) * 3;
	}
	#endregion

	public void OnSetNewSoldierNumber(){
		InputField s = TrainingQText;
		string soldiers = Regex.Replace(s.text, "[^0-9]", "");
		if (soldiers != "") {
			int soldierQuantity = Int32.Parse (soldiers);
			if (soldierQuantity > TotalSoldiersAvailable ()) {
				ShowPanel(TrainingQHolder);
				Panel.GetMessageText (TrainingQHolder).text=msgWarningSOldierQuantiyOverQuota+"\n"+msgSoldierQuantityWantToTrain;
				Debug.Log ("Number of new soldiers cannot be larger than available, inputed: " + soldierQuantity);
			} else {   //Valid number
				Panel.GetMessageText (TrainingQHolder).text= msgSoldierQuantityWantToTrain;
				HidePanel( TrainingQHolder);
				ShowConfirmNewSoldierTraining(soldierQuantity);

			}
		} else {
			return;
		}
	}


	void ShowConfirmNewSoldierTraining(int soldierQuantity){Debug.Log ("ShowConfirmNewSoldierTraining()");
		var p = ProductDict.Instance;
		string msg = "";
//		if (CheckArmedEquipmentAvailabilityForNewSoldiers (soldierQuantity)) {
		float time = CalculateTrainingTimeForNewSoldiers (soldierQuantity);
		game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt = soldierQuantity;
		msg = msgShowConfirmNewSoldierTraining;
		msg = msg.Replace ("%soldier%", soldierQuantity.ToString ());
		msg = msg.Replace ("%time%", Utilities.TimeUpdate.Time (new TimeSpan (0, 0, (int)time)));
		if (time > 10) {
			Panel.GetMessageText (TrainingQAHolder).text = msg;
			Panel.GetHeader (TrainingQAHolder).text = headerTrainingQAHolder;
			ShowPanel (TrainingQAHolder);
		} else {
			OnConfirmedNewSoldierTraining();
		}
//		} else {
//			// TODO show error message and 
//			Panel.GetHeader(CannotTrainSoldierPopup).text = headerNotEnoughEquipementForNewSoldiers;
//			msg = msgNotEnoughEquipementForNewSoldiers;
//			Debug.Log("Enough Weapon: "+(soldierQuantity > game.weapon.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt).quantity));
//			if (soldierQuantity > game.weapon.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt).quantity){
//				msg += p.products[game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt].name+"："+
//					(soldierQuantity - game.weapon.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt).quantity).ToString()+"\n";
//			}
//			Debug.Log("Enough Armor: "+(soldierQuantity > game.armor.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt).quantity));
//			if (soldierQuantity > game.armor.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt).quantity){
//				msg += p.products[game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt].name+"："+
//					(soldierQuantity - game.armor.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt).quantity).ToString()+"\n";
//			}
//			if (soldierQuantity > game.shield.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt).quantity){
//				msg += p.products[game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt].name+"："+
//					(soldierQuantity - game.shield.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt).quantity).ToString()+"\n";
//			}
//
//			Panel.GetMessageText(TrainingEquHolder).text = msg;
//			Panel.GetCancelButton(TrainingEquHolder).transform.GetChild(0).GetComponent<Text>().text = "取消";
//			isNewSoldiers = true;
//			ShowPanel(TrainingEquHolder);
//		}
	}

	void OnConfirmedNewSoldierTraining(){
		int soldiers = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt;
		Debug.Log ("trainingSoldiers: "+game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt);
		game.login.attributes ["TotalDeductedSoldiers"].AsInt = game.login.attributes ["TotalDeductedSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt;
		game.login.UpdateObject ();
		ConfirmedTrainingForNewSoldiers ();
		ShowTotalSoldiersAvailableText();
		SetDataPanel();
		TrainingQText.text = "";
//		int index = game.weapon.FindIndex (x => x.type == game.soldiers [AssigningSoldier - 1].attributes ["weapon"].AsInt);
//		game.weapon[index].quantity -= soldiers;
//		game.weapon [index].UpdateObject ();
//		index = game.armor.FindIndex (x => x.type == game.soldiers [AssigningSoldier - 1].attributes ["armor"].AsInt);
//		game.armor[index].quantity -= soldiers;
//		game.armor [index].UpdateObject ();
//		index = game.shield.FindIndex (x => x.type == game.soldiers [AssigningSoldier - 1].attributes ["shield"].AsInt);
//		game.shield[index].quantity -= soldiers;
//		game.shield [index].UpdateObject ();

		int soldierQuantity = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt;
		string msg = "";
		if (!CheckArmedEquipmentAvailabilityForNewSoldiers (soldierQuantity)) {
			// TODO show error message and 
			Panel.GetHeader(CannotTrainSoldierPopup).text = headerNotEnoughEquipementForNewSoldiers;
			msg = msgNotEnoughEquipementForNewSoldiers;
			if (game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt > 0){
				if (soldierQuantity > game.weapon.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt).quantity){
					msg += p.products[game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt].name+"："+
						(soldierQuantity - game.weapon.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["weapon"].AsInt).quantity).ToString()+"\n";
				}
			}
			if (game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt > 0){
				if (soldierQuantity > game.armor.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt).quantity){
					msg += p.products[game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt].name+"："+
						(soldierQuantity - game.armor.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["armor"].AsInt).quantity).ToString()+"\n";
				}
			}
			if (game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt > 0){
				if (soldierQuantity > game.shield.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt).quantity){
					msg += p.products[game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt].name+"："+
						(soldierQuantity - game.shield.Find (x => x.type == game.soldiers[AssigningSoldier - 1].attributes["shield"].AsInt).quantity).ToString()+"\n";
				}
			}
			if (msg != msgNotEnoughEquipementForNewSoldiers){
				Panel.GetMessageText(TrainingEquHolder).text = msg;
				Panel.GetCancelButton(TrainingEquHolder).transform.GetChild(0).GetComponent<Text>().text = "取消";
				isNewSoldiers = true;
				ShowPanel(TrainingEquHolder);
			}
		}
	}

	void ConfirmedTrainingForNewSoldiers(){
		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		j.Add ("TargetHit",new JSONData(j["Hit"].AsFloat));
		j.Add ("TargetDodge",new JSONData(j["Dodge"].AsFloat));
		j.Add ("TargetStrength",new JSONData(j["Strength"].AsFloat));
		j.Add ("TargetAttackSpeed",new JSONData(j["AttackSpeed"].AsFloat));
		j.Add ("TargetMorale",new JSONData(j["Morale"].AsFloat));
		j.Add ("TargetWand",new JSONData(j["Wand"].AsFloat));
		j.Add ("TargetPiercingLongWeapon",new JSONData(j["PiercingLongWeapon"].AsFloat));
		j.Add ("TargetBows",new JSONData(j["Bows"].AsFloat));
		j.Add ("TargetHighEndBows",new JSONData(j["HighEndBows"].AsFloat));
		j.Add ("TargetHiddenWeapon",new JSONData(j["HiddenWeapon"].AsFloat));
		j.Add ("TargetKnife",new JSONData(j["Knife"].AsFloat));
		j.Add ("TargetHackTypeLongWeapon",new JSONData(j["HackTypeLongWeapon"].AsFloat));
		j.Add ("TargetSword",new JSONData(j["Sword"].AsFloat));
		j.Add ("TargetHighEndSword",new JSONData(j["HighEndSword"].AsFloat));
		j.Add ("TargetSpecialWeapon",new JSONData(j["SpecialWeapon"].AsFloat));
		j.Add ("TargetAxe",new JSONData(j["Axe"].AsFloat));
		j.Add ("TargetHammer",new JSONData(j["Hammer"].AsFloat));
		DateTime d = DateTime.Now;
		j.Add ("ETATrainingTime", new JSONData (d.Add (new TimeSpan(0,0,(int)TotalTrainingTime)).ToString()));
		
		game.soldiers [AssigningSoldier - 1].UpdateObject ();
		HidePanel(TrainingQAHolder);
	}

	float CalculateTrainingTimeForNewSoldiers(int soldiers){
		JSONClass j = game.soldiers [AssigningSoldier - 1].attributes;
		TotalTrainingTime = 0;
		TotalTrainingTime += CalculateSoldierTrainingTime("Hit", soldiers, j["Hit"].AsFloat - 30);
		TotalTrainingTime += CalculateSoldierTrainingTime("Dodge", soldiers, j ["Dodge"].AsFloat - 20);
		TotalTrainingTime += CalculateSoldierTrainingTime("Strength", soldiers, j ["Strength"].AsFloat - 50);
		TotalTrainingTime += CalculateSoldierTrainingTime("AttackSpeed", soldiers, j ["AttackSpeed"].AsFloat - 1);
		TotalTrainingTime += CalculateSoldierTrainingTime("Morale", soldiers, j ["Morale"].AsFloat - 50);
		TotalTrainingTime += CalculateSoldierTrainingTime("Wand", soldiers, j ["Wand"].AsFloat - 10);
		TotalTrainingTime += CalculateSoldierTrainingTime("PiercingLongWeapon", soldiers, j ["PiercingLongWeapon"].AsFloat - 3);
		TotalTrainingTime += CalculateSoldierTrainingTime("Bows", soldiers, j ["Bows"].AsFloat - 3);
		TotalTrainingTime += CalculateSoldierTrainingTime("HighEndBows", soldiers, j ["HighEndBows"].AsFloat - 1);
		TotalTrainingTime += CalculateSoldierTrainingTime("HiddenWeapon", soldiers, j ["HiddenWeapon"].AsFloat - 1);
		TotalTrainingTime += CalculateSoldierTrainingTime("Knife", soldiers, j ["Knife"].AsFloat - 8);
		TotalTrainingTime += CalculateSoldierTrainingTime("HackTypeLongWeapon", soldiers, j ["HackTypeLongWeapon"].AsFloat - 3);
		TotalTrainingTime += CalculateSoldierTrainingTime("Sword", soldiers, j ["Sword"].AsFloat - 5);
		TotalTrainingTime += CalculateSoldierTrainingTime("HighEndSword", soldiers, j ["HighEndSword"].AsFloat);
		TotalTrainingTime += CalculateSoldierTrainingTime("SpecialWeapon", soldiers, j ["SpecialWeapon"].AsFloat);
		TotalTrainingTime += CalculateSoldierTrainingTime("Axe", soldiers, j ["Axe"].AsFloat - 3);
		TotalTrainingTime += CalculateSoldierTrainingTime("Hammer", soldiers, j ["Hammer"].AsFloat - 4);
		return TotalTrainingTime;
	}

	public bool CheckArmedEquipmentAvailabilityForNewSoldiers(int soldierQuantity){
		Game game = Game.Instance;
		string msg = msgExtraEquipmentRequiredForNewSoldiers;
		if (game.soldiers [AssigningSoldier - 1].attributes ["weapon"].AsInt > 0 ||
			game.soldiers [AssigningSoldier - 1].attributes ["armor"].AsInt > 0 ||
			game.soldiers [AssigningSoldier - 1].attributes ["shield"].AsInt > 0) {
			if (soldierQuantity < game.weapon.Find (x => x.type == game.soldiers [AssigningSoldier - 1].attributes ["weapon"].AsInt).quantity) {
				if (soldierQuantity < game.armor.Find (x => x.type == game.soldiers [AssigningSoldier - 1].attributes ["armor"].AsInt).quantity) {
					if (soldierQuantity < game.shield.Find (x => x.type == game.soldiers [AssigningSoldier - 1].attributes ["shield"].AsInt).quantity) {
						return true;
					}
				}
			}
		}
		return false;
	}

	public static void CheckArmedEquipmentAvailability(){
		staticDisablePanel.SetActive (false);
		Debug.Log ("CheckArmedEquipmentAvailability()");
		Game game = Game.Instance;
		string msg = msgExtraEquipmentRequired;
		int numOfSoldiers = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity;
		var count = 0;
		if (AssigningWeaponId != 0) {
			count = game.weapon.Count;
			for (int i = 0 ; i< count ; i++){
				if(game.weapon[i].type == AssigningWeaponId){
					Debug.Log ("CheckArmedEquipmentAvailability(), Weapon Availability: "+game.weapon[i].quantity);
					Debug.Log ("CheckArmedEquipmentAvailability(), Assigning Number of soldiers: "+game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"]);
					if (game.weapon[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity){
						game.soldiers [AssigningSoldier - 1].attributes["weapon"].AsInt = game.weapon[i].type;
						game.soldiers[AssigningSoldier - 1].UpdateObject();
						Debug.Log ("CheckArmedEquipmentAvailability(): Weapon not enough.");
						msg = msg.Replace("%SQ%",(game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity).ToString());
						msg = msg.Replace("%EQ%",(numOfSoldiers - game.weapon[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticShowPanel( staticTrainingEquHolder);
					}else{
						game.soldiers [AssigningSoldier - 1].attributes["weapon"].AsInt = AssigningWeaponId;
						game.soldiers [AssigningSoldier - 1].UpdateObject();
//						game.weapon[i].SetQuantity(game.weapon[i].quantity - game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt);
					}
				}
			}
		}
		if (AssigningArmorId != 0) {
			count = game.armor.Count;
			for (int i = 0 ; i< count ; i++){
				if(game.armor[i].type == AssigningArmorId){

					if (game.armor[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity){
						game.soldiers [AssigningSoldier - 1].attributes["armor"].AsInt = game.armor[i].type;
						game.soldiers[AssigningSoldier - 1].UpdateObject();
						msg = msg.Replace("%SQ%",(game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity).ToString());
						msg = msg.Replace("%EQ%",(numOfSoldiers - game.armor[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticShowPanel( staticTrainingEquHolder);
					}else{
						game.soldiers [AssigningSoldier - 1].attributes["armor"].AsInt = AssigningArmorId;
						game.soldiers [AssigningSoldier - 1].UpdateObject();
//						game.armor[i].SetQuantity(game.armor[i].quantity - game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt);
					}
				}
			}
		}
		Debug.Log (AssigningShieldId);
		if (AssigningShieldId != 0) {
			Debug.Log (AssigningShieldId);
			count = game.shield.Count;

			for (int i = 0 ; i< count ; i++){
				if(game.shield[i].type == AssigningShieldId){
					if (game.shield[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity){
						game.soldiers [AssigningSoldier - 1].attributes["shield"].AsInt = game.shield[i].type;
						game.soldiers[AssigningSoldier - 1].UpdateObject();
						msg = msg.Replace("%SQ%",(game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt + game.soldiers [AssigningSoldier - 1].quantity).ToString());
						msg = msg.Replace("%EQ%",(numOfSoldiers - game.shield[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticShowPanel( staticTrainingEquHolder);
					}else{
						game.soldiers [AssigningSoldier - 1].attributes["shield"].AsInt = AssigningShieldId;
						game.soldiers [AssigningSoldier - 1].UpdateObject();
//						game.shield[i].SetQuantity(game.shield[i].quantity - game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt);
					}
				}
			}
		}
		AssigningWeaponId = 0;
		AssigningArmorId = 0;
		AssigningShieldId = 0;
	}

	public void ShowNewWeaponPanel(){
		DestroySchoolFieldWeaponPanel(ArmyListHolder.transform);
//		ProductDict p = ProductDict.Instance;
		var count = game.weapon.Count;
		Transform panel = ArmyListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
#if UNITY_EDITOR
			obj.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);
#else
			obj.transform.localScale = new Vector3 (1f, 1f, 1f);
#endif
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
#if UNITY_EDITOR
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
#else
			obj.transform.localScale = new Vector3 (1f, 1f, 1f);
#endif
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
#if UNITY_EDITOR
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
#else
			obj.transform.localScale = new Vector3 (1f, 1f, 1f);
#endif
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
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt > 0 || game.soldiers [AssigningSoldier - 1].quantity> 0) {
			ShowNewWeaponPanel ();
		} else {
			HidePanel(ArmyListHolder);
			ShowPanel(AssignNSPopup);
			Debug.Log("No soldier assigned");
		}
	}

	public void OnArmorButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0 || game.soldiers [AssigningSoldier - 1].quantity> 0) {
			ShowNewArmorPanel ();
		} else {
			HidePanel(ArmorListHolder);
			ShowPanel( AssignNSPopup);
		}
	}

	public void OnShieldButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0 || game.soldiers [AssigningSoldier - 1].quantity> 0) {
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
//		Debug.Log ("Soldier attributes: "+game.soldiers [AssigningSoldier - 1].attributes.ToString());
		if (j ["trainingSoldiers"].AsInt != 0) {
			for (var i = 0; i < 17; i++) {
				dataHolder.GetChild(i*2+1).GetComponent<Text>().text = (Mathf.Round(j[valueList[i]].AsFloat*100)/100).ToString();						dataHolder.GetChild (i * 2 + 1).GetComponent<Text> ().text = (Mathf.Round (j [valueList [i]].AsFloat * 100) / 100).ToString ();
			}
		} else {
			for (var i = 0; i < 17; i++) {
				dataHolder.GetChild (i * 2 + 1).GetComponent<Text> ().text = (Mathf.Round (j [valueList [i]].AsFloat * 100) / 100).ToString ();
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
		game.soldiers [AssigningSoldier - 1].UpdateObject ();
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
			msg = msg.Replace ("%TM%", TimeUpdate.Time(time));
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
		Debug.Log ("AssigningStarDust: " + AssigningStarDust);
		Debug.Log ("StarDust: " + game.wealth [1].value);
		game.wealth [1].Deduct (AssigningStarDust);
		if (AssigningWeaponId > 0) {
			index = game.weapon.FindIndex (x => x.type == AssigningWeaponId);
			game.weapon [index].quantity += AssigningQuantity;
			game.weapon [index].UpdateObject ();
		} else if (AssigningArmorId > 0) {
			index = game.armor.FindIndex (x => x.type == AssigningArmorId);
			game.armor [index].quantity += AssigningQuantity;
			game.armor [index].UpdateObject ();
		} else if (AssigningShieldId > 0) {
			index = game.shield.FindIndex (x => x.type == AssigningShieldId);
			game.shield [index].quantity += AssigningQuantity;
			game.shield [index].UpdateObject ();
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

		// TODO Only for existing soldiers. New soldiers should be trained automatically without question to get a equalizer with the existings.
		int trainingSoldiers;
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt > 0) {
			trainingSoldiers = game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt;
		} else {
			trainingSoldiers = game.soldiers [AssigningSoldier - 1].quantity;
		}
		Debug.Log ("Training Soldiers: " + trainingSoldiers);
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
		Debug.Log ("AttackSpeed: "+Mathf.Round(j ["AttackSpeed"].AsFloat*100)/100);
		Debug.Log ("AS Target: "+AttackSpeed.GetChild (1).GetChild (1).GetComponent<Slider> ().value);

		if (TotalTrainingTime > 0) {
			HidePanel(AdjustSoildersAttribute);
			ShowConfirmTrainingPanel(trainingSoldiers);

		} else {
			HidePanel(AdjustSoildersAttribute);
		}

	}

	float CalculateSoldierTrainingTime(string type, int NumOfSoldiers, float AbilityToBeTrained){

		int session = Mathf.CeilToInt ((float)NumOfSoldiers/1000);
		float time = 0;
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
		trainParam.Add ("Hammer", 0.7f);
		time = AbilityToBeTrained*session/trainParam[type];
		return time*3600; //Return as number of secounds
	}

	void ShowConfirmTrainingPanel(int numOfSoldiers){
		string msg = msgQuestionConfirmTimeForTrainingSOldiers;
		TimeSpan time = new TimeSpan ((long)SchoolField.TotalTrainingTime *1000*10000);
		msg = msg.Replace ("%SQ%", numOfSoldiers.ToString ());
		Debug.Log (time.ToString ());
		msg = msg.Replace ("%TT%", Utilities.TimeUpdate.Time (time));
		Panel.GetMessageText(TrainingQAHolder).text = msg;
		Panel.GetHeader (TrainingQAHolder).text = headerTrainingQAHolder;
		ShowPanel (TrainingQAHolder);
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
		Panel.GetHeader(TrainingQAHolder).text = headerShowConfirmNewSoldierTraining;
		Panel.GetMessageText(TrainingQAHolder).text = msgShowConfirmNewSoldierTraining;
		TrainingQText.text = "";
		game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt = 0;
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

	public static void CompletingTrainingSoldiers(int s){
		Game game = Game.Instance;
		game.soldiers [s].attributes ["Hit"].AsFloat = game.soldiers [s].attributes ["TargetHit"].AsFloat;
		game.soldiers [s].attributes ["Dodge"].AsFloat = game.soldiers [s].attributes ["TargetDodge"].AsFloat;
		game.soldiers [s].attributes ["Strength"].AsFloat = game.soldiers [s].attributes ["TargetStrength"].AsFloat;
		game.soldiers [s].attributes ["AttackSpeed"].AsFloat = game.soldiers [s].attributes ["TargetAttackSpeed"].AsFloat;
		game.soldiers [s].attributes ["Morale"].AsFloat = game.soldiers [s].attributes ["TargetMorale"].AsFloat;
		game.soldiers [s].attributes ["Wand"].AsFloat = game.soldiers [s].attributes ["TargetWand"].AsFloat;
		game.soldiers [s].attributes ["PiercingLongWeapon"].AsFloat = game.soldiers [s].attributes ["TargetPiercingLongWeapon"].AsFloat;
		game.soldiers [s].attributes ["Bows"].AsFloat = game.soldiers [s].attributes ["TargetBows"].AsFloat;
		game.soldiers [s].attributes ["HighEndBows"].AsFloat = game.soldiers [s].attributes ["TargetHighEndBows"].AsFloat;
		game.soldiers [s].attributes ["HiddenWeapon"].AsFloat = game.soldiers [s].attributes ["TargetHiddenWeapon"].AsFloat;
		game.soldiers [s].attributes ["Knife"].AsFloat = game.soldiers [s].attributes ["TargetKnife"].AsFloat;
		game.soldiers [s].attributes ["HackTypeLongWeapon"].AsFloat = game.soldiers [s].attributes ["TargetHackTypeLongWeapon"].AsFloat;
		game.soldiers [s].attributes ["Sword"].AsFloat = game.soldiers [s].attributes ["TargetSword"].AsFloat;
		game.soldiers [s].attributes ["HighEndSword"].AsFloat = game.soldiers [s].attributes ["TargetHighEndSword"].AsFloat;
		game.soldiers [s].attributes ["SpecialWeapon"].AsFloat = game.soldiers [s].attributes ["TargetSpecialWeapon"].AsFloat;
		game.soldiers [s].attributes ["Axe"].AsFloat = game.soldiers [s].attributes ["TargetAxe"].AsFloat;
		game.soldiers [s].attributes ["Hammer"].AsFloat = game.soldiers [s].attributes ["TargetHammer"].AsFloat;
		game.soldiers [s].attributes.Remove ("ETATrainingTime");
		game.soldiers [s].attributes.Remove ("TargetHit");
		game.soldiers [s].attributes.Remove ("TargetDodge");
		game.soldiers [s].attributes.Remove ("TargetStrength");
		game.soldiers [s].attributes.Remove ("TargetAttackSpeed");
		game.soldiers [s].attributes.Remove ("TargetMorale");
		game.soldiers [s].attributes.Remove ("TargetWand");
		game.soldiers [s].attributes.Remove ("TargetPiercingLongWeapon");
		game.soldiers [s].attributes.Remove ("TargetBows");
		game.soldiers [s].attributes.Remove ("TargetHighEndBows");
		game.soldiers [s].attributes.Remove ("TargetHiddenWeapon");
		game.soldiers [s].attributes.Remove ("TargetKnife");
		game.soldiers [s].attributes.Remove ("TargetHackTypeLongWeapon");
		game.soldiers [s].attributes.Remove ("TargetSword");
		game.soldiers [s].attributes.Remove ("TargetHighEndSword");
		game.soldiers [s].attributes.Remove ("TargetSpecialWeapon");
		game.soldiers [s].attributes.Remove ("TargetAxe");
		game.soldiers [s].attributes.Remove ("TargetHammer");
		if (game.soldiers [s].attributes ["trainingSoldiers"].AsInt > 0) {
			game.soldiers [s].quantity += game.soldiers [s].attributes ["trainingSoldiers"].AsInt;
		}
		game.soldiers [s].attributes ["trainingSoldiers"].AsInt = 0;
		game.soldiers [s].UpdateObject ();
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

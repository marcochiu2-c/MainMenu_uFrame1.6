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
	public GameObject NewSoldierPanel;
	public GameObject AssignNSPopup;
	public GameObject ButtonHolder;
	public GameObject ArmyListHolder;
	public GameObject ArmyQAHolder;
	public GameObject MountListHolder;
	public GameObject MountQAHolder;
	public GameObject ArmorListHolder;
	public GameObject ArmorQAHolder;
	public GameObject ShieldListHolder;
	public GameObject ShieldQAHolder;
	public GameObject TrainingQHolder;
	public GameObject TrainingEquHolder;
	public GameObject TrainingQAHolder;
	public static GameObject staticArmyQAHolder;
	public static GameObject staticArmorQAHolder;
	public static GameObject staticShieldQAHolder;
	public static GameObject staticTrainingEquHolder;

	public static int AssigningWeaponId;
	public static int AssigningArmorId;
	public static int AssigningShieldId;
	public static int AssigningSoldier;  // 兵種

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
//		Debug.Log (TotalSoldiersAvailable());
		InvokeRepeating ("ShowTotalSoldiersAvailableText", 0, 60);
		AddButtonListener ();
		SetDataPanel ();
		staticArmyQAHolder = ArmyQAHolder;
		staticArmorQAHolder = ArmorQAHolder;
		staticShieldQAHolder = ShieldQAHolder;
		staticTrainingEquHolder = TrainingEquHolder;
	}
	
	// Update is called once per frame
	void Update () {
//		if (drag.parentToReturnTo != null) {
//			if (drag.parentToReturnTo == drag.placeholderParent && !hasTrainingQHolderShown &&
//				(drag.placeholderParent.ToString () == "DollPanel (UnityEngine.RectTransform)")) {
//				#region DropTheImageAndInfoCopy
////				drag.placeholderParent.GetComponent<Image>().sprite = StudentPic;
//				AcademyTeach at = drag.placeholderParent.parent.GetComponent ("AcademyTeach") as AcademyTeach;
//				if (drag.placeholderParent.ToString () == "DollPanel (UnityEngine.RectTransform)") {
//					drag.transform.SetParent(NewSoldierPanel.transform);
//					NewSoldierPanel.transform.GetChild(0).localPosition = new Vector3( rtx,rty,0);
//					TrainingQHolder.SetActive(true);
//					hasTrainingQHolderShown = true;
//				}
//				#endregion
//			}
//		}
	}
	
	void AddButtonListener(){
//		TrainingQAHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { Debug.Log ("OnTrainingQAHolderConfirmed()");OnTrainingQAHolderConfirmed(); });
//		Debug.Log (NewSoldierPanel.transform.GetChild (1));
		NewSoldierPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
			Debug.Log ("Training soldiers number: "+game.soldiers[AssigningSoldier-1].attributes["trainingSoldiers"]);
			if (game.soldiers[AssigningSoldier-1].attributes["trainingSoldiers"].AsInt == 0){
				TrainingQHolder.SetActive(true);
			}
		});
		DollPanel.transform.GetChild (0).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			OnWeaponButtonClicked (); });
		DollPanel.transform.GetChild (1).GetChild (0).GetComponent<Button> ().onClick.AddListener (() => {
			OnArmorButtonClicked (); });
		DollPanel.transform.GetChild (1).GetChild (1).GetComponent<Button> ().onClick.AddListener (() => {
			OnShieldButtonClicked (); });
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
		#endregion

		TrainingEquHolder.transform.GetChild(2).GetChild(0).GetComponent<Button>().onClick.AddListener(() => { OnAddEquipmentConfirmed();});
		TrainingEquHolder.transform.GetChild(2).GetChild(1).GetComponent<Button>().onClick.AddListener(() => { 
			TrainingEquHolder.SetActive(false);
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
		availSoldiersText.text = "現有兵數：\n"+i;
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
		Text s = TrainingQHolder.transform.GetChild (1).GetChild (0).GetChild (2).GetComponent<Text> ();
		string soldiers = Regex.Replace(s.text, "[^0-9]", "");
		if (soldiers != "") {
			int soldierQuantity = Int32.Parse (soldiers);
			if (soldierQuantity > TotalSoldiersAvailable ()) {
				TrainingQHolder.SetActive (true);
				TrainingQHolder.transform.GetChild(1).GetComponent<Text>().text="<color=red>輸入兵數不能大於現有兵數</color>\n請輸入欲訓練士兵數目";
				Debug.Log ("Number of new soldiers cannot be larger than available, inputed: " + soldierQuantity);
			} else {   //Valid number
				transform.GetChild(9).GetChild(1).GetComponent<Text>().text="請輸入欲訓練士兵數目";
				transform.GetChild (9).gameObject.SetActive (false);
//				Debug.Log ("士兵數目："+s);
				game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldierQuantity;
				game.soldiers[SchoolField.AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = soldierQuantity;
				ShowTotalSoldiersAvailableText();
				wsc.Send ("users","SET",game.login.toJSON());
				JSONClass json = new JSONClass ();
				json.Add ("id",new JSONData(game.soldiers[SchoolField.AssigningSoldier-1].id));
				json.Add ("userId",new JSONData(game.login.id));
				json.Add ("json", game.soldiers [SchoolField.AssigningSoldier - 1].attributes);
				wsc.Send ("soldier", "SET", json);
				s.text = "";
				TrainingQHolder.SetActive(false);
				CheckArmedEquipmentAvailability();
				SetDataPanel();
			}
		} else {
			return;
		}
	}

	public static void CheckArmedEquipmentAvailability(){
		Game game = Game.Instance;
		string msg ="訓練%SQ%的士兵需要製造額外%EQ%的裝備，找工匠嗎？";
		var count = 0;
		if (AssigningWeaponId != 0) {
			count = game.weapon.Count;
			AssigningArmorId = 0;
			AssigningShieldId = 0;
			for (int i = 0 ; i< count ; i++){
				if(game.weapon[i].type == AssigningWeaponId){

					if (game.weapon[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt){
						msg = msg.Replace("%SQ%",game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"]);
						msg = msg.Replace("%EQ%",(game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt - game.weapon[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticTrainingEquHolder.SetActive(true);
					}
				}
			}
		}
		if (AssigningArmorId != 0) {
			count = game.weapon.Count;
			AssigningWeaponId = 0;
			AssigningShieldId = 0;
			for (int i = 0 ; i< count ; i++){
				if(game.armor[i].type == AssigningArmorId){

					if (game.armor[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt){
						msg = msg.Replace("%SQ%",game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"]);
						msg = msg.Replace("%EQ%",(game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt - game.armor[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticTrainingEquHolder.SetActive(true);
					}
				}
			}
		}

		if (AssigningShieldId != 0) {
			count = game.shield.Count;
			AssigningWeaponId = 0;
			AssigningArmorId = 0;
			for (int i = 0 ; i< count ; i++){
				if(game.shield[i].type == AssigningShieldId){
					if (game.shield[i].quantity < game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt){
						msg = msg.Replace("%SQ%",game.soldiers[AssigningSoldier - 1].attributes["trainingSoldiers"]);
						msg = msg.Replace("%EQ%",(game.soldiers [AssigningSoldier - 1].attributes["trainingSoldiers"].AsInt - game.shield[i].quantity).ToString());
						staticTrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text = msg;
						staticTrainingEquHolder.SetActive(true);
					}
				}
			}
		}
	}

	public void ShowNewWeaponPanel(){
		DestroySchoolFieldWeaponPanel(ArmyListHolder.transform);
		ProductDict p = new ProductDict ();
		var count = game.weapon.Count;
		Transform panel = ArmyListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
			obj.SetSchoolFieldWeapon (SchoolField.AssigningSoldier,
								game.weapon [i].type,
								p.products [game.weapon [i].type].name, 
		                        game.weapon [i].quantity.ToString (), 
		                        p.products [game.weapon [i].type].attributes ["Category"],
		                        "");

			obj.transform.SetParent (panel);
		}

		ArmyListHolder.SetActive (true);
	}

	public void ShowNewArmorPanel(){
		DestroySchoolFieldWeaponPanel(ArmorListHolder.transform);
		ProductDict p = new ProductDict ();
		var count = game.armor.Count;
		Transform panel = ArmorListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
			obj.SetSchoolFieldWeapon (SchoolField.AssigningSoldier,
									  game.armor [i].type,
			                          p.products [game.armor [i].type].name, 
			                          game.armor [i].quantity.ToString (), 
			                          "",
			                          "");
			
			obj.transform.SetParent (panel);
			
		}
		ArmorListHolder.SetActive (true);
	}

	public void ShowNewShieldPanel(){
		DestroySchoolFieldWeaponPanel(ShieldListHolder.transform);
		ProductDict p = new ProductDict ();
		var count = game.shield.Count;
		Transform panel = ShieldListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
			obj.SetSchoolFieldWeapon (SchoolField.AssigningSoldier,
			                          game.shield [i].type,
			                          p.products [game.shield [i].type].name, 
			                          game.shield [i].quantity.ToString (), 
			                          "",
			                          "");
			
			obj.transform.SetParent (panel);
			
		}
		ShieldListHolder.SetActive (true);
	}
	
	public void ShowIsNeedRecruitArtisanPanel(int soldiers){
		string txt = TrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text;
		txt = txt.Replace ("%SQ%", soldiers.ToString ());
		TrainingEquHolder.transform.GetChild (1).GetComponent<Text> ().text = txt;
		TrainingEquHolder.SetActive (true);
	}

	public void OnWeaponButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0) {
			ShowNewWeaponPanel ();
		} else {
			ArmyListHolder.SetActive(false);
			AssignNSPopup.SetActive(true);
			Debug.Log("No soldier assigned");
		}
	}

	public void OnArmorButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0) {
			ShowNewArmorPanel ();
		} else {
			ArmorListHolder.SetActive (false);
			AssignNSPopup.SetActive (true);
		}
	}

	public void OnShieldButtonClicked(){
		if (game.soldiers [AssigningSoldier - 1].attributes ["trainingSoldiers"].AsInt != 0) {
			ShowNewShieldPanel ();
		} else {
			ShieldListHolder.SetActive (false);
			AssignNSPopup.SetActive (true);
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
		if (j ["trainingSoldiers"].AsInt != 0) {
			for (var i = 0; i < 17; i++){
				dataHolder.GetChild(i*2+1).GetComponent<Text>().text = j[valueList[i]];
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
		ProductDict p = new ProductDict ();
		panelDict [armedCategory].SetActive (true);

		qText = panelDict [armedCategory].transform.GetChild (0).GetComponent<Text> ().text;
		if (armedCategory == "weapon"){
			qText = qText.Replace ("%Army%", nameDict [armedCategory]);
		}else if (armedCategory == "armor"){
			qText = qText.Replace ("%Armor%", nameDict [armedCategory]);
		}else if (armedCategory == "shield"){
			qText = qText.Replace ("%Shield%", nameDict [armedCategory]);
		}
		panelDict[armedCategory].transform.GetChild(0).GetComponent<Text>().text = qText;
		qText = panelDict[armedCategory].transform.GetChild(1).GetComponent<Text>().text;
		if (armedCategory == "weapon") {
			qText = qText.Replace ("%Army%", p.products [game.soldiers [SchoolField.AssigningSoldier - 1].attributes [armedCategory].AsInt].name);
			qText = qText.Replace ("%Armys%", p.products [armedType].name);
		}else if (armedCategory == "armor"){
			qText = qText.Replace ("%Armor%", p.products [game.soldiers [SchoolField.AssigningSoldier - 1].attributes [armedCategory].AsInt].name);
			qText = qText.Replace ("%Armors%", p.products [armedType].name);
		}else if (armedCategory == "shield"){
			qText = qText.Replace ("%Shield1%", p.products [game.soldiers [SchoolField.AssigningSoldier - 1].attributes [armedCategory].AsInt].name);
			qText = qText.Replace ("%Shield2%", p.products [armedType].name);
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
		panelDict [category].SetActive (false);

	}

	void OnAddEquipmentConfirmed(){
		//TODO add Artisan Info
		JSONClass j = new JSONClass ();
		j.Add ("artisan_id", new JSONData (0));
		if (AssigningWeaponId != 0) {
			j.Add("target_id", new JSONData(AssigningWeaponId));
			game.soldiers [AssigningSoldier - 1].attributes.Add ("weaponProducing", new JSONData (true));
		} else if (AssigningArmorId != 0) {
			j.Add("target_id", new JSONData(AssigningArmorId));
			game.soldiers [AssigningSoldier - 1].attributes.Add ("armorProducing", new JSONData (true));
		} else if (AssigningShieldId != 0) {
			j.Add("target_id", new JSONData(AssigningShieldId));
			game.soldiers [AssigningSoldier - 1].attributes.Add ("shieldProducing", new JSONData (true));
		}
		j.Add ("start_time", DateTime.Now.ToString());
		//		j.Add ("eta_time", TODO assignTime);
		j.Add ("resources", new JSONData (""));
		j.Add ("metalsmith", new JSONData (0));
		j.Add ("details", new JSONData (""));
		j.Add ("status", new JSONData(4));
		game.artisans.Add (new Artisans (j));


		// TODO add the ETA time to soldiers if new one is end later
		if (game.soldiers [AssigningSoldier - 1].attributes ["artisanETA"] != null) {
			if (DateTime.Parse(game.soldiers [AssigningSoldier - 1].attributes ["artisanETA"]) > DateTime.Now){
				if (DateTime.Parse(game.soldiers [AssigningSoldier - 1].attributes ["artisanETA"]) < new DateTime()/* TODO new ETA  */){
					game.soldiers [AssigningSoldier - 1].attributes.Add("artisanETA",new JSONData(0)); // TODO change 0 to .NET DateTime
				}
			}
		} else {
			game.soldiers [AssigningSoldier - 1].attributes.Add("artisanETA",new JSONData(0)); // TODO change 0 to .NET DateTime
		}

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Utilities;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleJSON;

public enum Tech{ WeaponTechnology = 1, EasternWeaponTechnology = 2, WesternWeaponTechnology = 3,
	FinePolished = 4, Reinforcement = 5, EquipmentWeight = 6, Tempered = 7,
	Pottery = 8, MetalComponents = 9, Mechanical = 10, SpecialProduction = 11,
	SeamEdge = 12, TanningOperation = 13, ArmorSystem = 14, Mining = 15,
	Bronze = 16, MetalFabrication = 17, CastingFencing = 18, GunProduction = 19,
	Fletcher = 20, SystemShield = 21, ProductionCrossbow = 22,
	WoodWorker = 23, Geometry = 24, Physics = 25, BasicScience = 26,
	Chemistry = 27, PeriodicTable = 28, MagnesiumApplications = 29,
	Biology = 30, BodyStructure = 31, Kinetics = 32, Nutrition = 33,
	ScienceTraining = 34, CoalApplication = 35, ChainSteel = 36,
	Psychology = 37, MindReading = 38, MindControl = 39, PaperMaking = 40, 
	Typography = 41, Compass = 42, IChing = 43
}

public enum TechTreeDialogCommand { NoCounselorSelected, ConfirmTraining }

public class TechTree : MonoBehaviour {
	public static List<Button> CounselorButtons = new List<Button>();
	public static Text TotalIQText;
	public static Button TechnologySelector;
	public static Text TechnologyLabel;
	public static Text FromLv;
	public static Text ToLv;
	public static Text RemainTrainingTime;

//	public GameObject CounselorSelectorHolder;
	public static GameObject CounselorList;

	public static GameObject WeaponTechnologyHolder;
	public static GameObject PotteryHolder;
	public static GameObject SeamEdgeHolder;
	public static GameObject MiningHolder;
	public static GameObject WoodworkerHolder;
	public static GameObject BasicScienceHolder;
	public static GameObject IChingHolder;
	public static GameObject TreeDiagram;
	public static GameObject MessageDialog;
	public static GameObject ConfirmDialog;
	public static GameObject DisablePanel;

	static Button WeaponTechnologyButton;
	static Button PotteryButton;
	static Button SeamEdgeButton;
	static Button MiningButton;
	static Button WoodworkerButton;
	static Button BasicScienceButton;
	static Button IChingButton;
	static Button BackButton;
	static Button CloseButton;

	List<Button> TechButtons;

	public static Tech AssigningTech;
	public static TechTreeDialogCommand DialogCommand; 
	
	public static int[] AssigningCounselor = new int[]{0,0,0,0,0};
	public static int AssigningSlot=0;
	public static int AssigningCounselorSlot = 0;
	public const int DBSlot = 8;
	
	static List<int> TechRequirementNone = new List<int>{};
	static List<int> TechRequirement1 = new List<int>{1};
	static List<int> TechRequirement2 = new List<int>{1,(int)Tech.MetalFabrication};
	static List<int> TechRequirement3 = new List<int>{1,(int)Tech.WoodWorker,(int)Tech.Bronze};
	static List<int> TechRequirement4 = new List<int>{(int)Tech.BasicScience};
	static List<int> KnowledgeRequire1 = new List<int>{2003,2004,2005};
	static List<int> KnowledgeRequire2 = new List<int>{(int)Knowledge.MetalProcessing,(int)Knowledge.Crafts,(int)Knowledge.Geometry,
		(int)Knowledge.Physics,(int)Knowledge.Chemistry,(int)Knowledge.PeriodicTable
	};
	static List<int> KnowledgeRequire3 = new List<int>{2001,2002};
	static List<int> KnowledgeRequire4 = new List<int>{2012,2013};

	public static List<Counselor> cList;
	public static List<TechItem> TechItems = new List<TechItem>{
		new TechItem("",0,TechRequirementNone,new List<int>(),0,0,""),
		new TechItem("兵器工藝",1, TechRequirementNone,	KnowledgeRequire3,0,90,"製作武器以及兵器相關科技的前置條件。"),
		new TechItem("東亞兵器工藝",2, TechRequirement1,	KnowledgeRequire1,10, 100,"製作東亞兵器。"),
		new TechItem("西洋兵器工藝",2, TechRequirement1,	KnowledgeRequire1,10, 100,"製作西洋兵器。"),
		new TechItem("精細打磨",3, TechRequirement2,		KnowledgeRequire2,20, 110,"按級數提升武器鋒利程度(%)。"),
		new TechItem("加固",3, TechRequirement2,			KnowledgeRequire2,20, 110,"按級數加固裝甲及盾(%)。"),
		new TechItem("裝備減重",3, TechRequirement2,KnowledgeRequire2,20, 110,"按級數減少所有裝備重量(%)。"),
		new TechItem("千錘百鍊",5, new List<int>{1,4,5,6,(int)Tech.MetalFabrication,27},new List<int>{(int)Knowledge.Catapult,(int)Knowledge.GunpowderModulation,(int)Knowledge.Psychology},40, 130,"為將士製作超凡兵器。"),
		new TechItem("陶藝",1, new List<int>(),KnowledgeRequire3,0, 90,"其他科技的前置條件。"),
		new TechItem("金屬組件",2, new List<int>{8,(int)Tech.MetalFabrication},KnowledgeRequire1,10, 100,"其他科技的前置條件。"),
		new TechItem("機械",3, new List<int>{9,(int)Tech.Physics},KnowledgeRequire2,20, 110,"製作裝備的前置條件。"),
		new TechItem("特殊製作",3, new List<int>{9},		KnowledgeRequire2,20, 110,"製作裝備的前置條件。"),
		new TechItem("縫刃",1, TechRequirementNone,		KnowledgeRequire3,0, 90,"製作裝備的前置條件。"),
		new TechItem("製革術",1, new List<int>{1,(int)Tech.SeamEdge},KnowledgeRequire3,0, 90,"製作裝備及其他科技的前置條件。"),
		new TechItem("製甲術",3, new List<int>{1,(int)Tech.TanningOperation,(int)Tech.Bronze},KnowledgeRequire2,20, 110,"製作裝備的前置條件。"),
		new TechItem("採礦",1, TechRequirementNone,		KnowledgeRequire3,0, 90,"其他科技的前置條件。"),
		new TechItem("青銅器",1.5f, new List<int>{(int)Tech.Mining},KnowledgeRequire3,5, 95,"製作裝備及其他科技的前置條件。"),
		new TechItem("冶鐵",2, new List<int>{(int)Tech.Bronze},KnowledgeRequire1,10, 100,"製作裝備及其他科技的前置條件。"),
		new TechItem("鑄劍術",4, new List<int>{1,(int)Tech.Bronze},KnowledgeRequire4, 30, 120,"製作裝備的前置條件。"),
		new TechItem("槍製作",3, TechRequirement3,		KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("造箭",2, TechRequirement3,			KnowledgeRequire1, 10, 100,"製作裝備的前置條件。"),
		new TechItem("製盾術",3, TechRequirement3,		KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("弓弩製作",3, new List<int>{1,(int)Tech.WoodWorker,(int)Tech.Bronze,(int)Tech.Physics},KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("木工",1, TechRequirementNone,		KnowledgeRequire3, 0, 90,"其他科技的前置條件。"),
		new TechItem("幾何",1.5f, TechRequirementNone,	KnowledgeRequire3, 5, 95,"其他科技的前置條件。"),
		new TechItem("物理",2, new List<int>{(int)Tech.Geometry,(int)Tech.BasicScience},KnowledgeRequire1, 10, 100,"其他科技的前置條件。"),
		new TechItem("基礎科學",1, TechRequirementNone,	KnowledgeRequire3, 0, 90,"其他科技的前置條件。"),
		new TechItem("化學",2, TechRequirement4,			KnowledgeRequire1, 10, 100,"其他科技的前置條件。"),
		new TechItem("元素表",2, new List<int>{(int)Tech.Chemistry},KnowledgeRequire1, 10, 100,"其他科技的前置條件。"),
		new TechItem("鎂應用",2, new List<int>{(int)Tech.PeriodicTable},KnowledgeRequire1, 10, 100,"其他科技的前置條件。"),
		new TechItem("生物學",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"其他科技的前置條件。"),
		new TechItem("人體結構",3, new List<int>{(int)Tech.Biology},KnowledgeRequire2, 20, 110,"其他科技的前置條件。"),
		new TechItem("運動學",3, new List<int>{(int)Tech.BodyStructure},KnowledgeRequire2, 20, 110,"按級數提升士兵攻速及行速(%)。"),
		new TechItem("營養學",3, new List<int>{(int)Tech.Biology,(int)Tech.Chemistry},KnowledgeRequire2, 20, 110,"按級數提升士兵體格上限。"),
		new TechItem("科學訓練",4, new List<int>{(int)Tech.Kinetics,(int)Tech.Nutrition},KnowledgeRequire4, 30, 120,"按級數減少士兵訓練時間(%)。"),
		new TechItem("煤應用",3, new List<int>{(int)Tech.PeriodicTable},KnowledgeRequire2, 20, 110,"其他科技的前置條件。"),
		new TechItem("鍊鋼",3, new List<int>{(int)Tech.MagnesiumApplications,(int)Tech.CoalApplication},KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("心理學",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"其他科技的前置條件。"),
		new TechItem("讀心術",4, new List<int>{(int)Tech.Psychology},KnowledgeRequire4, 30, 120,"按等級揭示敵軍佈陣(最多50%)。"),
		new TechItem("心靈操控",5, new List<int>{(int)Tech.MindReading},new List<int>{(int)Knowledge.Catapult,(int)Knowledge.GunpowderModulation,(int)Knowledge.Psychology}, 40, 130,"按等級提升自軍士氣、減低敵軍士氣、提升叫囂效果。"),
		new TechItem("造紙術",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"按等級增加在學堂學習的速度(%)。"),
		new TechItem("印刷術",3, new List<int>{(int)Tech.PaperMaking,(int)Tech.Mechanical},KnowledgeRequire2, 20, 110,"按等級增加在學堂學習的速度(%)。"),
		new TechItem("指南針",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"增強偵察兵效能。"),
		new TechItem("易學",7, TechRequirementNone,		new List<int>{(int)Knowledge.IChing}, 60, 170,"按等級揭示敵軍佈陣(最多108%)。")
	};

	Game game;
	string CounselorNotSelectedHeader = "未選軍師";
	string CounselorNotSelectedMessage = "軍師閣下，請先選擇至少一個軍師";

	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;

	static int[] LevelIQPercentage = {0,1,2,4,6,8,10,12,14,18,25};

	public static int GetIQRequirement(int tech, int level){
		int totalIQ = 2160000;
		Debug.Log ("Total IQ: " + totalIQ  * (TechItems [tech].Weight / 114));
		Debug.Log ("Level: " + level);
		Debug.Log ("Level IQ: " + LevelIQPercentage [level]);
		return Mathf.RoundToInt (totalIQ*(TechItems[tech].Weight/114)*(LevelIQPercentage[level]/100f));
	}


	// Use this for initialization
	void Start(){

	}

	void OnEnable () {
		game = Game.Instance;
		AssignGameObjectVariable ();
		MapTechButtons ();
		if (TechTreePrefab.person.Count == 0) {
			SetupStudentPrefabList ();
		} else {
			for (int i = 0 ; i < TechTreePrefab.person.Count ; i++){
				TechTreePrefab.person[i].gameObject.SetActive(true);
			}
		}

//		staticCounselorSelectorHolder = CounselorSelectorHolder.transform;
		InvokeRepeating ("SetRemainingTime", 0, 1);
//		SetupTechTreeDiagram ();
		CallTechTree ();
	}

	void MapTechButtons(){
		TechButtons = new List<Button>();
		for (int i = 0; i < 7; i++) {
			TechButtons.Add ( WeaponTechnologyHolder.transform.GetChild (i).gameObject.GetComponent<Button> ());
		}
		for (int i = 0; i < 4; i++) {
			TechButtons.Add (PotteryHolder.transform.GetChild (i).gameObject.GetComponent<Button> ());
		}
		TechButtons.Add ( SeamEdgeHolder.transform.GetChild(0).gameObject.GetComponent<Button>());
		TechButtons.Add ( SeamEdgeHolder.transform.GetChild(1).gameObject.GetComponent<Button>());
		TechButtons.Add ( SeamEdgeHolder.transform.GetChild(2).gameObject.GetComponent<Button>());
		for (int i = 0; i < 8; i++) {
			TechButtons.Add ( MiningHolder.transform.GetChild (i).gameObject.GetComponent<Button> ());
		}
		TechButtons.Add ( WoodworkerHolder.transform.GetChild (0).gameObject.GetComponent<Button> ());
		for (int i = 0; i < 19; i++) {
			TechButtons.Add ( BasicScienceHolder.transform.GetChild (i).gameObject.GetComponent<Button> ());
		}
		TechButtons.Add ( IChingHolder.transform.GetChild (0).gameObject.GetComponent<Button> ());
	}

	void OnDisable(){
		CancelInvoke ();
	}
		
	// Update is called once per frame
	void Update () {

	}

	void CallTechTree(){
		SetCharacters ();
		TechnologyLabel.text = TechItems[ GetCurrentTech ()].Item;
		game = Game.Instance;
		for (int i = 0; i < 5; i++) {
			AssigningCounselor[i] = game.trainings[DBSlot].attributes["TechTreeCounselors"][i].AsInt;
			if (AssigningCounselor[i] > 0){
			CounselorButtons[i].GetComponent<Image>().sprite = 
				imageDict[game.counselor.Find (x => x.id == AssigningCounselor[i]).attributes["type"].AsInt];
			CounselorButtons[i].transform.GetChild(0).GetComponent<Text>().text = 
				nameDict[game.counselor.Find (x => x.id == AssigningCounselor[i]).attributes["type"].AsInt];
			}
		}
		// TODO add ICON image here and after assignment
		TotalIQText.text = TotalIQOfCounselors ().ToString();
		AssigningTech = (Tech)game.trainings[DBSlot].type;
		if (game.trainings [DBSlot].etaTimestamp > DateTime.Now) {
			FromLv.text = "由 "+game.login.attributes [Enum.GetName (typeof(Tech), AssigningTech)].AsInt.ToString ()+" 級";
			ToLv.text = "升至 "+(game.login.attributes [Enum.GetName (typeof(Tech), AssigningTech)].AsInt + 1).ToString ()+" 級";
		} else {
			FromLv.text = "";
			ToLv.text = "";
		}
		DisablePanel = transform.FindChild ("DisablePanel").gameObject;
		MessageDialog = transform.FindChild ("MessageDialog").gameObject;
		ConfirmDialog = transform.FindChild ("ConfirmDialog").gameObject;
		AddButtonListener ();
		if (game.trainings[DBSlot].etaTimestamp < DateTime.Now){
			ClearUIInformation();
		}

	}

	public void AssignGameObjectVariable(){
		Transform layout = transform.GetChild (1);
		Transform timeLeft = layout.GetChild (2);
		TotalIQText = layout.GetChild (1).GetChild (1).GetComponent<Text> ();
		TechnologySelector = timeLeft.GetChild (0).GetComponent<Button> ();
		TechnologyLabel = timeLeft.GetChild (1).GetComponent<Text> ();
		FromLv = timeLeft.GetChild (2).GetComponent<Text> ();
		ToLv = timeLeft.GetChild (3).GetComponent<Text> ();
		RemainTrainingTime = timeLeft.GetChild (4).GetComponent<Text> ();
		CounselorList = transform.GetChild (2).gameObject;
		TreeDiagram = transform.Find ("TreeDiagram").gameObject;
		Transform technologyHolder = TreeDiagram.transform.GetChild (1);
		WeaponTechnologyHolder = technologyHolder.Find ("WeaponTechnologyHolder").gameObject;
		PotteryHolder = technologyHolder.Find ("PotteryHolder").gameObject;
		SeamEdgeHolder = technologyHolder.Find ("SeamEdgeHolder").gameObject;
		MiningHolder = technologyHolder.Find ("MiningHolder").gameObject;
		WoodworkerHolder = technologyHolder.Find ("WoodworkerHolder").gameObject;
		BasicScienceHolder = technologyHolder.Find ("BasicScienceHolder").gameObject;
		IChingHolder = technologyHolder.Find ("IChingHolder").gameObject;
		Transform buttonHolder = TreeDiagram.transform.GetChild (0).GetChild (0);
		WeaponTechnologyButton = buttonHolder.Find ("WeaponTechnology").GetComponent<Button>();
		PotteryButton = buttonHolder.Find ("Pottery").GetComponent<Button>();
		SeamEdgeButton = buttonHolder.Find ("SeamEdge").GetComponent<Button>();
		MiningButton = buttonHolder.Find ("Mining").GetComponent<Button>();
		WoodworkerButton = buttonHolder.Find ("Woodworker").GetComponent<Button>();
		BasicScienceButton = buttonHolder.Find ("BasicScience").GetComponent<Button>();
		IChingButton = buttonHolder.Find ("IChing").GetComponent<Button>();

		BackButton = transform.Find ("BackButton").GetComponent<Button>();
		CloseButton = transform.Find ("CloseButton").GetComponent<Button>();
		Transform counselorButtonHolder = layout.GetChild (1).GetChild (0);
		for (int i = 0; i < 5; i++) {
			CounselorButtons.Add (counselorButtonHolder.GetChild (i).GetComponent<Button>());
		}

	}

	/// <summary>
	/// Clears the user interface information while training completed.
	/// </summary>
	void ClearUIInformation(){
		for (int i = 0; i < 5; i++){
			TechTree.CounselorButtons[i].GetComponent<Image>().sprite = MainScene.TechTreeKnob;
			TechTree.CounselorButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
		}
		TechTree.TotalIQText.text = "";
		TechTree.TechnologyLabel.text = "";
		TechTree.FromLv.text = "";
		TechTree.ToLv.text = "";
		TechTree.RemainTrainingTime.text = "00:00:00";
	}
	
	void SetHeadImages(){

	}
	
	public void SetRemainingTime(){
		if (game.trainings [DBSlot].etaTimestamp > DateTime.Now) {
			RemainTrainingTime.text = Utilities.TimeUpdate.Time(game.trainings[DBSlot].etaTimestamp);
		} else {
			RemainTrainingTime.text = "00:00:00";
		}
	}

	// Button Listener
	void AddButtonListener(){
		WeaponTechnologyButton.onClick.AddListener (() => {ChangeHolder("WeaponTechnology");});
		PotteryButton.onClick.AddListener (() => {ChangeHolder("Pottery");});
		SeamEdgeButton.onClick.AddListener (() => {ChangeHolder("SeamEdge");});
		MiningButton.onClick.AddListener (() => {ChangeHolder("Mining");});
		WoodworkerButton.onClick.AddListener (() => {ChangeHolder("Woodworker");});
		BasicScienceButton.onClick.AddListener (() => {ChangeHolder("BasicScience");});
		IChingButton.onClick.AddListener (() => {ChangeHolder("IChingButton");});

		for (int i = 0; i < 5; i++) {
			CounselorButtons[i].onClick.AddListener(()=> {
				OnCounselorButtonClicked(EventSystem.current.currentSelectedGameObject.name);
			});
		}
		TechnologySelector.onClick.AddListener (() => {
			if (game.trainings [DBSlot].etaTimestamp < DateTime.Now && TotalIQOfCounselors() > 0) {  // training not started
				SetupTechTreeDiagram ();
				TreeDiagram.SetActive(true);
			}else{
				if (game.trainings [DBSlot].etaTimestamp < DateTime.Now){
					Panel.GetHeader(MessageDialog).text = CounselorNotSelectedHeader;
					Panel.GetMessageText(MessageDialog).text = CounselorNotSelectedMessage;
					DialogCommand = TechTreeDialogCommand.NoCounselorSelected;
					ShowPanel(MessageDialog);
				}
			}
		});
		BackButton.onClick.AddListener (() => {
			CounselorList.SetActive(false);
			TreeDiagram.SetActive(false);
		});
		CloseButton.onClick.AddListener (() => {
			CounselorList.SetActive(false);
			TreeDiagram.SetActive(false);
			gameObject.SetActive(false);
		});
		Panel.GetConfirmButton(MessageDialog).onClick.AddListener(() => {
			if (DialogCommand == TechTreeDialogCommand.NoCounselorSelected){
				HidePanel(MessageDialog);
			}
		});
		Panel.GetConfirmButton (ConfirmDialog).onClick.AddListener (() => {
			if (DialogCommand == TechTreeDialogCommand.ConfirmTraining){
				OnConfirmedTraining();
				HidePanel(ConfirmDialog);
			}
		});
		Panel.GetCancelButton (ConfirmDialog).onClick.AddListener (() => {
			if (DialogCommand == TechTreeDialogCommand.ConfirmTraining){
				HidePanel(ConfirmDialog);
			}
		});


	}

	public void SetupStudentPrefabList(){
		//		Utilities.ShowLog.Log ("Counselor Wisdom: " + game.counselor [1].attributes.ToString());
		cList = new List<Counselor> ();
		for (int j = 0 ; j < game.counselor.Count; j++){
			cList.Add (new Counselor(game.counselor[j]));
		}
		Debug.Log ("Number of Counselors: "+game.counselor.Count);
		
		int cslCount = 0; 
		Trainings training = game.trainings[DBSlot];
		for (var i = 0 ; i < cList.Count ; i++){
			for (int j=0; j < training.attributes["TechTreeCounselors"].Count; j++){
				if (training.attributes["TechTreeCounselors"][j].AsInt == cList[i].id){
					cList.Remove(cList[i]);
				}
			}
		}  
		cslCount = cList.Count;
		for (var i = 0 ; i < cslCount ; i++){
			StartCoroutine( CreateCandidateItemNew(cList[i]));
		}		
	}
	
	public IEnumerator CreateCandidateItemNew(Counselor character){
		var type = character.type;
		TechTreePrefab obj = Instantiate(Resources.Load("TechTreePrefab") as GameObject).GetComponent<TechTreePrefab>();
		TechTreePrefab.person.Add (obj);
		yield return 0;
		obj.SetCounselor (character);
		obj.AddButtonListener ();
	}

	void OnCounselorButtonClicked(string name){
		if (game.trainings [DBSlot].etaTimestamp < DateTime.Now) {  // training not started
			if (name == "Button") {
				//TechTreePrefab.person.Find (x => x.counselor.id == AssigningCounselor[AssigningCounselorSlot]).gameObject.SetActive(true);
				AssigningCounselorSlot = 0;
			}else if (name == "Button (1)") {
				//TechTreePrefab.person.Find (x => x.counselor.id == AssigningCounselor[AssigningCounselorSlot]).gameObject.SetActive(true);
				AssigningCounselorSlot = 1;
			}else if (name == "Button (2)") {
				//TechTreePrefab.person.Find (x => x.counselor.id == AssigningCounselor[AssigningCounselorSlot]).gameObject.SetActive(true);
				AssigningCounselorSlot = 2;
			}else if (name == "Button (3)") {
				//TechTreePrefab.person.Find (x => x.counselor.id == AssigningCounselor[AssigningCounselorSlot]).gameObject.SetActive(true);
				AssigningCounselorSlot = 3;
			}else if (name == "Button (4)") {
//				TechTreePrefab.person.Find (x => x.counselor.id == AssigningCounselor[AssigningCounselorSlot]).gameObject.SetActive(true);
				AssigningCounselorSlot = 4;
			}
			CounselorList.SetActive (true);
		}
		TechTreePrefab c = TechTreePrefab.person.Find (x => x.counselor.id == AssigningCounselor [AssigningCounselorSlot]);
		if (c != null) {
			c.gameObject.SetActive (true);
		}
	}


	void ChangeHolder(string holder){
		WeaponTechnologyHolder.SetActive (false);
		PotteryHolder.SetActive (false);
		SeamEdgeHolder.SetActive (false);
		MiningHolder.SetActive (false);
		WoodworkerHolder.SetActive (false);
		BasicScienceHolder.SetActive (false);
		IChingHolder.SetActive (false);
		if (holder == "WeaponTechnology") {
			WeaponTechnologyHolder.SetActive (true);
		}else if (holder == "Pottery") {
			PotteryHolder.SetActive (true);
		}else if (holder == "SeamEdge") {
			SeamEdgeHolder.SetActive (true);
		}else if (holder == "IChing") {
			IChingHolder.SetActive (true);
		}else if (holder == "Mining") {
			MiningHolder.SetActive (true);
		}else if (holder == "Woodworker") {
			WoodworkerHolder.SetActive (true);
		}else if (holder == "BasicScience") {
			BasicScienceHolder.SetActive (true);
		}
	}

	void NotEnoughMoney(){
		// Open a Confirm/Cancel dialog 

	}



	void ConfirmingTraining(string techName){
		TechnologyLabel.text = TechItems[(int)AssigningTech].Item;
		Debug.Log ("Total Training Hours: "+GetTotalTrainingHours ());
		Panel.GetHeader (ConfirmDialog).text = "進行科研";
		Panel.GetMessageText (ConfirmDialog).text = "軍師閣下，進行科研「"+TechItems[(int)AssigningTech].Item+"」需時 "+TimeUpdate.Time( GetTotalTrainingHours());
		Debug.Log ("Assigning Tech Level : "+ (game.login.attributes[Enum.GetName(typeof(Tech),AssigningTech)].AsInt));
		DialogCommand = TechTreeDialogCommand.ConfirmTraining;
		ConfirmDialog.SetActive (true);
	
	}

	void OnConfirmedTraining(){
		Debug.Log ("OnConfirmedTraining()");
		TreeDiagram.SetActive(false);

		game.trainings[DBSlot].type = (int)AssigningTech;
		game.trainings[DBSlot].targetId = 0;
		game.trainings[DBSlot].startTimestamp = DateTime.Now;
		game.trainings[DBSlot].trainerId = 0;
		game.trainings[DBSlot].etaTimestamp = DateTime.Now+GetTotalTrainingHours();
		game.trainings[DBSlot].status = (int)TrainingStatus.OnGoing;
		for (int i = 0; i < 5; i++) {
			game.trainings [DBSlot].attributes ["TechTreeCounselors"][i].AsInt = AssigningCounselor[i];
		}
		game.trainings[DBSlot].UpdateObject();
	}

	TimeSpan GetTotalTrainingHours(){
		Debug.Log ("Total Counselor IQ: " + TotalIQOfCounselors ());
		var tech = Enum.GetName (typeof(Tech), AssigningTech);
		Debug.Log (tech+" Level: "+game.login.attributes[tech].AsInt);
		float seconds = 3600 / TotalIQOfCounselors() * GetIQRequirement((int)AssigningTech,game.login.attributes[tech].AsInt) ;
		return new TimeSpan (0,0,(int)seconds);
	}

	bool isTrainingValid(Tech tech){
		List<Knowledge> knowledgeList = GetAvailableKnowlegdeList ();
		List<Tech> techList = GetAvailableTechList ();
		float highestIQ = HighestIQOfCounselors ();

		int count = 0;
		TechItem techs = TechItems [(int)tech];

//		ShowLog.Log (techs.Item);
//		if (tech == Tech.WeaponTechnology) {
////			Debug.Log("Weapon Tech, Tech requirement: "+string.Join(",", TechItems[0].TechRequirement.Select(x => x.ToString()).ToArray()));
//			Debug.Log("Weapon Tech, Tech requirement: "+ TechItems[0].TechRequirement.Count);
//			Debug.Log("Weapon Tech, Knowledge requirement: "+string.Join(",", TechItems[0].KnowledgeRequirement.Select(x => x.ToString()).ToArray()));
//			Debug.Log("Knowledge List:"+string.Join(",", knowledgeList.Select(x => x.ToString()).ToArray()));
//			Debug.Log("Tech List:"+string.Join(",", techList.Select(x => x.ToString()).ToArray()));
//		}
//		if (tech == Tech.EasternWeaponTechnology) {
////			Debug.Log("Eastern Weapon Tech, Tech requirement: "+string.Join(",", TechItems[1].TechRequirement.Select(x => x.ToString()).ToArray()));
//			Debug.Log("Eastern Weapon Tech, Tech requirement: "+ TechItems[1].TechRequirement.Count);
//		}
//		if (tech == Tech.Tempered) {
//			Debug.Log("Tempered, Tech requirement: "+string.Join(",", TechItems[6].TechRequirement.Select(x => x.ToString()).ToArray()));
//			Debug.Log("Tempered, Knowledge requirement: "+string.Join(",", TechItems[6].KnowledgeRequirement.Select(x => x.ToString()).ToArray()));
//		}
		if (techs.MinimumIQ > highestIQ /*|| techs.MinimumLevel > level*/) {
			ShowLog.Log("IQ Not Enough, Minimum IQ: "+ techs.MinimumIQ+" Highest IQ: "+highestIQ);
			return false;
		}

		count = techs.TechRequirement.Count;
		for (int i = 0; i < count ; i++){	
			if (!techList.Exists(x => (int)x == techs.TechRequirement[i])){
				ShowLog.Log("No Required Tech: "+(Tech)techs.TechRequirement[i]);
				return false;
			}
	    }
		count = techs.KnowledgeRequirement.Count;
		for (int i = 0; i < count ; i++){
			if (!knowledgeList.Exists(x => (int)x == techs.KnowledgeRequirement[i])){
				ShowLog.Log("No Required Knowledge: "+(Knowledge)techs.KnowledgeRequirement[i]);
				return false;
			}
		}

		return true;
	}

	/// <summary>
	/// Gets the current tech the play have.
	/// </summary>
	/// <returns>The current tech.</returns>
	public static int GetCurrentTech(){
		Game game = Game.Instance;
		return game.trainings [DBSlot].type;
	}

	/// <summary>
	/// The total of the IQ of selected counselors.
	/// </summary>
	/// <returns>The IQ of counselors.</returns>
	public static float TotalIQOfCounselors(){
		Game game = Game.Instance;
		float totalIQ = 0;
		int count = AssigningCounselor.Length;
		Counselor c;
		for (int i = 0; i < count; i++) {
			c = game.counselor.Find (x => x.id == AssigningCounselor[i]);
			if (c != null){
				totalIQ += c.attributes["attributes"]["IQ"].AsFloat;
			}
		}
		return totalIQ;  
	}

	/// <summary>
	/// Highests the IQ of selected counselors.
	/// </summary>
	/// <returns>The IQ of counselors.</returns>
	float HighestIQOfCounselors(){
		float highestIQ = 0;
		int count = AssigningCounselor.Length;
		Counselor c;
		for (int i = 0; i < count; i++) {
			c = game.counselor.Find(x => x.id == AssigningCounselor[i]);
			if (c != null){
				if (c.attributes["attributes"]["IQ"].AsFloat > highestIQ){
					highestIQ = c.attributes["attributes"]["IQ"].AsFloat;
				}
			}
		}
		return highestIQ;  
	}

	public int GetTechLevel(Tech tech){
		string techName = Enum.GetName(typeof(Tech),(int)tech);
		return game.login.attributes [techName].AsInt;
	}

	public static List<Tech> GetAvailableTechList(){
		Game game = Game.Instance;
		int numOfEnum = Enum.GetNames (typeof(Tech)).Length;
		List<Tech> list = new List<Tech> ();

		for (int j = 1; j < numOfEnum+1 ; j++){
		    if (game.login.attributes[Enum.GetName(typeof(Tech),(Tech)j)].AsInt > 0){
				if (!list.Exists(x => x == (Tech)j)){
					list.Add ((Tech)j);
				}
			}	
		}

		if (list.Count==numOfEnum){
			return list;
		}

		return list;
	}


	public static List<Knowledge> GetAvailableKnowlegdeList(){
		Game game = Game.Instance;
		int numOfEnum = Enum.GetNames (typeof(Knowledge)).Length;
		List<Knowledge> list = new List<Knowledge> ();
		int co;

		for (int i = 0; i < 5; i++) {

			co = game.counselor.FindIndex (x  => x.id == AssigningCounselor[i]);
			if (co < 0){
				continue;
			}

			for (int j = 2001; j < numOfEnum+2000 ; j++){
				if (game.counselor[co].attributes["attributes"]["KnownKnowledge"][Enum.GetName (typeof(Knowledge),(Knowledge)j)].AsInt > 0){
					if (!list.Exists(x => x == (Knowledge)j)){
						list.Add ((Knowledge)j);
					}
				}
			}
			if (list.Count==numOfEnum){
				return list;
			}
		}
		return list;
	}

	void SetupTechTreeDiagram(){
		JSONClass tech = game.login.attributes;
		List<Knowledge> knowledges = GetAvailableKnowlegdeList ();
		int count = TechButtons.Count;
		for (int i = 0; i< count; i++) {
			if (isTrainingValid ((Tech)(i+1))) {
				TechButtons[i].interactable = true;
			}else{
				TechButtons[i].interactable = false;
			}
		}
	}

	public void OnTechButtonClicked(){
		if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == WeaponTechnologyHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				AssigningTech = Tech.WeaponTechnology;
				ConfirmingTraining ("WeaponTechnology");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (1)") {
				AssigningTech = Tech.EasternWeaponTechnology;
				ConfirmingTraining ("EasternWeaponTechnology");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (2)") {
				AssigningTech = Tech.WesternWeaponTechnology;
				ConfirmingTraining ("WesternWeaponTechnology");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (3)") {
				AssigningTech = Tech.FinePolished;
				ConfirmingTraining ("FinePolished");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (4)") {
				AssigningTech = Tech.Reinforcement;
				ConfirmingTraining ("Reinforcement");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (5)") {
				AssigningTech = Tech.EquipmentWeight;
				ConfirmingTraining ("EquipmentWeight");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (6)") {
				AssigningTech = Tech.Tempered;
				ConfirmingTraining ("Tempered");
			}
		} else if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == PotteryHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				AssigningTech = Tech.Pottery;
				ConfirmingTraining ("Pottery");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (1)") {
				AssigningTech = Tech.MetalComponents;
				ConfirmingTraining ("MetalComponents");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (2)") {
				AssigningTech = Tech.Mechanical;
				ConfirmingTraining ("Mechanical");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (3)") {
				AssigningTech = Tech.SpecialProduction;
				ConfirmingTraining ("SpecialProduction");
			}
		} else if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == SeamEdgeHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				AssigningTech = Tech.SeamEdge;
				ConfirmingTraining ("SeamEdge");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (1)") {
				AssigningTech = Tech.TanningOperation;
				ConfirmingTraining ("TanningOperation");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (2)") {
				AssigningTech = Tech.ArmorSystem;
				ConfirmingTraining ("ArmorSystem");
			}
		} else if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == MiningHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				AssigningTech = Tech.Mining;
				ConfirmingTraining ("Mining");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (1)") {
				AssigningTech = Tech.Bronze;
				ConfirmingTraining ("Bronze");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (2)") {
				AssigningTech = Tech.MetalFabrication;
				ConfirmingTraining ("MetalFabrication");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (3)") {
				AssigningTech = Tech.CastingFencing;
				ConfirmingTraining ("CastingFencing");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (4)") {
				AssigningTech = Tech.GunProduction;
				ConfirmingTraining ("GunProduction");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (5)") {
				AssigningTech = Tech.Fletcher;
				ConfirmingTraining ("Fletcher");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (6)") {
				AssigningTech = Tech.SystemShield;
				ConfirmingTraining ("SystemShield");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (7)") {
				AssigningTech = Tech.ProductionCrossbow;
				ConfirmingTraining ("ProductionCrossbow");
			}
		} else if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == WoodworkerHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				ConfirmingTraining ("WoodWorker");
			}
		} else if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == BasicScienceHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				AssigningTech = Tech.Geometry;
				ConfirmingTraining ("Geometry");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (1)") {
				AssigningTech = Tech.Physics;
				ConfirmingTraining ("Physics");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (2)") {
				AssigningTech = Tech.BasicScience;
				ConfirmingTraining ("BasicScience");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (3)") {
				AssigningTech = Tech.Chemistry;
				ConfirmingTraining ("Chemistry");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (4)") {
				AssigningTech = Tech.PeriodicTable;
				ConfirmingTraining ("PeriodicTable");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (5)") {
				AssigningTech = Tech.MagnesiumApplications;
				ConfirmingTraining ("MagnesiumApplications");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (6)") {
				AssigningTech = Tech.Biology;
				ConfirmingTraining ("Biology");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (7)") {
				AssigningTech = Tech.BodyStructure;
				ConfirmingTraining ("BodyStructure");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (8)") {
				AssigningTech = Tech.Kinetics;
				ConfirmingTraining ("Kinetics");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (9)") {
				AssigningTech = Tech.Nutrition;
				ConfirmingTraining ("Nutrition");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (10)") {
				AssigningTech = Tech.ScienceTraining;
				ConfirmingTraining ("ScienceTraining");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (11)") {
				AssigningTech = Tech.CoalApplication;
				ConfirmingTraining ("CoalApplication");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (12)") {
				AssigningTech = Tech.ChainSteel;
				ConfirmingTraining ("ChainSteel");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (13)") {
				AssigningTech = Tech.Psychology;
				ConfirmingTraining ("Psychology");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (14)") {
				AssigningTech = Tech.MindReading;
				ConfirmingTraining ("MindReading");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (15)") {
				AssigningTech = Tech.MindControl;
				ConfirmingTraining ("MindControl");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (16)") {
				AssigningTech = Tech.PaperMaking;
				ConfirmingTraining ("PaperMaking");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (17)") {
				AssigningTech = Tech.Typography;
				ConfirmingTraining ("Typography");
			} else if (EventSystem.current.currentSelectedGameObject.name == "Button (18)") {
				AssigningTech = Tech.Compass;
				ConfirmingTraining ("Compass");
			}
		} else if (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject == IChingHolder) {
			if (EventSystem.current.currentSelectedGameObject.name == "Button") {
				AssigningTech = Tech.IChing;
				ConfirmingTraining ("IChing");
			}
		}
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
//	
//	void ChangePanel(GameObject panel1, GameObject panel2){
//		Debug.Log (panel1 + " panel is change to " + panel2);
//		panel1.SetActive (false);
//		panel2.SetActive (true);
//	}

	private void SetCharacters(){
		LoadHeadPic headPic = LoadHeadPic.Instance;
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
	}
}


public class TechItem{
	public string Item{ get; set; }
	public float Weight { get; set; }
	public List<int> TechRequirement { get; set; }
	public List<int> KnowledgeRequirement { get; set; }
	public int MinimumLevel { get ; set; }
	public int MinimumIQ { get ; set; }
	public string Usage { get; set; } 

	public TechItem(){}

	public TechItem(string i, float w, List<int> t, List<int> k, int ml, int mi, string u){
		Item = i;
		Weight = w;
		TechRequirement = t;
		KnowledgeRequirement = k;
		MinimumLevel = ml;
		MinimumIQ = mi;
		Usage = u;
	}
}

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
	public List<Button> CounselorButtons;
	public Text TotalIQText;
	public Button TechnologySelector;
	public Text TechnologyLabel;
	public Text FromLv;
	public Text ToLv;
	public Text RemainTrainingTime;

//	public GameObject CounselorSelectorHolder;
	public GameObject CounselorList;

	public GameObject WeaponTechnologyHolder;
	public GameObject PotteryHolder;
	public GameObject SeamEdgeHolder;
	public GameObject MiningHolder;
	public GameObject WoodworkerHolder;
	public GameObject BasicScienceHolder;
	public GameObject IChingHolder;
	public GameObject TreeDiagram;
	public static GameObject MessageDialog;
	public static GameObject ConfirmDialog;
	public static GameObject DisablePanel;

	public Button WeaponTechnologyButton;
	public Button PotteryButton;
	public Button SeamEdgeButton;
	public Button MiningButton;
	public Button WoodworkerButton;
	public Button BasicScienceButton;
	public Button IChingButton;
	public Button BackButton;
	public Button CloseButton;

	public static List<Button> staticCounselorButtons;
	public static Transform staticCounselorPanel;
	public static Text staticTotalIQText;
//	public static Transform staticCounselorSelectorHolder;
	public static Tech AssigningTech;
	public static TechTreeDialogCommand DialogCommand; 
	public static Button staticTechnologySelector;
	public static Text staticTechnologyLabel;
	public static Text staticFromLv;
	public static Text staticToLv;
	public static Text staticRemainTrainingTime;
	public static int[] AssigningCounselor = new int[]{0,0,0,0,0};
	public static int AssigningSlot=0;
	public static int AssigningCounselorSlot = 0;
	
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
		if (TechTreePrefab.person.Count == 0) {
			SetupStudentPrefabList ();
		}
		staticCounselorButtons = CounselorButtons;
		staticCounselorPanel = CounselorList.transform;
		staticTotalIQText = TotalIQText;
		staticTechnologyLabel = TechnologyLabel;
		staticFromLv = FromLv;
		staticToLv = ToLv;
		staticRemainTrainingTime = RemainTrainingTime;
//		staticCounselorSelectorHolder = CounselorSelectorHolder.transform;
		InvokeRepeating ("SetRemainingTime", 0, 1);
		SetupTechTreeDiagram ();
		CallTechTree ();
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
			AssigningCounselor[i] = game.trainings[43].attributes["TechTreeCounselors"][i].AsInt;
			if (AssigningCounselor[i] > 0){
			CounselorButtons[i].GetComponent<Image>().sprite = 
				imageDict[game.counselor.Find (x => x.id == AssigningCounselor[i]).attributes["type"].AsInt];
			CounselorButtons[i].transform.GetChild(0).GetComponent<Text>().text = 
				nameDict[game.counselor.Find (x => x.id == AssigningCounselor[i]).attributes["type"].AsInt];
			}
		}
		// TODO add ICON image here and after assignment
		TotalIQText.text = TotalIQOfCounselors ().ToString();
		AssigningTech = (Tech)game.trainings[43].type;
		if (game.trainings [43].etaTimestamp > DateTime.Now) {
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
		if (game.trainings[43].etaTimestamp < DateTime.Now){
			ClearUIInformation();
		}

	}

	void ClearUIInformation(){
		for (int i = 0; i < 5; i++){
			TechTree.staticCounselorButtons[i].GetComponent<Image>().sprite = null;
			TechTree.staticCounselorButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
		}
		TechTree.staticTotalIQText.text = "";
		TechTree.staticTechnologyLabel.text = "";
		TechTree.staticFromLv.text = "";
		TechTree.staticToLv.text = "";
		TechTree.staticRemainTrainingTime.text = "00:00:00";
	}
	
	void SetHeadImages(){

	}
	
	public void SetRemainingTime(){
		if (game.trainings [43].etaTimestamp > DateTime.Now) {
			RemainTrainingTime.text = Utilities.TimeUpdate.Time(game.trainings[43].etaTimestamp);
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
			if (game.trainings [43].etaTimestamp < DateTime.Now && TotalIQOfCounselors() > 0) {  // training not started
				SetupTechTreeDiagram ();
				TreeDiagram.SetActive(true);
			}else{
				if (game.trainings [43].etaTimestamp < DateTime.Now){
					Panel.GetHeader(MessageDialog).text = "未選軍師";
					Panel.GetMessageText(MessageDialog).text = "軍師閣下，請先選擇至少一個軍師";
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
		Trainings training = game.trainings[43];
		for (var i = 0 ; i < cList.Count ; i++){
			for (int j=0; j < training.attributes["TechTreeCounselors"].Count; j++){
				if (training.attributes["TechTreeCounselors"][j].AsInt == cList[i].id){
					cList.Remove(cList[i]);
				}
			}
//			if (training.trainerId == cList[i].id || training.targetId == cList[i].id){
//				//					Debug.Log ("Id of character those are training: "+Academy.cStudentList[i].id);
//				cList.Remove(cList[i]);
//			}

		}  
		cslCount = cList.Count;
		for (var i = 0 ; i < cslCount ; i++){
			//			CreateStudentItem (Academy.cStudentList[i]);
			StartCoroutine( CreateCandidateItemNew(cList[i]));
		}
		//		cslCount = Academy.cSelfLearnList.Count;
		//		for (var i = 0 ; i < cslCount ; i++){
		//			CreateSelfLearnItem (Academy.cSelfLearnList[i]);
		//		}
		
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
		if (game.trainings [43].etaTimestamp < DateTime.Now) {  // training not started
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

//			game.trainings[43].type = (int)AssigningTech;
//			game.trainings[43].targetId = 0;
//			game.trainings[43].startTimestamp = DateTime.Now;
//			game.trainings[43].trainerId = 0;
//			game.trainings[43].etaTimestamp = DateTime.Now+GetTotalTrainingHours(43);
//			game.trainings[43].status = (int)TrainingStatus.OnGoing;
//			game.trainings[43].UpdateObject();
		TechnologyLabel.text = TechItems[(int)AssigningTech].Item;
		Debug.Log ("Total Training Hours: "+GetTotalTrainingHours ());
		Panel.GetHeader (ConfirmDialog).text = "進行科研";
		Panel.GetMessageText (ConfirmDialog).text = "軍師閣下，進行科研需時 "+TimeUpdate.Time( GetTotalTrainingHours());
		Debug.Log ("Assigning Tech Level : "+ (game.login.attributes[Enum.GetName(typeof(Tech),AssigningTech)].AsInt));
		DialogCommand = TechTreeDialogCommand.ConfirmTraining;
		ConfirmDialog.SetActive (true);
	
	}

	void OnConfirmedTraining(){
		Debug.Log ("OnConfirmedTraining()");
		TreeDiagram.SetActive(false);

		game.trainings[43].type = (int)AssigningTech;
		game.trainings[43].targetId = 0;
		game.trainings[43].startTimestamp = DateTime.Now;
		game.trainings[43].trainerId = 0;
		game.trainings[43].etaTimestamp = DateTime.Now+GetTotalTrainingHours();
		game.trainings[43].status = (int)TrainingStatus.OnGoing;
		for (int i = 0; i < 5; i++) {
			game.trainings [43].attributes ["TechTreeCounselors"][i].AsInt = AssigningCounselor[i];
		}
		game.trainings[43].UpdateObject();
//		Debug.Log (game.trainings[43].ToString());
	}

	TimeSpan GetTotalTrainingHours(){
		Debug.Log ("Total Counselor IQ: " + TotalIQOfCounselors ());
		var tech = Enum.GetName (typeof(Tech), AssigningTech);
		Debug.Log (tech+" Level: "+game.login.attributes[tech].AsInt);
		float seconds = 3600 / TotalIQOfCounselors() * GetIQRequirement((int)AssigningTech,game.login.attributes[tech].AsInt) ;
		return new TimeSpan (0,0,(int)seconds);
	}

	bool isTrainingValid(Tech tech){
		int index = 43;
		List<Knowledge> knowledgeList = GetAvailableKnowlegdeList ();
		List<Tech> techList = GetAvailableTechList ();
		float highestIQ = HighestIQOfCounselors (index);
//		int level = CharacterPage.UserLevelCalculator (game.login.exp);
		int count = 0;
		TechItem techs = TechItems [(int)tech];
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
			return false;
		}

		count = techs.TechRequirement.Count;
		for (int i = 0; i < count ; i++){	
			if (!knowledgeList.Exists(x => (int)x == techs.TechRequirement[i])){
				return false;
			}
	    }
		count = techs.KnowledgeRequirement.Count;
		for (int i = 0; i < count ; i++){
			if (!knowledgeList.Exists(x => (int)x == techs.KnowledgeRequirement[i])){
				return false;
			}
		}

		return true;
	}
	
	public static int GetCurrentTech(){
		Game game = Game.Instance;
		return game.trainings [43].type;
	}


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

	float HighestIQOfCounselors(int index){
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
		int count = game.counselor.Count;
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
//			if (game.trainings[43].attributes["TechTreeCounselors"].Count==0){
//				continue;
//			}
			co = game.counselor.FindIndex (x  => x.id == AssigningCounselor[i]);
			if (co < 0){
				continue;
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Woodworker"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Woodworker)){
					list.Add (Knowledge.Woodworker);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["MetalFabrication"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.MetalFabrication)){
					list.Add (Knowledge.MetalFabrication);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["EasternHistory"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.EasternHistory)){
					list.Add (Knowledge.EasternHistory);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["WesternHistory"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.WesternHistory)){
					list.Add (Knowledge.WesternHistory);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["ChainSteel"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.ChainSteel)){
					list.Add (Knowledge.ChainSteel);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["MetalProcessing"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.MetalProcessing)){
					list.Add (Knowledge.MetalProcessing);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Crafts"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Crafts)){
					list.Add (Knowledge.Crafts);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Geometry"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Geometry)){
					list.Add (Knowledge.Geometry);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Physics"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Physics)){
					list.Add (Knowledge.Physics);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Chemistry"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Chemistry)){
					list.Add (Knowledge.Chemistry);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["PeriodicTable"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.PeriodicTable)){
					list.Add (Knowledge.PeriodicTable);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Pulley"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Pulley)){
					list.Add (Knowledge.Pulley);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Anatomy"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Anatomy)){
					list.Add (Knowledge.Anatomy);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Catapult"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Catapult)){
					list.Add (Knowledge.Catapult);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["GunpowderModulation"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.GunpowderModulation)){
					list.Add (Knowledge.GunpowderModulation);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["Psychology"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Psychology)){
					list.Add (Knowledge.Psychology);
				}
			}
			if (game.counselor[co].attributes["attributes"]["KnownKnowledge"]["IChing"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.IChing)){
					list.Add (Knowledge.IChing);
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
//		if (knowledges.Exists(x => x == Knowledge.Woodworker)){
//			WeaponTechnologyHolder.transform.GetChild(0).gameObject.SetActive(true);
//		}
//		if (tech ["WeaponTechnology"].AsInt > 0) {
//			if (knowledges.Exists(x => (int)x == KnowledgeRequire1[0]) &&
//			    knowledges.Exists(x => (int)x == KnowledgeRequire1[1]) &&
//			    knowledges.Exists(x => (int)x == KnowledgeRequire1[2])){
//				WeaponTechnologyHolder.transform.GetChild(1).gameObject.SetActive(true);
//				WeaponTechnologyHolder.transform.GetChild(2).gameObject.SetActive(true);
//			}
//		}
//		if (tech ["EasternWeaponTechnology"].AsInt > 0 && tech ["WesternWeaponTechnology"].AsInt >0 && tech["MetalFabrication"].AsInt > 0) {
//			WeaponTechnologyHolder.transform.GetChild(1).gameObject.SetActive(true);
//			WeaponTechnologyHolder.transform.GetChild(2).gameObject.SetActive(true);
//		}
		if (isTrainingValid (Tech.WeaponTechnology)) {
			WeaponTechnologyHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.EasternWeaponTechnology)) {
			WeaponTechnologyHolder.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.WesternWeaponTechnology)) {
			WeaponTechnologyHolder.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.FinePolished)) {
			WeaponTechnologyHolder.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Reinforcement)) {
			WeaponTechnologyHolder.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.EquipmentWeight)) {
			WeaponTechnologyHolder.transform.GetChild(5).gameObject.SetActive(true);
		}
		if (isTrainingValid (Tech.Tempered)) {
			WeaponTechnologyHolder.transform.GetChild(6).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Pottery)) {
			PotteryHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.MetalComponents)) {
			PotteryHolder.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Mechanical)) {
			PotteryHolder.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.SpecialProduction)) {
			PotteryHolder.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.SeamEdge)) {
			SeamEdgeHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.TanningOperation)) {
			SeamEdgeHolder.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.ArmorSystem)) {
			SeamEdgeHolder.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Mining)) {
			MiningHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Bronze)) {
			MiningHolder.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.MetalFabrication)) {
			MiningHolder.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.CastingFencing)) {
			MiningHolder.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.CastingFencing)) {
			MiningHolder.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.GunProduction)) {
			MiningHolder.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.SystemShield)) {
			MiningHolder.transform.GetChild(6).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.ProductionCrossbow)) {
			MiningHolder.transform.GetChild(7).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.WoodWorker)) {
			WoodworkerHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Geometry)) {
			BasicScienceHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Physics)) {
			BasicScienceHolder.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.BasicScience)) {
			BasicScienceHolder.transform.GetChild(2).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Chemistry)) {
			BasicScienceHolder.transform.GetChild(3).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.PeriodicTable)) {
			BasicScienceHolder.transform.GetChild(4).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.MagnesiumApplications)) {
			BasicScienceHolder.transform.GetChild(5).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Biology)) {
			BasicScienceHolder.transform.GetChild(6).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.BodyStructure)) {
			BasicScienceHolder.transform.GetChild(7).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Kinetics)) {
			BasicScienceHolder.transform.GetChild(8).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Nutrition)) {
			BasicScienceHolder.transform.GetChild(9).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.ScienceTraining)) {
			BasicScienceHolder.transform.GetChild(10).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.CoalApplication)) {
			BasicScienceHolder.transform.GetChild(11).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.ChainSteel)) {
			BasicScienceHolder.transform.GetChild(12).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Psychology)) {
			BasicScienceHolder.transform.GetChild(13).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.MindReading)) {
			BasicScienceHolder.transform.GetChild(14).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.MindControl)) {
			BasicScienceHolder.transform.GetChild(15).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.PaperMaking)) {
			BasicScienceHolder.transform.GetChild(16).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Typography)) {
			BasicScienceHolder.transform.GetChild(17).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.Compass)) {
			BasicScienceHolder.transform.GetChild(18).gameObject.GetComponent<Button>().interactable = true;
		}
		if (isTrainingValid (Tech.IChing)) {
			IChingHolder.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
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

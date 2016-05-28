using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Utilities;
using UnityEngine.UI;

public enum Tech{ WeaponTechnology, EasternWeaponTechnology, WesternWeaponTechnology,
					FinePolished, Reinforcement, EquipmentWeight, Tempered,
					Pottery, MetalComponents, Mechanical, SpecialProduction,
					SeamEdge, TanningOperation, ArmorSystem, Mining,
					Bronze, MetalFabrication, CastingFencing, GunProduction,
					Fletcher, SystemShield, ProductionCrossbow,
					WoodWorker, Geometry, Physics, BasicScience,
					Chemistry, PeriodicTable, MagnesiumApplications,
					Biology, BodyStructure, Kinetics, Nutrition,
					ScienceTraining, CoalApplicaiton, ChainSteel,
					Psychology, MindReading, MindControl, PaperMaking, 
					Typography, Compass, IChing
}

public class TechTree : MonoBehaviour {
	public GameObject WeaponTechnologyHolder;
	public GameObject PotteryHolder;
	public GameObject SeamEdgeHolder;
	public GameObject TanningOperationHolder;
	public GameObject MiningHolder;
	public GameObject WoodworkerHolder;
	public GameObject BasicScienceHolder;

	public Button WeaponTechnologyButton;
	public Button PotteryButton;
	public Button SeamEdgeButton;
	public Button TanningOperationButton;
	public Button MiningButton;
	public Button WoodworkerButton;
	public Button BasicScienceButton;




	static List<int> TechRequirementNone = new List<int>{};
	static List<int> TechRequirement1 = new List<int>{0};
	static List<int> TechRequirement2 = new List<int>{0,(int)Tech.MetalFabrication};
	static List<int> TechRequirement3 = new List<int>{0,(int)Tech.WoodWorker,(int)Tech.Bronze};
	static List<int> TechRequirement4 = new List<int>{(int)Tech.BasicScience};
	static List<int> KnowledgeRequire1 = new List<int>{2003,2004,2005};
	static List<int> KnowledgeRequire2 = new List<int>{(int)Knowledge.MetalProcessing,(int)Knowledge.Crafts,(int)Knowledge.Geometry,
		(int)Knowledge.Physics,(int)Knowledge.Chemistry,(int)Knowledge.PeriodicTable
	};
	static List<int> KnowledgeRequire3 = new List<int>{2001,2002};
	static List<int> KnowledgeRequire4 = new List<int>{2012,2013};
	public static List<TechItem> TechItems = new List<TechItem>{
		new TechItem("兵器工藝",1, TechRequirementNone,	KnowledgeRequire3,0,90,"製作武器以及兵器相關科技的前置條件。"),
		new TechItem("東亞兵器工藝",2, TechRequirement1,	KnowledgeRequire1,10, 100,"製作東亞兵器。"),
		new TechItem("西洋兵器工藝",2, TechRequirement1,	KnowledgeRequire1,10, 100,"製作西洋兵器。"),
		new TechItem("精細打磨",3, TechRequirement2,		KnowledgeRequire2,20, 110,"按級數提升武器鋒利程度(%)。"),
		new TechItem("加固",3, TechRequirement2,			KnowledgeRequire2,20, 110,"按級數加固裝甲及盾(%)。"),
		new TechItem("裝備減重",3, TechRequirement2,KnowledgeRequire2,20, 110,"按級數減少所有裝備重量(%)。"),
		new TechItem("千錘百鍊",5, new List<int>{0,3,4,5,(int)Tech.MetalFabrication,26},new List<int>{(int)Knowledge.Catapult,(int)Knowledge.GunpowderModulation,(int)Knowledge.Psychology},40, 130,"為將士製作超凡兵器。"),
		new TechItem("陶藝",1, new List<int>(),KnowledgeRequire3,0, 90,"其他科技的前置條件。"),
		new TechItem("金屬組件",2, new List<int>{7,(int)Tech.MetalFabrication},KnowledgeRequire1,10, 100,"其他科技的前置條件。"),
		new TechItem("機械",3, new List<int>{8,(int)Tech.Physics},KnowledgeRequire2,20, 110,"製作裝備的前置條件。"),
		new TechItem("特殊製作",3, new List<int>{8},		KnowledgeRequire2,20, 110,"製作裝備的前置條件。"),
		new TechItem("縫刃",1, TechRequirementNone,		KnowledgeRequire3,0, 90,"製作裝備的前置條件。"),
		new TechItem("製革術",1, new List<int>{0,(int)Tech.SeamEdge},KnowledgeRequire3,0, 90,"製作裝備及其他科技的前置條件。"),
		new TechItem("製甲術",3, new List<int>{0,(int)Tech.TanningOperation,(int)Tech.Bronze},KnowledgeRequire2,20, 110,"製作裝備的前置條件。"),
		new TechItem("採礦",1, TechRequirementNone,		KnowledgeRequire3,0, 90,"其他科技的前置條件。"),
		new TechItem("青銅器",1.5f, new List<int>{(int)Tech.Mining},KnowledgeRequire3,5, 95,"製作裝備及其他科技的前置條件。"),
		new TechItem("冶鐵",2, new List<int>{(int)Tech.Bronze},KnowledgeRequire1,10, 100,"製作裝備及其他科技的前置條件。"),
		new TechItem("鑄劍術",4, new List<int>{0,(int)Tech.Bronze},KnowledgeRequire4, 30, 120,"製作裝備的前置條件。"),
		new TechItem("槍製作",3, TechRequirement3,		KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("造箭",2, TechRequirement3,			KnowledgeRequire1, 10, 100,"製作裝備的前置條件。"),
		new TechItem("製盾術",3, TechRequirement3,		KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("弓弩製作",3, new List<int>{0,(int)Tech.WoodWorker,(int)Tech.Bronze,(int)Tech.Physics},KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
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
		new TechItem("鍊鋼",3, new List<int>{(int)Tech.MagnesiumApplications,(int)Tech.CoalApplicaiton},KnowledgeRequire2, 20, 110,"製作裝備的前置條件。"),
		new TechItem("心理學",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"其他科技的前置條件。"),
		new TechItem("讀心術",4, new List<int>{(int)Tech.Psychology},KnowledgeRequire4, 30, 120,"按等級揭示敵軍佈陣(最多50%)。"),
		new TechItem("心靈操控",5, new List<int>{(int)Tech.MindReading},new List<int>{(int)Knowledge.Catapult,(int)Knowledge.GunpowderModulation,(int)Knowledge.Psychology}, 40, 130,"按等級提升自軍士氣、減低敵軍士氣、提升叫囂效果。"),
		new TechItem("造紙術",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"按等級增加在學堂學習的速度(%)。"),
		new TechItem("印刷術",3, new List<int>{(int)Tech.PaperMaking,(int)Tech.Mechanical},KnowledgeRequire2, 20, 110,"按等級增加在學堂學習的速度(%)。"),
		new TechItem("指南針",3, TechRequirement4,		KnowledgeRequire2, 20, 110,"增強偵察兵效能。"),
		new TechItem("易學",7, TechRequirementNone,		new List<int>{(int)Knowledge.IChing}, 60, 170,"按等級揭示敵軍佈陣(最多108%)。")
	};

	Game game;
	public static Tech AssigningTech;
	public static int AssigningSlot=0;

	static int[] LevelIQPercentage = {0,1,2,4,6,8,10,12,14,18,25};

	public static int GetIQRequirement(int tech, int level){
		int totalIQ = 2160000;
		Debug.Log ("Total IQ: " + totalIQ * (TechItems [tech].Weight / 116));
		Debug.Log ("Level IQ: " + LevelIQPercentage [level]);
		return Mathf.RoundToInt (totalIQ*(TechItems[tech].Weight/116)*(LevelIQPercentage[level]/100f));
	}


	// Use this for initialization
	void Start(){
		CallTechTree (); 
	}

	void OnEnable () {
		game = Game.Instance;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void CallTechTree(){
		AddButtonListener ();
	}

	// Button Listener
	void AddButtonListener(){
		WeaponTechnologyButton.onClick.AddListener (() => {ChangeHolder("WeaponTechnology");});
		PotteryButton.onClick.AddListener (() => {ChangeHolder("Pottery");});
		SeamEdgeButton.onClick.AddListener (() => {ChangeHolder("SeamEdge");});
		TanningOperationButton.onClick.AddListener (() => {ChangeHolder("TanningOperationButton");});
		MiningButton.onClick.AddListener (() => {ChangeHolder("Mining");});
		WoodworkerButton.onClick.AddListener (() => {ChangeHolder("Woodworker");});
		BasicScienceButton.onClick.AddListener (() => {ChangeHolder("BasicScience");});
	}

	void ChangeHolder(string holder){
		WeaponTechnologyHolder.SetActive (false);
		PotteryHolder.SetActive (false);
		SeamEdgeHolder.SetActive (false);
		TanningOperationHolder.SetActive (false);
		MiningHolder.SetActive (false);
		WoodworkerHolder.SetActive (false);
		BasicScienceHolder.SetActive (false);
		if (holder == "WeaponTechnology") {
			WeaponTechnologyHolder.SetActive (true);
		}else if (holder == "Pottery") {
			PotteryHolder.SetActive (true);
		}else if (holder == "SeamEdge") {
			SeamEdgeHolder.SetActive (true);
		}else if (holder == "TanningOperation") {
			TanningOperationHolder.SetActive (true);
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



	void ConfirmTraining(){
		int slot = AssigningSlot + 43;
		if (isTrainingValid()) {
			game.trainings[slot].type = (int)AssigningTech;
			game.trainings[slot].targetId = 0;
			game.trainings[slot].startTimestamp = DateTime.Now;
			game.trainings[slot].trainerId = 0;
			game.trainings[slot].etaTimestamp = DateTime.Now+GetTotalTrainingHours(slot);
			game.trainings[slot].status = (int)TrainingStatus.OnGoing;
			game.trainings[slot].UpdateObject();
		}
	}

	TimeSpan GetTotalTrainingHours(int index){
		float ticks = 36000000000 * TotalIQOfCounselors(index) / GetIQRequirement((int)AssigningTech,CharacterPage.UserLevelCalculator(game.login.exp)) ;
		return new TimeSpan ((int)ticks);
	}

	bool isTrainingValid(){
		int index = AssigningSlot + 43;
		List<Knowledge> knowledgeList = GetAvailableKnowlegdeList ();
		List<Tech> techList = GetAvailableTechList ();
		float highestIQ = HighestIQOfCounselors (index);
		int level = CharacterPage.UserLevelCalculator (game.login.exp);
		int count = 0;
		TechItem tech = TechItems [(int)AssigningTech];

		if (tech.MinimumIQ > highestIQ || tech.MinimumLevel > level) {
			return false;
		}

		count = tech.TechRequirement.Count;
		for (int i = 0; i < count ; i++){	
			if (!knowledgeList.Exists(x => (int)x == tech.KnowledgeRequirement[i])){
				return false;
			}
	    }
		count = tech.KnowledgeRequirement.Count;
		for (int i = 0; i < count ; i++){
			if (!knowledgeList.Exists(x => (int)x == tech.KnowledgeRequirement[i])){
				return false;
			}
		}

		return true;
	}
	

	float TotalIQOfCounselors(int index){
		float totalIQ = 0;
		int count = game.trainings [index].attributes ["TechTreeCounselors"].Count;
		for (int i = 0; i < count; i++) {
			totalIQ += game.counselor.Find (x => x.id == game.trainings [index].attributes ["TechTreeCounselors"][i].AsInt).attributes["IQ"].AsFloat;
		}
		return totalIQ;  
	}

	float HighestIQOfCounselors(int index){
		float highestIQ = 0;
		int count = game.trainings [index].attributes ["TechTreeCounselors"].Count;
		Counselor c;
		for (int i = 0; i < count; i++) {
			c = game.counselor.Find(x => x.id == game.trainings[index].attributes["TechTreeCounselors"][i].AsInt);
			if (c.attributes["attributes"]["IQ"].AsFloat > highestIQ){
				highestIQ = c.attributes["attributes"]["IQ"].AsFloat;
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
//		for (int i = 0; i < count; i++) {
			for (int j = 0; j < numOfEnum ; j++){
			    if (game.login.attributes[Enum.GetName(typeof(Tech),(Tech)j)].AsInt > 0){
					if (!list.Exists(x => x == (Tech)j)){
						list.Add ((Tech)j);
					}
				}	
			}
//			if (game.login.attributes["WeaponTechnology"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.WeaponTechnology)){
//					list.Add (Tech.WeaponTechnology);
//				}
//			}
//			if (game.login.attributes["EasternWeaponTechnology"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.EasternWeaponTechnology)){
//					list.Add (Tech.EasternWeaponTechnology);
//				}
//			}
//			if (game.login.attributes["EasternWeaponTechnology"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.WesternWeaponTechnology)){
//					list.Add (Tech.WesternWeaponTechnology);
//				}
//			}
//			if (game.login.attributes["FinePolished"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.FinePolished)){
//					list.Add (Tech.FinePolished);
//				}
//			}
//			if (game.login.attributes["Reinforcement"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Reinforcement)){
//					list.Add (Tech.Reinforcement);
//				}
//			}
//			if (game.login.attributes["EquipmentWeight"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.EquipmentWeight)){
//					list.Add (Tech.EquipmentWeight);
//				}
//			}
//			if (game.login.attributes["Tempered"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Tempered)){
//					list.Add (Tech.Tempered);
//				}
//			}
//			if (game.login.attributes["Pottery"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Pottery)){
//					list.Add (Tech.Pottery);
//				}
//			}
//			if (game.login.attributes["SpecialProduction"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.SpecialProduction)){
//					list.Add (Tech.SpecialProduction);
//				}
//			}
//			if (game.login.attributes["SeamEdge"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.SeamEdge)){
//					list.Add (Tech.SeamEdge);
//				}
//			}
//			if (game.login.attributes["TanningOperation"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.TanningOperation)){
//					list.Add (Tech.TanningOperation);
//				}
//			}
//			if (game.login.attributes["ArmorSystem"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.ArmorSystem)){
//					list.Add (Tech.ArmorSystem);
//				}
//			}
//			if (game.login.attributes["Mining"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Mining)){
//					list.Add (Tech.Mining);
//				}
//			}
//			if (game.login.attributes["Bronze"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Bronze)){
//					list.Add (Tech.Bronze);
//				}
//			}
//			if (game.login.attributes["MetalFabrication"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.MetalFabrication)){
//					list.Add (Tech.MetalFabrication);
//				}
//			}
//			if (game.login.attributes["CastingFencing"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.CastingFencing)){
//					list.Add (Tech.CastingFencing);
//				}
//			}
//			if (game.login.attributes["Fletcher"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Fletcher)){
//					list.Add (Tech.Fletcher);
//				}
//			}
//			if (game.login.attributes["SystemShield"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.SystemShield)){
//					list.Add (Tech.SystemShield);
//				}
//			}
//			if (game.login.attributes["ProductionCrossbow"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.ProductionCrossbow)){
//					list.Add (Tech.ProductionCrossbow);
//				}
//			}
//			if (game.login.attributes["WoodWorker"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.WoodWorker)){
//					list.Add (Tech.WoodWorker);
//				}
//			}
//			if (game.login.attributes["Geometry"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Geometry)){
//					list.Add (Tech.Geometry);
//				}
//			}
//			if (game.login.attributes["Physics"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Physics)){
//					list.Add (Tech.Physics);
//				}
//			}
//			if (game.login.attributes["BasicScience"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.BasicScience)){
//					list.Add (Tech.BasicScience);
//				}
//			}
//			if (game.login.attributes["Chemistry"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Chemistry)){
//					list.Add (Tech.Chemistry);
//				}
//			}
//			if (game.login.attributes["PeriodicTable"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.PeriodicTable)){
//					list.Add (Tech.PeriodicTable);
//				}
//			}
//			if (game.login.attributes["MagnesiumApplications"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.MagnesiumApplications)){
//					list.Add (Tech.MagnesiumApplications);
//				}
//			}
//			if (game.login.attributes["Biology"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Biology)){
//					list.Add (Tech.Biology);
//				}
//			}
//			if (game.login.attributes["BodyStructure"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.BodyStructure)){
//					list.Add (Tech.BodyStructure);
//				}
//			}
//			if (game.login.attributes["Kinetics"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Kinetics)){
//					list.Add (Tech.Kinetics);
//				}
//			}
//			if (game.login.attributes["Nutrition"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Nutrition)){
//					list.Add (Tech.Nutrition);
//				}
//			}
//			if (game.login.attributes["ScienceTraining"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.ScienceTraining)){
//					list.Add (Tech.ScienceTraining);
//				}
//			}
//			if (game.login.attributes["CoalApplicaiton"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.CoalApplicaiton)){
//					list.Add (Tech.CoalApplicaiton);
//				}
//			}
//			if (game.login.attributes["ChainSteel"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.ChainSteel)){
//					list.Add (Tech.ChainSteel);
//				}
//			}
//			if (game.login.attributes["Psychology"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Psychology)){
//					list.Add (Tech.Psychology);
//				}
//			}
//			if (game.login.attributes["MindReading"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.MindReading)){
//					list.Add (Tech.MindReading);
//				}
//			}
//			if (game.login.attributes["MindControl"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.MindControl)){
//					list.Add (Tech.MindControl);
//				}
//			}
//			if (game.login.attributes["PaperMaking"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.PaperMaking)){
//					list.Add (Tech.PaperMaking);
//				}
//			}
//			if (game.login.attributes["Typography"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Typography)){
//					list.Add (Tech.Typography);
//				}
//			}
//			if (game.login.attributes["Compass"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.Compass)){
//					list.Add (Tech.Compass);
//				}
//			}
//			if (game.login.attributes["IChing"].AsInt > 0){
//				if (!list.Exists(x => x == Tech.IChing)){
//					list.Add (Tech.IChing);
//				}
//			}
			if (list.Count==numOfEnum){
				return list;
			}
//		}
		return list;
	}


	public static List<Knowledge> GetAvailableKnowlegdeList(){
		Game game = Game.Instance;
		int numOfEnum = Enum.GetNames (typeof(Knowledge)).Length;
		List<Knowledge> list = new List<Knowledge> ();
		int count = game.counselor.Count;
		for (int i = 0; i < count; i++) {
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Woodworker"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Woodworker)){
					list.Add (Knowledge.Woodworker);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["MetalFabrication"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.MetalFabrication)){
					list.Add (Knowledge.MetalFabrication);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["EasternHistory"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.EasternHistory)){
					list.Add (Knowledge.EasternHistory);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["WesternHistory"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.WesternHistory)){
					list.Add (Knowledge.WesternHistory);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["ChainSteel"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.ChainSteel)){
					list.Add (Knowledge.ChainSteel);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["MetalProcessing"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.MetalProcessing)){
					list.Add (Knowledge.MetalProcessing);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Crafts"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Crafts)){
					list.Add (Knowledge.Crafts);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Geometry"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Geometry)){
					list.Add (Knowledge.Geometry);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Physics"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Physics)){
					list.Add (Knowledge.Physics);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Chemistry"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Chemistry)){
					list.Add (Knowledge.Chemistry);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["PeriodicTable"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.PeriodicTable)){
					list.Add (Knowledge.PeriodicTable);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Pulley"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Pulley)){
					list.Add (Knowledge.Pulley);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Anatomy"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Anatomy)){
					list.Add (Knowledge.Anatomy);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Catapult"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Catapult)){
					list.Add (Knowledge.Catapult);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["GunpowderModulation"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.GunpowderModulation)){
					list.Add (Knowledge.GunpowderModulation);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["Psychology"].AsInt > 0){
				if (!list.Exists(x => x == Knowledge.Psychology)){
					list.Add (Knowledge.Psychology);
				}
			}
			if (game.counselor[i].attributes["attributes"]["KnownKnowledge"]["IChing"].AsInt > 0){
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

//	void ShowPanel(GameObject panel){
//		DisablePanel.SetActive (true);
//		Debug.Log ("ShowPanel");
//		panel.SetActive (true);
//	}
//	
//	void HidePanel(GameObject panel){
//		DisablePanel.SetActive (false);
//		Debug.Log ("HidePanel");
//		panel.SetActive (false);
//	}
//	
//	void ChangePanel(GameObject panel1, GameObject panel2){
//		Debug.Log (panel1 + " panel is change to " + panel2);
//		panel1.SetActive (false);
//		panel2.SetActive (true);
//	}
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

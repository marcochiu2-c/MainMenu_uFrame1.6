//#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using SimpleJSON;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public enum ActivePopupEnum{
	none,IQPopup,CommandedPopup,KnowledgePopup,FightingPopup
}

public class Academy : MonoBehaviour
{


	class ItemData
	{
		public float SliderValue;
		public string ImageKey;
	}

	public Button[] buttons = new Button[4];
	string[] btnName = new string[4];
	public Button[] qaButtons = new Button[2];
	string[] qaBtnName = new string[2];
	public Button closeButton;
	public Button backButton;
	public GameObject AcademyHolder;
	public GameObject SelfStudyHolder;
	public GameObject TeachHolder;
	public GameObject QAHolder;
	public GameObject KnowledgeListHolder;
	public GameObject ConfirmTeacherBy;
	public GameObject LowerThanTrainer;
	public Transform TeachScrollPanel;
	public Transform StudentScrollPanel;
	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;
	public static GameObject staticTeachHolder;
	public static List<Counselor> cStudentList;
	bool firstCalled = false;
	public int TrainingTimeTaught = 10;  // in hours
	public int TrainingCoolDownPeriod = 24; // in hours, cannot train again in the same slot for given hours.
	public static GameObject staticAcademyHolder;
	WsClient wsc;
	Game game;
//	public ListView.ColumnHeaderCollection ListViewColumns;
//	public ListView.ListViewItemCollection ListViewItems;

	public static ActivePopupEnum activePopup;
	Dictionary<ActivePopupEnum,string> academyCategoryText = new Dictionary<ActivePopupEnum, string>();
	Dictionary<string,ActivePopupEnum> activePopupName = new Dictionary<string, ActivePopupEnum>();
	private const int columnWidthCount = 5;
	List<TechTreeObject> techTreeList = new List<TechTreeObject> ();
	int numberOfTech = 0;

	// Use this for initialization
	void Start(){
		CallAcademy ();
	}

	public void CallAcademy(){
		staticAcademyHolder = AcademyHolder;
		btnName [0] = "IQButton";
		btnName [1] = "CommandedButton";
		btnName [2] = "KnowledgeButton";
		btnName [3] = "FightingButton";
		qaBtnName [0] = "SelfStudyButton";
		qaBtnName [1] = "TeachButton";
		SetDictionary ();
		Debug.Log ("Supposed to be AcademyHolder: "+transform);
		Debug.Log ("Supposed to be base panel of AcademyHolder: "+transform.GetChild(1));

		AddButtonListener ();
		KnowledgeOption.AssignButton (KnowledgeListHolder);
		KnowledgeOption.AddButtonListener (KnowledgeListHolder,gameObject);
		Debug.Log (KnowledgeOption.Woodworker);
		Academy.staticTeachHolder = TeachHolder;



		wsc = WsClient.Instance;
		game = Game.Instance;
		cStudentList = new List<Counselor> ();
		techTreeList = TechTreeObject.GetList (1);
		numberOfTech = techTreeList.Count;


		SetCharacters ();

		AcademyTeach.commonPanel = TeachScrollPanel;
		AcademyStudent.commonPanel = StudentScrollPanel;
		
		#region setListOfTrainings
		List<Trainings> tList = game.trainings;

		var tlCount = tList.Count;
		
		for (int k = 0 ; k < tlCount ; k++){
			
		}
		
		#endregion
		

		SetupStudentPrefabList ();
		SetupAcademyPanel ();
		SetTeachItems (tList);


		//CreateTeachItems ("IQ", 5);
	}

	public void SetupStudentPrefabList(){
		for (int j = 0 ; j < game.counselor.Count; j++){
			cStudentList.Add (new Counselor(game.counselor[j]));
		}
		Debug.Log ("Number of Counselors: "+game.counselor.Count);

		int cslCount = 0; 
		List<Trainings> tList = game.trainings;
		var tlCount = tList.Count;
		for (var i = 0 ; i < Academy.cStudentList.Count ; i++){
			for (var j = 0 ; j < tlCount ; j++){
				if (tList[j].trainerId == Academy.cStudentList[i].id || tList[j].targetId == Academy.cStudentList[i].id){
//					Debug.Log ("Id of character those are training: "+Academy.cStudentList[i].id);
					Academy.cStudentList.Remove(Academy.cStudentList[i]);
				}
			}
		}  
		cslCount = Academy.cStudentList.Count;
		for (var i = 0 ; i < cslCount ; i++){
			CreateStudentItem (Academy.cStudentList[i]);
		}
	}

	public void CreateStudentItem(Counselor character){
		var type = character.type;
		AcademyStudent obj = Instantiate(Resources.Load("AcademyStudentPrefab") as GameObject).GetComponent<AcademyStudent>();
		obj.characterType = character.type;
		obj.characterId = character.id;
		obj.StudentPic =  imageDict[type];
		obj.StudentImageText.text = nameDict[type];
		AcademyStudent.Students.Add (obj);
	}

	public void SetTeachItems(List<Trainings> tr){
		Game game = Game.Instance;
		Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge ();
		AcademyTeach si = new AcademyTeach();

		int trainCount = 20;
		for (int i =0; i< trainCount; i++) {
			int trainingType = tr[i].type;
			int trainerId = tr[i].trainerId;
			int targetId = tr[i].targetId;
//		GameObject sp=null;
			if (i<5) {
				si = AcademyTeach.IQTeach[i];
				if(tr[i].targetId != 0){
					si.KnowledgeText.text = (game.counselor[game.counselor.FindIndex(x => x.id == tr[i].targetId)].attributes["attributes"]["IQ"].AsFloat+1).ToString();
				}
			} else if (i> 4 && i<10) {
				si = AcademyTeach.CommandedTeach[i-5];
				if(tr[i].targetId != 0){
					Debug.Log (game.counselor.FindIndex(x => x.id == 3))	;
					Debug.Log (game.counselor[game.counselor.FindIndex(x => x.id == tr[i].targetId)].attributes["attributes"]["Leadership"].AsFloat)	;
					si.KnowledgeText.text = (game.counselor[game.counselor.FindIndex(x => x.id == tr[i].targetId)].attributes["attributes"]["Leadership"].AsFloat+1).ToString();
				}
			} else if (i> 9 && i<15) {
				si = AcademyTeach.KnowledgeTeach[i-10];
			} else if (i> 14 && i<20) {
				si = AcademyTeach.FightingTeach[i-15];
			}
			si.trainingObject = game.trainings[i];
			si.etaTimestamp = tr[i].etaTimestamp;
			if (trainerId != 0 && targetId !=0){
				si.TeacherPic = imageDict [trainerId];
			    si.TeacherImageText.text = nameDict [trainerId];
				si.StudentPic = imageDict [targetId];
				si.StudentImageText.text = nameDict [targetId];
				si.trainerId = trainerId;

//				si.TeacherDropZone.enabled = false;
//				si.StudentDropZone.enabled = false;
				si.isDropZoneEnabled = false;

				si.targetId = targetId;
				si.trainingType = tr[i].type;
			}else{
				si.TeacherPic = null;
				si.TeacherImageText.text = nameDict [trainerId];
				si.StudentPic = null;
				si.StudentImageText.text = nameDict [targetId];
				si.isDropZoneEnabled = true;
			}
			if (i<5) {
				AcademyTeach.IQTeach[i] = si;
			} else if (i> 4 && i<10) {
				AcademyTeach.CommandedTeach[i-5] = si;
			} else if (i> 9 && i<15) {
				AcademyTeach.KnowledgeTeach[i-10] = si;
			} else if (i> 14 && i<20) {
				AcademyTeach.FightingTeach[i-15] = si;
			}

		}
	}

	public void SetupAcademyPanel(){
		for (var i = 0; i < 5; i++) {
			AcademyTeach.IQTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
			AcademyTeach.CommandedTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
			AcademyTeach.KnowledgeTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
			AcademyTeach.FightingTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
		}
	}


	public void SetPanelActive (string panel){
		if (panel == "IQ") {
			AcademyTeach.showPanelItems(AcademyTeach.IQTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.FightingTeach);
		}else if (panel == "Commanded") {
			AcademyTeach.hidePanelItems(AcademyTeach.IQTeach);
			AcademyTeach.showPanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.FightingTeach);
		}else if (panel == "Knowledge") {
			AcademyTeach.hidePanelItems(AcademyTeach.IQTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.showPanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.FightingTeach);
		}else if (panel == "Fighting") {
			AcademyTeach.hidePanelItems(AcademyTeach.IQTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.showPanelItems(AcademyTeach.FightingTeach);
		}
		AcademyStudent.showPanelItems(AcademyStudent.Students);
		SetStudentImageText (panel);
	}

	void SetStudentImageText(string panel){
		int count = AcademyStudent.Students.Count;

		List<AcademyStudent> s = AcademyStudent.Students;
		for (var i =0; i < count; i++) {
			 s[i].SetAttributeText(panel);
		}
	}


	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
//		if (!firstCalled) {
//			Invoke("CallAcademy",1);
//		}
//		AcademyTeach.setDropZone(AcademyTeach.IQTeach);
//		AcademyTeach.setDropZone(AcademyTeach.CommandedTeach);
//		AcademyTeach.setDropZone(AcademyTeach.KnowledgeTeach);
//		AcademyTeach.setDropZone(AcademyTeach.FightingTeach);


		// re-setup while no student prefab exist since prefabs destroyed while panel close
		if (gameObject.activeSelf == true && AcademyStudent.Students.Count == 0 && game.counselor.Count != 0){
			SetupStudentPrefabList();
		}
	}

	public void SetPanelParent(string panelToShow){
		var count = 0;
		if (panelToShow == "IQ") {
			count = AcademyTeach.IQTeach.Count;
			for (var i = 0; i < count; i++){

			}
		}
	}



	void OnButtonClick(Button btn){
//		De
		Debug.Log ("Button in Academy clicked");
		GameObject selfStudyPopup = GameObject.Find ("SelfStudyHolder");
		GameObject teacherPopup = GameObject.Find ("TeachHolder");
//		SelfStudyHolder.SetActive (true);
//		TeachHolder.SetActive (true);
		QAHolder.SetActive (true);
		Academy.activePopup = activePopupName[btn.name];
		Debug.Log (Academy.activePopup);
	}

	void OnQAButtonClick(Button btn){
		QAHolder.SetActive (false);
		if (btn.name == "SelfStudyButton") {
			SelfStudyHolder.SetActive(true);
		} else {
			TeachHolder.SetActive(true);
		}

		SelfStudyHolder.transform.Find("Popup/Text").gameObject.GetComponent<Text>().text = academyCategoryText[Academy.activePopup];
		TeachHolder.transform.Find("Header").gameObject.GetComponent<Text>().text = academyCategoryText[Academy.activePopup];

		if (Academy.activePopup == ActivePopupEnum.IQPopup) {
			SetPanelActive ("IQ");
			SetTaughtHeaderTargetText ("IQ");
		} else if (Academy.activePopup == ActivePopupEnum.CommandedPopup) {
			SetPanelActive ("Commanded");
			SetTaughtHeaderTargetText ("Commanded");
		} else if (Academy.activePopup == ActivePopupEnum.KnowledgePopup) {
			SetPanelActive ("Knowledge");
			SetTaughtHeaderTargetText ("Knowledge");
		} else if (Academy.activePopup == ActivePopupEnum.FightingPopup) {
			SetPanelActive ("Fighting");
			SetTaughtHeaderTargetText ("Fighting");
		}

	}

	void SetTaughtHeaderTargetText(string header){
		string txt = "";
		if (header == "Knowledge") {
			txt = "學問";
		} else {
			txt = "期望值";
		}
		TeachHolder.transform.GetChild (1).GetChild (0).GetChild (2).GetComponent<Text> ().text = txt;
	}

	void AddButtonListener(){
		for (var i = 0; i < 4; i++) {
			Transform child = buttons [i].transform;
			buttons[i].onClick.AddListener(() => { OnButtonClick(child.GetComponent<Button>()); });
		}
		for (var i = 0; i < 2; i++) {
			Transform child = qaButtons [i].transform;
			qaButtons [i].onClick.AddListener (() => {
				OnQAButtonClick (child.GetComponent<Button> ()); });
		}
		
		backButton.onClick.AddListener (() => {
			Academy.activePopup = ActivePopupEnum.none;
			SelfStudyHolder.SetActive(false);
			TeachHolder.SetActive(false);
			gameObject.SetActive(true);
		});
		closeButton.onClick.AddListener (() => {
			var count = AcademyStudent.Students.Count;
			for (int i = 0 ; i < count ; i++){
//				GameObject.DestroyImmediate( StudentScrollPanel.transform.GetChild(0).gameObject);
				GameObject.DestroyImmediate( AcademyStudent.Students[i].gameObject);
				AcademyStudent.Students.Remove(AcademyStudent.Students[i].gameObject.GetComponent<AcademyStudent>());
			}
			AcademyStudent.Students = new List<AcademyStudent>();
			Academy.activePopup = ActivePopupEnum.none;
			SelfStudyHolder.SetActive(false);
			TeachHolder.SetActive(false);
			AcademyHolder.SetActive(false);
			MainScene.MainUIHolder.SetActive(true);
		});
		KnowledgeListHolder.transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (() => {
			KnowledgeListHolder.SetActive(false);
			ResetTeachPanel();
		});

		Utilities.Panel.GetConfirmButton(LowerThanTrainer).onClick.AddListener (() => {
			LowerThanTrainer.SetActive(false);
			ResetTeachPanel();
//			Trainings obj  = AcademyStudent.currentTeachItem.trainingObject;
//			int index = game.trainings.FindIndex(x => x == obj);
//			if (index < 5){
//				SetStudentImageText ("IQ");
//			}else if (index > 4 && index < 10){
//				SetStudentImageText ("Commaned");
//			}else if (index > 9 && index < 15){
//				SetStudentImageText ("Knowledge");
//			}else if (index > 14 && index < 20){
//				SetStudentImageText ("Fighting");
//			}

			if (AcademyStudent.IsLevelNotReach){
				AcademyStudent.reCreateStudentItem(AcademyStudent.currentStudentPrefab.GetComponent<AcademyStudent>(),false);
				GameObject.Destroy(AcademyStudent.currentStudentPrefab);
				AcademyStudent.Students.Remove(AcademyStudent.currentStudentPrefab.GetComponent<AcademyStudent>());
				AcademyStudent.IsLevelNotReach = false;
			}

		});

		Utilities.Panel.GetConfirmButton (ConfirmTeacherBy).onClick.AddListener (() => {
			ConfirmTeacherBy.SetActive(false);
			//TODO set training object
			Trainings obj  = AcademyStudent.currentTeachItem.trainingObject;
			int index = game.trainings.FindIndex(x => x == obj);
			Debug.Log("Training item index in DB: "+obj.id);
			game.trainings[index].trainerId = AcademyStudent.currentTeachItem.trainerId;
			game.trainings[index].targetId = AcademyStudent.currentTeachItem.targetId;
			game.trainings[index].startTimestamp = DateTime.Now;
			game.trainings[index].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught,0,0);
			game.trainings[index].status = 1;
			if (index < 5){
				game.trainings[index].type = 1;
				AcademyStudent.currentTeachItem.KnowledgeText.text = (game.counselor[game.counselor.FindIndex(x => x.id == AcademyStudent.currentTeachItem.targetId)].attributes["attributes"]["IQ"].AsInt+1).ToString();
			}else if (index > 4 && index < 10){
				game.trainings[index].type = 2;
				AcademyStudent.currentTeachItem.KnowledgeText.text = (game.counselor[game.counselor.FindIndex(x => x.id == AcademyStudent.currentTeachItem.targetId)].attributes["attributes"]["Leadership"].AsInt+1).ToString();
			}else if (index > 9 && index < 15){
				game.trainings[index].type = KnowledgeOption.knowledge;
				Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
				AcademyStudent.currentTeachItem.KnowledgeText.text = String.Format ("{0}({1})",
								KnowledgeDict[KnowledgeOption.knowledge],
				                game.counselor.Find(x => x.id == game.trainings[index].targetId).attributes["attributes"]["KnownKnowledge"][KnowledgeOption.knowledgeName]
				); 
			}else if (index > 14 && index < 20){

			}
			Debug.Log (game.trainings[index].toJSON());
			game.trainings[index].UpdateObject();
			AcademyStudent.currentTeachItem.LeftTimeText.text = Utilities.TimeUpdate.Time (game.trainings[index].etaTimestamp);
			KnowledgeOption.knowledge = 0;
			KnowledgeOption.knowledgeName ="";
		});

		Utilities.Panel.GetCancelButton (ConfirmTeacherBy).onClick.AddListener (() => {
			ConfirmTeacherBy.SetActive(false);
			ResetTeachPanel();
		});
	}

	void ResetTeachPanel(){
		AcademyStudent.reCreateStudentItem(AcademyStudent.currentTeachItem);
		AcademyStudent.currentTeachItem.TeacherImage.sprite = null;
		AcademyStudent.currentTeachItem.StudentImage.sprite = null;
		AcademyStudent.currentTeachItem.TeacherImageText.text = "";
		AcademyStudent.currentTeachItem.StudentImageText.text = "";
		AcademyStudent.currentTeachItem.KnowledgeText.text = "";
		AcademyStudent.currentTeachItem.LeftTimeText.text = "00:00:00";
		AcademyStudent.currentTeachItem.trainerId=0;
		AcademyStudent.currentTeachItem.targetId=0;
		AcademyStudent.currentTeachItem.trainingType=0;
		AcademyStudent.currentTeachItem.targetType = 0;
	}

	void SetDictionary(){
		activePopupName.Add (btnName [0], ActivePopupEnum.IQPopup);
		activePopupName.Add (btnName [1], ActivePopupEnum.CommandedPopup);
		activePopupName.Add (btnName [2], ActivePopupEnum.KnowledgePopup);
		activePopupName.Add (btnName [3], ActivePopupEnum.FightingPopup);
		academyCategoryText.Add (ActivePopupEnum.IQPopup, "智商");
		academyCategoryText.Add (ActivePopupEnum.CommandedPopup, "統率");
		academyCategoryText.Add (ActivePopupEnum.KnowledgePopup, "學問");
		academyCategoryText.Add (ActivePopupEnum.FightingPopup, "陣法");
	}

	public Sprite GeungTseNga;
	public Sprite NgHei;
	public Sprite SuenMo;

	public Sprite NgTseSeui;
	public Sprite ChoGwai;
	public Sprite GunChung;
	public Sprite FanYi;
	public Sprite ChuGotLeung;
	public Sprite SuenBan;
	public Sprite CheungLeung;
	public Sprite LauBakWan;
	public Sprite FanTseng;
	public Sprite MiuFun;
	public Sprite LauBingChung;
	public Sprite YeLuChucai;
	public Sprite FanManChing;
	public Sprite TsengKwokFan;
	public Sprite WongShekGong;
	public Sprite WaiLiuTse;
	public Sprite TinYeungJeui;
	public Sprite PongGyun;
	public Sprite PongTong;
	public Sprite SiMaYi;
	public Sprite ChowYu;
	public Sprite YuHim;
	public Sprite YeungKwokChung;
	public Sprite ChiuTim;
	public Sprite ChuBunDeui;
	public Sprite SanYeungSau;





	private void SetCharacters(){
		imageDict = new Dictionary<int,Sprite>();
		nameDict = new Dictionary<int,string> ();
		// Add some images.
		imageDict.Add(/*"姜子牙",*/ 1,GeungTseNga);
		imageDict.Add(/*"吳起 ",*/ 2, NgHei);
		imageDict.Add(/*"孫武",*/ 3,SuenMo);
		imageDict.Add(/*"伍子胥",*/ 4,NgTseSeui);
		imageDict.Add(/*"曹劌",*/ 5,ChoGwai);
		imageDict.Add(/*"管仲",*/ 6,GunChung);
		imageDict.Add(/*"范蠡",*/ 7,FanYi);
		imageDict.Add(/*"諸葛亮",*/ 8,ChuGotLeung);
		imageDict.Add(/*"孫臏",*/ 9,SuenBan);
		imageDict.Add(/*"張良",*/ 10,CheungLeung);
		imageDict.Add(/*"劉基",*/ 11,LauBakWan);
		imageDict.Add(/*"范增",*/ 12,FanTseng);
		imageDict.Add(/*"苗訓",*/ 13,MiuFun);
		imageDict.Add(/*"劉秉忠",*/ 14,LauBingChung);
		imageDict.Add(/*"耶律楚材",*/ 15,YeLuChucai);
		imageDict.Add(/*"范文程",*/ 16,FanManChing);
		imageDict.Add(/*"曾國藩",*/ 17,TsengKwokFan);
		imageDict.Add(/*"黃石公",*/ 18,WongShekGong);
		imageDict.Add(/*"尉繚子",*/ 19,WaiLiuTse);
		imageDict.Add(/*"田穰苴",*/ 20,TinYeungJeui);
		imageDict.Add(/*"龐涓",*/ 21,PongGyun);
		imageDict.Add(/*"龐統",*/ 22,PongTong);
		imageDict.Add(/*"司馬懿",*/ 23,SiMaYi);
		imageDict.Add(/*"周瑜",*/ 24,ChowYu);
		imageDict.Add(/*"于謙",*/ 25,YuHim);
		imageDict.Add(/*"楊國忠",*/ 26,YeungKwokChung);
		imageDict.Add(/*"趙括",*/ 27,ChiuTim);
		imageDict.Add(/*"朱般懟",*/ 28,ChuBunDeui);
		imageDict.Add(/*"辰漾守",*/ 29,SanYeungSau);
		nameDict.Add (0, "");
		nameDict.Add(1,"姜子牙");
		nameDict.Add(2,"吳起");
		nameDict.Add(3,"孫武");
		nameDict.Add(4,"伍子胥");
		nameDict.Add(5,"曹劌");
		nameDict.Add(6,"管仲");
		nameDict.Add(7,"范蠡");
		nameDict.Add(8,"諸葛亮");
		nameDict.Add(9,"孫臏");
		nameDict.Add(10,"張良");
		nameDict.Add(11,"劉基");
		nameDict.Add(12,"范增");
		nameDict.Add(13,"苗訓");
		nameDict.Add(14,"劉秉忠");
		nameDict.Add(15,"耶律楚材");
		nameDict.Add(16,"范文程");
		nameDict.Add(17,"曾國藩");
		nameDict.Add(18,"黃石公");
		nameDict.Add(19,"尉繚子");
		nameDict.Add(20,"田穰苴");
		nameDict.Add(21,"龐涓");
		nameDict.Add(22,"龐統");
		nameDict.Add(23,"司馬懿");
		nameDict.Add(24,"周瑜");
		nameDict.Add(25,"于謙");
		nameDict.Add(26,"楊國忠");
		nameDict.Add(27,"趙括");
		nameDict.Add(28,"朱般懟");
		nameDict.Add(29,"辰漾守");

	}
}


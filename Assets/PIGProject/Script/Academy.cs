//TODO Delete CommandedPopup, KnowledgePopup and FightingPopup and events in 4 buttons

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
	public Transform TeachScrollPanel;
	public Transform StudentScrollPanel;
	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;
	public static GameObject staticTeachHolder;
	public static List<Counselor> cStudentList;
	bool firstCalled = false;
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

		btnName [0] = "IQButton";
		btnName [1] = "CommandedButton";
		btnName [2] = "KnowledgeButton";
		btnName [3] = "FightingButton";
		qaBtnName [0] = "SelfStudyButton";
		qaBtnName [0] = "TeachButton";
		SetDictionary ();
		Debug.Log ("Supposed to be AcademyHolder: "+transform);
		Debug.Log ("Supposed to be base panel of AcademyHolder: "+transform.GetChild(1));
//		for (var i = 0; i <4; i++) {  // set Buttons array by script
//			buttons[i] = transform.GetChild(1).GetChild(1).GetChild(i).GetComponent<Button>();
//		}
		AddButtonListener ();


		Academy.staticTeachHolder = TeachHolder;



		wsc = WsClient.Instance;
		game = Game.Instance;

		techTreeList = TechTreeObject.GetList (1);
		numberOfTech = techTreeList.Count;
		
		//		JSONNode json = new JSONClass ();
//
//		if (wsc.conn.IsAlive) {
//
//			json ["data"] = "1"; //game.login.id;
//
//			json ["action"] = "GET";
//			json ["table"] = "users";
//			wsc.conn.Send (json.ToString ());
//
//			json ["data"] = "1";
//			json ["table"] = "getCounselorInfoByUserId";
//			wsc.conn.Send (json.ToString ());
//
//			json ["table"] = "generals";
//			wsc.conn.Send (json.ToString ());
//		} else {
//			Debug.Log ("Websocket Connection Lost!");
//		}

		SetCharacters ();

		AcademyTeach.commonPanel = TeachScrollPanel;
		AcademyStudent.commonPanel = StudentScrollPanel;
		
		#region setListOfTrainings
		List<Trainings> tList = game.trainings;
#if TEST
		tList.Add (new Trainings(2,2001,20,12, DateTime.Now, new DateTime(2016,4,1,0,0,0),1));
		tList.Add (new Trainings(3,2002,21,15, DateTime.Now, new DateTime(2016,4,1,0,0,0),1));
#else

#endif
		var tlCount = tList.Count;
		
		for (int k = 0 ; k < tlCount ; k++){
			
		}
		
		#endregion
		
		#region setListOfCounselorStudentCandidate
		
		Academy.cStudentList = game.counselor;
		Debug.Log ("Number of Counselors: "+game.counselor.Count);
#if TEST
		Academy.cStudentList.Add (new Counselor(30,new JSONClass(),2,1));
		Academy.cStudentList.Add (new Counselor(20,new JSONClass(),21,1));
		Academy.cStudentList.Add (new Counselor(21,new JSONClass(),25,1));
		Academy.cStudentList.Add (new Counselor(23,(JSONClass) JSON.Parse("{\"skills\":[{\"name\":\"木工\",\"level\":1},{\"name\":\"化學\",\"level\":4}]}"),26,1));
		Academy.cStudentList.Add (new Counselor(24,new JSONClass(),27,1));
		Academy.cStudentList.Add (new Counselor(25,new JSONClass(),28,1));
#endif
		int cslCount = 0; 

		for (var i = 0 ; i < Academy.cStudentList.Count ; i++){
			for (var j = 0 ; j < tlCount ; j++){
				if (tList[j].trainerId == Academy.cStudentList[i].id || tList[j].targetId == Academy.cStudentList[i].id){
					Debug.Log ("Id of character those are training: "+Academy.cStudentList[i].id);
					Academy.cStudentList.Remove(Academy.cStudentList[i]);
				}
			}
		}  
		cslCount = Academy.cStudentList.Count;
		for (var i = 0 ; i < cslCount ; i++){
			CreateStudentItem (Academy.cStudentList[i]);
		}
		//AcademyStudent.showSkillsOptionPanel("knowledge");
		#endregion
//		var num = game.trainings.Count;
//		for (var i = 0; i < num; i++) {
//			CreateItem(game.trainings[i].type,game.trainings[i].trainerId,game.trainings[i].targetId, game.trainings[i].etaTimestamp);
//		}

		// Test Data

		SetupAcademyPanel ();
		SetTeachItems (tList);


		//CreateTeachItems ("IQ", 5);
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
		Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge ();
		List<AcademyTeach> si=null;
		int iqCount = 0;
		int cCount = 0;
		int kCount = 0;
		int fCount = 0;
		int tempCount=0;

		int trainCount = (tr.Count>20)? 20 : tr.Count;
		for (int i =0; i< trainCount; i++) {
			int trainType = tr[i].type;
			int trainerId = tr[i].trainerId;
			int targetId = tr[i].targetId;
//		GameObject sp=null;
			if (trainType >= 0 && trainType <= 1000) {
				si = AcademyTeach.IQTeach;
				tempCount = iqCount++;
			} else if (trainType >= 1001 && trainType <= 2000) {
				si = AcademyTeach.CommandedTeach;
				tempCount = cCount++;
			} else if (trainType >= 2001 && trainType <= 3000) {
				si = AcademyTeach.KnowledgeTeach;
				tempCount = kCount++;
			} else if (trainType >= 3001 && trainType <= 4000) {
				si = AcademyTeach.FightingTeach;
				tempCount = fCount++;
			}

			si[tempCount].etaTimestamp = tr[i].etaTimestamp;
			si[tempCount].KnowledgeText.text = KnowledgeDict [trainType];
			si[tempCount].TeacherPic = imageDict [trainerId];
			si[tempCount].StudentPic = imageDict [targetId];
			si[tempCount].TeacherImageText.text = nameDict [trainerId];
			si[tempCount].StudentImageText.text = nameDict [targetId];
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

#if TEST
	public void CreateTeachItems(string panel,int quantity){
		List<AcademyTeach> si=null;
		GameObject sp=null;
		if (panel == "IQ") {
			si = AcademyTeach.IQTeach;
		}else if (panel == "Commanded") {
			si = AcademyTeach.CommandedTeach;
		}else if (panel == "Knowledge") {
			si = AcademyTeach.KnowledgeTeach;
		}else if (panel == "Fighting") {
			si = AcademyTeach.FightingTeach;
		}
		
		for (var i = 0; i < quantity; i++) { 
			AcademyTeach obj = Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>();
			obj.etaTimestamp = new DateTime(2016,3,25,i,0,0);
			Debug.Log (new DateTime(2016,3,25,i,0,0));
			obj.KnowledgeText.text = panel;
			si.Add (obj);
			obj.TeacherPic = imageDict[i+1];
			obj.StudentPic = imageDict[i+1];
			obj.TeacherImageText.text = nameDict[i+1];
			obj.StudentImageText.text = nameDict[i+1];
		}
	}
#endif

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
		AcademyTeach.setDropZone(AcademyTeach.IQTeach);
		AcademyTeach.setDropZone(AcademyTeach.CommandedTeach);
		AcademyTeach.setDropZone(AcademyTeach.KnowledgeTeach);
		AcademyTeach.setDropZone(AcademyTeach.FightingTeach);

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
		} else if (Academy.activePopup == ActivePopupEnum.CommandedPopup) {
			SetPanelActive ("Commanded");
		} else if (Academy.activePopup == ActivePopupEnum.KnowledgePopup) {
			SetPanelActive ("Knowledge");
		} else if (Academy.activePopup == ActivePopupEnum.FightingPopup) {
			SetPanelActive ("Fighting");
		}

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
			Academy.activePopup = ActivePopupEnum.none;
			SelfStudyHolder.SetActive(false);
			TeachHolder.SetActive(false);
			AcademyHolder.SetActive(false);
			MainScene.MainUIHolder.SetActive(true);

		});
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


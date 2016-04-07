//TODO Delete CommandedPopup, KnowledgePopup and FightingPopup and events in 4 buttons

<<<<<<< HEAD
#define TEST
=======
//#define TEST
>>>>>>> feature/MainMenu-shawn

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
<<<<<<< HEAD


=======
>>>>>>> feature/MainMenu-shawn
	
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
<<<<<<< HEAD
=======
	public Transform StudentScrollPanel;
	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;
	public static GameObject staticTeachHolder;
	public static List<Counselor> cStudentList;
	bool firstCalled = false;
>>>>>>> feature/MainMenu-shawn
	WsClient wsc;
	Game game;
//	public ListView.ColumnHeaderCollection ListViewColumns;
//	public ListView.ListViewItemCollection ListViewItems;

	public static ActivePopupEnum activePopup;
<<<<<<< HEAD
	Dictionary<string,string> academyCategoryText = new Dictionary<string, string>();
	Dictionary<string,ActivePopupEnum> activePopupName = new Dictionary<string, ActivePopupEnum>();
	private const int columnWidthCount = 5;
//	public ListView listView;
//	public static Endgame.ListView staticAcademyListView;
//	public static ListView.ColumnHeaderCollection staticAcademyListViewColumns;
//	private int columnCount
//	{
//		get
//		{
////			return Academy.staticAcademyListView.Columns.Count;
//		}
//	}
//	private int[] columnWidthStates = null;

	// Use this for initialization
	void Awake(){
//		CallAcademy ();
//		Academy.staticAcademyListView = listView;
//		ColumnHeader MasterColumn = new ColumnHeader ();
//		MasterColumn.Text = "師傅";
//		listView.AddColumnToHierarchy (MasterColumn);
//		SetDataGrid ();
=======
	Dictionary<ActivePopupEnum,string> academyCategoryText = new Dictionary<ActivePopupEnum, string>();
	Dictionary<string,ActivePopupEnum> activePopupName = new Dictionary<string, ActivePopupEnum>();
	private const int columnWidthCount = 5;
	List<TechTreeObject> techTreeList = new List<TechTreeObject> ();
	int numberOfTech = 0;

	// Use this for initialization
	void Start(){
>>>>>>> feature/MainMenu-shawn
		CallAcademy ();
	}

	public void CallAcademy(){
<<<<<<< HEAD
		//this.ListView = GameObject.Find ("/_MainMenuSceneRoot/Canvas/AcademyHolder/Panel/TeachHolder/ContentHolder/DataHolder/LeftHolder/ListView").GetComponents<ListView> ()[0];
		//Debug.Log (GameObject.Find ("/_MainMenuSceneRoot/Canvas/AcademyHolder/Panel/TeachHolder/ContentHolder/DataHolder/LeftHolder/ListView").GetComponents<ListView> ()[0]);
//		SetImages ();
=======
>>>>>>> feature/MainMenu-shawn

		btnName [0] = "IQButton";
		btnName [1] = "CommandedButton";
		btnName [2] = "KnowledgeButton";
		btnName [3] = "FightingButton";
		qaBtnName [0] = "SelfStudyButton";
		qaBtnName [0] = "TeachButton";
		SetDictionary ();
<<<<<<< HEAD
		AddButtonListener ();


		wsc = WsClient.Instance;
		game = Game.Instance;

//		JSONNode json = new JSONClass ();
=======
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
>>>>>>> feature/MainMenu-shawn
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

<<<<<<< HEAD


		AcademyTeach.commonPanel = TeachScrollPanel;

=======
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
		
//		Academy.cStudentList = game.counselor;
//		Debug.Log ("Number of Counselors: "+game.counselor.Count);
//#if TEST
//		Academy.cStudentList.Add (new Counselor(30,new JSONClass(),2,1));
//		Academy.cStudentList.Add (new Counselor(20,new JSONClass(),21,1));
//		Academy.cStudentList.Add (new Counselor(21,new JSONClass(),25,1));
//		Academy.cStudentList.Add (new Counselor(23,(JSONClass) JSON.Parse("{\"skills\":[{\"name\":\"木工\",\"level\":1},{\"name\":\"化學\",\"level\":4}]}"),26,1));
//		Academy.cStudentList.Add (new Counselor(24,new JSONClass(),27,1));
//		Academy.cStudentList.Add (new Counselor(25,new JSONClass(),28,1));
//#endif
//		int cslCount = 0; 
//
//		for (var i = 0 ; i < Academy.cStudentList.Count ; i++){
//			for (var j = 0 ; j < tlCount ; j++){
//				if (tList[j].trainerId == Academy.cStudentList[i].id || tList[j].targetId == Academy.cStudentList[i].id){
//					Debug.Log ("Id of character those are training: "+Academy.cStudentList[i].id);
//					Academy.cStudentList.Remove(Academy.cStudentList[i]);
//				}
//			}
//		}  
//		cslCount = Academy.cStudentList.Count;
//		for (var i = 0 ; i < cslCount ; i++){
//			CreateStudentItem (Academy.cStudentList[i]);
//		}
		//AcademyStudent.showSkillsOptionPanel("knowledge");
		#endregion
>>>>>>> feature/MainMenu-shawn
//		var num = game.trainings.Count;
//		for (var i = 0; i < num; i++) {
//			CreateItem(game.trainings[i].type,game.trainings[i].trainerId,game.trainings[i].targetId, game.trainings[i].etaTimestamp);
//		}
<<<<<<< HEAD
		CreateItems ("IQ", 5);

	}

	public void CreateItem(int trainType, int trainerId, int targetId, DateTime etaTime){
		List<AcademyTeach> si=null;
		GameObject sp=null;
		if (trainType >= 0 && trainType <= 100) {
			si = AcademyTeach.IQTeach;
		}else if (trainType >= 1001 && trainType <= 2000) {
			si = AcademyTeach.CommandedTeach;
		}else if (trainType >= 2001 && trainType <= 3000) {
			si = AcademyTeach.KnowledgeTeach;
		}else if (trainType >= 3001 && trainType <= 4000) {
			si = AcademyTeach.FightingTeach;
		}
		AcademyTeach obj = Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>();
		obj.LeftTimeValue = "";
		obj.KnowledgeValue = "";
		si.Add (obj);
	}

#if TEST
	public void CreateItems(string panel,int quantity){
=======

		// Test Data

		SetupAcademyPanel ();
		SetTeachItems (tList);


		//CreateTeachItems ("IQ", 5);
	}

	public void SetupStudentPrefabList(){
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
		List<Trainings> tList = game.trainings;
		var tlCount = tList.Count;
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
>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
			obj.etaTimestamp = new DateTime(2016,3,20,i,0,0);

			Debug.Log("ETA: "+obj.etaTimestamp);
			obj.KnowledgeValue = panel;
			si.Add (obj);
		}
		Debug.Log("Now: "+DateTime.Now);
=======
			obj.etaTimestamp = new DateTime(2016,3,25,i,0,0);
			Debug.Log (new DateTime(2016,3,25,i,0,0));
			obj.KnowledgeText.text = panel;
			si.Add (obj);
			obj.TeacherPic = imageDict[i+1];
			obj.StudentPic = imageDict[i+1];
			obj.TeacherImageText.text = nameDict[i+1];
			obj.StudentImageText.text = nameDict[i+1];
		}
>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
=======
		AcademyStudent.showPanelItems(AcademyStudent.Students);

>>>>>>> feature/MainMenu-shawn
	}


	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
<<<<<<< HEAD
		
=======
//		if (!firstCalled) {
//			Invoke("CallAcademy",1);
//		}
		AcademyTeach.setDropZone(AcademyTeach.IQTeach);
		AcademyTeach.setDropZone(AcademyTeach.CommandedTeach);
		AcademyTeach.setDropZone(AcademyTeach.KnowledgeTeach);
		AcademyTeach.setDropZone(AcademyTeach.FightingTeach);

>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
=======
//		De
>>>>>>> feature/MainMenu-shawn
		Debug.Log ("Button in Academy clicked");
		GameObject selfStudyPopup = GameObject.Find ("SelfStudyHolder");
		GameObject teacherPopup = GameObject.Find ("TeachHolder");
//		SelfStudyHolder.SetActive (true);
//		TeachHolder.SetActive (true);
		QAHolder.SetActive (true);
		Academy.activePopup = activePopupName[btn.name];
		Debug.Log (Academy.activePopup);
<<<<<<< HEAD
		//GameObject.Find ("/Canvas/AcademyHolder/Panel/PopupHolder/Popup/Text").GetComponent<Text>().text = academyCategoryText[btn.name];
	}

	void OnQAButtonClick(Button btn){
		Debug.Log ("QA Button in Academy clicked");

=======
	}

	void OnQAButtonClick(Button btn){
>>>>>>> feature/MainMenu-shawn
		QAHolder.SetActive (false);
		if (btn.name == "SelfStudyButton") {
			SelfStudyHolder.SetActive(true);
		} else {
			TeachHolder.SetActive(true);
		}
<<<<<<< HEAD
		SelfStudyHolder.transform.Find("Popup/Text").gameObject.GetComponent<Text>().text = academyCategoryText[btn.name];
		TeachHolder.transform.Find("Header").gameObject.GetComponent<Text>().text = academyCategoryText[btn.name];
		Debug.Log (btn.name);
=======

		SelfStudyHolder.transform.Find("Popup/Text").gameObject.GetComponent<Text>().text = academyCategoryText[Academy.activePopup];
		TeachHolder.transform.Find("Header").gameObject.GetComponent<Text>().text = academyCategoryText[Academy.activePopup];

>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
		Transform child1 = qaButtons [0].transform;
		qaButtons[0].onClick.AddListener(() => { OnQAButtonClick(child1.GetComponent<Button>()); });
		Transform child2= buttons [1].transform;
		qaButtons[1].onClick.AddListener(() => { OnQAButtonClick(child2.GetComponent<Button>()); });
=======
		for (var i = 0; i < 2; i++) {
			Transform child = qaButtons [i].transform;
			qaButtons [i].onClick.AddListener (() => {
				OnQAButtonClick (child.GetComponent<Button> ()); });
		}
>>>>>>> feature/MainMenu-shawn
		
		backButton.onClick.AddListener (() => {
			Academy.activePopup = ActivePopupEnum.none;
			SelfStudyHolder.SetActive(false);
			TeachHolder.SetActive(false);
<<<<<<< HEAD
		});
		closeButton.onClick.AddListener (() => {
=======
			gameObject.SetActive(true);
		});
		closeButton.onClick.AddListener (() => {
			var count = AcademyStudent.Students.Count;
			for (int i = 0 ; i < count ; i++){
//				GameObject.DestroyImmediate( StudentScrollPanel.transform.GetChild(0).gameObject);
				GameObject.DestroyImmediate( AcademyStudent.Students[i].gameObject);
			}
			AcademyStudent.Students = new List<AcademyStudent>();
>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
		academyCategoryText.Add (btnName [0], "智商");
		academyCategoryText.Add (btnName [1], "統率");
		academyCategoryText.Add (btnName [2], "學問");
		academyCategoryText.Add (btnName [3], "陣法");
	}

	public Sprite YaSinTipMukYi;
	public Sprite YuHim;
	public Sprite NgTseSeui;
	public Sprite BakPei;
	public Sprite SinJan;
	public Sprite SinYing;
	public Sprite LauBakWan;
	public Sprite LauBingChung;
	public Sprite SiNeiGin;
	public Sprite SiSiMing;
	public Sprite SiManShui;
	public Sprite SiMaYi;
	public Sprite NgSaamGwai;
	public Sprite NgHei;
	public Sprite LuiBo;
	public Sprite ChowAhFu;
	public Sprite ChowYu;
	public Sprite ChowSauYing;
	public Sprite JitBit;
	public Sprite SinHongShun;
	public Sprite WongShekGong;
	public Sprite HaHouDun;
	public Sprite GeungTseNga;
	public Sprite SuenMo;
	public Sprite SuenBan;
	public Sprite OnLukShan;
	public Sprite JungJaak;
	public Sprite WaiLiuTse;
	public Sprite WaiChiGingDak;
	public Sprite NgokFei;
	public Sprite SheungYuChun;
	public Sprite LimPaul;
	public Sprite CheungShukYe;
	public Sprite CheungLeung;
	public Sprite CheungLiu;
	public Sprite CheungHop;
	public Sprite CheungFei;
	public Sprite ChuiTat;
	public Sprite NgouLoi;
	public Sprite MoYungYinChiu;
	public Sprite ChikGaiGwong;
	public Sprite SiLong;
	public Sprite ChoGwai;
	public Sprite TsengKwokFan;
	public Sprite ChuBunDeui;
	public Sprite LeeSaiJik;
	public Sprite LeeKwongLee;
	public Sprite LeeManChung;
	public Sprite LeeGingLung;
	public Sprite LeeMuk;
	public Sprite LeeSauNing;
	public Sprite LeeJiShing;
	public Sprite YeungJiCheung;
	public Sprite YeungKwokChung;
	public Sprite YeungYip;
	public Sprite YeungSou;
	public Sprite YeungHou;
	public Sprite LokNgai;
	public Sprite DikChing;
	public Sprite WongBakDong;
	public Sprite WongFaChing;
	public Sprite WongYuenMou;
	public Sprite WongYi;
	public Sprite WongChin;
	public Sprite GumNing;
	public Sprite TinYeungJeui;
	public Sprite ButJoiYu;
	public Sprite PakHei;
	public Sprite ChunLeungYuk;
	public Sprite ChingNgauGam;
	public Sprite MukGwaiYing;
	public Sprite GunChung;
	public Sprite YeLuChucai;
	public Sprite FaMukLan;
	public Sprite MiuFun;
	public Sprite FanTseng;
	public Sprite FanManChing;
	public Sprite FanManFu;
	public Sprite FanYi;
	public Sprite FanYung;
	public Sprite MongTim;
	public Sprite SitYanGwai;
	public Sprite WaiChing;
	public Sprite YuenShungWun;
	public Sprite YuenYingTai;
	public Sprite ChuGotLeung;
	public Sprite HoYeukBut;
	public Sprite ChoiSheungA;
	public Sprite ChiuChe;
	public Sprite ChiuTim;
	public Sprite ChiuWan;
	public Sprite SanYeungSau;
	public Sprite KwokTseYi;
	public Sprite ChengYanTai;
	public Sprite ChengShingGong;
	public Sprite KwanYu;
	public Sprite ChanHingChi;
	public Sprite FokHuiBing;
	public Sprite HonSaiChung;
	public Sprite HongYu;
	public Sprite FeiLim;
	public Sprite MaChiu;
	public Sprite SinYuChungTong;
	public Sprite WongChung;
	public Sprite PongDak;
	public Sprite PongGyun;
	public Sprite PongTong;


//	private void SetImages(){
//		ImageList imageList = new ImageList();
//		// Add some images.
//
//		imageList.Images.Add("也先帖木兒", YaSinTipMukYi);
//		imageList.Images.Add("于謙", YuHim);
//		imageList.Images.Add("伍子胥", NgTseSeui);
//		imageList.Images.Add("伯嚭", BakPei);
//		imageList.Images.Add("先軫", SinJan);
//		imageList.Images.Add("冼英", SinYing);
//		imageList.Images.Add("劉基", LauBakWan);
//		imageList.Images.Add("劉秉忠", LauBingChung);
//		imageList.Images.Add("史彌堅", SiNeiGin);
//		imageList.Images.Add("史思明", SiSiMing);
//		imageList.Images.Add("史萬歲", SiManShui);
//		imageList.Images.Add("司馬懿", SiMaYi);
//		imageList.Images.Add("吳三桂", NgSaamGwai);
//		imageList.Images.Add("吳起 ", NgHei);
//		imageList.Images.Add("呂布", LuiBo);
//		imageList.Images.Add("周亞夫", ChowAhFu);
//		imageList.Images.Add("周瑜", ChowYu);
//		imageList.Images.Add("周秀英", ChowSauYing);
//		imageList.Images.Add("哲別", JitBit);
//		imageList.Images.Add("單雄信", SinHongShun);
//		imageList.Images.Add("黃石公", WongShekGong);
//		imageList.Images.Add("夏侯惇", HaHouDun);
//		imageList.Images.Add("姜子牙", GeungTseNga);
//		imageList.Images.Add("孫武", SuenMo);
//		imageList.Images.Add("孫臏", SuenBan);
//		imageList.Images.Add("安祿山", OnLukShan);
//		imageList.Images.Add("宗澤", JungJaak);
//		imageList.Images.Add("尉繚子", WaiLiuTse);
//		imageList.Images.Add("尉遲敬德", WaiChiGingDak);
//		imageList.Images.Add("岳飛", NgokFei);
//		imageList.Images.Add("常遇春", SheungYuChun);
//		imageList.Images.Add("廉頗", LimPaul);
//		imageList.Images.Add("張叔夜", CheungShukYe);
//		imageList.Images.Add("張良", CheungLeung);
//		imageList.Images.Add("張遼", CheungLiu);
//		imageList.Images.Add("張郃", CheungHop);
//		imageList.Images.Add("張飛", CheungFei);
//		imageList.Images.Add("徐達", ChuiTat);
//		imageList.Images.Add("惡來", NgouLoi);
//		imageList.Images.Add("慕容延釗", MoYungYinChiu);
//		imageList.Images.Add("戚繼光", ChikGaiGwong);
//		imageList.Images.Add("施琅", SiLong);
//		imageList.Images.Add("曹劌", ChoGwai);
//		imageList.Images.Add("曾國藩", TsengKwokFan);
//		imageList.Images.Add("朱般懟", ChuBunDeui);
//		imageList.Images.Add("李勣", LeeSaiJik);
//		imageList.Images.Add("李廣利", LeeKwongLee);
//		imageList.Images.Add("李文忠", LeeManChung);
//		imageList.Images.Add("李景隆", LeeGingLung);
//		imageList.Images.Add("李牧", LeeMuk);
//		imageList.Images.Add("李秀寧", LeeSauNing);
//		imageList.Images.Add("李自成", LeeJiShing);
//		imageList.Images.Add("楊嗣昌", YeungJiCheung);
//		imageList.Images.Add("楊國忠", YeungKwokChung);
//		imageList.Images.Add("楊業", YeungYip);
//		imageList.Images.Add("楊素", YeungSou);
//		imageList.Images.Add("楊鎬", YeungHou);
//		imageList.Images.Add("樂毅", LokNgai);
//		imageList.Images.Add("狄青", DikChing);
//		imageList.Images.Add("王伯當", WongBakDong);
//		imageList.Images.Add("王化貞", WongFaChing);
//		imageList.Images.Add("楊嗣昌", WongYuenMou);
//		imageList.Images.Add("王異", WongYi);
//		imageList.Images.Add("王翦", WongChin);
//		imageList.Images.Add("甘寧", GumNing);
//		imageList.Images.Add("田穰苴", TinYeungJeui);
//		imageList.Images.Add("畢再遇", ButJoiYu);
//		imageList.Images.Add("白起", PakHei);
//		imageList.Images.Add("秦良玉", ChunLeungYuk);
//		imageList.Images.Add("程咬金", ChingNgauGam);
//		imageList.Images.Add("穆桂英", MukGwaiYing);
//		imageList.Images.Add("管仲", GunChung);
//		imageList.Images.Add("耶律楚材", YeLuChucai);
//		imageList.Images.Add("花木蘭", FaMukLan);
//		imageList.Images.Add("苗訓", MiuFun);
//		imageList.Images.Add("范增", FanTseng);
//		imageList.Images.Add("范文程", FanManChing);
//		imageList.Images.Add("范文虎", FanManFu);
//		imageList.Images.Add("范蠡", FanYi);
//		imageList.Images.Add("范雍", FanYung);
//		imageList.Images.Add("蒙恬", MongTim);
//		imageList.Images.Add("薛仁貴", SitYanGwai);
//		imageList.Images.Add("衛青", WaiChing);
//		imageList.Images.Add("袁崇煥", YuenShungWun);
//		imageList.Images.Add("袁應泰", YuenYingTai);
//		imageList.Images.Add("諸葛亮", ChuGotLeung);
//		imageList.Images.Add("賀若弼", HoYeukBut);
//		imageList.Images.Add("賽尚阿", ChoiSheungA);
//		imageList.Images.Add("趙奢", ChiuChe);
//		imageList.Images.Add("趙括", ChiuTim);
//		imageList.Images.Add("趙雲", ChiuWan);
//		imageList.Images.Add("辰漾守", SanYeungSau);
//		imageList.Images.Add("郭子儀", KwokTseYi);
//		imageList.Images.Add("鄭仁泰", ChengYanTai);
//		imageList.Images.Add("鄭成功", ChengShingGong);
//		imageList.Images.Add("陳慶之", ChanHingChi);
//		imageList.Images.Add("霍去病", FokHuiBing);
//		imageList.Images.Add("韓世忠", HonSaiChung);
//		imageList.Images.Add("項羽", HongYu);
//		imageList.Images.Add("飛廉", FeiLim);
//		imageList.Images.Add("馬超", MaChiu);
//		imageList.Images.Add("鮮于仲通", SinYuChungTong);
//		imageList.Images.Add("黃忠", WongChung);
//		imageList.Images.Add("龐德", PongDak);
//		imageList.Images.Add("龐涓", PongGyun);
//		imageList.Images.Add("龐統", PongTong);
//		imageList.Images.Add("關羽", KwanYu);
//
//
//		// Set the listview's image list.
////		this.ListView.SmallImageList = imageList;
//
////		this.ListView.SubItemClicked += this.OnSubItemClicked;
//	}
=======
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
>>>>>>> feature/MainMenu-shawn
}


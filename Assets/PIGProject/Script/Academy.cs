//TODO Delete CommandedPopup, KnowledgePopup and FightingPopup and events in 4 buttons



using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Endgame;
using WebSocketSharp;
using SimpleJSON;
using UnityEngine.EventSystems;

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


	
//	public Button[] buttons = new Button[4];
//	string[] btnName = new string[4];
//	public Button[] qaButtons = new Button[2];
//	string[] qaBtnName = new string[2];
	public Button closeButton;
	public Button backButton;
	public GameObject AcademyHolder;
	public GameObject TeachHolder;
	WsClient wsc;
	Game game;
	public ListView.ColumnHeaderCollection ListViewColumns;
	public ListView.ListViewItemCollection ListViewItems;

	public static ActivePopupEnum activePopup;
	Dictionary<string,string> academyCategoryText = new Dictionary<string, string>();
	Dictionary<string,ActivePopupEnum> activePopupName = new Dictionary<string, ActivePopupEnum>();
	private const int columnWidthCount = 5;
	public ListView listView;
	public static Endgame.ListView staticAcademyListView;
	public static ListView.ColumnHeaderCollection staticAcademyListViewColumns;
	private int columnCount
	{
		get
		{
			return Academy.staticAcademyListView.Columns.Count;
		}
	}
	private int[] columnWidthStates = null;

	// Use this for initialization
	void Awake(){
//		CallAcademy ();
		Academy.staticAcademyListView = listView;
		ColumnHeader MasterColumn = new ColumnHeader ();
		MasterColumn.Text = "師傅";
		listView.AddColumnToHierarchy (MasterColumn);
		SetDataGrid ();
	}

	public void CallAcademy(){
		//this.ListView = GameObject.Find ("/_MainMenuSceneRoot/Canvas/AcademyHolder/Panel/TeachHolder/ContentHolder/DataHolder/LeftHolder/ListView").GetComponents<ListView> ()[0];
		//Debug.Log (GameObject.Find ("/_MainMenuSceneRoot/Canvas/AcademyHolder/Panel/TeachHolder/ContentHolder/DataHolder/LeftHolder/ListView").GetComponents<ListView> ()[0]);
		SetImages ();

//		btnName [0] = "IQButton";
//		btnName [1] = "CommandedButton";
//		btnName [2] = "KnowledgeButton";
//		btnName [3] = "FightingButton";
//		qaBtnName [0] = "SelfStudyButton";
//		qaBtnName [0] = "TeachButton";
//		SetDictionary ();
//		AddButtonListener ();


		wsc = WsClient.Instance;
		game = Game.Instance;

		JSONNode json = new JSONClass ();

		if (wsc.conn.IsAlive) {

			json ["data"] = "1"; //game.login.id;

			json ["action"] = "GET";
			json ["table"] = "users";
			wsc.conn.Send (json.ToString ());

			json ["data"] = "1";
			json ["table"] = "getCounselorInfoByUserId";
			wsc.conn.Send (json.ToString ());

			json ["table"] = "generals";
			wsc.conn.Send (json.ToString ());
		} else {
			Debug.Log ("Websocket Connection Lost!");
		}
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
		
	}
	/*
	void OnButtonClick(Button btn){
		Debug.Log ("Button in Academy clicked");
		GameObject selfStudyPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/SelfStudyHolder/");
		GameObject teacherPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/TeachHolder/");
		selfStudyPopup.SetActive (true);
		teacherPopup.SetActive (true);
		GameObject qaPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/QAHolder/");
		qaPopup.SetActive (true);
		Academy.activePopup = activePopupName[btn.name];
		//GameObject.Find ("/Canvas/AcademyHolder/Panel/PopupHolder/Popup/Text").GetComponent<Text>().text = academyCategoryText[btn.name];
	}

	void OnQAButtonClick(Button btn){
		Debug.Log ("QA Button in Academy clicked");
		//		GameObject selfStudyPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/SelfStudyHolder/");
		//		GameObject teacherPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/TeachHolder/");
		//		selfStudyPopup.SetActive (true);
		//		teacherPopup.SetActive (true);
		//GameObject qaPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/QAHolder/");
		//Academy.activePopup = activePopupName[btn.name];
		GameObject qaPopup = GameObject.Find ("/Canvas/AcademyHolder/Panel/QAHolder/");
		qaPopup.SetActive (false);
		if (btn.name == "SelfStudyButton") {
			GameObject.Find ("/Canvas/AcademyHolder/Panel/SelfStudyHolder").SetActive(true);
		} else {
			GameObject.Find ("/Canvas/AcademyHolder/Panel/TeachHolder").SetActive(true);
		}
		GameObject.Find ("/Canvas/AcademyHolder/Panel/SelfStudyHolder/Popup/Text").GetComponent<Text>().text = academyCategoryText[btn.name];
		GameObject.Find ("/Canvas/AcademyHolder/Panel/TeachHolder/Popup/Text").GetComponent<Text>().text = academyCategoryText[btn.name];
	}

	void AddButtonListener(){

		for (var i = 0; i < 4; i++) {
			Transform child = buttons [i].transform;
			buttons[i].onClick.AddListener(() => { OnButtonClick(child.GetComponent<Button>()); });
		}
		backButton.onClick.AddListener (() => {
			Academy.activePopup = ActivePopupEnum.none;
			GameObject.Find ("/Canvas/AcademyHolder/Panel/SelfStudyHolder/").SetActive(false);
			GameObject.Find ("/Canvas/AcademyHolder/Panel/TeachHolder/").SetActive(false);
		});
		closeButton.onClick.AddListener (() => {
			Academy.activePopup = ActivePopupEnum.none;
			GameObject.Find ("/Canvas/AcademyHolder/Panel/SelfStudyHolder/").SetActive(false);
			GameObject.Find ("/Canvas/AcademyHolder/Panel/TeachhHolder/").SetActive(false);
			GameObject.Find ("AcademyHolder").SetActive(false);
			MainScene.MainUIHolder.SetActive(true);

		});
	}

	void SetDictionary(){
		activePopupName.Add (btnName [0], ActivePopupEnum.IQPopup);
		activePopupName.Add (btnName [1], ActivePopupEnum.CommandedPopup);
		activePopupName.Add (btnName [2], ActivePopupEnum.KnowledgePopup);
		activePopupName.Add (btnName [3], ActivePopupEnum.FightingPopup);
		academyCategoryText.Add (btnName [0], "智商");
		academyCategoryText.Add (btnName [1], "統率");
		academyCategoryText.Add (btnName [2], "學問");
		academyCategoryText.Add (btnName [3], "陣法");
	}
*/
	public void SetDataGrid(){	
		//		int x = (int)ga.transform.position.x;
		//		int y = (int)ga.transform.position.y;
		//		Rect size = ga.GetComponent<RectTransform> ().rect;
		
		//		Debug.Log ("X: "+(size.width));
		//		Debug.Log ("Y: "+size.height);
		//GUI.Label (new Rect (size.height-x, size.width-y, 80, 30), "<size=20>Lau Bei</size>");

		if ( Academy.staticAcademyListView != null)
		{
//			if ( MenuScreenView.staticAcademyListView.Columns != null){
//				ListViewColumns =  MenuScreenView.staticAcademyListView.Columns;

//			}
			ListView.ColumnHeaderCollection ListViewColumns = Academy.staticAcademyListViewColumns;
			Academy.staticAcademyListView.SuspendLayout();
			{

				ColumnHeader MasterColumn = new ColumnHeader ();
				MasterColumn.Text = "師傅";
//				Academy.staticAcademyListView.AddColumnToHierarchy (MasterColumn);
				ListViewColumns.Add (MasterColumn);

				ColumnHeader StudentColumn = new ColumnHeader ();
				StudentColumn.Text = "徒弟";
				ListViewColumns.Add (StudentColumn);
				
				ColumnHeader KnowledgeColumn = new ColumnHeader ();
				KnowledgeColumn.Text = "學問";
				ListViewColumns.Add (KnowledgeColumn);
				
				ColumnHeader TimeColumn = new ColumnHeader ();
				TimeColumn.Text = "時間";
				ListViewColumns.Add (TimeColumn);

				
				ListViewColumns[0].Width = 200;
				ListViewColumns[1].Width = 200;
				ListViewColumns[2].Width = 200;
				ListViewColumns[3].Width = 200;
				
				this.columnWidthStates = new int[this.columnCount];
			}
			Academy.staticAcademyListView.ResumeLayout();
			AddDataGrid ();


//			renderer.enabled = false;
//			renderer = AcademyHolder.transform.GetComponentsInChildren<MeshRenderer>();
//			renderer.enabled = false;

		}
	}

	private void AddDataGrid(){
					AddListViewItem (
						"史彌堅",
						"Can't Get Better than this (Original Version)",
						"Mathew Gil, John Courtidis, Sam Littlemore, Parachute Youth",
						"5:02"
					);
					AddListViewItem ("史彌堅","Forever", "Haim", "4:05" );
					AddListViewItem ( "史彌堅","City Boy", "Donkeyboy", "3:25");
					AddListViewItem ( "史彌堅","Dance All Night (feat. Matisyahu)", "The Dirty Heads", "3:26");
					AddListViewItem ("史彌堅", "Call it Off", "Washed Out", "3:33");
					AddListViewItem ("史彌堅","Because Of You", "C2C, Pigeon John", "3:42" );
					AddListViewItem ( "史彌堅","Flutes", "Hot Chip", "7:05");
					AddListViewItem (
						"史彌堅",
						"Get Lucky",
						"Daft Punk, Pharrell Williams, Nile Rodgers",
						"6:10"
					);
					AddListViewItem ("史彌堅" ,"Candy", "Robbie Williams", "3:21" );
					AddListViewItem ("史彌堅","Body Work", "Morgan Page, Tegan And Sara", "3:53" );
					AddListViewItem ("史彌堅", "Free to Flee", "OHO", "4:00");
	}

	private ListViewItem CreateListViewItem(string imageKey, string master, string student, string knowledge)
	{
		string[] subItemTexts = new string[]
		{
			master,
			student,
			knowledge
		};

		ListViewItem item = new ListViewItem(subItemTexts);

		// Add an image to the first subitem.
		item.ImageKey = imageKey;
		ItemData itemData = new ItemData();
		itemData.ImageKey = imageKey;
		itemData.SliderValue = 0;
		item.Tag = itemData;
		item.SubItems [1].ImageKey = imageKey;

		// NOTE: Any custom controls to be added to the list view item 
		// should be created in OnItemBecameVisible, and destroyed in
		// OnItemBecameInvisible. This is because the list view only
		// creates GameObjects to display the items that are visible,
		// rather than for every item in ListView.Items.

		return item;
	}

	private void AddListViewItem(string imageKey, string master, string student, string knowledge)
	{
		ListViewItem item = CreateListViewItem(imageKey, master, student,knowledge);

		Academy.staticAcademyListView.Items.Add(item);

	}

	public void RemoveAllItems(){
		int cnt = ListViewItems.Count;
		for (int i = 0; i < cnt; i++) {
			ListViewItems.RemoveAt (0);
		}

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


	private void SetImages(){
		ImageList imageList = new ImageList();
		// Add some images.

		imageList.Images.Add("也先帖木兒", YaSinTipMukYi);
		imageList.Images.Add("于謙", YuHim);
		imageList.Images.Add("伍子胥", NgTseSeui);
		imageList.Images.Add("伯嚭", BakPei);
		imageList.Images.Add("先軫", SinJan);
		imageList.Images.Add("冼英", SinYing);
		imageList.Images.Add("劉基", LauBakWan);
		imageList.Images.Add("劉秉忠", LauBingChung);
		imageList.Images.Add("史彌堅", SiNeiGin);
		imageList.Images.Add("史思明", SiSiMing);
		imageList.Images.Add("史萬歲", SiManShui);
		imageList.Images.Add("司馬懿", SiMaYi);
		imageList.Images.Add("吳三桂", NgSaamGwai);
		imageList.Images.Add("吳起 ", NgHei);
		imageList.Images.Add("呂布", LuiBo);
		imageList.Images.Add("周亞夫", ChowAhFu);
		imageList.Images.Add("周瑜", ChowYu);
		imageList.Images.Add("周秀英", ChowSauYing);
		imageList.Images.Add("哲別", JitBit);
		imageList.Images.Add("單雄信", SinHongShun);
		imageList.Images.Add("黃石公", WongShekGong);
		imageList.Images.Add("夏侯惇", HaHouDun);
		imageList.Images.Add("姜子牙", GeungTseNga);
		imageList.Images.Add("孫武", SuenMo);
		imageList.Images.Add("孫臏", SuenBan);
		imageList.Images.Add("安祿山", OnLukShan);
		imageList.Images.Add("宗澤", JungJaak);
		imageList.Images.Add("尉繚子", WaiLiuTse);
		imageList.Images.Add("尉遲敬德", WaiChiGingDak);
		imageList.Images.Add("岳飛", NgokFei);
		imageList.Images.Add("常遇春", SheungYuChun);
		imageList.Images.Add("廉頗", LimPaul);
		imageList.Images.Add("張叔夜", CheungShukYe);
		imageList.Images.Add("張良", CheungLeung);
		imageList.Images.Add("張遼", CheungLiu);
		imageList.Images.Add("張郃", CheungHop);
		imageList.Images.Add("張飛", CheungFei);
		imageList.Images.Add("徐達", ChuiTat);
		imageList.Images.Add("惡來", NgouLoi);
		imageList.Images.Add("慕容延釗", MoYungYinChiu);
		imageList.Images.Add("戚繼光", ChikGaiGwong);
		imageList.Images.Add("施琅", SiLong);
		imageList.Images.Add("曹劌", ChoGwai);
		imageList.Images.Add("曾國藩", TsengKwokFan);
		imageList.Images.Add("朱般懟", ChuBunDeui);
		imageList.Images.Add("李勣", LeeSaiJik);
		imageList.Images.Add("李廣利", LeeKwongLee);
		imageList.Images.Add("李文忠", LeeManChung);
		imageList.Images.Add("李景隆", LeeGingLung);
		imageList.Images.Add("李牧", LeeMuk);
		imageList.Images.Add("李秀寧", LeeSauNing);
		imageList.Images.Add("李自成", LeeJiShing);
		imageList.Images.Add("楊嗣昌", YeungJiCheung);
		imageList.Images.Add("楊國忠", YeungKwokChung);
		imageList.Images.Add("楊業", YeungYip);
		imageList.Images.Add("楊素", YeungSou);
		imageList.Images.Add("楊鎬", YeungHou);
		imageList.Images.Add("樂毅", LokNgai);
		imageList.Images.Add("狄青", DikChing);
		imageList.Images.Add("王伯當", WongBakDong);
		imageList.Images.Add("王化貞", WongFaChing);
		imageList.Images.Add("楊嗣昌", WongYuenMou);
		imageList.Images.Add("王異", WongYi);
		imageList.Images.Add("王翦", WongChin);
		imageList.Images.Add("甘寧", GumNing);
		imageList.Images.Add("田穰苴", TinYeungJeui);
		imageList.Images.Add("畢再遇", ButJoiYu);
		imageList.Images.Add("白起", PakHei);
		imageList.Images.Add("秦良玉", ChunLeungYuk);
		imageList.Images.Add("程咬金", ChingNgauGam);
		imageList.Images.Add("穆桂英", MukGwaiYing);
		imageList.Images.Add("管仲", GunChung);
		imageList.Images.Add("耶律楚材", YeLuChucai);
		imageList.Images.Add("花木蘭", FaMukLan);
		imageList.Images.Add("苗訓", MiuFun);
		imageList.Images.Add("范增", FanTseng);
		imageList.Images.Add("范文程", FanManChing);
		imageList.Images.Add("范文虎", FanManFu);
		imageList.Images.Add("范蠡", FanYi);
		imageList.Images.Add("范雍", FanYung);
		imageList.Images.Add("蒙恬", MongTim);
		imageList.Images.Add("薛仁貴", SitYanGwai);
		imageList.Images.Add("衛青", WaiChing);
		imageList.Images.Add("袁崇煥", YuenShungWun);
		imageList.Images.Add("袁應泰", YuenYingTai);
		imageList.Images.Add("諸葛亮", ChuGotLeung);
		imageList.Images.Add("賀若弼", HoYeukBut);
		imageList.Images.Add("賽尚阿", ChoiSheungA);
		imageList.Images.Add("趙奢", ChiuChe);
		imageList.Images.Add("趙括", ChiuTim);
		imageList.Images.Add("趙雲", ChiuWan);
		imageList.Images.Add("辰漾守", SanYeungSau);
		imageList.Images.Add("郭子儀", KwokTseYi);
		imageList.Images.Add("鄭仁泰", ChengYanTai);
		imageList.Images.Add("鄭成功", ChengShingGong);
		imageList.Images.Add("陳慶之", ChanHingChi);
		imageList.Images.Add("霍去病", FokHuiBing);
		imageList.Images.Add("韓世忠", HonSaiChung);
		imageList.Images.Add("項羽", HongYu);
		imageList.Images.Add("飛廉", FeiLim);
		imageList.Images.Add("馬超", MaChiu);
		imageList.Images.Add("鮮于仲通", SinYuChungTong);
		imageList.Images.Add("黃忠", WongChung);
		imageList.Images.Add("龐德", PongDak);
		imageList.Images.Add("龐涓", PongGyun);
		imageList.Images.Add("龐統", PongTong);
		imageList.Images.Add("關羽", KwanYu);


		// Set the listview's image list.
//		this.ListView.SmallImageList = imageList;

//		this.ListView.SubItemClicked += this.OnSubItemClicked;
	}
}


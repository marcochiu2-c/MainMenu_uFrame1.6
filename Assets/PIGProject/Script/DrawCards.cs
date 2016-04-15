//#define TEST


using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using WebSocketSharp;






public class DrawCards : MonoBehaviour {
	WsClient wsc;
	Game game;
	JSONNode json;
	List<GeneralCards> generalList = new List<GeneralCards>();
	List<CounselorCards> counselorList = new List<CounselorCards> ();

	public GameObject TenDrawHolder;
	public GameObject SingleCardHolder;

	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;
	public Button backButton;
	public Button closeButton;

	const int rankNeedRedraw = 1;
	const int rankStopRedraw = 2;

	int numberOfCounselors = 0;
	int numberOfGenerals = 0;

	bool calledByDrawTenCards = false;

	// Use this for initialization
	void Awake () {
		// Initialize services and variables
		game = Game.Instance;
		wsc = WsClient.Instance;

		generalList = GeneralCards.GetList (1001);
		numberOfGenerals = generalList.Count;


		counselorList = CounselorCards.GetList (1);
		numberOfCounselors = counselorList.Count;


		Debug.Log ("Number of counselors: " + numberOfCounselors);
		Debug.Log ("Number of generals: " + numberOfGenerals);
//		Debug.Log ( counselorList[28].Name);
//		Debug.Log ( counselorList[28].Rank);

		wsc.conn.OnError += (sender, e) => {
			Debug.LogError (e.Message);
		};
	}

	void Start(){  // TODO: Change to uframe start event
		CallDrawCards ();
	}

	public void CallDrawCards(){

		AddButtonListener ();
		json = new JSONClass ();
		SetCharacters ();
//		//Debug.Log (json.ToString ());
//		if (wsc.conn.IsAlive) {
//			json ["data"] = "1";
//			json ["action"] = "GET";
//			json ["table"] = "users";
//			wsc.Send (json.ToString ()); 
//
//			json ["table"] = "generals";
//			wsc.Send (json.ToString ()); 
//
//			json ["table"] = "getCounselorInfoByUserId";
//			wsc.Send (json.ToString ()); 
//		} else {
//			Debug.Log ("Websocket Connection Lost!");
//		}

	}

	// Update is called once per frame
	void Update () {

	}

	public void OnButtonDrawSingleCard(){
		if (true) {
			DrawSingleCard ();
		}
	}

	void AddButtonListener(){
		
		backButton.onClick.AddListener (() => {
			SingleCardHolder.SetActive(false);
			gameObject.SetActive(true);
			TenDrawHolder.SetActive(false);
			if (calledByDrawTenCards){
				
				TenDrawHolder.SetActive(true);
				calledByDrawTenCards = false;
			}

//			DrawCards.activePopup = ActivePopupEnum.none;
//			SelfStudyHolder.SetActive(false);
//			TeachHolder.SetActive(false);
		});

		closeButton.onClick.AddListener (() => {
			SingleCardHolder.SetActive(false);
			gameObject.SetActive(false);
			TenDrawHolder.SetActive(false);
			calledByDrawTenCards = false;

		});
	}

	public void DrawSingleCard(){
		int random = UnityEngine.Random.Range (1, numberOfCounselors + numberOfGenerals);
		bool isCounselors = (random <= numberOfCounselors);
		int result = (isCounselors) ? random : random - numberOfCounselors + 1000;
		Debug.Log("Random number: "+random);
//		Debug.Log ("Random Number: "+random);
		Debug.Log ("Result Number: "+ result);
		json = new JSONClass ();
#if TEST
		json["data"].Add ("userId",new JSONData(1));
#else
		json["data"].Add ("userId",new JSONData(game.login.id));
#endif
		json["data"].Add ("type" , new JSONData(result));
		json["data"].Add ("level", new JSONData(1));
		if (isCounselors) {
			json["data"].Add ("attributes", counselorList[result].ToJSON());
			json ["table"] = "counselors";
			game.counselor.Add (new Counselor(0, counselorList[result].ToJSON(), result,1));
		} else {

			json["data"].Add ("attributes", generalList[random - numberOfCounselors].ToJSON());
			json ["table"] = "generals";
			game.general.Add (new General(0, generalList[random - numberOfCounselors].ToJSON(), result, 1));
		}

		json ["action"] = "NEW";

		Debug.Log (json.ToString ());
		wsc.Send (json.ToString ());
		calledByDrawTenCards = false;
		ShowCardPanel (result);
	}

	public void OnButtonDrawTenCards(){
		if (true) {
			DrawTenCards ();
		}
	}

	public void DrawTenCards(){
		//string[] storageJsonArray = new string[10];
		JSONArray generalNode = new JSONArray();
		JSONArray counselorNode = new JSONArray();
		JSONNode j;
		Storage s = new Storage();
		bool[] isCounselors = new bool[10];
		int random;
		bool isRedraw = true;
		bool isMustRedraw = false;
		int[] results = new int[10];
		do {
			isMustRedraw = false;
			generalNode = new JSONArray();
			counselorNode = new JSONArray();
			for (int i=0; i<10; i++) {
				random = UnityEngine.Random.Range (1, numberOfCounselors + numberOfGenerals);
				isCounselors[i] = (random <= numberOfCounselors);
//				Debug.Log ("Random Number: "+random);
				results[i] = (isCounselors[i]) ? random : random - numberOfCounselors + 1000;
				if (isCounselors[i]){ 
					Debug.Log("Drawn number:"+(results[i]));
					isRedraw = (counselorList[results[i]].Rank != rankStopRedraw) ; // Make it false if any card Rank 2
					if (counselorList[results[i]].Rank == rankNeedRedraw) isMustRedraw = true;
					j = new JSONClass();
					j.Add("type",new JSONData(results[i]));
					j.Add("level",new JSONData(1));
					j.Add("userId",new JSONData(game.login.id));
					j.Add ("attributes", counselorList[results[i]].ToJSON());
					counselorNode.Add(j);
					//storageJsonArray[i] = "{\"type\":"+results[i]+",\"level\":1}";
				}else{ // result == generals
					isRedraw = ( generalList[random-numberOfCounselors].Rank != rankStopRedraw); // Make it false if any card Rank 2
					if (generalList[random-numberOfCounselors].Rank == rankNeedRedraw) isMustRedraw = true;
					j = (JSONNode)s.toJSON ();
					j.Add("type",new JSONData(results[i]));
					j.Add("level",new JSONData(1));
					j.Add("userId",new JSONData(game.login.id));
					j.Add ("attributes", generalList[results[i]-1000].ToJSON());
					generalNode.Add(j);
				}
			}
		} while(isRedraw|| isMustRedraw);
		//string data = "["+ String.Join(" , ", storageJsonArray)+"]";
		var json = new JSONClass ();
		if (wsc.conn.IsAlive) {
			json ["data"] = counselorNode;
			json ["action"] = "NEW";
			json ["table"] = "multiCounselors";
			Debug.Log (json.ToString());
			wsc.Send (json.ToString ());

			json ["data"] = generalNode;
			json ["action"] = "NEW";
			json ["table"] = "multiGenerals";
			wsc.Send (json.ToString ());
		} else {
			Debug.Log ("Websocket Connection Lost!");
		}

		ShowTenDrawPanel (results);

	}

	public void ShowTenDrawPanel(int[] characters){
		Transform[] draw = new Transform[10];
		for (int i = 0; i < 10; i++) {
			draw[i] = TenDrawHolder.transform.GetChild(i);  // Get the Image button

			Debug.Log ("Characters number: "+characters[i]);
			draw[i].GetComponent<Image>().sprite = imageDict[characters[i]];
			draw[i].GetChild(0).GetComponent<Text>().text = nameDict[characters[i]];
			draw[i].GetChild(1).GetComponent<Text>().text = characters[i].ToString();
		}
		TenDrawHolder.SetActive (true);
	}

	public void ShowCardPanel(Button btn){
		calledByDrawTenCards = true;
		int num = Int32.Parse(btn.transform.GetChild(1).GetComponent<Text>().text);
		ShowCardPanel (num);
	}


	public void ShowCardPanel(int character){
		GameObject panel = transform.GetChild (4).gameObject;
		Image img = panel.transform.GetChild (0).GetComponent<Image> ();
		Transform cardHolder = panel.transform.GetChild (1);
		Text Name = cardHolder.GetChild (1).GetComponent<Text> ();
		Text Level = cardHolder.GetChild (3).GetComponent<Text> ();
		Text IQ = cardHolder.GetChild (5).GetComponent<Text> ();
		Text Leadership = cardHolder.GetChild (7).GetComponent<Text> ();
		Text Prestige = cardHolder.GetChild (9).GetComponent<Text> ();
		Text Courage = cardHolder.GetChild (11).GetComponent<Text> ();
		Text Force = cardHolder.GetChild (13).GetComponent<Text> ();
		Text Physical = cardHolder.GetChild (15).GetComponent<Text> ();
		Text Obedience = cardHolder.GetChild (17).GetComponent<Text> ();
		Text Formation = cardHolder.GetChild (19).GetComponent<Text> ();
		Text Knowledge = cardHolder.GetChild (21).GetComponent<Text> ();
		panel.SetActive (true);

		if (character < 1000) {
			img.sprite = imageDict[character];
			Name.text = nameDict [character];
			Level.text="1";
			IQ.text=counselorList[character].InitialIQ.ToString();
			Leadership.text=counselorList[character].InitialLeadership.ToString();
			Prestige.text=counselorList[character].InitialPrestige.ToString();
			Courage.text="-";
			Force.text="-";
			Physical.text="-";
			Obedience.text="-";
			Formation.text=counselorList[character].KnownFormation;
			Knowledge.text=counselorList[character].KnownKnowledge;
		}else {  //(character > 1000) {
			int actualNumber = character-1000;
			Debug.Log (img);
			Debug.Log (imageDict[character]);
			img.sprite = imageDict[character];
			Name.text = nameDict[character];
			Level.text="1";
			IQ.text=generalList[actualNumber].InitialIQ.ToString();
			Leadership.text=generalList[actualNumber].InitialLeadership.ToString();
			Prestige.text=generalList[actualNumber].InitialPrestige.ToString();
			Courage.text=generalList[actualNumber].InitialCourage.ToString();
			Force.text=generalList[actualNumber].InitialForce.ToString();
			Physical.text=generalList[actualNumber].InitialPhysical.ToString();
			Obedience.text=generalList[actualNumber].Obedience.ToString();
			Formation.text="-";
			Knowledge.text="-";
		}
	}

	// TODO: add method OnButtonDrawPremiumCard

	// TODO: add method OnButtonDrawSingleCardWithCard

#region SpriteDeclartion
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
#endregion
	
	private void SetCharacters(){
		imageDict = new Dictionary<int,Sprite> ();
		nameDict = new Dictionary<int,string> ();
		// Add some images.
		
		imageDict.Add (/*"也先帖木兒",*/1047, YaSinTipMukYi);
		imageDict.Add (/*"于謙",*/25, YuHim);
		imageDict.Add (/*"伍子胥",*/4, NgTseSeui);
		imageDict.Add (/*"伯嚭",*/1074, BakPei);
		imageDict.Add (/*"先軫",*/1068, SinJan);
		imageDict.Add (/*"冼英",*/1059, SinYing);
		imageDict.Add (/*"劉基",*/11, LauBakWan);
		imageDict.Add (/*"劉秉忠",*/14, LauBingChung);
		imageDict.Add (/*"史彌堅",*/1066, SiNeiGin);
		imageDict.Add (/*"史思明",*/1028, SiSiMing);
		imageDict.Add (/*"史萬歲",*/1075, SiManShui);
		imageDict.Add (/*"司馬懿",*/23, SiMaYi);
		imageDict.Add (/*"吳三桂",*/1054, NgSaamGwai);
		imageDict.Add (/*"吳起 ",*/2, NgHei);
		imageDict.Add (/*"呂布",*/1019, LuiBo);
		imageDict.Add (/*"周亞夫",*/1011, ChowAhFu);
		imageDict.Add (/*"周瑜",*/24, ChowYu);
		imageDict.Add (/*"周秀英",*/1056, ChowSauYing);
		imageDict.Add (/*"哲別",*/1035, JitBit);
		imageDict.Add (/*"單雄信",*/1070, SinHongShun);
		imageDict.Add (/*"黃石公",*/18, WongShekGong);
		imageDict.Add (/*"夏侯惇",*/1020, HaHouDun);
		imageDict.Add (/*"姜子牙",*/1, GeungTseNga);
		imageDict.Add (/*"孫武",*/3, SuenMo);
		imageDict.Add (/*"孫臏",*/9, SuenBan);
		imageDict.Add (/*"安祿山",*/1027, OnLukShan);
		imageDict.Add (/*"宗澤",*/1063, JungJaak);
		imageDict.Add (/*"尉繚子",*/19, WaiLiuTse);
		imageDict.Add (/*"尉遲敬德",*/1025, WaiChiGingDak);
		imageDict.Add (/*"岳飛",*/1032, NgokFei);
		imageDict.Add (/*"常遇春",*/1038, SheungYuChun);
		imageDict.Add (/*"廉頗",*/1007, LimPaul);
		imageDict.Add (/*"張叔夜",*/1065, CheungShukYe);
		imageDict.Add (/*"張良",*/10, CheungLeung);
		imageDict.Add (/*"張遼",*/1078, CheungLiu);
		imageDict.Add (/*"張郃",*/1022, CheungHop);
		imageDict.Add (/*"張飛",*/1015, CheungFei);
		imageDict.Add (/*"徐達",*/1036, ChuiTat);
		imageDict.Add (/*"惡來",*/1073, NgouLoi);
		imageDict.Add (/*"慕容延釗",*/1067, MoYungYinChiu);
		imageDict.Add (/*"戚繼光",*/1039, ChikGaiGwong);
		imageDict.Add (/*"施琅",*/1061, SiLong);
		imageDict.Add (/*"曹劌",*/5, ChoGwai);
		imageDict.Add (/*"曾國藩",*/17, TsengKwokFan);
		imageDict.Add (/*"朱般懟",*/28, ChuBunDeui);
		imageDict.Add (/*"李勣",*/1024, LeeSaiJik);
		imageDict.Add (/*"李廣利",*/1042, LeeKwongLee);
		imageDict.Add (/*"李文忠",*/1037, LeeManChung);
		imageDict.Add (/*"李景隆",*/1048, LeeGingLung);
		imageDict.Add (/*"李牧",*/1008, LeeMuk);
		imageDict.Add (/*"李秀寧",*/1058, LeeSauNing);
		imageDict.Add (/*"李自成",*/1055, LeeJiShing);
		imageDict.Add (/*"楊嗣昌",*/1052, YeungJiCheung);
		imageDict.Add (/*"楊國忠",*/26, YeungKwokChung);
		imageDict.Add (/*"楊業",*/1030, YeungYip);
		imageDict.Add (/*"楊素",*/1077, YeungSou);
		imageDict.Add (/*"楊鎬",*/1049, YeungHou);
		imageDict.Add (/*"樂毅",*/1003, LokNgai);
		imageDict.Add (/*"狄青",*/1031, DikChing);
		imageDict.Add (/*"王伯當",*/1069, WongBakDong);
		imageDict.Add (/*"王化貞",*/1050, WongFaChing);
		imageDict.Add (/*"王玄謨",*/1043, WongYuenMou);
		imageDict.Add (/*"王異",*/1060, WongYi);
		imageDict.Add (/*"王翦",*/1009, WongChin);
		imageDict.Add (/*"甘寧",*/1023, GumNing);
		imageDict.Add (/*"田穰苴",*/20, TinYeungJeui);
		imageDict.Add (/*"畢再遇",*/1062, ButJoiYu);
		imageDict.Add (/*"白起",*/1006, PakHei);
		imageDict.Add (/*"秦良玉",*/1057, ChunLeungYuk);
		imageDict.Add (/*"程咬金",*/1071, ChingNgauGam);
		imageDict.Add (/*"穆桂英",*/1033, MukGwaiYing);
		imageDict.Add (/*"管仲",*/6, GunChung);
		imageDict.Add (/*"耶律楚材",*/15, YeLuChucai);
		imageDict.Add (/*"花木蘭",*/1002, FaMukLan);
		imageDict.Add (/*"苗訓",*/13, MiuFun);
		imageDict.Add (/*"范增",*/12, FanTseng);
		imageDict.Add (/*"范文程",*/16, FanManChing);
		imageDict.Add (/*"范文虎",*/1046, FanManFu);
		imageDict.Add (/*"范蠡",*/7, FanYi);
		imageDict.Add (/*"范雍",*/1045, FanYung);
		imageDict.Add (/*"蒙恬",*/1004, MongTim);
		imageDict.Add (/*"薛仁貴",*/1026, SitYanGwai);
		imageDict.Add (/*"衛青",*/1012, WaiChing);
		imageDict.Add (/*"袁崇煥",*/1040, YuenShungWun);
		imageDict.Add (/*"袁應泰",*/1051, YuenYingTai);
		imageDict.Add (/*"諸葛亮",*/8, ChuGotLeung);
		imageDict.Add (/*"賀若弼",*/1076, HoYeukBut);
		imageDict.Add (/*"賽尚阿",*/1053, ChoiSheungA);
		imageDict.Add (/*"趙奢",*/1005, ChiuChe);
		imageDict.Add (/*"趙括",*/27, ChiuTim);
		imageDict.Add (/*"趙雲",*/1016, ChiuWan);
		imageDict.Add (/*"辰漾守",*/29, SanYeungSau);
		imageDict.Add (/*"郭子儀",*/1029, KwokTseYi);
		imageDict.Add (/*"鄭仁泰",*/1064, ChengYanTai);
		imageDict.Add (/*"鄭成功",*/1041, ChengShingGong);
		imageDict.Add (/*"陳慶之",*/1001, ChanHingChi);
		imageDict.Add (/*"霍去病",*/1013, FokHuiBing);
		imageDict.Add (/*"韓世忠",*/1034, HonSaiChung);
		imageDict.Add (/*"項羽",*/1010, HongYu);
		imageDict.Add (/*"飛廉",*/1072, FeiLim);
		imageDict.Add (/*"馬超",*/1017, MaChiu);
		imageDict.Add (/*"鮮于仲通",*/1044, SinYuChungTong);
		imageDict.Add (/*"黃忠",*/1018, WongChung);
		imageDict.Add (/*"龐德",*/1021, PongDak);
		imageDict.Add (/*"龐涓",*/21, PongGyun);
		imageDict.Add (/*"龐統",*/22, PongTong);
		imageDict.Add (/*"關羽",*/1014, KwanYu);
		
		nameDict.Add (1047, "也先帖木兒");
		nameDict.Add (25, "于謙");
		nameDict.Add (4, "伍子胥");
		nameDict.Add (1074, "伯嚭");
		nameDict.Add (1068, "先軫");
		nameDict.Add (1059, "冼英");
		nameDict.Add (11, "劉基");
		nameDict.Add (14, "劉秉忠");
		nameDict.Add (1066, "史彌堅");
		nameDict.Add (1028, "史思明");
		nameDict.Add (1075, "史萬歲");
		nameDict.Add (23, "司馬懿");
		nameDict.Add (1054, "吳三桂");
		nameDict.Add (2, "吳起");
		nameDict.Add (1019, "呂布");
		nameDict.Add (1011, "周亞夫");
		nameDict.Add (24, "周瑜");
		nameDict.Add (1056, "周秀英");
		nameDict.Add (1035, "哲別");
		nameDict.Add (1070, "單雄信");
		nameDict.Add (18, "黃石公");
		nameDict.Add (1020, "夏侯惇");
		nameDict.Add (1, "姜子牙");
		nameDict.Add (3, "孫武");
		nameDict.Add (9, "孫臏");
		nameDict.Add (1027, "安祿山");
		nameDict.Add (1063, "宗澤");
		nameDict.Add (19, "尉繚子");
		nameDict.Add (1025, "尉遲敬德");
		nameDict.Add (1032, "岳飛");
		nameDict.Add (1038, "常遇春");
		nameDict.Add (1007, "廉頗");
		nameDict.Add (1065, "張叔夜");
		nameDict.Add (10, "張良");
		nameDict.Add (1078, "張遼");
		nameDict.Add (1022, "張郃");
		nameDict.Add (1015, "張飛");
		nameDict.Add (1036, "徐達");
		nameDict.Add (1073, "惡來");
		nameDict.Add (1067, "慕容延釗");
		nameDict.Add (1039, "戚繼光");
		nameDict.Add (1061, "施琅");
		nameDict.Add (5, "曹劌");
		nameDict.Add (17, "曾國藩");
		nameDict.Add (28, "朱般懟");
		nameDict.Add (1024, "李勣");
		nameDict.Add (1042, "李廣利");
		nameDict.Add (1037, "李文忠");
		nameDict.Add (1048, "李景隆");
		nameDict.Add (1008, "李牧");
		nameDict.Add (1058, "李秀寧");
		nameDict.Add (1055, "李自成");
		nameDict.Add (1052, "楊嗣昌");
		nameDict.Add (26, "楊國忠");
		nameDict.Add (1030, "楊業");
		nameDict.Add (1077, "楊素");
		nameDict.Add (1049, "楊鎬");
		nameDict.Add (1003, "樂毅");
		nameDict.Add (1031, "狄青");
		nameDict.Add (1069, "王伯當");
		nameDict.Add (1050, "王化貞");
		nameDict.Add (1043, "王玄謨");
		nameDict.Add (1060, "王異");
		nameDict.Add (1009, "王翦");
		nameDict.Add (1023, "甘寧");
		nameDict.Add (20, "田穰苴");
		nameDict.Add (1062, "畢再遇");
		nameDict.Add (1006, "白起");
		nameDict.Add (1057, "秦良玉");
		nameDict.Add (1071, "程咬金");
		nameDict.Add (1033, "穆桂英");
		nameDict.Add (6, "管仲");
		nameDict.Add (15, "耶律楚材");
		nameDict.Add (1002, "花木蘭");
		nameDict.Add (13, "苗訓");
		nameDict.Add (12, "范增");
		nameDict.Add (16, "范文程");
		nameDict.Add (1046, "范文虎");
		nameDict.Add (7, "范蠡");
		nameDict.Add (1045, "范雍");
		nameDict.Add (1004, "蒙恬");
		nameDict.Add (1026, "薛仁貴");
		nameDict.Add (1012, "衛青");
		nameDict.Add (1040, "袁崇煥");
		nameDict.Add (1051, "袁應泰");
		nameDict.Add (8, "諸葛亮");
		nameDict.Add (1076, "賀若弼");
		nameDict.Add (1053, "賽尚阿");
		nameDict.Add (1005, "趙奢");
		nameDict.Add (27, "趙括");
		nameDict.Add (1016, "趙雲");
		nameDict.Add (29, "辰漾守");
		nameDict.Add (1029, "郭子儀");
		nameDict.Add (1064, "鄭仁泰");
		nameDict.Add (1041, "鄭成功");
		nameDict.Add (1001, "陳慶之");
		nameDict.Add (1013, "霍去病");
		nameDict.Add (1034, "韓世忠");
		nameDict.Add (1010, "項羽");
		nameDict.Add (1072, "飛廉");
		nameDict.Add (1017, "馬超");
		nameDict.Add (1044, "鮮于仲通");
		nameDict.Add (1018, "黃忠");
		nameDict.Add (1021, "龐德");
		nameDict.Add (21, "龐涓");
		nameDict.Add (22, "龐統");
		nameDict.Add (1014, "關羽");
	}
}


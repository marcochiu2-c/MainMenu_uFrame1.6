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

	char[] charToTrim = { '"' };

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


//		Debug.Log (counselorList .Find(x => x.id == 1).ToJSON().ToString());
//		Debug.Log (generalList.Find(x => x.id == 1001).ToJSON().ToString());
		if (IsTodayFirstDraw()) {
			ResetFreeDrawCount();
		}
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
			json["data"].Add ("attributes", counselorList.Find(x => x.id == result).ToJSON());
			json ["table"] = "counselors";
			game.counselor.Add (new Counselor(0, counselorList.Find(x => x.id == result).ToJSON(), result,1));
		} else {

			json["data"].Add ("attributes", generalList.Find(x => x.id == result).ToJSON());
			json ["table"] = "generals";
			game.general.Add (new General(0, generalList.Find(x => x.id == result).ToJSON(), result, 1));
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
					isRedraw = (counselorList.Find(x => x.id == results[i]).Rank != rankStopRedraw) ; // Make it false if any card Rank 2
					if (counselorList.Find(x => x.id == results[i]).Rank == rankNeedRedraw) isMustRedraw = true;
					j = new JSONClass();
					j.Add("type",new JSONData(results[i]));
					j.Add("level",new JSONData(1));
					j.Add("userId",new JSONData(game.login.id));
					j.Add ("attributes", counselorList.Find(x => x.id == results[i]).ToJSON());
					counselorNode.Add(j);
					//storageJsonArray[i] = "{\"type\":"+results[i]+",\"level\":1}";
				}else{ // result == generals
					isRedraw = ( generalList[random-numberOfCounselors].Rank != rankStopRedraw); // Make it false if any card Rank 2
					if (generalList.Find(x => x.id == results[i]).Rank == rankNeedRedraw) isMustRedraw = true;
					j = (JSONNode)s.toJSON ();
					j.Add("type",new JSONData(results[i]));
					j.Add("level",new JSONData(1));
					j.Add("userId",new JSONData(game.login.id));
					j.Add ("attributes", generalList.Find(x => x.id == results[i]).ToJSON());
					generalNode.Add(j);
				}
			}
		} while(isRedraw|| isMustRedraw);
		//string data = "["+ String.Join(" , ", storageJsonArray)+"]";

		if (wsc.conn.IsAlive) {
//			var json = new JSONClass ();
//			json ["data"] = counselorNode;
//			json ["action"] = "NEW";
//			json ["table"] = "multiCounselors";
//			Debug.Log (json.ToString());
			wsc.Send ("multiCounselors","NEW",counselorNode);

//			json ["data"] = generalNode;
//			json ["action"] = "NEW";
//			json ["table"] = "multiGenerals";
//			Debug.Log (json.ToString ());
			wsc.Send ("multiGenerals","NEW",generalNode);
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

	bool IsTodayFirstDraw(){
		if(game.login.attributes["LastDrawTime"]==null){
			ResetFreeDrawCount();
		}
		if (Convert.ToDateTime (game.login.attributes ["LastDrawTime"].ToString ().Trim (charToTrim)).Date < DateTime.Today) {
			return true;
		} else {
			return false;
		}
	}

	void ResetFreeDrawCount(){
		game.login.attributes.Add ("LastDrawTime",new JSONData(DateTime.Now.ToString().Trim(charToTrim)));
		game.login.attributes.Add ("FreeDraw",new JSONData(5));
		game.login.UpdateObject ();
	}

	public void OnConfirmDefaultDraw(){
				if (Convert.ToDateTime (game.login.attributes ["LastDrawTime"].ToString ().Trim(charToTrim)) + new TimeSpan (0, 5, 0) > DateTime.Now) {
			// TODO show Panel for not available
			return;
		}
		if (game.login.attributes ["FreeDraw"].AsInt == 0) {
			// TODO show Panel for no quota left
			return;
		}
		game.login.attributes["LastDrawTime"] = DateTime.Now.ToString();
		game.login.attributes ["FreeDraw"].AsInt = game.login.attributes ["FreeDraw"].AsInt - 1;
		game.login.UpdateObject ();

		DrawSingleCard ();
	}

	public void OnConfirmSuperDraw(){
		game.wealth [0].Deduct (400);	
		DrawSingleCard ();
	}

	public void OnConfirmTimeTravelDraw(){
		game.wealth [1].Deduct (80);	
		DrawSingleCard ();
	}

	public void OnConfirmTenSuperDraw(){
		game.wealth [0].Deduct (3600);	
		DrawTenCards ();
	}
	
	public void OnConfirmTenTimeTravelDraw(){
		game.wealth [1].Deduct (720);	
		DrawTenCards ();
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
	

	private void SetCharacters(){
		LoadBodyPic bodyPic = LoadBodyPic.Instance;
		imageDict = bodyPic.imageDict;
		nameDict = bodyPic.nameDict;
	}
}


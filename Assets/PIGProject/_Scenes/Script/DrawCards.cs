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



	const int rankNeedRedraw = 7;
	const int rankStopRedraw = -1;

	int numberOfCounselors = 0;
	int numberOfGenerals = 0;

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

		wsc.conn.OnError += (sender, e) => {
			Debug.LogError (e.Message);
		};
	}

	void Start(){  // TODO: Change to uframe start event
		CallDrawCards ();
	}

	public void CallDrawCards(){
		
		json = new JSONClass ();

		//Debug.Log (json.ToString ());
		if (wsc.conn.IsAlive) {
			json ["data"] = "3";
			json ["action"] = "GET";
			json ["table"] = "users";
			wsc.Send (json.ToString ()); 

			json ["table"] = "generals";
			wsc.Send (json.ToString ()); 

			json ["table"] = "counselors";
			wsc.Send (json.ToString ()); 
		} else {
			Debug.Log ("Websocket Connection Lost!");
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

	public void DrawSingleCard(){
		int random = UnityEngine.Random.Range (1, numberOfCounselors + numberOfGenerals);
		bool isCounselors = (random <= numberOfCounselors);
		int result = (isCounselors) ? random : random - numberOfCounselors + 1000;

		//Debug.Log ("Random Number: "+random);

		//Debug.Log ("Result Number: "+ result);

		json = new JSONClass ();
		json["data"].Add ("userId",new JSONData(game.login.id));
		json["data"].Add ("type" , new JSONData(result));
		json["data"].Add ("level", new JSONData(1));
		if (isCounselors) {
			json["data"].Add ("attributes", counselorList[result].ToJSON());
			json ["table"] = "counselors";
		} else {
			json["data"].Add ("attributes", generalList[result].ToJSON());
			json ["table"] = "generals";
		}

		json ["action"] = "NEW";

		Debug.Log (json.ToString ());
		wsc.Send (json.ToString ());

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
				//Debug.Log (random);
				results[i] = (isCounselors[i]) ? random : random - numberOfCounselors + 1000;
				if (isCounselors[i]){ 
					isRedraw = (counselorList[random-1].Rank != rankStopRedraw) ; // Make it false if any card Rank 6
					if (counselorList[random-1].Rank == rankNeedRedraw) isMustRedraw = true;
					j = new JSONClass();
					j.Add("type",new JSONData(results[i]));
					j.Add("level",new JSONData(1));
					j.Add("user_id",new JSONData(game.login.id));
					j.Add ("attributes", counselorList[results[i]].ToJSON());
					counselorNode.Add(j);
					//storageJsonArray[i] = "{\"type\":"+results[i]+",\"level\":1}";
				}else{ // result == generals
					isRedraw = ( generalList[random-numberOfCounselors].Rank != rankStopRedraw); // Make it false if any card Rank 6
					if (generalList[random-numberOfCounselors].Rank == rankNeedRedraw) isMustRedraw = true;
					j = (JSONNode)s.toJSON ();
					j.Add("type",new JSONData(results[i]));
					j.Add("level",new JSONData(1));
					j.Add("user_id",new JSONData(game.login.id));
					j.Add ("attributes", generalList[results[i]].ToJSON());
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
			wsc.Send (json.ToString ());

			json ["data"] = generalNode;
			json ["action"] = "NEW";
			json ["table"] = "multiGenerals";
			wsc.Send (json.ToString ());
		} else {
			Debug.Log ("Websocket Connection Lost!");
		}
	}

	// TODO: add method OnButtonDrawPremiumCard

	// TODO: add method OnButtonDrawSingleCardWithCard
}


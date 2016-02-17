using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class DrawCards : MonoBehaviour {
	WsClient wsc;
	Game game;
	JSONClass json;
	List<Generals> generalList = new List<Generals>();
	List<Counselors> counselorList = new List<Counselors> ();

	const int rankNeedRedraw = 7;
	const int rankStopRedraw = -1;

	int numberOfCounselors = 0;
	int numberOfGenerals = 0;

	// Use this for initialization
	void Start () {
		// Initialize services and variables
		game = new Game ();
		wsc = WsClient.Instance;
		generalList = Generals.GetList (1001);
		numberOfGenerals = generalList.Count;

		counselorList = Counselors.GetList (1);
		numberOfCounselors = counselorList.Count;

		Debug.Log ("Number of counselors: "+numberOfCounselors);
		Debug.Log ("Number of generals: "+numberOfGenerals);
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnButtonDrawSingleCard(){
		// TODO: Draw a card and return card number
		int random = UnityEngine.Random.Range (1, numberOfCounselors + numberOfGenerals);
		bool isCounselors = (random <= numberOfCounselors);
		int result = (isCounselors) ? random : random - numberOfCounselors + 1000;

		//Debug.Log ("Random Number: "+random);

		//Debug.Log ("Result Number: "+ result);

		Storage s = new Storage(){productId= result, type =2, quantity =1}; //TODO: Personnel number instead of 0.

		json = new JSONClass ();
		if (isCounselors) {
			json["data"]  = "{\"type\":"+result+",\"level\":1}";
		} else {
			json.Add ("data", (JSONNode)s.toJSON ());
		}

		json["action"]="NEW";
		json["table"]= (isCounselors)? "counselors" : "storage" ;
		Debug.Log (json.ToString ());
		wsc.Send(json.ToString());

		wsc.conn.OnMessage += (sender, e) => {
			Debug.Log(e.Data);
		};
	}

	public void OnButtonDrawTenCards(){
		string[] storageJsonArray = new string[10];
		JSONNode j;
		Storage s = new Storage();
		bool[] isCounselors = new bool[10];
		int random;
		bool isRedraw = true;
		bool isMustRedraw = false;
		JSONClass json;
		int[] results = new int[10];
		do {
			isMustRedraw = false;
			for (int i=0; i<10; i++) {
				random = UnityEngine.Random.Range (1, numberOfCounselors + numberOfGenerals);
				isCounselors[i] = (random <= numberOfCounselors);
				//Debug.Log (random);
				results[i] = (isCounselors[i]) ? random : random - numberOfCounselors + 1000;
				s = new Storage (){productId= results[i], type =2, quantity =1}; //TODO: Personnel number instead of 0.
				if (isCounselors[i]){ 
					isRedraw = (counselorList[random-1].Rank != rankStopRedraw) ; // Make it false if any card Rank 6
					if (counselorList[random-1].Rank == rankNeedRedraw) isMustRedraw = true;
					storageJsonArray[i] = "{\"type\":"+results[i]+",\"level\":1}";
				}else{ // result == generals
					isRedraw = ( generalList[random-numberOfCounselors].Rank != rankStopRedraw); // Make it false if any card Rank 6
					if (generalList[random-numberOfCounselors].Rank == rankNeedRedraw) isMustRedraw = true;
					j = (JSONNode)s.toJSON ();
					storageJsonArray[i] = j.ToString();
				}
			}
		} while(isRedraw|| isMustRedraw);
		//string data = "["+ String.Join(" , ", storageJsonArray)+"]";
		json = new JSONClass ();
		for (int i = 0; i<10; i++) {
			json["data"]  = storageJsonArray [i];
			json["action"]="NEW";
			json["table"]= (isCounselors[i])? "counselors" : "storage" ;
			wsc.Send (json.ToString ());
		}

		json ["data"] = "";

		wsc.conn.OnMessage += (sender, e) => {
			Debug.Log(e.Data);
		};
	}

	// TODO: add method OnButtonDrawPremiumCard

	// TODO: add method OnButtonDrawSingleCardWithCard
}

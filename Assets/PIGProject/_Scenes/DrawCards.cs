using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using WebSocketSharp;

enum jsonFuncNumberEnum {
	allData = 0,

	//  1-18 users 
	newUser = 1,
	updateUser= 2,
	getUserInformationByUserId = 3,
	login = 4,
	logout = 5,
	updateUserExperiencePoint = 6,
	updateUserSNS = 7,
	addCheckInStatus = 8,
	updateCheckInStatus = 9,
	getCheckinInfo = 10,
	setMonthCardExpiryDate = 11,

	updateAllData = 19,

	//  20-29 storage
	setStorageItem = 20,
	setStorageItemUsed = 21,
	getItemInStorage = 22,
	getAllItemsInStorage = 23,
	newMultipleStorage = 25,
	setMultipleCounselors = 26,
	//  30-39 Gift
	addReceivedGift = 30,
	getGiftInfo = 31,
	// 40-49 Wealth
	setWealth = 41,
	setAllWealth = 42,

	// 50-69 Chat
	createChatRoom = 50,
	getActiveChatroomInfo= 51,
	getChatroomByUserId= 52,
	addChatRoomMember = 54,
	removeChatRoomMember = 55,
	getActiveChatroomMemberInfo = 56,
	newChatEntry = 60,
	getChatHistories= 61,
	setChatMessageRead = 62,

	newPrivateChatHistories = 65,
	getPrivateChatHistories = 66,
	setPrivateChatMessageRead = 67,

	// 80-89 Friends
	getActiveFriendshipInfo = 81,
	requestFriend = 82,
	confirmFriend = 83,
	unFriend = 84,

	// 150-159 Generals
	addGeneral = 150,
	updateGeneral = 151,
	markDeleteGeneral = 152,
	addSoldier = 153,
	updateSoldier = 154,
	updateGeneralTeam = 155,


	// 160-169 Counselors
	addCounselorEntry = 160,
	updateCounselors = 161,
	getCounselorInfoByUserId = 162,

	// 170-179 Trainings
	addTraining = 170,
	updateTraining = 171,
	trainingComplete = 172,

	//200-219 Wars/ Battle
	setBattleState = 200,
	newPVPWar = 201,
	addGeneralsToWarfare = 202,

	getWarfareInfoByUserId = 210,
	getWarfareGeneralsInfoByUserId = 211,
	findMatchingPlayer = 212,
	findMatchingPlayerSpecifyCountry = 213,



	// 220-239 War equipments
	addWeapon = 220,
	addProtectiveEquipment = 221,
	getWeaponsBySoldierId = 230,
	getProtectiveEquipmentsBySoldierId = 231,

	// 240-249 Buildings
	addBuilding = 240,
	updateBuilding = 241,
};




public class DrawCards : MonoBehaviour {
	WsClient wsc;
	Game game;
	JSONNode json;
	List<Generals> generalList = new List<Generals>();
	List<Counselors> counselorList = new List<Counselors> ();

	const int rankNeedRedraw = 7;
	const int rankStopRedraw = -1;

	int numberOfCounselors = 0;
	int numberOfGenerals = 0;

	// Use this for initialization
	void Awake () {
		// Initialize services and variables
		game = Game.Instance;
		wsc = WsClient.Instance;

		generalList = Generals.GetList (1001);
		numberOfGenerals = generalList.Count;

		counselorList = Counselors.GetList (1);
		numberOfCounselors = counselorList.Count;

		Debug.Log ("Number of counselors: " + numberOfCounselors);
		Debug.Log ("Number of generals: " + numberOfGenerals);


		wsc.conn.OnMessage += (sender, e) => { 
			Debug.Log (e.Data);
			var j = JSON.Parse(e.Data);

			switch ((jsonFuncNumberEnum)j["func"].AsInt){
			case jsonFuncNumberEnum.getUserInformationByUserId:
				game.login = new Login((JSONClass)j["obj"]);
				break;
			case jsonFuncNumberEnum.getPrivateChatHistories:
				var jArray = j["obj"];
				Debug.Log(j["obj"][1]["message"]);
				break;
			default:
				break;
			}
		};


		wsc.conn.OnError += (sender, e) => {
			Debug.LogError (e.Message);
		};
	}

	void Start(){


		json = new JSONClass ();
		json ["data"] = "3";

		json ["action"] = "GET";
		json ["table"] = "users";
		//json ["table"] = "private_chat_histories";
		//Debug.Log (json.ToString ());//{"action":"GET", "table":"users", "data":"3"}
		if (wsc.conn.IsAlive) {
			wsc.Send (json.ToString ()); 
		} else {
			Debug.Log ("Connection Lost!");
		}
	}

	// Update is called once per frame
	void Update () {
  
	}


	public void OnButtonDrawSingleCard(){
		int random = UnityEngine.Random.Range (1, numberOfCounselors + numberOfGenerals);
		bool isCounselors = (random <= numberOfCounselors);
		int result = (isCounselors) ? random : random - numberOfCounselors + 1000;

		//Debug.Log ("Random Number: "+random);

		//Debug.Log ("Result Number: "+ result);

		Storage s = new Storage(){productId= result, type =2, quantity =1}; //TODO: Personnel number instead of 0.

		json = new JSONClass ();
		if (isCounselors) {
			json["data"].Add ("userId",new JSONData(game.login.id));
			json["data"].Add ("type" , new JSONData(result));
			json["data"].Add ("level", new JSONData(1));
		} else {
			json.Add ("data", (JSONNode)s.toJSON ());
			json["data"].Add ("userId",new JSONData (game.login.id));
		}
		if (isCounselors) {
			json ["action"] = "NEW";
			json ["table"] = (isCounselors) ? "counselors" : "storage";
			Debug.Log (json.ToString ());
			wsc.Send (json.ToString ());
		}
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

	}

	// TODO: add method OnButtonDrawPremiumCard

	// TODO: add method OnButtonDrawSingleCardWithCard
}


using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using System.Net.Sockets;
using System.Net;
using SimpleJSON;

enum jsonFuncNumberEnum {
	allData = 0,

	//  1-19 users 
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
	getCheckinGiftInfo = 12,
	newAccountWealth = 13,
	newAccountBuilding = 14,
	getUserInformationByDeviceId = 15,
	getUserInformationBySnsUrl = 16,
	newAccountSoldier = 17,
	updateAllData = 19,

	//  20-29 storage
	setStorageItem = 20,
	setStorageItemUsed = 21,
	getItemInStorage = 22,
	getAllItemsInStorage = 23,
	getItemsInStorageByType = 24,
	newMultipleStorage = 25,
	newMultipleCounselors = 26,
	newMultipleGenarals = 27,

	//  30-39 Gift
	addReceivedGift = 30,
	getGiftInfo = 31,
	getGiftContent = 32,

	// 40-49 Wealth
	setWealth = 41,
	setAllWealth = 42,
	getWealth = 43,

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
	getFriendshipInfo = 85,

	// 150-159 Generals
	addGeneral = 150,
	updateGeneral = 151,
	changeGeneralStatus = 152,
	addSoldier = 153,
	updateSoldier = 154,
	updateGeneralTeam = 155,
	getGeneralOnly = 156,
	getGeneralTeam = 157,
	getSoldier = 158,

	// 160-169 Counselors
	addCounselorEntry = 160,
	updateCounselors = 161,
	getCounselorInfoByUserId = 162,
	changeCounselorStatus = 163,

	// 170-179 Trainings
	addTraining = 170,
	updateTraining = 171,
	trainingComplete = 172,
	getTraining = 173,

	//200-219 Wars/ Battle
	setBattleState = 200,
	newPVPWar = 201,
	addGeneralsToWarfare = 202,

	getWarfareInfoByUserId = 210,
	getWarfareGeneralsInfoByUserId = 211,
	findMatchingPlayer = 212,
	findMatchingPlayerSpecifyCountry = 213,
	getWholeWarfareInfoByUserId = 214,

	// 220-239 War equipments
	addWeapon = 220,
	addArmor  = 221,
	addShield = 222,
	updateWeapon = 223,
	updateArmor  = 224,
	updateShield = 225,
	getWeaponsByUserId = 230,
	getArmorsByUserId  = 231,
	getShieldsByUserId = 232,
		
	// 240-249 Buildings
	addBuilding = 240,
	updateBuilding = 241,
	getBuilding = 242,

	// 250-259 Artisans
	addArtisanJob = 250,
	updateArtisanJob = 251,
	getArtisanJob = 252,
	newAccountArtisanJobs = 253,

	// 280-289 New Account equipment
	newAccountWeapon = 280,
	newAccountArmor  = 281,
	newAccountShield = 282,

};


public class WsClient {
	//string server = "";
	public WebSocket conn;
	//string table = "";
	//string result = "";
	private string ip = "23.91.96.158";
//	private string ip = "192.168.100.64";
	private int port = 8000; 
	Game game;
	// Use this for initialization

	private static readonly WsClient s_Instance = new WsClient();

	public static WsClient Instance
	{
		get
		{
			return s_Instance;
		}
	}

	public bool healthCheck(){
		return true;
	}

	public WsClient () {
		game = Game.Instance;
		bool success = TestConnection();
		var count = 0;
		if (success) {
			Debug.Log ("Server check ok");
			string server = String.Format("ws://{0}:{1}/",  ip,  port );
			using (conn = new WebSocket (server,"game"));
			conn.Connect ();

			conn.OnOpen += (sender, e) => conn.Send ("Server Connected");

			conn.OnError += (sender, e) => {
				Debug.Log("Websocket Error");
				conn.Close ();
			};

			conn.OnClose += (sender, e) => {
//				Debug.Log("Websocket Close");
				conn.Close ();
			};

			conn.OnMessage += (sender, e) => { 
				if (e.Data != "[]"){
					Debug.Log("Received Message: " +e.Data);
					handleMessage(JSON.Parse(e.Data));
				}
			};

		} else {
			Debug.Log ("Server check failed");
			return;
		}

		conn.SslConfiguration.ServerCertificateValidationCallback =
			(sender, certificate, chain, sslPolicyErrors) => {
			// Do something to validate the server certificate.
			Debug.Log ("Cert: " + certificate);

			return true; // If the server certificate is valid.
		};



	}

	void handleMessage(JSONNode j){
		Game game = Game.Instance;
		JSONArray jArray;
		JSONNode json;
		switch ((jsonFuncNumberEnum)j ["func"].AsInt) {
		case jsonFuncNumberEnum.allData:
//			game.parseJSON(e.Data);
			break;
		case jsonFuncNumberEnum.getUserInformationByUserId:
			if (j["obj"]!="[  ]"){
				MainScene.UserInfo = (JSONClass)j ["obj"];
			}
			break;
		case jsonFuncNumberEnum.getUserInformationByDeviceId:
			if (j["obj"]!="[  ]"){
				MainScene.UserInfo = (JSONClass)j ["obj"];
				Debug.Log ("MainScene.UserInfo received");
				LoginScreen.user = (JSONClass)j ["obj"];
			}
			break;
		case jsonFuncNumberEnum.getUserInformationBySnsUrl:
			if (j["obj"]!="[  ]"){
				MainScene.UserInfo = (JSONClass)j ["obj"];
				LoginScreen.userWithSns = (JSONClass)j ["obj"];
			}
			break;
		case jsonFuncNumberEnum.getPrivateChatHistories:
			if (j["obj"]!="[  ]"){
				jArray = (JSONArray)j ["obj"];
				Debug.Log (j ["obj"] [1] ["message"]);
			}
			break;
		case jsonFuncNumberEnum.addGeneral:
			if (j["obj"]!="[  ]"){
				MainScene.GeneralLastInsertId = j ["obj"] ["lastInsertId"].AsInt;
			}
			break;
		case jsonFuncNumberEnum.addCounselorEntry:
			if (j["obj"]!="[  ]"){
				MainScene.CounselorLastInsertId = j ["obj"] ["lastInsertId"].AsInt;
			}
			break;	
		case jsonFuncNumberEnum.getGeneralOnly:
		case jsonFuncNumberEnum.newMultipleStorage:
			if (j ["obj"] != "") {
				MainScene.GeneralInfo = j ["obj"];
			}
			break;
		case jsonFuncNumberEnum.getCounselorInfoByUserId:
		case jsonFuncNumberEnum.newMultipleCounselors:
			if (j ["obj"] != "") {
				MainScene.CounselorInfo = j ["obj"];
			}
			break;
		case jsonFuncNumberEnum.getCheckinGiftInfo:
			var chkin = CheckInContent.Instance;
			if (j ["obj"] != "") {
				for (int i = 0; i < 30; i++) {
					chkin.giftNumber [i] = j ["obj"] [i].AsInt;
				}
			}
			break;
		case jsonFuncNumberEnum.getAllItemsInStorage:
			if (j ["obj"] != "") {
				MainScene.StorageInfo = j ["obj"];
			}
			break;
		case jsonFuncNumberEnum.getWarfareInfoByUserId:
			if (j["obj"]!="[  ]"){
				MainScene.WarfareInfo = j["obj"];
			}
			break;
		case jsonFuncNumberEnum.getCheckinInfo:
			if (j["obj"]!="[  ]"){
				game.checkinStatus = new CheckInStatus((JSONClass)j["obj"]);
				CheckIn.checkedInDate = game.checkinStatus.days.Count;
			}
			break;
		case jsonFuncNumberEnum.newAccountWealth:
		case jsonFuncNumberEnum.getWealth:
			if (j["obj"].Count != 0){
				List<Wealth> wealthList;
				wealthList = new List<Wealth> ();
		//					SimpleJSON.JSONArray wealths = (SimpleJSON.JSONArray) SimpleJSON.JSON.Parse (e.Data);
				Debug.Log (j["obj"]);
				JSONArray wealths = (SimpleJSON.JSONArray) j["obj"];
				IEnumerator w = wealths.GetEnumerator ();
				while (w.MoveNext ()) {
					wealthList.Add (new Wealth((SimpleJSON.JSONClass) w.Current));
				}
				for (int i = 0; i<3; i++){
					if (wealthList[i].type == 1){
						MainScene.sValue = wealthList[i].value.ToString ();
						MainScene.silverFeatherValue = wealthList[i];
					}else if (wealthList[i].type == 2){
						MainScene.sdValue = wealthList[i].value.ToString ();
						MainScene.stardustValue = wealthList[i];
					}else if (wealthList[i].type == 3){
						MainScene.rValue = wealthList[i].value.ToString ();
						MainScene.resourceValue = wealthList[i];
					}
				}
				game.wealth = wealthList;
			}else{
				if (game.login.id!=0){
					Send ("wealth","GET",new JSONData(game.login.id));
				}
//				}else{
//					Send ("getUserInformationByDeviceId","GET",new JSONData(SystemInfo.deviceUniqueIdentifier));
//				}
			}
			break;
		case jsonFuncNumberEnum.getWeaponsByUserId:
			if (j["obj"]!="[  ]"){
				MainScene.WeaponInfo = j["obj"];
			}
			break;
		case jsonFuncNumberEnum.getArmorsByUserId:
			if (j["obj"]!="[  ]"){
				MainScene.ArmorInfo = j["obj"];
			}
			break;
		case jsonFuncNumberEnum.getShieldsByUserId:
			if (j["obj"]!="[  ]"){
				MainScene.ShieldInfo = j["obj"];
			}
			break;
		case jsonFuncNumberEnum.updateCheckInStatus:
			if (j["obj"]!="[  ]"){
				Debug.Log(String.Format("Update Check In Status: {0}",(j["obj"]["success"].AsBool ? "Success" : "Failed")));
			}
			break;

		case jsonFuncNumberEnum.getFriendshipInfo:
//			Debug.Log(j);
			if (j["obj"]!="[  ]"){
				MainScene.FriendInfo = j["obj"];
			}
			break;

		case jsonFuncNumberEnum.getTraining:
			if (j["obj"]!="[  ]"){
				MainScene.TrainingInfo = j["obj"];
			}
			break;
//			addArtisanJob = 250,
//			updateArtisanJob = 251,
//			getArtisanJob = 252,
		case jsonFuncNumberEnum.getArtisanJob: case jsonFuncNumberEnum.newAccountArtisanJobs:
			if (j["obj"]!="[  ]"){
				MainScene.ArtisanInfo = j["obj"];
			}
			break;
		case jsonFuncNumberEnum.newUser:
			if (j["obj"]!="[  ]"){
				MainScene.newUserId = j["obj"]["lastInsertId"].AsInt;
			}
			break;
		case jsonFuncNumberEnum.getSoldier:
			if (j["obj"]!="[  ]"){
				MainScene.SoldierInfo = j["obj"];
			}
			break;
		default:
			break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}

	private bool TestConnection(){
		TcpClient client = new TcpClient(); 
		bool result = false;
		try
		{
			client.BeginConnect(ip, port, null, null).AsyncWaitHandle.WaitOne(3000); 
			result = client.Connected;
		}


		catch { }
		finally {
			client.Close ();
		}
		return result;
	}

	public void Send(String json){
		if (conn.IsAlive) {
			Debug.Log ("Sending Command");
			conn.Send (json);
		}
	}

	public void OnConnectionFailed(){
		Debug.Log( "Connection lost");
	}

	public static DateTime UnixTimestampToDateTime(long timestamp,int tz)
	{
		DateTime unixRef = new DateTime(1970, 1, 1, 0, 0, 0);
		return unixRef.AddSeconds(timestamp+tz*60*60);
	}

	public static long UnixTimeStamp(DateTime dt)
	{
		var timeSpan = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
		return (long)timeSpan.TotalSeconds;
	}

	public static string JSDate(DateTime dt)
	{
		return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute + ":" + dt.Second;
	}

	public void Send(string table,string action, JSONNode j){
		if (conn.IsAlive) {
			JSONClass json = new JSONClass ();
			json.Add ("action", new JSONData (action));
			json.Add ("table", new JSONData (table));
			json.Add ("data", j);
			Debug.Log (json.ToString ());
			conn.Send (json.ToString ());
		}
	}



}

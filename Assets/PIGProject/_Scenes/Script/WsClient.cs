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
	getCheckinGiftInfo = 12,

	updateAllData = 19,

	//  20-29 storage
	setStorageItem = 20,
	setStorageItemUsed = 21,
	getItemInStorage = 22,
	getAllItemsInStorage = 23,
	getItemsInStorageByType = 24,
	newMultipleStorage = 25,
	newMultipleCounselors = 26,

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

	// 150-159 Generals
	addGeneral = 150,
	updateGeneral = 151,
	changeGeneralStatus = 152,
	addSoldier = 153,
	updateSoldier = 154,
	updateGeneralTeam = 155,
	getGeneralOnly = 156,


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
	addProtectiveEquipment = 221,
	updateWeapon = 222,
	updateProtectiveEquipment = 223,
	getWeaponsBySoldierId = 230,
	getProtectiveEquipmentsBySoldierId = 231,
	getWeaponByUserId = 232,
	getProtectiveEquipmentByUserId = 233,

	// 240-249 Buildings
	addBuilding = 240,
	updateBuilding = 241,
	getBuilding = 242,
};


public class WsClient {
	//string server = "";
	public WebSocket conn;
	//string table = "";
	//string result = "";
	private string ip = "192.168.100.64";
	private int port = 8000; 
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
		Game game = Game.Instance;
		bool success = TestConnection();
		var count = 0;
		if (success) {
			Debug.Log ("Server check ok");
			string server = String.Format("wss://{0}:{1}/",  ip,  port );
			using (conn = new WebSocket (server,"game"));
			conn.Connect ();

			conn.OnOpen += (sender, e) => conn.Send ("Server Connected");

			conn.OnError += (sender, e) => {
				Debug.Log("Websocket Error");
				conn.Close ();
			};

			conn.OnClose += (sender, e) => {
				Debug.Log("Websocket Close");
				conn.Close ();
			};

			conn.OnMessage += (sender, e) => { 
//				Debug.Log(e.Data);
				var j = JSON.Parse(e.Data);
				JSONArray jArray;
				JSONNode json;
				switch ((jsonFuncNumberEnum)j["func"].AsInt){
				case jsonFuncNumberEnum.allData:
					game.parseJSON(e.Data);
					break;
				case jsonFuncNumberEnum.getUserInformationByUserId:
					game.login = new Login((JSONClass)j["obj"]);
					break;
				case jsonFuncNumberEnum.getPrivateChatHistories:
					jArray = (JSONArray)j["obj"];
					Debug.Log(j["obj"][1]["message"]);
					break;
				case jsonFuncNumberEnum.addGeneral:
					MainScene.GeneralLastInsertId = j["obj"]["lastInsertId"].AsInt;
					break;
				case jsonFuncNumberEnum.addCounselorEntry:
					MainScene.CounselorLastInsertId = j["obj"]["lastInsertId"].AsInt;
					break;	
				case jsonFuncNumberEnum.getGeneralOnly:
					MainScene.GeneralInfo = j["obj"];
					break;
				case jsonFuncNumberEnum.getCounselorInfoByUserId:
					MainScene.CounselorInfo = j["obj"];
					break;
				case jsonFuncNumberEnum.newMultipleStorage:
					MainScene.GeneralInfo = j["obj"];
					break;
				case jsonFuncNumberEnum.newMultipleCounselors:
					MainScene.CounselorInfo = j["obj"];
					break;
				case jsonFuncNumberEnum.getCheckinGiftInfo:
					var chkin = CheckInContent.Instance;
					for (int i = 0; i < 30; i++){
						chkin.giftNumber[i]  =j["obj"][i].AsInt;
					}
					break;
				case jsonFuncNumberEnum.getAllItemsInStorage:
					MainScene.StorageInfo = j["obj"];
					break;
				case jsonFuncNumberEnum.getWarfareInfoByUserId:
					MainScene.WarfareInfo = j["obj"];
					break;
				case jsonFuncNumberEnum.getCheckinInfo:
					game.checkinStatus = new CheckInStatus((JSONClass)j["obj"]);
					CheckIn.checkedInDate = game.checkinStatus.days.Count;
					Debug.Log(e.Data);
					break;
				case jsonFuncNumberEnum.getWealth:
						List<Wealth> wealthList;
						wealthList = new List<Wealth> ();
						//SimpleJSON.JSONArray wealths = (SimpleJSON.JSONArray) SimpleJSON.JSON.Parse (e.Data);
						JSONArray wealths = (SimpleJSON.JSONArray) j["obj"];
						IEnumerator w = wealths.GetEnumerator ();
						while (w.MoveNext ()) {
							wealthList.Add (new Wealth((SimpleJSON.JSONClass) w.Current));
						}
						MainScene.sValue = wealthList[0].value.ToString ();
						MainScene.rValue = wealthList[1].value.ToString ();
						MainScene.sdValue = wealthList[2].value.ToString ();
					break;
				case jsonFuncNumberEnum.updateCheckInStatus:
					Debug.Log(String.Format("Update Check In Status: {0}",(j["obj"]["success"].AsBool ? "Success" : "Failed")));
					break;
				default:
					break;
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
		Debug.Log ("Sending Command");
		conn.Send (json);
	}

	public void OnConnectionFailed(){
		Debug.Log( "Connection lost");
	}

	public String UnixTimestampToDateTime(long timestamp,int tz)
	{
		DateTime unixRef = new DateTime(1970, 1, 1, 0, 0, 0);
		return unixRef.AddSeconds(timestamp+tz*60*60).ToString();
	}

	public long UnixTimeNow(DateTime dt)
	{
		var timeSpan = (dt - new DateTime(1970, 1, 1, 0, 0, 0));
		return (long)timeSpan.TotalSeconds;
	}


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using UnityEngine;
using WebSocketSharp;
using System.Net.Sockets;
using System.Net;
using SimpleJSON;

public class WsClientService : WsClientServiceBase {
	
	//string server = "";
	public WebSocket conn;
	//string table = "";
	//string result = "";
	private string ip = "192.168.100.64";
	private int port = 8000; 
	// Use this for initialization
	
	/// <summary>
	/// This method is invoked whenever the kernel is loading.
	/// Since the kernel lives throughout the entire lifecycle of the game, this will only be invoked once.
	/// </summary>
	public override void Setup() {
		base.Setup();
		// Use the line below to subscribe to events.
		// this.OnEvent<MyEvent>().Subscribe(myEventInstance=>{  TODO });
		
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
	
	/// <summary>
	// This method is executed when using this.Publish(new ViewCreatedEvent())
	/// </summary>
	/*
	public override void ViewCreatedEventHandler(ViewCreatedEvent data) {
		base.ViewCreatedEventHandler(data);
		// Process the commands information.  Also, you can publish new events by using the line below.
		// this.Publish(new AnotherEvent())
	}
	*/
}

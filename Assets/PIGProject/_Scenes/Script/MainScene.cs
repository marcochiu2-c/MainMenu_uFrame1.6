using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

// TODO if anything updated wealth table, pls set MainScene.needReloadFromDB to true




public class MainScene : MonoBehaviour {
	WsClient wsc;
	Text silverFeatherText;
	Text resourceText;
	Text starDustText;
	JSONClass json;
	Game game;
	public static GameObject MainUIHolder;
	public static bool needReloadFromDB = true;
	public static int userId = 0;

	Wealth wealth;
	public static string sValue ="";
	public static string rValue ="";
	public static string sdValue ="";


	public static int GeneralLastInsertId = 0;
	public static int CounselorLastInsertId = 0;
	public static JSONNode GeneralInfo = null;
	public static JSONNode CounselorInfo = null;
	public static JSONNode StorageInfo = null;
	public static JSONNode WarfareInfo = null;
	public static Nullable<DateTime> GeneralLastUpdate = null;
	public static Nullable<DateTime> CounselorLastUpdate = null;
	public static Nullable<DateTime> StorageLastUpdate = null;
	public static Nullable<DateTime> WarfareLastUpdate = null;

	void Start(){
		CallMainScene ();
	}
	// Use this for initialization
	public void CallMainScene () {
		if (MainScene.needReloadFromDB) {
			MainUIHolder = GameObject.Find ("MainUIHolder");
			Debug.Log (MainUIHolder);
			//silverFeatherText  = GameObject.Find ("/Canvas/HeaderHolder/SilverFeatherPanel/SilverFeatherText").GetComponents<Text>() [0];
			//resourceText = GameObject.Find ("/Canvas/HeaderHolder/ResourcesPanel/ResourcesText").GetComponents<Text> () [0];
			//starDustText = GameObject.Find ("/Canvas/HeaderHolder/StardustPanel/StardustText").GetComponents<Text> () [0];
			silverFeatherText = GameObject.Find ("SilverFeatherText").GetComponents<Text> () [0];
			resourceText = GameObject.Find ("ResourcesText").GetComponents<Text> () [0];
			starDustText = GameObject.Find ("StardustText").GetComponents<Text> () [0];
			game = Game.Instance;
			wsc = WsClient.Instance;
			MainScene.userId = 3;  // TODO: load userID from DB
			json = new JSONClass ();
			json.Add ("data", new JSONData (MainScene.userId));
			json ["action"] = "GET";
			json ["table"] = "wealth";

			wsc.Send (json.ToString ());
			MainScene.needReloadFromDB = false;
		}
		

	}

	// Update is called once per frame
	void Update () {
		if (MainScene.sValue != "" || MainScene.rValue != "" || MainScene.sdValue != "") {
			silverFeatherText.text = MainScene.sValue;
			resourceText.text = MainScene.rValue;
			starDustText.text = MainScene.sdValue;
			MainScene.sValue ="";
			MainScene.rValue ="";
			MainScene.sdValue ="";
		}

		SetGameObjectFromServer ();
	}

	void SetGameObjectFromServer(){
		var count = 0;
		if (MainScene.GeneralInfo != null) {
			game.general = new List<General> ();
			count = MainScene.GeneralInfo.Count;
			for (var i = 0; i < count; i++) {
				game.general.Add (new General (MainScene.GeneralInfo[i]));
			}
			//			Debug.Log(MainScene.GeneralInfo);
			//			Debug.Log(game.storage.Count);
			MainScene.GeneralInfo = null;
			MainScene.GeneralLastUpdate = DateTime.Now;
		}
		if (MainScene.CounselorInfo != null) {
			game.counselor = new List<Counselor> ();
			count = MainScene.CounselorInfo.Count;
			for (var i = 0; i < count; i++) {
				game.counselor.Add (new Counselor (MainScene.CounselorInfo[i]));
			}
			//			Debug.Log(MainScene.CounselorInfo);
			//			Debug.Log(game.counselor.Count);
			MainScene.CounselorInfo = null;
			MainScene.CounselorLastUpdate = DateTime.Now;
		}
		if (MainScene.StorageInfo != null) {
			game.storage = new List<Storage> ();
			count = MainScene.StorageInfo.Count;
			for (var i = 0; i < count; i++) {
				game.storage.Add (new Storage (MainScene.StorageInfo[i]));
			}
			MainScene.StorageInfo = null;
			MainScene.StorageLastUpdate = DateTime.Now;
		}
		if (MainScene.WarfareInfo != null) {
			game.warfare = new List<Warfare> ();
			count = MainScene.WarfareInfo.Count;
			int x=0;
			for (var i = 0; i < count; i++) {
				x = game.warfare.FindIndex(item => item.id == MainScene.WarfareInfo[i]["id"].AsInt);
				if (x>=0){
					game.warfare[x].AddGeneral(MainScene.WarfareInfo[i]["general_id"].AsInt,MainScene.WarfareInfo[i]["general_json"]);
				}else {
					game.warfare.Add (new Warfare (MainScene.WarfareInfo[i]));
				}
			}
			MainScene.WarfareInfo = null;
			MainScene.WarfareLastUpdate = DateTime.Now;
		}
		if (MainScene.CounselorLastInsertId != 0) {
			count = game.counselor.Count;
			game.counselor [count - 1].id = MainScene.CounselorLastInsertId;
			MainScene.CounselorLastInsertId = 0;
		}

		if (MainScene.GeneralLastInsertId != 0) {
			count = game.general.Count;
			game.general [count - 1].id = MainScene.GeneralLastInsertId;
			MainScene.GeneralLastInsertId = 0;
		}
	}
}

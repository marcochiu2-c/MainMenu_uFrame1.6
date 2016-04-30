using UnityEngine;
using UnityEngine.UI;
using System;
using OnePF;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
//using Endgame;
//using Facebook.Unity;

// TODO if anything updated wealth table, pls set MainScene.needReloadFromDB to true
using Facebook.Unity;




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
	public Button MainCharButton;
	public static string sValue ="";
	public static string rValue ="";
	public static string sdValue ="";
	public static string noticeURL ="";
	public static Wealth resourceValue = null;
	public static Wealth stardustValue = null;
	public static Wealth silverFeatherValue = null;
	public static int newUserId = 0;
	public static int GeneralLastInsertId = 0;
	public static int CounselorLastInsertId = 0;
	public static JSONNode UserInfo = null;
	public static JSONNode GeneralInfo = null;
	public static JSONNode CounselorInfo = null;
	public static JSONNode StorageInfo = null;
	public static JSONNode WarfareInfo = null;
	public static JSONNode WeaponInfo = null;
	public static JSONNode ArmorInfo = null;
	public static JSONNode ShieldInfo = null;
	public static JSONNode FriendInfo = null;
	public static JSONNode TrainingInfo = null;
	public static JSONNode ArtisanInfo = null;
	public static JSONNode SoldierInfo = null;
	public static Nullable<DateTime> GeneralLastUpdate = null;
	public static Nullable<DateTime> CounselorLastUpdate = null;
	public static Nullable<DateTime> StorageLastUpdate = null;
	public static Nullable<DateTime> WarfareLastUpdate = null;
	public static Nullable<DateTime> FriendLastUpdate = null;
	Shop shop;

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

	void Start(){

		CallMainScene ();
	}
	// Use this for initialization
	public void CallMainScene () {
//		MeshRenderer renderer = listView.transform.GetComponentInChildren<MeshRenderer>();
//		renderer.enabled = false; 
		if (FB.IsLoggedIn) {
			FB.API("me/picture?type=square&height=128&width=128", HttpMethod.GET, FbGetPicture);
		}


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
			MainScene.userId = game.login.id;  // TODO: load userID from DB
			if (MainScene.userId != 0){
				reloadFromDB();
			}else{
				MainScene.needReloadFromDB = true;
			}
			Store.GetStorageInfoFromDB();

			LoadHeadPic headPic = LoadHeadPic.SetCharacters();
			LoadBodyPic bodyPic = LoadBodyPic.SetCharacters();
		}

		shop = GetComponent<Shop> ();
		Invoke ("CallShop", 3);


		wsc.Send("notice_url","GET",new JSONData (MainScene.userId));
		InvokeRepeating("OnGeneralTrainComplete",1,4);
		InvokeRepeating("CheckObject",1,2);
	}

	void CallShop(){
		shop.CallShop ();
	}

	void CheckObject(){
		if (game == null) {
			Debug.Log ("Game object null");
			game = Game.Instance;
			silverFeatherText = GameObject.Find ("SilverFeatherText").GetComponents<Text> () [0];
			resourceText = GameObject.Find ("ResourcesText").GetComponents<Text> () [0];
			starDustText = GameObject.Find ("StardustText").GetComponents<Text> () [0];
			MainScene.sValue = game.wealth[0].value.ToString();
			MainScene.sdValue = game.wealth[1].value.ToString();
			MainScene.rValue = game.wealth[2].value.ToString();
			reloadFromDB();
		}
	}

	public void reloadFromDB(){
		wsc.Send("wealth","GET",new JSONData (MainScene.userId));
		wsc.Send("counselors","GET",new JSONData (MainScene.userId));
		wsc.Send("generals","GET",new JSONData (MainScene.userId));
		wsc.Send ("weapon","GET",new JSONData (MainScene.userId));
		wsc.Send ("armor","GET",new JSONData (MainScene.userId));
		wsc.Send ("shield","GET",new JSONData (MainScene.userId));
		wsc.Send ("soldier","GET",new JSONData (MainScene.userId));
		wsc.Send ("artisan","GET", new JSONData (MainScene.userId));
		wsc.Send ("getCheckinInfo","GET", new JSONData (MainScene.userId));
		wsc.Send ("training","GET", new JSONData (MainScene.userId));
		MainScene.needReloadFromDB = false;
	}

	// Update is called once per frame
	void Update () {
		if (MainScene.sValue != "" ) {
			silverFeatherText.text = MainScene.sValue;
			MainScene.sValue ="";
		}
		if ( MainScene.rValue != "" ) {
			resourceText.text = MainScene.rValue;
			MainScene.rValue ="";
		}
		if ( MainScene.sdValue != "") {
			starDustText.text = MainScene.sdValue;
			MainScene.sdValue ="";
		}

		SetGameObjectFromServer ();

		if (MainScene.userId == 0) {
			wsc.Send ("getUserInformationByDeviceId", "GET", new JSONData (SystemInfo.deviceUniqueIdentifier));
		}
	}


	void SetGameObjectFromServer(){
		var count = 0;
		if (MainScene.UserInfo != null) {
			game.login = new Login ((JSONClass)MainScene.UserInfo);
//			Debug.Log (game.login.ToString());
			MainScene.UserInfo = null;
			if (MainScene.userId == 0){
				MainScene.userId = game.login.id;
				reloadFromDB();
			}
			MainCharButton.transform.GetChild(0).GetComponent<Text>().text = "等級 "+CharacterPage.UserLevelCalculator(game.login.exp).ToString();

		}
		if (MainScene.GeneralInfo != null) {
			game.general = new List<General> ();
			count = MainScene.GeneralInfo.Count;
			for (var i = 0; i < count; i++) {
				game.general.Add (new General (MainScene.GeneralInfo[i]));
			}
			//			Debug.Log(MainScene.GeneralInfo);
			//			Debug.Log(game.general.Count);
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
			Debug.Log (game.counselor[count-1].toJSON().ToString());
			MainScene.CounselorLastInsertId = 0;
		}

		if (MainScene.GeneralLastInsertId != 0) {
			count = game.general.Count;
			game.general [count - 1].id = MainScene.GeneralLastInsertId;
			MainScene.GeneralLastInsertId = 0;
			wsc.Send("generals","GET",new JSONData (MainScene.userId));
		}
		if (MainScene.stardustValue != null || MainScene.silverFeatherValue != null || MainScene.resourceValue != null) {
			game.wealth[0] = MainScene.silverFeatherValue;
			game.wealth[1] = MainScene.stardustValue;
			game.wealth[2] = MainScene.resourceValue;
			MainScene.silverFeatherValue = MainScene.stardustValue = MainScene.resourceValue =null;
		}
		if (MainScene.FriendInfo != null) {
			game.friend = new List<Friend> ();
			count = MainScene.FriendInfo.Count;
			for (var i = 0; i < count; i++) {
				game.friend.Add (new Friend (MainScene.FriendInfo[i]));
			}
			var cnt = game.friend.Count;
			MainScene.FriendInfo = null;
			MainScene.FriendLastUpdate = DateTime.Now;
		}
		if (MainScene.TrainingInfo != null) {
			game.trainings = new List<Trainings> ();
			count = MainScene.TrainingInfo.Count;
			for (var i = 0; i < count; i++) {
				game.trainings.Add (new Trainings (MainScene.TrainingInfo[i]));
			}
			MainScene.TrainingInfo = null;
		}
		if (MainScene.ArtisanInfo != null) {
			game.artisans = new List<Artisans> ();
			count = MainScene.ArtisanInfo.Count;
			for (var i = 0; i < count; i++) {
				game.artisans.Add (new Artisans (MainScene.ArtisanInfo[i]));
			}
			MainScene.ArtisanInfo = null;
		}
		if (MainScene.WeaponInfo != null) {
			game.weapon = new List<Weapon>();
			count = MainScene.WeaponInfo.Count;
			for (var i = 0; i < count; i++) {
				game.weapon.Add (new Weapon (MainScene.WeaponInfo[i]));
			}

			MainScene.WeaponInfo = null;
		}
		if (MainScene.ArmorInfo != null) {
			game.armor = new List<Armor>();
			count = MainScene.ArmorInfo.Count;
			for (var i = 0; i < count; i++) {
				game.armor.Add (new Armor (MainScene.ArmorInfo[i]));
			}
			
			MainScene.ArmorInfo = null;
		}
		if (MainScene.ShieldInfo != null) {
			game.shield = new List<Shield>();
			count = MainScene.ShieldInfo.Count;
			for (var i = 0; i < count; i++) {
				game.shield.Add (new Shield (MainScene.ShieldInfo[i]));
			}
			MainScene.ShieldInfo = null;
		}
		if (MainScene.SoldierInfo != null) {
			game.soldiers = new List<Soldiers>();
			count = MainScene.SoldierInfo.Count;
			for (var i = 0; i < count; i++) {
				game.soldiers.Add (new Soldiers (MainScene.SoldierInfo[i]));
			}
//			Debug.Log ("Number of training soldiers of First team: "+game.soldiers[0].attributes["trainingSoldiers"]);
			MainScene.SoldierInfo = null;
		}
		if (MainScene.newUserId != 0) {
			game.login.id = MainScene.newUserId;
			MainScene.userId = MainScene.newUserId;
			MainScene.newUserId = 0;
		}
	}



	public void FbGetPicture(IGraphResult result){
		if (result.Error == null) {

//			MainScene.ProfilePictureSprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
			Sprite avatar = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
			MainCharButton.image.sprite = avatar;
		}else{
			Debug.Log(result.Error);
		}
	}

	void OnGeneralTrainComplete(){
//		GeneralTrainPrefab general = null;
		int point = 0;
		General g = new General ();
		Dictionary<int,string> trainDict = new Dictionary<int,string> ();
		trainDict.Add (40, "Courage");
		trainDict.Add (41, "Force");
		trainDict.Add (42, "Physical");
		if (game.trainings.Count > 40) {
			for (int i = 40 ; i < 43; i++){
				if (game.trainings[i].status == 1 && game.trainings[i].etaTimestamp < DateTime.Now){
					g = game.general.Find(x=>x.id == game.trainings[i].targetId);
					point = game.trainings[i].type;
					g.attributes[trainDict[i]].AsFloat = g.attributes[trainDict[i]].AsFloat + point;
					GeneralTrainPrefab.currentPrefab =null;
					g.UpdateObject();
					game.trainings[i].Completed();
					GeneralTrain.RunningItem[i-40] = 0;
//
//					general = GeneralTrainPrefab.GTrain.Find(x => x.general.id == game.trainings[i].targetId);
//					if(general != null){
//						general.gameObject.SetActive(true);
//					}
				}
			}
		}
	}

}

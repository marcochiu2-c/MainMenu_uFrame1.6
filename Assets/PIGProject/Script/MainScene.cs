using UnityEngine;
using UnityEngine.UI;
using System;
using OnePF;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
//using Endgame;

// TODO if anything updated wealth table, pls set MainScene.needReloadFromDB to true
using Facebook.Unity;
using Utilities;

public enum EquipmentEnum {Weapon,Armor,Shield};

public enum TrainingStatus {Cancelled = 0, OnGoing = 1, NotStarted = 2 ,Completed =3 , OnGoingCannotCancel =4 };

public class MainScene : MonoBehaviour {
	WsClient wsc;
	Text silverFeatherText;
	Text resourceText;
	Text starDustText;
	JSONClass json;
	Game game;
	GameObject ArtisanPanel;
	public static GameObject MainUIHolder;
	public static bool needReloadFromDB = true;
	public static int userId = 0;
	Wealth wealth;
	public Button MainCharButton;
	public static GameObject NoticeDialog;
	public static GameObject DisablePanel;
	public static string sValue ="";
	public static string rValue ="";
	public static string sdValue ="";
//	public static string noticeURL ="";
	public static string noticeText ="";
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
	public static JSONNode TeamInfo = null;
	public static Nullable<DateTime> GeneralLastUpdate = null;
	public static Nullable<DateTime> CounselorLastUpdate = null;
	public static Nullable<DateTime> StorageLastUpdate = null;
	public static Nullable<DateTime> WarfareLastUpdate = null;
	public static Nullable<DateTime> FriendLastUpdate = null;
	Shop shop;


	void Awake(){
		Application.targetFrameRate = 30;
		DontDestroyOnLoad(gameObject);
	}

	void Start(){
//		Debug.Log ("IQ: "+TechTree.GetIQRequirement(tech,level) );
		CallMainScene ();

//		StartCoroutine (TestTechItemData ());
	}

//	IEnumerator TestTechItemData(){
//		Dictionary<int,string> knowDict = SetDict.Knowledge ();
//		int count = TechTree.TechItems.Count;
//		int techCount = 0;
//		yield return new WaitForSeconds(3);
//		for (int i = 0; i < count; i++) {
//			print ("Name: "+TechTree.TechItems[i].Item);
//			techCount = TechTree.TechItems[i].TechRequirement.Count;
//			for (int j = 0 ; j < techCount; j ++){
//				print ("TechRequirement: "+TechTree.TechItems[TechTree.TechItems[i].TechRequirement[j]].Item);
//			}
//			print (" ");
//			techCount = TechTree.TechItems[i].KnowledgeRequirement.Count;
//			for (int j = 0 ; j < techCount; j ++){
//				print ("KnowledgeRequirement: "+knowDict[TechTree.TechItems[i].KnowledgeRequirement[j]]);
//			}
//			print (" ");
//		}
//
//	}
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

		DisablePanel = transform.Find ("DisablePanel").gameObject;
		NoticeDialog = transform.Find ("NoticeDialog").gameObject;
		AddButtonListener ();
		shop = GetComponent<Shop> ();
		Invoke ("CallShop", 3);

		ArtisanPanel = transform.parent.FindChild ("ArtisanHolder").gameObject;
//		wsc.Send("notice_url","GET",new JSONData (MainScene.userId));
		wsc.Send("notice_text","GET",new JSONData (MainScene.userId));
		InvokeRepeating("OnGeneralTrainComplete",1,4);
		InvokeRepeating("CheckObject",1,2);
		InvokeRepeating("QuitIfConnectionFailed",0,10);
		InvokeRepeating ("OnSchoolFieldSoldierTrainComplete", 2, 4);
		InvokeRepeating ("OnAcademyTrainingComplete", 3, 4);
		InvokeRepeating ("OnArtisanJobsComplete", 3, 4);
		InvokeRepeating ("OnTechTreeTrainingComplete", 2, 4);
	}

	void OnTechTreeTrainingComplete(){
		Game game = Game.Instance;
		Login lo = game.login;
		string techName = "";
		if (game.trainings[43].status == 1 && game.trainings[43].etaTimestamp < DateTime.Now){
			techName = Enum.GetName(typeof(Tech),game.trainings[43].type);
			lo.attributes.Add(techName,new JSONData(lo.attributes[techName].AsInt+1));
			lo.UpdateObject();
			game.trainings [43].status = 3;
			game.trainings [43].trainerId = 0;
			game.trainings [43].targetId = 0;
			game.trainings [43].type = 0;
			game.trainings [43].attributes ["TechTreeCounselors"] = new JSONArray();
			game.trainings [43].UpdateObject();
			if (TechTree.TotalIQText != null){
				for (int i = 0; i < 5; i++){
					TechTree.CounselorButtons[i].GetComponent<Image>().sprite = null;
					TechTree.CounselorButtons[i].transform.GetChild(0).GetComponent<Text>().text = "";
				}
				TechTree.TotalIQText.text = "";
				TechTree.TechnologyLabel.text = "";
				TechTree.FromLv.text = "";
				TechTree.ToLv.text = "";
				TechTree.RemainTrainingTime.text = "00:00:00";
			}
		}


	}

	void OnArtisanJobsComplete (){
		if (ArtisanPanel.activeSelf) {
			return;
		}
		Game game = Game.Instance;
		int id= 0;
		if (game.artisans [0].etaTimestamp <= DateTime.Now && (game.artisans[0].status == (int)TrainingStatus.OnGoing || game.artisans[0].status==4)) {
			if (game.artisans[0].targetId > 0){
				id = game.weapon.FindIndex (x => x.type == game.artisans[0].targetId);
				game.weapon[id].quantity += game.artisans[0].quantity;
				game.weapon[id].UpdateObject();
				game.artisans[0].status = (int)TrainingStatus.Completed;
				game.artisans[0].UpdateObject();
				WeaponMaking.Weapons.Find(x => x.id == game.weapon[id].type).UpdateRemainingTime();
			}
		}
		if (game.artisans [1].etaTimestamp <= DateTime.Now && (game.artisans[1].status == (int)TrainingStatus.OnGoing || game.artisans[1].status==4)) {
			if (game.artisans[1].targetId > 0){
				id = game.armor.FindIndex (x => x.type == game.artisans[1].targetId);
				game.armor[id].quantity += game.artisans[1].quantity;
				game.armor[id].UpdateObject();
				game.artisans[1].status = (int)TrainingStatus.Completed;
				game.artisans[1].UpdateObject();
				WeaponMaking.Armors.Find(x => x.id == game.armor[id].type).UpdateRemainingTime();
			}
		}
		if (game.artisans [2].etaTimestamp <= DateTime.Now && (game.artisans[2].status == (int)TrainingStatus.OnGoing || game.artisans[2].status==4)) {
			if (game.artisans[2].targetId > 0){
				id = game.shield.FindIndex (x => x.type == game.artisans[2].targetId);
				game.shield[id].quantity += game.artisans[2].quantity;
				game.shield[id].UpdateObject();
				game.artisans[2].status = (int)TrainingStatus.Completed;
				game.artisans[2].UpdateObject();
				WeaponMaking.Shields.Find(x => x.id == game.shield[id].type).UpdateRemainingTime();
			}
		}
	}

	void OnAcademyTrainingComplete(){
		Dictionary<int, string> kId = SetDict.KnowledgeID ();
		for (int i = 0; i < 40; i++) {
			if (game.trainings[i].status == (int)TrainingStatus.OnGoing && game.trainings[i].etaTimestamp < DateTime.Now){
				Counselor co = game.counselor.Find (x=>x.id == game.trainings [i].targetId);
				if (i < 5 || (i >19 && i < 25)){ 
					co.attributes["attributes"]["IQ"].AsFloat = co.attributes["attributes"]["IQ"].AsFloat + 1;
				}else if ((i > 4 && i < 10)|| (i > 24 && i < 30)){
					co.attributes["attributes"]["Leadership"].AsFloat =co.attributes["attributes"]["IQ"].AsFloat + 1;
				}else if ((i > 9 && i < 15)|| (i > 29 && i < 35)){
					co.attributes["attributes"]["KnownKnowledge"][kId[game.trainings[i].type]].AsInt = co.attributes["attributes"]["KnownKnowledge"][kId[game.trainings[i].type]].AsInt + 1;
//					switch (game.trainings [i].type){
//					case 2001:
//						co.attributes["attributes"]["KnownKnowledge"]["Woodworker"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Woodworker"].AsInt + 1;
//						break;
//					case 2002:
//						co.attributes["attributes"]["KnownKnowledge"]["MetalFabrication"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["MetalFabrication"].AsInt + 1;
//						break;
//					case 2003:
//						co.attributes["attributes"]["KnownKnowledge"]["EasternHistory"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["EasternHistory"].AsInt + 1;
//						break;
//					case 2004:
//						co.attributes["attributes"]["KnownKnowledge"]["WesternHistory"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["WesternHistory"].AsInt + 1;
//						break;
//					case 2005:
//						co.attributes["attributes"]["KnownKnowledge"]["ChainSteel"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["ChainSteel"].AsInt + 1;
//						break;					
//					case 2006:
//						co.attributes["attributes"]["KnownKnowledge"]["MetalProcessing"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["MetalProcessing"].AsInt + 1;
//						break;					
//					case 2007:
//						co.attributes["attributes"]["KnownKnowledge"]["Crafts"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Crafts"].AsInt + 1;
//						break;					
//					case 2008:
//						co.attributes["attributes"]["KnownKnowledge"]["Geometry"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Geometry"].AsInt + 1;
//						break;					
//					case 2009:
//						co.attributes["attributes"]["KnownKnowledge"]["Physics"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Physics"].AsInt + 1;
//						break;					
//					case 2010:
//						co.attributes["attributes"]["KnownKnowledge"]["Chemistry"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Chemistry"].AsInt + 1;
//						break;					
//					case 2011:
//						co.attributes["attributes"]["KnownKnowledge"]["PeriodicTable"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["PeriodicTable"].AsInt + 1;
//						break;					
//					case 2012:
//						co.attributes["attributes"]["KnownKnowledge"]["Pulley"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Pulley"].AsInt + 1;
//						break;					
//					case 2013:
//						co.attributes["attributes"]["KnownKnowledge"]["Anatomy"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Anatomy"].AsInt + 1;
//						break;					
//					case 2014:
//						co.attributes["attributes"]["KnownKnowledge"]["Catapult"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Catapult"].AsInt + 1;
//						break;					
//					case 2015:
//						co.attributes["attributes"]["KnownKnowledge"]["GunpowderModulation"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["GunpowderModulation"].AsInt + 1;
//						break;					
//					case 2016:
//						co.attributes["attributes"]["KnownKnowledge"]["Psychology"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Psychology"].AsInt + 1;
//						break;
//					case 2017:
//						co.attributes["attributes"]["KnownKnowledge"]["IChing"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["IChing"].AsInt + 1;
//						break;
//					}
				}else if ((i > 14 && i < 20)||(i > 34 && i < 40)){
					
				}
				co.UpdateObject();
//				game.trainings [i].status = 3;
//				game.trainings [i].trainerId = 0;
//				game.trainings [i].targetId = 0;
//				game.trainings [i].type = 0;
//				game.trainings [i].UpdateObject();
				game.trainings [i].Completed();
			}                                                                   
		}
	}
	
	void QuitIfConnectionFailed(){
		if (!wsc.conn.IsAlive) {
			ShowLog.Log ("Websocket connection failed.");
			Application.Quit ();
		}
	}


	void OnSchoolFieldSoldierTrainComplete(){
		int NumberOfTypeOfSoldiers = 8;
		//		ShowLog.Log (Game.Instance);
		game = Game.Instance;
		if (game.soldiers [0] == null) {
			reloadFromDB ();
			return;
		} else {
			for (int i = 0; i < NumberOfTypeOfSoldiers; i++) {
				if (game.soldiers [i].attributes ["ETATrainingTime"] != null) {
					if (Convert.ToDateTime (game.soldiers [i].attributes ["ETATrainingTime"]) < DateTime.Now) {
						SchoolField.CompletingTrainingSoldiers(i);
					}
				}
			}
		}
	}
	

	void AddButtonListener(){
		Panel.GetConfirmButton(NoticeDialog).onClick.AddListener(()=>{
			ShowLog.Log ("Close Notice Dialog");
			HidePanel(NoticeDialog);
		});
	}



	void CallShop(){
		shop.CallShop ();
	}

	/// <summary>
	/// Check if game variable is not assigned, will reload from db if so.
	/// </summary>
	void CheckObject(){
		if (game == null) {
			ShowLog.Log ("Game object null");
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
		Utilities.ShowLog.Log ("User ID: " + Game.Instance.login.id);
		Utilities.ShowLog.Log ("reloadFromDB(); ");
		if (MainScene.userId > 0) {
			wsc.Send ("wealth", "GET", new JSONData (MainScene.userId));
			wsc.Send ("counselors", "GET", new JSONData (MainScene.userId));
			wsc.Send ("generals", "GET", new JSONData (MainScene.userId));
			wsc.Send ("weapon", "GET", new JSONData (MainScene.userId));
			wsc.Send ("armor", "GET", new JSONData (MainScene.userId));
			wsc.Send ("shield", "GET", new JSONData (MainScene.userId));
			wsc.Send ("soldier", "GET", new JSONData (MainScene.userId));
			wsc.Send ("artisan", "GET", new JSONData (MainScene.userId));
			wsc.Send ("getCheckinInfo", "GET", new JSONData (MainScene.userId));
			wsc.Send ("training", "GET", new JSONData (MainScene.userId));
			wsc.Send ("team", "GET", new JSONData(MainScene.userId));
			MainScene.needReloadFromDB = false;
			Utilities.ShowLog.Log ("Game Wealth from reloadFromDB: " + game.wealth [0].toJSON ().ToString ());
		}
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

	/// <summary>
	///  Handler to assign JSON object that received from server
	/// </summary>
	void SetGameObjectFromServer(){
		var count = 0;
		if (MainScene.UserInfo != null) {
			game.login = new Login ((JSONClass)MainScene.UserInfo);TechTree.GetAvailableTechList ();
//			ShowLog.Log (game.login.ToString());
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
			ShowLog.Log (game.counselor[count-1].toJSON().ToString());
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
//			List<Knowledge> list = TechTree.GetAvailableKnowlegdeList ();
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
		if (MainScene.TeamInfo != null) {
			game.teams = new List<Teams> ();
			count = MainScene.TeamInfo.Count;
			for (var i = 0; i < count; i++) {
				game.teams.Add (new Teams (MainScene.TeamInfo[i]));
			}
			game.teams[2].UpdateObject();
			MainScene.TeamInfo = null;
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
			ShowLog.Log(result.Error);
		}
	}

	void OnGeneralTrainComplete(){
//		GeneralTrainPrefab general = null;
		game = Game.Instance;
		if (game == null) {
			reloadFromDB();
			return;
		}
		int point = 0;
		General g = new General ();
		Dictionary<int,string> trainDict = new Dictionary<int,string> ();
		trainDict.Add (40, "Courage");
		trainDict.Add (41, "Force");
		trainDict.Add (42, "Physical");
		if (game.trainings.Count > 40) {
			for (int i = 40 ; i < 43; i++){
				if (game.trainings[i].status == (int)TrainingStatus.OnGoing && game.trainings[i].etaTimestamp < DateTime.Now){
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

	void ShowPanel(GameObject panel){
		DisablePanel.SetActive (true);
		panel.SetActive (true);
	}
	
	void HidePanel(GameObject panel){
		DisablePanel.SetActive (false);
		panel.SetActive (false);
	}

}

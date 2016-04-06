using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SimpleJSON;
using UnityEngine;


[Serializable]
public class Game{
	public Login login { get; set; }
	public List<Wealth> wealth { get; set; }
	public List<Friend> friend { get; set; }
	public ChatRoom chatRoom { get; set; }
	public List<Counselor> counselor { get; set; } 
	public List<General> general { get; set; }
	public List<Trainings> trainings { get; set; }
	public List<Buildings> buildings { get; set; }
	public List<Warfare> warfare { get; set; }
	public List<Message> messages { get; set; }
	public List<PrivateMessage> privateMessages { get; set; }
	public List<Storage> storage { get; set; }
	public CheckInStatus checkinStatus { get; set; }
	public List<Weapon> weapon { get; set; }
	public List<Armor> armor { get; set; }
	public List<Shield> shield { get; set; }
	public List<Artisans> artisans { get; set; }
	public List<Soldiers> soldiers { get; set; }

	private static readonly Game s_Instance = new Game();
	
	public static Game Instance
	{
		get
		{
			return s_Instance;
		}
	}

	public Game(){
		login = new Login ();
		wealth = new List<Wealth> ();
		for (int i = 0; i < 3; i++) {
			wealth.Add (new Wealth ());
		}

		friend = new List<Friend> ();
		chatRoom = new ChatRoom ();
		general = new List<General> ();
		counselor = new List<Counselor> ();
		trainings = new List<Trainings> ();
		buildings = new List<Buildings> ();
		warfare = new List<Warfare> ();
		messages = new List<Message> ();
		privateMessages = new List<PrivateMessage> ();
		storage = new List<Storage> ();
		checkinStatus = new CheckInStatus ();
		artisans = new List<Artisans> ();
		//_additionalData = new Dictionary<string, Newtonsoft.Json.Linq.JToken>();
	}

	public void parseJSON(string json){
		JSONNode j = JSON.Parse (json);
		// Set Login part
		login.id = j ["login"] ["id"].AsInt;
		login.lastLoginTime = DateTime.Parse (j ["login"] ["lastLoginTime"]);
		login.lastActionTime = DateTime.Parse (j ["login"] ["lastActionTime"]);
		login.name = j ["login"] ["name"];
		login.exp = j ["login"] ["exp"].AsInt ;
		login.warCredit = j ["login"] ["warCredit"].AsInt;
		login.country = j ["login"] ["country"];

		// Set Wealth part
		for (int i=0; i<3; i++) {
			wealth[i].id = j["wealth"][i]["id"].AsInt;
			wealth[i].type = j["wealth"][i]["type"].AsInt;
			wealth[i].value = j["wealth"][i]["value"].AsInt;
		}

		// Set Friend part
		int count = j["friend"].Count;
		for (int i=0; i<count; i++) {
			friend[i].id = j["friend"][i]["id"].AsInt;
			friend[i].friendId = j["friend"][i]["friendId"].AsInt;
			friend[i].name = j["friend"][i]["name"];
		}

		// Set ChatRoom part
		chatRoom.id = j["chatroom"]["id"].AsInt;
		chatRoom.adminId = j["chatroom"]["adminId"].AsInt;
		chatRoom.name = j["chatroom"]["name"];

		// Set Counselor part
		count = j["general"].Count;
		for (int i=0; i<count; i++) {
			counselor[i].type = j ["counselor"] ["type"].AsInt;
			counselor[i].attributes = (JSONClass)j ["counselor"] ["attributes"];
		}

		// Set Army(Generals, Soldiers, Weapons, Shields and Armors)
		count = j["general"].Count;
		for (int i=0; i<count; i++) {
			general[i].id = j["general"][i]["id"].AsInt;
			general[i].attributes = j["general"][i]["attributes"];
			general[i].type = j["general"][i]["type"].AsInt;
			general[i].soldiers.id = j["general"][i]["soldier"]["id"].AsInt;
			general[i].soldiers.type =  j["general"][i]["soldier"]["type"].AsInt;
			general[i].soldiers.quantity =  j["general"][i]["soldier"]["quantity"].AsInt;
		}

		count = j["trainings"].Count;
		for (int i=0; i<count; i++) {
			trainings[i].id = j["trainings"][i]["id"].AsInt;
			trainings[i].type = j["trainings"][i]["type"].AsInt;
			trainings[i].targetId = j["trainings"][i]["targetId"].AsInt;
			trainings[i].startTimestamp = DateTime.Parse (j["trainings"][i]["startTimestamp"]);
			trainings[i].etaTimestamp = DateTime.Parse (j["trainings"][i]["etaTimestamp"]);
			trainings[i].status = j["trainings"][i]["status"].AsInt;
		}

		count = j["buildings"].Count;
		for (int i=0; i<count; i++) {
			buildings[i].id = j["trainings"][i]["id"].AsInt;
			buildings[i].type = j["trainings"][i]["type"].AsInt;
			buildings[i].level = j["trainings"][i]["level"].AsInt;
		}

		count = j["warfare"].Count;
		var aCount = 0;
		var dCount = 0;
		for (int i=0; i<count; i++) {

			warfare[i].id = j["warfare"][i]["id"].AsInt;
			warfare[i].attackerId = j["warfare"][i]["attackerId"].AsInt;
			warfare[i].defenderId = j["warfare"][i]["defenderId"].AsInt;
			warfare[i].timestamp = DateTime.Parse (j["warfare"][i]["timestamp"]);
			aCount = j["warfare"][i]["wartime_general_json"].Count;

			warfare[i].attackerGeneralId.Add (j["warfare"][i]["general_id"].AsInt);
			warfare[i].attackerGeneralJson.Add (j["warfare"][i]["wartime_general_json"]);



			warfare[i].result = j["warfare"][i]["result"].AsInt;
		}

		count = j["messages"].Count;
		for (int i=0; i<count; i++) {
			messages[i].id = j["messages"][i]["id"].AsInt;
			messages[i].timestamp = DateTime.Parse (j["messages"][i]["timestamp"]);
			messages[i].senderId = j["messages"][i]["senderId"].AsInt;
			messages[i].message = j["messages"][i]["message"];
		}

		count = j["privateMessages"].Count;
		for (int i=0; i<count; i++) {
			privateMessages[i].id = j["privateMessages"][i]["id"].AsInt;
			privateMessages[i].timestamp = DateTime.Parse (j["privateMessages"][i]["timestamp"]);	
			privateMessages[i].senderId = j["privateMessages"][i]["senderId"].AsInt;
			privateMessages[i].message = j["privateMessages"][i]["message"];
			privateMessages[i].isTo = j["privateMessages"][i]["isTo"].AsBool;
		}

		count = j["storages"].Count;
		for (int i=0; i<count; i++) {
			storage[i].id = j["storage"][i]["prodId"].AsInt;
			storage[i].productId = j["storage"][i]["id"].AsInt;
			storage[i].type = j["storage"][i]["senderId"].AsInt;
			storage[i].quantity = j["storage"][i]["quantity"].AsInt;
		}
	}



	/*[JsonExtensionData]
	private IDictionary<string, Newtonsoft.Json.Linq.JToken> _additionalData;

	[OnDeserialized]
	private void OnDeserialized(StreamingContext context)
	{
		       // SAMAccountName is not deserialized to any property
		        // and so it is added to the extension data dictionary
		string attributes = (string)_additionalData["attributes"];

	}*/
}


[Serializable]
public class Login {
	public int id { get; set; }
	public DateTime registerTime{ get; set; }
	public DateTime lastLoginTime { get; set; }
	public DateTime lastActionTime { get; set; }
	public string name { get; set; }
	public int snsType { get; set; }
	public string snsURL { get; set; }
	public int exp { get; set; }
	public int warCredit { get; set; }
	public string country { get; set; }
	public string playerID { get; set; }
	public string deviceID { get; set; }
	public JSONClass attributes { get; set; }

	public Login(){
		id = 0;
	}
	//{"player_id":"3","device_id":"","sns_type":1,"sns_url":"10153232082329123","country":"HK","register_time":1437706124,"last_login_time":1437713377,"last_login_ip":3232235781,"total_login_time":622037,"status":1,"exp":1043,"war_credits":1124,"battle_started_time":"2016-02-02T02:53:00.000Z"}
	public Login (SimpleJSON.JSONClass j){
		id = j ["user_id"].AsInt;
		name = j ["name"];
		snsType = j ["sns_type"].AsInt;
		snsURL = j ["sns_url"];
		registerTime = WsClient.UnixTimestampToDateTime( (long)j ["register_time"].AsInt,8);
		lastLoginTime = WsClient.UnixTimestampToDateTime( (long)j ["last_login_time"].AsInt,8);
		exp = j ["exp"].AsInt;
		warCredit = j ["war_credits"].AsInt;
		country = j ["country"];
		playerID = j ["player_id"];
		deviceID = j ["device_id"];
		if (j ["attributes"].ToString ().Length != 0) {
			attributes = (JSONClass)j ["attributes"];
		} else {
			WsClient wsc = WsClient.Instance;
			wsc.Send ("getUserInformationByDeviceId","GET",new JSONData(SystemInfo.deviceUniqueIdentifier));
		};
	}
	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("name", new JSONData( name));
		j.Add ("snsType", new JSONData( snsType));
		j ["snsURL"] = snsURL;
		j ["lastLoginTime"] = lastLoginTime.ToString ();
		j ["lastActionTime"] = lastActionTime.ToString ();
		j.Add ("exp", new JSONData(exp));
		j.Add ("warCredit",new JSONData( warCredit));
		j ["country"] = country;
		j ["playerID"] = playerID;
		j ["deviceID"] = deviceID;
		j ["attributes"] = attributes;
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}
}

[Serializable]
public class Wealth {
	public int id { get; set; }
	public int type { get; set; }  // 1 - SilverFeather, 2 - Stardust , 3 - Resources
	public int value  { get; set; }

	public Wealth(){}

	public Wealth(int i, int t, int v){
		id = i;
		type = t;
		value = v;
	}

	public Wealth(SimpleJSON.JSONNode j){
		id = j["pk"].AsInt;
		type = j["currency_id"].AsInt;
		value = j["currency_value"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("pk", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("value", new JSONData (value));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}

	public void Add(int money){
		value += money;
		UpdateCurrency ();
	}

	public void Deduct(int money){
		value -= money;
		UpdateCurrency ();
	}

	public void Set (int money){
		value = money;
		UpdateCurrency ();
	}

	void UpdateCurrency(){
		switch (type) {
		case 1:
			MainScene.sValue = value.ToString();
			break;
		case 2:
			MainScene.sdValue = value.ToString();
			break;
		case 3:
			MainScene.rValue = value.ToString();
			break;
		}
		WsClient wsc = WsClient.Instance;
		JSONClass j = new JSONClass ();
		j.Add("pk",new JSONData(id));
		j.Add ("value", new JSONData (value));
		wsc.Send ("wealth", "SET", j);
	}
}

[Serializable]
public class Friend {
	public int id { get; set; }
	public int friendId { get; set; }
	public string name { get; set; }
	public int status { get; set; } // 0:unfriended, 1:ongoing, 2:requesting, 3:be requested

	public Friend(){
	}

	public Friend(SimpleJSON.JSONNode j){
		id = j ["pk"].AsInt;
		friendId = j ["friend_user_id"].AsInt;
		name = j ["name"];
		status = j ["status"].AsInt;
	}

	public JSONClass toJSON(){	
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("friendId", new JSONData (friendId));
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}

//		public void UpdateObject(){
//			WsClient wsc = WsClient.Instance;
//			wsc.Send ("counselors", "SET", toJSON());
//		}
}

[Serializable]
public class ChatRoom {
	public int id { get; set; }
	public int adminId { get; set; }
	public string name { get; set; }

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("adminId", new JSONData (adminId));
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}

//	public void UpdateObject(){
//		WsClient wsc = WsClient.Instance;
//		wsc.Send ("counselors", "SET", toJSON());
//	}
}

[Serializable]
public class Counselor {
	public int id { get; set; }
	public JSONClass attributes { get; set; }
	public int type { get; set; }
	public int status { get; set; }
	// Status:  0: deleted, 1: active, allow to be trained, not ready for battle
	
	public Counselor(){
	}

	public Counselor(int i, JSONClass attr, int t, int s){
		id = i;
		attributes = attr;
		type = t;
		status = s;
	}

	public Counselor (SimpleJSON.JSONNode j){
		id = j["id"].AsInt;
		attributes = (JSONClass)j ["attributes"];
		type = j["attributes"]["type"].AsInt;
		status = j ["status"].AsInt;
	}
	
	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData(id));
		j ["attributes"] = attributes;
		j.Add ("status", new JSONData (status));
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}

	/// <summary>
	/// Update the Object to database</summary>
	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("counselors", "SET", toJSON());
	}
}


[Serializable]
public class General {
	public int id { get; set; }
	public string attributes { get; set; }
	public int type { get; set; }
	public Soldiers soldiers { get; set; }
	public int status { get; set; }

	public General(){
		soldiers = new Soldiers ();
	}

	public General(int i, string attr, int t, int st, Soldiers s = null){
		id = i;
		attributes = attr;
		type = t;
		status = st;
		soldiers = s;
	}

	public General(SimpleJSON.JSONNode j){
		id = j["general_id"].AsInt;
		attributes = j ["general_json"];
		type = j["general_type"].AsInt;
		status = j ["status"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["attributes"] = attributes;
		j.Add ("type", new JSONData (type));
		j.Add ("status", new JSONData (status));
		j ["soldiers"] = soldiers.toJSON ();
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}

	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("generals", "SET", toJSON());
	}
}

[Serializable]
public class Soldiers{
	public int id { get; set; }
	public JSONClass attributes { get; set; }
	public int type { get; set; }
	public int quantity { get; set; }

	public Soldiers(){
	}

	public Soldiers(JSONNode j){
		id = j ["soldier_id"].AsInt;
		attributes = (JSONClass)j ["soldier_json"];
		type = j ["type"].AsInt;
		quantity = j ["quantity"].AsInt;
	}
	
	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["json"] = attributes;
		j.Add ("type", new JSONData (type));
		j.Add ("quantity", new JSONData (quantity));
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}

	public void SetQuantity (int q){
		quantity = q;
		UpdateQuantity ();
	}
	
	void UpdateQuantity(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("soldier", "SET", toJSON());
	}
}

[Serializable]
public class Weapon {
	public int id { get; set; }
	public int type { get; set; }
	public int level { get; set; }
	public int quantity { get; set; }

	public Weapon(){
	}

	public Weapon(int i, int t, int l, int q){
		id = i;
		type = t;
		level = l;
		quantity = q;
	}

	public Weapon(JSONNode j){
		id = j ["weapon_id"].AsInt;
		type = j ["weapon_type"].AsInt;
		level = j ["weapon_level"].AsInt;
		quantity = j ["quantity"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		j.Add ("quantity", new JSONData (quantity));
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}

	public void SetQuantity (int q){
		quantity = q;
		UpdateObject ();
	}

	/// <summary>
	/// Update the Object to database</summary>
	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("weapon", "SET", toJSON());
	}
}

[Serializable]
public class Armor {
	public int id { get; set; }
	public int type { get; set; }
	public int level { get; set; }
	public int quantity { get; set; }

	public Armor(){
	}

	public Armor(int i, int t, int l, int q){
		id = i;
		type = t;
		level = l;
		quantity = q;
	}

	public Armor(JSONNode j){
		id = j ["armor_id"].AsInt;
		type = j ["armor_type"].AsInt;
		level = j ["armor_level"].AsInt;
		quantity = j ["quantity"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		j.Add ("quantity", new JSONData (quantity));
		j.Add ("userId", new JSONData(game.login.id));
		return j;
	}
	
	public void SetQuantity (int q){
		quantity = q;
		UpdateObject ();
	}
	
	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("armor", "SET", toJSON ());
	}
}

[Serializable]
public class Shield {
	public int id { get; set; }
	public int type { get; set; }
	public int level { get; set; }
	public int quantity { get; set; }

	public Shield(){
	}

	public Shield(int i, int t, int l, int q){
		id = i;
		type = t;
		level = l;
		quantity = q;
	}

	public Shield(JSONNode j){
		id = j ["shield_id"].AsInt;
		type = j ["shield_type"].AsInt;
		level = j ["shield_level"].AsInt;
		quantity = j ["quantity"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		j.Add ("quantity", new JSONData (quantity));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}
	
	public void SetQuantity (int q){
		quantity = q;
		UpdateObject ();
	}
	
	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("shield", "SET", toJSON());
	}
}

[Serializable]
public class Trainings {
	public int id { get; set; }
	public int type { get; set; }
	public int trainerId { get; set; }
	public int targetId { get; set; }
	public DateTime startTimestamp { get; set; }
	public DateTime etaTimestamp { get; set; }
	public int status { get; set; }

	public Trainings(int i, int t, int tr, int tid, DateTime sts, DateTime ets, int s){
		id = i;
		type = t;
		trainerId = tr;
		targetId = tid;
		startTimestamp = sts;
		etaTimestamp = ets;
		status = s;
	}

	public Trainings(JSONNode j){
		id = j ["id"].AsInt;
		type = j ["type"].AsInt;
		trainerId = j ["trainerId"].AsInt;
		targetId = j ["targetId"].AsInt;
		startTimestamp = Convert.ToDateTime(j ["startTimestamp"]);
		etaTimestamp = Convert.ToDateTime(j ["etaTimestamp"]);
		status = j ["status"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("trainerId", new JSONData (trainerId));
		j.Add ("targetId", new JSONData (targetId));
		j ["startTimestamp"] = startTimestamp.ToString();
		j ["etaTimestamp"] = etaTimestamp.ToString();
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}
}

[Serializable]
public class Buildings {
	public int id { get; set; }
	public int type { get; set; }
	public int level{ get; set; }

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}

	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("buildings", "SET", toJSON());
	}
}

[Serializable]
public class Warfare {
	public int id { get; set; }
	public int attackerId { get; set; }
	public int defenderId { get; set; }
	public DateTime timestamp { get; set; }
	public List<int> attackerGeneralId;
	public List<string> attackerGeneralJson;
	public int result { get; set; }  // Positive: Attacker Win, Negative: Defender Win

	public Warfare(JSONNode j){
		id = j ["id"].AsInt;
		attackerId = j ["attacker_id"].AsInt;
		defenderId = j ["defender_id"].AsInt;
		timestamp = Convert.ToDateTime(j ["timestamp"]);
		attackerGeneralId.Add (j ["general_id"].AsInt);
		attackerGeneralJson.Add (j ["wartime_general_json"]);
		result = j ["result"].AsInt;
	}

	public void AddGeneral(int id, string json){
		attackerGeneralId.Add (id);
		attackerGeneralJson.Add (json);
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		var aCount = attackerGeneralId.Count;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("attackerId", new JSONData (attackerId));
		j ["timestamp"] = timestamp.ToString();
		j.Add ("attackerGeneral", new JSONArray ());
		for (var i = 0; i < aCount; i++) {
			j["attackerGeneral"].Add ( new JSONData (attackerGeneralId[i]));
		}
		j.Add ("result", new JSONData (result));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}
}

[Serializable]
public class Message {
	public int id { get; set; }
	public DateTime timestamp { get; set; }
	public int senderId { get; set; }
	public string message { get; set; }

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["timestamp"] = timestamp.ToString();
		j.Add ("senderId", new JSONData (senderId));
		j ["message"] = message;
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}
}

[Serializable]
public class PrivateMessage : Message{
	public bool isTo { get; set; }

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["timestamp"] = timestamp.ToString();
		j.Add ("senderId", new JSONData (senderId));
		j ["message"] = message;
		j.Add ("isTo", new JSONData (isTo));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}
}


[Serializable]
public class Storage{
	public int id { get; set; }
	public int productId { get; set; }
	public int type { get; set; }
	public int quantity { get; set; }

	public Storage(){
	}

	public Storage(int i, int pi, int t, int q){
		id = i;
		productId = pi;
		type = t;
		quantity = q;
	}

	public Storage (SimpleJSON.JSONNode j){
		id = j["id"].AsInt;
		productId = j ["prod_id"].AsInt;
		type = j["prod_type"].AsInt;
		quantity = j["quantity"].AsInt;
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("prodId", new JSONData (productId));
		j.Add ("type", new JSONData (type));
		j.Add ("quantity", new JSONData (quantity));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}

	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("storage", "SET", toJSON());
	}
}

[Serializable]
public class CheckInStatus{
	public int id { get; set; }
	public int year { get; set; }
	public int month { get; set; }
	public List<int> days{ get; set; }

	public CheckInStatus(){
		
	}

	public CheckInStatus(int i, int y, int m, List<int> d){
		id = i;
		year = y;
		month = m;
		days = d;
	}

	public CheckInStatus (SimpleJSON.JSONClass j){
		
		days = new List<int> ();
		id = j ["checkin_id"].AsInt;
		JSONClass date = (JSONClass)j ["checkin_json"];

		if (date ["year"].AsInt == DateTime.Now.Year && date ["month"].AsInt == DateTime.Now.Month) {
			year = date ["year"].AsInt;
			month = date ["month"].AsInt;
			int count = date ["days"].Count;
			for (int i = 0; i < count; i++) {
				days.Add(date ["days"] [i].AsInt);
			}
		} else {
			year = DateTime.Now.Year;
			month = DateTime.Now.Month;
			days = new List<int> ();
		}
	}

	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("year", new JSONData (year));
		j.Add ("month", new JSONData (month));
		j.Add ("days", new JSONArray());
		int count = days.Count;
		for (int i = 0; i < count; i++) {
			j["days"].Add(new JSONData(days[i]));
		}
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}

	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("checkin", "SET", toJSON());
	}
}

[Serializable]
public class Artisans {
	public int id { get; set; }
	public int targetId { get; set; }
	public int resources { get; set; }
	public int metalsmith { get; set; }
	public int quantity { get; set; }
	public DateTime startTimestamp { get; set; }
	public DateTime etaTimestamp { get; set; }
	public string details { get; set; }
	public int status { get; set; } // 0:cancelled, 1:ongoing, 2:not started ,3:completed, 4: ongoing, not allow to cancel
	
	public Artisans(){
	}
	
	public Artisans(JSONNode j){
		id = j ["artisan_id"].AsInt;
		targetId = j ["target_id"].AsInt;
		resources = j ["resources"].AsInt;
		metalsmith = j ["metalsmith"].AsInt;
		quantity = j ["quantity"].AsInt;
		startTimestamp = Convert.ToDateTime(j ["start_time"]);
		etaTimestamp = Convert.ToDateTime(j ["eta_time"]);
		details = j ["details"];
		status = j ["status"].AsInt;
	}
	
	public JSONClass toJSON(){
		Game game = Game.Instance;
		JSONClass j = new JSONClass ();
		j.Add ("artisanId", new JSONData (id));
		j.Add ("targetId", new JSONData (targetId));
		j.Add ("resources",new JSONData( resources));
		j.Add ("metalsmith", new JSONData (metalsmith));
		j.Add ("quantity", new JSONData (quantity));
		j ["start_time"] = new JSONData (WsClient.JSDate(startTimestamp));
		j ["eta_time"] = new JSONData (WsClient.JSDate(etaTimestamp));
		j.Add ("status", new JSONData (status));
		j.Add ("userId", new JSONData (game.login.id));
		return j;
	}

	public void UpdateObject(){
		WsClient wsc = WsClient.Instance;
		wsc.Send ("artisans", "SET", toJSON());
	}
}


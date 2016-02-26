using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SimpleJSON;


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
			counselor[i].attributes = j ["counselor"] ["attributes"];
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
			general[i].soldiers.weapons.id =  j["general"][i]["soldier"]["weapons"]["type"].AsInt;
			general[i].soldiers.weapons.type =  j["general"][i]["soldier"]["weapons"]["type"].AsInt;
			general[i].soldiers.weapons.level =  j["general"][i]["soldier"]["weapons"]["level"].AsInt;
			general[i].soldiers.armors.id =  j["general"][i]["soldier"]["armors"]["type"].AsInt;
			general[i].soldiers.armors.type =  j["general"][i]["soldier"]["armors"]["type"].AsInt;
			general[i].soldiers.armors.level =  j["general"][i]["soldier"]["armors"]["level"].AsInt;
			general[i].soldiers.shields.id =  j["general"][i]["soldier"]["shields"]["type"].AsInt;
			general[i].soldiers.shields.type =  j["general"][i]["soldier"]["shields"]["type"].AsInt;
			general[i].soldiers.shields.level =  j["general"][i]["soldier"]["shields"]["level"].AsInt;
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
	public DateTime lastLoginTime { get; set; }
	public DateTime lastActionTime { get; set; }
	public string name { get; set; }
	public int snsType { get; set; }
	public string snsURL { get; set; }
	public int exp { get; set; }
	public int warCredit { get; set; }
	public string country { get; set; }
	public string deviceID { get; set; }

	public Login(){
	}
	//{"player_id":"3","device_id":"","sns_type":1,"sns_url":"10153232082329123","country":"HK","register_time":1437706124,"last_login_time":1437713377,"last_login_ip":3232235781,"total_login_time":622037,"status":1,"exp":1043,"war_credits":1124,"battle_started_time":"2016-02-02T02:53:00.000Z"}
	public Login (SimpleJSON.JSONClass j){
		id = j ["user_id"].AsInt;
		name = j ["name"];
		snsType = j ["sns_type"].AsInt;
		snsURL = j ["sns_url"];
		exp = j ["exp"].AsInt;
		warCredit = j ["war_credit"].AsInt;
		country = j ["country"];
		deviceID = j ["device_id"];
	}
	public JSONClass toJSON(){
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
		j ["deviceID"] = deviceID;
		return j;
	}
}

[Serializable]
public class Wealth {
	public int id { get; set; }
	public int type { get; set; }  // 1 - SilverFeather, 2 - Stardust , 3 - Resources
	public int value  { get; set; }

	public Wealth(){
	}

	public Wealth(SimpleJSON.JSONClass j){
		id = j["pk"].AsInt;
		type = j["currency_id"].AsInt;
		value = j["currency_value"].AsInt;
	}

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("value", new JSONData (value));
		return j;
	}
}

[Serializable]
public class Friend {
	public int id { get; set; }
	public int friendId { get; set; }
	public string name { get; set; }
	public int status { get; set; } // 0:unfriended, 1:ongoing, 2:requesting, 3:be requested

	public JSONClass toJSON(){	
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("friendId", new JSONData (friendId));
		return j;
	}
}

[Serializable]
public class ChatRoom {
	public int id { get; set; }
	public int adminId { get; set; }
	public string name { get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("adminId", new JSONData (adminId));
		return j;
	}
}

[Serializable]
public class Counselor {
	public int id { get; set; }
	public string attributes { get; set; }
	public int type { get; set; }
	
	public Counselor(){
	}

	public Counselor (SimpleJSON.JSONNode j){
		id = j["id"].AsInt;
		attributes = j ["attributes"];
		type = j["attributes"]["type"].AsInt;
	}
	
	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData(id));
		j ["attributes"] = attributes;
		j.Add ("type", new JSONData (type));
		return j;
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
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["attributes"] = attributes;
		j.Add ("type", new JSONData (type));
		j.Add ("status", new JSONData (status));
		j ["soldiers"] = soldiers.toJSON ();
		return j;
	}
}

[Serializable]
public class Soldiers{
	public int id { get; set; }
	public string attributes { get; set; }
	public int type { get; set; }
	public int quantity { get; set; }
	public Weapon weapons { get; set; }
	public Armor armors { get; set; }
	public Shield shields { get; set; }

	public Soldiers(){
		weapons = new Weapon ();
		armors = new Armor ();
		shields = new Shield ();
	}

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["attributes"] = attributes;
		j.Add ("type", new JSONData (type));
		j.Add ("quantity", new JSONData (quantity));
		j ["weapons"] = weapons.toJSON ();
		j ["armors"] = armors.toJSON ();
		j ["shields"] = shields.toJSON ();
		return j;
	}
}

[Serializable]
public class Weapon {
	public int id { get; set; }
	public int type { get; set; }
	public int level { get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		return j;
	}
}

[Serializable]
public class Armor {
	public int id { get; set; }
	public int type { get; set; }
	public int level { get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		return j;
	}
}

[Serializable]
public class Shield {
	public int id { get; set; }
	public int type { get; set; }
	public int level { get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		return j;
	}
}

[Serializable]
public class Trainings {
	public int id { get; set; }
	public int type { get; set; }
	public int targetId { get; set; }
	public DateTime startTimestamp { get; set; }
	public DateTime etaTimestamp { get; set; }
	public int status { get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("targetId", new JSONData (targetId));
		j ["startTimestamp"] = startTimestamp.ToString();
		j ["etaTimestamp"] = etaTimestamp.ToString();
		return j;
	}
}

[Serializable]
public class Buildings {
	public int id { get; set; }
	public int type { get; set; }
	public int level{ get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("type", new JSONData (type));
		j.Add ("level", new JSONData (level));
		return j;
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
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["timestamp"] = timestamp.ToString();
		j.Add ("senderId", new JSONData (senderId));
		j ["message"] = message;
		return j;
	}
}

[Serializable]
public class PrivateMessage : Message{
	public bool isTo { get; set; }

	public JSONClass toJSON(){
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j ["timestamp"] = timestamp.ToString();
		j.Add ("senderId", new JSONData (senderId));
		j ["message"] = message;
		j.Add ("isTo", new JSONData (isTo));
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
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("prodId", new JSONData (productId));
		j.Add ("type", new JSONData (type));
		j.Add ("quantity", new JSONData (quantity));
		return j;
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
		JSONClass j = new JSONClass ();
		j.Add ("id", new JSONData (id));
		j.Add ("year", new JSONData (year));
		j.Add ("month", new JSONData (month));
		j.Add ("days", new JSONArray());
		int count = days.Count;
		for (int i = 0; i < count; i++) {
			j["days"].Add(new JSONData(days[i]));
		}
		return j;
	}
}
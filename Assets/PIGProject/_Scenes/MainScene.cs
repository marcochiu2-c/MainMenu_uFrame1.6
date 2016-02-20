using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class MainScene : MonoBehaviour {
	WsClient wsc;
	Text silverFeatherText;
	Text resourceText;
	Text starDustText;
	JSONClass json;
	Game game;
	List<Wealth> wealthList;
	Wealth wealth;
	string sValue ="";
	string rValue ="";
	string sdValue ="";

	// Use this for initialization
	void Start () {
		silverFeatherText  = GameObject.Find ("/Canvas/MainUIHolder/HeaderHolder/SilverFeatherPanel/SilverFeatherText").GetComponents<Text>() [0];
		resourceText = GameObject.Find ("/Canvas/MainUIHolder/HeaderHolder/ResourcesPanel/ResourcesText").GetComponents<Text> () [0];
		starDustText = GameObject.Find ("/Canvas/MainUIHolder/HeaderHolder/StardustPanel/StardustText").GetComponents<Text> () [0];
		game = new Game ();
		wsc = WsClient.Instance;
		int userId = 3;
		json = new JSONClass ();
		json.Add("data",new JSONData(userId));
		json["action"]="GET";
		json["table"]="wealth";

		wsc.Send(json.ToString());
		
		wsc.conn.OnMessage += (sender, e) => {
			wealthList = new List<Wealth> ();
			SimpleJSON.JSONArray wealths = (SimpleJSON.JSONArray) SimpleJSON.JSON.Parse (e.Data);
			IEnumerator w = wealths.GetEnumerator ();
			while (w.MoveNext ()) {
				wealthList.Add (new Wealth((SimpleJSON.JSONClass) w.Current));
			}
			sValue = wealthList[0].value.ToString ();
			rValue = wealthList[1].value.ToString ();
			sdValue = wealthList[2].value.ToString ();
		};
	}
	
	// Update is called once per frame
	void Update () {
		silverFeatherText.text = sValue;
		resourceText.text = rValue;
		starDustText.text = sdValue;
	}
}

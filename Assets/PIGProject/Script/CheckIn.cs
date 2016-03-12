using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using WebSocketSharp;



public class CheckIn : MonoBehaviour
{
	JSONClass checkin = new JSONClass();
	CheckInContent chkinContent = CheckInContent.Instance;
	WsClient wsc;
	Game game;
	Button[] buttons = new Button[30];
	public static int checkedInDate=0;
	// Use this for initialization

	void Start(){  // TODO: Change to uframe start event
		CallCheckIn ();
	}


	private void CallCheckIn ()
	{
		
		AddButtonListener ();

		wsc = WsClient.Instance;
		game = Game.Instance;
		DateTime now = DateTime.Now;
		checkin.Add ("month", new JSONData (now.Month));
		checkin.Add ("days", new JSONArray ());

		JSONNode json = new JSONClass ();

		if (wsc.conn.IsAlive) {

			json ["data"] = "1";

			json ["action"] = "GET";
			json ["table"] = "users";
			wsc.conn.Send (json.ToString ());
			json ["data"] = "";

			json ["action"] = "GET";
			json ["table"] = "getCheckinGiftInfo";
//			wsc.conn.Send (json.ToString ());

			json ["data"] = "1";

			json ["action"] = "GET";
			json ["table"] = "getCheckinInfo";
			wsc.conn.Send (json.ToString ()); 
		} else {
			Debug.Log ("Websocket Connection Lost!");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (CheckIn.checkedInDate > 0) {
			for (var i = 0; i < CheckIn.checkedInDate; i++) {
				DisableDayButton(i);
			}
			CheckIn.checkedInDate = 0;
		}
	}

	public static void DisableDayButton(int i){
		GameObject.Find ("Day" + (i + 1)).GetComponent<Button> ().interactable = false;
	}

	/*public void DoCheckIn(GameObject btn){
		int day = Int32.Parse(btn.name.Substring(3));
		DoCheckIn (day);
	}*/

	public void DoCheckIn(Button btn){
		//Debug.Log (String.Format("{0} Clicked.",btn));
		int day = Int32.Parse(btn.name.Substring(3));
		DoCheckIn (day);

	}

	public void DoCheckIn(int d){
		// d is the day number for human reading while day is for CPU read
		int day = d - 1; // have the day start from 0

//		Debug.Log(game.checkinStatus.days[0]);

//		if (day == CheckIn.checkedInDate ) { // for the next available check in.
		Debug.Log(String.Format("Day {0} checking in.",chkinContent.giftNumber[day]));
		if (!game.checkinStatus.days.Contains (DateTime.Now.Day)) {
			Debug.Log ("Checking in as " + DateTime.Now.Day);
			game.checkinStatus.days.Add (DateTime.Now.Day);
			JSONNode json = new JSONClass ();
			json ["data"] = game.checkinStatus.toJSON ();
			Debug.Log (json["data"]);
			json ["data"].Add ("userId", new JSONData (game.login.id));
			json ["table"] = "checkin";
			json ["action"] = "SET";
			Debug.Log (json.ToString ());
			wsc.Send (json.ToString ());
			DisableDayButton(game.checkinStatus.days.Count-1);
		}
//		} else {
//			// TODO: show unavailable check in on screen
//		}
	}

	void AddButtonListener(){
		for (var i = 0; i < 30; i++) {
			buttons[i] = GameObject.Find("Day"+(i+1)).GetComponent<Button>();
			Transform child = buttons [i].transform;
			buttons[i].onClick.AddListener(() => { DoCheckIn(child.GetComponent<Button>()); });
		}
	}

}



public class CheckInContent{
	private static readonly CheckInContent s_Instance = new CheckInContent();

	public static CheckInContent Instance
	{
		get
		{
			return s_Instance;
		}
	}

	public int[] giftNumber = new int[30];
}
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
	enum CheckinWealthEnum {SilverFeather =0 ,Stardust =1, Resources =2};
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

//		wsc.conn.Send ("getCheckinGiftInfo","GET",json.ToString ());

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
//			json ["data"] = game.checkinStatus.toJSON ();
			Debug.Log (json["data"]);
//			json ["table"] = "checkin";
//			json ["action"] = "SET";
//			Debug.Log (json.ToString ());
//			wsc.Send (json.ToString ());
			game.checkinStatus.UpdateObject();
			Rewards();
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

		transform.GetChild(2).GetChild(2).GetComponent<Button>().onClick.AddListener(() => { 
			HideSignedPopup();
		});
	}

	void Rewards(){
		int day = game.checkinStatus.days.Count;
		int wealthType = 0;  // 0 - SilverFeather, 1 - Stardust , 2 - Resources   (1 less than definition in DB)
		int[] sf = new int[]{0,13, 27, 40, 53, 67, 80};
		int[] sd = new int[]{0, 3, 5, 8, 11, 16, 21};
		int[] res = new int[]{0, 5333, 10667, 16000, 21333, 32000, 42667};
		string[] wt = new String[]{"銀羽","時之星塵","資源"};
		int quantity = 0;
		switch (day) {
		case 1: case 8: case 15:  // q: 1
			wealthType = (int) CheckinWealthEnum.SilverFeather;
			quantity = sf[1];
			break;
		case 2: case 9: case 16:  // q: 2
			wealthType = (int) CheckinWealthEnum.SilverFeather;
			quantity = sf[2];
			break;
		case 3: case 10: case 17:  // q: 1
			wealthType = (int) CheckinWealthEnum.Resources;
			quantity = res[1];
			break;
		case 4: case 11: case 18:  // q: 2
			wealthType = (int) CheckinWealthEnum.Resources;
			quantity = res[2];
			break;
		case 5: case 12: case 19:  // q: 3
			wealthType = (int) CheckinWealthEnum.Resources;
			quantity = res[3];
			break;
		case 6: case 13: case 20:  // q: 1
			wealthType = (int) CheckinWealthEnum.Stardust;
			quantity = sd[1];
			break;
		case 7: case 14: case 21:  // q: 2
			wealthType = (int) CheckinWealthEnum.Stardust;
			quantity = sd[2];
			break;
		case 22:
			wealthType = (int) CheckinWealthEnum.SilverFeather;  // q: 3
			quantity = sf[3];
			break;
		case 23:
			wealthType = (int) CheckinWealthEnum.SilverFeather;  // q: 4 
			quantity = sf[4];
			break;
		case 24:
			wealthType = (int) CheckinWealthEnum.Resources;  // q: 4
			quantity = res[4];
			break;
		case 25:
			wealthType = (int) CheckinWealthEnum.Resources;  // q: 5
			quantity = res[5];
			break;
		case 26:
			wealthType = (int) CheckinWealthEnum.Resources;  // q: 6
			quantity = res[6];
			break;
		case 27:
			wealthType = (int) CheckinWealthEnum.Stardust;  // q: 3
			quantity = sd[3];
			break;
		case 28:
			wealthType = (int) CheckinWealthEnum.Stardust;  // q: 4
			quantity = sd[4];
			break;
		case 29:
			wealthType = (int) CheckinWealthEnum.Resources;  // q: 6
			quantity = res[6];
			break;
		case 30:
			wealthType = (int) CheckinWealthEnum.Stardust;  // q: 6
			quantity = sd[6];
			break;
		}
		ShowSignedPopup (quantity+" "+wt[wealthType]);
		game.wealth [wealthType].Add (quantity);
	}

	void ShowSignedPopup(string rewards){
		GameObject panel = transform.GetChild (2).gameObject;
		panel.SetActive (true);
		panel.transform.GetChild (1).GetComponent<Text> ().text = rewards;
	}

	void HideSignedPopup(){
		transform.GetChild (2).gameObject.SetActive (false);
//		gameObject.SetActive (false);
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
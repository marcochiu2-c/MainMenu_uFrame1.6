using UnityEngine;
using System.Collections;
using SimpleJSON;
using WebSocketSharp;

public class LoginScreen : MonoBehaviour {
	private Game game;
	private WsClient wsc;
	private string deviceId="";
	public static JSONClass user=null;

	// Use this for initialization
	void Start () {
		CallLoginScreen ();

	}

	private void CallLoginScreen(){
		game = Game.Instance;
		wsc = WsClient.Instance;
		deviceId = SystemInfo.deviceUniqueIdentifier;
		// TODO send device ID to server for checking user existance
		JSONNode json = new JSONClass ();
		json ["data"] = deviceId;
		json ["action"] = "GET";
		json ["table"] = "getUserInformationByDeviceId";
		wsc.conn.Send (json.ToString ());
	}
	
	// Update is called once per frame
	void Update () {

		if (user!=null){
			if (user.ToString ()=="{\"success\":false}") {
				Debug.Log ("user not existed");
				// TODO if user Choose not login with SNS, setup account   SetupStandaloneAccount();
				
					// TODO else check if SNS account exist in DB, login
				
				// TODO else setup account with SNS account  SetupSocialNetworkAccount();
			}else{
				game.login = new Login(user);
				// TODO ask if user want to link up account with SNS account
			}
			user =null;
		}


	}

	private void SetupStandaloneAccount(){
		JSONClass json = new JSONClass ();
		JSONClass j = new JSONClass ();
		j.Add ("snsType", new JSONData(0));
		j.Add ("snsURL", new JSONData(" "));
		j.Add ("deviceId", SystemInfo.deviceUniqueIdentifier);
		//j.Add ("name", )// TODO grab name from UI
		json.Add ("action", new JSONData("NEW"));
		json.Add ("table", new JSONData("users"));
		json.Add ("data", j);
		wsc.conn.Send (json.ToString ());
	}

	private void SetupSocialNetworkAccount(){
		JSONClass json = new JSONClass ();
		JSONClass j = new JSONClass ();
		// TODO if facebook j.Add ("snsType", new JSONData(1));
		// TODO if Google+ j.Add ("snsType", new JSONData(2));
		// TODO j.Add ("snsURL", ); // TODO grab URL from UI
		j.Add ("deviceId", SystemInfo.deviceUniqueIdentifier);
		//j.Add ("name", )// TODO grab name from UI
		json.Add ("action", new JSONData("NEW"));
		json.Add ("table", new JSONData("users"));
		json.Add ("data", j);
		wsc.conn.Send (json.ToString ());
	}
}

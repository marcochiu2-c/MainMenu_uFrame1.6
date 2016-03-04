using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using WebSocketSharp;
//using Soomla;
//using Soomla.Profile;
using Facebook.Unity;

public class LoginScreen : MonoBehaviour {
	private Game game;
	private WsClient wsc;
	private string deviceId="";
	private string snsURL="";
	public static JSONClass user=null;
	public static JSONClass userWithSns=null;
	public InputField NewUserStandAloneName;
	public InputField NewUserSNSAccountName;
	public Slider slider;
	public GameObject loadImage;
	public GameObject newUserPanel;
	public GameObject newFBUserPanel;
	private bool newFBUserPanelActivated=false;
//	public bool sentDBSNSCheckRequest = false;
	bool userCancelledLogin = false;

	string debugText = "";
	public static bool isSNSInit = false;
	public static bool isSNSLoggedIn = false;


	// Use this for initialization
	void Awake() {
		CallLoginScreen ();
		
	}
	
	private void CallLoginScreen(){
//		ProfileEvents.OnSoomlaProfileInitialized += () => {
//			debugText += "SoomlaProfile Initialized !"+"\n";
//			Soomla.SoomlaUtils.LogDebug("LoginScreen", "SoomlaProfile Initialized !");
//			LoginScreen.isSoomlaInit = true;
//			SoomlaProfile.Login(
//				Provider.FACEBOOK,                        // Social Provider
//				null
//				);
//		};
//		
//		ProfileEvents.OnLoginFinished += (UserProfile UserProfile, bool autoLogin, string payload) => {
//			//SoomlaProfile.MultiShare("Ha Ha Ha",
//			//                         "~/Documents/device-2015-07-17-104246.png");
//			Soomla.SoomlaUtils.LogDebug("LogInFinish","User: "+UserProfile.ProfileId);
//			snsURL = UserProfile.ProfileId;
//			LoginScreen.isSoomlaLoggedIn = true;
//			//			SoomlaProfile.GetContacts(Provider.FACEBOOK);
//			//var purchase = StoreInfo.GetPurchasableItemWithProductId("stardust_10")
//			//      .PurchaseType as PurchaseWithMarket;
//			//Debug.Log("Price of Star dust 10 pack "+ purchase.MarketItem.MarketPriceAndCurrency);
//			
//		};
//		SoomlaProfile.Initialize ();

		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}

		game = Game.Instance;
		wsc = WsClient.Instance;
		deviceId = SystemInfo.deviceUniqueIdentifier;

		// TODO send device ID to server for checking user existance
		JSONNode json = new JSONClass ();
		json ["data"] = deviceId;
		json ["action"] = "GET";
		json ["table"] = "getUserInformationByDeviceId";
		wsc.conn.Send (json.ToString ());


//		Debug.Log ("NewFBUserPanel Active: " + newFBUserPanel.activeSelf);
	}
	
	// Update is called once per frame
	void Update () {

		if (user!=null){
			Debug.Log (user);
			if (user.ToString ()=="{\"success\":false}") {
				Debug.Log ("user not existed");
				
				// TODO else setup account with SNS account  SetupSocialNetworkAccount();
			}else{ // user found in DB
				game.login = new Login(user);
				if (game.login.snsType==1){  // if account already login with SNS
					LoginSNS();
					GotoMainUI();
				}
				//                if (game.login.snsType!=0){  // Already registered with SNS


//                }else{ // not yet registered with SNS
				//                    TODO show Facebook register reminder first
				//                    GotoMainUI();
//                }
			}
			user =null;
		}
		if (userWithSns != null)
			Debug.Log (snsURL);
		if (newFBUserPanelActivated) {  // For device not link with SNS account and user choose to link with SNS

			if(game.login.id!=0){
				if(userWithSns!=null && snsURL!=""){ 
					Debug.Log (userWithSns.ToString ());
					if (userWithSns.ToString() == "{\"success\":false}") {
						Debug.Log ("No SNS account found in DB");
						SetupSocialNetworkAccount();
						GotoMainUI ();
					} else {
						Debug.Log ("SNS account found.");
						GotoMainUI ();
					}
					userWithSns = null;
				}
			}

			
		}


	}

	public void SetupStandaloneAccount(){
		JSONClass json = new JSONClass ();
		JSONClass j = new JSONClass ();
		j.Add ("snsType", new JSONData(0));
		j.Add ("snsURL", new JSONData(" "));
		j.Add ("deviceId", SystemInfo.deviceUniqueIdentifier);
		j.Add ("name", NewUserStandAloneName.text);// TODO grab name from UI
		SubmitUserRegisterData (j);
	}
	
	public void SetupSocialNetworkAccount(){
		if (game.login.id==0) { // Link SNS with new user
			JSONClass json = new JSONClass ();
			JSONClass j = new JSONClass ();
			j.Add ("snsType", new JSONData(1));  // For facebook only
			// TODO if Google+ j.Add ("snsType", new JSONData(2));
			j.Add ("snsURL", snsURL); 
			j.Add ("deviceId", SystemInfo.deviceUniqueIdentifier);
			//j.Add ("name", NewUserSNSAccountName.text )// TODO grab name from UI
			SubmitUserRegisterData (j);
		} else {  // Link SNS with old user
			JSONClass data = new JSONClass ();
			JSONClass json = new JSONClass ();

			data.Add ("id",new JSONData(game.login.id));
			data.Add ("snsType",new JSONData(1)); // For facebook only
			data.Add ("snsURL", new JSONData(snsURL));
			data.Add ("deviceId", new JSONData(SystemInfo.deviceUniqueIdentifier));

			json.Add ("action", new JSONData("SET"));
			json.Add ("table", new JSONData("sns"));
			json.Add ("data", data);
			wsc.conn.Send (json.ToString ());
		}
	}
	
	private void SubmitUserRegisterData(JSONNode jData){
		JSONClass json = new JSONClass ();
		json.Add ("action", new JSONData("NEW"));
		json.Add ("table", new JSONData("users"));
		json.Add ("data", jData);
		wsc.conn.Send (json.ToString ());
	}
	
	private void GotoMainUI(){
//		ClickToLoadAsync cla = new ClickToLoadAsync();
//		cla.loadingBar = slider;
//		cla.loadingImage = loadImage;
//		cla.ClickAsync(5);
		Debug.Log ("User ID: "+game.login.id);
		Application.LoadLevel ("MainMenuScene");
	}

	private bool LoginSNS(){

		if (LoginScreen.isSNSInit){
			var perms = new List<string>(){"public_profile", "email", "user_friends"};
			FB.LogInWithReadPermissions(perms, AuthCallback);

//			SoomlaProfile.Login(
//				Provider.FACEBOOK,                        // Social Provider
//				null
//				);
		}
		return LoginScreen.isSNSInit;
	}

	public void ActivateNewFBUserPanel(){
		newFBUserPanel.SetActive (true);
		newFBUserPanelActivated = true;
		userCancelledLogin = false;
		LoginSNS ();
	}

	public void CheckUserSNSInDB(){
//		Debug.Log ("User not linked with SNS");
		JSONNode json = new JSONClass ();
		json ["data"] = snsURL;
		json ["action"] = "GET";
		json ["table"] = "getUserInformationBySnsUrl";
		wsc.conn.Send (json.ToString ());
//		sentDBSNSCheckRequest = true;
	}

	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			LoginScreen.isSNSInit = true;
			// ...
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
	
	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	private void AuthCallback (ILoginResult result) {
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			snsURL = aToken.UserId;
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}

			if (game.login.id!=0){
				Debug.Log (game.login.id);
				isSNSLoggedIn=true;
				CheckUserSNSInDB();
			}
		} else {
			Debug.Log("User cancelled login");
			userCancelledLogin = true;
			newFBUserPanel.SetActive(false);
		}
	}
}

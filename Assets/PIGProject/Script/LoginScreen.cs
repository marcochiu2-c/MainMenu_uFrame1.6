using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using WebSocketSharp;
//using Soomla;
//using Soomla.Profile;
//using Facebook.Unity;
//using Facebook.MiniJSON;
using System;
using System.IO;
<<<<<<< HEAD
=======
using Facebook.Unity;
>>>>>>> feature/MainMenu-shawn

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
<<<<<<< HEAD

=======
	public Button EnterGameButton;
	public Button EnterGameFBButton;
>>>>>>> feature/MainMenu-shawn
//	public bool sentDBSNSCheckRequest = false;
	bool userCancelledLogin = false;

	string debugText = "";
	public static bool isSNSInit = false;
	public static bool isSNSLoggedIn = false;

<<<<<<< HEAD
=======
	public GameObject loadingImageScreen;
>>>>>>> feature/MainMenu-shawn

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
<<<<<<< HEAD
		/*
=======

>>>>>>> feature/MainMenu-shawn
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
<<<<<<< HEAD
		*/
=======
>>>>>>> feature/MainMenu-shawn

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
<<<<<<< HEAD

		if (user!=null){
			Debug.Log (user);
=======
//		Debug.Log (game.login.snsType);
		if (user!=null){

//			Debug.Log (user);
>>>>>>> feature/MainMenu-shawn
			if (user.ToString ()=="{\"success\":false}") {
				Debug.Log ("user not existed");
				
				// TODO else setup account with SNS account  SetupSocialNetworkAccount();
<<<<<<< HEAD
			}else{ // user found in DB
				game.login = new Login(user);
				if (game.login.snsType==1){  // if account already login with SNS
					LoginSNS();

				}
				//                if (game.login.snsType!=0){  // Already registered with SNS


//                }else{ // not yet registered with SNS
				//                    TODO show Facebook register reminder first
				//                    GotoMainUI();
//                }
			}
			user =null;
		}
=======
			}else if (LoginScreen.user != null) {
				var j = LoginScreen.user["attributes"];
				game.login = new Login ((JSONClass)LoginScreen.user);

				LoginScreen.user = null;
			}

			user =null;
		}

		if (game.login.snsType==1){  // if account already login with SNS
//			Debug.Log ("account already login with SNS");
			LoginSNS();
		}	
//		}else if (game.login.snsType==0 && game.login.id==0){// not yet registered with SNS
//			//TODO show Facebook register reminder first
//
//			GotoMainUI();
//		}
>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
=======
					MainScene.UserInfo = userWithSns;
>>>>>>> feature/MainMenu-shawn
					userWithSns = null;
				}
			}

			
		}


	}

<<<<<<< HEAD
	public void SetupStandaloneAccount(){
=======
	public void EnterGameNoSNS(){
		if (game.login.id != 0) {
			GotoMainUI ();
		} else {
			newUserPanel.SetActive (true);
		}
	}

	public void SetupStandaloneAccount(){
		if (NewUserStandAloneName.text.Trim () == "") {
			return;
		}
>>>>>>> feature/MainMenu-shawn
		JSONClass json = new JSONClass ();
		JSONClass j = new JSONClass ();
		j.Add ("snsType", new JSONData(0));
		j.Add ("snsURL", new JSONData(" "));
		j.Add ("deviceId", SystemInfo.deviceUniqueIdentifier);
		j.Add ("name", NewUserStandAloneName.text);// TODO grab name from UI
		SubmitUserRegisterData (j);
	}
	
	public void SetupSocialNetworkAccount(){
<<<<<<< HEAD
=======
		if (NewUserSNSAccountName.text.Trim () == "") {
			return;
		}
>>>>>>> feature/MainMenu-shawn
		if (game.login.id==0) { // Link SNS with new user
			JSONClass json = new JSONClass ();
			JSONClass j = new JSONClass ();
			j.Add ("snsType", new JSONData(1));  // For facebook only
			// TODO if Google+ j.Add ("snsType", new JSONData(2));
			j.Add ("snsURL", snsURL); 
			j.Add ("deviceId", new JSONData(SystemInfo.deviceUniqueIdentifier));
<<<<<<< HEAD
			j.Add ("name", new JSONData(NewUserSNSAccountName));
=======
			j.Add ("name", new JSONData(NewUserSNSAccountName.text));
>>>>>>> feature/MainMenu-shawn
			//j.Add ("name", NewUserSNSAccountName.text )// TODO grab name from UI
			SubmitUserRegisterData (j);
		} else {  // Link SNS with old user
			JSONClass data = new JSONClass ();
			JSONClass json = new JSONClass ();

<<<<<<< HEAD
			data.Add ("id",new JSONData(game.login.id));
=======
			data.Add ("id",new JSONData(game.login.id));   
>>>>>>> feature/MainMenu-shawn
			data.Add ("snsType",new JSONData(1)); // For facebook only
			data.Add ("snsURL", new JSONData(snsURL));
			data.Add ("deviceId", new JSONData(SystemInfo.deviceUniqueIdentifier));

			json.Add ("action", new JSONData("SET"));
			json.Add ("table", new JSONData("sns"));
			json.Add ("data", data);
<<<<<<< HEAD
=======
			Debug.Log (json.ToString ());
>>>>>>> feature/MainMenu-shawn
			wsc.conn.Send (json.ToString ());
		}
	}
	
	private void SubmitUserRegisterData(JSONNode jData){
		JSONClass json = new JSONClass ();
		json.Add ("action", new JSONData("NEW"));
		json.Add ("table", new JSONData("users"));
		json.Add ("data", jData);
		wsc.conn.Send (json.ToString ());
<<<<<<< HEAD
=======

>>>>>>> feature/MainMenu-shawn
		GotoMainUI ();
	}
	
	private void GotoMainUI(){
//		ClickToLoadAsync cla = new ClickToLoadAsync();
//		cla.loadingBar = slider;
//		cla.loadingImage = loadImage;
//		cla.ClickAsync(5);
<<<<<<< HEAD
=======
//		loadingImageScreen.SetActive (true);
		newUserPanel.SetActive (false);
		newFBUserPanel.SetActive (false);
		EnterGameButton.gameObject.SetActive (false);
		EnterGameFBButton.gameObject.SetActive (false);
>>>>>>> feature/MainMenu-shawn
		Debug.Log ("User ID: "+game.login.id);
		Application.LoadLevel ("MainMenuScene");
	}

	private bool LoginSNS(){
<<<<<<< HEAD

		if (LoginScreen.isSNSInit){
			var perms = new List<string>(){"public_profile", "email", "user_friends","user_photos"};
			//FB.LogInWithReadPermissions(perms, AuthCallback);
=======
		Debug.Log ("Logging in with SNS");
		if (LoginScreen.isSNSInit){
			var perms = new List<string>(){"public_profile", "email", "user_friends","user_photos"};
			FB.LogInWithReadPermissions(perms, AuthCallback);
>>>>>>> feature/MainMenu-shawn

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

<<<<<<< HEAD
=======
	public void CheckUserIn(){
		wsc.Send ("login", "SET", new JSONData (game.login.id));
	}

>>>>>>> feature/MainMenu-shawn
	public void CheckUserSNSInDB(){
//		Debug.Log ("User not linked with SNS");
		JSONNode json = new JSONClass ();
		json ["data"] = snsURL;
		json ["action"] = "GET";
		json ["table"] = "getUserInformationBySnsUrl";
		wsc.conn.Send (json.ToString ());
//		sentDBSNSCheckRequest = true;
	}
<<<<<<< HEAD
	
	/*
=======

>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
	*/
=======
	
>>>>>>> feature/MainMenu-shawn
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
<<<<<<< HEAD
	
	/*
=======

>>>>>>> feature/MainMenu-shawn
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

//			StartCoroutine(FbGetPicture(snsURL));

<<<<<<< HEAD


=======
			CheckUserIn();
			if (game.login.snsType==0){
				SetupSocialNetworkAccount();
			}
>>>>>>> feature/MainMenu-shawn

			GotoMainUI();
			if (game.login.id!=0){
				isSNSLoggedIn=true;
				CheckUserSNSInDB();
			}
		} else {
			Debug.Log("User cancelled login");
			userCancelledLogin = true;
			newFBUserPanel.SetActive(false);
		}
	}
<<<<<<< HEAD
	*/
=======
>>>>>>> feature/MainMenu-shawn


}

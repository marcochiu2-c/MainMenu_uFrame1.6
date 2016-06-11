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
	public Button EnterGameButton;
	public Button EnterGameFBButton;
	//	public bool sentDBSNSCheckRequest = false;
	bool userCancelledLogin = false;
	
	string debugText = "";
	public static bool isSNSInit = false;
	public static bool isSNSLoggedIn = false;
	
	public GameObject loadingImageScreen;
	private AsyncOperation async;
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
		wsc.Send ("getUserInformationByDeviceId", "GET", new JSONData (deviceId));
		
		
		//		Debug.Log ("NewFBUserPanel Active: " + newFBUserPanel.activeSelf);
	}
	
	// Update is called once per frame
	void Update () {
		//		Debug.Log (game.login.snsType);
		if (user!=null){
			
			
			if (user.ToString ()=="{\"success\":false}") {
				Debug.Log ("user not existed");
				
				// TODO else setup account with SNS account  SetupSocialNetworkAccount();
			}else if (LoginScreen.user != null) {
				var j = LoginScreen.user["attributes"];
				game.login = new Login ((JSONClass)LoginScreen.user);
				//				Debug.Log (user);
				LoginScreen.user = null;
			}
			
			user =null;
		}
		
//		if (game.login.snsType==1 && isSNSInit){  // if account already login with SNS
//			//			Debug.Log ("account already login with SNS");
//			LoginSNS();
//		}	
		//		}else if (game.login.snsType==0 && game.login.id==0){// not yet registered with SNS
		//			//TODO show Facebook register reminder first
		//
		//			GotoMainUI("MainMenuScene");
		//		}
		if (userWithSns != null)
			Debug.Log (snsURL);
		if (newFBUserPanelActivated) {  // For device not link with SNS account and user choose to link with SNS
			
			if(game.login.id!=0){
				if(userWithSns!=null && snsURL!=""){ 
					Debug.Log (userWithSns.ToString ());
					if (userWithSns.ToString() == "{\"success\":false}") {
						Debug.Log ("No SNS account found in DB");
						SetupSocialNetworkAccount();
						GotoMainUI ("MainMenuScene");
					} else {
						Debug.Log ("SNS account found.");
						GotoMainUI ("MainMenuScene");
					}
					MainScene.UserInfo = userWithSns;
					userWithSns = null;
				}
			}
			
			
		}
		
		
	}
	
	public void EnterGameNoSNS(){
		if (game.login.id != 0) {
			GotoMainUI ("MainMenuScene");
		} else {
			newUserPanel.SetActive (true);
		}
	}
	
	public void SetupStandaloneAccount(){
		if (NewUserStandAloneName.text.Trim () == "") {
			return;
		}
		JSONClass json = new JSONClass ();
		JSONClass j = new JSONClass ();
		j.Add ("snsType", new JSONData(0));
		j.Add ("snsURL", new JSONData(" "));
		j.Add ("deviceId", SystemInfo.deviceUniqueIdentifier);
		j.Add ("name", NewUserStandAloneName.text);// TODO grab name from UI
		SubmitUserRegisterData (j);
	}
	
	public void SetupSocialNetworkAccount(){
		if (NewUserSNSAccountName.text.Trim () == "") {
			Debug.Log ("User id: "+game.login.id);
			if (game.login.id==0){
				
				return;
			}
		}
		if (game.login.id==0) { // Link SNS with new user
			JSONClass json = new JSONClass ();
			JSONClass j = new JSONClass ();
			j.Add ("snsType", new JSONData(1));  // For facebook only
			// TODO if Google+ j.Add ("snsType", new JSONData(2));
			j.Add ("snsURL", snsURL); 
			j.Add ("deviceId", new JSONData(SystemInfo.deviceUniqueIdentifier));
			j.Add ("name", new JSONData(NewUserSNSAccountName.text));
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
			Debug.Log (json.ToString ());
			wsc.conn.Send (json.ToString ());
		}
	}
	
	private void SubmitUserRegisterData(JSONNode jData){
		JSONClass json = new JSONClass ();
		json.Add ("action", new JSONData("NEW"));
		json.Add ("table", new JSONData("users"));
		json.Add ("data", jData);
		wsc.conn.Send (json.ToString ());
		
		GotoMainUI ("MainMenuScene");
	}
	
	private void GotoMainUI(string level){
		//		ClickToLoadAsync cla = new ClickToLoadAsync();
		//		cla.loadingBar = slider;
		//		cla.loadingImage = loadImage;
		//		cla.ClickAsync(5);
		JSONClass json = new JSONClass ();
		json.Add ("action", new JSONData("SET"));
		json.Add ("table", new JSONData("login"));
		wsc.conn.Send (json.ToString ());
		loadingImageScreen.SetActive (true);
		newUserPanel.SetActive (false);
		newFBUserPanel.SetActive (false);
		EnterGameButton.gameObject.SetActive (false);
		EnterGameFBButton.gameObject.SetActive (false);
		Debug.Log ("User ID: "+game.login.id);
		StartCoroutine (LoadLevelWithBar ("MainMenuScene"));
	}
	
	IEnumerator LoadLevelWithBar (string level){
		async = Application.LoadLevelAsync("MainMenuScene");
		while(!async.isDone){
			slider.value=async.progress;
			yield return null;
		}	
	}

	static bool tryLogin = false;
	private bool LoginSNS(){

		if (LoginScreen.isSNSInit){
			if (tryLogin) return false;
			Debug.Log ("Logging in with SNS");
			tryLogin = true;

			var perms = new List<string>(){"public_profile", "email", "user_friends","user_photos"};
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
	
	public void CheckUserIn(){
		wsc.Send ("login", "SET", new JSONData (game.login.id));
	}
	
	public void CheckUserSNSInDB(){
		//		Debug.Log ("User not linked with SNS");
		//		JSONNode json = new JSONClass ();
		//		json ["data"] = snsURL;
		//		json ["action"] = "GET";
		//		json ["table"] = "getUserInformationBySnsUrl";
		//		wsc.conn.Send (json.ToString ());
		wsc.Send ("getUserInformationBySnsUrl", "GET", snsURL);
		//		sentDBSNSCheckRequest = true;
	}
	
	private void InitCallback ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK
			LoginScreen.isSNSInit = true;
			if (game.login.snsType==1) {
				Utilities.ShowLog.Log ("SNS ID: " + game.login.snsURL);
				LoginSNS ();
			}
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
		Debug.Log (result);
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log(aToken.UserId);
			snsURL = aToken.UserId;
			// Print current access token's granted permissions
#if UNITY_EDITOR
			foreach (string perm in aToken.Permissions) {
				Debug.Log(perm);
			}
#endif
			
			
			if (game.login.snsType==0){
				SetupSocialNetworkAccount();
			}
			CheckUserIn();
			GotoMainUI("MainMenuScene");
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
	
	
}
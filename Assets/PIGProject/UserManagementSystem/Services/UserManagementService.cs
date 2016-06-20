using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using UnityEngine;
using uFrame.MVVM;
using SimpleJSON;


public class UserManagementService : UserManagementServiceBase
{

    [Inject("LocalUser")] public UserViewModel LocalUser;
	public MainScene MainScene;
    public override void Setup()
    {
        base.Setup();
		// for develop only
		LocalUser.AuthorizationState = AuthorizationState.Authorized;
		LocalUser.ScreenState = ScreenState.MainMenu;
		//Debug.Log (MainScene == null ? "MainScene  is null" : MainScene.name);
		//LocalUser.AuthorizationState = AuthorizationState.Unauthorized;
		//Game game = Game.Instance;
		//Debug.Log ("Game Wealth: " + game.wealth[0].toJSON().ToString());
    }

    public void AuthorizeLocalUser(string Username, string Password)
    {
        if (Username != "pig" && Password != "pig")
        {
            Debug.Log("authorized in service");
            LocalUser.AuthorizationState = AuthorizationState.Authorized;
        }
    }
	
	/*
	public void loadDB ()
	{
		if(GameObject.Find ("MainUIHolder").GetComponent<MainScene> () != null)
			MainScene = GameObject.Find ("MainUIHolder").GetComponent<MainScene> ();
				
		WsClient wsc = WsClient.Instance;
		Debug.Log ("loadDB from UserManagermentService");
		
		MainScene.needReloadFromDB = true;
		MainScene.CallMainScene();
	}
	*/
}

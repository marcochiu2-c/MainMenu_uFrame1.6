using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;
using SimpleJSON;
using Utilities;
using UnityEngine;


public class ConferenceScreenController : ConferenceScreenControllerBase {
	Game game;
	WsClient wsc = WsClient.Instance;
	public UserViewModel LocalUser;
	
	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	
    public override void InitializeConferenceScreen(ConferenceScreenViewModel viewModel) {
        base.InitializeConferenceScreen(viewModel);
        // This is called when a ConferenceScreenViewModel is created
        
		LocalUser = uFrameKernel.Container.Resolve<UserViewModel>("LocalUser");
		Debug.Log (LocalUser == null ? "LocalUser is null" : LocalUser.Identifier);
		//Debug.Log ("Game Wealth: " + game.wealth[1].toJSON().ToString());
		game = Game.Instance;
		
		LocalUser.UserLevel = CharacterPage.UserLevelCalculator(game.login.exp);
		int userLevel = LocalUser.UserLevel;
				//Testing use
		//int userLevel = 25;
		LocalUser.TotalTeam = 1;
		
		if (userLevel > 10)
			LocalUser.TotalTeam = 2;
		
		if (userLevel > 20)
			LocalUser.TotalTeam = 3;
		
		if (userLevel > 30)
			LocalUser.TotalTeam = 4;
		
		if (userLevel > 40)
			LocalUser.TotalTeam = 5;
		
		for (int i = 1; i <= LocalUser.TotalTeam; i++)
		{
			LocalUser.Soldier.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			LocalUser.Soldier[i - 1].Max_Health = 0;
			Debug.Log ("SoldierVM added");
			
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		}
		
		for(int i = 0; i < 5; i++)
		{
			if(game.teams[i].status == 1)
			{
				LocalUser.SetTeam = true;
				viewModel.Group = i + 1;
				viewModel.SoldierQuantity = game.teams[i].soldierQuantity;
				viewModel.SoldierType = game.teams[i].soldierJson["type"].AsInt;
				SetSoldierData(viewModel);
				//Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(_ => SetSoldierData(viewModel));
			}
		}
	}
    
    
    public override void InitSoldierValue(ConferenceScreenViewModel viewModel) {
		base.InitSoldierValue(viewModel);
        
		float soldierHealth = 1000; 
		int AssigningSoldier = 0;
		/*
		Soldier data should be passed from menu:
			Name Soldier 12345
				Float Health
				Int AttackSpeed
				Float Physique
				Int HitPoint
				Int WeaponProficiency
				float Dodge
				Int InitialMorale
				Int Prestige
				Career Career
				SenseStyle Sense
		*/
		
		
		/*
		for (int i = 1; i <= LocalUser.TotalTeam ; i++)
		{
			SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		}
		*/
		
		Debug.Log (game.soldiers[AssigningSoldier].attributes);
		
		
		/// Soldier Type: Get and select Soldier Type from School Field
		AssigningSoldier = 0;
		
		
		/// Put the game soldier value into battle value
		/*
		viewModel.Physique = 70;
		viewModel.HitPoint = 70;
		viewModel.Dodge = 30;
		viewModel.WeaponProficiency = 90;
		viewModel.Health = 1999;
		viewModel.AttackSpeed  = 1;
		viewModel.InitialMorale =  80;
		viewModel.moraleStandard = 70;
		viewModel.Prestige = 100;
		*/
		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierVM[i].AttackSpeed = game.soldiers[AssigningSoldier].attributes["AttackSpeed"].AsInt;
			SoldierVM[i].Physique = 70;
			SoldierVM[i].HitPoint = 70;
			SoldierVM[i].WeaponProficiency = 90;
			SoldierVM[i].Dodge = game.soldiers[AssigningSoldier].attributes["Dodge"].AsFloat;
			SoldierVM[i].InitialMorale = game.soldiers[AssigningSoldier].attributes["Morale"].AsInt;
			SoldierVM[i].Prestige = 100;
			//SoldierVM[i].Career = game.soldiers[AssigningSoldier].attributes["Career"].AsInt;
			
			SoldierVM[i].Health = 2001;
			SoldierVM[i].Max_Health = 2001;
		}
		
		
		/// Soldier Quatity: Set Number in TextField
		//SoldierVM[0].Health = soldierHealth;
		//game.soldiers[SchoolField.AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = soldierQuantity;
		//game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldierQuantity;
		//
    }
    
    
	public override void SetSoldierData(ConferenceScreenViewModel viewModel) {
		base.SetSoldierData(viewModel);
		
		int SoldierGroup = viewModel.Group - 1;
		int SoldierType = viewModel.SoldierType - 1;
		
		Debug.Log("SoldierGroup: " +  SoldierGroup);
		Debug.Log("AttackSpeed: " + game.soldiers[SoldierType].attributes["AttackSpeed"].AsFloat);
		
		LocalUser.Soldier[SoldierGroup].AttackSpeed = game.soldiers[SoldierType].attributes["AttackSpeed"].AsFloat;
		LocalUser.Soldier[SoldierGroup].Physique = game.soldiers[SoldierType].attributes["Strength"].AsFloat;
		LocalUser.Soldier[SoldierGroup].HitPoint = game.soldiers[SoldierType].attributes["Hit"].AsFloat;
		LocalUser.Soldier[SoldierGroup].Dodge = game.soldiers[SoldierType].attributes["Dodge"].AsFloat;
		LocalUser.Soldier[SoldierGroup].InitialMorale = game.soldiers[SoldierType].attributes["Morale"].AsFloat;
		LocalUser.Soldier[SoldierGroup].WeaponProficiency = 90;
		LocalUser.Soldier[SoldierGroup].Prestige = 100;
		//SoldierVM[i].Career = game.soldiers[AssigningSoldier].attributes["Career"].AsInt;
		
		LocalUser.Soldier[SoldierGroup].Health = viewModel.SoldierQuantity;
		LocalUser.Soldier[SoldierGroup].Max_Health = viewModel.SoldierQuantity;
		
		//TODO: change weapon quantity
		
	}

    public override void SetTeam(ConferenceScreenViewModel viewModel) {
        base.SetTeam(viewModel);
    }
}


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
        GetSoldierValue();
    }
    
    public void GetSoldierValue()
    {
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
		
		game = Game.Instance;
		
		for (int i = 1; i <= 5 ; i++)
		{
			// Get the Controllers, ViewModels and Views from Kernel
			SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		}
		
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
			
			//Debug.Log ("AttackSpeed: " + SoldierVM[0].AttackSpeed);
		}
		
		
		/// Soldier Quatity: Set Number in TextField
		SoldierVM[0].Health = soldierHealth;
		//game.soldiers[SchoolField.AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = soldierQuantity;
		//game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldierQuantity;
		
		
		
		//
		
		
    }

    public override void SetSoldierData(ConferenceScreenViewModel viewModel) {
        base.SetSoldierData(viewModel);
        
        int SoldierGroup = viewModel.Group;
        int SoldierType = viewModel.SoldierType;
        
		SoldierVM[SoldierGroup].AttackSpeed = game.soldiers[SoldierType].attributes["AttackSpeed"].AsInt;
		SoldierVM[SoldierGroup].Physique = game.soldiers[SoldierType].attributes["Strength"].AsInt;
		SoldierVM[SoldierGroup].HitPoint = game.soldiers[SoldierType].attributes["Hit"].AsInt;
		SoldierVM[SoldierGroup].Dodge = game.soldiers[SoldierType].attributes["Dodge"].AsFloat;
		SoldierVM[SoldierGroup].InitialMorale = game.soldiers[SoldierType].attributes["Morale"].AsInt;
		SoldierVM[SoldierGroup].WeaponProficiency = 90;
		SoldierVM[SoldierGroup].Prestige = 100;
		//SoldierVM[i].Career = game.soldiers[AssigningSoldier].attributes["Career"].AsInt;
		
		SoldierVM[SoldierGroup].Health =  viewModel.SoldierQuantity ;
		SoldierVM[SoldierGroup].Max_Health =  SoldierVM[SoldierGroup].Health;
		
    }
}

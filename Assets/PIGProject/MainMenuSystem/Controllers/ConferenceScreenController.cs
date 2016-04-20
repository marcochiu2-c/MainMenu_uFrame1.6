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
	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	
    public override void InitializeConferenceScreen(ConferenceScreenViewModel viewModel) {
        base.InitializeConferenceScreen(viewModel);
        // This is called when a ConferenceScreenViewModel is created
        SetSoldierVM();
    }
    
    public void SetSoldierVM()
    {
		int AssigningSoldier = 0;
		float soldierHealth = 1000; 
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
		
		for(int i = 1; i <= 5; i++)
		{
		// Get the Controllers, ViewModels and Views from Kernel
		SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
		Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		}
		Debug.Log (game.soldiers[AssigningSoldier].attributes);
		
		
		/// Soldier Type: Get and select Soldier Type from School Field
		
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
			
			int maxSoldeirsQuatity = game.soldiers[AssigningSoldier].quantity;
	
		for(int i = 0; i < 5; i++)
		{
			SoldierVM[i].AttackSpeed = game.soldiers[AssigningSoldier].attributes["AttackSpeed"].AsInt;
			SoldierVM[i].Physique = 70;
			SoldierVM[i].HitPoint = 70;
			SoldierVM[i].Dodge = game.soldiers[AssigningSoldier].attributes["Dodge"].AsFloat;
			SoldierVM[i].InitialMorale = 70;
			SoldierVM[i].Prestige = 100;
			SoldierVM[i].WeaponProficiency = 90;
			SoldierVM[i].Career = Career.Swordman;
			SoldierVM[i].Sense = SenseStyle.AGGRESSIVE;
			
			SoldierVM[i].Health = 2001;
			SoldierVM[i].Max_Health = SoldierVM[i].Health;
			//Debug.Log ("AttackSpeed: " + SoldierVM[0].AttackSpeed);
		}
		
		/// Soldier Quatity: Set Number in TextField
		SoldierVM[0].Health = soldierHealth;
		//game.soldiers[SchoolField.AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = soldierQuantity;
		//game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldierQuantity;
		
		//
    }
}

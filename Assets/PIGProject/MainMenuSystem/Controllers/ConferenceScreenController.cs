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
        //GetSoldierValue();
    }
    
    public void SetSoldierVM(int i, int AssigningSoldier)
    {
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
		
		// Get the Controllers, ViewModels and Views from Kernel
		SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
		Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		
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
	
			
			SoldierVM[i].AttackSpeed = game.soldiers[AssigningSoldier].attributes["AttackSpeed"].AsInt;
			SoldierVM[i].Physique = game.soldiers[AssigningSoldier].attributes["Strength"].AsInt;
			SoldierVM[i].HitPoint = game.soldiers[AssigningSoldier].attributes["Hit"].AsInt;
			SoldierVM[i].Dodge = game.soldiers[AssigningSoldier].attributes["Dodge"].AsFloat;
			SoldierVM[i].InitialMorale = game.soldiers[AssigningSoldier].attributes["Morale"].AsInt;
			SoldierVM[i].Prestige = game.soldiers[AssigningSoldier].attributes["Strength"].AsInt;
			//SoldierVM[i].WeaponProficiency = game.soldiers[AssigningSoldier].attributes["AttackSpeed"].AsInt;
			//SoldierVM[i].Career = game.soldiers[AssigningSoldier].attributes["Career"].AsInt;
			
			SoldierVM[i].Health = 2001;
			//Debug.Log ("AttackSpeed: " + SoldierVM[0].AttackSpeed);
		
		
		/// Soldier Quatity: Set Number in TextField
		SoldierVM[0].Health = soldierHealth;
		//game.soldiers[SchoolField.AssigningSoldier-1].attributes["trainingSoldiers"].AsInt = soldierQuantity;
		//game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldierQuantity;
		
		//
    }
}

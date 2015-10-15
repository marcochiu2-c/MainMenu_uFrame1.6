using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using UnityEngine;

public class SoldierController : SoldierControllerBase {
	public override void InitializeSoldier(SoldierViewModel viewModel) {
		base.InitializeSoldier(viewModel);
        // This is called when a SoldierViewModel is createdmove
		viewModel.Physique = 70;
		viewModel.HitPoint = 70;
		viewModel.Dodge = 30;
		viewModel.WeaponProficiency = 90;
		viewModel.Health = 1999;
		viewModel.AttackSpeed  = 1;
		viewModel.InitialMorale =  80;
		viewModel.moraleStandard = 70;
		viewModel.Prestige = 100;
		//viewModel.Weapon = new Weapons{Weight = 2,OtherHit = 50, CriticalHit = 30, FatalHit = 20, Sharpness = 90, IsSharp = true};
		//viewModel.Armor = new Armors {Weight = 2, OtherCover = 30, CriticalCover = 30, FatalCover = 30, Hardness =30};
		//viewModel.Shield = new Shields{Weight = 0 , BlockRate = 0, Hardness =0};
		//viewModel.Formation = new Formations{HitPoint = 10, Dodge = 10, Morale =5};
		Debug.Log(viewModel.Health);
	}
	
	public override void ChangeActionStyle(SoldierViewModel viewModel) {
        base.ChangeActionStyle(viewModel);
    }

	public override void ChangeMoveStyle(SoldierViewModel viewModel) {
        base.ChangeMoveStyle(viewModel);
    }

	public override void ChangeQuantity(SoldierViewModel viewModel) {
        base.ChangeQuantity(viewModel);
    }


    public override void PlayAction(SoldierViewModel viewModel) {
		base.PlayAction(viewModel);
		for(int i=0; i < viewModel.playlist.Count; i++)
		{

			//viewModel.playlist[i].saveMove
			//viewModel.playlist[i].SaveAction
			//viewModel.playlist[i].savePointLocation
			//MainGameController.StartBattle(SoldierVM[0], TargetVM[i], SoldierView, TargetV[i]);

			//call move
		}
    }
}

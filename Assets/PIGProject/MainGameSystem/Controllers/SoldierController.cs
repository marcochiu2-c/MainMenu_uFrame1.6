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
		//viewModel.Weapon = new Weapons{Weight = 2,OtherHit = 50, CriticalHit = 30, FatalHit = 20, Sharpness = 90, IsSharp = true};
		//viewModel.Armor = new Armors {Weight = 2, OtherCover = 30, CriticalCover = 30, FatalCover = 30, Hardness =30};
		//viewModel.Shield = new Shields{Weight = 0 , BlockRate = 0, Hardness =0};
		//viewModel.Formation = new Formations{HitPoint = 10, Dodge = 10, Morale =5};
	}
	
	public override void ChangeActionStyle(SoldierViewModel viewModel) {
        base.ChangeActionStyle(viewModel);

		if(viewModel.Action == ActionStyle.ATTACK)
		{
			//Debug.Log ("ActionStyle is ATTACK");
		}

		else if(viewModel.Action == ActionStyle.ASSAULT)
		{
			//Debug.Log ("ActionStyle is ASSAULT");
		}

		else if(viewModel.Action == ActionStyle.FEINT)
		{
			//Debug.Log ("ActionStyle is FEINT");
		}

		else if(viewModel.Action == ActionStyle.PIN)
		{
			//Debug.Log ("ActionStyle is PIN");
		}

		else if(viewModel.Action == ActionStyle.RAID)
		{
			//Debug.Log ("ActionStyle is RAID");
		}

		else if(viewModel.Action == ActionStyle.SEARCH)
		{
			//Debug.Log ("ActionStyle is SEARCH");
		}

		else if(viewModel.Action == ActionStyle.YAWP)
		{
			//Debug.Log ("ActionStyle is YWAP");
		}
    }
}

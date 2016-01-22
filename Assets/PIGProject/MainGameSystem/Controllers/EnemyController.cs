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


public class EnemyController : EnemyControllerBase {
    
    public override void InitializeEnemy(EnemyViewModel viewModel) {
        base.InitializeEnemy(viewModel);
        // This is called when a EnemyViewModel is created
		viewModel.Physique = 100;
		viewModel.HitPoint = 80;
		viewModel.Dodge = 50;
		viewModel.WeaponProficiency = 90;
		viewModel.Health = 2000;
		viewModel.AttackSpeed  = 4;
		viewModel.InitialMorale =  80;
		viewModel.moraleStandard = 70;
		viewModel.Prestige = 100;
		//viewModel.Weapon = new Weapons {Weight = 2,OtherHit = 50, CriticalHit = 30, FatalHit = 20, Sharpness = 90, IsSharp = true};
		//viewModel.Armor = new Armors {Weight = 2, OtherCover = 30, CriticalCover = 30, FatalCover = 30, Hardness =30};
		//viewModel.Shield = new Shields{Weight = 0 , BlockRate = 0, Hardness =0};
		//viewModel.Formation = new Formations {HitPoint = 10, Dodge = 10, Morale =5};
		//Debug.Log(viewModel.Weapon);
    }
}

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
using System.Timers;
using UnityEngine.UI;

public class EntityController : EntityControllerBase {
	public object[] param;
	public MainGameRootViewModel MainGameVM;

    public override void InitializeEntity(EntityViewModel viewModel) {
        base.InitializeEntity(viewModel);
		MainGameVM = uFrameKernel.Container.Resolve<MainGameRootViewModel>("MainGameRoot");
		//Debug.Log (MainGameVM== null ? "MainGameVM is null" : MainGameVM.Identifier);
	}
	
	public void StartWar(object source, System.Timers.ElapsedEventArgs e)	{
		//Result (caller)
		
	}
	
	public override void ChangeBattleState(EntityViewModel viewModel) {
		base.ChangeBattleState(viewModel);
		
		if(viewModel.BattleState == BattleState.CONFUSING)
		{
			Debug.Log(viewModel.Name + "is confusing");
			var DoDamage = Observable.Interval(TimeSpan.FromSeconds(1))
				.Subscribe(_ => 
				{
					viewModel.Health -= 100;
					Debug.Log (viewModel.Name + " is confusing and hit themselves");
				}); 
			Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(_ => DoDamage.Dispose());
		}
	}
	
	public override void ChangeHealth(EntityViewModel viewModel) {
		base.ChangeBattleState(viewModel);
		//MainGameVM.GameState = GameState.GameOver;
		
		viewModel.BattleState = BattleState.DEAD;
				
		Debug.Log("I Die la~~~~~~~~~~~~~~~~~~~" + viewModel.BattleState);
		
		if(viewModel is SoldierViewModel)
			MainGameVM.SoldierCount -= 1;
		else
			MainGameVM.EnemyCount -= 1;
			
		if(MainGameVM.EnemyCount == 0 || MainGameVM.SoldierCount == 0)
			MainGameVM.GameState = GameState.GameOver;

	}
	/*
	public int getGCDOfAttackSpeed(EntityViewModel viewModel){
			int Remainder;
			int a = viewModel.AttackSpeed;
			int b = viewModel.Opponent.AttackSpeed;
			Debug.Log ("Attack Speed of A: " + a + " Attack Speed of B: " + b);
			
			while( b != 0 )
			{
				Remainder = a % b;
				a = b;
				b = Remainder;
				//Debug.Log ("Remainder: "+Remainder);
			}
			
			return a;
		}
		
	public float WeightDeduct (EntityViewModel viewModel){
			int w = viewModel.Weapon.Weight + viewModel.Armor.Weight + viewModel.Shield.Weight;
			if (w > viewModel.Physique) {
			Debug.LogError (viewModel.Name + ", Physique: " + viewModel.Physique + " cannot be larger than the sum of the weight of all equipment." + w);
				//if (DEBUG) w = (float) Physique;
			}
		return 1 - w / viewModel.Physique;
		}
		
	public float ActualHit(EntityViewModel viewModel){
			const float MIN = 0.01f;
			const float MAX = 0.99f;
			//Debug.Log (Name+"ActualMorale(): " + ActualMorale ());
			#if DEBUG
		Debug.Log (viewModel.Name + " Formation.HitPoint: " + viewModel.Formation.HitPoint);
			#endif
		float ah = viewModel.HitPoint * WeightDeduct (viewModel) * viewModel.WeaponProficiency * (ActualMorale (viewModel) / viewModel.moraleStandard) / 10000f;
			ah += ah * viewModel.Formation.HitPoint / 100;
			ah = ah > MAX ? MAX : ah;
			ah = ah < MIN ? MIN : ah;
			return ah;
			//return HitPoint * WeightDeduct() * WeaponProficiency /10000;
		}
		
	public float ActualDodge(EntityViewModel viewModel){
			const float MIN = 0.01f;
			const float MAX = 0.99f;
			float ad = 0;
			//	if (SoldiersDEBUG) Debug.Log (Name + " ActualMorale(): " + ActualMorale ()+"%");
			ad = viewModel.Dodge * WeightDeduct(viewModel) * (ActualMorale (viewModel) / viewModel.moraleStandard) * (100 + viewModel.Formation.Dodge)/10000f;
			ad = ad > MAX ? MAX : ad;
			ad = ad < MIN ? MIN : ad;
			
			return ad;
		}


	public float Hit (EntityViewModel viewModel){ 
			//if (SoldiersDEBUG) Debug.Log (Name + " Opponent.ActualDodge(): " + (Opponent.ActualDodge()*100)+"%");
		return viewModel.actualHit * (1 - viewModel.Opponent.ActualDodge(viewModel) - (viewModel.Opponent.Shield.BlockRate/100f));
			//return (100 - blockage - missHit - dodgeToHit)/100f;
		}
		
	public float Blockage (EntityViewModel viewModel){
		return ActualHit()*(1-viewModel.Opponent.ActualDodge(viewModel))* viewModel.Opponent.Shield.BlockRate;
		}

		
	public float ActualMorale(EntityViewModel viewModel){  
			#if DEBUG
		Debug.Log (viewModel.Name + " InitialMorale: " + viewModel.InitialMorale);
			//Debug.Log (Name + " AdvisorPrestige - oppo.AdvisorPrestige: " + AdvisorPrestige + "  "+ Opponent.AdvisorPrestige);
			//Debug.Log (Name + " GeneralPrestige - oppo.GeneralPrestige: " + GeneralPrestige  + "  "+ Opponent.GeneralPrestige);
			#endif
			return viewModel.InitialMorale * (200f + (viewModel.Prestige - viewModel.Opponent.Prestige)) * (100f + viewModel.Formation.Morale)/20000f;
		}
	*/
}
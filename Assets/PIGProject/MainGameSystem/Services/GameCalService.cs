using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using UniRx;
using uFrame.MVVM;
using uFrame.Kernel;
using UnityEngine;


public class GameCalService : GameCalServiceBase {
	public SoldierViewModel Soldiers;
	//[Inject] public EnemyViewModel Enemy;
	public List<SoldierViewModel> soldiers = new List<SoldierViewModel>();

	public override void Setup()
	{
		base.Setup();
		Observable.EveryUpdate().Subscribe(_ => 
		{
			//UpdateBattle();
		});
	}



	/*
	public void UpdateBattle(){

		for (int i=0; i < soldiers.Count; i++) {
			if (Soldiers.TimeStarted && (Time.time-soldiers[i].startime>=1f/soldiers[i].AttackSpeed)){
				//soldiers[i].Result(this);
				//soldiers[i].startime=Time.time;
			}
		}
		
		if (Time.time > 60)
			soldiers = null;
	}

	public 	void OnStartButtonClicked(){
		/*
		soldiers.Add (new SoldierViewModel (SetParameterSoldier1 ()));
		soldiers.Add (new SoldierViewModel (SetParameterSoldier2 ()));
		//soldiers.Add (new Soldiers (SetParameterSoldier3 ()));
		soldiers [0].Opponent = soldiers [1];
		soldiers [1].Opponent = soldiers [0];
		//soldiers [2].Opponent = soldiers [1];
		soldiers [0].action = ActionStyle.PIN;
		//soldiers [2].action = ActionStyle.ATTACK;
		Soldiers.DEBUG = false;
		soldiers [0].WarTimeLimitInSecond = 30;
		soldiers [1].WarTimeLimitInSecond = 30;
		Debug.Log ("GCD Of AttackSpeed: " + soldiers [0].getGCDOfAttackSpeed ());
		soldiers [0].UpdatePerRound = soldiers [0].AttackSpeed / soldiers [0].getGCDOfAttackSpeed ();
		soldiers [1].UpdatePerRound = soldiers [1].AttackSpeed / soldiers [1].getGCDOfAttackSpeed ();
		soldiers [0].ElementsPerSecond = soldiers [0].UpdatePerRound * soldiers [1].UpdatePerRound;
		soldiers [1].ElementsPerSecond = soldiers [1].ElementsPerSecond;
		
		for (int i=0; i<soldiers.Count; i++) {
			soldiers [i].GetHealthProbabilities ();
			soldiers[i].starttime = Time.time;
			
			//soldiers [i].Timer.Start ();
			
			soldiers[i].caller= this;
		}
		Soldiers.timerStarted =true;
	*/
	//}
	

}

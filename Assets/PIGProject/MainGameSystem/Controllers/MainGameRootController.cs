using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using Gamelogic;
using Gamelogic.Grids;
using System.Timers;
using DG.Tweening;
using Random = UnityEngine.Random;

public class MainGameRootController : MainGameRootControllerBase {
    
	public static float WarStartTime = 0;


    public override void InitializeMainGameRoot(MainGameRootViewModel viewModel) {
        base.InitializeMainGameRoot(viewModel);
        // This is called when a MainGameRootViewModel is created
         //viewModel.PlayerIQ = 200;
         ///viewModel.WinCondition = WinCondition.Tower;
    }

    public override void GoToMenu(MainGameRootViewModel viewModel) {
        base.GoToMenu(viewModel);
    }

    public override void Play(MainGameRootViewModel viewModel) {
        base.Play(viewModel);
    }

    public override void GameOver(MainGameRootViewModel viewModel) {
        base.GameOver(viewModel);
        Debug.Log ("Gameover");
    }

	// P1 = Soldier
	// P2 = Enemy
	public void StartBattle(EntityViewModel P1, EntityViewModel P2, EntityView P1v, EntityView P2v, ActionStyle action)
	{
		Debug.Log("Running StartBattle");

		List<EntityViewModel> soldiers = new List<EntityViewModel>();
		List<EntityView> soldiersView = new List<EntityView>();
		bool _battleFinished = false;
		bool TimerStarted = false;
		//soldiers.Insert(index1, P1);
		//soldiers.Insert(index2, P2);
		//soldiersView.Insert (index1, P1v);
		//soldiersView.Insert (index2, P2v);
		//soldiers[index1].Opponent = soldiers[index2];
		//soldiers[index2].Opponent = soldiers[index1];
		//soldiersView[index1].OpponentView = soldiersView[index2];
		//soldiersView[index2].OpponentView = soldiersView[index1];

		soldiers.Insert(0, P1);
		soldiers.Insert(1, P2); 
		soldiersView.Insert (0, P1v);
		soldiersView.Insert (1, P2v);

		//Need to check if not the first guy, prevent to run these script
		//if(P1.BattleState == BattleState.WAITING)
		//{
	   	soldiers[0].Opponent = soldiers[1];
		soldiersView[0].OpponentView = soldiersView[1];
		soldiers[0].TimeStarted = true;

		P1.BattleState = BattleState.FIGHTING;
		//}


		//check if P2 fighting to others, not match first
		if(P2.BattleState == BattleState.WAITING)
		{
			P2.Opponent = P1;
			soldiers[1].Opponent = soldiers[0];
			soldiersView[1].OpponentView = soldiersView[0];
			soldiers[1].TimeStarted = true;

			P2.BattleState = BattleState.FIGHTING;
		}


		for (int i = 0; i < soldiers.Count; i++)
		{
			Debug.Log ("soldiers.Count: " + soldiers.Count);
			soldiers[i].GetHealthProbabilities();
			soldiers[i].starttime = Time.time;
		}

		WarStartTime = Time.time;

		//if(soldiers[0] != null)
		//	soldiers[0].TimeStarted = true;
		//if(soldiers[1] != null)
		//	soldiers[1].TimeStarted = true;
		_battleFinished = false;
		
		if(action == ActionStyle.ASSAULT)
		{
			float prob = Random.value;
			
			if(prob >= 0.25f)
				P1.BattleState = BattleState.CONFUSING;
			else if(prob < 0.5 && prob >= 0.5f)
				P2.BattleState = BattleState.CONFUSING;
			else if(prob < 0.5f && prob >= 0.75f)
			{
				P1.BattleState = BattleState.CONFUSING;
				P2.BattleState = BattleState.CONFUSING;
			}
			
		}

		//Observable.EveryUpdate().Where (_ => soldiers[0].TimeStarted == true && soldiers[1].TimeStarted).Subscribe(_ => 
		Observable.EveryUpdate().Subscribe(_ => 
		{
			if (soldiers.Count > 0)
			{
				//check if P1 finish battle from others
				if(P1.BattleState == BattleState.WAITING)
				{
					soldiers[0].Opponent = soldiers[1];
					soldiersView[0].OpponentView = soldiersView[1];
					_battleFinished = false;
					WarStartTime = Time.time;
					P1.BattleState = BattleState.FIGHTING;
					
					soldiers[0].GetHealthProbabilities();
					soldiers[0].starttime = Time.time;
					soldiers[0].TimeStarted = true;
					soldiers[1].TimeStarted = true;
				}
				//check if P2 finish battle from others
				if(P2.BattleState == BattleState.WAITING)
				{
					soldiers[1].Opponent = soldiers[0];
					soldiersView[1].OpponentView = soldiersView[0];
					_battleFinished = false;
					WarStartTime = Time.time;
					P2.BattleState = BattleState.FIGHTING;
					
					soldiers[1].GetHealthProbabilities();
					soldiers[1].starttime = Time.time;
					soldiers[0].TimeStarted = true;
					soldiers[1].TimeStarted = true;
				}
				

				for (int i=0; i < soldiers.Count; i++) 
				{
					if (soldiers[i].TimeStarted  && soldiers[i].Opponent != null && Time.time - soldiers[i].starttime >= 1f / soldiers[i].AttackSpeed) 
					{
						
						Result (WarStartTime, soldiers[i], soldiersView[i], action);
						soldiers[i].starttime = Time.time;
					}

					if (!soldiers[i].TimeStarted)
					{
						Debug.Log ("Waiting from GameController");
						if(P1 != null && P1.BattleState != BattleState.DEAD) P1.BattleState = BattleState.WAITING;
						if(P2 != null && P2.BattleState != BattleState.DEAD) P2.BattleState = BattleState.WAITING;
						_battleFinished = true;
						soldiers.Clear ();
					}
				}
			}
		});
	}

	public void Result(float warStartTime, EntityViewModel p, EntityView pV, ActionStyle action){
		float factor = 1.0f;
		float timeDiff = Time.time - warStartTime;
		/*
		if (action == ActionStyle.PIN){
			factor = 0.5f;
		}
		*/

		//AcionStyle
		/*
		if(action == ActionStyle.ASSAULT)
		{
			float prob = Random.value;

			if(prob >= 0.25f)
				p.Opponent.BattleState = BattleState.CONFUSING;
			else if(prob < 0.5 && prob >= 0.5f)
				p.BattleState = BattleState.CONFUSING;
			else if(prob < 0.5f && prob >= 0.75f)
			{
				p.BattleState = BattleState.CONFUSING;
				p.Opponent.BattleState = BattleState.CONFUSING;
			}

		}
		*/
		
		/*
		else if(action == ActionStyle.RAID)
		{
			float prob = Random.value; 

			if(prob >= 0.5f)
				p.Opponent.BattleState = BattleState.CONFUSING;
		}
		*/

		if(action == ActionStyle.FEINT)
		{
			if(timeDiff >= 1f)
			{
				p.TimeStarted = false;
				p.Counter++;
				return;
			}
		}


		else if(action == ActionStyle.PIN)
		{
			factor = 0.5f;
		}
		
		float health = p.Opponent.Health * Mathf.Pow (p.Opponent.noHurt, p.Health / (float)p.Opponent.Health);
		float d = p.Opponent.Health - Mathf.Pow (p.Opponent.noHurt + p.Opponent.hurt, p.Health / (float)p.Opponent.Health) * p.Opponent.Health;
		float ht = p.Opponent.Health - health - d;	
		p.Opponent.Dead = d * factor;
		p.Opponent.Hurt = ht * factor;
		//healthHistory [nextCnt] = (healthHistory [counter] - Mathf.RoundToInt(d  * factor) - Mathf.RoundToInt(ht*factor));
		p.Opponent.Health = (p.Opponent.Health - p.Opponent.Dead - p.Opponent.Hurt) >= 0.5 ? p.Opponent.Health - p.Opponent.Dead - p.Opponent.Hurt : 0;

		//if(p.Opponent.Health > 0)
			pV.AtkAndUpdateHealth();

		if (p.Opponent.Health <= 0) 
		{
			p.TimeStarted = false;
			return;
		}
		//Debug.Log (Name + " Health: " + healthHistory [Counter]);
		string colorTag = p.Name != "Soldier3" ? "<color=red>" :"<color=yellow>";
		colorTag = p.Name == "Soldier4" ? "<color=purple>" : colorTag;
		//call animation, but how to call the view???
		Debug.Log (colorTag + "Time</color> " + timeDiff + "s, " + p.Opponent.Name + " Results: " + health + " " + ht + " " + d + " <color=blue>Opponent.Counter:</color> " + p.Counter);
		Debug.Log (colorTag + "Time</color> " + timeDiff + "s, " + p.Opponent.Name + " Actual Results: " + p.Opponent.Health + " " + p.Opponent.Hurt + " " + p.Opponent.Dead + " Total: " + (p.Opponent.Health + p.Opponent.Hurt + p.Opponent.Dead));
		//Debug.Log (colorTag + "Round</color> " + roundCounter + ", " + p.Opponent.Name + " Results: " + health + "   " + ht  + "  " + d  + " <color=blue>Opponent.Counter:</color> " + p.Counter);
		//Debug.Log (colorTag + "Round</color> " + roundCounter + ", " + p.Opponent.Name + " Actual Results: " + p.Opponent.Health + "   " + p.Opponent.Hurt + "  " + p.Opponent.Dead + " Total: " + (p.Opponent.Health + p.Opponent.Hurt + p.Opponent.Dead));
		p.Counter++;
		}
		
	}

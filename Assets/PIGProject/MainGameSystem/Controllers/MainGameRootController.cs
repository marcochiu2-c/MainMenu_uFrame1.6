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

public class MainGameRootController : MainGameRootControllerBase {
    
	public List<EntityViewModel> soldiers = new List<EntityViewModel>();
	public List<EntityView> soldiersView = new List<EntityView>();
	public bool TimerStarted = false;
	public static float WarStartTime = 0;


    public override void InitializeMainGameRoot(MainGameRootViewModel viewModel) {
        base.InitializeMainGameRoot(viewModel);
        // This is called when a MainGameRootViewModel is created
    }

    public override void GoToMenu(MainGameRootViewModel viewModel) {
        base.GoToMenu(viewModel);
    }

    public override void Play(MainGameRootViewModel viewModel) {
        base.Play(viewModel);
    }

    public override void GameOver(MainGameRootViewModel viewModel) {
        base.GameOver(viewModel);
    }


	public void StartBattle(EntityViewModel P1, EntityViewModel P2, EntityView P1v, EntityView P2v){
		Debug.Log("Running StartBattle");

		soldiers.Insert(0, P1);
		soldiers.Insert(1, P2);
		soldiersView.Insert (0, P1v);
		soldiersView.Insert (1, P2v);
		soldiers[0].Opponent = soldiers[1];
		soldiers[1].Opponent = soldiers[0];
		soldiersView[0].OpponentView = soldiersView[1];
		soldiersView[1].OpponentView = soldiersView[0];
		soldiers[0].WarTimeLimitInSecond = 30;
		soldiers[1].WarTimeLimitInSecond = 30;
		soldiers[0].ElementsPerSecond = soldiers[0].UpdatePerRound * soldiers[1].UpdatePerRound;
		soldiers[1].ElementsPerSecond = soldiers[1].ElementsPerSecond;
		
		for (int i=0; i<soldiers.Count; i++) {
			soldiers[i].GetHealthProbabilities();
			soldiers[i].starttime = Time.time;
		}

		WarStartTime = Time.time;
		TimerStarted = true;
		Observable.EveryUpdate().Subscribe(_ => 
		{
			if (soldiers.Count > 0) 
			{
				for (int i=0; i < soldiers.Count; i++) 
				{
					if (TimerStarted && (Time.time - soldiers[i].starttime >= 1f / soldiers[i].AttackSpeed)) 
					{
						Result (WarStartTime, soldiers[0].Counter, soldiers[i], soldiersView[i]);
						soldiers[i].starttime = Time.time;
					}
					else if (!TimerStarted)
					{
						//Debug.Log (soldiers[i] + "is null");
						//soldiers[i] = null;
					}
				}
			}
		});
	}

	public void Result(float warStartTime, int roundCounter, EntityViewModel p, EntityView pV){
		float factor = 1.0f;
		/*
		if (action == ActionStyle.PIN){
			factor = 0.5f;
		}
		*/
		
		float health = p.Opponent.Health * Mathf.Pow (p.Opponent.noHurt, p.Health / (float)p.Opponent.Health);
		float d = p.Opponent.Health - Mathf.Pow (p.Opponent.noHurt + p.Opponent.hurt, p.Health / (float)p.Opponent.Health) * p.Opponent.Health;
		float ht = p.Opponent.Health - health - d;	
		p.Opponent.Dead = d * factor;
		p.Opponent.Hurt = ht * factor;
		//healthHistory [nextCnt] = (healthHistory [counter] - Mathf.RoundToInt(d  * factor) - Mathf.RoundToInt(ht*factor));
		p.Opponent.Health = (p.Opponent.Health - p.Opponent.Dead - p.Opponent.Hurt) >= 0.5 ? p.Opponent.Health - p.Opponent.Dead - p.Opponent.Hurt : 0;

		pV.AtkAndUpdateHealth();

		//Debug.Log (Name + " Health: " + healthHistory [Counter]);
		string colorTag = p.Name != "Soldier3" ? "<color=red>" :"<color=yellow>";
		colorTag = p.Name == "Soldier4" ? "<color=purple>" : colorTag;
		float timeDiff = Time.time - warStartTime;
		//call animation, but how to call the view???
		Debug.Log (colorTag + "Time</color> " + timeDiff + "s, " + p.Opponent.Name + " Results: " + health + " " + ht + " " + d + " <color=blue>Opponent.Counter:</color> " + p.Counter);
		Debug.Log (colorTag + "Time</color> " + timeDiff + "s, " + p.Opponent.Name + " Actual Results: " + p.Opponent.Health + " " + p.Opponent.Hurt + " " + p.Opponent.Dead + " Total: " + (p.Opponent.Health + p.Opponent.Hurt + p.Opponent.Dead));
		//Debug.Log (colorTag + "Round</color> " + roundCounter + ", " + p.Opponent.Name + " Results: " + health + "   " + ht  + "  " + d  + " <color=blue>Opponent.Counter:</color> " + p.Counter);
		//Debug.Log (colorTag + "Round</color> " + roundCounter + ", " + p.Opponent.Name + " Actual Results: " + p.Opponent.Health + "   " + p.Opponent.Hurt + "  " + p.Opponent.Dead + " Total: " + (p.Opponent.Health + p.Opponent.Hurt + p.Opponent.Dead));

		if (p.Opponent.Health <= 0) {
				TimerStarted = false;
				return;
			}
		p.Counter++;
		}
		
	}

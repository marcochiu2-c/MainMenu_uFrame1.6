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

public class MainGameRootController : MainGameRootControllerBase {
    
	public List<EntityViewModel> soldiers = new List<EntityViewModel>();
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

	public void StartBattle(EntityViewModel P1, EntityViewModel P2){
		Debug.Log("Running StartBattle");

		soldiers.Add (P1);
		soldiers.Add (P2);
		//soldiers.Add (new Soldiers (SetParameterSoldier3 ()));
		//soldiers.Add (new Soldiers (SetParameterSoldier4 ()));
		soldiers[0].Opponent = soldiers[1];
		soldiers[1].Opponent = soldiers[0];
		//soldiers [2].Opponent = soldiers [1];
		//soldiers [3].Opponent = soldiers [1];
		soldiers[0].Action = ActionStyle.PIN;
		//soldiers [2].action = ActionStyle.PIN;
		//soldiers [3].action = ActionStyle.PIN;
		soldiers[0].WarTimeLimitInSecond = 30;
		soldiers[1].WarTimeLimitInSecond = 30;
		soldiers[0].ElementsPerSecond = soldiers[0].UpdatePerRound * soldiers[1].UpdatePerRound;
		soldiers[1].ElementsPerSecond = soldiers[1].ElementsPerSecond;
		
		for (int i=0; i<soldiers.Count; i++) {
			soldiers[i].GetHealthProbabilities ();
			soldiers[i].starttime = Time.time;
		}
		WarStartTime = Time.time;
		TimerStarted = true;
		Observable.EveryUpdate().Subscribe(_ => 
		{
			if (soldiers.Count > 0) {
				for (int i=0; i < soldiers.Count; i++) {
					if (TimerStarted && (Time.time - soldiers[i].starttime >= 1f / soldiers[i].AttackSpeed)) {
						Result (WarStartTime, soldiers[0].Counter, soldiers[i]);
						soldiers[i].starttime = Time.time;
					}
				}
			}
			
			if (Time.time > 60)
				soldiers = null;
		});
	}

	public void Result(float warStartTime, int roundCounter, EntityViewModel p){
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

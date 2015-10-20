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
    
	//public List<EntityViewModel> soldiers = new List<EntityViewModel>();
	//public List<EntityView> soldiersView = new List<EntityView>();
	private bool TimerStarted = false;
	private bool _battleFinished = false;
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


	public void StartBattle(EntityViewModel P1, EntityViewModel P2, EntityView P1v, EntityView P2v)
	{
		Debug.Log("Running StartBattle");

		List<EntityViewModel> soldiers = new List<EntityViewModel>();
		List<EntityView> soldiersView = new List<EntityView>();

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
		soldiers[0].Opponent = soldiers[1];
		soldiers[1].Opponent = soldiers[0];
		soldiersView[0].OpponentView = soldiersView[1];
		soldiersView[1].OpponentView = soldiersView[0];


		for (int i = 0; i < soldiers.Count; i++)
		{
			Debug.Log ("soldiers.Count: " + soldiers.Count);
			soldiers[i].GetHealthProbabilities();
			soldiers[i].starttime = Time.time;
		}

		WarStartTime = Time.time;
		soldiers[0].TimeStarted = true;
		soldiers[1].TimeStarted = true;
		//if(soldiers[0] != null)
		//	soldiers[0].TimeStarted = true;
		//if(soldiers[1] != null)
		//	soldiers[1].TimeStarted = true;
		_battleFinished = false;

		Observable.EveryUpdate().Where (_ => soldiers[0].TimeStarted == true && soldiers[1].TimeStarted).Subscribe(_ => 
		{
			if (soldiers.Count > 0) 
			{
				for (int i=0; i < soldiers.Count; i++) 
				{
					if (soldiers[i].TimeStarted && (Time.time - soldiers[i].starttime >= 1f / soldiers[i].AttackSpeed)) 
					{
						Result (WarStartTime, soldiers[i], soldiersView[i]);
						soldiers[i].starttime = Time.time;
					}
					else if (!soldiers[i].TimeStarted)
					{
						_battleFinished = true;
						soldiers.Clear ();
					}
				}
			}
		});

		//if(_battleFinished ) soldiers.Clear ();
	}

	public void Result(float warStartTime, EntityViewModel p, EntityView pV){
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

		//if(p.Opponent.Health > 0)
			pV.AtkAndUpdateHealth();

		if (p.Opponent.Health <= 0) 
		{
			p.TimeStarted = false;
			return;
		}
		//TimerStarted = false;

		//Debug.Log (Name + " Health: " + healthHistory [Counter]);
		string colorTag = p.Name != "Soldier3" ? "<color=red>" :"<color=yellow>";
		colorTag = p.Name == "Soldier4" ? "<color=purple>" : colorTag;
		float timeDiff = Time.time - warStartTime;
		//call animation, but how to call the view???
		Debug.Log (colorTag + "Time</color> " + timeDiff + "s, " + p.Opponent.Name + " Results: " + health + " " + ht + " " + d + " <color=blue>Opponent.Counter:</color> " + p.Counter);
		Debug.Log (colorTag + "Time</color> " + timeDiff + "s, " + p.Opponent.Name + " Actual Results: " + p.Opponent.Health + " " + p.Opponent.Hurt + " " + p.Opponent.Dead + " Total: " + (p.Opponent.Health + p.Opponent.Hurt + p.Opponent.Dead));
		//Debug.Log (colorTag + "Round</color> " + roundCounter + ", " + p.Opponent.Name + " Results: " + health + "   " + ht  + "  " + d  + " <color=blue>Opponent.Counter:</color> " + p.Counter);
		//Debug.Log (colorTag + "Round</color> " + roundCounter + ", " + p.Opponent.Name + " Actual Results: " + p.Opponent.Health + "   " + p.Opponent.Hurt + "  " + p.Opponent.Dead + " Total: " + (p.Opponent.Health + p.Opponent.Hurt + p.Opponent.Dead));
		p.Counter++;
		}
		
	}

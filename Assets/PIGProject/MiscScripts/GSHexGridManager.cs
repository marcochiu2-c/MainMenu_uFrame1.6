﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Gamelogic;
using Gamelogic.Grids;
using Gamelogic.Grids.GoldenSkull;
using Gamelogic.Grids.Examples;
using DG.Tweening;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using uFrame.MVVM;
using uFrame.Serialization;


public class GSHexGridManager : uFrameGridBehaviour<FlatHexPoint> {

	//List for Entity ViewModel and View
	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	public List<EnemyViewModel> TargetVM = new List<EnemyViewModel>();
	public List<EnemyView> TargetV = new List<EnemyView>();
	public List<SoldierView> SoldierV = new List<SoldierView>();

	public SoldierViewModel SelectedSodlierVM;
	
	public MainGameRootController MainGameController;
	
	public SpriteCell pathPrefab;
	public GameObject pathRoot;
	public Text myText;
	
	private FlatHexPoint start;
	private FlatHexPoint finish;
	private Vector3 tempPoint;
	private int step = 0;
	private int _sNum = 0;	//index for SoldierVM and SoldierV List
	private bool _targetSelected = false;
	private bool[] _movefinish = new bool[] {false, false, false, false, false};
	private FlatHexGrid<GSCell> walkableGrid;
	
	
	// Call when Kernel have been loaded
	public override void KernelLoaded()
	{
		base.KernelLoaded();
		
		for (int i = 1; i <= 5; i++)
		{
			// Get the Controllers, ViewModels and Views from Kernel
			MainGameController = uFrameKernel.Container.Resolve<MainGameRootController>();
			SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			TargetVM.Add(uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy" + i));
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.ActualHit() + " and " + SoldierVM.ActualDodge() + " and " + SoldierVM.ActualMorale());
			
			GameObject objEnemy = GameObject.Find("Enemy" + i);
			TargetV.Add (objEnemy.GetComponent<EnemyView>() as EnemyView);
			
			GameObject objSoldier = GameObject.Find("Soldier" + i);
			SoldierV.Add (objSoldier.GetComponent<SoldierView>() as SoldierView);
		}

		//TODO
		walkableGrid = (FlatHexGrid<GSCell>) Grid.CastValues<GSCell, FlatHexPoint>();

		foreach (var point in walkableGrid)
		{
			walkableGrid[point].IsWalkable = true;
		}

		InitPosition();
	}
	
	//Init all Gameobject in the Grid
	public void InitPosition()
	{	
		
		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierVM[i].CurrentPointLocation = new FlatHexPoint(i, 0);
			//walkableGrid[SoldierVM[i].CurrentPointLocation].IsWalkable = false;
			SoldierV[i].transform.position = Map[SoldierVM[i].CurrentPointLocation];
		}
		//Random the Enemy position later
		TargetVM[0].CurrentPointLocation = new FlatHexPoint(0, 7);
		TargetVM[1].CurrentPointLocation = new FlatHexPoint(10, 5);
		TargetVM[2].CurrentPointLocation = new FlatHexPoint(9, 0);
		TargetVM[3].CurrentPointLocation = new FlatHexPoint(2, 9);
		TargetVM[4].CurrentPointLocation = new FlatHexPoint(6, 6);
		
		for(int i = 0; i < TargetV.Count; i++)
			TargetV[i].transform.position = Map[TargetVM[i].CurrentPointLocation];
		
		start = SoldierVM[_sNum].CurrentPointLocation;
	}


	//Call once a point has been clicked
	public void OnClick(FlatHexPoint point)
	{	

		for(int i=0; i < SoldierVM.Count; i++)
		{
			//TODO
			if(point == SoldierVM[i].CurrentPointLocation)
			{
				//_sNum = i;
				Debug.Log ("Soldier Selected");
			}
		}
		
		if(SoldierV[_sNum] != null)
		{	
			//Clicked on Move State
			if(SoldierVM[_sNum].SoldierState == SoldierState.MOVE)
			{
				//Algorithm from Path Finding and update the Point from Soldier
				start = SoldierVM[_sNum].CurrentPointLocation;
				finish = point;
				SoldierVM[_sNum].CurrentPointLocation = point;
				var path = Algorithms.AStar(Grid, start, finish);
				StartCoroutine(MovePath(path, SoldierVM[_sNum].Movement, SoldierV[_sNum]));

				//Change state from move to attack after move
				SoldierVM[_sNum].SoldierState = SoldierState.ATTACK;
			}

			//Clicked on attack State
			else if (SoldierVM[_sNum].SoldierState == SoldierState.ATTACK)
			{
				//Range of Attack:
				//Swordman < 2
				//Archer < 3
				var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[_sNum].CurrentPointLocation) < 2);

				switch (SoldierVM[_sNum].Career)
				{
				case Career.Swordman:
					neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[_sNum].CurrentPointLocation) < 2);
					break;
				case Career.Archer:
					neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[_sNum].CurrentPointLocation) < 3);
					break;
				default:
					neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[_sNum].CurrentPointLocation) < 2);
					break;
				}

				
				foreach (var neighbor in neighborhoodPoints)
				{
					for(int j = 0; j < TargetVM.Count; j++)
					{
						if(neighbor == point && neighbor == TargetVM[j].CurrentPointLocation)
						{
							//FindNearly cell, get the target
							//if point is one of neighbour, target = pointtarget
							Debug.Log ("Target Selected");
							myText.text = "Target Selected";

							//Save the information into playlist
							SoldierVM[_sNum].playlist.Insert((int)SoldierVM[_sNum].Counter, new PlayList(SoldierVM[_sNum].CurrentPointLocation, SoldierVM[_sNum].Movement ,SoldierVM[_sNum].Action, TargetVM[j], TargetV[j]));

							SoldierVM[_sNum].Counter++;
							_targetSelected = true;
							goto EndofAttack; //break the foreach
						}
					}
				}
				
			EndofAttack:
					if(!_targetSelected)
				{
					Debug.Log ("You can't Attack");
					myText.text = "You can't Attack, Please Move";
					//Save the information into playlist without target
					SoldierVM[_sNum].playlist.Insert((int)SoldierVM[_sNum].Counter, new PlayList(SoldierVM[_sNum].CurrentPointLocation, SoldierVM[_sNum].Movement ,SoldierVM[_sNum].Action, null, null));
					SoldierVM[_sNum].Counter++;
				}
				//Change state from move to move after attack
				SoldierVM[_sNum].SoldierState = SoldierState.MOVE;
			}//End of ATTACK State
		}
		_targetSelected = false;
	}


	public IEnumerator MovePath(IEnumerable<FlatHexPoint> path, MoveStyle move, EntityView entityView)
	{
		var pathList = path.ToList();
		
		if(pathList.Count < 2) yield break; //Not a valid path
		
		//yield return new WaitForSeconds(0.2f);
		foreach (var point in path)
		{
			var pathNode = Instantiate(pathPrefab);

			pathNode.transform.parent = pathRoot.transform;
			pathNode.transform.localScale = Vector3.one * 0.3f;
			pathNode.transform.localPosition = Map[point];

			//show the path with circle
			if (point == start)
			{
				//pathNode.Color
				pathNode.Color = ExampleUtils.Colors[1];
			}
			else if (point == finish)
			{
				pathNode.Color = ExampleUtils.Colors[3];
			}
			else
			{
				pathNode.Color = ExampleUtils.Colors[2];
			}
			
		}
		
		for(int i = 0; i < pathList.Count - 1; i++)
		{
			yield return StartCoroutine(entityView.Move(Map[pathList[i]], Map[pathList[i+1]], move));
		}
	}

	public void EndTurn()
	{
		SoldierV[_sNum].RendererColor(Color.grey);
		_sNum++;
		int num = _sNum + 1;
		Debug.Log ("Next Player");
		myText.text = ("Please Move Soldier" + num);
	}
	

	public void PlayBtn()
	{
		pathRoot.SetActive(false);
		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierV[i].RendererColor(Color.white);
			StartCoroutine(PlayPlayList(i));
		}
	}

	public IEnumerator PlayPlayList(int i)
	{
		for(int x = 0; x < SoldierVM.Count; x++)
		{
			SoldierVM[x].CurrentPointLocation = new FlatHexPoint(x, 0);
			SoldierV[x].transform.position = Map[SoldierVM[x].CurrentPointLocation];
			start = SoldierVM[i].CurrentPointLocation;
		}

		for(int j = 0; j < SoldierVM[i].playlist.Count; j++)
		{
			//Move State
			if(j==0)
				start = SoldierVM[i].CurrentPointLocation;
			else
				start = SoldierVM[i].playlist[j-1].SavePointLocation;
			
			finish = SoldierVM[i].playlist[j].SavePointLocation;
			SoldierVM[i].CurrentPointLocation = SoldierVM[i].playlist[j].SavePointLocation;
			
			PathFinding(start, finish, SoldierVM[i].playlist[j].SaveMove, SoldierV[i]);

			//if Feint, let the enemy follow the corresponding soldier, may be need to check 
			if( j != 0 && SoldierVM[i] != null && SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
			{
				if(SoldierVM[i].playlist[j-1].SaveEnemyVM != null && SoldierVM[i].playlist[j-1].SaveEnemyVM.BattleState == BattleState.WAITING)
				{
					var eStart = SoldierVM[i].playlist[j-1].SaveEnemyVM.CurrentPointLocation;
					if(SoldierVM[i].playlist[j-1].SavePointLocation != null)
					{
						var eFinish = finish - new FlatHexPoint(0, 1);
						SoldierVM[i].playlist[j-1].SaveEnemyVM.CurrentPointLocation = eFinish;
						PathFinding(eStart, eFinish, SoldierVM[i].playlist[j].SaveMove, SoldierVM[i].playlist[j-1].SaveEnemyView);
					}
				}
			}

			//TODO: Waiting the move state finish
			yield return new WaitForSeconds(0.5f);
			//if(_movefinish[i] == false)
			//	yield return new WaitForSeconds(0.1f);
			
			//Attack state
			//use this check attack style and check the logic!!!
			if(SoldierVM[i].playlist[j].SaveEnemyVM !=null)
			{
				SoldierVM[i].playlist[j].SaveEnemyVM.PlayList = SoldierVM[i].playlist[j];
				MainGameController.StartBattle(SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM, SoldierV[i], SoldierVM[i].playlist[j].SaveEnemyView, SoldierVM[i].playlist[j].SaveAction); 
				//this check maybe not good, please find another way to repalce it
				//while(SoldierVM[i].playlist[j].SaveEnemyVM.Health > 0 && SoldierVM[i].Health > 0 && SoldierVM[i].TimeStarted == true)
				while(SoldierVM[i].BattleState != BattleState.WAITING)
				{
					//Debug.Log ("Wating finish Battle");
					//yield return new WaitForSeconds(SoldierVM[0].AttackSpeed >= SoldierVM[0].Opponent.AttackSpeed ? 1/SoldierVM[0].AttackSpeed : 1/SoldierVM[0].Opponent.AttackSpeed);
					yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
				}
			}

			//Enemy Logic
			yield return new WaitForSeconds(0.5f);
		}
	}

	public bool IsCellAccessible(GSCell cell)
	{
		return cell.IsWalkable;
	}


	public void PathFinding(FlatHexPoint s,  FlatHexPoint f, MoveStyle moveStyle, EntityView entityView)
	{

		var path = Algorithms.AStar(
			Grid,
			s,
			f);

		/*
		var path = Algorithms.AStar2(
			Grid,
			s,
			f,
			(p,q) => p.DistanceFrom(q),
			c => true, //accessing your custom bool from the GSCell
			1);
		*/

		if (path == null) return; //then there is no path between the start and goal.
		StartCoroutine(MovePath(path, moveStyle, entityView));
	}

	//This is the distance function used by the path algorithm.
	private float EuclideanDistance(FlatHexPoint p, FlatHexPoint q)
	{
		float distance = (Map[p] - Map[q]).magnitude;
		
		return distance;
	}
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Gamelogic;
using Gamelogic.Grids;
using DG.Tweening;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using uFrame.MVVM;
using uFrame.Serialization;
using uFrame.MVVM;


public class GSHexGridManager : uFrameGridBehaviour<FlatHexPoint> {

	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	public List<EnemyViewModel> TargetVM = new List<EnemyViewModel>();

	public SoldierViewModel SelectedSodlierVM;

	public List<EnemyView> TargetV = new List<EnemyView>();
	public List<SoldierView> SoldierV = new List<SoldierView>();


	public MainGameRootController MainGameController;
	//public SoldierViewModel TargetVM;
	//public SoldierView TargetView;

	public SpriteCell pathPrefab;
	public GameObject pathRoot;
	public Text myText;

	private FlatHexPoint start;
	private FlatHexPoint finish;
	private Vector3 tempPoint;
	private int step = 0;
	private int _sNum = 0;
	private bool _targetSelected = false;


	//
	public override void KernelLoaded()
	{
		base.KernelLoaded();

		for (int i = 1; i <= 5; i++)
		{
			SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			TargetVM.Add(uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy" + i));
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.ActualHit() + " and " + SoldierVM.ActualDodge() + " and " + SoldierVM.ActualMorale());

			GameObject objEnemy = GameObject.Find("Enemy" + i);
			TargetV.Add (objEnemy.GetComponent<EnemyView>() as EnemyView);

			GameObject objSoldier = GameObject.Find("Soldier" + i);
			SoldierV.Add (objSoldier.GetComponent<SoldierView>() as SoldierView);
			//EnemyView temp = GameObject.Find("Enemy" + i) as EnemyView;
			//TargetV.Add(temp);
		}

		MainGameController = uFrameKernel.Container.Resolve<MainGameRootController>();
		//Debug.Log (MainGameController == null ? "MainGameController is null" : "MainGameController is here" );

		InitPosition();
	}

	//Init all Gameobject in the Grid
	public void InitPosition()
	{	

		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierVM[i].CurrentPointLocation = new FlatHexPoint(i, 0);
			SoldierV[i].transform.position = Map[SoldierVM[i].CurrentPointLocation];
		}
		//Random position later
		TargetVM[0].CurrentPointLocation = new FlatHexPoint(0, 4);
		TargetVM[1].CurrentPointLocation = new FlatHexPoint(9, 3);
		TargetVM[2].CurrentPointLocation = new FlatHexPoint(2, 5);
		TargetVM[3].CurrentPointLocation = new FlatHexPoint(0, 6);
		TargetVM[4].CurrentPointLocation = new FlatHexPoint(5, 4);

		for(int i = 0; i < TargetV.Count; i++)
			TargetV[i].transform.position = Map[TargetVM[i].CurrentPointLocation];

		start = SoldierVM[_sNum].CurrentPointLocation;
	}

	public void OnClick(FlatHexPoint point)
	{	
		//Debug.Log (point.BasePoint);   //return (x,y)
		for(int i=0; i < SoldierVM.Count; i++)
		{
			if(point == SoldierVM[i].CurrentPointLocation)
			{
				//_sNum = i;
				Debug.Log ("Soldier Selected");
			}
		}

		if(SoldierV[_sNum] != null)
		{	
			if(SoldierVM[_sNum].SoldierState == SoldierState.MOVE)
			{

				Debug.Log ("Fuck you from OnClick > Move");
				start = SoldierVM[_sNum].CurrentPointLocation;
				finish = point;
				SoldierVM[_sNum].CurrentPointLocation = point;
				var path = Algorithms.AStar(Grid, start, finish);
				
				StartCoroutine(MovePath(path, SoldierVM[_sNum].Movement, _sNum));
				SoldierVM[_sNum].SoldierState = SoldierState.ATTACK;
			}
			
			else if (SoldierVM[_sNum].SoldierState == SoldierState.ATTACK)
			{
				// (target.CurrentPointLocation == point && in Grid.GetAllNeighbors(player))
				foreach (var neighbor in Grid.GetAllNeighbors(SoldierVM[_sNum].CurrentPointLocation))
				{
					for(int j = 0; j < TargetVM.Count; j++)
					{
						if(neighbor == point && neighbor == TargetVM[j].CurrentPointLocation)
					//if(neighbor == point && neighbor == TargetVM[0].CurrentPointLocation)
						{
							//FindNearly cell, get the target
						//if point is one of neighbour, target = pointtarget
							Debug.Log ("Target Selected");
							myText.text = "Target Selected";
							//Call GameRoot or cal
							SoldierVM[_sNum].playlist.Insert((int)SoldierVM[_sNum].Counter, new PlayList(SoldierVM[_sNum].CurrentPointLocation, SoldierVM[_sNum].Movement ,SoldierVM[_sNum].Action, TargetVM[j], TargetV[j]));
							//MainGameController.StartBattle(SoldierVM[0], TargetVM[i], SoldierView, TargetV[i]);
							//StartCoroutine(Battle (SoldierVM, TargetVM, SoldierView, TargetView));
							SoldierVM[_sNum].Counter++;
							_targetSelected = true;
							goto EndofAttack;
						}
					}
				}

			EndofAttack:
				if(!_targetSelected)
				{
					Debug.Log ("You can't Attack");
					myText.text = "You can't Attack, Please Move";
					SoldierVM[_sNum].playlist.Insert((int)SoldierVM[_sNum].Counter, new PlayList(SoldierVM[_sNum].CurrentPointLocation, SoldierVM[_sNum].Movement ,SoldierVM[_sNum].Action, null, null));
					SoldierVM[_sNum].Counter++;
				}
			
				SoldierVM[_sNum].SoldierState = SoldierState.MOVE;
			}//End of ATTACK State
		}
		_targetSelected = false;
	}

	public IEnumerator MovePath(IEnumerable<FlatHexPoint> path, MoveStyle move, int SoldierNum)
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
			
			if (point == start)
			{
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
			yield return StartCoroutine(SoldierV[SoldierNum].Move(Map[pathList[i]], Map[pathList[i+1]], move));
		}
	}

	public void PlayBtn()
	{
		for(int i = 0; i < SoldierVM.Count; i++)
			StartCoroutine(PlayPlayList(i));
	}

	public void EndTurn()
	{
		_sNum++;
		int num = _sNum + 1;
		Debug.Log ("Next Player");
		myText.text = ("Please Move Soldier" + num);
	}

	//for Soldiervm[0] ONLY for now
	public IEnumerator PlayPlayList(int i)
	{
		for(int x = 0; x < SoldierVM.Count; x++)
		{
			SoldierVM[x].CurrentPointLocation = new FlatHexPoint(x, 0);
			SoldierV[x].transform.position = Map[SoldierVM[x].CurrentPointLocation];
			start = SoldierVM[i].CurrentPointLocation;
		}

		yield return new WaitForSeconds(0.5f);

		//for(int i = 0; i < SoldierVM.Count; i++)
		//{
			for(int j = 0; j < SoldierVM[i].playlist.Count; j++)
			{
				//Move
				if(j==0)
					start = SoldierVM[i].CurrentPointLocation;
				else
					start = SoldierVM[i].playlist[j-1].SavePointLocation;

				finish = SoldierVM[i].playlist[j].SavePointLocation;
				SoldierVM[i].CurrentPointLocation = SoldierVM[i].playlist[j].SavePointLocation;
				var path = Algorithms.AStar(Grid, start, finish);
				yield return StartCoroutine(MovePath(path, SoldierVM[i].playlist[j].SaveMove, i));

				yield return new WaitForSeconds(0.2f);


				//Attack
			if(SoldierVM[i].playlist[j].SaveEnemyVM !=null)
			{
				MainGameController.StartBattle(SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM, SoldierV[i], SoldierVM[i].playlist[j].SaveEnemyView); 

			//this check maybe not good, please find another way to repalce it
				while(SoldierVM[i].playlist[j].SaveEnemyVM.Health > 0 && SoldierVM[i].Health > 0)
				{
					//Debug.Log ("Wating finish Battle");
					//yield return new WaitForSeconds(SoldierVM[0].AttackSpeed >= SoldierVM[0].Opponent.AttackSpeed ? 1/SoldierVM[0].AttackSpeed : 1/SoldierVM[0].Opponent.AttackSpeed);
					yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
				}
			}

				yield return new WaitForSeconds(0.5f);
			}

		yield break;
		//}
	}

	/*
	public IEnumerator Battle(SoldierViewModel p1, EnemyViewModel p2, SoldierView p1v, EnemyView p2v)
	{
		//separte them finally, need two tweener
		Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.Movement + " and " + SoldierVM.Health + " and " + SoldierVM.Action);
		while(p1.Health >= p2.Power && p2.Health >= p1.Power){
			
			p1.Health -=  p2.Power;
			if(p1.Health <= 0) p1.Health = 0;
			
			p2.Health -=  p1.Power;
			if(p2.Health <= 0) p2.Health = 0;

			//p2v.UpdateHealth(p2.Health);

			p1v.transform.DOPunchPosition(Vector3.right *100, 0.1f, 100, 1f, false).OnComplete(() => {
				p2v.transform.DOPunchPosition(Vector3.right *100, 0.1f, 100, 1f, false);
				//p1v.UpdateHealth(p1.Health);
			});
			myText.text =  "Your Quantity: " + p1.Health + " Target Quantity: " + p2.Health;
			//Debug.Log("Player Quantity: " + p1._Quantity);
			//Debug.Log("Target Quantity: " + p2._Quantity);
			
			yield return new WaitForSeconds(1);
		}
		
		SoldierVM.SoldierState = SoldierState.MOVE;
		myText.text = "You are dead !";
	}
	*/

	public static class ExampleUtils
	{
		public static Color ColorFromInt(int r, int g, int b)
		{
			return new Color(r/255.0f, g/255.0f, b/255.0f, 0.5f);
		}
		
		public static Color ColorFromInt(int r, int g, int b, int a)
		{
			return new Color(r/255.0f, g/255.0f, b/255.0f, a/255.0f);
		}
		
		public static Color[] Colors =
		{
			ColorFromInt(133, 219, 233),
			ColorFromInt(198, 224, 34),
			ColorFromInt(255, 215, 87),
			ColorFromInt(228, 120, 129),
			
			ColorFromInt(42, 192, 217),
			ColorFromInt(114, 197, 29),
			ColorFromInt(247, 188, 0),
			ColorFromInt(215, 55, 82),
			
			ColorFromInt(205, 240, 246),
			ColorFromInt(229, 242, 154),
			ColorFromInt(255, 241, 153),
			ColorFromInt(240, 182, 187),
			
			ColorFromInt(235, 249, 252),
			ColorFromInt(241, 249, 204),
			ColorFromInt(255, 252, 193),
			ColorFromInt(247, 222, 217),
			
			Color.black
		};
	}

}

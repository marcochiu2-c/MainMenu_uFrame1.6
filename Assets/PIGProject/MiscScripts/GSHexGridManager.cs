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


public class GSHexGridManager : uFrameGridBehaviour<FlatHexPoint> {

	//public SoldierViewModel SoldierVM;
	public SoldierView SoldierView; 

	//public EnemyViewModel TargetVM;
	//public EnemyView TargetView1;
	//public EnemyView TargetView2;
	//public EnemyView TargetView3;
	//public EnemyView TargetView4;
	//public EnemyView TargetView5;

	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	public List<EnemyViewModel> TargetVM = new List<EnemyViewModel>();

	public List<EnemyView> TargetV = new List<EnemyView>();

	public MainGameRootController MainGameController;
	
	//public SoldierViewModel TargetVM;
	//public SoldierView TargetView;

	public SpriteCell pathPrefab;
	public GameObject pathRoot;
	public Text myText;

	private FlatHexPoint start;
	private FlatHexPoint finish;
	private Vector3 tempPoint;


	//
	public override void KernelLoaded()
	{
		base.KernelLoaded();
		SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier"));
		Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.ActualHit() + " and " + SoldierVM.ActualDodge() + " and " + SoldierVM.ActualMorale());

		for (int i = 1; i <= 5; i++)
			TargetVM.Add(uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy" + i));

		for (int i = 1; i <= 5; i++)
		{

			GameObject obj = GameObject.Find("Enemy" + i);
			TargetV.Add (obj.GetComponent<EnemyView>() as EnemyView);
			//EnemyView temp = GameObject.Find("Enemy" + i) as EnemyView;
			//TargetV.Add(temp);
		}
		/*
		TargetVM.Add(uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy"));
		TargetVM.Add(uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy2"));
		Debug.Log (TargetVM[0] == null ? "TargetVM is null" : "TargetVM is here");
		Debug.Log (TargetVM[1] == null ? "TargetVM is null" : "TargetVM is here");
		*/

		MainGameController = uFrameKernel.Container.Resolve<MainGameRootController>();
		Debug.Log (MainGameController == null ? "MainGameController is null" : "MainGameController is here" );
		//TargetVM = uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier2");
		//Debug.Log (TargetVM == null ? "TargetVM is null" : TargetVM.Movement + " and " + TargetVM.Quantity + " and " + TargetVM.Action);

		InitPosition();
	}

	//Init all Gameobject in the Grid
	public void InitPosition()
	{	

		SoldierVM[0].CurrentPointLocation = FlatHexPoint.Zero;
		SoldierView.transform.position = Map[FlatHexPoint.Zero];
		start = SoldierVM[0].CurrentPointLocation;

		//TargetView1.transform.position = Map[new FlatHexPoint(7, 4)];
		//TargetView2.transform.position = Map[new FlatHexPoint(8, 0)];

		TargetVM[0].CurrentPointLocation = new FlatHexPoint(7, 4);
		TargetVM[1].CurrentPointLocation = new FlatHexPoint(8, 2);
		TargetVM[2].CurrentPointLocation = new FlatHexPoint(2, 5);
		TargetVM[3].CurrentPointLocation = new FlatHexPoint(0, 6);
		TargetVM[4].CurrentPointLocation = new FlatHexPoint(5, 4);

		for(int i = 0; i < TargetV.Count; i++)
			TargetV[i].transform.position = Map[TargetVM[i].CurrentPointLocation];
	}

	public void OnClick(FlatHexPoint point)
	{	
		//Debug.Log (point.BasePoint);   //return (x,y)
		if(point == SoldierVM[0].CurrentPointLocation)
		{
			//TODO
			Debug.Log ("Selected");
		}
		
		if(SoldierView != null)
		{	
			if(SoldierVM[0].SoldierState == SoldierState.MOVE)
			{
				start = SoldierVM[0].CurrentPointLocation;
				finish = point;
				SoldierVM[0].CurrentPointLocation = point;
				var path = Algorithms.AStar(Grid, start, finish);
				
				StartCoroutine(MovePath(path));
				SoldierVM[0].SoldierState = SoldierState.ATTACK;
			}
			
			else if (SoldierVM[0].SoldierState == SoldierState.ATTACK)
			{
				// (target.CurrentPointLocation == point && in Grid.GetAllNeighbors(player))
				foreach (var neighbor in Grid.GetAllNeighbors(SoldierVM[0].CurrentPointLocation))
				{
					for(int i = 0; i < TargetVM.Count; i++)
					{
						if(neighbor == point && neighbor == TargetVM[i].CurrentPointLocation)
					//if(neighbor == point && neighbor == TargetVM[0].CurrentPointLocation)
						{
							//FindNearly cell, get the target
						//if point is one of neighbour, target = pointtarget
							Debug.Log ("You can Attack");
							//Call GameRoot or cal
							MainGameController.StartBattle(SoldierVM[0], TargetVM[i], SoldierView, TargetV[i]);
							//StartCoroutine(Battle (SoldierVM, TargetVM, SoldierView, TargetView));
							return;
						}
					}
				}
				Debug.Log ("You can't Attack");
				myText.text = "You can't Attack, Please Move";
				SoldierVM[0].SoldierState = SoldierState.MOVE;
			}
		}
	}

	public IEnumerator MovePath(IEnumerable<FlatHexPoint> path)
	{
		var pathList = path.ToList();
		
		if(pathList.Count < 2) yield break; //Not a valid path
		
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
			yield return StartCoroutine(SoldierView.Move(Map[pathList[i]], Map[pathList[i+1]]));
		}
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

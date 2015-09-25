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

	public SoldierViewModel SoldierVM;
	public SoldierView SoldierView; 

	public EnemyViewModel TargetVM;
	public EnemyView TargetView;
	
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
		SoldierVM = uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier");
		Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.Movement + " and " + SoldierVM.Health + " and " + SoldierVM.Action);


		TargetVM  = uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy");
		//Debug.Log (TargetVM == null ? "TargetVM is null" : TargetVM.Quantity);

		//TargetVM = uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier2");
		//Debug.Log (TargetVM == null ? "TargetVM is null" : TargetVM.Movement + " and " + TargetVM.Quantity + " and " + TargetVM.Action);

		InitPosition();
	}

	//Init all Gameobject in the Grid
	public void InitPosition()
	{	
		SoldierView.CurrentPointLocation = FlatHexPoint.Zero;
		SoldierView.transform.position = Map[FlatHexPoint.Zero];
		start = SoldierView.CurrentPointLocation;
		
		TargetView.CurrentPointLocation = new FlatHexPoint(7, 4);
		TargetView.transform.position = Map[new FlatHexPoint(7, 4)];
	}

	public void OnClick(FlatHexPoint point)
	{	
		//Debug.Log (point.BasePoint);   //return (x,y)
		
		if(point == SoldierView.CurrentPointLocation)
		{
			//TODO
			Debug.Log ("Selected");
		}
		
		if(SoldierView != null)
		{	
			if(SoldierVM.SoldierState == SoldierState.MOVE)
			{
				start = SoldierView.CurrentPointLocation;
				finish = point;
				SoldierView.CurrentPointLocation = point;
				var path = Algorithms.AStar(Grid, start, finish);
				
				StartCoroutine(MovePath(path));
				SoldierVM.SoldierState = SoldierState.ATTACK;
			}
			
			else if (SoldierVM.SoldierState == SoldierState.ATTACK)
			{
				
				
				// (target.CurrentPointLocation == point && in Grid.GetAllNeighbors(player))
				foreach (var neighbor in Grid.GetAllNeighbors(SoldierView.CurrentPointLocation))
				{
					if(neighbor == point && neighbor == TargetView.CurrentPointLocation)
					{
						//FindNearly cell, get the target
						//if point is one of neighbour, target = pointtarget
						Debug.Log ("You can Attack");
						StartCoroutine(Battle (SoldierVM, TargetVM, SoldierView, TargetView));
						return;
					}
				}
				Debug.Log ("You can't Attack");
				myText.text = "You can't Attack, Please Move";
				SoldierVM.SoldierState = SoldierState.MOVE;
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

	public IEnumerator Battle(SoldierViewModel p1, EnemyViewModel p2, SoldierView p1v, EnemyView p2v)
	{
		//separte them finally, need two tweener
		Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.Movement + " and " + SoldierVM.Health + " and " + SoldierVM.Action);
		while(p1.Health >= p2.Power && p2.Health >= p1.Power){
			
			p1.Health -=  p2.Power;
			if(p1.Health <= 0) p1.Health = 0;
			
			p2.Health -=  p1.Power;
			if(p2.Health <= 0) p2.Health = 0;

			p2v.UpdateHealth(p2.Health);

			p1v.transform.DOPunchPosition(Vector3.right *100, 0.1f, 100, 1f, false).OnComplete(() => {
				p2v.transform.DOPunchPosition(Vector3.right *100, 0.1f, 100, 1f, false);
				p1v.UpdateHealth(p1.Health);
			});
			myText.text =  "Your Quantity: " + p1.Health + " Target Quantity: " + p2.Health;
			//Debug.Log("Player Quantity: " + p1._Quantity);
			//Debug.Log("Target Quantity: " + p2._Quantity);
			
			yield return new WaitForSeconds(1);
		}
		
		SoldierVM.SoldierState = SoldierState.MOVE;
		myText.text = "You are dead !";
	}
	
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

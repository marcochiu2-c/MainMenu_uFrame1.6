  using System.Collections;
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
using UnityEngine.EventSystems;
using System.Timers;
using Random = UnityEngine.Random;
using Com.LuisPedroFonseca.ProCamera2D;

public class GSHexGridManager : uFrameGridBehaviour<FlatHexPoint> {

	public ProCamera2DCinematics Cinematics;
	public ProCamera2D ProCamera2D;
	/// List for Entity ViewModel and View
	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	public List<EnemyViewModel> TargetVM = new List<EnemyViewModel>();
	public List<EnemyView> TargetV = new List<EnemyView>();
	public List<SoldierView> SoldierV = new List<SoldierView>();
	public SoldierViewModel SelectedSodlierVM;
	
	public MainGameRootController MainGameController;
	public MainGameRootViewModel MainGameVM;

	public Button Play_Btn;
	public Button EndTurn_Btn;
	public Button Restart_Btn;
	public Button NextEnemy_Btn;
	public GameObject InfoPanel;

	public SpriteCell pathPrefab;
	public SpriteCell markNode;
	public GameObject pathRoot;

	public Text myText;
	public bool selectPoint = false;

	public AudioSource audio;
	
	private FlatHexPoint start;
	private FlatHexPoint finish;
	private FlatHexPoint _savePoint;
	private Vector3 tempPoint;
	private int step = 0;
	private int _sNum = 0;	//index for SoldierVM and SoldierV List
	private bool _targetSelected = false;
	private FlatHexGrid<GSCell> walkableGrid;
	private int _soldierCount = 0;
	
	/// Call when Kernel have been loaded
	public override void KernelLoaded()
	{
		base.KernelLoaded();
		
		for (int i = 1; i <= 5; i++)
		{
			// Get the Controllers, ViewModels and Views from Kernel
			MainGameVM = uFrameKernel.Container.Resolve<MainGameRootViewModel>("MainGameRoot");
			//Debug.Log (MainGameVM== null ? "MainGameVM is null" : MainGameVM.Identifier);
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

		walkableGrid = (FlatHexGrid<GSCell>) Grid.CastValues<GSCell, FlatHexPoint>();

		foreach (var point in walkableGrid)
		{
			walkableGrid[point].IsWalkable = true;
		}

		InitPosition();
		
		//Auto Call Onclick for the actionstyle without select target
		Observable.EveryUpdate()
		.Where(_ => SoldierVM[_sNum].SoldierState == SoldierState.ATTACK && (
			       SoldierVM[_sNum].Action == ActionStyle.STANDBY ||
			       SoldierVM[_sNum].Action == ActionStyle.YAWP ||
			       SoldierVM[_sNum].Action == ActionStyle.SEARCH ||
			       SoldierVM[_sNum].Action == ActionStyle.A_ATK))
		.Subscribe(_=> {
		OnClick (SoldierVM[_sNum].CurrentPointLocation);
		});
		
		//BGM
		audio.Play();
		ProCamera2D.enabled = false;
		InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
		StartCoroutine(EnemyShowAnimation());
	}
	
	/// <summary>
	/// Init all Behavior in the Grid
	/// </summary>
	public void InitPosition()
	{	

		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierVM[i].CurrentPointLocation = new FlatHexPoint(i, 0);
			//walkableGrid[SoldierVM[i].CurrentPointLocation].IsWalkable = false;
			SoldierV[i].transform.position = Map[SoldierVM[i].CurrentPointLocation] + new Vector3(0, -18);
		}
		//Random the Enemy position later
		TargetVM[0].CurrentPointLocation = new FlatHexPoint(6, 0);
		TargetVM[1].CurrentPointLocation = new FlatHexPoint(8, 5);
		TargetVM[2].CurrentPointLocation = new FlatHexPoint(11, 1);
		TargetVM[3].CurrentPointLocation = new FlatHexPoint(25, -8);
		TargetVM[4].CurrentPointLocation = new FlatHexPoint(28, -6);
		
		
		TargetV[0].GetComponent<Renderer>().sortingOrder = 6;
		TargetV[0].GetComponent<Renderer>().sortingOrder = 1;
		TargetV[0].GetComponent<Renderer>().sortingOrder = 2;
		TargetV[0].GetComponent<Renderer>().sortingOrder = 5;
		TargetV[0].GetComponent<Renderer>().sortingOrder = 2;
		

		//Set the river to logic false
		walkableGrid[new FlatHexPoint(0, 7)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(0, 8)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(1, 7)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(1, 8)].IsWalkable = false;

		walkableGrid[new FlatHexPoint(3, 3)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(3, 2)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(4, 3)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(4, 2)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(4, 1)].IsWalkable = false;

		walkableGrid[new FlatHexPoint(5, -1)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(6, -2)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(6, -3)].IsWalkable = false;

		walkableGrid[new FlatHexPoint(14, 1)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(14, 0)].IsWalkable = false;

		walkableGrid[new FlatHexPoint(16, -4)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(17, -3)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(17, -4)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(17, -5)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(17, -6)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(18, -4)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(18, -5)].IsWalkable = false;

		walkableGrid[new FlatHexPoint(18, -8)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(18, -9)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(19, -7)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(19, -8)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(19, -9)].IsWalkable = false;

		walkableGrid[new FlatHexPoint(27, -6)].IsWalkable = false;
		walkableGrid[new FlatHexPoint(27, -7)].IsWalkable = false;

		//Set Enemy Touch Logic fasle
		for(int i = 0; i < TargetVM.Count; i++)
			walkableGrid[TargetVM[i].CurrentPointLocation].IsWalkable = false;

		for(int i = 0; i < TargetV.Count; i++)
		{
			TargetV[i].transform.position = Map[TargetVM[i].CurrentPointLocation] + new Vector3(0, -18);
		}
		
		start = SoldierVM[_sNum].CurrentPointLocation;
		
		TargetV[1].gameObject.SetActive(false);
	}


	/// <summary>
	/// EnemyShowAnimatio
	/// </summary>
	public IEnumerator EnemyShowAnimation()
	{
		ProCamera2D.enabled = true;
		
		yield return new WaitForSeconds(0.5f);
		Cinematics.Play();
		
		for(int i = 0; i < 5; i++)
		{
			Cinematics.GoToNextTarget();
			yield return new WaitForSeconds(0.5f);
		}
		Cinematics.Stop();
		ProCamera2D.enabled = false;
		InfoPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutExpo).OnStart(() =>  InfoPanel.SetActive(true));
	}
	
	/// <summary>
	/// Find Enemy
	/// </summary>
	public void FindEnemyBtn()
	{
		ProCamera2D.enabled = true;
		
		if (Cinematics.IsActive)
		{
			Cinematics.Stop();
			NextEnemy_Btn.transform.DOMove(new Vector3(NextEnemy_Btn.transform.position.x - 50, NextEnemy_Btn.transform.position.y), 0.5f).SetEase(Ease.OutSine).OnComplete(() => NextEnemy_Btn.gameObject.SetActive(false));
			
		}
		else
		{
			Cinematics.Play();
			NextEnemy_Btn.transform.DOMove(new Vector3(NextEnemy_Btn.transform.position.x + 50, NextEnemy_Btn.transform.position.y), 0.5f).SetEase(Ease.OutSine).OnStart(() => NextEnemy_Btn.gameObject.SetActive(true));
			
		}
		//Cinematics.GoToNextTarget();		
		//ProCamera2D.enabled = false;
	}
	
	public void NextEnemyBtn()
	{
		//if (Cinematics.IsActive)
			Cinematics.GoToNextTarget();
		//StartCoroutine(FindEnemy());
	}
	
	
	public IEnumerator FindEnemy()
	{
		//ProCamera2D.enabled = true;
		
		
		Cinematics.GoToNextTarget();
		yield return new WaitForSeconds(0.6f);
		//ProCamera2D.enabled = false;
	}
	
	/// <summary>
	/// Call once a point has been clicked
	/// </summary>
	void Update()
	{
		
		
		if(selectPoint && SoldierVM[_sNum].SoldierState == SoldierState.MOVE)
		{
			start = SoldierVM[_sNum].CurrentPointLocation;
			finish = _savePoint;
			SoldierVM[_sNum].CurrentPointLocation = _savePoint;
			PathFinding(start, finish, SoldierVM[_sNum].Movement, SoldierV[_sNum], SoldierVM[_sNum]);
			
			//Change state from move to attack after move
			SoldierVM[_sNum].SoldierState = SoldierState.ATTACK;
		}
		
		if(_soldierCount == 5 && MainGameVM != null)
			{
				MainGameVM.SoldierCount = 0;
				MainGameVM.GameState = GameState.GameOver;
				StopAllCoroutines();
			    _soldierCount = 0;
			}
	}
		
		/*
		//StandBy
		if(SoldierVM[_sNum] != null && SoldierVM[_sNum].Action == ActionStyle.STANDBY)
		{
			myText.text = "OK, Please Move";
			SoldierVM[_sNum].playlist.Insert((int)SoldierVM[_sNum].Counter, new PlayList(SoldierVM[_sNum].CurrentPointLocation, SoldierVM[_sNum].Movement ,SoldierVM[_sNum].Action, null, null));
			SoldierVM[_sNum].Counter++;
		    SoldierVM[_sNum].SoldierState = SoldierState.MOVE;

            _targetSelected = false;
			selectPoint = false;
			
			//OnClick(SoldierVM[_sNum].CurrentPointLocation)
		}
		*/

	/// <summary>
	/// Get the Grid point, 
	/// SoldierState.MOVE -> update the point graphically
	/// SoldierState.ATTACK -> update the playlist
	/// </summary>
	public void OnClick(FlatHexPoint point)
	{
		if(EventSystem.current.IsPointerOverGameObject()) return;
		//{
		
		Touch[] touches = Input.touches;
		
		if (touches.Length >= 2) return;
		
		myText.text = "Please Move";

		ProCamera2D.enabled = false;
		
		if (walkableGrid[point].IsWalkable == false && SoldierVM[_sNum].SoldierState == SoldierState.MOVE)
		{
			myText.text = "Please Select Another Point";
			return;
		}
			
		for(int i=0; i < SoldierVM.Count; i++)
		{
			if(point == SoldierVM[i].CurrentPointLocation)
			{
				//_sNum = i;
				Debug.Log ("Soldier Selected");
				point += new FlatHexPoint(1,0);
			}
		}
		
		if(SoldierV[_sNum] != null)
		{	
			//Clicked on Move State, a red markNode will be created
			if(SoldierVM[_sNum].SoldierState == SoldierState.MOVE)
			{

				if(markNode == null) 
					markNode = Instantiate(pathPrefab);
				
				markNode.transform.parent = pathRoot.transform;
				markNode.transform.localScale = Vector3.one * 0.3f;
				markNode.transform.localPosition = Map[point];
				markNode.Color = ExampleUtils.Colors[3];
				_savePoint = point;
			}
			
			
			
			//Clicked on attack State
			else if (SoldierVM[_sNum].SoldierState == SoldierState.ATTACK)
			{
				
				//No need to select target
				if (SoldierVM[_sNum].Action == ActionStyle.STANDBY || SoldierVM[_sNum].Action == ActionStyle.YAWP || SoldierVM[_sNum].Action == ActionStyle.SEARCH || SoldierVM[_sNum].Action == ActionStyle.A_ATK)
				{
					goto EndofAttack;
				}
				
				
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
							//Debug.Log ("Target Selected");
							
							if(TargetVM[j].Action == ActionStyle.A_ATK || !TargetV[j].gameObject.activeSelf)
								goto EndofAttack;
								
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
					    //Save the information into playlist without target
					    SoldierVM[_sNum].playlist.Insert((int)SoldierVM[_sNum].Counter, new PlayList(SoldierVM[_sNum].CurrentPointLocation, SoldierVM[_sNum].Movement ,SoldierVM[_sNum].Action, null, null));
					    SoldierVM[_sNum].Counter++;
					    
					if (SoldierVM[_sNum].Action == ActionStyle.STANDBY || SoldierVM[_sNum].Action == ActionStyle.YAWP || SoldierVM[_sNum].Action == ActionStyle.SEARCH || SoldierVM[_sNum].Action == ActionStyle.A_ATK)
						myText.text = "OK, Please Move";
					else
						myText.text = "You can't Attack, Please Move";
				   }
				//Change state from move to move after attack and thus check the panel
				SoldierVM[_sNum].SoldierState = SoldierState.MOVE;
			}//End of ATTACK State
		}
		_targetSelected = false;
		selectPoint = false;
		//}
	}


	/// <summary>
	/// Update the position for the related entityView and check in every move
	/// </summary>
	public IEnumerator MovePath(IEnumerable<FlatHexPoint> path, MoveStyle move, EntityView entityView, EntityViewModel entityVM)
	{
		//Debug.Log ("Moving from " + entityView);
		var pathList = path.ToList();
		bool battleFinish = false;

		if(pathList.Count < 2) yield break; //Not a valid path

		entityVM.Moving = true;
			
		Debug.Log (entityView is SoldierView ? "It is SoldierView" : "It is EnemyView");
		
		foreach (var point in path)
		{
			var pathNode = Instantiate(pathPrefab);

			pathNode.transform.parent = pathRoot.transform;
			pathNode.transform.localScale = Vector3.one * 0.3f;
			pathNode.transform.localPosition = Map[point];

			//show the path with circle
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
	
		//if find enemy, attack it first
		//check like walkable -> enemyExist, then call StartBattle
		//run this script if play
		for(int i = 0; i < pathList.Count - 1 ; i++)
		{
			//Check condition if not Feint		
			//if(Play_Btn.interactable == false && entityVM.Sense == SenseStyle.AGGRESSIVE && entityVM.Action != ActionStyle.FEINT)
			//if(true)
			if(Play_Btn.interactable == false)
			{
				var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(pathList[i]) < 2);

				foreach (var neighbor in neighborhoodPoints)
				{
					//Attack Enemy if the enemy is not the feint target or ambush
					if (entityVM is SoldierViewModel)
					{
						for(int j = 0; j < TargetVM.Count; j++)
						{
							//check here enemy feinted by soldier
							if(neighbor == TargetVM[j].CurrentPointLocation)
							{		  
							//TargetVM[j].BattleState = BattleState.FIGHTING;
								if(TargetVM[j].Action != ActionStyle.FEINT && TargetVM[j].Action != ActionStyle.A_ATK && TargetV[j].gameObject.activeSelf)
							    {	
									Debug.Log(TargetVM[j] +  " " + TargetVM[j].Action);
								    MainGameController.StartBattle(TargetVM[j], entityVM , TargetV[j], entityView, ActionStyle.ATTACK);
								    Debug.Log("Enemy Attack when Soldier moving");
							    }
							
							//while(entityVM.BattleState != BattleState.WAITING)
							//yield return new WaitForSeconds(0.5f);
							}
						} // Enf of for loop
					} // End of if Soldier
						
					else if (entityVM is EnemyViewModel)
					{
						for(int j = 0; j < SoldierVM.Count; j++)
					    {
						//check here enemy feinted by soldier?
						    if(neighbor == SoldierVM[j].CurrentPointLocation)
						    {
							    if(SoldierVM[j].Action == ActionStyle.A_ATK)
							    {
								    MainGameController.StartBattle(SoldierVM[j], entityVM , SoldierV[j], entityView, ActionStyle.ATTACK);
								    Debug.Log("Soldier Attack when Enemy moving");
							    }
						   }
						} // Enf of for loop
					} // End of if Enemy
				} // End of foreach
				while(entityVM.BattleState != BattleState.WAITING)
					yield return new WaitForSeconds(0.2f);
			}
			
		//Sorting Order
			if(entityVM.CurrentPointLocation.X == pathList[i+1].X)
			{
				if(entityVM.CurrentPointLocation.Y >= pathList[i].Y)
					entityView.GetComponent<Renderer>().sortingOrder -= 1;
				
				else
					entityView.GetComponent<Renderer>().sortingOrder += 1;
			}
			
			
			else if (entityVM.CurrentPointLocation.X / 2 == pathList[i+1].X / 2)
			{
				//Debug.Log (entityVM.CurrentPointLocation.X / 2 + " and " + pathList[i+1].X / 2);
				if(entityVM.CurrentPointLocation.Y > pathList[i+1].Y)
					entityView.GetComponent<Renderer>().sortingOrder += 1;
			}
			
			else
			{
				if(entityVM.CurrentPointLocation.Y == pathList[i+1].Y)
					entityView.GetComponent<Renderer>().sortingOrder -= 1;
			}
			
			
			yield return StartCoroutine(entityView.Move(Map[pathList[i]], Map[pathList[i+1]], move));
			
			if(entityVM is EnemyViewModel)
			{
				walkableGrid[entityVM.CurrentPointLocation].IsWalkable = true;
				//Debug.Log (entityVM.CurrentPointLocation + "isWalkable: " + walkableGrid[entityVM.CurrentPointLocation].IsWalkable);
			}
			
			entityVM.CurrentPointLocation = pathList[i+1];
			
			if(entityVM is EnemyViewModel)
			{
				walkableGrid[entityVM.CurrentPointLocation].IsWalkable = false;
				//Debug.Log (entityVM.CurrentPointLocation + "isWalkable: " + walkableGrid[entityVM.CurrentPointLocation].IsWalkable);
			}
			while(entityVM.BattleState != BattleState.WAITING)
				yield return new WaitForSeconds(0.5f);
			//Debug.Log ("EntityVM: " + entityVM + "Point: " + entityVM.CurrentPointLocation);	
		}//End for path for loop

		entityVM.Moving = false;
		
		
		//[forEnemy]Check neighhborPoinr whether Soldier is here
		if(Play_Btn.interactable == false && entityVM is EnemyViewModel)
		{
			var neighborhoodEndPoints = Grid.Where(p => p.DistanceFrom(entityVM.CurrentPointLocation) < 2);
		
			foreach (var neighbor in neighborhoodEndPoints)
			{
				for(int j = 0; j < SoldierVM.Count; j++)
				{
					if(neighbor == SoldierVM[j].CurrentPointLocation)
					{
							MainGameController.StartBattle(SoldierVM[j], entityVM , SoldierV[j], entityView, ActionStyle.ATTACK);
							Debug.Log("Soldier Attack when Enemy moving");
					}
				}
			}
			while(entityVM.BattleState != BattleState.WAITING)
				yield return new WaitForSeconds(0.2f);
		}	
	}

/*
	public void CheckNeighborandAttack(FlatHexPoint currentPoint, int range, EntityView entityView, EntityViewModel entityVM)
	{
		var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(currentPoint) < range);
		//var _counter = 0;
		
		foreach (var neighbor in neighborhoodPoints)
		{
			//if (entityView is SoldierView) _counter = SoldierVM.Count;
			//else _counter = TargetVM.Count;
			if (entityView is SoldierView)
				for(int a = 0; a < TargetVM[a].Count; a++)
					//check here enemy feinted by soldier?
					if(neighbor == TargetVM[a].CurrentPointLocation)
				    {
		
					//TargetVM[j].BattleState = BattleState.FIGHTING;
					MainGameController.StartBattle(TargetVM[a], entityVM , TargetV[a], entityView, ActionStyle.ATTACK);
					Debug.Log("Enemy Attack when Soldier moving");
				
					//while(entityVM.BattleState != BattleState.WAITING)
					//yield return new WaitForSeconds(0.5f);
				   }
		}
	}	
*/
	
	/// <summary>
	/// Run the playlist and call StartBattle
	/// </summary>
	public IEnumerator PlayPlayList(int i)
	{
		for(int x = 0; x < SoldierVM.Count; x++)
		{
			SoldierVM[x].CurrentPointLocation = new FlatHexPoint(x, 0);
			SoldierV[x].transform.position = Map[SoldierVM[x].CurrentPointLocation];
			
			SoldierV[x].GetComponent<Renderer>().sortingOrder = 9 - x / 2 ;
			start = SoldierVM[i].CurrentPointLocation;
		}
 
		for(int j = 0; j < SoldierVM[i].playlist.Count; j++)
		{
			///-------------------Move State-----------------///	
			if(j==0)
				start = SoldierVM[i].CurrentPointLocation;
			else
				start = SoldierVM[i].playlist[j-1].SavePointLocation;
			
			finish = SoldierVM[i].playlist[j].SavePointLocation;
			SoldierVM[i].CurrentPointLocation = finish;
			
			//if(j != 0 &&  SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
			//	SoldierVM[i].playlist[j-1].SaveEnemyVM.Action = ActionStyle.FEINT;

			PathFinding(start, finish, SoldierVM[i].playlist[j].SaveMove, SoldierV[i], SoldierVM[i]);
			
			
			///--------------------Attack Style : Feint-----------------///
			SoldierVM[i].Action = SoldierVM[i].playlist[j].SaveAction;
			
			///if Feint, let the enemy follow the corresponding soldier, may be need to check
			if( j >= 1 && SoldierVM[i] != null && SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
			{
				//if(SoldierVM[i].playlist[j-1].SaveEnemyVM != null && SoldierVM[i].playlist[j-1].SaveEnemyVM.BattleState == BattleState.WAITING)
				if(SoldierVM[i].playlist[j-1].SaveEnemyVM != null)
				{
					var eStart = SoldierVM[i].playlist[j-1].SaveEnemyVM.CurrentPointLocation;
					if(SoldierVM[i].playlist[j-1].SavePointLocation != null)
					{
						Debug.Log (i + " Feint: Enemy follow the soldier");
						var eFinish = SoldierVM[i].playlist[j].SavePointLocation + DirectionPoint(SoldierVM[i].playlist[j-1].SavePointLocation, SoldierVM[i].playlist[j-1].SaveEnemyVM.CurrentPointLocation);
						//if eFinish doesn't exist
						//var eFinish = new FlatHexPoint(1 , 0);
						//SoldierVM[i].playlist[j-1].SaveEnemyVM.CurrentPointLocation = eFinish;
						PathFinding(eStart, eFinish, SoldierVM[i].playlist[j].SaveMove, SoldierVM[i].playlist[j-1].SaveEnemyView, SoldierVM[i].playlist[j-1].SaveEnemyVM);
					}
				}
			}

			///--------------------Wait Moving Finish-----------------///
			while (SoldierVM[i].Moving)
			{
				//Debug.Log("Waiting Attack");
				yield return new WaitForSeconds(0.2f);
			}
			
			///--------------------Attack State-------------------------///
			///--------------------Attack Style : A_ATK-----------------///
			if (SoldierVM[i].playlist[j].SaveAction == ActionStyle.A_ATK)
			{
				//SoldierVM[i].CurrentPointLocation = SoldierVM[i].playlist[j+1].SavePointLocation;
				Debug.Log (SoldierVM[i] + " is waiting enemy");
				yield return new WaitForSeconds(2f);
			}
			
			///--------------------Attack Style : YAWP----------------///
			if(SoldierVM[i].playlist[j].SaveAction == ActionStyle.YAWP)
			{
				var yawpNeighborPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[i].playlist[j].SavePointLocation) < 3);

				foreach (var neighbor in yawpNeighborPoints
				)
				{
					for(int x = 0; x < TargetVM.Count; x++)
						//check here enemy feinted by soldier?
						if(neighbor == TargetVM[x].CurrentPointLocation)
						{
							Debug.Log ("YAWP!!!!");
						    myText.text = SoldierVM[i].Name + " YAWP!!!";
						    yield return new WaitForSeconds(0.3f);
						    var finishPoint = SoldierVM[i].playlist[j].SavePointLocation + DirectionPoint(SoldierVM[i].playlist[j].SavePointLocation, TargetVM[x].CurrentPointLocation);
							PathFinding(TargetVM[x].CurrentPointLocation, finishPoint, MoveStyle.FAST, TargetV[x], TargetVM[x]);
							
						   if(!TargetV[x].gameObject.activeSelf)
						   {
							  TargetV[1].gameObject.SetActive(true);
						   }
						    
						   while(TargetVM[x].Moving == true || !TargetV[x].gameObject.activeSelf)
							    yield return new WaitForSeconds(0.2f);
						    
						    MainGameController.StartBattle(SoldierVM[i], TargetVM[x], SoldierV[i], TargetV[x], ActionStyle.ATTACK);
						    
						    while(SoldierVM[i].BattleState != BattleState.WAITING)
						    {
							   //Debug.Log ("Wating finish Battle");
							   yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
						    }
						}
				}
				
			}
			
			///--------------------Attack Style : SEARCH----------------///
			if(SoldierVM[i].playlist[j].SaveAction == ActionStyle.SEARCH)
			{
				var yawpNeighborPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[i].playlist[j].SavePointLocation) < 3);
				
				foreach (var neighbor in yawpNeighborPoints )
				{
					for(int x = 0; x < TargetVM.Count; x++)
						//check here enemy feinted by soldier?
						if(neighbor == TargetVM[x].CurrentPointLocation)
					    {
						   Debug.Log ("YAWP!!!!");
						   myText.text = SoldierVM[i].Name + " Enemy Finds!!!";
						   yield return new WaitForSeconds(0.3f);
						   var finishPoint = TargetVM[x].CurrentPointLocation + DirectionPoint(TargetVM[x].CurrentPointLocation, SoldierVM[i].playlist[j].SavePointLocation);
						   
						   if(!TargetV[x].gameObject.activeSelf)
						   {
							TargetV[1].gameObject.SetActive(true);
						   }
						   //TargetV[x].gameObject.GetComponent<Renderer>().sortingOrder = 12;
						   
						   PathFinding(SoldierVM[i].CurrentPointLocation, finishPoint, MoveStyle.FAST, SoldierV[i], SoldierVM[i]);
						
						   while(TargetVM[x].Moving == true)
							   yield return new WaitForSeconds(0.2f);
						   
						   MainGameController.StartBattle(SoldierVM[i], TargetVM[x], SoldierV[i], TargetV[x], ActionStyle.ATTACK);
						   
						   while(SoldierVM[i].BattleState != BattleState.WAITING)
						   {
							   //Debug.Log ("Wating finish Battle");
							   yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
						   }
					    }
				}
			}
			
			/*
			if( j != 0 && SoldierVM[i].playlist[j].SaveEnemyVM == null && SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
			{
				Debug.Log ("Feint Attack");
				//MainGameController.StartBattle(SoldierVM[i].playlist[j-1].SaveEnemyVM, SoldierVM[i], SoldierVM[i].playlist[j-1].SaveEnemyView, SoldierV[i],  ActionStyle.ATTACK);
			}
			*/
			
			///--------------------Attack Style : Basic Attack - ATTACK and ASSULT and PIN and FEINT----------------///
			if(SoldierVM[i].playlist[j].SaveEnemyVM != null && SoldierVM[i].playlist[j].SaveEnemyVM.Action != ActionStyle.A_ATK && SoldierVM[i].playlist[j].SaveEnemyView.gameObject.activeSelf)
			{	
				SoldierVM[i].playlist[j].SaveEnemyVM.PlayList = SoldierVM[i].playlist[j];
				MainGameController.StartBattle(SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM, SoldierV[i], SoldierVM[i].playlist[j].SaveEnemyView, SoldierVM[i].playlist[j].SaveAction); 
				//this check maybe not good, please find another way to repalce it
				//while(SoldierVM[i].playlist[j].SaveEnemyVM.Health > 0 && SoldierVM[i].Health > 0 && SoldierVM[i].TimeStarted == true)
				
				if (SoldierVM[i].playlist[j].SaveAction == ActionStyle.FEINT)
				{
					SoldierVM[i].playlist[j].SaveEnemyVM.Action = ActionStyle.FEINT;
				}
					
				/*
				if( j != 0 && SoldierVM[i] != null && SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
				{
					Debug.Log ("Feint Attack");
					MainGameController.StartBattle(SoldierVM[i].playlist[j-1].SaveEnemyVM, SoldierVM[i], SoldierVM[i].playlist[j-1].SaveEnemyView, SoldierV[i],  ActionStyle.ATTACK);
				}
				*/

				while(SoldierVM[i].BattleState != BattleState.WAITING)
				{
					//Debug.Log ("Wating finish Battle");
					yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
				}
			}
			
			SoldierVM[i].BattleState = BattleState.WAITING;
			//Enemy Logic
			yield return new WaitForSeconds(0.5f);
		}
		
		//while(SoldierVM[i].BattleState != BattleState.WAITING)
		//	yield return new WaitForSeconds(0.2f);
			
		SoldierVM[i].SoldierState = SoldierState.FINISH;
	    _soldierCount++;
	}
	
	/// <summary>
	/// check the cell is walkable or not
	/// </summary>
	public bool IsCellAccessible(GSCell cell)
	{
		return cell.IsWalkable;
	}
	
	/// <summary>
	/// check direction
	/// </summary>
	public FlatHexPoint DirectionPoint(FlatHexPoint targetPoint, FlatHexPoint currentPoint)
	{
		//NorthEast, North, NorthWest, SouthWest, South, SouthEast
		FlatHexPoint point = currentPoint - targetPoint;
		
		//N, NE, E, S
		if(point.X >= 0)
		{
			if(point.Y > 0)
				return FlatHexPoint.North;
			else if (point.Y == 0)
				return FlatHexPoint.NorthEast;
			else
			{
				if(point.X == 0)
					return FlatHexPoint.South;
				else
				    return FlatHexPoint.SouthEast;
			}
		}
		
		//NW, SW
		else
		{
			if(point.Y > 0)
				return FlatHexPoint.NorthWest;
			else
				return FlatHexPoint.SouthWest;
		}
	}

	public void PathFinding(FlatHexPoint s,  FlatHexPoint f, MoveStyle moveStyle, EntityView entityView, EntityViewModel entityVM)
	{

		//var path = Algorithms.AStar(Grid, s, f);
		var path = Algorithms.AStar(
			Grid,
			s,
			f,
			(p,q) => p.DistanceFrom(q),
			c => ((GSCell) c).IsWalkable, //accessing your custom bool from the GSCell
			(p, q) => 1);

		//Debug.Log(entityView.name + "PathFinding" + path);
		if (path == null) 
		{
			Debug.Log("there is no path between the start and goal.");
			return; //then there is no path between the start and goal.
		}

		StartCoroutine(MovePath(path, moveStyle, entityView, entityVM));
	}

	public void EndTurn()
	{
		SoldierV[_sNum].RendererColor(Color.grey);
		if (_sNum < 4)
			_sNum++;
		else
		{
			myText.text = "Please Click Play Button";
			return;
		}
		
		
		SoldierVM[_sNum].SoldierState = SoldierState.MOVE;
		myText.text = ("Please Move Soldier" + (_sNum + 1));
		
		//Debug.Log (Cinematics.CinematicTargets);
		//Cinematics.GoToNextTarget();
		ProCamera2D.enabled = true;
		ProCamera2D.RemoveCameraTarget(SoldierV[_sNum-1].transform);
		ProCamera2D.AddCameraTarget(SoldierV[_sNum].transform);
	}
	
	
	public void PlayBtn()
	{
		pathRoot.SetActive(false);
		selectPoint = false;
		
		Play_Btn.interactable = false;
		EndTurn_Btn.interactable = false;
		
		StopAllCoroutines();
		//ProCamera2D.enabled = false;
		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierVM[i].SoldierState = SoldierState.PLAY;
			SoldierVM[i].Action = ActionStyle.ATTACK;
			SoldierV[i].RendererColor(Color.white);
			StartCoroutine(PlayPlayList(i));
		}
	}
	
	public void RedoBtn()
	{
		//del the pre. playlist
		if(SoldierVM[_sNum].playlist.Count > 1)
		{
			SoldierVM[_sNum].playlist.RemoveAt(SoldierVM[_sNum].playlist.Count - 1);
			SoldierV[_sNum].transform.position = Map[SoldierVM[_sNum].playlist[SoldierVM[_sNum].playlist.Count - 1].SavePointLocation] + new Vector3(0, -18);
		}
		
		else
		{
			SoldierVM[_sNum].CurrentPointLocation = new FlatHexPoint(_sNum, 0);
			SoldierV[_sNum].transform.position = Map[SoldierVM[_sNum].CurrentPointLocation];
			myText.text = "Not more action redo";
		}
		
	}
	
	//Testing use only
	public void ReStartBtn()
	{
		//Application.LoadLevel(Application.loadedLevel);
		for(int i = 0 ; i < SoldierVM.Count ; i++)
		{
			SoldierVM[i].playlist.Clear();
		}
		
		Application.LoadLevel ("MainGameScene");
	}

	/// This is the distance function used by the path algorithm.
	private float EuclideanDistance(FlatHexPoint p, FlatHexPoint q)
	{
		float distance = (Map[p] - Map[q]).magnitude;
		return distance;
	}
}

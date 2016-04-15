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
	public Button Redo_Btn;
	public Button NextEnemy_Btn;
	public GameObject InfoPanel;
	public GameObject EnemyTower;
	
	public SpriteCell pathPrefab;
	public SpriteCell markNode;
	public GameObject pathRoot;
	
	public Text myText;
	public Text IQDisplay;
	public AudioSource audio;
	public Touch[] touches;
	public int sNum = 0;	//index for SoldierVM and SoldierV List
	public bool selectPoint = false;
	public bool beginner = true;
	
	public FlatHexPoint towerPoint = new FlatHexPoint(29, -5);
	
	private FlatHexPoint start;
	private FlatHexPoint finish;
	private FlatHexPoint _savePoint;
	private Vector3 tempPoint;
	private bool _targetSelected = false;
	private FlatHexGrid<GSCell> walkableGrid;
	private int _soldierCount = 0;  
	private bool _clicking = false;
	private float _touchPosition;
	
	/// Call when Kernel have been loaded
	public override void KernelLoaded()
	{
		base.KernelLoaded();
		
		for (int i = 1; i <= 5; i++)
		{
			// Get the Controllers, ViewModels and Views from Kernel
			SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			TargetVM.Add(uFrameKernel.Container.Resolve<EnemyViewModel>("Enemy" + i));
			//Debug.Log (MainGameVM== null ? "MainGameVM is null" : MainGameVM.Identifier);
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM.ActualHit() + " and " + SoldierVM.ActualDodge() + " and " + SoldierVM.ActualMorale());
			
			GameObject objEnemy = GameObject.Find("Enemy" + i);
			TargetV.Add (objEnemy.GetComponent<EnemyView>() as EnemyView);
			
			GameObject objSoldier = GameObject.Find("Soldier" + i);
			SoldierV.Add (objSoldier.GetComponent<SoldierView>() as SoldierView);
		}
		
		MainGameVM = uFrameKernel.Container.Resolve<MainGameRootViewModel>("MainGameRoot");
		MainGameController = uFrameKernel.Container.Resolve<MainGameRootController>();
		
		//init MainGAmeVM, hard code first
		MainGameVM.PlayerIQ = 200;
		MainGameVM.SoldierCount = 5;
		MainGameVM.EnemyCount = 4;
		
		
		//int winCondition = PlayerPrefs.GetInt("WinCondition");
		int winCondition = 2;
		
		//Temp use
		if (winCondition == 0)
		{
			MainGameVM.WinCondition = WinCondition.Enemies;
			Debug.Log("Change win condition: " + MainGameVM.WinCondition);
		}		
		else if (winCondition == 1)
		{
			MainGameVM.WinCondition = WinCondition.Boss;
			Debug.Log("Change win condition: " + MainGameVM.WinCondition);
		}
		else
		{
			MainGameVM.WinCondition = WinCondition.Tower;
			Debug.Log("Change win condition: " + MainGameVM.WinCondition);
		}
		
		PlayerPrefs.DeleteKey("WinCondition");
		
		MainGameVM.GameState = GameState.Playing;
		
		IQDisplay.text = "Player IQ: " + MainGameVM.PlayerIQ;
		
		walkableGrid = (FlatHexGrid<GSCell>) Grid.CastValues<GSCell, FlatHexPoint>();
		
		foreach (var point in walkableGrid)
		{
			walkableGrid[point].IsWalkable = true;
			walkableGrid[point].IsEnemy = false;
			walkableGrid[point].IsSoldier = false;
		}
		
		InitPosition();
		
		//Auto Call Onclick for the actionstyle without select target
		/*
		Observable.EveryUpdate()
			.Where(_ => SoldierVM[sNum].SoldierState == SoldierState.ATTACK && (
				SoldierVM[sNum].Action == ActionStyle.STANDBY ||
				SoldierVM[sNum].Action == ActionStyle.YAWP ||
				SoldierVM[sNum].Action == ActionStyle.SEARCH ||
				SoldierVM[sNum].Action == ActionStyle.A_ATK))
			.Subscribe(_=> {
				OnClick (SoldierVM[sNum].CurrentPointLocation);
			});
		*/
		
		//BGM
		audio.Play();
		
		ProCamera2D.enabled = false;
		InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
		StartCoroutine(EnemyShowAnimation());
		
		//if(beginner)
		//	BeginnerGuide();
	}
	
	/// <summary>
	/// Init all Behavior in the Grid
	/// </summary>
	public void InitPosition()
	{	
		
		for(int i = 0; i < SoldierVM.Count; i++)
		{
			SoldierVM[i].CurrentPointLocation = new FlatHexPoint(0, 0);
			//walkableGrid[SoldierVM[i].CurrentPointLocation].IsWalkable = false;
			SoldierV[i].transform.position = Map[SoldierVM[i].CurrentPointLocation] + new Vector3(0, -18);
		}
		
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
		
		
		for(int i = 0; i < SoldierVM.Count; i++)
		{
			walkableGrid[SoldierVM[i].CurrentPointLocation].IsSoldier = true;
		}
		
		/// <summary>
		/// Set Enemy Position
		/// case 1: Random all the enemy
		/// case 2: set unique positon
		/// </summary>	
		
		// Random Enemy Position
		if (MainGameVM.WinCondition == null)
		{
			Debug.Log("MainGameVM is null");
		}
		
		else
		{
			Debug.Log("WinCondition: " + MainGameVM.WinCondition + "Player IQ: " + MainGameVM.PlayerIQ);
		}
		
		if (MainGameVM.WinCondition == WinCondition.Enemies)
		{
			for(int i = 0; i <TargetVM.Count; i++)
			{
				//Random Point
				var randomPoint = walkableGrid.RandomItem();
				
				while(!walkableGrid[randomPoint].IsWalkable || walkableGrid[randomPoint].IsEnemy || walkableGrid[randomPoint].IsSoldier)
					randomPoint = walkableGrid.RandomItem();
				
				TargetVM[i].CurrentPointLocation = randomPoint;
			}
			
			//Ambush Enemy2
			TargetV[1].RendererColor(ExampleUtils.Colors[4]);
			TargetV[1].FindChild("HealthBarCanvas").gameObject.SetActive(false);
		}
		
		else if (MainGameVM.WinCondition == WinCondition.Boss)
		{
			Debug.Log("MainGameVM.WinCondition == WinCondition.Boss");
			TargetVM[0].CurrentPointLocation = new FlatHexPoint(26, -6);
			TargetVM[1].CurrentPointLocation = new FlatHexPoint(26, -7);
			TargetVM[2].CurrentPointLocation = new FlatHexPoint(27, -8);
			TargetVM[3].CurrentPointLocation = new FlatHexPoint(28, -8);
			TargetVM[4].CurrentPointLocation = new FlatHexPoint(27, -7);    //Boss
			
			
			TargetV[0].GetComponent<Renderer>().sortingOrder = 2;
			TargetV[1].GetComponent<Renderer>().sortingOrder = 3;
			TargetV[2].GetComponent<Renderer>().sortingOrder = 4;
			TargetV[3].GetComponent<Renderer>().sortingOrder = 3;
			TargetV[4].GetComponent<Renderer>().sortingOrder = 3;
			
			//Ambush Enemy2
			TargetV[1].RendererColor(ExampleUtils.Colors[4]);
			TargetV[1].FindChild("HealthBarCanvas").gameObject.SetActive(false);
			
		}
		
		else if (MainGameVM.WinCondition == WinCondition.Tower)
		{
			//TODO: not confirm the setting yet
			
			EnemyTower.gameObject.SetActive(true);
			TargetVM[0].CurrentPointLocation = new FlatHexPoint(27, -4);
			TargetVM[1].CurrentPointLocation = new FlatHexPoint(29, -6);
			TargetVM[2].CurrentPointLocation = new FlatHexPoint(26, -4);
			TargetVM[3].CurrentPointLocation = new FlatHexPoint(28, -8);
			TargetVM[4].CurrentPointLocation = new FlatHexPoint(29, -9);    //Boss
			
			
			TargetV[0].GetComponent<Renderer>().sortingOrder = 0;
			TargetV[1].GetComponent<Renderer>().sortingOrder = 1;
			TargetV[2].GetComponent<Renderer>().sortingOrder = 1;
			TargetV[3].GetComponent<Renderer>().sortingOrder = 3;
			TargetV[4].GetComponent<Renderer>().sortingOrder = 4;
			
			
			
			TargetV[1].RendererColor(ExampleUtils.Colors[4]);
			TargetV[1].FindChild("HealthBarCanvas").gameObject.SetActive(false);
		}
		
		//set better position
		for(int i = 0; i < TargetV.Count; i++)
		{
			TargetV[i].transform.position = Map[TargetVM[i].CurrentPointLocation] + new Vector3(0, -18);
			walkableGrid[TargetVM[i].CurrentPointLocation].IsEnemy = true;
		}
		
		start = SoldierVM[sNum].CurrentPointLocation;
	}
	
	/// <summary>
	/// EnemyShow Animation in the beginning
	/// </summary>
	public IEnumerator EnemyShowAnimation()
	{
		ProCamera2D.enabled = true;
		
		yield return new WaitForSeconds(0.5f);
		Cinematics.Play();
		
		//yield return new WaitForSeconds(3f);
		
		//Cinematics.GoToNextTarget();
		
		//yield return new WaitForSeconds(2f);
		
		//Cinematics.Stop();
		//ProCamera2D.enabled = false;
		//InfoPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutExpo).OnStart(() =>  InfoPanel.SetActive(true));
	}
	
	/// <summary>
	/// Check Touch.Phase and Gameover
	/// </summary>
	void Update()
	{
		touches = Input.touches;
		
		if (touches.Length >= 2)
		{
			_clicking = false;
		}
		
		else if (touches.Length == 1)
		{
			_touchPosition = touches[0].deltaPosition.magnitude;
			//Debug.Log("touch.deltaPosition: " + _touchPosition);
			
			if(touches[0].phase == TouchPhase.Ended)
				if(touches[0].phase == TouchPhase.Stationary || _touchPosition < 1f || Input.GetMouseButton (0))
			{
				Debug.Log("Clicked");
				_clicking = true;
			}
			else
				_clicking = false;
		}
		
		
		
		if (MainGameVM != null && MainGameVM.WinCondition == WinCondition.Enemies)
		{
			//After all Soldiers Finished their command
			if(_soldierCount == 5 && MainGameVM != null)
			{
				MainGameVM.SoldierCount = 0;
				MainGameVM.GameState = GameState.GameOver;
				StopAllCoroutines();
				_soldierCount = 0;
			}
		}
		
		
	}
	
	//call after mov/attack accommand clicked
	public void MoveOrAttackPointSelected()
	{
		if(MainGameVM.PlayerIQ == 0)
		{
			myText.text = "Not enough IQ";
			return;
		}
		
		else if(selectPoint && SoldierVM[sNum].SoldierState == SoldierState.MOVE)
		{
			start = SoldierVM[sNum].CurrentPointLocation;
			finish = _savePoint;
			SoldierVM[sNum].CurrentPointLocation = _savePoint;
			DelPrePathNodes();
			PathFinding(start, finish, SoldierVM[sNum].Movement, SoldierV[sNum], SoldierVM[sNum], null);
			
			//Change state from move to attack after move
			SoldierVM[sNum].SoldierState = SoldierState.ATTACK;
			selectPoint = false;
		}// End of Move State
		
		//Point selected and Soldier in AttackState
		
		else if(selectPoint && SoldierVM[sNum].SoldierState == SoldierState.ATTACK )
			//else if(selectPoint && SoldierVM[sNum].SoldierState == SoldierState.ATTACK && SoldierVM[sNum].Action == ActionStyle.STANDBY || SoldierVM[sNum].Action == ActionStyle.YAWP || SoldierVM[sNum].Action == ActionStyle.SEARCH || SoldierVM[sNum].Action == ActionStyle.A_ATK)
		{
			
			var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
			
			if (SoldierVM[sNum].Career == Career.Archer)
				neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 3);
			
			if (SoldierVM[sNum].Action == ActionStyle.STANDBY || SoldierVM[sNum].Action == ActionStyle.YAWP || SoldierVM[sNum].Action == ActionStyle.SEARCH || SoldierVM[sNum].Action == ActionStyle.A_ATK)
			{
				goto EndofAttack;
			}
			//Range of Attack:
			//Swordman < 2
			//Archer < 3
			/*
			switch (SoldierVM[sNum].Career)
			{
			case Career.Swordman:
				neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
				break;
			case Career.Archer:
				neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 3);
				break;
			default:
				neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
				break;
			}
			*/
			
			
			foreach (var neighbor in neighborhoodPoints)
			{
				if(walkableGrid[neighbor].IsEnemy)
					for(int j = 0; j < TargetVM.Count; j++)
				{
					if(neighbor == _savePoint && neighbor == TargetVM[j].CurrentPointLocation)
					{
						//FindNearly cell, get the target
						//if point is one of neighbour, target = pointtarget
						//Debug.Log ("Target Selected");
						
						if(TargetVM[j].Action == ActionStyle.A_ATK)
							goto EndofAttack;
						
						myText.text = "Target Selected";
						
						//Save the information into playlist
						SoldierVM[sNum].playlist.Insert((int)SoldierVM[sNum].Counter, new PlayList(SoldierVM[sNum].CurrentPointLocation, SoldierVM[sNum].Movement ,SoldierVM[sNum].Action, TargetVM[j], TargetV[j]));
						SoldierVM[sNum].Counter++;
						_targetSelected = true;
						goto EndofAttack; //break the foreach
					}
				}
			}
			
		EndofAttack:
				if(!_targetSelected)
			{
				//Save the information into playlist without target
				SoldierVM[sNum].playlist.Insert((int)SoldierVM[sNum].Counter, new PlayList(SoldierVM[sNum].CurrentPointLocation, SoldierVM[sNum].Movement ,SoldierVM[sNum].Action, null, null));
				SoldierVM[sNum].Counter++;
				
				if (SoldierVM[sNum].Action == ActionStyle.STANDBY || SoldierVM[sNum].Action == ActionStyle.YAWP || SoldierVM[sNum].Action == ActionStyle.SEARCH || SoldierVM[sNum].Action == ActionStyle.A_ATK)
					myText.text = "OK, Please Move";
				else
				{
					myText.text = "You can't Attack, Please select another Point";
					return;
				}
			}
			//Change state from move to move after attack and thus check the panel
			SoldierVM[sNum].SoldierState = SoldierState.MOVE;
			selectPoint = false;
			
			//var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(_savePoint) < 2);
			foreach (var neighbor in neighborhoodPoints)
			{
				if(walkableGrid[neighbor].IsWalkable)
					walkableGrid[neighbor].Color = Color.white;
			}
			
			_targetSelected = false;
			selectPoint = false;
			
			MainGameVM.PlayerIQ -= 10;
			IQDisplay.text = "Player IQ: " + MainGameVM.PlayerIQ;
			
			if(MainGameVM.PlayerIQ == 0)
			{
				myText.text = "Please Click Play Button";
			}
			
		}//End of ATTACK State
		
	}
	
	/// <summary>
	/// Get the Grid point, 
	/// SoldierState.MOVE -> update the point graphically
	/// SoldierState.ATTACK -> update the playlist
	/// </summary>
	public void OnClick(FlatHexPoint point)
	{
		if(ProCamera2D.enabled == true)
			ProCamera2D.enabled = false;
		
		if(_clicking == false) return;
		
		if(EventSystem.current.IsPointerOverGameObject()) return;
		
		if (walkableGrid[point].IsWalkable == false || walkableGrid[point].IsEnemy == true && SoldierVM[sNum].SoldierState == SoldierState.MOVE)
		{
			myText.text = "Please Select Another Point";
			return;
		}
		
		for(int i=0; i < SoldierVM.Count; i++)
		{
			if(point == SoldierVM[i].CurrentPointLocation)
			{
				//sNum = i;
				Debug.Log ("Soldier Selected");
				//point += new FlatHexPoint(1,0);
				//return;
			}
		}
		
		if(SoldierV[sNum] != null)
		{
			_clicking = false;
			//Clicked on Move State, a red markNode will be created
			if(SoldierVM[sNum].SoldierState == SoldierState.MOVE)
			{
				if(markNode == null) 
					markNode = Instantiate(pathPrefab);
				
				markNode.transform.parent = pathRoot.transform;
				markNode.transform.localScale = Vector3.one * 0.3f;
				markNode.transform.localPosition = Map[point];
				markNode.Color = ExampleUtils.Colors[3];
				_savePoint = point;
				
				myText.text = "Please Select Move Style";
			}
			
			//Clicked on Attack State, a blue markNode will be created
			else if (SoldierVM[sNum].SoldierState == SoldierState.ATTACK)
			{
				//
				if(markNode == null) 
					markNode = Instantiate(pathPrefab);
				
				markNode.transform.parent = pathRoot.transform;
				markNode.transform.localScale = Vector3.one * 0.3f;
				markNode.transform.localPosition = Map[point];
				markNode.FindChild("Sprite").GetComponent<Renderer>().sortingOrder = 20;
				markNode.Color = ExampleUtils.Colors[0];
				_savePoint = point;
				
				//myText.text = "Please Select Action Style";
				
			}
			_targetSelected = false;
			selectPoint = false;
		}
	}
	
	/// <summary>
	/// Update the position for the related entityView and check in every move
	/// </summary>
	public IEnumerator MovePath(IEnumerable<FlatHexPoint> path, MoveStyle move, EntityView entityView, EntityViewModel entityVM, EntityViewModel oppVM)
	{
		//Debug.Log ("Moving from " + entityView);
		var pathList = path.ToList();
		bool battleFinish = false;
		
		if(pathList.Count < 2) yield break; //Not a valid path
		
		entityVM.Moving = true;
		
		Debug.Log ( "MovePath: " + entityVM.Identifier);
		
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
					if (entityVM is SoldierViewModel && walkableGrid[neighbor].IsEnemy)
					{
						for(int j = 0; j < TargetVM.Count; j++)
						{
							if(neighbor == TargetVM[j].CurrentPointLocation)
							{		  
								//TargetVM[j].BattleState = BattleState.FIGHTING;
								//check here enemy feinted by soldier or target
								if(TargetVM[j].Action != ActionStyle.FEINT && TargetVM[j].Action != ActionStyle.A_ATK && oppVM != TargetVM[j])
								{	
									Debug.Log(TargetVM[j] +  " " + TargetVM[j].Action);
									MainGameController.StartBattle(entityVM, TargetVM[j], entityView, TargetV[j], ActionStyle.ATTACK);
									//MainGameController.StartBattle(TargetVM[j], entityVM , TargetV[j], entityView, ActionStyle.ATTACK);
									Debug.Log("Enemy Attack when Soldier moving");
								}
								
								if (TargetVM[j].Action == ActionStyle.A_ATK && move == MoveStyle.SLOW)
								{
									TargetV[j].RendererColor(ExampleUtils.Colors[5]);
									TargetV[j].FindChild("HealthBarCanvas").gameObject.SetActive(true);
									TargetVM[j].Action = ActionStyle.ATTACK;
									MainGameController.StartBattle(entityVM, TargetVM[j], entityView, TargetV[j], ActionStyle.ATTACK);
								}
								//while(entityVM.BattleState != BattleState.WAITING)
								//yield return new WaitForSeconds(0.5f);
							}
						} // Enf of for loop
					} // End of if Soldier
					
					else if (entityVM is EnemyViewModel && walkableGrid[neighbor].IsSoldier)
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
				
				while(entityVM.BattleState != BattleState.WAITING && entityVM.BattleState != BattleState.DEAD)
					yield return new WaitForSeconds(0.2f);
				
				if(entityVM.BattleState == BattleState.DEAD)
				{
					entityVM.Moving = false;
					break;
				}
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
			
			/// Set Grid isSoldier isEnemy CurerentPosition
			if(entityVM is EnemyViewModel)
			{
				walkableGrid[entityVM.CurrentPointLocation].IsEnemy = false;
				//Debug.Log (entityVM.CurrentPointLocation + "isWalkable: " + walkableGrid[entityVM.CurrentPointLocation].IsWalkable);
			}
			
			else if(entityVM is SoldierViewModel)
			{
				walkableGrid[entityVM.CurrentPointLocation].IsSoldier = false;
			} 
			
			entityVM.CurrentPointLocation = pathList[i+1];
			
			if(entityVM is EnemyViewModel)
			{
				walkableGrid[entityVM.CurrentPointLocation].IsEnemy = true;
				//Debug.Log (entityVM.CurrentPointLocation + "isWalkable: " + walkableGrid[entityVM.CurrentPointLocation].IsWalkable);
			}
			
			else if(entityVM is SoldierViewModel)
			{
				walkableGrid[entityVM.CurrentPointLocation].IsSoldier = true;
			} 
			
			
			while(entityVM.BattleState != BattleState.WAITING)
				yield return new WaitForSeconds(0.5f);
			//Debug.Log ("EntityVM: " + entityVM + "Point: " + entityVM.CurrentPointLocation);	
		}//End for path for loop
		
		entityVM.Moving = false;
		
		//[ForSoldier]Display the range
		if(Play_Btn.interactable == true && entityVM is SoldierViewModel)
		{
			var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(entityVM.CurrentPointLocation) < 2);
			if (entityVM.Career == Career.Archer)
				neighborhoodPoints = Grid.Where(p => p.DistanceFrom(entityVM.CurrentPointLocation) < 3);
			
			foreach (var neighbor in neighborhoodPoints)
			{
				if(walkableGrid[neighbor].IsWalkable)
					walkableGrid[neighbor].Color = ExampleUtils.Colors[3];
			}
		}
		
		//[forEnemy]Check neighhborPoinr whether Soldier is here
		if(Play_Btn.interactable == false && entityVM is EnemyViewModel)
		{
			var neighborhoodEndPoints = Grid.Where(p => p.DistanceFrom(entityVM.CurrentPointLocation) < 2);
			if (entityVM.Career == Career.Archer)
				neighborhoodEndPoints = Grid.Where(p => p.DistanceFrom(entityVM.CurrentPointLocation) < 3);
			
			foreach (var neighbor in neighborhoodEndPoints)
			{
				if(walkableGrid[neighbor].IsSoldier)
					for(int j = 0; j < SoldierVM.Count; j++)
				{
					if(neighbor == SoldierVM[j].CurrentPointLocation)
					{
						MainGameController.StartBattle(SoldierVM[j], entityVM , SoldierV[j], entityView, ActionStyle.ATTACK);
						Debug.Log("Soldier Attack when Enemy moving");
					}
				}
			}
			while(entityVM.BattleState != BattleState.WAITING && entityVM.BattleState != BattleState.DEAD)
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
		//for(int x = 0; x < SoldierVM.Count; x++)
		//{
		SoldierVM[i].CurrentPointLocation = new FlatHexPoint(0, 0);
		SoldierV[i].transform.position = Map[SoldierVM[i].CurrentPointLocation];
		
		SoldierV[i].GetComponent<Renderer>().sortingOrder = 9 - i / 2 ;
		start = SoldierVM[i].CurrentPointLocation;
		//}
		
		for(int j = 0; j < SoldierVM[i].playlist.Count && SoldierVM[i].BattleState != BattleState.DEAD; j++)
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
			
			PathFinding(start, finish, SoldierVM[i].playlist[j].SaveMove, SoldierV[i], SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM);
			
			
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
						var eFinish = NearPoint(SoldierVM[i].playlist[j].SavePointLocation, 2);
						//if eFinish doesn't exist
						//var eFinish = new FlatHexPoint(1 , 0);
						//SoldierVM[i].playlist[j-1].SaveEnemyVM.CurrentPointLocation = eFinish;
						PathFinding(eStart, eFinish, SoldierVM[i].playlist[j].SaveMove, SoldierVM[i].playlist[j-1].SaveEnemyView, SoldierVM[i].playlist[j-1].SaveEnemyVM, null);
					}
				}
			}
			
			///--------------------Wait Moving Finish-----------------///
			while (SoldierVM[i].Moving)
				yield return new WaitForSeconds(0.2f);
			
			///--------------------Attack State-------------------------///
			
			if(SoldierVM[i].BattleState == BattleState.DEAD)
				Debug.Log ("Dead！！！No more attack");
			
			///--------------------Attack Style : A_ATK-----------------///
			else if (SoldierVM[i].playlist[j].SaveAction == ActionStyle.A_ATK)
			{
				//SoldierVM[i].CurrentPointLocation = SoldierVM[i].playlist[j+1].SavePointLocation;
				Debug.Log (SoldierVM[i] + " is waiting enemy");
				yield return new WaitForSeconds(2f);
			}
			
			///--------------------Attack Style : YAWP----------------///
			else if(SoldierVM[i].playlist[j].SaveAction == ActionStyle.YAWP)
			{
				var yawpNeighborPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[i].playlist[j].SavePointLocation) < 3);
				
				foreach (var neighbor in yawpNeighborPoints)
				{
					if(walkableGrid[neighbor].IsEnemy)
						for(int x = 0; x < TargetVM.Count; x++)
					{
						if(neighbor == TargetVM[x].CurrentPointLocation)
						{
							Debug.Log ("YAWP!!!!");
							myText.text = SoldierVM[i].Name + " YAWP!!!";
							yield return new WaitForSeconds(0.3f);
							var finishPoint = SoldierVM[i].playlist[j].SavePointLocation + DirectionPoint(SoldierVM[i].playlist[j].SavePointLocation, TargetVM[x].CurrentPointLocation);
							PathFinding(TargetVM[x].CurrentPointLocation, finishPoint, MoveStyle.FAST, TargetV[x], TargetVM[x], null);
							
							if(TargetVM[x].Action == ActionStyle.A_ATK)
							{
								TargetV[x].RendererColor(ExampleUtils.Colors[5]);
								TargetV[x].FindChild("HealthBarCanvas").gameObject.SetActive(true);
								TargetVM[x].Action = ActionStyle.ATTACK;
							}
							
							while(TargetVM[x].Moving == true || !TargetV[x].gameObject.activeSelf)
								yield return new WaitForSeconds(0.2f);
							
							
							MainGameController.StartBattle(SoldierVM[i], TargetVM[x], SoldierV[i], TargetV[x], ActionStyle.ATTACK);
							
							while(SoldierVM[i].BattleState != BattleState.WAITING && SoldierVM[i].BattleState != BattleState.DEAD)
							{
								//Debug.Log ("Wating finish Battle");
								yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
							}
						}
					}
				}
			}
			
			///--------------------Attack Style : SEARCH----------------///
			else if(SoldierVM[i].playlist[j].SaveAction == ActionStyle.SEARCH)
			{
				var yawpNeighborPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[i].playlist[j].SavePointLocation) < 3);
				
				foreach (var neighbor in yawpNeighborPoints)
				{
					if(walkableGrid[neighbor].IsEnemy)	
						for(int x = 0; x < TargetVM.Count; x++)
					{
						//check here enemy feinted by soldier?
						if(neighbor == TargetVM[x].CurrentPointLocation)
						{
							Debug.Log ("SEARCH!!!!");
							myText.text = SoldierVM[i].Name + " Enemy Finds!!!";
							//yield return new WaitForSeconds(0.3f);
							var finishPoint = TargetVM[x].CurrentPointLocation + DirectionPoint(TargetVM[x].CurrentPointLocation, SoldierVM[i].playlist[j].SavePointLocation);
							
							if(TargetVM[x].Action == ActionStyle.A_ATK)
							{
								TargetV[x].RendererColor(ExampleUtils.Colors[5]);
								TargetV[x].FindChild("HealthBarCanvas").gameObject.SetActive(true);
								TargetVM[x].Action = ActionStyle.ATTACK;
							}
							
							PathFinding(SoldierVM[i].CurrentPointLocation, finishPoint, MoveStyle.FAST, SoldierV[i], SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM);
							
							while(SoldierVM[i].Moving == true)
								yield return new WaitForSeconds(0.2f);
							
							MainGameController.StartBattle(SoldierVM[i], TargetVM[x], SoldierV[i], TargetV[x], ActionStyle.ATTACK);
							
							while(SoldierVM[i].BattleState != BattleState.WAITING && SoldierVM[i].BattleState != BattleState.DEAD)
							{
								//Debug.Log ("Wating finish Battle");
								yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
							}
						}
					}
				}
			}
			
			//if( j != 0 && SoldierVM[i].playlist[j].SaveEnemyVM == null && SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
			if(j != 0 && SoldierVM[i].playlist[j-1].SaveAction == ActionStyle.FEINT)
			{
				Debug.Log ("Feint Attack");
				//NearPoint
				var neighborPoint = Grid.Where(p => p.DistanceFrom(SoldierVM[i].playlist[j].SavePointLocation) < 2);
				foreach (var neighbor in neighborPoint)
					
					
				{
					if(walkableGrid[neighbor].IsEnemy)
						MainGameController.StartBattle(SoldierVM[i].playlist[j-1].SaveEnemyVM, SoldierVM[i], SoldierVM[i].playlist[j-1].SaveEnemyView, SoldierV[i],  ActionStyle.ATTACK);
				}
			}
			
			///--------------------Attack Style : Basic Attack - ATTACK and ASSULT and PIN and FEINT----------------///
			if(SoldierVM[i].playlist[j].SaveEnemyVM != null && SoldierVM[i].playlist[j].SaveEnemyVM.Action != ActionStyle.A_ATK)
			{	
				SoldierVM[i].playlist[j].SaveEnemyVM.PlayList = SoldierVM[i].playlist[j];
				
				//if enemy range is not enough, wwaiting until moved
				if(SoldierVM[i].Career == Career.Archer && SoldierVM[i].playlist[j].SaveEnemyVM.Career == Career.Swordman)
				{
					var finishPoint = SoldierVM[i].playlist[j].SaveEnemyVM.CurrentPointLocation + DirectionPoint(SoldierVM[i].playlist[j].SaveEnemyVM.CurrentPointLocation, SoldierVM[i].playlist[j].SavePointLocation);
					PathFinding(SoldierVM[i].playlist[j].SaveEnemyVM.CurrentPointLocation, finishPoint, MoveStyle.FAST, SoldierVM[i].playlist[j].SaveEnemyView, SoldierVM[i].playlist[j].SaveEnemyVM, SoldierVM[i]);
				}
				
				MainGameController.StartBattle(SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM, SoldierV[i], SoldierVM[i].playlist[j].SaveEnemyView, SoldierVM[i].playlist[j].SaveAction); 
				//this check maybe not good, please find another way to repalce it
				//while(SoldierVM[i].playlist[j].SaveEnemyVM.Health > 0 && SoldierVM[i].Health > 0 && SoldierVM[i].TimeStarted == true)
				
				if (SoldierVM[i].playlist[j].SaveAction == ActionStyle.FEINT)
				{
					SoldierVM[i].playlist[j].SaveEnemyVM.Action = ActionStyle.FEINT;
				}
				
				while(SoldierVM[i].BattleState != BattleState.WAITING && SoldierVM[i].BattleState != BattleState.DEAD)
				{
					//Debug.Log ("Wating finish Battle");
					yield return new WaitForSeconds(1/SoldierVM[i].AttackSpeed);
				}
				
				if(SoldierVM[i].BattleState == BattleState.DEAD)
					break;
			}
			
			//SoldierVM[i].BattleState = BattleState.WAITING;
			//Enemy Logic
			yield return new WaitForSeconds(0.5f);
		}
		
		//while(SoldierVM[i].BattleState != BattleState.WAITING)
		//	yield return new WaitForSeconds(0.2f);
		
		Debug.Log("WinCondition: " + MainGameVM.WinCondition);
		
		//the following logic is about soldier will move to the tower after all command finished
		//this is the case that win condition is entering the tower 
		if(MainGameVM.WinCondition == WinCondition.Tower)
		{
			if(SoldierVM[i].CurrentPointLocation != towerPoint)
			{
				PathFinding(SoldierVM[i].CurrentPointLocation, towerPoint, MoveStyle.FAST, SoldierV[i], SoldierVM[i], null);
				Debug.Log ("Soldier" + i + "moves to the tower");
			}
			//PathFinding(start, finish, SoldierVM[i].playlist[j].SaveMove, SoldierV[i], SoldierVM[i], SoldierVM[i].playlist[j].SaveEnemyVM);
			
			///--------------------Wait Moving Finish-----------------///
			while (SoldierVM[i].Moving)
				yield return new WaitForSeconds(0.2f);
			
			
			//Check the Soldier enter the tower or not
			if(SoldierVM[i].CurrentPointLocation == towerPoint)
			{
				Debug.Log ("GameOver");
				MainGameVM.EnemyCount = -1;
				MainGameVM.GameState = GameState.GameOver;
				
				yield return new WaitForSeconds(2f);
				StopAllCoroutines();
			}
		}
		
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
		FlatHexPoint finalDirectionPoint;
		//N, NE, E, S
		
		if(point.X >= 0)
		{
			if(point.Y > 0)
				finalDirectionPoint = FlatHexPoint.North;
			else if (point.Y == 0)
				finalDirectionPoint = FlatHexPoint.NorthEast;
			else
			{
				if(point.X == 0)
					finalDirectionPoint = FlatHexPoint.South;
				else
					finalDirectionPoint = FlatHexPoint.SouthEast;
			}
		}
		//NW, SW
		else
		{
			if(point.Y > 0)
				finalDirectionPoint = FlatHexPoint.NorthWest;
			else
				finalDirectionPoint = FlatHexPoint.SouthWest;
		}
		
		return finalDirectionPoint;
	}
	
	/// <summary>
	/// reutrn the near point from targetPoint
	/// </summary>
	public FlatHexPoint NearPoint(FlatHexPoint targetPoint, int range)
	{
		var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(targetPoint) < range);
		
		foreach (var neighour in neighborhoodPoints)
		{
			if(walkableGrid[neighour].IsWalkable && !walkableGrid[neighour].IsEnemy)
				return neighour;
		}
		
		return FlatHexPoint.Zero;
	} 
	
	public void PathFinding(FlatHexPoint s,  FlatHexPoint f, MoveStyle moveStyle, EntityView entityView, EntityViewModel entityVM, EntityViewModel oppVM)
	{
		
		//var path = Algorithms.AStar(Grid, s, f);
		var path = Algorithms.AStar(
			Grid,
			s,
			f,
			(p,q) => p.DistanceFrom(q),
			c => ((GSCell) c).IsWalkable && !((GSCell) c).IsEnemy, //accessing your custom bool from the GSCell
			(p, q) => 1);
		
		//Debug.Log(entityView.name + "PathFinding" + path);
		if (path == null) 
		{
			Debug.Log("there is no path between the start and goal.");
			return; //then there is no path between the start and goal.
		}
		
		StartCoroutine(MovePath(path, moveStyle, entityView, entityVM, oppVM));
	}
	
	public void DelPrePathNodes()
	{
		foreach (Transform child in pathRoot.transform)
			GameObject.Destroy(child.gameObject);
	}
	
	/// <summary>
	/// All Button Functions
	/// </summary>
	
	public void EndTurn()
	{
		
		var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
		
		if (SoldierVM[sNum].Career == Career.Archer)
			neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 3);
		
		foreach (var neighbor in neighborhoodPoints)
		{
			if(walkableGrid[neighbor].IsWalkable)
				walkableGrid[neighbor].Color = Color.white;
		}
		
		SoldierV[sNum].RendererColor(Color.grey);
		if (sNum < 4)
			sNum++;
		else
		{
			myText.text = "Please Click Play Button";
			return;
		}
		
		SoldierVM[sNum].SoldierState = SoldierState.MOVE;
		selectPoint = false;
		myText.text = ("Please Move Soldier" + (sNum + 1));
		
		//Debug.Log (Cinematics.CinematicTargets);
		//Cinematics.GoToNextTarget();
		ProCamera2D.enabled = true;
		ProCamera2D.RemoveCameraTarget(SoldierV[sNum-1].transform);
		ProCamera2D.AddCameraTarget(SoldierV[sNum].transform);
	}
	
	public void PlayBtn()
	{
		pathRoot.SetActive(false);
		selectPoint = false;
		
		Play_Btn.interactable = false;
		EndTurn_Btn.interactable = false;
		Redo_Btn.interactable = false;
		//Restart_Btn.interactable = false;
		
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
		var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
		
		if (SoldierVM[sNum].Career == Career.Archer)
			neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 3);
		
		foreach (var neighbor in neighborhoodPoints)
		{
			if(walkableGrid[neighbor].IsWalkable)
				walkableGrid[neighbor].Color = Color.white;
		}
		
		if(SoldierVM[sNum].playlist.Count > 1)
		{
			if (SoldierVM[sNum].SoldierState == SoldierState.MOVE)
			{
				SoldierVM[sNum].playlist.RemoveAt(SoldierVM[sNum].playlist.Count - 1);
				SoldierVM[sNum].Counter--;
				MainGameVM.PlayerIQ +=10;
				IQDisplay.text = "Player IQ: " + MainGameVM.PlayerIQ;
			}
			
			SoldierVM[sNum].CurrentPointLocation = SoldierVM[sNum].playlist[SoldierVM[sNum].playlist.Count - 1].SavePointLocation;
			SoldierV[sNum].transform.position = Map[SoldierVM[sNum].CurrentPointLocation] + new Vector3(0, -18);
		}
		
		else
		{
			SoldierVM[sNum].CurrentPointLocation = new FlatHexPoint(sNum, 0);
			SoldierV[sNum].transform.position = Map[SoldierVM[sNum].CurrentPointLocation];
			myText.text = "Not more action redo";
		}
		
		SoldierVM[sNum].SoldierState = SoldierState.MOVE;
		selectPoint = false;
		DelPrePathNodes();
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
	
	
	public void ChangeSoldierBtn()
	{
		
		var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
		
		if (SoldierVM[sNum].Career == Career.Archer)
			neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 3);
		
		foreach (var neighbor in neighborhoodPoints)
		{
			if(walkableGrid[neighbor].IsWalkable)
				walkableGrid[neighbor].Color = Color.white;
		}
		
		//Change sNum
		if (sNum < 4)
			sNum++;
		
		else 
			sNum = 0;
		
		SoldierV[sNum].RendererColor(Color.white);
		
		//Change Camera position
		SoldierVM[sNum].SoldierState = SoldierState.MOVE;
		selectPoint = false;
		myText.text = ("Please Move Soldier" + (sNum + 1));
		
		//Debug.Log (Cinematics.CinematicTargets);
		//Cinematics.GoToNextTarget();
		ProCamera2D.enabled = true;
		if (sNum == 0)
			ProCamera2D.RemoveCameraTarget(SoldierV[4].transform);
		else	
			ProCamera2D.RemoveCameraTarget(SoldierV[sNum - 1].transform);
		
		ProCamera2D.AddCameraTarget(SoldierV[sNum].transform);
	}
	
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
	
	public void SelectSoldierBtn(int i)
	{
		
		var neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 2);
		
		if (SoldierVM[sNum].Career == Career.Archer)
			neighborhoodPoints = Grid.Where(p => p.DistanceFrom(SoldierVM[sNum].CurrentPointLocation) < 3);
		
		foreach (var neighbor in neighborhoodPoints)
		{
			if(walkableGrid[neighbor].IsWalkable)
				walkableGrid[neighbor].Color = Color.white;
		}
		
		SoldierV[sNum].RendererColor(Color.grey);
		ProCamera2D.enabled = true;
		ProCamera2D.RemoveCameraTarget(SoldierV[sNum].transform);
		
		sNum = i - 1;
		
		ProCamera2D.AddCameraTarget(SoldierV[sNum].transform);
		
		SoldierV[sNum].RendererColor(Color.white);
		
		//Change Camera position
		SoldierVM[sNum].SoldierState = SoldierState.MOVE;
		selectPoint = false;
		myText.text = ("Please Move Soldier" + (sNum + 1));
	}
	
	/*
public void NextEnemyBtn()
{
	//if (Cinematics.IsActive)
	Cinematics.GoToNextTarget();
	//StartCoroutine(FindEnemy());
}
*/
	
	/// This is the distance function used by the path algorithm.
	private float EuclideanDistance(FlatHexPoint p, FlatHexPoint q)
	{
		float distance = (Map[p] - Map[q]).magnitude;
		return distance;
	}
}
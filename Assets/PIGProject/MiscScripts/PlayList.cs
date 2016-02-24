using UnityEngine;
using System.Collections;
using Gamelogic.Grids;

public struct PlayList
{
	public FlatHexPoint SavePointLocation;
	public MoveStyle SaveMove;
	public ActionStyle SaveAction;
	public EntityViewModel SaveEnemyVM;
	public EntityView SaveEnemyView;
	//public int[] HealthHistory = new int[50];
	
	public PlayList(FlatHexPoint savePointLocation, MoveStyle saveMove, ActionStyle saveAction, EntityViewModel saveEnemyVM, EntityView saveEnemyView)
	{
		this.SavePointLocation = savePointLocation;
		this.SaveMove = saveMove;
		this.SaveAction = saveAction;
		this.SaveEnemyVM = saveEnemyVM;
		this.SaveEnemyView = saveEnemyView;
		//this.HealthHistory = healthHistory;
	}
}

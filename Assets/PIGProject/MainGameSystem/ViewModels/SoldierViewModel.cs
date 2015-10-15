using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UnityEngine;
using UniRx;
using Gamelogic.Grids;

public partial class SoldierViewModel : SoldierViewModelBase {

	public List<PlayList> playlist = new List<PlayList>(){};
	/*
	public struct PlayList
	{
		public FlatHexPoint SavePointLocation;
		public MoveStyle SaveMove;
		public ActionStyle SaveAction;
		public EntityViewModel SaveEnemyVM;
		//public int[] HealthHistory = new int[50];
		
		public PlayList(FlatHexPoint savePointLocation, MoveStyle saveMove, ActionStyle saveAction, EntityViewModel saveEnemyVM)
		{
			this.SavePointLocation = savePointLocation;
			this.SaveMove = saveMove;
			this.SaveAction = saveAction;
			this.SaveEnemyVM = saveEnemyVM;
			//this.HealthHistory = healthHistory;
		}
	}
	*/
}

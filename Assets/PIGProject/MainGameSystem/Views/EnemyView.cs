using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Services;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UniRx;
using UnityEngine;
using Gamelogic.Grids;

public class EnemyView : EnemyViewBase {
	public GSHexGridManager gSHexGridManager;
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as EnemyViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }

    public override void Bind() {
        base.Bind();
        // Use this.Enemy to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
    }

    public override void BattleStateChanged(BattleState bState) {
		//ExecuteBattleStateCommand();
		//GSHexGridManager gSHexGridManager = gameObject.GetComponent<GSHexGridManager>();
		//gSHexGridManager = GameObject.Find("HexMapGrid").GetComponent(GSHexGridManager);

		/*
		if(bState == BattleState.WAITING)
			if(this.Enemy.Opponent != null && this.Enemy.PlayList.SaveAction == ActionStyle.FEINT)
		{
			//follow the soldier
			FlatHexPoint start = this.Enemy.CurrentPointLocation;
			FlatHexPoint finish = new FlatHexPoint(10,10);
			Debug.Log(start + " " + finish);
			this.Enemy.CurrentPointLocation = new FlatHexPoint(10,10);
			if(gSHexGridManager == null)
				Debug.Log("gSHexGridManager is null");
			if(gSHexGridManager != null)
				gSHexGridManager.PathFinding(start, finish, this.Enemy.Movement, this);
		}
		*/
	}
}

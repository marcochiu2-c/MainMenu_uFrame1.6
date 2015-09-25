using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;


public class SoldierController : SoldierControllerBase {

	public override void InitializeSoldier(SoldierViewModel viewModel) {
		base.InitializeSoldier(viewModel);
        // This is called when a PlayerViewModel is createdmove
		//viewModel.PlayerState = PlayerState.ATTACK;
		//viewModel.Movement = MoveStyle.NORMAL;
		//viewModel.Quantity = 100;
		//viewModel.MovementProperty.OnNext(new ChangeActionStyleCommand() {this.Soldier, MoveStyle.SLOW});
	}
	
	public override void ChangeActionStyle(SoldierViewModel viewModel) {
        base.ChangeActionStyle(viewModel);
    }

	public override void ChangeMoveStyle(SoldierViewModel viewModel) {
        base.ChangeMoveStyle(viewModel);
    }

	public override void ChangeQuantity(SoldierViewModel viewModel) {
        base.ChangeQuantity(viewModel);
    }

}

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
	/*
	public void UpdateQuantity(int number){
		float maxHealth = 100f;
		
		this._Quantity = number;
		var curHealth = this._Quantity/Max_Quantity;
		//Debug.Log("curHealth: " + curHealth);
		
		healthBar.transform.localScale = new Vector3(curHealth, healthBar.transform.localScale.y);
		
		
	}
	*/
}

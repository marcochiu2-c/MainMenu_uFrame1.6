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
using UnityEngine.UI;


public class MainGameRootView : MainGameRootViewBase {
    
	public Text gameOverText;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
		//gameOverText = GameObject.Find("Text_GameOver").GetComponent<Text>();
		gameOverText.gameObject.SetActive(false);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as MainGameRootViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.MainGameRoot to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
    }

    public override void GameStateChanged(GameState gameState) {
		if (gameState == GameState.Playing) return;
		
		if(this.MainGameRoot.SoldierCount == 0)
		{
			Debug.Log ("You Lose");
			gameOverText.text = "GameOver";
			gameOverText.gameObject.SetActive(true);
		}
		else if (this.MainGameRoot.EnemyCount == 0)
		{
			Debug.Log ("You Win");
			gameOverText.text = "You Win";
			gameOverText.gameObject.SetActive(true);
		}
    }
}

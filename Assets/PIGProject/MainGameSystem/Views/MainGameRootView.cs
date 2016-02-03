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
using DG.Tweening;


public class MainGameRootView : MainGameRootViewBase {
    
	public Text gameOverText;
	
	public Button InfoButton;
	public Button InfoCloseButton;
	public Button InfoAtkButton;
	public Button InfoMoveButton;
	public Button InfoMissionButton;
	public Button StartBattleButton;
	
	public TextAsset atkInfo;
	public TextAsset moveInfo;
	public TextAsset missionInfo;
	public GameObject InfoPanel;
	public GameObject BlockPanel;
	public Text InfoText;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
		//gameOverText = GameObject.Find("Text_GameOver").GetComponent<Text>();
		gameOverText.gameObject.SetActive(false);
		//TextAsset atkInfo = Resources.Load<TextAsset> ("ATK_Info");
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as MainGameRootViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		InfoText.text = missionInfo.text;
	}
	
	public override void Bind() {
        base.Bind();
        // Use this.MainGameRoot to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
        
        //InfoPanel
		this.BindButtonToHandler(InfoButton, () => {
		
			if(InfoPanel.activeSelf == false)
			{
				InfoPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad).OnStart(() =>  InfoPanel.SetActive(true));
				BlockPanel.SetActive(true);
			}
				
			else
			{
				InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
				BlockPanel.SetActive(false);	
			}
			/*
			Publish(new NotifyCommand(){
				Message = atkInfo.text 
		    });
		    */
		});
		
		this.BindButtonToHandler(InfoCloseButton, () => { 
			InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
			BlockPanel.SetActive(false);
		});
		
		this.BindButtonToHandler(StartBattleButton, () => { 
			InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
			BlockPanel.SetActive(false);
		});
		
		this.BindButtonToHandler(InfoAtkButton, () => { 
			InfoText.text = atkInfo.text;
		});
		
		this.BindButtonToHandler(InfoMoveButton, () => { 
			InfoText.text = moveInfo.text;
		});
		
		this.BindButtonToHandler(InfoMissionButton, () => { 
			InfoText.text = missionInfo.text;
		});
		
    }

    public override void GameStateChanged(GameState gameState) {
		if (gameState == GameState.Playing) return;
		
		if (this.MainGameRoot.EnemyCount == 0)
		{
			Debug.Log ("You Win");
			gameOverText.text = "You Win";
			gameOverText.gameObject.SetActive(true);
			ExecuteGameOver();
		}
		
		else if(this.MainGameRoot.SoldierCount == 0)
		{
			Debug.Log ("You Lose");
			gameOverText.text = "GameOver";
			gameOverText.gameObject.SetActive(true);
			ExecuteGameOver();
		}

    }
}

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


public class SetBattleScreenView : SetBattleScreenViewBase {

	public Button BackButton;
	public Button DailyBattleButton;
	public Button SpecialBattleButton;
	public Button LimitedBattleButton;
	public Button HolidayBattleButton;
	
	public Button MapButton;
	public Button GoButton;
	public Button NextButton;
	public Button PreButton;
	
	public GameObject DailyMission;
	public GameObject SpecialMission;
	public GameObject LimitedMission;
	public GameObject HolidayMission;
	
	public GameObject TeamSelection;
	public GameObject SelectionPanel;
	
	public Slider loadingBar;
	public GameObject loadingImage;
	
	private AsyncOperation _async;
	private int _moveCount = 0;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as SetBattleScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.SetBattleScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (BackButton, () => {
			DailyMission.gameObject.SetActive (false);
			SpecialMission.gameObject.SetActive (false);
			LimitedMission.gameObject.SetActive (false);
			HolidayMission.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (DailyBattleButton, () => {
			
			DailyMission.gameObject.SetActive (true);
			SpecialMission.gameObject.SetActive (false);
			LimitedMission.gameObject.SetActive (false);
			HolidayMission.gameObject.SetActive (false);
			
			/*
			Publish(new UnloadSceneCommand()
			        {
				SceneName = "MainMenuScene"
			});
			Publish(new LoadSceneCommand()
			        {
				SceneName = "MainGameScene"
			});
			*/
			
			/*
			loadingImage.SetActive (true);
			StartCoroutine (LoadLevelWithBar ("MainGameScene"));
			*/
			
		});

		this.BindButtonToHandler (SpecialBattleButton, () => {
			DailyMission.gameObject.SetActive (false);
			SpecialMission.gameObject.SetActive (true);
			LimitedMission.gameObject.SetActive (false);
			HolidayMission.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (LimitedBattleButton, () => {
			DailyMission.gameObject.SetActive (false);
			SpecialMission.gameObject.SetActive (false);
			LimitedMission.gameObject.SetActive (true);
			HolidayMission.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (HolidayBattleButton, () => {
			DailyMission.gameObject.SetActive (false);
			SpecialMission.gameObject.SetActive (false);
			LimitedMission.gameObject.SetActive (false);
			HolidayMission.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (MapButton, () => {
			TeamSelection.gameObject.SetActive(true);
		});
		
		this.BindButtonToHandler (GoButton, () => {
			loadingImage.SetActive (true);
			StartCoroutine (LoadLevelWithBar ("MainGameScene"));
		});
		
		/*
		this.BindButtonToHandler (NextButton, () => {
			SelectionPanel.transform.DOMoveX(SelectionPanel.transform.position.x - 645, 0.5f);
			
			PreButton.interactable = true;
			
			_moveCount++;
			if(_moveCount == 4)
				NextButton.interactable = false;
			
		});
		
		this.BindButtonToHandler (PreButton, () => {
			SelectionPanel.transform.DOMoveX(SelectionPanel.transform.position.x + 645, 0.5f);
			
			NextButton.interactable = true;
			
			_moveCount--;
			if(_moveCount == 0)
				PreButton.interactable = false;
			
		});
		*/

    }
    
	IEnumerator LoadLevelWithBar (string level)
	{
		_async = Application.LoadLevelAsync(level);
		while(!_async.isDone)
		{
			loadingBar.value=_async.progress;
			yield return null;
		}
		
	}
}

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


public class SetBattleScreenView : SetBattleScreenViewBase {

	public Button BackButton;
	public Button DailyBattleButton;
	public Button SpecialBattleButton;
	public Button LimitedBattleButton;
	public Button HolidayBattleButton;
	
	public Button Mission1Button;
	public Button Mission2Button;
	public Button Mission3Button;
	public Button Mission4Button;
	public Button Mission5Button;
	public Button Mission6Button;
	public Button Mission7Button;
	public Button Mission8Button;
	public Button Mission9Button;
	public Button Mission10Button;
	public Button Mission11Button;
	public Button Mission12Button;
	public Button Mission13Button;
	public Button Mission14Button;
	public Button Mission15Button;
	
	public Button GoToConferenceBtn;
	
	public GameObject DailyMission;
	public GameObject SpecialMission;
	public GameObject LimitedMission;
	public GameObject HolidayMission;
	public GameObject GoToConferencePanel;
	
	public MainGameRootViewModel MainGameVM;
	public UserViewModel LocalUser;
	
	public Slider loadingBar;
	public GameObject loadingImage;
	
	private AsyncOperation _async;
		
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as SetBattleScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        
		MainGameVM = uFrameKernel.Container.Resolve<MainGameRootViewModel>("MainGameRoot");
		Debug.Log (MainGameVM== null ? "MainGameVM is null" : MainGameVM.Identifier);
		
		LocalUser = uFrameKernel.Container.Resolve<UserViewModel>("LocalUser");
		Debug.Log (LocalUser == null ? "LocalUser is null" : LocalUser.Identifier);

    }
    
    public override void Bind() {
        base.Bind();
        // Use this.SetBattleScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
		var evt = new RequestMainMenuScreenCommand();
		
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
		
		
		this.BindButtonToHandler (Mission1Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission2Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission3Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission4Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission5Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission6Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission7Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission8Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission9Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission10Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission11Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission12Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission13Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission14Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission15Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission15Button, () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (GoToConferenceBtn, () => {
			evt.ScreenType = typeof(ConferenceScreenViewModel);
			Publish(evt);
			
			DailyMission.gameObject.SetActive (false);
			SpecialMission.gameObject.SetActive (false);
			LimitedMission.gameObject.SetActive (false);
			HolidayMission.gameObject.SetActive (false);
			GoToConferencePanel.gameObject.SetActive(false);
			
		});
    }
    
    public void ChangetoMainGame()
    {
		
		if(LocalUser.SetTeam == false)
		{
			GoToConferencePanel.gameObject.SetActive(true);
			return;
		}
		
		Publish(new UnloadSceneCommand()
		        {
			SceneName = "MainMenuScene"
		});
		
		loadingImage.SetActive (true); 
		StartCoroutine (LoadLevelWithBar ("MainGameScene"));
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

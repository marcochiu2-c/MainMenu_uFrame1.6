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
	public static Button DailyBattleButton;
	public static Button SpecialBattleButton;
	public static Button LimitedBattleButton;
	public static Button HolidayBattleButton;
	
	/*
	public static Button Mission1Button;
	public static Button Mission2Button;
	public static Button Mission3Button;
	public static Button Mission4Button;
	public static Button Mission5Button;
	public static Button Mission6Button;
	public static Button Mission7Button;
	public static Button Mission8Button;
	public static Button Mission9Button;
	public static Button Mission10Button;
	public static Button Mission11Button;
	public static Button Mission12Button;
	public static Button Mission13Button;
	public static Button Mission14Button;
	public static Button Mission15Button;
	*/
	
	public static Button[] Mission_1Button = new Button[15];
	public static Button[] Mission_2Button = new Button[15];
	public static Button[] Mission_3Button = new Button[15];
	public static Button[] Mission_4Button = new Button[15];
	public static Button[] Mission_5Button = new Button[15];
	public static Button[] Mission_6Button = new Button[16];
	public static Button[] Mission_7Button = new Button[16];
	public static Button[] Mission_8Button = new Button[16];
	public static Button[] Mission_9Button = new Button[16];
	public static Button[] Mission_10Button = new Button[16];
	
	
	
	public Button GoToConferenceBtn;
	
	public static GameObject DailyMission;
	public static GameObject SpecialMission;
	public static GameObject LimitedMission;
	public static GameObject HolidayMission;
	public static GameObject GoToConferencePanel;
	
	public MainGameRootViewModel MainGameVM;
	public UserViewModel LocalUser;
	public Game game;
	
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
		
		game = Game.Instance;
		
		AssignGameObjectVariable();

    }
    
	public void AssignGameObjectVariable()
	{
		Transform panel = ScreenUIContainer.transform.Find ("Panel");
		Transform popupHolder = panel.Find ("PopupHolder");
		Transform btnHolder = panel.Find ("ButtonHolder");
		
		DailyMission = popupHolder.Find("DailyMission").gameObject;
		SpecialMission = popupHolder.Find("SpecialMission").gameObject;
		LimitedMission = popupHolder.Find("LimitedMission").gameObject;
		HolidayMission = popupHolder.Find("HolidayMission").gameObject;
		GoToConferencePanel = panel.Find("GoToConference").gameObject;
		
		//BackButton = btnHolder.Find ("DailyBattleButton").GetComponent<Button> ();
		DailyBattleButton = btnHolder.Find ("DailyBattleButton").GetComponent<Button> ();
		SpecialBattleButton = btnHolder.Find ("SpecialBattleButton").GetComponent<Button> ();
		LimitedBattleButton = btnHolder.Find ("LimitedBattleButton").GetComponent<Button> ();
		HolidayBattleButton = btnHolder.Find ("HolidayBattleButton").GetComponent<Button> ();
		
		Transform[] missionMap = new Transform[10];
		
		for(int i = 0; i < 10; i++)
		{
			int num = i+1;
			missionMap[i] = DailyMission.transform.Find("Mission" + num + "Map");
		}
		
		
		for(int i = 0; i < 15; i++)
		{
			int num = i+1;
			Mission_1Button[i] = missionMap[0].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 15; i++)
		{
			int num = i+1;
			Mission_2Button[i] = missionMap[1].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 15; i++)
		{
			int num = i+1;
			Mission_3Button[i] = missionMap[2].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 15; i++)
		{
			int num = i+1;
			Mission_4Button[i] = missionMap[3].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 15; i++)
		{
			int num = i+1;
			Mission_5Button[i] = missionMap[4].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 16; i++)
		{
			int num = i+1;
			Mission_6Button[i] = missionMap[5].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 16; i++)
		{
			int num = i+1;
			Mission_7Button[i] = missionMap[6].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 16; i++)
		{
			int num = i+1;
			Mission_8Button[i] = missionMap[7].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 16; i++)
		{
			int num = i+1;
			Mission_9Button[i] = missionMap[8].Find("Mission" + num).GetComponent<Button> ();
		}
		
		for(int i = 0; i < 16; i++)
		{
			int num = i+1;
			Mission_10Button[i] = missionMap[9].Find("Mission" + num).GetComponent<Button> ();
		}
		
		/*
		for(int i = 0; i < 10; i++)
		{
		 
			for(int j = 0; j < 15; j++)
			{
				int num = j+1;
				MissionButton[i][j] = missionMap[i].Find("Mission" + num).GetComponent<Button> ();
			}
			
			if(i <= 5)
				MissionButton[i][15] = missionMap[i].Find("Mission16").GetComponent<Button> ();
		}
		*/
		
		/*
		Mission1Button = mission1Map.Find("Mission1").GetComponent<Button> ();
		Mission2Button = mission1Map.Find("Mission2").GetComponent<Button> ();
		Mission3Button = mission1Map.Find("Mission3").GetComponent<Button> ();
		Mission4Button = mission1Map.Find("Mission4").GetComponent<Button> ();
		Mission5Button = mission1Map.Find("Mission5").GetComponent<Button> ();
		Mission6Button = mission1Map.Find("Mission6").GetComponent<Button> ();
		Mission7Button = mission1Map.Find("Mission7").GetComponent<Button> ();
		Mission8Button = mission1Map.Find("Mission8").GetComponent<Button> ();
		Mission9Button = mission1Map.Find("Mission9").GetComponent<Button> ();
		Mission10Button = mission1Map.Find("Mission10").GetComponent<Button> ();
		Mission11Button = mission1Map.Find("Mission11").GetComponent<Button> ();
		Mission12Button = mission1Map.Find("Mission12").GetComponent<Button> ();
		Mission13Button = mission1Map.Find("Mission13").GetComponent<Button> ();
		Mission14Button = mission1Map.Find("Mission14").GetComponent<Button> ();
		Mission15Button = mission1Map.Find("Mission115").GetComponent<Button> ();
		*/
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
		
		//出陣－逢3, 6, 9, 12關打塔，15/16關打 boss，其餘打人。
		//Mission_1
		this.BindButtonToHandler (Mission_1Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_1Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		//Mission_2
		this.BindButtonToHandler (Mission_2Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_2Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_3
		this.BindButtonToHandler (Mission_3Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_3Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_4
		this.BindButtonToHandler (Mission_4Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_4Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_5
		this.BindButtonToHandler (Mission_5Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_5Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_6
		this.BindButtonToHandler (Mission_6Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_6Button[15], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_7
		this.BindButtonToHandler (Mission_7Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_7Button[15], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_8
		this.BindButtonToHandler (Mission_8Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_8Button[15], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_9
		this.BindButtonToHandler (Mission_9Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_9Button[15], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
			ChangetoMainGame();
		});
		
		//Mission_10
		this.BindButtonToHandler (Mission_10Button[0], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[1], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[2], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[3], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[4], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[5], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[6], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[7], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[8], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[9], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[10], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[11], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Tower;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[12], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[13], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[14], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Enemies;
			
			ChangetoMainGame();
		});
		
		this.BindButtonToHandler (Mission_10Button[15], () => {
			if (LocalUser != null)
				LocalUser.WinCondition = WinCondition.Boss;
			
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
		
		for(int i = 0; i < 5; i++)
		{
			if(game.teams[i].status == 1)
			{
				LocalUser.SetTeam = true;
				break;
			}
		}
		
		if(LocalUser.SetTeam == false)
		{
			GoToConferencePanel.gameObject.SetActive(true);
			return;
		}
		
		LocalUser.TotalTeam = 0;
		Debug.Log("before goto Battle: " + LocalUser.Soldier.Count);
		
		for(int i = 0; i < LocalUser.Soldier.Count; i++)
		{
			if(LocalUser.Soldier[i].Max_Health > 0)
				LocalUser.TotalTeam++;
		}
		
		
		Debug.Log("TotalTeam: " + LocalUser.TotalTeam);
		
		

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

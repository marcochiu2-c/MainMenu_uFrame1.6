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
//using Endgame;


public class MenuScreenView : MenuScreenViewBase
{
	//public GameObject DisablePanel;
	//public Button LevelSelectButton;
    //public Button SettingsButton;
    //public Button ExitButton;
//	public Transform heading;
	public static Button NoticeButton;
	//public Button HeadButton;
	public static Button CardButton;
	public static Button SetBattleButton;
	public static Button CharPageButton;
	public static Button ShopButton;
	public static Button AcademyButton;
	public static Button ArtisanButton;
	public static Button TrainButton;
	public static Button ConferenceButton;
	public static Button ParallelButton;
	public static Button CompanionButton;
	public static Button TechnologyTreeButton;
	public static Button StorageButton;
	public static Button BuySFButton;
	public static Button BuyRButton;
	public static Button BuySDButton;
	public static Button SchoolFieldButton;
	
//	public Button SampleButton;
	
	public static MainScene MainScene;
	public UserViewModel LocalUser;

	public GameObject StorageHolder;
//	public Endgame.ListView ItemPanel;
//	public static Endgame.ListView staticItemPanel;
//	public Endgame.ListView AcademyListView;
//	public static Endgame.ListView staticAcademyListView;
//	public static ListView.ColumnHeaderCollection staticAcademyListViewColumns;
	//public Image TestArea;

    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
//		MenuScreenView.staticAcademyListViewColumns  = AcademyListView.Columns;

		Debug.Log ("InitializeViewModel()");
		AssignGameObjectVariable ();
		LocalUser =  uFrameKernel.Container.Resolve<UserViewModel>("LocalUser");
	}

	public void AssignGameObjectVariable(){

		MainScene = GameObject.Find ("MainUIHolder").GetComponent<MainScene> ();
		Transform BuildingHolder = MainScene.transform.GetChild (0).GetChild (0);
		NoticeButton = BuildingHolder.GetChild (1).GetChild (2).GetComponent<Button> ();
		CardButton = BuildingHolder.GetChild (0).GetChild (1).GetComponent<Button> ();
		SetBattleButton = BuildingHolder.GetChild (1).GetChild (0).GetComponent<Button> ();
		CharPageButton = ScreenUIContainer.transform.parent.GetChild(1).GetChild (0).GetComponent<Button> ();
		ShopButton = BuildingHolder.GetChild (0).GetChild (2).GetComponent<Button> ();

		AcademyButton = BuildingHolder.GetChild (2).Find ("Academy").GetComponent<Button> ();
		ArtisanButton = BuildingHolder.GetChild (2).Find ("Artisan").GetComponent<Button> ();
		TrainButton = BuildingHolder.GetChild (2).Find ("Train").GetComponent<Button> ();
		SchoolFieldButton = BuildingHolder.GetChild (2).Find ("SchoolField").GetComponent<Button> ();
		ConferenceButton = BuildingHolder.GetChild(1).Find("Conference").GetComponent<Button> ();
		ParallelButton = BuildingHolder.GetChild(2).Find("Parallel").GetComponent<Button> ();
		CompanionButton = BuildingHolder.GetChild(1).Find("Companion").GetComponent<Button> ();
		TechnologyTreeButton = BuildingHolder.GetChild (0).Find ("TechnologyTree").GetComponent<Button> ();
		StorageButton = BuildingHolder.GetChild (0).Find ("Storage").GetComponent<Button> ();
		BuySFButton = ScreenUIContainer.transform.GetChild (0).GetChild (2).GetComponent<Button> ();
		BuyRButton = ScreenUIContainer.transform.GetChild (1).GetChild (2).GetComponent<Button> ();
		BuySDButton = ScreenUIContainer.transform.GetChild (2).GetChild (2).GetComponent<Button> ();

	}
    
    public override void Bind() {
        base.Bind();

        // Bind each button to handler:
        // When button is clicked, handler is excuted
        // Ex: When we press LevelSelectButton, we publish
        // RequestMainMenuScreenCommand and pass LevelSelectScreenViewModel type
		var evt = new RequestMainMenuScreenCommand();
		var evtPopUp = new NotifyCommand();
		
		/*
        this.BindButtonToHandler(LevelSelectButton, () =>
        {
			evt.ScreenType = typeof(LevelSelectScreenViewModel);
			Publish(evt);
			//Publish(new RequestMainMenuScreenCommand()
            //{
            //    ScreenType = typeof(LevelSelectScreenViewModel)
            //});
        });

        this.BindButtonToHandler(SettingsButton, () =>
        {
			evt.ScreenType = typeof(SettingsScreenViewModel);
			Publish(evt);
        });
        */

		this.BindButtonToHandler(NoticeButton, () =>
		{
			evt.ScreenType = typeof(NoticeScreenViewModel);
			Publish(evt);
		});
		
		/*
		this.BindButtonToHandler(HeadButton, () =>
		{
			evt.ScreenType = typeof(SampleScreenViewModel);
			Publish(evt);
		});
		*/

		this.BindButtonToHandler(CardButton, () =>
			{
				evt.ScreenType = typeof(CardScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(CharPageButton, () =>
			{
				evt.ScreenType = typeof(CharPageScreenViewModel);
				Publish(evt);
			});
		
		this.BindButtonToHandler(ShopButton, () =>
			{
				evt.ScreenType = typeof(ShopScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(BuySFButton, () =>
			{
				evt.ScreenType = typeof(ShopScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(BuyRButton, () =>
			{
				evt.ScreenType = typeof(ShopScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(BuySDButton, () =>
			{

				evt.ScreenType = typeof(ShopScreenViewModel);
				Publish(evt);
			});
		
		this.BindButtonToHandler(AcademyButton, () =>
		    {
//				MenuScreenView.staticAcademyListView = AcademyListView;
//				MenuScreenView.staticAcademyListViewColumns  = MenuScreenView.staticAcademyListVie/w.Columns;

				evt.ScreenType = typeof(AcademyScreenViewModel);
				Publish(evt);
//			Academy academy = new Academy();
//			academy.CallAcademy();
			});

		this.BindButtonToHandler(ArtisanButton, () =>
			{
				evt.ScreenType = typeof(ArtisanScreenViewModel);
				Publish(evt);
			});
		
		this.BindButtonToHandler(TrainButton, () =>
			{
				evt.ScreenType = typeof(TrainScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(ConferenceButton, () =>
			{
				evt.ScreenType = typeof(ConferenceScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(ParallelButton, () =>
			{
				evt.ScreenType = typeof(ParallelScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(CompanionButton, () =>
			{
				evt.ScreenType = typeof(CompanionScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler(TechnologyTreeButton, () =>
			{
				evt.ScreenType = typeof(TechnologyTreeScreenViewModel);
				Publish(evt);
			});

		this.BindButtonToHandler (StorageButton, () => 
			{
				evt.ScreenType = typeof(StorageScreenViewModel);
				Publish (evt);

			});

		this.BindButtonToHandler (SchoolFieldButton, () => 
			{
				evt.ScreenType = typeof(SchoolFieldScreenViewModel);
				Publish (evt);
			});
		
		//Temp use
		this.BindButtonToHandler(SetBattleButton, () =>
			{

				evt.ScreenType = typeof(SetBattleScreenViewModel);
				Publish(evt);
				/*
				Publish(new UnloadSceneCommand()
					{
						SceneName = "MainMenuScene" // Unload  main menu scene
					});

				Publish(new LoadSceneCommand()
					{
						//SceneName = arg.LevelScene // Load level scene
						SceneName = "MainGameScene" // Load level scene
					});
				*/
				
				//Publish(new MainMenuFinishedEvent());
			});

//		this.BindButtonToHandler(SampleButton, () =>
//			{
//				evt.ScreenType = typeof(SampleScreenViewModel);
//				Publish(evt);
//			});
        // This follows the same logic, but we use Method Group syntax.
        // And we do not publish event. We just quit.
		//this.BindButtonToHandler(ExitButton, Application.Quit);
        //Equivalent to 
        //this.BindButtonToHandler(ExitButton, () => { Application.Quit; });

		//this.BindButtonToHandler(TryButton, Application.Quit);
    }

	public override void IsActiveChanged(Boolean active)
	{
		base.IsActiveChanged(active);
		//DisablePanel.gameObject.SetActive(!active);
		Debug.Log ("MenuScreen is active? " + active + "LocalUser ScreenState: " + LocalUser.ScreenState);
		
		if (active == true && LocalUser.ScreenState == ScreenState.MainGame)
		{
			Debug.Log ("call reloadFromDB from MainScreenView");
			MainScene.needReloadFromDB = true;
			MainScene.CallMainScene();
			//MainScene.reloadFromDB();
			LocalUser.ScreenState = ScreenState.MainMenu;
		}
			

	}


}

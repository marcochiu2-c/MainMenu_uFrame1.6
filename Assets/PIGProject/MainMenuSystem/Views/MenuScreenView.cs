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
	public Button NoticeButton;
	//public Button HeadButton;
	public Button CardButton;
	public Button SetBattleButton;
	public Button CharPageButton;
	public Button ShopButton;
	public Button AcademyButton;
	public Button ArtisanButton;
	public Button TrainButton;
	public Button ConferenceButton;
	public Button ParallelButton;
	public Button CompanionButton;
	public Button TechnologyTreeButton;
	public Button StorageButton;
	public Button BuySFButton;
	public Button BuyRButton;
	public Button BuySDButton;
	public Button SchoolFieldButton;
	
	public Button SampleButton;

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

		this.BindButtonToHandler(SampleButton, () =>
			{
				evt.ScreenType = typeof(SampleScreenViewModel);
				Publish(evt);
			});
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
	}


}

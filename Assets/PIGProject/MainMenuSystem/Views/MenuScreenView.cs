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


public class MenuScreenView : MenuScreenViewBase
{
	public GameObject DisablePanel;
	public Button LevelSelectButton;
    public Button SettingsButton;
    public Button ExitButton;
	public Button NoticeButton;
	public Button HeadButton;
	public Button CardButton;
	public Button SetBattleButton;
	//public Image TestArea;

    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
    }
    
    public override void Bind() {
        base.Bind();

        // Bind each button to handler:
        // When button is clicked, handler is excuted
        // Ex: When we press LevelSelectButton, we publish
        // RequestMainMenuScreenCommand and pass LevelSelectScreenViewModel type
		var evt = new RequestMainMenuScreenCommand();
		var evtPopUp = new NotifyCommand();

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

		this.BindButtonToHandler(NoticeButton, () =>
		{
			evt.ScreenType = typeof(NoticeScreenViewModel);
			Publish(evt);
		});

		this.BindButtonToHandler(HeadButton, () =>
		{
			evt.ScreenType = typeof(SampleScreenViewModel);
			Publish(evt);
		});

		this.BindButtonToHandler(CardButton, () =>
			{
				evt.ScreenType = typeof(CardScreenViewModel);
				Publish(evt);
			});

		//Temp use
		this.BindButtonToHandler(SetBattleButton, () =>
			{
				
				Publish(new UnloadSceneCommand()
					{
						SceneName = "MainMenuScene" // Unload  main menu scene
					});

				Publish(new LoadSceneCommand()
					{
						//SceneName = arg.LevelScene // Load level scene
						SceneName = "MainGameScene" // Load level scene
					});
				
				//Publish(new MainMenuFinishedEvent());
			});
		
        // This follows the same logic, but we use Method Group syntax.
        // And we do not publish event. We just quit.
		this.BindButtonToHandler(ExitButton, Application.Quit);
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

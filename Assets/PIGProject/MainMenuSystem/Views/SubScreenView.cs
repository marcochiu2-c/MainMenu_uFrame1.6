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
using DG.Tweening;
using uFrame.IOC;


/*
 * This view serves as a base class for all the SubScreen views
 * It handles screen activation/deactivation.
 * It also handles binding for Close command. You can configure it using the inspector.
 */
public class SubScreenView : SubScreenViewBase
{

    public GameObject ScreenUIContainer;
	[Inject("LocalUser")] public UserViewModel LocalUser;

    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
    }

    public override void Bind() {
        base.Bind();
    }

    public override void IsActiveChanged(Boolean active)
    {

        /* 
         * Always make sure, that you cache components used in the binding handlers BEFORE you actually bind.
         * This is important, since when Binding to the viewmodel, Handlers are invoked immidiately
         * with the current values, to ensure that view state is consistant.
         * For example, you can do this in Awake or in KernelLoading/KernelLoaded.
         * However, in this example we simply use public property to get a reference to ScreenUIContainer.
         * So we do not have to cache anything.
         */
		//Default 
		//ScreenUIContainer.gameObject.SetActive(active);
		
		///<summary>
		/// 
		///</summary>
		//Debug.Log(ScreenUIContainer.name);
		

		if(ScreenUIContainer.name == "LoginScreenPanel"){
			ScreenUIContainer.gameObject.SetActive(active);
		}

		//if(LocalUser != null && LocalUser.AuthorizationState == AuthorizationState.Authorized){

		if (ScreenUIContainer.name == "MainMenuPanel"){
				ScreenUIContainer.gameObject.SetActive(true);
				//Debug.Log ("Hello from MainMenuPanel codition");
			}

		else if (ScreenUIContainer.name == "LevelSelectPanel"){
				ScreenUIContainer.gameObject.SetActive(active);
				//Debug.Log ("Hello from MainMenuPanel codition");
		}

		else if(active){
			if(ScreenUIContainer.name == "SettingsPanel")
				ScreenUIContainer.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBack).OnStart(()=>ScreenUIContainer.gameObject.SetActive(true));
			else
				ScreenUIContainer.transform.DOMove(new Vector3(134, 53, -2.5f), 0.5f).SetEase(Ease.OutSine).OnStart(()=>ScreenUIContainer.gameObject.SetActive(true));
			//Debug.Log (ScreenUIContainer.name + " actived");
		}
		else{
			if(ScreenUIContainer.name == "SettingsPanel")
						ScreenUIContainer.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack).OnComplete(()=>ScreenUIContainer.gameObject.SetActive(false));
			else
						ScreenUIContainer.transform.DOMove(new Vector3(134, -80, -2.5f), 0.5f).SetEase(Ease.OutSine).OnComplete(()=>ScreenUIContainer.gameObject.SetActive(false));
			//Debug.Log (ScreenUIContainer.name + " not actived");
		}
		//}						
	}
}
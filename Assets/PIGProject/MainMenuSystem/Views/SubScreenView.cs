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



/*
 * This view serves as a base class for all the SubScreen views
 * It handles screen activation/deactivation.
 * It also handles binding for Close command. You can configure it using the inspector.
 */
public class SubScreenView : SubScreenViewBase
{

    public GameObject ScreenUIContainer;


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
			return;
		}

		//else if(LocalUser.AuthorizationState == AuthorizationState.Authorized){

		else if (ScreenUIContainer.name == "MainMenuPanel"){
				ScreenUIContainer.gameObject.SetActive(active);

			}

		else if(active){
			ScreenUIContainer.gameObject.SetActive(active);
			ScreenUIContainer.transform.DOMove(new Vector3(120, 61, 4), 1, true).SetEase(Ease.OutSine);
			Debug.Log (ScreenUIContainer.name + " actived");
		}
		else{
			ScreenUIContainer.transform.DOMove(new Vector3(300, 61, 4), 1, true).SetEase(Ease.OutSine).OnComplete(()=>HidePanel(active));
			Debug.Log (ScreenUIContainer.name + " not actived");
		}
	}

	private void HidePanel(Boolean active){
		ScreenUIContainer.gameObject.SetActive(active);
		Debug.Log (ScreenUIContainer.name + " run from HidePanel");
	}
	
}
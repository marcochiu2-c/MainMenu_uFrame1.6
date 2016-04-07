#define TEST
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
using Utilities;


/*
 * This view serves as a base class for all the SubScreen views
 * It handles screen activation/deactivation.
 * It also handles binding for Close command. You can configure it using the inspector.
 */
public class SubScreenView : SubScreenViewBase
{

    public GameObject ScreenUIContainer;
	public AudioSource bgm;
	
	[Inject("LocalUser")] public UserViewModel LocalUser;
	public AudioSource bgm;
	float time = 0.5f;

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
		
		/*
		if(ScreenUIContainer.name == "LoginScreenPanel"){
			ScreenUIContainer.gameObject.SetActive(active);
		}
		*/

		//if(LocalUser != null && LocalUser.AuthorizationState == AuthorizationState.Authorized){

		/*
		if (ScreenUIContainer.name == "MainMenuPanel"){
				ScreenUIContainer.gameObject.SetActive(true);
				//Debug.Log ("Hello from MainMenuPanel codition");
			}
		

		if (ScreenUIContainer.name == "LevelSelectPanel"){
			ScreenUIContainer.gameObject.SetActive(active);
				//Debug.Log ("Hello from MainMenuPanel codition");
		}
		*/

		if(active)
		{
<<<<<<< HEAD
			if (ScreenUIContainer.name == "HeaderHolder")
			{
				bgm.Play();
				return;
			}
			ScreenUIContainer.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBack).OnStart(()=>
			{
				ScreenUIContainer.gameObject.SetActive(true);
				bgm.Play ();
			});
			//.OnComplete(() => bgm.Play ());
			
=======
			if (ScreenUIContainer.name == "MainUIHolder") return;
//			if (ScreenUIContainer.name == "AcademyHolder") return;
#if (TEST)
			ScreenUIContainer.transform.DOLocalMoveY(2f, time).SetEase(Ease.InOutBack).OnStart(()=>{
				Debug.Log ("DoLocalMoveY called");
				ScreenUIContainer.gameObject.SetActive(true);
				bgm.Play ();
			});
#else
			//ScreenUIContainer.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBack).OnStart(()=>ScreenUIContainer.gameObject.SetActive(true));
>>>>>>> feature/MainMenu-shawn
			//Debug.Log (ScreenUIContainer.name + " actived");

#endif
		}
		else
		{
<<<<<<< HEAD
			if (ScreenUIContainer.name == "HeaderHolder")
			{
				bgm.Stop();
				return;
			}
			ScreenUIContainer.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack).OnStart(() => bgm.Stop ()).OnComplete(()=>ScreenUIContainer.gameObject.SetActive(false));
=======
			if (ScreenUIContainer.name == "MainUIHolder") return;
//			if (ScreenUIContainer.name == "AcademyHolder") return;
#if (TEST)	
//			ShowLog.Log ("SubScreen Close");
			ScreenUIContainer.transform.DOLocalMoveY(759f, time).SetEase(Ease.InOutBack).OnStart(()=>{
				bgm.Play ();

				if (ScreenUIContainer.gameObject.ToString()=="AcademyHolder (UnityEngine.GameObject)"){
					Debug.Log (ScreenUIContainer.gameObject);
				}else{
					ScreenUIContainer.gameObject.SetActive(false);
				}
			});
#else
			//ScreenUIContainer.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack).OnComplete(()=>ScreenUIContainer.gameObject.SetActive(false));
#endif
>>>>>>> feature/MainMenu-shawn
		}
					
	}
}
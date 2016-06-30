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


public class CompanionScreenView : CompanionScreenViewBase {

	public static Button counselorsButton;
	public static Button soldiersButton;

	public static Button backButton;
	public static Button closeButton;

	public static GameObject CounselorsHolder;
	public static GameObject GeneralsHolder;
	public static GameObject CardHolder;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as CompanionScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		AssignGameObjectVariable ();
	}
	
	public void AssignGameObjectVariable(){
		Transform panel = ScreenUIContainer.transform.Find ("Panel");
		Transform popupHolder = panel.Find ("PopupHolder");
		Transform btnHolder = panel.Find ("ButtonHolder");
		
		CounselorsHolder = popupHolder.Find ("CounselorsHolder").gameObject;
		GeneralsHolder = popupHolder.Find ("GeneralsHolder").gameObject;
		CardHolder = ScreenUIContainer.transform.Find ("CardHolder").gameObject;
		
		counselorsButton = btnHolder.Find ("CounselorsButton").GetComponent<Button>();
		soldiersButton = btnHolder.Find ("GeneralButton").GetComponent<Button>();
		backButton = ScreenUIContainer.transform.Find ("BackButton").GetComponent<Button>();
		closeButton = ScreenUIContainer.transform.Find ("CloseButton").GetComponent<Button>();
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.CompanionScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
		this.BindButtonToHandler (backButton, () => {
			if (CardHolder.activeSelf){
				CardHolder.SetActive(false);
			}else{
				CounselorsHolder.SetActive (false);
				GeneralsHolder.SetActive (false);
			}
		});
		
		this.BindButtonToHandler (closeButton, () => {
			CounselorsHolder.SetActive (false);
			GeneralsHolder.SetActive (false);
			CardHolder.SetActive(false);
			gameObject.SetActive(false);
		});

		this.BindButtonToHandler (counselorsButton, () => {
			CounselorsHolder.gameObject.SetActive (true);
			GeneralsHolder.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (soldiersButton, () => {
			GeneralsHolder.gameObject.SetActive (true);
			CounselorsHolder.gameObject.SetActive (false);
		});

    }
}

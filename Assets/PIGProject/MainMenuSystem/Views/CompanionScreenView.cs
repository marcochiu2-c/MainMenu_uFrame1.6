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

	public Button counselorsButton;
	public Button soldiersButton;
	public Button backButton;
	public Button closeButton;

	public GameObject CounselorsHolder;
	public GameObject GeneralsHolder;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as CompanionScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.CompanionScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
		this.BindButtonToHandler (backButton, () => {
			CounselorsHolder.gameObject.SetActive (false);
			GeneralsHolder.gameObject.SetActive (false);

		});
		this.BindButtonToHandler (closeButton, () => {
			CounselorsHolder.gameObject.SetActive (false);
			GeneralsHolder.gameObject.SetActive (false);
			gameObject.SetActive(false);
		});

		this.BindButtonToHandler (counselorsButton, () => {
			CounselorsHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (soldiersButton, () => {
			GeneralsHolder.gameObject.SetActive (true);
		});

    }
}

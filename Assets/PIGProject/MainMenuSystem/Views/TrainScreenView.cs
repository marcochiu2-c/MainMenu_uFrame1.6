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


public class TrainScreenView : TrainScreenViewBase {

	public Button CourageButton;
	public Button ForceButton;
	public Button StrengthButton;

	public GameObject CouragePopup;
	public GameObject ForcePopup;
	public GameObject StrengthPopup;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as TrainScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.TrainScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (CourageButton, () => {
			CouragePopup.gameObject.SetActive (true);
			ForcePopup.gameObject.SetActive (false);
			StrengthPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (ForceButton, () => {
			CouragePopup.gameObject.SetActive (false);
			ForcePopup.gameObject.SetActive (true);
			StrengthPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (StrengthButton, () => {
			CouragePopup.gameObject.SetActive (false);
			ForcePopup.gameObject.SetActive (false);
			StrengthPopup.gameObject.SetActive (true);
		});
    }
}

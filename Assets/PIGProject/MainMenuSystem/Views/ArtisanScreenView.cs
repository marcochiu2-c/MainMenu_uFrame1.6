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


public class ArtisanScreenView : ArtisanScreenViewBase {

	public Button armsButton;
	public Button armorButton;
	public Button shieldButton;
	public Button backButton;

	public GameObject armsPopup;
	public GameObject armorPopup;
	public GameObject shieldPopup;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ArtisanScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.ArtisanScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (armsButton, () => {
			armsPopup.SetActive (true);
			armorPopup.SetActive (false);
			shieldPopup.SetActive (false);
			ArtisanHolder.OpenedHolder = 1;
		});

		this.BindButtonToHandler (armorButton, () => {
			armsPopup.SetActive (false);
			armorPopup.SetActive (true);
			shieldPopup.SetActive (false);
			ArtisanHolder.OpenedHolder = 2;
		});

		this.BindButtonToHandler (shieldButton, () => {
			armsPopup.SetActive (false);
			armorPopup.SetActive (false);
			shieldPopup.SetActive (true);
			ArtisanHolder.OpenedHolder = 3;
		});

		this.BindButtonToHandler (backButton,()=>{
			armsPopup.SetActive (false);
			armorPopup.SetActive (false);
			shieldPopup.SetActive (false);
		});
    }
}

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


public class StorageScreenView : StorageScreenViewBase {

	public Button weaponButton;
	public Button armorButton;
	public Button sheildButton;
	public Button mountsButton;

	public GameObject weaponGrid;
	public GameObject armorGrid;
	public GameObject sheildGrid;
	public GameObject mountsGrid;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as StorageScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.StorageScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (weaponButton, () => {
			weaponGrid.gameObject.SetActive (true);
			armorGrid.gameObject.SetActive (false);
			sheildGrid.gameObject.SetActive (false);
			mountsGrid.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (armorButton, () => {
			weaponGrid.gameObject.SetActive (false);
			armorGrid.gameObject.SetActive (true);
			sheildGrid.gameObject.SetActive (false);
			mountsGrid.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (sheildButton, () => {
			weaponGrid.gameObject.SetActive (false);
			armorGrid.gameObject.SetActive (false);
			sheildGrid.gameObject.SetActive (true);
			mountsGrid.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (mountsButton, () => {
			weaponGrid.gameObject.SetActive (false);
			armorGrid.gameObject.SetActive (false);
			sheildGrid.gameObject.SetActive (false);
			mountsGrid.gameObject.SetActive (true);
		});

    }
}

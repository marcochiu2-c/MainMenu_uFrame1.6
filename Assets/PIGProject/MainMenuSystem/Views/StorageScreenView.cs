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

	public Button WeaponButton;
	public Button ArmorButton;
	public Button SheildButton;
	public Button MountsButton;

	public GameObject WeaponGridList;
	public GameObject ArmorGridList;
	public GameObject SheildGridList;
	public GameObject MountsGridList;
    
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

		this.BindButtonToHandler (WeaponButton, () => {
			WeaponGridList.gameObject.SetActive (true);
			ArmorGridList.gameObject.SetActive (false);
			SheildGridList.gameObject.SetActive (false);
			MountsGridList.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (ArmorButton, () => {
			WeaponGridList.gameObject.SetActive (false);
			ArmorGridList.gameObject.SetActive (true);
			SheildGridList.gameObject.SetActive (false);
			MountsGridList.gameObject.SetActive (false);
		});


		this.BindButtonToHandler (SheildButton, () => {
			WeaponGridList.gameObject.SetActive (false);
			ArmorGridList.gameObject.SetActive (false);
			SheildGridList.gameObject.SetActive (true);
			MountsGridList.gameObject.SetActive (false);
		});


		this.BindButtonToHandler (MountsButton, () => {
			WeaponGridList.gameObject.SetActive (false);
			ArmorGridList.gameObject.SetActive (false);
			SheildGridList.gameObject.SetActive (false);
			MountsGridList.gameObject.SetActive (true);
		});
    }
}

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

	public static Button armsButton;
	public static Button armorButton;
	public static Button shieldButton;
	public static Button backButton;

	public static GameObject armsPopup;
	public static GameObject armorPopup;
	public static GameObject shieldPopup;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ArtisanScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		AssignGameObjectVariable ();
	}
	
	public void AssignGameObjectVariable(){
		Transform holder = ScreenUIContainer.transform.Find ("Holder");
		Transform popupHolder = holder.Find ("PopupHolder");
		Transform btnHolder = popupHolder.Find ("ButtonHolder");

		armsPopup = popupHolder.Find ("ArtisanArmsPopup").gameObject;
		armorPopup = popupHolder.Find ("ArmorPopup").gameObject;
		shieldPopup = popupHolder.Find ("ShieldPopup").gameObject;

		armsButton = btnHolder.Find ("ArmsButton").GetComponent<Button>();
		armorButton = btnHolder.Find ("ArmorButton").GetComponent<Button>();
		shieldButton = btnHolder.Find ("ShieldButton").GetComponent<Button>();
		backButton = ScreenUIContainer.transform.Find ("BackButton").GetComponent<Button>();
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

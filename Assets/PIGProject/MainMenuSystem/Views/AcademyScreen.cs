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


public class AcademyScreeView : AcademyScreenViewBase {

	public Button IQButton;
	public Button CommandedButton;
	public Button KnowledgeButton;
	public Button FightingButton;

	public GameObject IQPopup;
	public GameObject CommandPopup;
	public GameObject KnowledgePopup;
	public GameObject FightingPopup;
   
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as AcademyScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.AcademyScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (IQButton, () => {
			IQPopup.gameObject.SetActive (true);
			CommandPopup.gameObject.SetActive (false);
			KnowledgePopup.gameObject.SetActive (false);
			FightingPopup.gameObject.SetActive (false);
		});
		this.BindButtonToHandler (CommandedButton, () => {
			IQPopup.gameObject.SetActive (false);
			CommandPopup.gameObject.SetActive (true);
			KnowledgePopup.gameObject.SetActive (false);
			FightingPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (KnowledgeButton, () => {
			IQPopup.gameObject.SetActive (false);
			CommandPopup.gameObject.SetActive (false);
			KnowledgePopup.gameObject.SetActive (true);
			FightingPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (FightingButton, () => {
			IQPopup.gameObject.SetActive (false);
			CommandPopup.gameObject.SetActive (false);
			KnowledgePopup.gameObject.SetActive (false);
			FightingPopup.gameObject.SetActive (true);
		});
    }
}

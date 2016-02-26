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


public class AcademyScreenView : AcademyScreenViewBase {

	public Button BackButton;

	public Button IQButton;
	public Button CommandedButton;
	public Button KnowledgeButton;
	public Button FightingButton;

	public Button SelfStudyButton;
	public Button TeachButton;

	public GameObject QAHolder;

	public GameObject SelfStudyHolder;
	public GameObject TeachHolder;
	//public GameObject KnowledgePopup;
	//public GameObject FightingPopup;
	    
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

		this.BindButtonToHandler (BackButton, () =>
		{
			SelfStudyHolder.gameObject.SetActive (false);
			TeachHolder.gameObject.SetActive (false);
			QAHolder.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (IQButton, () =>
		{
			QAHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (CommandedButton, () =>
		{
			QAHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (KnowledgeButton, () =>
		{
			QAHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (FightingButton, () =>
		{
			QAHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (SelfStudyButton, () =>
		{
			SelfStudyHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (TeachButton, () =>
		{
			TeachHolder.gameObject.SetActive (true);
		});

    }
}

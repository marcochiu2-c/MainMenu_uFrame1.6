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

	public Button IQButton;
	public Button CommandedButton;
	public Button KnowledgeButton;
	public Button FightingButton;
	public Button BackButton;
	public Button CloseButton;
	public Button SelfStudyButton;
	public Button TeachButton;


	public GameObject qaHolder;

	public GameObject SelfStudyHolder;
	public GameObject TeachHolder;
	public Text SelfStudyPopupTitle;
	public Text TeachPopupTitle;

	


	public GameObject Popup;
//	public GameObject CommandedPopup;
//	public GameObject KnowledgePopup;
//	public GameObject FightingPopup;
    
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
			qaHolder.SetActive(true);
			SelfStudyPopupTitle.text = "智商";
			TeachPopupTitle.text = "智商";

			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.IQPopup;
//			CommandedPopup.gameObject.SetActive (false);
//			KnowledgePopup.gameObject.SetActive (false);
//			FightingPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (CommandedButton, () => {
			qaHolder.SetActive(true);
			SelfStudyPopupTitle.text = "統率";
			TeachPopupTitle.text = "統率";

			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.CommandedPopup;
//			CommandedPopup.gameObject.SetActive (true);
//			KnowledgePopup.gameObject.SetActive (false);
//			FightingPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (KnowledgeButton, () => {
			qaHolder.SetActive(true);	
			SelfStudyPopupTitle.text = "學問";
			TeachPopupTitle.text = "學問";

			// TODO: move next statement to model code
			Academy.activePopup = ActivePopupEnum.KnowledgePopup;
//			CommandedPopup.gameObject.SetActive (false);
//			KnowledgePopup.gameObject.SetActive (true);
//			FightingPopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (FightingButton, () => {
			qaHolder.SetActive(true);
			SelfStudyPopupTitle.text = "陣法";
			TeachPopupTitle.text = "陣法";

			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.FightingPopup;
//			CommandedPopup.gameObject.SetActive (false);
//			KnowledgePopup.gameObject.SetActive (false);
//			FightingPopup.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (BackButton, () => {
			SelfStudyHolder.SetActive (false);
			TeachHolder.SetActive (false);

			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.none;
		});

		this.BindButtonToHandler (CloseButton, () => {
			SelfStudyHolder.SetActive (false);
			TeachHolder.SetActive (false);
			qaHolder.SetActive(false);
			
			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.none;
		});
		this.BindButtonToHandler (SelfStudyButton, () => {
			Debug.Log ("SelfStudyButton clicked");
			qaHolder.SetActive(false);
			SelfStudyHolder.SetActive (true);
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (true);
		});
		this.BindButtonToHandler (TeachButton, () => {
			Debug.Log ("TeachButton clicked");
			qaHolder.SetActive(false);
			TeachHolder.SetActive (true);
			
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (true);
		});
    }
}

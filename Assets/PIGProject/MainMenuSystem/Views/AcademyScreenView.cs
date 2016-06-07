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
	
	public static Button IQButton;
	public static Button CommandedButton;
	public static Button KnowledgeButton;
	public static Button FightingButton;
	public static Button BackButton;
	public static Button CloseButton;
	public static Button SelfStudyButton;
	public static Button TeachButton;
	
	public static GameObject DisablePanel;
	public static GameObject qaHolder;
	
	public static GameObject SelfStudyHolder;
	public static GameObject TeachHolder;
	public static Text SelfStudyPopupTitle;
	public static Text TeachPopupTitle;
	//	public Text button5;
	
	Academy academy;

	//	public GameObject CommandedPopup;
	//	public GameObject KnowledgePopup;
	//	public GameObject FightingPopup;
	
	protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
		base.InitializeViewModel(model);
		// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
		// var vm = model as AcademyScreenViewModel;
		// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		AssignGameObjectVariable ();
	}

	public void AssignGameObjectVariable(){
		Transform panel = ScreenUIContainer.transform.Find ("Panel");
		Transform btnHolder = panel.Find ("ButtonHolder");
		DisablePanel = panel.Find ("DisablePanel").gameObject;
		qaHolder = panel.Find ("QAHolder").gameObject;
		SelfStudyHolder = panel.Find ("SelfStudyHolder").gameObject;
		TeachHolder = panel.Find ("TeachHolder").gameObject;
		IQButton = btnHolder.Find ("IQButton").GetComponent<Button> ();
		CommandedButton = btnHolder.Find ("CommandedButton").GetComponent<Button> ();
		KnowledgeButton = btnHolder.Find ("KnowledgeButton").GetComponent<Button> ();
		FightingButton = btnHolder.Find ("FightingButton").GetComponent<Button> ();

		SelfStudyButton = qaHolder.transform.GetChild (1).GetChild (0).GetComponent<Button> ();
		TeachButton = qaHolder.transform.GetChild (1).GetChild (1).GetComponent<Button> ();

		SelfStudyPopupTitle = SelfStudyHolder.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		TeachPopupTitle = Utilities.Panel.GetHeader (TeachHolder);

		BackButton = ScreenUIContainer.transform.Find ("BackButton").GetComponent<Button>();
		CloseButton = ScreenUIContainer.transform.Find ("CloseButton").GetComponent<Button>();
	}
	
	public override void Bind() {
		base.Bind();
		// Use this.AcademyScreen to access the viewmodel.
		// Use this method to subscribe to the view-model.
		// Any designer bindings are created in the base implementation.
		
		this.BindButtonToHandler (IQButton, () => {
			qaHolder.SetActive(true);
			SelfStudyPopupTitle.text = "智 商";
			TeachPopupTitle.text = "智 商";
			//button5.supportRichText = true;
			//button5.text = "<color=red>Test</color>";
			
			
			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.IQPopup;
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (CommandedButton, () => {
			qaHolder.SetActive(true);
			SelfStudyPopupTitle.text = "統 率";
			TeachPopupTitle.text = "統 率";
			
			
			// TODO: move next statement to Model code
			Academy.activePopup = ActivePopupEnum.CommandedPopup;
			//			CommandedPopup.gameObject.SetActive (true);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (KnowledgeButton, () => {
			qaHolder.SetActive(true);	
			SelfStudyPopupTitle.text = "學 問";
			TeachPopupTitle.text = "學 問";
			
			// TODO: move next statement to model code
			Academy.activePopup = ActivePopupEnum.KnowledgePopup;
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (true);
			//			FightingPopup.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (FightingButton, () => {
			qaHolder.SetActive(true);
			SelfStudyPopupTitle.text = "陣 法";
			TeachPopupTitle.text = "陣 法";
			
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
			TeachHolder.transform.localScale = new Vector3(1, 1, 1);
			//			academy =  new Academy();
			//			academy.SetDataGrid();
			//			academy.RemoveAllItems();
			
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (true);
		});
	}
}
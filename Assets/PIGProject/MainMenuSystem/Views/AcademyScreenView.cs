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
	public static GameObject AcademyType;
	
//	public static GameObject SelfStudyHolder;
//	public static GameObject TeachHolder;
//	public static Text SelfStudyPopupTitle;
//	public static Text TeachPopupTitle;
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
		Transform panel = ScreenUIContainer.transform.Find ("NewLayout");
		Transform StudentSelector = panel.Find ("CounselorSelector");
		Transform btnHolder = panel.Find ("AcademySelector");
		DisablePanel = panel.Find ("DisablePanel").gameObject;
		AcademyType = panel.Find ("AcademyType").gameObject;
		IQButton = btnHolder.Find ("Button").GetComponent<Button> ();
		CommandedButton = btnHolder.Find ("Button (1)").GetComponent<Button> ();
		KnowledgeButton = btnHolder.Find ("Button (2)").GetComponent<Button> ();
		FightingButton = btnHolder.Find ("Button (3)").GetComponent<Button> ();

		SelfStudyButton = AcademyType.transform.GetChild (0).GetChild (0).GetComponent<Button> ();
		TeachButton = AcademyType.transform.GetChild (0).GetChild (1).GetComponent<Button> ();

//		SelfStudyPopupTitle = SelfStudyHolder.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
//		TeachPopupTitle = Utilities.Panel.GetHeader (TeachHolder);

		BackButton = ScreenUIContainer.transform.Find ("BackButton").GetComponent<Button>();
		CloseButton = ScreenUIContainer.transform.Find ("CloseButton").GetComponent<Button>();
	}
	
	public override void Bind() {
		base.Bind();
		// Use this.AcademyScreen to access the viewmodel.
		// Use this method to subscribe to the view-model.
		// Any designer bindings are created in the base implementation.
		
		this.BindButtonToHandler (IQButton, () => {
//			SelfStudyPopupTitle.text = "智 商";
//			TeachPopupTitle.text = "智 商";
			//button5.supportRichText = true;
			//button5.text = "<color=red>Test</color>";
			
			
			// TODO: move next statement to Model code
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (CommandedButton, () => {
//			SelfStudyPopupTitle.text = "統 率";
//			TeachPopupTitle.text = "統 率";
			
			
			// TODO: move next statement to Model code
			//			CommandedPopup.gameObject.SetActive (true);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (KnowledgeButton, () => {
//			SelfStudyPopupTitle.text = "學 問";
//			TeachPopupTitle.text = "學 問";
			
			// TODO: move next statement to model code
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (true);
			//			FightingPopup.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (FightingButton, () => {
//			SelfStudyPopupTitle.text = "陣 法";
//			TeachPopupTitle.text = "陣 法";
			
			// TODO: move next statement to Model code
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (BackButton, () => {
//			SelfStudyHolder.SetActive (false);
//			TeachHolder.SetActive (false);
			// TODO: move next statement to Model code
		});
		
		this.BindButtonToHandler (CloseButton, () => {
//			SelfStudyHolder.SetActive (false);
//			TeachHolder.SetActive (false);
			
			// TODO: move next statement to Model code
		});
		this.BindButtonToHandler (SelfStudyButton, () => {
			Debug.Log ("SelfStudyButton clicked");
//			SelfStudyHolder.SetActive (true);
			
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (true);
		});
		this.BindButtonToHandler (TeachButton, () => {
			Debug.Log ("TeachButton clicked");
//			TeachHolder.transform.localScale = new Vector3(1, 1, 1);
			//			academy =  new Academy();
			//			academy.SetDataGrid();
			//			academy.RemoveAllItems();
			
			//			CommandedPopup.gameObject.SetActive (false);
			//			KnowledgePopup.gameObject.SetActive (false);
			//			FightingPopup.gameObject.SetActive (true);
		});
	}
}
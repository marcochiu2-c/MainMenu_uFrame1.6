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


public class SchoolFieldScreenView : SchoolFieldScreenViewBase {

	public static Button BackButton;
	public static Button CloseButton;
	public static Button SoldierButton;
	public static Button ArmyButton;
	public static Button MountButton;
	public static Button ArmorButton;
	public static Button ShieldButton;
	public static Button UnmountButton;
//	public Button BufferButton;
	public static Button TrainingButton;
	public static Button AssignSoldierButton;
	public static Button AssignSoldierConfirmButton;
	public static Button SoldierType1Button;
	public static Button SoldierType2Button;
	public static Button SoldierType3Button;
	public static Button SoldierType4Button;
	public static Button SoldierType5Button;
	public static Button SoldierType6Button;
	public static Button SoldierType7Button;
	public static Button SoldierType8Button;

	public static Button ArmyQAHolderCancelButton;
	public static Button ArmorQAHolderCancelButton;
	public static Button ShieldQAHolderCancelButton;
	public static Button closeTrainingButton;
	public static Button closeUnmountEquipmentButton;

	public static GameObject DisablePanel;
	public static GameObject ArmyListHolder;
	public static GameObject MountListHolder;
	public static GameObject ArmorListHolder;
	public static GameObject ShieldListHolder;
	//public GameObject ArmyQAHolder;
	//public GameObject MountQAHolder;
	//public GameObject ArmorQAHolder;
	//public GameObject ShieldQAHolder;
	public static GameObject UnmountEquipment;
	public static GameObject TrainingHolder;
	public static GameObject TrainingQHolder;
	public static GameObject TrainingQAHolder;
	public static GameObject TrainingEquHolder;
	public static GameObject TrainingEquConfirmAHolder;
	public static GameObject TrainingInProgress;
	public static GameObject ConfirmSpeedUpHolder;
	public static GameObject AssignNSHolder;
	public static GameObject ArmyQAHolder;
	public static GameObject ArmorQAHolder;
	public static GameObject ShieldQAHolder;
	public static GameObject DollPanel;
	public static GameObject DataPanel;
	public static GameObject NewSoldierPanel;
	
	protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
		base.InitializeViewModel(model);
		// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
		// var vm = model as SchoolFieldScreenViewModel;
		// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		AssignGameObjectVariable ();
	}

	public void AssignGameObjectVariable(){
//		ArmyButton = ScreenUIContainer.transform.GetChild (1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Button>();
		Transform PanelHolder = ScreenUIContainer.transform.GetChild (1);
		DollPanel = PanelHolder.GetChild (0).GetChild (0).gameObject;
		DataPanel = PanelHolder.GetChild (0).GetChild (1).gameObject;
		NewSoldierPanel = PanelHolder.GetChild (0).GetChild (2).gameObject;
		AssignNSHolder = ScreenUIContainer.transform.Find ("AssignNSPopup").gameObject;
		DisablePanel = ScreenUIContainer.transform.Find ("DisablePanel").gameObject;
		ArmyListHolder = ScreenUIContainer.transform.Find ("ArmyListHolder").gameObject;
		MountListHolder = ScreenUIContainer.transform.Find ("MountListHolder").gameObject;
		ArmorListHolder = ScreenUIContainer.transform.Find ("AromrListHolder").gameObject;
		ShieldListHolder = ScreenUIContainer.transform.Find ("ShieldListHolder").gameObject;
		UnmountEquipment = ScreenUIContainer.transform.Find ("UnmountEquipment").gameObject;
		TrainingHolder = ScreenUIContainer.transform.Find ("AdjustSoildersAttribute").gameObject;
		TrainingQHolder = ScreenUIContainer.transform.Find ("TrainingQHolder").gameObject;
		TrainingQAHolder = ScreenUIContainer.transform.Find ("TrainingQAHolder").gameObject;
		TrainingEquHolder = ScreenUIContainer.transform.Find ("TrainingEquHolder").gameObject;
		TrainingEquConfirmAHolder = ScreenUIContainer.transform.Find ("TrainEquConfirmHolder").gameObject;
		TrainingInProgress = ScreenUIContainer.transform.Find ("TrainingInProgress").gameObject;
		ConfirmSpeedUpHolder = ScreenUIContainer.transform.Find ("ConfirmSpeedUpHolder").gameObject;
		ArmyQAHolder = ScreenUIContainer.transform.Find ("ArmysQAHolder").gameObject;
		ArmorQAHolder = ScreenUIContainer.transform.Find ("ArmorsQAHolder").gameObject;
		ShieldQAHolder = ScreenUIContainer.transform.Find ("ShieldQAHolder").gameObject;

		ArmyButton = DollPanel.transform.GetChild (0).GetChild(0).GetComponent<Button> ();
		MountButton = DollPanel.transform.GetChild (0).GetChild(1).GetComponent<Button> ();
		SoldierButton = DollPanel.transform.GetChild (0).GetChild(2).GetComponent<Button> ();
		ArmorButton = DollPanel.transform.GetChild (1).GetChild(0).GetComponent<Button> ();
		ShieldButton = DollPanel.transform.GetChild (1).GetChild(1).GetComponent<Button> ();
		UnmountButton = DollPanel.transform.GetChild (1).GetChild(2).GetComponent<Button> ();
		TrainingButton = DataPanel.transform.GetChild (1).GetComponent<Button> ();
		AssignSoldierButton = NewSoldierPanel.transform.GetChild (1).GetComponent<Button> ();
		AssignSoldierConfirmButton = Utilities.Panel.GetConfirmButton (AssignNSHolder);
		SoldierType1Button = PanelHolder.GetChild (1).GetChild (0).GetComponent<Button> ();
		SoldierType2Button = PanelHolder.GetChild (1).GetChild (1).GetComponent<Button> ();
		SoldierType3Button = PanelHolder.GetChild (1).GetChild (2).GetComponent<Button> ();
		SoldierType4Button = PanelHolder.GetChild (1).GetChild (3).GetComponent<Button> ();
		SoldierType5Button = PanelHolder.GetChild (1).GetChild (4).GetComponent<Button> ();
		SoldierType6Button = PanelHolder.GetChild (1).GetChild (5).GetComponent<Button> ();
		SoldierType7Button = PanelHolder.GetChild (1).GetChild (6).GetComponent<Button> ();
		SoldierType8Button = PanelHolder.GetChild (1).GetChild (7).GetComponent<Button> ();
		BackButton = ScreenUIContainer.transform.Find ("BackButton").GetComponent<Button> ();
		CloseButton = ScreenUIContainer.transform.Find ("CloseButton").GetComponent<Button> ();
		ArmyQAHolderCancelButton = Utilities.Panel.GetCancelButton (ArmyQAHolder);
		ArmorQAHolderCancelButton = Utilities.Panel.GetCancelButton (ArmorQAHolder);
		ShieldQAHolderCancelButton = Utilities.Panel.GetCancelButton (ShieldQAHolder);
		closeTrainingButton = TrainingHolder.transform.GetChild (8).GetChild (1).GetComponent<Button> ();
		closeUnmountEquipmentButton = Utilities.Panel.GetCancelButton (UnmountEquipment);
	}


	public override void Bind() {
		base.Bind();
		// Use this.SchoolFieldScreen to access the viewmodel.
		// Use this method to subscribe to the view-model.
		// Any designer bindings are created in the base implementation.
		
		this.BindButtonToHandler (BackButton, () => {
			DisablePanel.SetActive (false);
			/*
<<<<<<< HEAD
			MountListHolder.SetActive (false);
			ArmyListHolder.SetActive (false);
			ArmorListHolder.SetActive (false);
			ShieldListHolder.SetActive (false);
			SaveType.SetActive (false);
			TrainingHolder.SetActive (false);
			TrainingQHolder.SetActive(false);
			AssignNSHolder.SetActive(false);
			ArmyQAHolder.SetActive(false);
			ArmorQAHolder.SetActive(false);
			ShieldQAHolder.SetActive(false);
		});

		this.BindButtonToHandler (CloseButton, () => {
			DisablePanel.SetActive (false);
			MountListHolder.SetActive (false);
			ArmyListHolder.SetActive (false);
			ArmorListHolder.SetActive (false);
			ShieldListHolder.SetActive (false);
			SaveType.SetActive (false);
			TrainingHolder.SetActive (false);
			TrainingQHolder.SetActive(false);
			AssignNSHolder.SetActive(false);
			ArmyQAHolder.SetActive(false);
			ArmorQAHolder.SetActive(false);
			ShieldQAHolder.SetActive(false);

=======
*/
			MountListHolder.gameObject.SetActive (false);
			ArmyListHolder.gameObject.SetActive (false);
			ArmorListHolder.gameObject.SetActive (false);
			ShieldListHolder.gameObject.SetActive (false);
			ArmyQAHolder.SetActive (false);
			ArmorQAHolder.SetActive (false);
			ShieldQAHolder.SetActive (false);
//			SaveType.gameObject.SetActive (false);
			TrainingHolder.gameObject.SetActive (false);
			TrainingQHolder.SetActive (false);
			TrainingQAHolder.SetActive (false);
			TrainingEquHolder.SetActive (false);
			TrainingEquConfirmAHolder.SetActive (false);
			TrainingInProgress.SetActive (false);
			ConfirmSpeedUpHolder.SetActive (false);
		});

		this.BindButtonToHandler (CloseButton, () => {
			DisablePanel.SetActive (false);
			MountListHolder.gameObject.SetActive (false);
			ArmyListHolder.gameObject.SetActive (false);
			ArmorListHolder.gameObject.SetActive (false);
			ShieldListHolder.gameObject.SetActive (false);
			ArmyQAHolder.SetActive (false);
			ArmorQAHolder.SetActive (false);
			ShieldQAHolder.SetActive (false);
//			SaveType.gameObject.SetActive (false);
			TrainingHolder.gameObject.SetActive (false);
			TrainingQHolder.SetActive (false);
			TrainingQAHolder.SetActive (false);
			TrainingEquHolder.SetActive (false);
			TrainingEquConfirmAHolder.SetActive (false);
			TrainingInProgress.SetActive (false);
			ConfirmSpeedUpHolder.SetActive (false);
			gameObject.SetActive(false);
		});


		this.BindButtonToHandler (closeTrainingButton, () => {
			DisablePanel.SetActive (false);
			TrainingHolder.SetActive (false);

		});

		this.BindButtonToHandler (ArmyButton, () => {
//			ArmyListHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (MountButton, () => {
			MountListHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (ArmorButton, () => {
//			ArmorListHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (ShieldButton, () => {
//			ShieldListHolder.gameObject.SetActive (true);
		});

//		this.BindButtonToHandler (SaveButton, () => {
//			SaveType.gameObject.SetActive (true);
//		});

		this.BindButtonToHandler (TrainingButton, () => {
			TrainingHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (AssignSoldierButton, () => {
//			TrainingQHolder.gameObject.SetActive (true);
		});
		this.BindButtonToHandler (AssignSoldierConfirmButton, () => {
			AssignNSHolder.gameObject.SetActive (false);
		});
		this.BindButtonToHandler (SoldierType1Button, () => {
			SchoolField.AssigningSoldier = 1;
		});
		this.BindButtonToHandler (SoldierType2Button, () => {
			SchoolField.AssigningSoldier = 2;
		});
		this.BindButtonToHandler (SoldierType3Button, () => {
			SchoolField.AssigningSoldier = 3;
		});
		this.BindButtonToHandler (SoldierType4Button, () => {
			SchoolField.AssigningSoldier = 4;
		});
		this.BindButtonToHandler (SoldierType5Button, () => {
			SchoolField.AssigningSoldier = 5;
		});
		this.BindButtonToHandler (SoldierType6Button, () => {
			SchoolField.AssigningSoldier = 6;
		});
		this.BindButtonToHandler (SoldierType7Button, () => {
			SchoolField.AssigningSoldier = 7;
		});
		this.BindButtonToHandler (SoldierType8Button, () => {
			SchoolField.AssigningSoldier = 8;
		});
		this.BindButtonToHandler (ArmyQAHolderCancelButton, () => {
			ArmyQAHolder.SetActive(false);
		});
		this.BindButtonToHandler (ArmorQAHolderCancelButton, () => {
			ArmorQAHolder.SetActive(false);
		});
		this.BindButtonToHandler (ShieldQAHolderCancelButton, () => {
			ShieldQAHolder.SetActive(false);
		});
		this.BindButtonToHandler (UnmountButton, () => {
			UnmountEquipment.gameObject.SetActive (true);
		});
		this.BindButtonToHandler (closeUnmountEquipmentButton, () => {
			UnmountEquipment.gameObject.SetActive (false);
		});
	}
}
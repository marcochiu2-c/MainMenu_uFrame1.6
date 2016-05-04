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

	public Button BackButton;
	public Button CloseButton;
	public Button SoldierButton;
	public Button ArmyButton;
	public Button MountButton;
	public Button ArmorButton;
	public Button ShieldButton;
	public Button SaveButton;
	public Button BufferButton;
	public Button TrainingButton;
	public Button AssignSoldierButton;
	public Button AssignSoldierConfirmButton;
	public Button SoldierType1Button;
	public Button SoldierType2Button;
	public Button SoldierType3Button;
	public Button SoldierType4Button;
	public Button SoldierType5Button;
	public Button SoldierType6Button;
	public Button SoldierType7Button;
	public Button SoldierType8Button;

	public Button ArmyQAHolderCancelButton;
	public Button ArmorQAHolderCancelButton;
	public Button ShieldQAHolderCancelButton;
	public Button closeTrainingButton;

	public GameObject DisablePanel;
	public GameObject ArmyListHolder;
	public GameObject MountListHolder;
	public GameObject ArmorListHolder;
	public GameObject ShieldListHolder;
	//public GameObject ArmyQAHolder;
	//public GameObject MountQAHolder;
	//public GameObject ArmorQAHolder;
	//public GameObject ShieldQAHolder;
	public GameObject SaveType;
	public GameObject TrainingHolder;
	public GameObject TrainingQHolder;
	public GameObject TrainingQAHolder;
	public GameObject TrainingEquHolder;
	public GameObject TrainingEquConfirmAHolder;
	public GameObject TrainingInProgress;
	public GameObject ConfirmSpeedUpHolder;
	public GameObject AssignNSHolder;
	public GameObject ArmyQAHolder;
	public GameObject ArmorQAHolder;
	public GameObject ShieldQAHolder;
	
	protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
		base.InitializeViewModel(model);
		// NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
		// var vm = model as SchoolFieldScreenViewModel;
		// This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
	}
	
	public override void Bind() {
		base.Bind();
		// Use this.SchoolFieldScreen to access the viewmodel.
		// Use this method to subscribe to the view-model.
		// Any designer bindings are created in the base implementation.
		
		this.BindButtonToHandler (BackButton, () => {
			DisablePanel.SetActive (false);
			MountListHolder.gameObject.SetActive (false);
			ArmyListHolder.gameObject.SetActive (false);
			ArmorListHolder.gameObject.SetActive (false);
			ShieldListHolder.gameObject.SetActive (false);
			ArmyQAHolder.SetActive (false);
			ArmorQAHolder.SetActive (false);
			ShieldQAHolder.SetActive (false);
			SaveType.gameObject.SetActive (false);
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
			SaveType.gameObject.SetActive (false);
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
			TrainingHolder.gameObject.SetActive (false);
			//ArmyQAHolder.gameObject.SetActive (false);
			//MountQAHolder.gameObject.SetActive (false);
			//ArmorQAHolder.gameObject.SetActive (false);
			//ShieldQAHolder.gameObject.SetActive (false);
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

		this.BindButtonToHandler (SaveButton, () => {
			SaveType.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (TrainingButton, () => {
			TrainingHolder.gameObject.SetActive (true);
		});

		this.BindButtonToHandler (AssignSoldierButton, () => {
//			TrainingQHolder.gameObject.SetActive (true);
		});
		this.BindButtonToHandler (AssignSoldierConfirmButton, () => {
			DisablePanel.SetActive(false);
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
	}
}

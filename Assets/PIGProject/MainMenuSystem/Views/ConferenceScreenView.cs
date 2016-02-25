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


public class ConferenceScreenView : ConferenceScreenViewBase {

	public Button ArmyAttackCompiled;
	public Button ArmyGarrisonCompiled;
	public Button MilitaryAdviserAppointAgent;
	public Button DefensiveLinup;
	public Button Standings;

	public GameObject ArmyAttackCompiledPopup;
	public GameObject ArmyGarrisonCompiledPopup;
	public GameObject MilitaryAdviserAppointAgentPopup;
	public GameObject DefensiveLinupPopup;
	public GameObject Standingspopup;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ConferenceScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.ConferenceScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (ArmyAttackCompiled, () => {
			ArmyAttackCompiledPopup.gameObject.SetActive (true);
			ArmyGarrisonCompiledPopup.gameObject.SetActive (false);
			MilitaryAdviserAppointAgentPopup.gameObject.SetActive (false);
			DefensiveLinupPopup.gameObject.SetActive (false);
			Standingspopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (ArmyGarrisonCompiled, () => {
			ArmyAttackCompiledPopup.gameObject.SetActive (false);
			ArmyGarrisonCompiledPopup.gameObject.SetActive (true);
			MilitaryAdviserAppointAgentPopup.gameObject.SetActive (false);
			DefensiveLinupPopup.gameObject.SetActive (false);
			Standingspopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (MilitaryAdviserAppointAgent, () => {
			ArmyAttackCompiledPopup.gameObject.SetActive (false);
			ArmyGarrisonCompiledPopup.gameObject.SetActive (false);
			MilitaryAdviserAppointAgentPopup.gameObject.SetActive (true);
			DefensiveLinupPopup.gameObject.SetActive (false);
			Standingspopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (DefensiveLinup, () => {
			ArmyAttackCompiledPopup.gameObject.SetActive (false);
			ArmyGarrisonCompiledPopup.gameObject.SetActive (false);
			MilitaryAdviserAppointAgentPopup.gameObject.SetActive (false);
			DefensiveLinupPopup.gameObject.SetActive (true);
			Standingspopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (Standings, () => {
			ArmyAttackCompiledPopup.gameObject.SetActive (false);
			ArmyGarrisonCompiledPopup.gameObject.SetActive (false);
			MilitaryAdviserAppointAgentPopup.gameObject.SetActive (false);
			DefensiveLinupPopup.gameObject.SetActive (false);
			Standingspopup.gameObject.SetActive (true);
		});
    }
}

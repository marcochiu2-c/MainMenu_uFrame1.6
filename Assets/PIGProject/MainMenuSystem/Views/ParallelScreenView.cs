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


public class ParallelScreenView : ParallelScreenViewBase {

	public Button friends;
	public Button pvp;
	public Button guild;

	public GameObject Friends;
	public GameObject PVP;
	public GameObject Guild;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ParallelScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.ParallelScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (friends, () => {
			Friends.gameObject.SetActive (true);
			PVP.gameObject.SetActive (false);
			Guild.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (pvp, () => {
			Friends.gameObject.SetActive (false);
			PVP.gameObject.SetActive (true);
			Guild.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (guild, () => {
			Friends.gameObject.SetActive (false);
			PVP.gameObject.SetActive (false);
			Guild.gameObject.SetActive (true);
		});

    }
}

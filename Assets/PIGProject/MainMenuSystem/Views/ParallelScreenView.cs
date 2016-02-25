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

	public Button Friends;
	public Button PVP;
	public Button Guild;

	public GameObject FriendsPopup;
	public GameObject PVPPopup;
	public GameObject Guildpopup;
    
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

		this.BindButtonToHandler (Friends, () => {
			FriendsPopup.gameObject.SetActive (true);
			PVPPopup.gameObject.SetActive (false);
			Guildpopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (PVP, () => {
			FriendsPopup.gameObject.SetActive (false);
			PVPPopup.gameObject.SetActive (true);
			Guildpopup.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (Guild, () => {
			FriendsPopup.gameObject.SetActive (false);
			PVPPopup.gameObject.SetActive (false);
			Guildpopup.gameObject.SetActive (true);
		});
    }
}

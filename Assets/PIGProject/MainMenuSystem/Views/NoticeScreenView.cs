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


public class NoticeScreenView : NoticeScreenViewBase {

	public Button SignButton;
	public Button NoticeButton;
	public Button SpecialButton;
	public Button BackButton;
	public Button CloseButton;

	public GameObject SignHolder;
	public GameObject NoticeHolder;
	public GameObject SpecialHolder;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as NoticeScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    

    public override void Bind() {
        base.Bind();
        // Use this.NoticeScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
		/*this.BindButtonToHandler(SignButton, () =>
		{                                           
			Publish(new NotifyCommand()
			{
				Message = "Signed"
			});
		});*/

		this.BindButtonToHandler (BackButton, () => {
			SignHolder.SetActive (false);
			NoticeHolder.SetActive (false);
			SpecialHolder.SetActive(false);
		});

		this.BindButtonToHandler (CloseButton, () => {
			SignHolder.SetActive (false);
			NoticeHolder.SetActive (false);
			SpecialHolder.SetActive(false);
			gameObject.SetActive(false);
		});

		this.BindButtonToHandler (SignButton, () => {
			SignHolder.gameObject.SetActive (true);
			NoticeHolder.gameObject.SetActive (false);
			SpecialHolder.gameObject.SetActive(false);
			ExecuteSign();
		});

		this.BindButtonToHandler (NoticeButton, () => {
			SignHolder.gameObject.SetActive (false);
			NoticeHolder.gameObject.SetActive (true);
			SpecialHolder.gameObject.SetActive(false);
		});

		this.BindButtonToHandler (SpecialButton, () => {
			SignHolder.gameObject.SetActive (false);
			NoticeHolder.gameObject.SetActive (false);
			SpecialHolder.gameObject.SetActive (true);
		});
	}

}
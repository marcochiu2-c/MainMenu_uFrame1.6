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

	public static Button SignButton;
	public static Button NoticeButton;
	public static Button SpecialButton;
	public static Button BackButton;
	public static Button CloseButton;

	public static GameObject SignHolder;
	public static GameObject NoticeHolder;
	public static GameObject SpecialHolder;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as NoticeScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		AssignGameObjectVariable ();
    }
    
	void AssignGameObjectVariable(){
		Utilities.ShowLog.Log ("Which Container: "+ScreenUIContainer);
		SignHolder = ScreenUIContainer.transform.Find ("SignHolder").gameObject;
		NoticeHolder = ScreenUIContainer.transform.Find ("NoticeHolder").gameObject;
		SpecialHolder = ScreenUIContainer.transform.Find ("SpecialHolder").gameObject;

		Transform bgImage = ScreenUIContainer.transform.Find ("BackgroundImage");

		SignButton = bgImage.Find ("SignButton").GetComponent<Button> ();
		NoticeButton = bgImage.Find ("NoticeButton").GetComponent<Button> ();
		SpecialButton = bgImage.Find ("SpecialDealButton").GetComponent<Button> ();

		BackButton = ScreenUIContainer.transform.Find ("BackButton").GetComponent<Button> ();
		CloseButton = ScreenUIContainer.transform.Find ("CloseButton").GetComponent<Button> ();

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
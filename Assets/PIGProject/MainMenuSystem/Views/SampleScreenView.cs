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


public class SampleScreenView : SampleScreenViewBase {
    
	public Button DragDropButton;
	public Button ScrollButton;

	public GameObject DragDropPanel;
	public GameObject ScrollPanel;

    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as SampleScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.SampleScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler(DragDropButton, () =>
		{
			DragDropPanel.gameObject.SetActive(true);
			ScrollPanel.gameObject.SetActive(false);
		});

		this.BindButtonToHandler(ScrollButton, () =>
		{
			DragDropPanel.gameObject.SetActive(false);
			ScrollPanel.gameObject.SetActive(true);
		});
	}
}

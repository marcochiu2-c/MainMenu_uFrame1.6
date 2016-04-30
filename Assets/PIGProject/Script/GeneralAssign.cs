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

public class GeneralAssign : MonoBehaviour {

	// Use this for initialization
	
	public ConferenceScreenView ConferenceV;
	public Image generalIcon;
	public Button myBtn;
	
	/*
	public override void KernelLoaded()
	{
		base.KernelLoaded();
		myBtn.onClick.AddListener(() => { 
			ConferenceV = uFrameKernel.Container.Resolve<ConferenceScreenView>("ConferenceSCreen");
			Debug.Log (ConferenceV == null ? "ConferenceV is null" : "Conference is: " + ConferenceV.name);
			CallConferenceView ();
		});
	}
	*/
	public void Start()
	{
		myBtn.onClick.AddListener(() => { 
			ConferenceV = GameObject.Find("ConferenceScreen").GetComponent<ConferenceScreenView>();
			Debug.Log (ConferenceV == null ? "ConferenceV is null" : "Conference is: " + ConferenceV.name);
			CallConferenceView ();
		});
	}
	
	public void CallConferenceView ()
	{
		ConferenceV.AssignGeneral(generalIcon);
	}
}

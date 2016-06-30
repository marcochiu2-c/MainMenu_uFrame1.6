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
	public Text  generalName;
	public Text generalIQ;
	public Text generalLv;
	public Button myBtn;
	public int general_id;
	public int general_type;
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
			
			for (int i = 0; i <  5; i++)
			{
				if(general_id == ConferenceV.generalIDArray[i])
				{
					ConferenceV.PopUp();
					Debug.Log ("the general have been assigned");
					return;
				}
			}
			
			ConferenceV.generalIDArray[ConferenceV.whichTeam - 1] = general_id;
			CallConferenceView ();
		});
	}
	
	public void CallConferenceView ()
	{
		ConferenceV.AssignGeneral(general_type);
	}
}
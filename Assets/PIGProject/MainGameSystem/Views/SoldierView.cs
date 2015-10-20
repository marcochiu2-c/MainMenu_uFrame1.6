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
using Gamelogic.Grids;
using DG.Tweening;

public class SoldierView : SoldierViewBase {

	//public SoldierService SS;
	public Button IgnoreButton;
	public Button SlowButton; 
	public Button NormalButton; 
	public Button FastButton;

	public Button AttackButton;
	public Button AssaultButton;
	public Button RaidButton;
	public Button FeintButton;
	public Button PinButton;
	public Button YawpButton;
	public Button SearchButton;

	public Button PlayButton; 

	public Text myText;
	public GameObject MovePanel;
	public GameObject ActionPanel;
	[SerializeField]
	private int step = 0;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as PlayerViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		//Debug.Log("Max_Quantity = " + Max_Quantity);
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.Player to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
		this.BindButtonToHandler(IgnoreButton, () => this.Soldier.Movement = MoveStyle.IGNORE);
		this.BindButtonToHandler(SlowButton, () => this.Soldier.Movement = MoveStyle.SLOW);	
		this.BindButtonToHandler(NormalButton, () => this.Soldier.Movement = MoveStyle.NORMAL);	
		this.BindButtonToHandler(FastButton, () => this.Soldier.Movement = MoveStyle.FAST);
	
		this.BindButtonToHandler(AssaultButton, () => this.Soldier.Action = ActionStyle.ASSAULT);
		this.BindButtonToHandler(AttackButton, () => this.Soldier.Action = ActionStyle.ATTACK);
		this.BindButtonToHandler(RaidButton, () => this.Soldier.Action = ActionStyle.RAID);
		this.BindButtonToHandler(FeintButton, () => this.Soldier.Action = ActionStyle.FEINT);
		this.BindButtonToHandler(PinButton, () => this.Soldier.Action = ActionStyle.PIN);
		this.BindButtonToHandler(YawpButton, () => this.Soldier.Action = ActionStyle.YAWP);
		this.BindButtonToHandler(SearchButton, () => this.Soldier.Action = ActionStyle.SEARCH);


		this.BindButtonToHandler(PlayButton, () => PlayBattle());
    }

	//the LIst that save the move and command
	public void PlayBattle()
	{
		foreach (var item in this.Soldier.playlist)
		{
			Debug.Log("ID: " + this.Soldier.playlist.IndexOf(item) + " Point: " + item.SavePointLocation + " Move: " + item.SaveMove + " Action: " + item.SaveAction + " Opponent: " + item.SaveEnemyVM);	
		}
	}

	public override void SoldierStateChanged(SoldierState state) {

		base.SoldierStateChanged(state);
		myText.text = state + " State";

		//Change the Panel to Attack Panel
		if(state == SoldierState.ATTACK)
		{
			//Active Action btns
			Debug.Log ("Attack !!");
			ActionPanel.gameObject.SetActive(true);
			MovePanel.gameObject.SetActive(false);
			//this.playlist = new PlayList(this.CurrentPointLocation, this._Action);
			//this.Soldier.playlist.Insert(step, new PlayList(this.Entity.CurrentPointLocation, this.Soldier.Movement ,this.Soldier.Action, this.Soldier.Opponent));
			step++;
		}
		//Change the Panel to Move Panel
		if(state == SoldierState.MOVE)
		{
			//Active Move btns
			Debug.Log ("Move !!");
			MovePanel.gameObject.SetActive(true);
			ActionPanel.gameObject.SetActive(false);
		}

    }
	/*
    public override void isAttackChanged(Boolean attack) {
		if (attack)
		{
			Debug.Log ("Attack !!");
			ActionPanel.gameObject.SetActive(true);
			MovePanel.gameObject.SetActive(false);
			//this.playlist = new PlayList(this.CurrentPointLocation, this._Action);
			this.playlist.Insert(step, new PlayList(this.CurrentPointLocation, this._Movement ,this._Action));
			step++;
		}
		else
		{
			Debug.Log ("Move !!");
			MovePanel.gameObject.SetActive(true);
			ActionPanel.gameObject.SetActive(false);
		}
    }
	*/
}

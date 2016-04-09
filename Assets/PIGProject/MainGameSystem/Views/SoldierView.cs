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

	//public GSHexGridManager gSHexGridManager;

	/*
	public Button SlowButton; 
	public Button NormalButton; 
	public Button FastButton;

	public Button AttackButton;
	public Button AssaultButton;
	public Button FeintButton;
	public Button PinButton;
	public Button YawpButton;
	public Button SearchButton;
	public Button AATKButton;
	public Button StandByButton;

	public Button PlayButton; 
	*/

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
		//gSHexGridManager = GameObject.Find("HexMapGrid").GetComponent<GSHexGridManager>();
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.Player to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

    }

	//the LIst that save the move and command
	public void PlayBattle()
	{
		foreach (var item in this.Soldier.playlist)
		{
			Debug.Log("ID: " + this.Soldier.playlist.IndexOf(item) + " Point: " + item.SavePointLocation + " Move: " + item.SaveMove + 
			          " Action: " + item.SaveAction + " Opponent: " + item.SaveEnemyVM);	
		}
	}

	public override void SoldierStateChanged(SoldierState state) {

		base.SoldierStateChanged(state);
		myText.text = state + " State";

		//Change the Panel to Attack Panel

		if (state == SoldierState.ATTACK)
		{
			//Active Action btns
			//Debug.Log ("Attack !!");
			if (MovePanel.gameObject.activeSelf && !ActionPanel.gameObject.activeSelf)
			{
				//Debug.Log("Change to Action Panel");       
				MovePanel.transform.DOMove(new Vector3(MovePanel.transform.position.x, MovePanel.transform.position.y + 50.0f), 1).SetEase(Ease.OutSine).OnComplete(() => MovePanel.gameObject.SetActive(false));
				ActionPanel.transform.DOMove(new Vector3(MovePanel.transform.position.x, MovePanel.transform.position.y - 8.0f), 1).SetEase(Ease.OutSine).OnStart(() => ActionPanel.gameObject.SetActive(true));
				//MovePanel.transform.DOLocalMoveY(1.0f, 1).SetEase(Ease.OutSine).OnComplete(() => MovePanel.gameObject.SetActive(false));
				//ActionPanel.transform.DOLocalMoveY(-1.0f, 1).SetEase(Ease.OutSine).OnStart(() => ActionPanel.gameObject.SetActive(true));
			}

			//ActionPanel.gameObject.SetActive(true);
			//MovePanel.gameObject.SetActive(false);
		}
		//Change the Panel to Move Panel
		if (state == SoldierState.MOVE)
		{
			//Active Move btns
			//Debug.Log ("Move !!");

			if (!MovePanel.gameObject.activeSelf && ActionPanel.gameObject.activeSelf)
			{
				//Debug.Log("Change to Move Panel");
				MovePanel.transform.DOMove(new Vector3(MovePanel.transform.position.x, MovePanel.transform.position.y - 50.0f), 1).SetEase(Ease.OutSine).OnStart(() => MovePanel.gameObject.SetActive(true));
				ActionPanel.transform.DOMove(new Vector3(MovePanel.transform.position.x, MovePanel.transform.position.y + 8.0f), 1).SetEase(Ease.OutSine).OnComplete(() => ActionPanel.gameObject.SetActive(false));
				//MovePanel.transform.DOLocalMoveY(-1.0f, 1).SetEase(Ease.OutSine).OnStart(() => MovePanel.gameObject.SetActive(true));
				//ActionPanel.transform.DOLocalMoveY(1.0f, 1).SetEase(Ease.OutSine).OnComplete(() => ActionPanel.gameObject.SetActive(false));
			}
		}


		if (state == SoldierState.PLAY)
		{
			MovePanel.gameObject.SetActive(false);
			ActionPanel.gameObject.SetActive(false);
		}

    }

    public override void ActionChanged(ActionStyle action) {
		if (action == ActionStyle.A_ATK)
		{
			var color = this.gameObject.GetComponent<Renderer>().material.color;
			color.a = 200.0f/255.0f;
			//this.RendererColor(color);
		}
    }
}

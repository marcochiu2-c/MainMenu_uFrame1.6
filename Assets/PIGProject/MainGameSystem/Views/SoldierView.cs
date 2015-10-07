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
	private float Max_Quantity;
	private int step = 0;

	public List<PlayList> playlist = new List<PlayList>(){};

	public struct PlayList
	{
		public FlatHexPoint SavePointLocation;
		public MoveStyle SaveMove;
		public ActionStyle SaveAction;
		//public int[] HealthHistory = new int[50];
		
		public PlayList(FlatHexPoint savePointLocation, MoveStyle saveMove, ActionStyle saveAction)
		{
			this.SavePointLocation = savePointLocation;
			this.SaveMove = saveMove;
			this.SaveAction = saveAction;
			//this.HealthHistory = healthHistory;
		}
	}
    
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
	
	public void PlayBattle()
	{
		foreach (var item in playlist)
		{
			Debug.Log("ID: " + playlist.IndexOf(item) + " Point: " + item.SavePointLocation + " Move: " + item.SaveMove + " Action: " + item.SaveAction);	
		}
	}

	/*
	public void UpdateQuantity(float number)
	{
		float maxHealth = 100f;
		this._Health = number;
		var curHealth = (float)this._Health/Max_Quantity;
		//Debug.Log("curHealth: " + curHealth);

		healthBar.transform.localScale = new Vector3(curHealth, healthBar.transform.localScale.y);

		if(number == 0){
			//TODO
			//Destroy the object
		}
	}
	*/

	// try to use UniRx when have time	
	public IEnumerator Move(Vector3 currentPoint, Vector3 endPoint){
		float time = 0;
		const float totalTime = .3f;

		//onAction = true;
		while (time < totalTime)
		{
			float x = Mathf.Lerp(currentPoint.x, endPoint.x, time / totalTime);
			float y = Mathf.Lerp(currentPoint.y, endPoint.y, time / totalTime);

			//y -= 20;
			
			transform.position = new Vector3(x, y);
			time += Time.deltaTime;
		}

		//this._State = PlayerState.ATTACK;
		//Debug.Log ("After Move: " + this._State);
		yield return null;

		if(this.Soldier.Movement == MoveStyle.SLOW)
			yield return new WaitForSeconds(0.2f);
		
		else if(this.Soldier.Movement == MoveStyle.NORMAL)
			yield return new WaitForSeconds(0.1f);
		
		else if(this.Soldier.Movement == MoveStyle.FAST)
			yield return new WaitForSeconds(0.05f);
		
		else
			yield return new WaitForSeconds(0.4f);
	}



	public override void SoldierStateChanged(SoldierState state) {

		base.SoldierStateChanged(state);
		myText.text = state + " State";

		if(state == SoldierState.ATTACK)
		{
			//Active Action btns
			Debug.Log ("Attack !!");
			ActionPanel.gameObject.SetActive(true);
			MovePanel.gameObject.SetActive(false);
			//this.playlist = new PlayList(this.CurrentPointLocation, this._Action);
			this.playlist.Insert(step, new PlayList(this.CurrentPointLocation, this.Soldier.Movement ,this.Soldier.Action));
			step++;
		}

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

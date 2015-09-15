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

public class PlayerView : PlayerViewBase {
	//public IMap<FlatHexPoint> Map;
	public Button IgnoreButton;
	public Button SlowButton; 
	public Button NormalButton; 
	public Button FastButton;

	public Button AttackButton;
	public Button AssaultButton; 

	public Text myText;
	public GameObject healthBar;
	public GameObject MovePanel;
	public GameObject ActionPanel;

	private float Max_Quantity;

	public FlatHexPoint CurrentPointLocation
	{
		get;
		set;
	}
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as PlayerViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		Max_Quantity = (float)this._Quantity;
		Debug.Log("Max_Quantity = " + Max_Quantity);
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.Player to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		//MoveStyle
		this.BindButtonToHandler(IgnoreButton, () => ChangeMoveStyle(MoveStyle.IGNORE));
		this.BindButtonToHandler(SlowButton, () => ChangeMoveStyle(MoveStyle.SLOW));
		this.BindButtonToHandler(NormalButton, () => ChangeMoveStyle(MoveStyle.NORMAL));
		this.BindButtonToHandler(FastButton, () => ChangeMoveStyle(MoveStyle.FAST));

		this.BindButtonToHandler(AttackButton, () => ChangeActionStyle(ActionStyle.ATTACK));
		this.BindButtonToHandler(AssaultButton, () => ChangeActionStyle(ActionStyle.ASSAULT));


		//ActionStyle


    }

	public void UpdateQuantity(int number){
		float maxHealth = 100f;

		this._Quantity = number;
		var curHealth = (float)this._Quantity/Max_Quantity;
		//Debug.Log("curHealth: " + curHealth);

		healthBar.transform.localScale = new Vector3(curHealth, healthBar.transform.localScale.y);

		if(number == 0){
			//TODO
			//Destroy the object
		}
	}
	
	private void ChangeMoveStyle(MoveStyle m)
	{
		switch (m)
		{
		case MoveStyle.IGNORE:
			this._Movement = MoveStyle.IGNORE;
			break;
		case MoveStyle.SLOW:
			this._Movement = MoveStyle.SLOW;
			break;
		case MoveStyle.NORMAL:
			this._Movement = MoveStyle.NORMAL;
			break;
		case MoveStyle.FAST:
			this._Movement = MoveStyle.FAST;
			break;
		default:
			this._Movement = MoveStyle.NORMAL;
			break;
		}
	}

	private void ChangeActionStyle(ActionStyle a)
	{
		switch (a)
		{
		case ActionStyle.ATTACK:
			this._Action = ActionStyle.ATTACK;
			break;
		case ActionStyle.ASSAULT:
			this._Action = ActionStyle.ASSAULT;
			break;
		default:
			this._Action = ActionStyle.ATTACK;
			break;
		}
	}

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

		if(this._Movement == MoveStyle.SLOW)
			yield return new WaitForSeconds(0.2f);
		
		else if(this._Movement == MoveStyle.NORMAL)
			yield return new WaitForSeconds(0.1f);
		
		else if(this._Movement == MoveStyle.FAST)
			yield return new WaitForSeconds(0.05f);
		
		else
			yield return new WaitForSeconds(0.4f);
		

	}

	//TODO
	//Set BindButtonHandler for the movement
	public override void StateChanged(PlayerState state) {

		base.StateChanged(state);
		myText.text = state + " State";

		if(state == PlayerState.ATTACK)
		{
			//Active Action btns
			Debug.Log ("Attack !!");
			ActionPanel.gameObject.SetActive(true);
			MovePanel.gameObject.SetActive(false);
		}

		if(state == PlayerState.MOVE)
		{
			//Active Move btns
			Debug.Log ("Move !!");
			MovePanel.gameObject.SetActive(true);
			ActionPanel.gameObject.SetActive(false);
		}
		Debug.Log (this._State);
    }
}

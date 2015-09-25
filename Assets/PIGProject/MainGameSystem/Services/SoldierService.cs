using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using UniRx;
using uFrame.MVVM;
using uFrame.Kernel;
using UnityEngine;


public class SoldierService : SoldierServiceBase {
	//[Inject("Soldier")] public SoldierViewModel Soldier;
	/*
	public override void Setup()
	{
		base.Setup();
		//Every time user athorization state changes activate corresponding screen
		Soldier.SoldierStateProperty
			.StartWith(Soldier.SoldierState)
				.Subscribe(OnPlayerStateChanged);
	}
	
	private void OnPlayerStateChanged(SoldierState state)
	{
		//Just activate right screen based on authorization state`
		switch (state)
		{
		case SoldierState.ATTACK:
			//Debug.Log("ATTACK from PlayerService");
			break;
		case SoldierState.MOVE:
			//Debug.Log("MOVE from PlayerService");
			break;
		default:
			throw new ArgumentOutOfRangeException("state", state, null);
		}
	}
	*/
	
}

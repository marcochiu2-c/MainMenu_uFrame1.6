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
using DG.Tweening;

public class changemap : MonoBehaviour {

	public Button changeMap;
	public Button map1;
	public Button map2;
	public Button map3;
	public Button map4;
	public Button map5;
	public Button map6;
	public Button map7;
	public Button map8;
	public Button map9;
	public Button map10;

	public Transform mapButton;

	public void showMapButton(){
		mapButton.DOLocalMoveX (640,.5f);
	}

	public void hideMapButton(){
		mapButton.DOLocalMoveX (840,.5f);
	}

}

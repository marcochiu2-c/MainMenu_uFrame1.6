using UnityEngine;
using System.Collections;
using System;

public class SchoolField : MonoBehaviour {
	public GameObject DollPanel;
	public GameObject DataPanel;
	public GameObject NewSoldierPanel;
	Game game;

	// Use this for initialization
	void Start () {
		CallSchoolField ();
	}

	void CallSchoolField(){
		game = Game.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int TotalSoldierGenerated(){
		DateTime rt = game.login.registerTime;
		return (int)DateTime.Now.Subtract (rt).TotalMinutes;
	}
}

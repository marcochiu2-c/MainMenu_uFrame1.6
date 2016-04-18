using UnityEngine;
using UnityEngine.UI;
using Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

public class GeneralTrainPrefab : MonoBehaviour {
	public static List<GeneralTrainPrefab> GeneralTrain = new List<GeneralTrainPrefab>();
	public Image image;
	public Text Name;
	public Text AttrName;
	public Text AttrValue;
	public Text RemainingTime;
	public DateTime eta;
	public General general { get; set; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetName(string name){
		Name.text = name;
	}

	public string GetName(){
		return Name.text;
	}

	public void SetAttrName(string name){
		AttrName.text = name;
	}
	
	public string GetAttrName(){
		return AttrName.text;
	}

	public void SetAttr(string attr){
		AttrValue.text = attr;
	}
	
	public string GetAttr(){
		return AttrValue.text;
	}

	public void SetRemainingTime(DateTime dt){
		RemainingTime.text = Utilities.TimeUpdate.Time (dt);
		eta = dt;
	}

	public TimeSpan GetRemainingTime(){
		return eta - DateTime.Now;
	}




}

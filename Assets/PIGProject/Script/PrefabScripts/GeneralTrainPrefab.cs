using UnityEngine;
using UnityEngine.UI;
using Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

public class GeneralTrainPrefab : MonoBehaviour {
	public static List<GeneralTrainPrefab> GeneralTrain = new List<GeneralTrainPrefab>();
	public static GeneralTrainPrefab currentPrefab;
	public Image image;
	public Text Name;
	public Text AttrName;
	public Text AttrValue;
	public Text RemainingTimeName;
	public Text RemainingTime;
	public DateTime eta;
	public General general { get; set; }
	Color defaultColor;
	
	// Use this for initialization
	void Start () {
		Color.TryParseHexString ("#F5FBF818", out defaultColor);
		InvokeRepeating ("UpdateRemainingTime", 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPrefabClicked(){
		if (currentPrefab != gameObject.GetComponent<GeneralTrainPrefab> ()) {
			// Reset Previous Item
			// TODO check if null of currentPrefab
			if (currentPrefab != null){
				currentPrefab.gameObject.GetComponent<Image>().color = defaultColor;
			}
			// Set the clicked item to be currentPrefab
			currentPrefab = gameObject.GetComponent<GeneralTrainPrefab> ();
			currentPrefab.GetComponent<Image>().color = Color.green;
		}else{
			currentPrefab.gameObject.GetComponent<Image>().color = defaultColor;
			currentPrefab = null;
			return;
		}

	}
	
	public void SetName(string name){
		Name.text = name;
	}

	public string GetName(){
		return Name.text;
	}

	public void SetAttrName(string name){
		AttrName.text = name+"：";
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
		if (dt > DateTime.Now) {
			RemainingTimeName.text = "剩餘時間：";
			RemainingTime.text = Utilities.TimeUpdate.Time (dt);
			eta = dt;
		} else {
			RemainingTimeName.text = "";
			RemainingTime.text = "";
		}
	}

	public TimeSpan GetRemainingTime(){
		if (eta > DateTime.Now) {
			return eta - DateTime.Now;
		} else {
			return new TimeSpan(0,0,0);
		}
	}

	public void SetPrefabColor(Color c){
		gameObject.GetComponent<Image> ().color = c;
	}

	public Color getPrefabColor(){
		return gameObject.GetComponent<Image> ().color;
	}

	public void UpdateRemainingTime(){
		Game game = Game.Instance;
		if (eta > DateTime.Now) {
//			Debug.Log("GeneralTrain: "+GeneralTrain.FindIndex(x => x == this));
			RemainingTime.text = Utilities.TimeUpdate.Time (eta);
			gameObject.GetComponent<Image>().color = Color.yellow;
		}else {
			RemainingTimeName.text = "";
			RemainingTime.text = "";
			if (gameObject.GetComponent<GeneralTrainPrefab>() == currentPrefab){
				gameObject.GetComponent<Image>().color = Color.green;
			}else{
				gameObject.GetComponent<Image>().color = defaultColor;
			}
		}
	}


}

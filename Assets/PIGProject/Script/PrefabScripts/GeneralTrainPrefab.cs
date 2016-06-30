using UnityEngine;
using UnityEngine.UI;
using Utilities;
using System;
using System.Collections;
using System.Collections.Generic;

public enum GeneralTrainPanel { Courage = 0, Force = 1, Strength = 2, None = -1 };


public class GeneralTrainPrefab : MonoBehaviour {
	public static List<GeneralTrainPrefab> GTrain = new List<GeneralTrainPrefab>();
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
	Game game;
	
	// Use this for initialization
	void Start () {
		game = Game.Instance;
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

			if(GeneralTrain.OpenedPanel == (int)GeneralTrainPanel.Courage){
				Debug.Log ("game.trainings[5].status: "+game.trainings[5].status);
				if (game.trainings[5].status ==(int)TrainingStatus.OnGoing){
					return;
				}else{
					currentPrefab = gameObject.GetComponent<GeneralTrainPrefab> ();
					currentPrefab.GetComponent<Image>().color = Color.green;
				}
			}else if (GeneralTrain.OpenedPanel == (int)GeneralTrainPanel.Force){
				Debug.Log ("game.trainings[6].status: "+game.trainings[6].status);
				if (game.trainings[6].status ==(int)TrainingStatus.OnGoing){
					return;
				}else{
					currentPrefab = gameObject.GetComponent<GeneralTrainPrefab> ();
					currentPrefab.GetComponent<Image>().color = Color.green;
				}
			}else if (GeneralTrain.OpenedPanel == (int)GeneralTrainPanel.Strength){
				Debug.Log ("game.trainings[7].status: "+game.trainings[7].status);
				if (game.trainings[7].status ==(int)TrainingStatus.OnGoing){
					return;
				}else{
					currentPrefab = gameObject.GetComponent<GeneralTrainPrefab> ();
					currentPrefab.GetComponent<Image>().color = Color.green;
				}
			}
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

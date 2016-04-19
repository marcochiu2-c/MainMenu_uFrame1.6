using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
// 練功房
using System;

public class GeneralTrain : MonoBehaviour {
	public Transform CouragePopup;
	public Transform ForcePopup;
	public Transform StrengthPopup;
	public GameObject DisablePanel;
	public GameObject TrainSFHolder;
	public GameObject TrainSDHolder;
	public GameObject NotEnoughSF;
	public GameObject NotEnoughSD;
	public Button CourageButton;
	public Button ForceButton;
	public Button StrengthButton;
	public Button BackButton;
	public Button CloseButton;

	Transform CourageEventHolder;
	Transform ForceEventHolder;
	Transform StrengthEventHolder;
	int buttonClicked;
	string panel;
	// Use this for initialization
	Game game;
	Dictionary<int,int> timeDict = new Dictionary<int,int>();
	Dictionary<int,int> pointDict = new Dictionary<int,int>();
	Dictionary<int,int> featherDict = new Dictionary<int,int>();
	Dictionary<int,int> stardustDict = new Dictionary<int,int>();
	void Start () {
		CallGeneralTrain ();
	}

	void CallGeneralTrain(){
		game = Game.Instance;
		//Debug.Log (game.general [10].attributes);

//		SetupGeneralTrainingPrefab ();
		CourageEventHolder = CouragePopup.GetChild (0).GetChild (0).GetChild (1);
		ForceEventHolder = ForcePopup.GetChild (0).GetChild (0).GetChild (1);
		StrengthEventHolder = StrengthPopup.GetChild (0).GetChild (0).GetChild (1);
		SetTrainDict ();
		AddButtonListener ();
	}

	void SetTrainDict(){
		timeDict.Add (0, 5);
		timeDict.Add (1, 30);
		timeDict.Add (2, 120);
		timeDict.Add (3, 240);
		pointDict.Add (0, 5);
		pointDict.Add (1, 30);
		pointDict.Add (2, 120);
		pointDict.Add (3, 240);
		featherDict.Add (0, 5);
		featherDict.Add (1, 25);
		featherDict.Add (2, 100);
		featherDict.Add (3, 200);
		stardustDict.Add (0, 1);
		stardustDict.Add (1, 5);
		stardustDict.Add (2, 20);
		stardustDict.Add (3, 40);
	}

	void AddButtonListener(){
		BackButton.onClick.AddListener (() => {
			DestroyAllGeneralTrainingPrefab();
		});

		CloseButton.onClick.AddListener (() => {
			DestroyAllGeneralTrainingPrefab();
		});
		CourageButton.onClick.AddListener (() => {
			SetupGeneralTrainingPrefab ();
			OnPanelOpen(CouragePopup);
		});
		ForceButton.onClick.AddListener (() => {
			SetupGeneralTrainingPrefab ();
			OnPanelOpen(ForcePopup);
		});
		StrengthButton.onClick.AddListener (() => {
			SetupGeneralTrainingPrefab ();
			OnPanelOpen(StrengthPopup);
		});
		CourageEventHolder.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 0;
			panel = "Courage";
			TrainByFeather(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 0;
			panel = "Courage";
			TrainByStardust(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 0;
			panel = "Force";
			TrainByFeather(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 0;
			panel = "Force";
			TrainByStardust(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(0).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 0;
			panel = "Strength";
			TrainByFeather(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(0).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 0;
			panel = "Strength";
			TrainByStardust(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 1;
			panel = "Courage";
			TrainByFeather(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 1;
			panel = "Courage";
			TrainByStardust(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 1;
			panel = "Force";
			TrainByFeather(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 1;
			panel = "Force";
			TrainByStardust(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 1;
			panel = "Strength";
			TrainByFeather(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(1).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 1;
			panel = "Strength";
			TrainByStardust(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 2;
			panel = "Courage";
			TrainByFeather(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(2).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 2;
			panel = "Courage";
			TrainByStardust(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 2;
			panel = "Force";
			TrainByFeather(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(2).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 2;
			panel = "Force";
			TrainByStardust(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(2).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 2;
			panel = "Strength";
			TrainByFeather(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(2).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 2;
			panel = "Strength";
			TrainByStardust(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(3).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 3;
			panel = "Courage";
			TrainByFeather(buttonClicked,panel);
		});
		CourageEventHolder.GetChild(3).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 3;
			panel = "Courage";
			TrainByStardust(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(3).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 3;
			panel = "Force";
			TrainByFeather(buttonClicked,panel);
		});
		ForceEventHolder.GetChild(3).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 3;
			panel = "Force";
			TrainByStardust(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(3).GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 3;
			panel = "Strength";
			TrainByFeather(buttonClicked,panel);
		});
		StrengthEventHolder.GetChild(3).GetChild(1).GetChild(1).GetComponent<Button>().onClick.AddListener (() =>{
			buttonClicked = 3;
			panel = "Strength";
			TrainByStardust(buttonClicked,panel);
		});
		Utilities.Panel.GetConfirmButton (TrainSFHolder).onClick.AddListener (() => {
			// 10 Stardust for 1 hour
			Dictionary<string,int> panelDict = new Dictionary<string, int>();
			panelDict.Add("Courage",40);
			panelDict.Add("Force",41);
			panelDict.Add("Strength",42);
			game.wealth[0].Deduct(featherDict[buttonClicked]);
			Trainings tr  = game.trainings[panelDict[panel]];
			GeneralTrainPrefab.currentPrefab.transform.SetSiblingIndex(0);
			GeneralTrainPrefab.currentPrefab.SetPrefabColor(Color.yellow);
			GeneralTrainPrefab.currentPrefab.SetRemainingTime(DateTime.Now+new TimeSpan(0,timeDict[buttonClicked],0));
			tr.etaTimestamp = DateTime.Now+new TimeSpan(0,timeDict[buttonClicked],0);
			tr.type = pointDict[buttonClicked];
			tr.targetId = GeneralTrainPrefab.currentPrefab.general.id;
			tr.trainerId = 0;
			tr.startTimestamp = DateTime.Now;
			tr.status = 1;
			tr.UpdateObject();
			HidePanel(TrainSFHolder);
		});
		Utilities.Panel.GetCancelButton (TrainSFHolder).onClick.AddListener (() => {
			HidePanel(TrainSFHolder);
		});
		Utilities.Panel.GetConfirmButton (TrainSDHolder).onClick.AddListener (() => {
			Dictionary<int, string> attrDict = new Dictionary<int, string> ();
			attrDict.Add (0, "Courage");
			attrDict.Add (1, "Force");
			attrDict.Add (2, "Physical");
			game.wealth[1].Deduct(stardustDict[buttonClicked]);
			OnTrainingComplete(attrDict[buttonClicked], GeneralTrainPrefab.currentPrefab.general.id,stardustDict[buttonClicked]);
		});
		Utilities.Panel.GetCancelButton (TrainSDHolder).onClick.AddListener (() => {
			HidePanel(TrainSDHolder);
		});
		Utilities.Panel.GetConfirmButton (NotEnoughSF).onClick.AddListener (() => {
			HidePanel(NotEnoughSF);
			ShowPanel(TrainSDHolder);
			//TODO add result to the general
		});
		Utilities.Panel.GetCancelButton (NotEnoughSF).onClick.AddListener (() => {
			HidePanel(NotEnoughSF);
		});
	}

	void TrainByFeather(int type, string panel){
		Dictionary<string,int> panelDict = new Dictionary<string, int>();
		panelDict.Add("Courage",40);
		panelDict.Add ("Force", 41);
		panelDict.Add("Strength",42);
		if (game.trainings[panelDict[panel]].status == 1 || isTraining(GeneralTrainPrefab.currentPrefab.general.id)){
			return;
		}
		if(GeneralTrainPrefab.currentPrefab != null){
			if (game.wealth[0].value < featherDict[buttonClicked] || !(isHighestPoint(panel,GeneralTrainPrefab.currentPrefab.general.attributes[panel].AsFloat))){
				ShowPanel(TrainSFHolder);
			}else{
				HidePanel(TrainSFHolder);
				ShowPanel(NotEnoughSF);
			}
		}
	}

	void TrainByStardust(int type, string panel){
		Dictionary<string,int> panelDict = new Dictionary<string, int>();
		panelDict.Add("Courage",40);
		panelDict.Add ("Force", 41);
		panelDict.Add("Strength",42);
		if (game.trainings[panelDict[panel]].status == 1 || isTraining(GeneralTrainPrefab.currentPrefab.general.id)){
			return;
		}
		if(GeneralTrainPrefab.currentPrefab != null){
			if (game.wealth[1].value < stardustDict[buttonClicked] || !(isHighestPoint(panel,GeneralTrainPrefab.currentPrefab.general.attributes[panel].AsFloat))){
				ShowPanel(TrainSDHolder);
			}else{
				HidePanel(TrainSDHolder);
				ShowPanel(NotEnoughSD);
			}
		}
	}

	void OnTrainingComplete(string type, int id, int point){
		General g = game.general.Find (x => x.id == id);
		float value = g.attributes [type].AsFloat + point;
		g.attributes [type].AsFloat = value > g.attributes ["Highest"+type].AsFloat + point ? g.attributes ["Highest"+type].AsFloat : value;
		g.UpdateObject ();
		Trainings tr = game.trainings.Find (x => x.targetId == id);
		tr.status = 3;
		tr.UpdateObject ();
	}

	void OnPanelOpen(Transform panel){
		Dictionary<Transform, string> textDict = new Dictionary<Transform, string> ();
		textDict.Add (CouragePopup, "勇氣");
		textDict.Add (ForcePopup, "武力");
		textDict.Add (StrengthPopup, "體力");
		Dictionary<Transform, string> attrDict = new Dictionary<Transform, string> ();
		attrDict.Add (CouragePopup, "Courage");
		attrDict.Add (ForcePopup, "Force");
		attrDict.Add (StrengthPopup, "Physical");
		Dictionary<Transform, int> idDict = new Dictionary<Transform, int> ();
		idDict.Add (CouragePopup, 40);
		idDict.Add (ForcePopup, 41);
		idDict.Add (StrengthPopup, 42);

		int count = GeneralTrainPrefab.GeneralTrain.Count;
		int trainingGeneral = 0;
		for (int i = 0; i < count; i++) {
			GeneralTrainPrefab.GeneralTrain[i].SetAttrName(textDict[panel]);
			GeneralTrainPrefab.GeneralTrain[i].SetAttr(game.general[i].attributes[attrDict[panel]]);
			GeneralTrainPrefab.GeneralTrain[i].transform.parent = panel.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0);
			RectTransform rTransform = GeneralTrainPrefab.GeneralTrain[i].GetComponent<RectTransform>();
			rTransform.localScale= Vector3.one;
			if (game.trainings[idDict[panel]].status == 1 && game.trainings[idDict[panel]].targetId == game.general[i].id){
				GeneralTrainPrefab.GeneralTrain[i].SetRemainingTime(game.trainings[idDict[panel]].etaTimestamp);
				GeneralTrainPrefab.GeneralTrain[i].transform.SetSiblingIndex(0);
				GeneralTrainPrefab.currentPrefab = GeneralTrainPrefab.GeneralTrain[i];
			}else{
				GeneralTrainPrefab.GeneralTrain[i].eta = new DateTime(2015,1,1);
				if (game.trainings.Exists(x => x.targetId == game.general[i].id)){
					GeneralTrainPrefab.GeneralTrain[i].gameObject.SetActive(false);
				}
			}

		}
	}

	void SetupGeneralTrainingPrefab(){
		LoadHeadPic headPic = LoadHeadPic.Instance;

		var count = game.general.Count;
		for (var i=0; i <count; i++) {
			GeneralTrainPrefab gtp = Instantiate (Resources.Load ("GeneralTrainingPrefab") as GameObject).GetComponent<GeneralTrainPrefab> ();
			GeneralTrainPrefab.GeneralTrain.Add (gtp);
			gtp.general = game.general[i];
			gtp.SetName (gtp.general.attributes["Name"]);
			gtp.image.sprite = headPic.imageDict[gtp.general.type];
		}
	}

	void DestroyAllGeneralTrainingPrefab(){
		int count = GeneralTrainPrefab.GeneralTrain.Count;
		for (int i = 0; i < count; i++) {
			GameObject.DestroyImmediate(GeneralTrainPrefab.GeneralTrain[i].gameObject);
		}
		GeneralTrainPrefab.GeneralTrain = new List<GeneralTrainPrefab>();
	}

	// Update is called once per frame
	void Update () {
	
	}


	// return true if the corrspondance is already in the highest value
	bool isHighestPoint(string type, float point){
		float highestPoint = GeneralTrainPrefab.currentPrefab.general.attributes ["Highest" + type].AsFloat;
		return (point >= highestPoint);
	}

	bool isTraining(int id){
		for (int i = 40; i < 43; i++) { 
			if (game.trainings [i].etaTimestamp > DateTime.Now) {
				if (game.trainings [i].targetId == id) {
					return true;
				}
			}
		}
		return false;
	}



	void ShowPanel(Transform panel){
		DisablePanel.SetActive (true);
		panel.gameObject.SetActive (true);
	}

	void ShowPanel(GameObject panel){
		DisablePanel.SetActive (true);
		panel.SetActive (true);
	}
	
	void HidePanel(Transform panel){
		DisablePanel.SetActive (false);
		panel.gameObject.SetActive (false);
	}

	void HidePanel(GameObject panel){
		DisablePanel.SetActive (false);
		panel.SetActive (false);
	}
}

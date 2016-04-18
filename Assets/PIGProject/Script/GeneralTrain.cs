using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
// 練功房

public class GeneralTrain : MonoBehaviour {
	public Transform CouragePopup;
	public Transform ForcePopup;
	public Transform StrengthPopup;
	public GameObject DisablePanel;

	public Button CourageButton;
	public Button ForceButton;
	public Button StrengthButton;

	// Use this for initialization
	Game game;
	void Start () {
		CallGeneralTrain ();
	}

	void CallGeneralTrain(){
		game = Game.Instance;
		SetupGeneralTrainingPrefab ();
		AddButtonListener ();
	}

	void AddButtonListener(){
		CourageButton.onClick.AddListener (() => {
			OnPanelOpen(CouragePopup);
		});
		ForceButton.onClick.AddListener (() => {
			OnPanelOpen(ForcePopup);
		});
		StrengthButton.onClick.AddListener (() => {
			OnPanelOpen(StrengthPopup);
		});
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
		int count = GeneralTrainPrefab.GeneralTrain.Count;
		for (int i = 0; i < count; i++) {
			GeneralTrainPrefab.GeneralTrain[i].SetAttrName(textDict[panel]);
			GeneralTrainPrefab.GeneralTrain[i].SetAttr(attrDict[panel]);
			GeneralTrainPrefab.GeneralTrain[i].transform.parent = panel.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0);
			RectTransform rTransform = GeneralTrainPrefab.GeneralTrain[i].GetComponent<RectTransform>();
			rTransform.localScale= Vector3.one;
			Debug.Log (GeneralTrainPrefab.GeneralTrain[i].transform.parent);
		}
	}

	void SetupGeneralTrainingPrefab(){
		var count = game.general.Count;
		for (var i=0; i <count; i++) {
			GeneralTrainPrefab gtp = Instantiate (Resources.Load ("GeneralTrainingPrefab") as GameObject).GetComponent<GeneralTrainPrefab> ();
			GeneralTrainPrefab.GeneralTrain.Add (gtp);
			gtp.general = game.general[i];
			gtp.SetName (gtp.general.attributes["Name"]);
			gtp.image.sprite = Resources.Load<UnityEngine.Sprite>("YaSinTipMukYi");
			Debug.Log (Resources.Load <Sprite> ("../PIGProject/_Scenes/Characters/YaSinTipMukYi"));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}



	void ShowPanel(Transform panel){
		DisablePanel.SetActive (true);
		panel.gameObject.SetActive (true);
	}
	
	void HidePanel(Transform panel){
		DisablePanel.SetActive (false);
		panel.gameObject.SetActive (false);
	}
}

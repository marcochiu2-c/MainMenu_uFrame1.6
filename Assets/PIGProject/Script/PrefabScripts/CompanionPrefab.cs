using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utilities;

public class CompanionPrefab: MonoBehaviour {
	public Image image;
	public Text Name;

	public static List<CompanionPrefab> person = new List<CompanionPrefab>();

	public static Transform commonPanel;
	public General general;
	public Counselor counselor;
	public bool isCounselor { get; set; }
	private Game game;
	GameObject CardHolder;

	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;
	// Use this for initialization
	void Start () {
		game = Game.Instance;
		CardHolder = Companion.staticCompanionHolder.GetChild (3).gameObject;
		SetCharacters ();
		AddButtonListener ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void AddButtonListener(){
		GetComponent<Button> ().onClick.AddListener (() => {
			ShowCardPanel();
		});
	}

	void ShowCardPanel(){
		int character = (isCounselor) ? counselor.type : general.type;
		Transform cardHolder = CardHolder.transform.GetChild(1);
		Image img = CardHolder.transform.GetChild (0).GetComponent<Image> ();
		Text Name = cardHolder.transform.GetChild (1).GetComponent<Text> ();
		Text Level = cardHolder.GetChild (3).GetComponent<Text> ();
		Text IQ = cardHolder.GetChild (5).GetComponent<Text> ();
		Text Leadership = cardHolder.GetChild (7).GetComponent<Text> ();
		Text Prestige = cardHolder.GetChild (9).GetComponent<Text> ();
		Text Courage = cardHolder.GetChild (11).GetComponent<Text> ();
		Text Force = cardHolder.GetChild (13).GetComponent<Text> ();
		Text Physical = cardHolder.GetChild (15).GetComponent<Text> ();
		Text Obedience = cardHolder.GetChild (17).GetComponent<Text> ();
		Text Formation = cardHolder.GetChild (19).GetComponent<Text> ();
		Text Knowledge = cardHolder.GetChild (21).GetComponent<Text> ();
		CardHolder.SetActive (true);

		if (isCounselor) {
			img.sprite = imageDict[counselor.type];
			Name.text = nameDict [counselor.type];
			Level.text=counselor.attributes["attributes"]["level"].AsInt.ToString();
			IQ.text=counselor.attributes["attributes"]["IQ"].AsFloat.ToString();
			Leadership.text=counselor.attributes["attributes"]["Leadership"].AsFloat.ToString();
			Prestige.text=counselor.attributes["attributes"]["Prestige"].AsFloat.ToString();
			Courage.text="-";
			Force.text="-";
			Physical.text="-";
			Obedience.text="-";
			Formation.text="-";
			Knowledge.text="-";
			Debug.Log("Knowledge: "+counselor.attributes["attributes"]["KnownKnowledge"].ToString());
		}else {
			img.sprite = imageDict[general.type];
			Name.text = nameDict[general.type];
			if (general.attributes["level"]!=null){
				Level.text=general.attributes["level"];
			}else{
				Level.text="1".ToString();
			}
			IQ.text=general.attributes["IQ"].AsFloat.ToString();
			Leadership.text=general.attributes["Leadership"].AsFloat.ToString();
			Prestige.text=general.attributes["Prestige"].AsFloat.ToString();
			Courage.text=general.attributes["Courage"].AsFloat.ToString();
			Force.text=general.attributes["Force"].AsFloat.ToString();
			Physical.text=general.attributes["Physical"].AsFloat.ToString();
			Obedience.text=general.attributes["Obedience"].AsFloat.ToString();
			Formation.text="-";
			Knowledge.text="-";
		}
	}


	public void showPanelItems( Transform panel){
		LoadHeadPic headPic = LoadHeadPic.Instance;
		char[] charToTrim = { '"' };
		transform.parent = panel;
//		string panelName = panel.parent.parent.parent.name;
		if (isCounselor) {
			Name.text = counselor.attributes["attributes"]["Name"].ToString().Trim (charToTrim);
			image.sprite = headPic.imageDict[counselor.type];
		} else {  
			Name.text = general.attributes["Name"].ToString().Trim (charToTrim);
			image.sprite = headPic.imageDict[general.type];
		}
		RectTransform rTransform = transform.GetComponent<RectTransform>();
		rTransform.localScale= Vector3.one;

	}
	
	public static void showSkillsOptionPanel(string panel){
		var count = Academy.cStudentList.Count;
		Button[] button = new Button[14];
		if (panel == "學問") { //Knowledge
			KnowledgeOption obj = Instantiate(Resources.Load("KnowledgeOptionPrefab") as GameObject).GetComponent<KnowledgeOption>();
			obj.gameObject.SetActive(true);
			RectTransform rTransform = obj.gameObject.GetComponent<RectTransform>();
			rTransform.localScale= new Vector3(1,1,1);
			rTransform.position = new Vector3(612.75f,-352.5f,1);
			obj.transform.SetParent(Academy.staticTeachHolder.transform,false);
			for (var i = 0; i <14; i++) {
				button[i] = obj.transform.GetChild(0).GetChild (1).GetChild(i).gameObject.GetComponent<Button>();
				button[i].interactable = false;
				//				Debug.Log(button[i]);
			}
			for (int i =0 ; i< count; i++){
				for (int j=0; j < 14; j++){
					if (Academy.cStudentList[i].id==23){
						for (int k = 0; k<Academy.cStudentList[i].attributes["skills"].Count;k++){
							if (button[j].transform.GetChild(0).gameObject.GetComponent<Text>().text ==
							    Academy.cStudentList[i].attributes["skills"][k]["name"].Value){
								button[j].interactable = true;
							}
						}
					}
					//				Debug.Log("First skill name: "+Academy.cStudentList[i].attributes["skills"][0]["name"]);
					//				Debug.Log("First skill level: "+Academy.cStudentList[i].attributes["skills"][0]["level"]);
				}
			}
		}
	}

	private void SetCharacters(){
		LoadBodyPic bodyPic = LoadBodyPic.Instance;
		imageDict = bodyPic.imageDict;
		nameDict = bodyPic.nameDict;
	}
}

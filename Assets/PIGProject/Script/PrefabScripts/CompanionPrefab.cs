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
	// Use this for initialization
	void Start () {
		game = Game.Instance;
	}
	
	// Update is called once per frame
	void Update () {
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
	
}

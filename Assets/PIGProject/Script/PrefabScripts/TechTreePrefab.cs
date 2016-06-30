using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utilities;

public class TechTreePrefab: MonoBehaviour {
	public Image image;
	public Text Name;
	public Text AttrName;
	public Text AttrValue;

	public static List<TechTreePrefab> person = new List<TechTreePrefab>();
	public Counselor counselor;
	private Game game;
	GameObject CardHolder;

	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;
	// Use this for initialization
	void Start () {
		game = Game.Instance;
//		CardHolder = Companion.staticCompanionHolder.GetChild (3).gameObject;
		SetCharacters ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//TODO  Disallow any assignment in any OnGoing Jobs

	public void AddButtonListener(){
		GetComponent<Button> ().onClick.AddListener (() => {
			Debug.Log("Assigning counselor id: "+counselor.id);

			TechTree.AssigningCounselor[TechTree.AssigningCounselorSlot] = counselor.id;
			TechTree.CounselorButtons[TechTree.AssigningCounselorSlot].GetComponent<Image>().sprite = imageDict[counselor.type];
			TechTree.CounselorButtons[TechTree.AssigningCounselorSlot].transform.GetChild(0).GetComponent<Text>().text = nameDict[counselor.type];
			gameObject.SetActive(false);
			TechTree.CounselorList.SetActive(false);
			Debug.Log ("Assigned Counselors after assignment: "+game.trainings[8].attributes["TechTreeCounselors"].Count);
			TechTree.TotalIQText.text = TechTree.TotalIQOfCounselors().ToString();
		});
	}

	public void SetCounselor(Counselor c){
		counselor = c;
		image.sprite =  imageDict[counselor.type];
		Name.text = nameDict[counselor.type];
		SetPanel ();
		showPanelItems (TechTree.CounselorList.transform);
	}
//
//	void ShowCardPanel(){
//		int character = counselor.type;
//		Transform cardHolder = CardHolder.transform.GetChild(1);
//		Image img = CardHolder.transform.GetChild (0).GetComponent<Image> ();
//		Text Name = cardHolder.transform.GetChild (1).GetComponent<Text> ();
//		Text Level = cardHolder.GetChild (3).GetComponent<Text> ();
//		Text IQ = cardHolder.GetChild (5).GetComponent<Text> ();
//		Text Leadership = cardHolder.GetChild (7).GetComponent<Text> ();
//		Text Prestige = cardHolder.GetChild (9).GetComponent<Text> ();
//		Text Courage = cardHolder.GetChild (11).GetComponent<Text> ();
//		Text Force = cardHolder.GetChild (13).GetComponent<Text> ();
//		Text Physical = cardHolder.GetChild (15).GetComponent<Text> ();
//		Text Obedience = cardHolder.GetChild (17).GetComponent<Text> ();
//		Text Formation = cardHolder.GetChild (19).GetComponent<Text> ();
//		Text Knowledge = cardHolder.GetChild (21).GetComponent<Text> ();
//		CardHolder.SetActive (true);
//
//
//		img.sprite = imageDict[counselor.type];
//		Name.text = nameDict [counselor.type];
//		Level.text=counselor.attributes["attributes"]["level"].AsInt.ToString();
//		IQ.text=counselor.attributes["attributes"]["IQ"].AsFloat.ToString();
//		Leadership.text=counselor.attributes["attributes"]["Leadership"].AsFloat.ToString();
//		Prestige.text=counselor.attributes["attributes"]["Prestige"].AsFloat.ToString();
//		Courage.text="-";
//		Force.text="-";
//		Physical.text="-";
//		Obedience.text="-";
//		Formation.text="-";
//		Knowledge.text="-";
//		Debug.Log("Knowledge: "+counselor.attributes["attributes"]["KnownKnowledge"].ToString());
//		
//	}
	public void SetPanel(){
		AttrName.text = "智商：";
		AttrValue.text = counselor.attributes["attributes"]["IQ"];
	}

	public void showPanelItems( Transform panel){
		LoadHeadPic headPic = LoadHeadPic.Instance;
		char[] charToTrim = { '"' };
		transform.SetParent( panel.GetChild(1).GetChild(1).GetChild(0));
//		string panelName = panel.parent.parent.parent.name;

		Name.text = counselor.attributes["attributes"]["Name"].ToString().Trim (charToTrim);
		image.sprite = headPic.imageDict[counselor.type];
		
		RectTransform rTransform = transform.GetComponent<RectTransform>();
		rTransform.localScale= Vector3.one;

	}

	private void SetCharacters(){
		LoadHeadPic headPic = LoadHeadPic.Instance;
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
	}
}

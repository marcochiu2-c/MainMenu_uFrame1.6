using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utilities;

public class AcademyStudentNew: MonoBehaviour {
	public Image image;
	public Text Name;
	public Text AttrName;
	public Text AttrValue;

	public static List<AcademyStudentNew> person = new List<AcademyStudentNew>();
	public static AcademyTeach currentTeachItem;
	public static Transform commonPanel;
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
		AddButtonListener ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	//TODO  Disallow any assignment in any OnGoing Jobs

	void AddButtonListener(){
		AcademyTeach at;
		GetComponent<Button> ().onClick.AddListener (() => {
			Academy.staticCounselorHolder.SetActive(false);

			if (Academy.CounselorHolderFunction == "SelfStudy"){
				SelfStudy ss =  Academy.CounselorHolderButton.GetComponent<SelfStudy>();
				ss.targetId = counselor.id;
				ss.trainingType = counselor.type;
				ss.image.sprite = imageDict[counselor.type];
				ss.ImageText.text = nameDict[counselor.type];
				ss.etaTimestamp = DateTime.Now+new TimeSpan(20,0,0);
				ConfirmSelfStudyIfOk(ss);
				gameObject.SetActive(false);
			}else if (Academy.CounselorHolderFunction == "TeacherImage"){
				at = Academy.CounselorHolderButton.transform.parent.GetComponent<AcademyTeach>();
//				if (AttrName.text!=""){
					currentTeachItem = at;
					at.trainerId = counselor.id;
					at.trainingType = counselor.type;
					at.TeacherPic = imageDict[counselor.type];
					at.TeacherImageText.text = nameDict[counselor.type];
					string category = Academy.staticTeachHolder.transform.GetChild (0).GetComponent<Text>().text;
					//					AcademyStudent.showSkillsOptionPanel(category);
					ConfirmTrainingIfOk(at);
					gameObject.SetActive(false);
//				}else{


//				}
			}else if (Academy.CounselorHolderFunction == "StudentImage"){
				at = Academy.CounselorHolderButton.transform.parent.GetComponent<AcademyTeach>();
//				if (AttrName.text!=""){
					currentTeachItem = at;
					at.targetId = counselor.id;
					at.targetType = counselor.type;
					at.StudentPic = imageDict[counselor.type];
					at.StudentImageText.text = nameDict[counselor.type];
					ConfirmTrainingIfOk(at);
					gameObject.SetActive(false);
//				}
			}
		});
	}

	public void ConfirmSelfStudyIfOk(SelfStudy aSelfStudy){
		Game game = Game.Instance;
		Text Title = Academy.staticSelfStudyHolder.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		GameObject ConfirmTraining = Academy.staticAcademyHolder.transform.GetChild (1).GetChild(6).gameObject;
		if (aSelfStudy.targetId != 0) {
			if (Title.text == "智商"){
				if (game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["IQ"].AsFloat <
				    game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["HighestIQ"].AsFloat){
					ConfirmTraining.SetActive(true);
				}else{
					ShowLowerThanTrainerPanel("isMaxPoint");
				}
			}else if (Title.text == "統率"){
				if (game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["Leadership"].AsFloat <
				    game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["HighestLeadership"].AsFloat){
					ConfirmTraining.SetActive(true);
				}else{
					ShowLowerThanTrainerPanel("isMaxPoint");
				}
			}else if (Title.text == "學問"){
				Debug.Log("學問 Clicked");
				ShowKnowledgeListPanel(aSelfStudy.targetId);
			}else if (Title.text == "陣法"){
				
			}
			
		}
	}


	public void ConfirmTrainingIfOk(AcademyTeach aTeach){
		Game game = Game.Instance;
		//		GameObject LowerThanTrainer = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(8).gameObject;
		//		GameObject ConfirmTeacherBy = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(7).gameObject;
		GameObject LowerThanTrainer = Academy.staticLowerThanTrainer;
		GameObject ConfirmTeacherBy = Academy.staticConfirmTeacherBy;
		if (aTeach.trainerId != 0 && aTeach.targetId != 0) {
			if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "智商"){
				//			if (transform.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(0).GetComponent<Text>().text == "智商"){
				//				Debug.Log (game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.trainerId)].attributes["attributes"]["IQ"].AsFloat);
				if (game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.trainerId)].attributes["attributes"]["IQ"].AsFloat >
				    game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["IQ"].AsFloat){
					if(game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["IQ"].AsFloat < 
					   game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["HighestIQ"].AsFloat){
						ConfirmTeacherBy.SetActive(true);
					}else{
						ShowLowerThanTrainerPanel("isMaxPoint");
					}
				}else{
					ShowLowerThanTrainerPanel();
				}
				
			}else if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "統率"){
				if (game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.trainerId)].attributes["attributes"]["Leadership"].AsFloat >
				    game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["Leadership"].AsFloat){
					if(game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["Leadership"].AsFloat < 
					   game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["HighestLeadership"].AsFloat){
						ConfirmTeacherBy.SetActive(true);
					}else{
						ShowLowerThanTrainerPanel("isMaxPoint");
					}
				}else{
					ShowLowerThanTrainerPanel();
				}
			}else if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "學問"){
				Debug.Log( "學問");
				ShowKnowledgeListPanel(aTeach.trainerId,aTeach.targetId);
			}else if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "陣法"){
				
			}
		}
	}

	public void ShowLowerThanTrainerPanel(string isMaxPoint=""){
		//		GameObject LowerThanTrainer = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(8).gameObject;
		GameObject LowerThanTrainer = Academy.staticLowerThanTrainer;
		LowerThanTrainer.SetActive(true);
		if (isMaxPoint == "") {
			Utilities.Panel.GetHeader (LowerThanTrainer).text = "青出於藍";
			Utilities.Panel.GetMessageText (LowerThanTrainer).text = "所選軍師之能力高於師傅，請重新選擇。";
		} else if (isMaxPoint == "isMaxPoint"){
			Utilities.Panel.GetHeader(LowerThanTrainer).text = "學有所成";
			Utilities.Panel.GetMessageText(LowerThanTrainer).text = "所選軍師徒弟智商已達頂點，請重新選擇。";
		}
	}

	public void ShowKnowledgeListPanel(int student){
		Game game = Game.Instance;
		GameObject KnowledgeListHolder = Academy.staticAcademyHolder.transform.GetChild (1).GetChild (5).gameObject;
		KnowledgeOption.Woodworker.interactable = true;
		KnowledgeOption.MetalFabrication.interactable = true;
		KnowledgeOption.ChainSteel.interactable = true;
		KnowledgeOption.MetalProcessing.interactable = true;
		KnowledgeOption.Crafts.interactable = true;
		KnowledgeOption.Geometry.interactable = true;
		KnowledgeOption.Physics.interactable = true;
		KnowledgeOption.Chemistry.interactable = true;
		KnowledgeOption.PeriodicTable.interactable = true;
		KnowledgeOption.Pulley.interactable = true;
		KnowledgeOption.Anatomy.interactable = true;
		KnowledgeOption.Catapult.interactable = true;
		KnowledgeOption.GunpowderModulation.interactable = true;
		KnowledgeOption.Psychology.interactable = true;
		
		KnowledgeListHolder.SetActive (true);
	}

	public void ShowKnowledgeListPanel(int teacher, int student){
		Game game = Game.Instance;
		GameObject panel = Academy.staticKnowledgeListHolder;
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Woodworker"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Woodworker"].AsInt) {
			KnowledgeOption.Woodworker.interactable = true;
		} else {
			KnowledgeOption.Woodworker.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["MetalFabrication"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["MetalFabrication"].AsInt) {
			KnowledgeOption.MetalFabrication.interactable = true;
		} else {
			KnowledgeOption.MetalFabrication.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["ChainSteel"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["ChainSteel"].AsInt) {
			KnowledgeOption.ChainSteel.interactable = true;
		} else {
			KnowledgeOption.ChainSteel.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["MetalProcessing"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["MetalProcessing"].AsInt) {
			KnowledgeOption.MetalProcessing.interactable = true;
		} else {
			KnowledgeOption.MetalProcessing.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Crafts"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Crafts"].AsInt) {
			KnowledgeOption.Crafts.interactable = true;
		} else {
			KnowledgeOption.Crafts.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Geometry"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Geometry"].AsInt) {
			KnowledgeOption.Geometry.interactable = true;
		} else {
			KnowledgeOption.Geometry.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Physics"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Physics"].AsInt) {
			KnowledgeOption.Physics.interactable = true;
		} else {
			KnowledgeOption.Physics.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Chemistry"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Chemistry"].AsInt) {
			KnowledgeOption.Chemistry.interactable = true;
		} else {
			KnowledgeOption.Chemistry.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["PeriodicTable"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["PeriodicTable"].AsInt) {
			KnowledgeOption.PeriodicTable.interactable = true;
		} else {
			KnowledgeOption.PeriodicTable.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Pulley"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Pulley"].AsInt) {
			KnowledgeOption.Pulley.interactable = true;
		} else {
			KnowledgeOption.Pulley.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Anatomy"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Anatomy"].AsInt) {
			KnowledgeOption.Anatomy.interactable = true;
		} else {
			KnowledgeOption.Anatomy.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Catapult"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Catapult"].AsInt) {
			KnowledgeOption.Catapult.interactable = true;
		} else {
			KnowledgeOption.Catapult.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["GunpowderModulation"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["GunpowderModulation"].AsInt) {
			KnowledgeOption.GunpowderModulation.interactable = true;
		} else {
			KnowledgeOption.GunpowderModulation.interactable = false;
		}
		if (game.counselor [game.counselor.FindIndex (x => x.id == teacher)].attributes ["attributes"] ["KnownKnowledge"] ["Psychology"].AsInt > 
		    game.counselor [game.counselor.FindIndex (x => x.id == student)].attributes ["attributes"] ["KnownKnowledge"] ["Psychology"].AsInt) {
			KnowledgeOption.Psychology.interactable = true;
		} else {
			KnowledgeOption.Psychology.interactable = false;
		}
		panel.SetActive (true);
	}

	public void SetCounselor(Counselor c){
		counselor = c;
		image.sprite =  imageDict[counselor.type];
		Name.text = nameDict[counselor.type];
		showPanelItems (Academy.staticCounselorHolder.transform);
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
	public void SetPanel(ActivePopupEnum panel){
		if (panel == ActivePopupEnum.IQPopup) {
			AttrName.text = "智商：";
			AttrValue.text = counselor.attributes["attributes"]["IQ"];
		} else if (panel == ActivePopupEnum.CommandedPopup) {
			AttrName.text = "統率：";
			AttrValue.text = counselor.attributes["attributes"]["Leadership"];
		} else if (panel == ActivePopupEnum.KnowledgePopup || panel == ActivePopupEnum.FightingPopup) {
			AttrName.text = "";
			AttrValue.text = "";
		}
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

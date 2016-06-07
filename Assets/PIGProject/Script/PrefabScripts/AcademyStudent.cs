using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utilities;

public class AcademyStudent : MonoBehaviour {
	public Image StudentImage;
	public Text StudentImageText;
	
	public Sprite StudentPic { get { return StudentImage.sprite; } set { StudentImage.sprite = value; } }
	public static List<AcademyStudent> Students = new List<AcademyStudent>();
	public static AcademyTeach currentTeachItem;
	public static Transform commonPanel;
	public int characterType;
	public int characterId;
	public bool isDropped=false;
	private Game game;
	bool Called = false;
	GameObject candidatePanel;
	public static GameObject currentStudentPrefab;
	public static bool IsLevelNotReach=false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI () {

		Draggable drag = gameObject.GetComponent ("Draggable") as Draggable;
		if (drag.parentToReturnTo != null) {
			if (drag.parentToReturnTo == drag.placeholderParent && !isDropped &&
			    (drag.placeholderParent.ToString() == "StudentImage (UnityEngine.RectTransform)" ||
			 drag.placeholderParent.ToString() == "TeacherImage (UnityEngine.RectTransform)")){

				#region DropTheImageAndInfoCopy

				AcademyTeach at = drag.placeholderParent.parent.GetComponent("AcademyTeach") as AcademyTeach;
				if (!at.isDropZoneEnabled && !Called){
					AcademyStudent.reCreateStudentItem(this,false);
					GameObject.Destroy(this.gameObject);
					Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
					Called = true ;
					return;
				}else if (!at.isDropZoneEnabled){
					Called = false;
					return;
				}
				currentTeachItem = at;
				game = Game.Instance;
				TimeSpan ts = new TimeSpan(24,0,0);
				int index = game.trainings.FindIndex(x => x == at.trainingObject);
				#region CheckCoolDownPeriodAndLevel
				if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "智商"){
					if (game.trainings[AcademyTeach.IQTeach.FindIndex(x => x == at)].etaTimestamp > DateTime.Now + ts){
						AcademyStudent.reCreateStudentItem(this,false);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						return;
					}
					if (index > 1 && index < 5){
						ShowLevelNotReachPanel();
						currentStudentPrefab =this.gameObject;
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						IsLevelNotReach = true;
						return;
					}
				}else if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "統率"){
					if (game.trainings[AcademyTeach.CommandedTeach.FindIndex(x => x == at)].etaTimestamp > DateTime.Now + ts){
						AcademyStudent.reCreateStudentItem(this,false);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						return;
					}
					if (index > 6 && index < 10){
						ShowLevelNotReachPanel();
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						IsLevelNotReach = true;
						return;
					}
				}else if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "學問"){
					if (game.trainings[AcademyTeach.KnowledgeTeach.FindIndex(x => x == at)].etaTimestamp > DateTime.Now + ts){
						AcademyStudent.reCreateStudentItem(this,false);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						return;
					}
					if (index > 11 && index < 15){
						ShowLevelNotReachPanel();
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						IsLevelNotReach = true;
						return;
					}
				}else if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "陣法"){
					if (game.trainings[AcademyTeach.FightingTeach.FindIndex(x => x == at)].etaTimestamp > DateTime.Now + ts){
						AcademyStudent.reCreateStudentItem(this,false);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						return;
					}
					if (index > 16 && index < 20){
						ShowLevelNotReachPanel();
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						IsLevelNotReach = true;
						return;
					}
				}

				if (drag.placeholderParent.ToString() == "StudentImage (UnityEngine.RectTransform)"){
					if (at.targetId != 0){
						Debug.Log (at.targetId);

						AcademyStudent.reCreateStudentItem(this,false);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						return;
					}
					at.targetId = characterId;
					at.targetType = characterType;
					at.StudentImageText.text = StudentImageText.text;
					ConfirmTrainingIfOk(at);
				}else if (drag.placeholderParent.ToString() == "TeacherImage (UnityEngine.RectTransform)"){
					if (at.trainerId != 0){
						Debug.Log (at.trainerId);
						AcademyStudent.reCreateStudentItem(this,true);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
						return;
					}
					at.trainerId = characterId;
					at.trainingType = characterType;
					at.TeacherImageText.text = StudentImageText.text;
					string category = Academy.staticTeachHolder.transform.GetChild (0).GetComponent<Text>().text;
//					AcademyStudent.showSkillsOptionPanel(category);
					ConfirmTrainingIfOk(at);
				}
				#endregion
				drag.placeholderParent.GetComponent<Image>().sprite = StudentPic;
				#endregion

				GameObject.Destroy(this.gameObject);
				Students.Remove(this.gameObject.GetComponent<AcademyStudent>());
				isDropped = true;
			}
		}
	}

	public void ConfirmTrainingIfOk(AcademyTeach aTeach){
		Game game = Game.Instance;
		GameObject LowerThanTrainer = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(8).gameObject;
		GameObject ConfirmTeacherBy = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(7).gameObject;
		if (aTeach.trainerId != 0 && aTeach.targetId != 0) {
			if (Academy.staticTeachHolder.transform.GetChild(0).GetComponent<Text>().text == "智商"){
//			if (transform.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(0).GetComponent<Text>().text == "智商"){
				//				Debug.Log (game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.trainerId)].attributes["attributes"]["IQ"].AsFloat);
				if (game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.trainerId)].attributes["attributes"]["IQ"].AsFloat >
				    game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["IQ"].AsFloat){
					if(game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["IQ"].AsFloat < 
					   game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["HighestIQ"].AsFloat){
						Academy.ShowPanel(ConfirmTeacherBy);
					}else{
						ShowLowerThanTrainerPanel("isMaxPoint");
					}
				}else{
					ShowLowerThanTrainerPanel();
				}

			}else if (transform.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(0).GetComponent<Text>().text == "統率"){
				if (game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.trainerId)].attributes["attributes"]["Leadership"].AsFloat >
				    game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["Leadership"].AsFloat){
					if(game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["Leadership"].AsFloat < 
					   game.counselor[ game.counselor.FindIndex(x=> x.id ==  aTeach.targetId)].attributes["attributes"]["HighestLeadership"].AsFloat){
						Academy.ShowPanel(ConfirmTeacherBy);
					}else{
						ShowLowerThanTrainerPanel("isMaxPoint");
					}
				}else{
					ShowLowerThanTrainerPanel();
				}
			}else if (transform.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(0).GetComponent<Text>().text == "學問"){
				ShowKnowledgeListPanel(aTeach.trainerId,aTeach.targetId);
			}else if (transform.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(0).GetComponent<Text>().text == "陣法"){

			}
		}
	}

	public void ShowLowerThanTrainerPanel(string isMaxPoint=""){
		GameObject LowerThanTrainer = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.parent.GetChild(1).GetChild(8).gameObject;
		Academy.ShowPanel(LowerThanTrainer);
		if (isMaxPoint == "") {
			Utilities.Panel.GetHeader (LowerThanTrainer).text = "青出於藍";
			Utilities.Panel.GetMessageText (LowerThanTrainer).text = "所選軍師之能力高於師傅，請重新選擇。";
		} else if (isMaxPoint == "isMaxPoint"){
			Utilities.Panel.GetHeader(LowerThanTrainer).text = "學有所成";
			Utilities.Panel.GetMessageText(LowerThanTrainer).text = "所選軍師徒弟智商已達頂點，請重新選擇。";
		}
	}

	public static void ShowIsMaxPointPanel(GameObject LowerThanTrainer){
		Academy.ShowPanel(LowerThanTrainer);
		Utilities.Panel.GetHeader(LowerThanTrainer).text = "學有所成";
		Utilities.Panel.GetMessageText(LowerThanTrainer).text = "所選軍師徒弟智商已達頂點，請重新選擇。";
	}

	public static void ShowLevelNotReachPanel(){
		GameObject LowerThanTrainer = Academy.staticAcademyHolder.transform.GetChild(1).GetChild(8).gameObject;
		Academy.ShowPanel(LowerThanTrainer);
		Utilities.Panel.GetHeader(LowerThanTrainer).text = "功能未開放";
		Utilities.Panel.GetMessageText(LowerThanTrainer).text = "請耐心等候。";
	}

	public void ShowKnowledgeListPanel(int teacher, int student){
		Game game = Game.Instance;
		GameObject panel = transform.parent.parent.parent.parent.parent.parent.parent.parent.parent.gameObject;
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
		panel.transform.GetChild (5).gameObject.SetActive (true);
	}

	public void SetAttributeText(string panel){
		Game game = Game.Instance;
		if (panel == "IQ" || panel == "智商") {
			transform.GetChild(1).GetComponent<Text>().text = "智商：";
			transform.GetChild(2).GetComponent<Text>().text = game.counselor [game.counselor.FindIndex (x => x.id == characterId)].attributes["attributes"]["IQ"];
		} else if (panel == "Commanded" || panel == "統率") {
			transform.GetChild(1).GetComponent<Text>().text = "統率：";
			transform.GetChild(2).GetComponent<Text>().text = game.counselor[ game.counselor.FindIndex (x=> x.id ==  characterId)].attributes["attributes"]["Leadership"];
		} else if (panel == "Knowledge" || panel == "學問") {
			transform.GetChild(1).GetComponent<Text>().text ="";
			transform.GetChild(2).GetComponent<Text>().text ="";
		} else if (panel == "Fighting" || panel == "陣法") {
//			transform.GetChild(1).GetComponent<Text>().text ="陣法：";
//			transform.GetChild(2).GetComponent<Text>().text = game.counselor[ game.counselor.FindIndex(x=> x.id ==  characterId)].attributes["attributes"]["Formation"];
		}
}


	public static void showPanelItems(List<AcademyStudent> items){
		var count = items.Count;
		for (var i =0; i < count; i++) {
			items[i].transform.parent = AcademyStudent.commonPanel;
			RectTransform rTransform = items[i].GetComponent<RectTransform>();
			rTransform.localScale= Vector3.one;
		}
	}


	public static void reCreateStudentItem(AcademyTeach aTeach){
		AcademyStudent obj1 = Instantiate(Resources.Load("AcademyStudentPrefab") as GameObject).GetComponent<AcademyStudent>();
		AcademyStudent obj2 = Instantiate(Resources.Load("AcademyStudentPrefab") as GameObject).GetComponent<AcademyStudent>();

		obj1.characterType = aTeach.trainingType;
		obj1.characterId = aTeach.trainerId;
		obj1.StudentPic = aTeach.TeacherPic;
		obj1.StudentImageText.text = aTeach.TeacherImageText.text;

		obj2.characterType = aTeach.targetType;
		obj2.characterId = aTeach.targetId;
		obj2.StudentPic = aTeach.StudentPic;
		obj2.StudentImageText.text = aTeach.StudentImageText.text;
		Debug.Log (aTeach.transform.parent.parent.parent.parent.parent.parent.GetChild (0));
		string panel = aTeach.transform.parent.parent.parent.parent.parent.parent.GetChild (0).GetComponent<Text> ().text;
		if (panel == "智商") {
			if (obj1.StudentPic !=null){
				obj1.SetAttributeText ("IQ");
			}
			if (obj2.StudentPic !=null){
				obj2.SetAttributeText ("IQ");
			}
		}else if (panel == "統率") {
			if (obj1.StudentPic !=null){
				obj1.SetAttributeText ("Commanded");
			}
			if (obj2.StudentPic !=null){
				obj2.SetAttributeText ("Commanded");
			}
		}else if (panel == "學問") {
			if (obj1.StudentPic !=null){
				obj1.SetAttributeText ("Knowledge");
			}
			if (obj2.StudentPic !=null){
				obj2.SetAttributeText ("Knowledge");
			}
		}else if (panel == "陣法") {
			if (obj1.StudentPic !=null){
				obj1.SetAttributeText ("Fighting");
			}
			if (obj2.StudentPic !=null){
				obj2.SetAttributeText ("Fighting");
			}
		}
		RectTransform rTransform;
		if (aTeach.TeacherPic != null) {
			obj1.transform.parent = AcademyStudent.commonPanel;
			rTransform = obj1.GetComponent<RectTransform> ();
			rTransform.localScale = Vector3.one;
			Students.Add(obj1);
		} else {
			GameObject.Destroy(obj1);
		}
		if (aTeach.StudentPic != null) {
			obj2.transform.parent = AcademyStudent.commonPanel;
			rTransform = obj2.GetComponent<RectTransform> ();
			rTransform.localScale = Vector3.one;
			Students.Add(obj2);
		} else {
			GameObject.Destroy(obj2);
		}
	}

	public static void reCreateStudentItem(AcademyStudent aStudent, bool isTeacher){
		List<CounselorCards> c = CounselorCards.GetList (1);
		AcademyStudent obj = Instantiate(Resources.Load("AcademyStudentPrefab") as GameObject).GetComponent<AcademyStudent>();
		Debug.Log ("reCreateStudentItem()");
		obj.characterId = aStudent.characterId;
		obj.characterType = aStudent.characterType;
		obj.StudentPic = aStudent.StudentPic;
		obj.StudentImageText.text = c [aStudent.characterType-1].Name;
		string panel = aStudent.transform.parent.parent.parent.parent.parent.parent.parent.parent.GetChild (0).GetComponent<Text> ().text;
		if (panel == "智商") {
			obj.SetAttributeText ("IQ");
		}else if (panel == "統率") {
			obj.SetAttributeText ("Commanded");
		}else if (panel == "學問") {
			obj.SetAttributeText ("Knowledge");
		}else if (panel == "陣法") {
			obj.SetAttributeText ("Fighting");
		}

		obj.transform.parent = AcademyStudent.commonPanel;
		RectTransform rTransform = obj.GetComponent<RectTransform>();
		rTransform.localScale= Vector3.one;
		Students.Add(obj);
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

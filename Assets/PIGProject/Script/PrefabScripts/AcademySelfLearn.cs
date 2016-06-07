using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utilities;

public class AcademySelfLearn : MonoBehaviour {
	public Image StudentImage;
	public Text StudentImageText;
	
	public Sprite StudentPic { get { return StudentImage.sprite; } set { StudentImage.sprite = value; } }
	public static List<AcademySelfLearn> Students = new List<AcademySelfLearn>();
	public static Transform commonPanel;
	public int characterType;
	public int characterId;
	public bool isDropped=false;
	private Game game;
	bool Called = false;
	GameObject candidatePanel;
	public static GameObject currentStudentPrefab;
	public static bool isSelfStudy;
	public static SelfStudy currentSelfStudy;
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
			    (drag.placeholderParent.ToString() == "Panel (UnityEngine.RectTransform)")){
				Debug.Log (drag);
				#region DropTheImageAndInfoCopy
				
				SelfStudy at = drag.placeholderParent.GetComponent("SelfStudy") as SelfStudy;
				if (!at.isDropZoneEnabled && !Called){
					AcademySelfLearn.reCreateStudentItem(this);
					GameObject.Destroy(this.gameObject);
					Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
					Called = true ;
					return;
				}else if (!at.isDropZoneEnabled){
					Called = false;
					return;
				}

				currentSelfStudy = at;
				game = Game.Instance;
				TimeSpan ts = new TimeSpan(24,0,0);
				Text Title = Academy.staticSelfStudyHolder.transform.GetChild(0).GetChild(0).GetComponent<Text>();
				#region CheckCoolDownPeriod
				if (Title.text == "智商"){
					if (game.trainings[at.transform.GetSiblingIndex()/2].etaTimestamp > DateTime.Now + ts){
						AcademySelfLearn.reCreateStudentItem(this);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
						return;
					}
				}else if (Title.text == "統率"){
					if (game.trainings[at.transform.GetSiblingIndex()/2].etaTimestamp > DateTime.Now + ts){
						AcademySelfLearn.reCreateStudentItem(this);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
						return;
					}
				}else if (Title.text == "學問"){
					if (game.trainings[at.transform.GetSiblingIndex()/2].etaTimestamp > DateTime.Now + ts){
						AcademySelfLearn.reCreateStudentItem(this);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
						return;
					}
				}else if (Title.text == "陣法"){
					if (game.trainings[at.transform.GetSiblingIndex()/2].etaTimestamp > DateTime.Now + ts){
						AcademySelfLearn.reCreateStudentItem(this);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
						return;
					}
				}
				#endregion
				if (drag.placeholderParent.ToString() == "Panel (UnityEngine.RectTransform)"){
					if (at.targetId != 0){
						Debug.Log (at.targetId);
						
						AcademySelfLearn.reCreateStudentItem(this);
						GameObject.Destroy(this.gameObject);
						Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
						return;
					}
					at.targetId = characterId;
					at.trainingType = characterType;
					at.ImageText.text = StudentImageText.text;
					at.etaTimestamp = DateTime.Now+new TimeSpan(20,0,0);
					ConfirmTrainingIfOk(at);
				}
				drag.placeholderParent.GetComponent<Image>().sprite = StudentPic;
				#endregion
				
				#region OpenTeacherSkillsPanel
				// TODO if target object is TeacherImage, show panel for choosing skills/knowledges
				// TODO if both TeacherImage and StudentImage filled and choosen skills/knowledges, upload the details to DB.
				GameObject.Destroy(this.gameObject);
				Students.Remove(this.gameObject.GetComponent<AcademySelfLearn>());
				#endregion
				isDropped = true;
			}
		}
	}
	
	public void ConfirmTrainingIfOk(SelfStudy aSelfStudy){
		Game game = Game.Instance;
		Text Title = Academy.staticSelfStudyHolder.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		GameObject ConfirmTraining = Academy.staticAcademyHolder.transform.GetChild (1).GetChild(6).gameObject;
		if (aSelfStudy.targetId != 0) {
			isSelfStudy = true;
			if (Title.text == "智商"){
				if (game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["IQ"].AsFloat <
				    game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["HighestIQ"].AsFloat){
					Academy.ShowPanel(ConfirmTraining);
				}else{
					ShowLowerThanTrainerPanel("isMaxPoint");
				}
			}else if (Title.text == "統率"){
				if (game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["Leadership"].AsFloat <
				    game.counselor.Find (x=> x.id == aSelfStudy.targetId).attributes["attributes"]["HighestLeadership"].AsFloat){
					Academy.ShowPanel(ConfirmTraining);
				}else{
					ShowLowerThanTrainerPanel("isMaxPoint");
				}
			}else if (Title.text == "學問"){
				ShowKnowledgeListPanel(aSelfStudy.targetId);
			}else if (Title.text == "陣法"){
				
			}

		}
	}
	
	public void ShowLowerThanTrainerPanel(string isMaxPoint=""){
		GameObject LowerThanTrainer = Academy.staticAcademyHolder.transform.GetChild (1).GetChild(8).gameObject;
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
		LowerThanTrainer.SetActive(true);
		Utilities.Panel.GetHeader(LowerThanTrainer).text = "學有所成";
		Utilities.Panel.GetMessageText(LowerThanTrainer).text = "所選軍師徒弟智商已達頂點，請重新選擇。";
	}
	
	public static void ShowLevelNotReachPanel(){
		GameObject LowerThanTrainer = Academy.staticAcademyHolder.transform.GetChild(1).GetChild(8).gameObject;
		LowerThanTrainer.SetActive(true);
		Utilities.Panel.GetHeader(LowerThanTrainer).text = "功能未開放";
		Utilities.Panel.GetMessageText(LowerThanTrainer).text = "請耐心等候。";
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
	
	
	public static void showPanelItems(List<AcademySelfLearn> items){
		var count = items.Count;
		for (var i =0; i < count; i++) {
			items[i].transform.parent = AcademySelfLearn.commonPanel;
			RectTransform rTransform = items[i].GetComponent<RectTransform>();
			rTransform.localScale= Vector3.one;
		}
	}
	
	
	public static void reCreateStudentItem(SelfStudy aTeach){
		AcademySelfLearn obj = Instantiate(Resources.Load("AcademySelfLearnPrefab") as GameObject).GetComponent<AcademySelfLearn>();

//		obj.characterType = aTeach.targetType;
		obj.characterId = aTeach.targetId;
		obj.StudentPic = aTeach.image.sprite;
		obj.StudentImageText.text = aTeach.ImageText.text;
		string panel = Academy.staticAcademyHolder.transform.GetChild (1).GetChild (3).GetChild(0).GetChild (0).GetComponent<Text> ().text;
		if (panel == "智商") {
			obj.SetAttributeText ("IQ");
		}else if (panel == "統率") {
			obj.SetAttributeText ("Commanded");
			
		}else if (panel == "學問") {
			obj.SetAttributeText ("Knowledge");
		}else if (panel == "陣法") {
			obj.SetAttributeText ("Fighting");
		}

		obj.transform.parent = AcademySelfLearn.commonPanel;
		RectTransform rTransform = obj.GetComponent<RectTransform> ();
		rTransform.localScale = Vector3.one;
		Students.Add(obj);
	}
	
	public static void reCreateStudentItem(AcademySelfLearn aStudent){
		List<CounselorCards> c = CounselorCards.GetList (1);
		AcademySelfLearn obj = Instantiate(Resources.Load("AcademySelfLearnPrefab") as GameObject).GetComponent<AcademySelfLearn>();
		Debug.Log ("reCreateStudentItem()");
		obj.characterId = aStudent.characterId;
		obj.characterType = aStudent.characterType;
		obj.StudentPic = aStudent.StudentPic;
		obj.StudentImageText.text = c [aStudent.characterType-1].Name;
		string panel = Academy.staticAcademyHolder.transform.GetChild (1).GetChild (3).GetChild(0).GetChild (0).GetComponent<Text> ().text;
		if (panel == "智商") {
			obj.SetAttributeText ("IQ");
		}else if (panel == "統率") {
			obj.SetAttributeText ("Commanded");
		}else if (panel == "學問") {
			obj.SetAttributeText ("Knowledge");
		}else if (panel == "陣法") {
			obj.SetAttributeText ("Fighting");
		}
		
		obj.transform.parent = AcademySelfLearn.commonPanel;
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

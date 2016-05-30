//#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using SimpleJSON;
using UnityEngine.EventSystems;
using System;
using System.Linq;

//public enum ActivePopupEnum{
//	none,IQPopup,CommandedPopup,KnowledgePopup,FightingPopup
//}

public class Companion : MonoBehaviour
{
	
	
	class ItemData
	{
		public float SliderValue;
		public string ImageKey;
	}
	
	string[] qaBtnName = new string[2];
	public Button CloseButton;
	public Button BackButton;
	public GameObject CompanionHolder;
	public Transform GeneralsHolder;
	public Transform CounselorsHolder;
	public Button CounselorsButton;
	public Button GeneralsButton;
	public GameObject CardHolder;

	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;

	public static Transform staticCompanionHolder;
	public static List<General> cGeneralList;
	public static List<Counselor> cCounselorList;

	Transform CounselorScrollPanel;
	Transform GeneralScrollPanel;

	WsClient wsc;
	Game game;
	
	public static ActivePopupEnum activePopup;
	Dictionary<ActivePopupEnum,string> academyCategoryText = new Dictionary<ActivePopupEnum, string>();
	Dictionary<string,ActivePopupEnum> activePopupName = new Dictionary<string, ActivePopupEnum>();
	private const int columnWidthCount = 5;
	
	// Use this for initialization
	void OnEnable(){
		CallCompanion ();
	}
	

	public void CallCompanion(){
		
		wsc = WsClient.Instance;
		game = Game.Instance;
		cCounselorList = new List<Counselor> ();
		cGeneralList = new List<General> ();
		
		LoadHeadPic headPic = LoadHeadPic.Instance;
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;

		CounselorScrollPanel = CounselorsHolder.GetChild (1).GetChild (1).GetChild (0);
		GeneralScrollPanel = GeneralsHolder.GetChild (1).GetChild (1).GetChild (0);

		staticCompanionHolder = CompanionHolder.transform;

		SetupPrefabList ();
		AddButtonListener ();

		//Get the default Self Study Sprite
//		SelfStudy.defaultSprite = SelfStudyHolder.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (0).GetComponent<Image> ().sprite;

	}

	public void AddButtonListener(){
		CloseButton.onClick.AddListener (() => {
			foreach (CompanionPrefab co in CompanionPrefab.person) {
				GameObject.DestroyImmediate(co.gameObject);
			}
			CompanionPrefab.person.Clear();
		});
	}

	public void SetupPrefabList(){
		cCounselorList = game.counselor;
		cGeneralList = game.general;

		CompanionPrefab obj;
		int count = cCounselorList.Count;
		for (int i = 0; i < count; i++) {
			obj =  Instantiate(Resources.Load("CompanionPrefab") as GameObject).GetComponent<CompanionPrefab>();
			obj.transform.parent = CounselorScrollPanel.transform;
			obj.counselor = cCounselorList[i];
			obj.isCounselor = true;
			obj.showPanelItems(CounselorScrollPanel);
			CompanionPrefab.person.Add(obj);
		}
		count = cGeneralList.Count;
		for (int i = 0; i < count; i++) {
			obj = Instantiate(Resources.Load("CompanionPrefab") as GameObject).GetComponent<CompanionPrefab>();
			obj.transform.parent = GeneralScrollPanel.transform;
			obj.general = cGeneralList[i];
			obj.isCounselor = false;
			obj.showPanelItems(GeneralScrollPanel);
			CompanionPrefab.person.Add(obj);
		}

		Debug.Log ("Count of Counselor Prefab: "+CounselorsHolder.transform.GetChild (1).GetChild(1).GetChild(0).childCount);
		Debug.Log ("Count of General Prefab: "+GeneralsHolder.transform.GetChild (1).GetChild(1).GetChild(0).childCount);
//		
//		int cslCount = 0; 
//		List<Trainings> tList = game.trainings;
//		var tlCount = tList.Count;
//		for (var i = 0 ; i < cStudentList.Count ; i++){
//			for (var j = 0 ; j < tlCount ; j++){
//				if (tList[j].trainerId == Academy.cStudentList[i].id || tList[j].targetId == Academy.cStudentList[i].id){
//					//					Debug.Log ("Id of character those are training: "+Academy.cStudentList[i].id);
//					Academy.cStudentList.Remove(Academy.cStudentList[i]);
//					Academy.cSelfLearnList.Remove(Academy.cSelfLearnList[i]);
//				}
//			}
//		}  
//		cslCount = Academy.cStudentList.Count;
//		for (var i = 0 ; i < cslCount ; i++){
//			CreateStudentItem (Academy.cStudentList[i]);
//		}
//		cslCount = Academy.cSelfLearnList.Count;
//		for (var i = 0 ; i < cslCount ; i++){
//			CreateSelfLearnItem (Academy.cSelfLearnList[i]);
//		}
	}

	
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	void OnGUI()
	{

	}
	
	public void SetPanelParent(string panelToShow){
		var count = 0;
		if (panelToShow == "IQ") {
			count = AcademyTeach.IQTeach.Count;
			for (var i = 0; i < count; i++){
				
			}
		}
	}
	

	
//	void AddButtonListener(){
//		for (var i = 0; i < 4; i++) {
//			Transform child = buttons [i].transform;
//			buttons[i].onClick.AddListener(() => { OnButtonClick(child.GetComponent<Button>()); });
//		}
//		for (var i = 0; i < 2; i++) {
//			Transform child = qaButtons [i].transform;
//			qaButtons [i].onClick.AddListener (() => {
//				OnQAButtonClick (child.GetComponent<Button> ()); });
//		}
//		
//		backButton.onClick.AddListener (() => {
//			var count = AcademyStudent.Students.Count;
//			for (int i = 0 ; i < count ; i++){
//				GameObject.DestroyImmediate( AcademyStudent.Students[i].gameObject);
//				//				AcademyStudent.Students.Remove(AcademyStudent.Students[0].gameObject.GetComponent<AcademyStudent>());
//			}
//			count =AcademySelfLearn.Students.Count;
//			for (int i = 0 ; i < count ; i++){
//				GameObject.DestroyImmediate( AcademySelfLearn.Students[i].gameObject);
//				//				AcademySelfLearn.Students.Remove(AcademySelfLearn.Students[0].gameObject.GetComponent<AcademySelfLearn>());
//			}
//			AcademyStudent.Students = new List<AcademyStudent>();
//			AcademySelfLearn.Students = new List<AcademySelfLearn>();
//			Academy.activePopup = ActivePopupEnum.none;
//			SelfStudyHolder.SetActive(false);
//			TeachHolder.SetActive(false);
//			gameObject.SetActive(true);
//		});
//		closeButton.onClick.AddListener (() => {
//			var count = AcademyStudent.Students.Count;
//			Debug.Log(count);
//			for (int i = 0 ; i < count ; i++){
//				GameObject.DestroyImmediate( AcademyStudent.Students[i].gameObject);
//				//				AcademyStudent.Students.Remove(AcademyStudent.Students[i].gameObject.GetComponent<AcademyStudent>());
//			}
//			count =AcademySelfLearn.Students.Count;
//			Debug.Log(count);
//			for (int i = 0 ; i < count ; i++){
//				GameObject.DestroyImmediate( AcademySelfLearn.Students[i].gameObject);
//				//				AcademySelfLearn.Students.Remove(AcademySelfLearn.Students[i].gameObject.GetComponent<AcademySelfLearn>());
//			}
//			AcademyStudent.Students = new List<AcademyStudent>();
//			Debug.Log (AcademyStudent.Students.Count);
//			AcademySelfLearn.Students = new List<AcademySelfLearn>();
//			Academy.activePopup = ActivePopupEnum.none;
//			SelfStudyHolder.SetActive(false);
//			TeachHolder.SetActive(false);
//			CompanionHolder.SetActive(false);
//			MainScene.MainUIHolder.SetActive(true);
//		});
//		KnowledgeListHolder.transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (() => {
//			KnowledgeListHolder.SetActive(false);
//			ResetTeachPanel();
//		});
//		
//		Utilities.Panel.GetConfirmButton(LowerThanTrainer).onClick.AddListener (() => {
//			LowerThanTrainer.SetActive(false);
//			
//			
//			if (AcademySelfLearn.isSelfStudy){
//				AcademySelfLearn.reCreateStudentItem(AcademySelfLearn.currentStudentPrefab.GetComponent<AcademySelfLearn>());
//				GameObject.Destroy(AcademySelfLearn.currentStudentPrefab);
//				AcademySelfLearn.Students.Remove(AcademySelfLearn.currentStudentPrefab.GetComponent<AcademySelfLearn>());
//				
//				AcademySelfLearn.isSelfStudy = false;
//				return;
//			}
//			ResetTeachPanel();
//			if (AcademyStudent.IsLevelNotReach){
//				AcademyStudent.reCreateStudentItem(AcademyStudent.currentStudentPrefab.GetComponent<AcademyStudent>(),false);
//				GameObject.Destroy(AcademyStudent.currentStudentPrefab);
//				AcademyStudent.Students.Remove(AcademyStudent.currentStudentPrefab.GetComponent<AcademyStudent>());
//				AcademyStudent.IsLevelNotReach = false;
//			}
//			
//		});
//		
//		Utilities.Panel.GetConfirmButton (ConfirmTeacherBy).onClick.AddListener (() => {
//			ConfirmTeacherBy.SetActive(false);
//			//TODO set training object
//			Trainings obj  = AcademyStudent.currentTeachItem.trainingObject;
//			int index = game.trainings.FindIndex(x => x == obj);
//			Debug.Log("Training item index in DB: "+obj.id);
//			game.trainings[index].trainerId = AcademyStudent.currentTeachItem.trainerId;
//			game.trainings[index].targetId = AcademyStudent.currentTeachItem.targetId;
//			game.trainings[index].startTimestamp = DateTime.Now;
//			game.trainings[index].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught,0,0);
//			game.trainings[index].status = 1;
//			if (index < 5){
//				game.trainings[index].type = 1;
//				AcademyStudent.currentTeachItem.KnowledgeText.text = (game.counselor.Find(x => x.id == AcademyStudent.currentTeachItem.targetId).attributes["attributes"]["IQ"].AsInt+1).ToString();
//			}else if (index > 4 && index < 10){
//				game.trainings[index].type = 2;
//				AcademyStudent.currentTeachItem.KnowledgeText.text = (game.counselor.Find(x => x.id == AcademyStudent.currentTeachItem.targetId).attributes["attributes"]["Leadership"].AsInt+1).ToString();
//			}else if (index > 9 && index < 15){
//				game.trainings[index].type = KnowledgeOption.knowledge;
//				Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
//				AcademyStudent.currentTeachItem.KnowledgeText.text = String.Format ("{0}({1})",
//				                                                                    KnowledgeDict[KnowledgeOption.knowledge],
//				                                                                    game.counselor.Find(x => x.id == game.trainings[index].targetId).attributes["attributes"]["KnownKnowledge"][KnowledgeOption.knowledgeName]
//				                                                                    ); 
//			}else if (index > 14 && index < 20){
//				
//			}
//			Debug.Log (game.trainings[index].toJSON());
//			game.trainings[index].UpdateObject();
//			AcademyStudent.currentTeachItem.LeftTimeText.text = Utilities.TimeUpdate.Time (game.trainings[index].etaTimestamp);
//			KnowledgeOption.knowledge = 0;
//			KnowledgeOption.knowledgeName ="";
//		});
//		
//		Utilities.Panel.GetCancelButton (ConfirmTeacherBy).onClick.AddListener (() => {
//			ConfirmTeacherBy.SetActive(false);
//			ResetTeachPanel();
//		});
//		
//		Utilities.Panel.GetConfirmButton (ConfirmTraining).onClick.AddListener (() => {
//			ConfirmTraining.SetActive(false);
//			char[] charToTrim = { '"' };
//			Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
//			//TODO set training object
//			int index =0;
//			string title = SelfStudyHolder.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
//			if (title == "智商"){
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+20;
//			}else
//			if (title == "統率"){
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+25;
//			}else
//			if (title == "學問"){
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+30;
//			}else if (title == "陣法"){
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+35;
//			}
//			Debug.Log("Training item index in DB: "+game.trainings[index].id);
//			Debug.Log (index);
//			game.trainings[index].trainerId = 0;
//			game.trainings[index].targetId = AcademySelfLearn.currentSelfStudy.targetId;
//			game.trainings[index].startTimestamp = DateTime.Now;
//			game.trainings[index].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught * 2,0,0);
//			game.trainings[index].status = 1;
//			if (index < 25){
//				game.trainings[index].type = 1;
//				AcademySelfLearn.currentSelfStudy.ImageText.text = (game.counselor[game.counselor.FindIndex(x => x.id == AcademySelfLearn.currentSelfStudy.targetId)].attributes["attributes"]["IQ"].AsInt+1).ToString();
//			}else if (index > 24 && index < 30){
//				game.trainings[index].type = 2;
//				AcademySelfLearn.currentSelfStudy.ImageText.text = (game.counselor[game.counselor.FindIndex(x => x.id == AcademySelfLearn.currentSelfStudy.targetId)].attributes["attributes"]["Leadership"].AsInt+1).ToString();
//			}else if (index > 29 && index < 35){
//				game.trainings[index].type = KnowledgeOption.knowledge;
//				Debug.Log (KnowledgeOption.knowledge) ;
//				AcademySelfLearn.currentSelfStudy.ImageText.text = String.Format("{0} {1}({2})",
//				                                                                 (game.counselor[game.counselor.FindIndex(x => x.id == AcademySelfLearn.currentSelfStudy.targetId)].attributes["attributes"]["Name"]).ToString().Trim (charToTrim),
//				                                                                 KnowledgeDict[KnowledgeOption.knowledge],
//				                                                                 (game.counselor.Find(x => x.id == game.trainings[index].targetId).attributes["attributes"]["IQ"].AsFloat+1).ToString()
//				                                                                 );
//			}else if (index > 34 && index < 40){
//				
//			}
//			game.trainings[index].UpdateObject();
//			AcademySelfLearn.currentSelfStudy.ImageText.text +=  "\n"+Utilities.TimeUpdate.Time ( game.trainings[index].etaTimestamp);
//			KnowledgeOption.knowledge = 0;
//			KnowledgeOption.knowledgeName ="";
//		});
//		
//		Utilities.Panel.GetCancelButton (ConfirmTraining).onClick.AddListener (() => {
//			ConfirmTraining.SetActive(false);
//			ResetSelfStudyItem();
//		});
//	}




}


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
using Utilities;

public enum ActivePopupEnum{
	none,IQPopup,CommandedPopup,KnowledgePopup,FightingPopup
}

public class Academy : MonoBehaviour
{


	class ItemData
	{
		public float SliderValue;
		public string ImageKey;
	}

	public Button[] buttons = new Button[4];
	string[] btnName = new string[4];
	public Button[] qaButtons = new Button[2];
	string[] qaBtnName = new string[2];
	public Button closeButton;
	public Button backButton;
	public GameObject AcademyHolder;
	public GameObject SelfStudyHolder;
	public GameObject TeachHolder;
	public GameObject QAHolder;
	public GameObject KnowledgeListHolder;
	public GameObject ConfirmTeacherBy;
	public GameObject ConfirmTraining;
	public GameObject CounselorHolder;
	public GameObject LowerThanTrainer;
	public Transform TeachScrollPanel;
	public Transform StudentScrollPanel;
	public Transform SelfLearnScrollPanel;
	public static Dictionary<int,Sprite> imageDict;
	public static Dictionary<int,string> nameDict;
	public static GameObject staticLowerThanTrainer;
	public static GameObject staticConfirmTeacherBy;
	public static GameObject staticConfirmTraining;
	public static GameObject staticTeachHolder;
	public static GameObject staticSelfStudyHolder;
	public static GameObject staticKnowledgeListHolder;
	public static GameObject staticCounselorHolder;
	public static List<Counselor> cStudentList;
	public static List<Counselor> cSelfLearnList;
	bool firstCalled = false;
	public static string CounselorHolderFunction="";
	public static Button CounselorHolderButton;
	public int TrainingTimeTaught = 10;  // in hours
	public int TrainingCoolDownPeriod = 24; // in hours, cannot train again in the same slot for given hours.
	public static GameObject staticAcademyHolder;
	public static bool IsLevelNotReach=true;

	Text SelfStudyInstructionText;
	Text TeachInstructionText;
	WsClient wsc;
	Game game;
	
	public static ActivePopupEnum activePopup;
	Dictionary<ActivePopupEnum,string> academyCategoryText = new Dictionary<ActivePopupEnum, string>();
	Dictionary<string,ActivePopupEnum> activePopupName = new Dictionary<string, ActivePopupEnum>();
	Dictionary<ActivePopupEnum,string> academyDescription = new Dictionary<ActivePopupEnum, string>();
	private const int columnWidthCount = 5;
	List<TechTreeObject> techTreeList = new List<TechTreeObject> ();
	int numberOfTech = 0;

	// Use this for initialization
	void Start(){
		CallAcademy ();
	}

	public void CallAcademy(){

		btnName [0] = "IQButton";
		btnName [1] = "CommandedButton";
		btnName [2] = "KnowledgeButton";
		btnName [3] = "FightingButton";
		qaBtnName [0] = "SelfStudyButton";
		qaBtnName [1] = "TeachButton";
		SetDictionary ();
		Debug.Log ("Supposed to be AcademyHolder: "+transform);
		Debug.Log ("Supposed to be base panel of AcademyHolder: "+transform.GetChild(1));

		AddButtonListener ();
		KnowledgeOption.AssignButton (KnowledgeListHolder);
		KnowledgeOption.AddButtonListener (KnowledgeListHolder,gameObject);
		staticTeachHolder = TeachHolder;
		staticAcademyHolder = AcademyHolder;
		staticSelfStudyHolder = SelfStudyHolder;
		staticLowerThanTrainer = LowerThanTrainer;
		staticConfirmTeacherBy = ConfirmTeacherBy;
		staticConfirmTraining = ConfirmTraining;
		staticKnowledgeListHolder = KnowledgeListHolder;
		staticCounselorHolder = CounselorHolder;
		SelfStudyInstructionText = SelfStudyHolder.transform.GetChild (0).GetChild (1).GetChild (1).GetChild (0).GetChild (0).GetChild (0).GetComponent<Text>();
		TeachInstructionText = TeachHolder.transform.GetChild (1).GetChild (1).GetChild (1).GetChild (0).GetChild (0).GetChild (0).GetComponent<Text>();

		wsc = WsClient.Instance;
		game = Game.Instance;
		cStudentList = new List<Counselor> ();
		cSelfLearnList = new List<Counselor> ();
		techTreeList = TechTreeObject.GetList (1);
		numberOfTech = techTreeList.Count;

		//imageDict = new Dictionary<int,Sprite>();
		//nameDict = new Dictionary<int,string> ();
		SetCharacters ();
		SelfStudy.SetCharacters ();

		AcademyTeach.commonPanel = TeachScrollPanel;
		AcademyStudent.commonPanel = StudentScrollPanel;
		AcademySelfLearn.commonPanel = SelfLearnScrollPanel;
		
		#region setListOfTrainings
//		List<Trainings> tList = game.trainings;

//		var tlCount = tList.Count;
//		
//		for (int k = 0 ; k < tlCount ; k++){
//			
//		}
		
		#endregion

//		SetupStudentPrefabList ();
//		SetupAcademyTaughtPanel ();
//		SetTeachItems (tList);


		//Get the default Self Study Sprite
		SelfStudy.defaultSprite = SelfStudyHolder.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (0).GetComponent<Image> ().sprite;
	}

	public void SetupStudentPrefabList(){
//		Utilities.ShowLog.Log ("Counselor Wisdom: " + game.counselor [1].attributes.ToString());
		for (int j = 0 ; j < game.counselor.Count; j++){
			cStudentList.Add (new Counselor(game.counselor[j]));
			cSelfLearnList.Add (new Counselor(game.counselor[j]));
		}
		Debug.Log ("Number of Counselors: "+game.counselor.Count);

		int cslCount = 0; 
		List<Trainings> tList = game.trainings;
		var tlCount = tList.Count;
		for (var i = 0 ; i < Academy.cStudentList.Count ; i++){
			for (var j = 0 ; j < tlCount ; j++){
				if (tList[j].trainerId == Academy.cStudentList[i].id || tList[j].targetId == Academy.cStudentList[i].id){
//					Debug.Log ("Id of character those are training: "+Academy.cStudentList[i].id);
					Academy.cStudentList.Remove(Academy.cStudentList[i]);
					Academy.cSelfLearnList.Remove(Academy.cSelfLearnList[i]);
				}
			}
		}  
		cslCount = Academy.cStudentList.Count;
		Debug.Log ("Count of Students: "+cslCount);
		for (var i = 0 ; i < cslCount ; i++){
//			CreateStudentItem (Academy.cStudentList[i]);
			StartCoroutine( CreateStudentItemNew(Academy.cStudentList[i]));
		}
//		cslCount = Academy.cSelfLearnList.Count;
//		for (var i = 0 ; i < cslCount ; i++){
//			CreateSelfLearnItem (Academy.cSelfLearnList[i]);
//		}

	}

	public IEnumerator CreateStudentItemNew(Counselor character){
		var type = character.type;
		AcademyStudentNew obj = Instantiate(Resources.Load("AcademyStudentNew") as GameObject).GetComponent<AcademyStudentNew>();
		yield return new WaitForSeconds(1);
		obj.SetCounselor (character);
		AcademyStudentNew.person.Add (obj);
	}

	public void CreateStudentItem(Counselor character){
		var type = character.type;
		AcademyStudent obj = Instantiate(Resources.Load("AcademyStudentPrefab") as GameObject).GetComponent<AcademyStudent>();
		obj.characterType = character.type;
		obj.characterId = character.id;
		obj.StudentPic =  imageDict[type];
		obj.StudentImageText.text = nameDict[type];
		AcademyStudent.Students.Add (obj);
	}

	public void CreateSelfLearnItem(Counselor character){
		var type = character.type;
		AcademySelfLearn ss = Instantiate(Resources.Load("AcademySelfLearnPrefab") as GameObject).GetComponent<AcademySelfLearn>();
		ss.characterType = character.type;
		ss.characterId = character.id;
		ss.StudentPic =  imageDict[type];
		ss.StudentImageText.text = nameDict[type];
		AcademySelfLearn.Students.Add (ss);
	}
	
	public void SetTeachItems(List<Trainings> tr){
		Game game = Game.Instance;
		Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge ();
		Dictionary<int,string> KnowledgeID = Utilities.SetDict.KnowledgeID ();
		AcademyTeach si = new AcademyTeach();
		int trainerType=0;
		int targetType=0;

		int trainCount = 20;
		for (int i =0; i< trainCount; i++) {
			int trainingType = tr[i].type;
			int trainerId = tr[i].trainerId;
			if (trainerId > 0){
				trainerType = game.counselor.Find (x  => x.id == trainerId).type;
			}
			int targetId = tr[i].targetId;
			if(targetId > 0){
				targetType = game.counselor.Find (x  => x.id == targetId).type;
			}
			Debug.Log("SetTeachItems(): "+ tr[i].trainerId);
//		GameObject sp=null;
			if (i<5) {
				si = AcademyTeach.IQTeach[i];
				if(tr[i].targetId != 0){
					si.KnowledgeText.text = (game.counselor.Find(x => x.id == tr[i].targetId).attributes["attributes"]["IQ"].AsFloat+1).ToString();
				}
				if ((i == 3 || i ==4) && IsLevelNotReach){
					Debug.Log("AcademyTeach.IQTeach B");
					si.TeacherImage.GetComponent<Button>().interactable = false;
					si.StudentImage.GetComponent<Button>().interactable = false;
				}
			} else if (i> 4 && i<10) {
				si = AcademyTeach.CommandedTeach[i-5];
				if(tr[i].targetId != 0){
					si.KnowledgeText.text = (game.counselor.Find(x => x.id == tr[i].targetId).attributes["attributes"]["Leadership"].AsFloat+1).ToString();			
				}
				if ((i == 8 || i ==9) && IsLevelNotReach){
					si.TeacherImage.GetComponent<Button>().interactable = false;
					si.StudentImage.GetComponent<Button>().interactable = false;
				}	
			} else if (i> 9 && i<15) {
				si = AcademyTeach.KnowledgeTeach[i-10];
				if(tr[i].targetId != 0){
					si.KnowledgeText.text = String.Format ("{0}({1})",
					    KnowledgeDict[tr[i].type],
					    (game.counselor.Find(x => x.id == tr[i].targetId).attributes["attributes"]["KnownKnowledge"][KnowledgeID[tr[i].type]].AsInt+1).ToString()
					);
				}
				if ((i == 13 || i ==14) && IsLevelNotReach){
					si.TeacherImage.GetComponent<Button>().interactable = false;
					si.StudentImage.GetComponent<Button>().interactable = false;
				}
			} else if (i> 14 && i<20) {
				si = AcademyTeach.FightingTeach[i-15];
				if ((i == 18 || i ==19) && IsLevelNotReach){
					si.TeacherImage.GetComponent<Button>().interactable = false;
					si.StudentImage.GetComponent<Button>().interactable = false;
				}
			}

			si.trainingObject = game.trainings[i];
			si.etaTimestamp = tr[i].etaTimestamp;
			if (trainerId != 0 && targetId !=0){
				si.TeacherPic = imageDict [trainerType];
			    si.TeacherImageText.text = nameDict [trainerType];
				si.StudentPic = imageDict [targetType];
				si.StudentImageText.text = nameDict [targetType];
				si.trainerId = trainerId;

//				si.TeacherDropZone.enabled = false;
//				si.StudentDropZone.enabled = false;
				si.isDropZoneEnabled = false;

				si.targetId = targetId;
				si.trainingType = tr[i].type;
			}else{
				si.TeacherPic = null;
				si.TeacherImageText.text = "";
				si.StudentPic = null;
				si.StudentImageText.text = "";
				si.isDropZoneEnabled = true;
			}
			if (i<5) {
				AcademyTeach.IQTeach[i] = si;
			} else if (i> 4 && i<10) {
				AcademyTeach.CommandedTeach[i-5] = si;
			} else if (i> 9 && i<15) {
				AcademyTeach.KnowledgeTeach[i-10] = si;
			} else if (i> 14 && i<20) {
				AcademyTeach.FightingTeach[i-15] = si;
			}

		}
	}

	public void SetupAcademyTaughtPanel(){
		for (var i = 0; i < 5; i++) {
			AcademyTeach.IQTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
			AcademyTeach.CommandedTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
			AcademyTeach.KnowledgeTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
			AcademyTeach.FightingTeach.Add (Instantiate(Resources.Load("AcademyTeachPrefab") as GameObject).GetComponent<AcademyTeach>());
		}
	}


	public void SetPanelActive (string panel){
		if (panel == "IQ") {
			AcademyTeach.showPanelItems(AcademyTeach.IQTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.FightingTeach);
		}else if (panel == "Commanded") {
			AcademyTeach.hidePanelItems(AcademyTeach.IQTeach);
			AcademyTeach.showPanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.FightingTeach);
		}else if (panel == "Knowledge") {
			AcademyTeach.hidePanelItems(AcademyTeach.IQTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.showPanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.FightingTeach);
		}else if (panel == "Fighting") {
			AcademyTeach.hidePanelItems(AcademyTeach.IQTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.CommandedTeach);
			AcademyTeach.hidePanelItems(AcademyTeach.KnowledgeTeach);
			AcademyTeach.showPanelItems(AcademyTeach.FightingTeach);
		}
		AcademyStudent.showPanelItems(AcademyStudent.Students);
		SelfStudy.ShowPanelItems (panel);
		AcademySelfLearn.showPanelItems(AcademySelfLearn.Students);
		SetStudentImageText (panel);
	}

	void SetStudentImageText(string panel){
		int count = AcademyStudent.Students.Count;

		List<AcademyStudent> s = AcademyStudent.Students;
		for (var i =0; i < count; i++) {
			 s[i].SetAttributeText(panel);
		}

		List<AcademySelfLearn> sl = AcademySelfLearn.Students;
		for (var i =0; i < count; i++) {
			sl[i].SetAttributeText(panel);
		}
	}


	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
//		if (!firstCalled) {
//			Invoke("CallAcademy",1);
//		}
//		AcademyTeach.setDropZone(AcademyTeach.IQTeach);
//		AcademyTeach.setDropZone(AcademyTeach.CommandedTeach);
//		AcademyTeach.setDropZone(AcademyTeach.KnowledgeTeach);
//		AcademyTeach.setDropZone(AcademyTeach.FightingTeach);


		// re-setup while no student prefab exist since prefabs destroyed while panel close
//		if (gameObject.activeSelf == true && AcademyStudent.Students.Count == 0 && game.counselor.Count != 0){
//
//		}
	}

	public void SetPanelParent(string panelToShow){
		var count = 0;
		if (panelToShow == "IQ") {
			count = AcademyTeach.IQTeach.Count;
			for (var i = 0; i < count; i++){

			}
		}
	}



	void OnButtonClick(Button btn){
//		De
		Debug.Log ("Button in Academy clicked");
		GameObject selfStudyPopup = GameObject.Find ("SelfStudyHolder");
		GameObject teacherPopup = GameObject.Find ("TeachHolder");
//		SelfStudyHolder.SetActive (true);
//		TeachHolder.SetActive (true);
		QAHolder.SetActive (true);
		Academy.activePopup = activePopupName[btn.name];
		SelfStudyInstructionText.text = academyDescription [Academy.activePopup];
		TeachInstructionText.text = academyDescription [Academy.activePopup];
		Debug.Log (Academy.activePopup);
		List<Trainings> tList = game.trainings;
		SetupAcademyTaughtPanel ();
		ShowLog.Log ("Trying Setup Teach Prefabs");
		SetTeachItems (tList);
		SetupStudentPrefabList();

		StartCoroutine (SetAcademyStudentNew (Academy.activePopup));
	}

	IEnumerator SetAcademyStudentNew(ActivePopupEnum panel){
		yield return new WaitForSeconds (1);
		int count = AcademyStudentNew.person.Count;
		for (int i = 0; i < count ; i++){
			AcademyStudentNew.person[i].SetPanel(Academy.activePopup);
		}
	}

	void OnQAButtonClick(Button btn){
		QAHolder.SetActive (false);
		if (btn.name == "SelfStudyButton") {
			SelfStudyHolder.SetActive(true);
		} else {
			TeachHolder.SetActive(true);
		}

		#region SetPanelTitle
		SelfStudyHolder.transform.Find("Popup/Text").gameObject.GetComponent<Text>().text = academyCategoryText[Academy.activePopup];
		TeachHolder.transform.Find("Header").gameObject.GetComponent<Text>().text = academyCategoryText[Academy.activePopup];
		#endregion

		if (Academy.activePopup == ActivePopupEnum.IQPopup) {
			SetPanelActive ("IQ");
			SetTaughtHeaderTargetText ("IQ");
		} else if (Academy.activePopup == ActivePopupEnum.CommandedPopup) {
			SetPanelActive ("Commanded");
			SetTaughtHeaderTargetText ("Commanded");
		} else if (Academy.activePopup == ActivePopupEnum.KnowledgePopup) {
			SetPanelActive ("Knowledge");
			SetTaughtHeaderTargetText ("Knowledge");
		} else if (Academy.activePopup == ActivePopupEnum.FightingPopup) {
			SetPanelActive ("Fighting");
			SetTaughtHeaderTargetText ("Fighting");
		}

	}

	void SetTaughtHeaderTargetText(string header){
		string txt = "";
		if (header == "Knowledge") {
			txt = "學問";
		} else {
			txt = "期望值";
		}
		TeachHolder.transform.GetChild (1).GetChild (0).GetChild (2).GetComponent<Text> ().text = txt;
	}

	void DestroyPrefabObject(){
		var count = AcademyStudent.Students.Count;
		for (int i = 0 ; i < count ; i++){
			GameObject.DestroyImmediate( AcademyStudent.Students[i].gameObject);
		}
		count =AcademySelfLearn.Students.Count;
		for (int i = 0 ; i < count ; i++){
			GameObject.DestroyImmediate( AcademySelfLearn.Students[i].gameObject);
		}
		count = AcademyStudentNew.person.Count;
		for (int i = 0 ; i < count ; i++){
			GameObject.DestroyImmediate( AcademyStudentNew.person[i].gameObject);
		}
		foreach (AcademyTeach go in AcademyTeach.IQTeach) {
			GameObject.DestroyImmediate(go.gameObject);
		}
		foreach (AcademyTeach go in AcademyTeach.CommandedTeach) {
			GameObject.DestroyImmediate(go.gameObject);
		}
		foreach (AcademyTeach go in AcademyTeach.KnowledgeTeach) {
			GameObject.DestroyImmediate(go.gameObject);
		}
		foreach (AcademyTeach go in AcademyTeach.FightingTeach) {
			GameObject.DestroyImmediate(go.gameObject);
		}
		cStudentList.Clear ();
		cSelfLearnList.Clear ();

		AcademyTeach.IQTeach = new List<AcademyTeach>();
		AcademyTeach.CommandedTeach = new List<AcademyTeach>();
		AcademyTeach.KnowledgeTeach = new List<AcademyTeach>();
		AcademyTeach.FightingTeach = new List<AcademyTeach>();
		AcademyStudentNew.person = new List<AcademyStudentNew>();
	}

	void CloseAllPanel(string action){
		if (action == "Back") {
			if (ConfirmTeacherBy.activeSelf || ConfirmTraining.activeSelf || LowerThanTrainer.activeSelf || CounselorHolder.activeSelf) {
				ConfirmTeacherBy.SetActive (false);
				ConfirmTraining.SetActive (false);
				LowerThanTrainer.SetActive (false);
				CounselorHolder.SetActive (false);
			} else {
				SelfStudyHolder.SetActive (false);
				TeachHolder.SetActive (false);
			}
		} else if (action == "Close") {
			ConfirmTeacherBy.SetActive (false);
			ConfirmTraining.SetActive (false);
			LowerThanTrainer.SetActive (false);
			CounselorHolder.SetActive (false);
			SelfStudyHolder.SetActive (false);
			TeachHolder.SetActive (false);
		}
	}

	void AddButtonListener(){
		for (var i = 0; i < 4; i++) {
			Transform child = buttons [i].transform;
			buttons[i].onClick.AddListener(() => { OnButtonClick(child.GetComponent<Button>()); });
		}
		for (var i = 0; i < 2; i++) {
			Transform child = qaButtons [i].transform;
			qaButtons [i].onClick.AddListener (() => {
				OnQAButtonClick (child.GetComponent<Button> ()); });
		}
		
		backButton.onClick.AddListener (() => {
			DestroyPrefabObject();
			AcademyStudent.Students = new List<AcademyStudent>();
			AcademySelfLearn.Students = new List<AcademySelfLearn>();
			Academy.activePopup = ActivePopupEnum.none;
			CloseAllPanel("Back");
			gameObject.SetActive(true);
		});
		closeButton.onClick.AddListener (() => {
			DestroyPrefabObject();
			AcademyStudent.Students = new List<AcademyStudent>();
			AcademySelfLearn.Students = new List<AcademySelfLearn>();

			Academy.activePopup = ActivePopupEnum.none;
			CloseAllPanel("Close");
			AcademyHolder.SetActive(false);
			MainScene.MainUIHolder.SetActive(true);
			Utilities.ShowLog.Log("Unused Assets to be unloaded");
			Resources.UnloadUnusedAssets();
		});
		KnowledgeListHolder.transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (() => {
			KnowledgeListHolder.SetActive(false);
			ResetTeachPanel();
		});

		Utilities.Panel.GetConfirmButton(LowerThanTrainer).onClick.AddListener (() => {
			LowerThanTrainer.SetActive(false);
			Debug.Log ("Closing LowerThanTrainer Panel");

			if (AcademySelfLearn.isSelfStudy){
				AcademySelfLearn.reCreateStudentItem(AcademySelfLearn.currentStudentPrefab.GetComponent<AcademySelfLearn>());
				GameObject.Destroy(AcademySelfLearn.currentStudentPrefab);
				AcademySelfLearn.Students.Remove(AcademySelfLearn.currentStudentPrefab.GetComponent<AcademySelfLearn>());

				AcademySelfLearn.isSelfStudy = false;
				return;
			}
			ResetTeachPanel();
			if (AcademyStudent.IsLevelNotReach){
				AcademyStudent.reCreateStudentItem(AcademyStudent.currentStudentPrefab.GetComponent<AcademyStudent>(),false);
				GameObject.Destroy(AcademyStudent.currentStudentPrefab);
				AcademyStudent.Students.Remove(AcademyStudent.currentStudentPrefab.GetComponent<AcademyStudent>());
				AcademyStudent.IsLevelNotReach = false;
			}
		});

		Utilities.Panel.GetConfirmButton (ConfirmTeacherBy).onClick.AddListener (() => {
			ConfirmTeacherBy.SetActive(false);
			//TODO set training object
//			Trainings obj  = AcademyStudent.currentTeachItem.trainingObject;
			Trainings obj  = AcademyStudentNew.currentTeachItem.trainingObject;
			int index = game.trainings.FindIndex(x => x == obj);
			Debug.Log("Training item index in DB: "+obj.id);
			AcademyStudentNew.currentTeachItem.etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught,0,0);
			game.trainings[index].trainerId = AcademyStudentNew.currentTeachItem.trainerId;
			game.trainings[index].targetId = AcademyStudentNew.currentTeachItem.targetId;
			game.trainings[index].startTimestamp = DateTime.Now;
			game.trainings[index].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught,0,0);
			game.trainings[index].status = (int)TrainingStatus.OnGoing;
			if (index < 5){
				game.trainings[index].type = 1;
				AcademyStudentNew.currentTeachItem.KnowledgeText.text = (game.counselor.Find(x => x.id == AcademyStudentNew.currentTeachItem.targetId).attributes["attributes"]["IQ"].AsInt+1).ToString();
			}else if (index > 4 && index < 10){
				game.trainings[index].type = 2;
				AcademyStudentNew.currentTeachItem.KnowledgeText.text = (game.counselor.Find(x => x.id == AcademyStudentNew.currentTeachItem.targetId).attributes["attributes"]["Leadership"].AsInt+1).ToString();
			}else if (index > 9 && index < 15){
				game.trainings[index].type = KnowledgeOption.knowledge;
				Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
				AcademyStudentNew.currentTeachItem.KnowledgeText.text = String.Format ("{0}({1})",
								KnowledgeDict[KnowledgeOption.knowledge],
				                game.counselor.Find(x => x.id == game.trainings[index].targetId).attributes["attributes"]["KnownKnowledge"][KnowledgeOption.knowledgeName]+1
				); 
			}else if (index > 14 && index < 20){

			}
			Debug.Log (game.trainings[index].toJSON());
			game.trainings[index].UpdateObject();
			AcademyStudentNew.currentTeachItem.LeftTimeText.text = Utilities.TimeUpdate.Time (game.trainings[index].etaTimestamp);
			KnowledgeOption.knowledge = 0;
			KnowledgeOption.knowledgeName ="";
		});

		Utilities.Panel.GetCancelButton (ConfirmTeacherBy).onClick.AddListener (() => {
			ConfirmTeacherBy.SetActive(false);
			AcademyStudentNew.person.Find (x => x.counselor.id == AcademyStudentNew.currentTeachItem.trainerId).gameObject.SetActive(true);
			AcademyStudentNew.person.Find (x => x.counselor.id == AcademyStudentNew.currentTeachItem.targetId).gameObject.SetActive(true);
			ResetTeachPanel();
		});

		Utilities.Panel.GetConfirmButton (ConfirmTraining).onClick.AddListener (() => {
			ConfirmTraining.SetActive(false);
			char[] charToTrim = { '"' };
			Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
			//TODO set training object
			int index =0;
			string title = SelfStudyHolder.transform.GetChild(0).GetChild(0).GetComponent<Text>().text;
			if (title == "智商"){
				index = (Academy.CounselorHolderButton.transform.GetSiblingIndex()/2)+20;
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+20;
			}else
			if (title == "統率"){
				index = (Academy.CounselorHolderButton.transform.GetSiblingIndex()/2)+25;
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+25;
			}else
			if (title == "學問"){
				index = (Academy.CounselorHolderButton.transform.GetSiblingIndex()/2)+30;
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+30;
			}else if (title == "陣法"){
				index = (Academy.CounselorHolderButton.transform.GetSiblingIndex()/2)+35;
//				index = (AcademySelfLearn.currentSelfStudy.transform.GetSiblingIndex()/2)+35;
			}
			Debug.Log("Training item index in DB: "+game.trainings[index].id);
			Debug.Log (index);
			SelfStudy ss = Academy.CounselorHolderButton.GetComponent<SelfStudy>();
			game.trainings[index].trainerId = 0;
			game.trainings[index].targetId = ss.targetId;
//			game.trainings[index].targetId = AcademySelfLearn.currentSelfStudy.targetId;
			game.trainings[index].startTimestamp = DateTime.Now;
			game.trainings[index].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught * 2,0,0);
			game.trainings[index].status = (int)TrainingStatus.OnGoing;
			ss.ImageText.text = nameDict[ss.trainingType];
			if (index < 25){
				game.trainings[index].type = 1;
				ss.ImageText.text += (game.counselor.Find(x => x.id == ss.targetId).attributes["attributes"]["IQ"].AsInt+1).ToString();
			}else if (index > 24 && index < 30){
				game.trainings[index].type = 2;
				ss.ImageText.text += (game.counselor.Find(x => x.id == ss.targetId).attributes["attributes"]["Leadership"].AsInt+1).ToString();
			}else if (index > 29 && index < 35){
				game.trainings[index].type = KnowledgeOption.knowledge;
				Debug.Log (KnowledgeOption.knowledge) ;
				ss.ImageText.text = String.Format("{0}({1})",
				     KnowledgeDict[KnowledgeOption.knowledge],
				     (game.counselor.Find(x => x.id == game.trainings[index].targetId).attributes["attributes"]["IQ"].AsFloat+1).ToString()
				);
			}else if (index > 34 && index < 40){
				
			}
			game.trainings[index].UpdateObject();
			ss.ImageText.text +=  "\n"+Utilities.TimeUpdate.Time ( game.trainings[index].etaTimestamp);
			KnowledgeOption.knowledge = 0;
			KnowledgeOption.knowledgeName ="";
		});

		Utilities.Panel.GetCancelButton (ConfirmTraining).onClick.AddListener (() => {
			ConfirmTraining.SetActive(false);
			AcademyStudentNew.person.Find (x => x.counselor.id == AcademyStudentNew.currentTeachItem.trainerId).gameObject.SetActive(true);
			AcademyStudentNew.person.Find (x => x.counselor.id == AcademyStudentNew.currentTeachItem.targetId).gameObject.SetActive(true);
			ResetSelfStudyItem();
		});
	}

	void ResetTeachPanel(){
//		AcademyStudentNew.reCreateStudentItem(AcademyStudent.currentTeachItem);
		AcademyStudentNew.currentTeachItem.TeacherImage.sprite = null;
		AcademyStudentNew.currentTeachItem.StudentImage.sprite = null;
		AcademyStudentNew.currentTeachItem.TeacherImageText.text = "";
		AcademyStudentNew.currentTeachItem.StudentImageText.text = "";
		AcademyStudentNew.currentTeachItem.KnowledgeText.text = "";
		AcademyStudentNew.currentTeachItem.LeftTimeText.text = "00:00:00";
		AcademyStudentNew.currentTeachItem.trainerId=0;
		AcademyStudentNew.currentTeachItem.targetId=0;
		AcademyStudentNew.currentTeachItem.trainingType=0;
		AcademyStudentNew.currentTeachItem.targetType = 0;
	}

	void ResetSelfStudyItem(){
		AcademySelfLearn.reCreateStudentItem (AcademySelfLearn.currentSelfStudy);
		AcademySelfLearn.currentSelfStudy.image.sprite = null;
		AcademySelfLearn.currentSelfStudy.ImageText.text = "";
		AcademySelfLearn.currentSelfStudy.targetId=0;
		AcademySelfLearn.currentSelfStudy.trainingType=0;
//		AcademySelfLearn.currentSelfStudy.targetType = 0;
		AcademySelfLearn.currentSelfStudy.isDropZoneEnabled = true; 
		AcademySelfLearn.isSelfStudy = false;
	}


	void SetDictionary(){
		activePopupName.Add (btnName [0], ActivePopupEnum.IQPopup);
		activePopupName.Add (btnName [1], ActivePopupEnum.CommandedPopup);
		activePopupName.Add (btnName [2], ActivePopupEnum.KnowledgePopup);
		activePopupName.Add (btnName [3], ActivePopupEnum.FightingPopup);
		academyCategoryText.Add (ActivePopupEnum.IQPopup, "智商");
		academyCategoryText.Add (ActivePopupEnum.CommandedPopup, "統率");
		academyCategoryText.Add (ActivePopupEnum.KnowledgePopup, "學問");
		academyCategoryText.Add (ActivePopupEnum.FightingPopup, "陣法");
		academyDescription.Add(ActivePopupEnum.IQPopup, "智商\n\n正常人的智商介乎85~115之間，能夠率領百萬雄獅，智商當然並非一般常人可比，努力訓練謀士，率領百萬雄獅征戰天下。\n\n自修：於左方點選訓練空間，再選擇欲訓練的謀士即可。\n\n師承：於左方的訓練空間先選師傅，選擇謀士作為師傅，再選擇徒弟，並選擇謀士作為徒弟即可，徒弟會跟隨師傅進行特訓，特訓完成後，徒弟的智商即與師傅相同。");
		academyDescription.Add(ActivePopupEnum.CommandedPopup, "統率\n\n統率，即是可以率領的士兵數目，戰場上，多一根矛便是多一根矛，殺對手一個片甲不留。\n\n自修：於左方點選訓練空間，再選擇欲訓練的謀士即可。\n\n師承：於左方的訓練空間先選師傅，選擇謀士作為師傅，再選擇徒弟，並選擇謀士作為徒弟即可，徒弟會跟隨師傅進行特訓，特訓完成後，徒弟的統率即與師傅相同。");
		academyDescription.Add(ActivePopupEnum.KnowledgePopup, "學問\n\n除了智商、統率之外，學問亦是非常重要。學問可以鍛造各種更好的裝備。工欲善其事，必先利其器，努力學習吧。\n\n自修：於左方點選訓練空間，再選擇欲訓練的謀士即可。\n\n師承：於左方的訓練空間先選師傅，選擇謀士作為師傅，再選擇徒弟，並選擇謀士作為徒弟即可，徒弟會跟隨師傅進行特訓，特訓完成後，徒弟的學問即與師傅相同。");
		academyDescription.Add(ActivePopupEnum.FightingPopup,"陣法\n\n陣法可以令對方陷入水深火熱之中，亦可以令己方立於不敗之地，善同陣法，可獲長勝。\n\n自修：於左方點選訓練空間，再選擇欲訓練的謀士即可。\n\n師承：於左方的訓練空間先選師傅，選擇謀士作為師傅，再選擇徒弟，並選擇謀士作為徒弟即可，徒弟會跟隨師傅進行特訓，特訓完成後，徒弟的陣法即與師傅相同。");


	}




	public void SetCharacters(){
		LoadHeadPic headPic = LoadHeadPic.Instance;
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
	}
}


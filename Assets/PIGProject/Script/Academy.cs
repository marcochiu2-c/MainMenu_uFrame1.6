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



public enum LearningItemEnum{
	none,IQ,Commanded,Knowledge,Fighting
}

public enum LearningModeEnum{
	none,SelfStudy,Teach
}

public class Academy : MonoBehaviour
{


	class ItemData
	{
		public float SliderValue;
		public string ImageKey;
	}

	public static Button[] buttons = new Button[4];
	public static Button[] qaButtons = new Button[2];
	public static Button[] CounselorSelectorButtons = new Button[5];
	public static Button closeButton;
	public static Button backButton;
	public static GameObject SelfStudyHolder;
	public static GameObject TeachHolder;
	public static GameObject KnowledgeListHolder;
	public static GameObject ConfirmTeacherBy;
	public static GameObject ConfirmTraining;
	public static GameObject CounselorHolder;
	public static GameObject LowerThanTrainer;

	public static Dictionary<int,Sprite> imageDict = new Dictionary<int,Sprite>();
	public static Dictionary<int,string> nameDict = new Dictionary<int,string> ();

	public static GameObject DisablePanel;
	public static List<Counselor> CounselorList;
	bool firstCalled = false;
	public static string CounselorHolderFunction="";
	public static Button CounselorHolderButton;
	public int TrainingTimeTaught = 10;  // in hours
	public int TrainingCoolDownPeriod = 24; // in hours, cannot train again in the same slot for given hours.
	public static bool IsLevelNotReach=true;

//	Text SelfStudyInstructionText;
//	Text TeachInstructionText;
	WsClient wsc;
	Game game;

	Dictionary<LearningItemEnum,string> academyDescription = new Dictionary<LearningItemEnum, string>();
	private const int columnWidthCount = 5;
	List<TechTreeObject> techTreeList = new List<TechTreeObject> ();
	int numberOfTech = 0;
	public static int AssigningCounselorSlot = -1;
	public static Counselor[] AssigningCounselor = new Counselor[5];
	public static Counselor AssigningTeacher = null;
	public static LearningItemEnum AssigningLearning = LearningItemEnum.none;
	public static LearningModeEnum AssigningLearningMode = LearningModeEnum.none;
	public static Knowledge AssigningKnowledge = Knowledge.none;
	public static Sprite DefaultCounselorSelectorSprite;

	// Use this for initialization
	void Start(){
		SetDictionary ();
		SetupCounselorPrefabList ();
		AddButtonListener ();
	}


	void OnEnable(){
		DefaultCounselorSelectorSprite = transform.GetChild (1).GetChild (0).GetChild (0).GetChild (4).GetComponent<Image>().sprite;
		CallAcademy ();
		InvokeRepeating ("UpdateTrainingTime", 1, 1);
	}

	void OnDisable(){
		CancelInvoke ();
	}

	void UpdateTrainingTime(){
		for (int i = 0; i < 5; i++) {
			CounselorSelectorButtons [i].transform.GetChild (3).GetComponent<Text> ().text = (game.trainings[i].etaTimestamp < DateTime.Now) ?
				"" : TimeUpdate.Time(game.trainings[i].etaTimestamp) ;
			if (game.trainings[i].status == (int)TrainingStatus.OnGoing && game.trainings[i].etaTimestamp < DateTime.Now){
				Academy.SetCounselorSelectorButtonOnComplete(i);
			}
		}
	}

	public void CallAcademy(){
		wsc = WsClient.Instance;
		game = Game.Instance;

		SetCharacters ();

		AssignGameObjectVariable ();

		KnowledgeOption.AssignButton (KnowledgeListHolder);
		KnowledgeOption.AddButtonListener (KnowledgeListHolder,gameObject);

		CounselorList = new List<Counselor> ();
		techTreeList = TechTreeObject.GetList (1);
		numberOfTech = techTreeList.Count;

		ActivateAllCounselorPrefab ();
		HideTrainingCounselorPrefab ();
		EnableAllCounselorSelectorButtons ();
		
		AcademyScreenView.AcademyType.transform.Find ("TextDesc/Text").GetComponent<Text> ().text = "";
	}
	
	public void AssignGameObjectVariable(){
		Transform panel = transform.GetChild (1);
		DisablePanel = AcademyScreenView.DisablePanel;


		KnowledgeListHolder = panel.Find ("KnowledgeListHolder").gameObject;
		ConfirmTeacherBy = panel.Find ("ConfirmTeachBy").gameObject;
		ConfirmTraining = panel.Find ("ConfirmTraining").gameObject;
		CounselorHolder = panel.Find ("CounselorsHolder").gameObject;
		LowerThanTrainer = panel.Find ("LowerThanTrainer").gameObject;

		closeButton = transform.Find ("CloseButton").GetComponent<Button>();
		backButton = transform.Find ("BackButton").GetComponent<Button>();

		buttons [0] = AcademyScreenView.IQButton;
		buttons [1] = AcademyScreenView.CommandedButton;
		buttons [2] = AcademyScreenView.KnowledgeButton;
		buttons [3] = AcademyScreenView.FightingButton;

		qaButtons [0] = AcademyScreenView.SelfStudyButton;
		qaButtons [1] = AcademyScreenView.TeachButton;

		Transform CounselorSelector = panel.GetChild (0);
		for (int i = 0; i < 5; i++) {
			CounselorSelectorButtons [i] = CounselorSelector.GetChild (0).GetChild (i).GetComponent<Button>();
		}
		SetupCounselorSelectorButtons ();

	}

	public void SetupCounselorSelectorButtons(){
		Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
		for (int i = 0; i < 5; i++) {
			AssigningCounselorSlot = i;
			if (game.trainings[i].etaTimestamp > DateTime.Now){
				AssigningCounselor[i] = game.counselor.Find (x => x.id == game.trainings[i].targetId);
				string klge="";
				int klgeLevel=0;
				if (game.trainings[i].type ==1 ){
					klge = "智商";
					klgeLevel = AssigningCounselor[i].attributes["attributes"]["IQ"].AsInt;
				}else if (game.trainings[i].type ==2 ){
					klge = "統率";
					klgeLevel = AssigningCounselor[i].attributes["attributes"]["Leadership"].AsInt;
				}else if (game.trainings[i].type >2000 && game.trainings[i].type <2100 ){
					klge = KnowledgeDict[game.trainings[i].type];
					klgeLevel = AssigningCounselor[i].attributes["attributes"]["KnownKnowledge"][Enum.GetName(typeof(Knowledge),(Knowledge)game.trainings[i].type)].AsInt;
				}
				SetCounselorSelectorButton(nameDict[AssigningCounselor[i].type],
				                           (game.trainings[i].etaTimestamp-DateTime.Now),
				                           klge,
				                           klgeLevel+1
				                           );

			}else{
				AssigningCounselor[i] = null;
				SetCounselorSelectorButton("",TimeSpan.Zero);
			}
			AssigningCounselorSlot = -1;
		}
	}

	public void SetupCounselorPrefabList(){
		for (int j = 0 ; j < game.counselor.Count; j++){
			CounselorList.Add (new Counselor(game.counselor[j]));
		}

		int cslCount = Academy.CounselorList.Count;
		Debug.Log ("Count of Students: "+cslCount);
		for (var i = 0 ; i < cslCount ; i++){
			StartCoroutine( CreateStudentItem(Academy.CounselorList[i]));
		}
		ActivateAllCounselorPrefab ();
		HideTrainingCounselorPrefab ();
		EnableAllCounselorSelectorButtons ();
	}

	void ActivateAllCounselorPrefab(){
		int count = AcademyStudent.person.Count;
		for (var i = 0; i < count; i++) {
			AcademyStudent.person[i].gameObject.SetActive(true);
		}
	}

	/// <summary>
	/// Hides the training prefab for trainier/trainee counselors.
	/// </summary>
	void HideTrainingCounselorPrefab(){
		List<Trainings> tList = game.trainings;
		int count = AcademyStudent.person.Count;

		AcademyStudent asp = null;
		for (int i = 0; i < 5; i++) {
			asp = AcademyStudent.person.Find (x => x.counselor.id == tList[i].trainerId);
			if (asp != null) {
				asp.gameObject.SetActive(false);
			}
			asp = AcademyStudent.person.Find (x => x.counselor.id == tList[i].targetId);
			if (asp != null) {
				asp.gameObject.SetActive(false);
			}
		}
	}

	public IEnumerator CreateStudentItem(Counselor character){
		var type = character.type;
		AcademyStudent obj = Instantiate(Resources.Load("AcademyStudent") as GameObject).GetComponent<AcademyStudent>();
		yield return new WaitForSeconds(1);
		obj.SetCounselor (character);
		AcademyStudent.person.Add (obj);
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnCounselorSelectorButtonClick (int btn){
		if (game.trainings [btn].etaTimestamp < DateTime.Now) {  // Last training of this slot has ended or never started
			ActivateAllCounselorPrefab ();
			HideTrainingCounselorPrefab ();
			CounselorHolderFunction = "";
			AssigningCounselorSlot = btn;
			CounselorHolder.SetActive (true);
			AcademyStudent s;
			if (AssigningCounselor [Academy.AssigningCounselorSlot] != null) {
				s = AcademyStudent.person.Find (x => x.counselor.id == AssigningCounselor [Academy.AssigningCounselorSlot].id);
			} else {
				s = null;
			}
			if (s != null) {
				AssigningCounselor [Academy.AssigningCounselorSlot] = null;
				s.gameObject.SetActive (true);
			}
		} else {
			//TODO speed up training or cancel
		}
	}

	public static void OnCounselorSelected ()
	{
		for (int i = 0 ; i < 5; i++){
			if (AssigningCounselor[i] == null){
				CounselorSelectorButtons[i].interactable = false;
			}
		}
		CounselorSelectorButtons[AssigningCounselorSlot].interactable = true;
		for (int i = 0 ; i < 4; i++){
			buttons[i].interactable = true;
		}
	}

	void EnableAllCounselorSelectorButtons(){
		for (int i = 0 ; i < 5; i++){
			CounselorSelectorButtons[i].interactable = true;
		}
	}

	void OnLearningButtonClick(LearningItemEnum btn){
		AssigningLearning = btn;
		AssigningKnowledge = Knowledge.none;
		qaButtons [0].interactable = true;
		qaButtons [1].interactable = true;
		if (btn == LearningItemEnum.Knowledge) {
			ShowPanel(KnowledgeListHolder);
		}
		AcademyScreenView.AcademyType.transform.Find ("TextDesc/Text").GetComponent<Text> ().text = academyDescription [btn];
	}


	void OnQAButtonClick(LearningModeEnum btn){
		string klge = Enum.GetName(typeof(Knowledge),AssigningKnowledge);
		int level = AssigningCounselor [AssigningCounselorSlot].attributes ["attributes"] ["KnownKnowledge"] [klge].AsInt;
		if (btn == LearningModeEnum.SelfStudy) {
			ShowPanel(ConfirmTraining);

		} else if (btn == LearningModeEnum.Teach) {
			ShowLog.Log("Teach Pressed");
			HideItemsForTeach();
			CounselorHolderFunction = "ChoosingTeacher";
			CounselorHolder.SetActive(true);
		} else {
			return;
		}

	}

	void HideItemsForTeach(){
		int count = AcademyStudent.person.Count;
		ActivateAllCounselorPrefab();
		HideTrainingCounselorPrefab();
		if (AssigningLearning == LearningItemEnum.IQ) {
			for (int i = 0; i < count ; i++){
				if (AcademyStudent.person[i].counselor.attributes["attributes"]["IQ"].AsInt <= AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["IQ"].AsInt){
					AcademyStudent.person[i].gameObject.SetActive(false);
				}
			}
		}else if (AssigningLearning == LearningItemEnum.Commanded) {
			for (int i = 0; i < count ; i++){
				if (AcademyStudent.person[i].counselor.attributes["attributes"]["Leadership"].AsInt <= AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["Leadership"].AsInt){
					AcademyStudent.person[i].gameObject.SetActive(false);
				}
			}
		}else if (AssigningLearning == LearningItemEnum.Knowledge) {
			string klge = Enum.GetName(typeof(Knowledge),AssigningKnowledge);
			for (int i = 0; i < count ; i++){
				if (AcademyStudent.person[i].counselor.attributes["attributes"]["KnownKnowledge"][klge].AsInt <= AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["KnownKnowledge"][klge].AsInt){
					AcademyStudent.person[i].gameObject.SetActive(false);
				}
			}
		}else if (AssigningLearning == LearningItemEnum.Fighting) {
			return;
		}
	}

	public static void SetCounselorSelectorButton(string name, TimeSpan ts, string item="",int level=0){
		Text nameText = CounselorSelectorButtons[AssigningCounselorSlot].transform.GetChild(0).GetComponent<Text>();
		Text itemText = CounselorSelectorButtons [AssigningCounselorSlot].transform.GetChild (1).GetComponent<Text> ();
		Text levelText = CounselorSelectorButtons [AssigningCounselorSlot].transform.GetChild (2).GetComponent<Text> ();
		Text timeLeftText = CounselorSelectorButtons [AssigningCounselorSlot].transform.GetChild (3).GetComponent<Text> ();
		nameText.text = name;
		itemText.text = item;
		levelText.text = (level > 0) ? level.ToString () : "";
		timeLeftText.text = (ts > TimeSpan.Zero) ? TimeUpdate.Time (ts) : "";
		if (name == "") {
			CounselorSelectorButtons [AssigningCounselorSlot].GetComponent<Image> ().sprite = DefaultCounselorSelectorSprite;
		} else {
			CounselorSelectorButtons [AssigningCounselorSlot].GetComponent<Image> ().sprite = imageDict [AssigningCounselor [AssigningCounselorSlot].type];
		}
	}

	public static void SetCounselorSelectorButtonOnComplete(int slot){
		Text nameText = CounselorSelectorButtons[slot].transform.GetChild(0).GetComponent<Text>();
		Text itemText = CounselorSelectorButtons [slot].transform.GetChild (1).GetComponent<Text> ();
		Text levelText = CounselorSelectorButtons [slot].transform.GetChild (2).GetComponent<Text> ();
		Text timeLeftText = CounselorSelectorButtons [slot].transform.GetChild (3).GetComponent<Text> ();
		nameText.text = "";
		itemText.text = "";
		levelText.text =  "";
		timeLeftText.text =  "";
		CounselorSelectorButtons [slot].GetComponent<Image> ().sprite = DefaultCounselorSelectorSprite;

	}
	
	void CloseAllPanel(string action){
		if (action == "Back") {
			if (ConfirmTeacherBy.activeSelf || ConfirmTraining.activeSelf || LowerThanTrainer.activeSelf) {
				ConfirmTeacherBy.SetActive (false);
				ConfirmTraining.SetActive (false);
				LowerThanTrainer.SetActive (false);
			}else if (CounselorHolder.activeSelf){
				CounselorHolder.SetActive (false);
				if (game.trainings[AssigningCounselorSlot].etaTimestamp < DateTime.Now){
					ResetSettings();
				}
			}
			DisablePanel.SetActive(false);
		} else if (action == "Close") {
			DisablePanel.SetActive(false);
			ConfirmTeacherBy.SetActive (false);
			ConfirmTraining.SetActive (false);
			LowerThanTrainer.SetActive (false);
			CounselorHolder.SetActive (false);
		}
	}

	void ResetSettings(bool isClearButton=true){
		AssigningCounselor [AssigningCounselorSlot] = null;
		AssigningKnowledge = Knowledge.none;
		AssigningLearning = LearningItemEnum.none;
		AssigningLearningMode = LearningModeEnum.none;
		AssigningTeacher = null;

		for (int i = 0; i < 4; i++) { 
			buttons[i].interactable = false;
		}
		EnableAllCounselorSelectorButtons ();
		qaButtons [0].interactable = false;
		qaButtons [1].interactable = false;
		if (isClearButton) {
			SetCounselorSelectorButton ("", TimeSpan.Zero);
		}
		AssigningCounselorSlot = -1;

	}

	void AddButtonListener(){
		for (var i = 0; i < 4; i++) {
			Transform tr = buttons [i].transform;
			buttons[i].onClick.AddListener(() => { OnLearningButtonClick((LearningItemEnum)(tr.GetSiblingIndex()+1));});
		}
		for (var i = 0; i < 2; i++) {
			Transform tr = qaButtons [i].transform;
			qaButtons [i].onClick.AddListener (() => {
				OnQAButtonClick((LearningModeEnum)(tr.GetSiblingIndex()+1));
			});
		}

		for (var i = 0; i < 5; i++) {
			Transform tr = CounselorSelectorButtons [i].transform;
			CounselorSelectorButtons[i].onClick.AddListener(() => { OnCounselorSelectorButtonClick(tr.GetSiblingIndex());});
		}
		
		backButton.onClick.AddListener (() => {
			AssigningLearning = LearningItemEnum.none;
			CloseAllPanel("Back");
			gameObject.SetActive(true);

		});
		closeButton.onClick.AddListener (() => {
			AssigningLearning = LearningItemEnum.none;
			CloseAllPanel("Close");
			gameObject.SetActive(false);
			MainScene.MainUIHolder.SetActive(true);
		});
		KnowledgeListHolder.transform.GetChild (3).GetComponent<Button> ().onClick.AddListener (() => {
			KnowledgeListHolder.SetActive(false);
		});

		Utilities.Panel.GetConfirmButton (ConfirmTeacherBy).onClick.AddListener (() => {
			HidePanel(ConfirmTeacherBy);
			Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();
			int index = AssigningCounselorSlot;
			Debug.Log("Training item index in DB: "+game.trainings[AssigningCounselorSlot].id);
			game.trainings[index].trainerId = AssigningTeacher.id;
			game.trainings[index].targetId = AssigningCounselor[AssigningCounselorSlot].id;
			game.trainings[index].startTimestamp = DateTime.Now;
			game.trainings[index].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught,0,0);
			game.trainings[index].status = (int)TrainingStatus.OnGoing;
			if(AssigningLearning==LearningItemEnum.IQ){
				SetCounselorSelectorButton(nameDict[AssigningCounselor[AssigningCounselorSlot].type],
				                           new TimeSpan(TrainingTimeTaught,0,0),
				                           "智商",
				                           (AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["IQ"].AsInt+1)
				                           );
				game.trainings[AssigningCounselorSlot].type = 1;
			}if(AssigningLearning==LearningItemEnum.Commanded){
				SetCounselorSelectorButton(nameDict[AssigningCounselor[AssigningCounselorSlot].type],
				                           new TimeSpan(TrainingTimeTaught,0,0),
				                           "統率",
				                           (AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["Leadership"].AsInt+1)
				                           );
				game.trainings[AssigningCounselorSlot].type = 2;
			}else if (AssigningLearning==LearningItemEnum.Knowledge){
				string klge = Enum.GetName(typeof(Knowledge),AssigningKnowledge);
				SetCounselorSelectorButton(nameDict[AssigningCounselor[AssigningCounselorSlot].type],
				                           new TimeSpan(TrainingTimeTaught,0,0),
				                           KnowledgeDict[KnowledgeOption.knowledge],
				                           (AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["KnownKnowledge"][klge].AsInt+1)
				                           );
				game.trainings[AssigningCounselorSlot].type = (int)AssigningKnowledge;
			}else if (AssigningLearning==LearningItemEnum.Fighting){
				
			}
			Debug.Log (game.trainings[index].toJSON());
			game.trainings[index].UpdateObject();
			KnowledgeOption.knowledge = 0;
			KnowledgeOption.knowledgeName ="";
			ResetSettings(false);
		});

		Utilities.Panel.GetCancelButton (ConfirmTeacherBy).onClick.AddListener (() => {
			HidePanel(ConfirmTeacherBy);
			ResetSettings();
		});

		Utilities.Panel.GetConfirmButton (ConfirmTraining).onClick.AddListener (() => {
			HidePanel(ConfirmTraining);
			char[] charToTrim = { '"' };
			Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge();

			Debug.Log("Training item index in DB: "+game.trainings[AssigningCounselorSlot].id);
			game.trainings[AssigningCounselorSlot].trainerId = 0;
			game.trainings[AssigningCounselorSlot].targetId = AssigningCounselor[AssigningCounselorSlot].id;

			game.trainings[AssigningCounselorSlot].startTimestamp = DateTime.Now;
			game.trainings[AssigningCounselorSlot].etaTimestamp = DateTime.Now+new TimeSpan(TrainingTimeTaught * 2,0,0);
			game.trainings[AssigningCounselorSlot].status = (int)TrainingStatus.OnGoing;
			if(AssigningLearning==LearningItemEnum.IQ){
				SetCounselorSelectorButton(nameDict[AssigningCounselor[AssigningCounselorSlot].type],
				                           new TimeSpan(TrainingTimeTaught * 2,0,0),
				                           "智商",
				                           (AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["IQ"].AsInt+1)
					);
				game.trainings[AssigningCounselorSlot].type = 1;
			}if(AssigningLearning==LearningItemEnum.Commanded){
				SetCounselorSelectorButton(nameDict[AssigningCounselor[AssigningCounselorSlot].type],
				                           new TimeSpan(TrainingTimeTaught * 2,0,0),
				                           "統率",
				                           (AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["Leadership"].AsInt+1)
				                           );
				game.trainings[AssigningCounselorSlot].type = 2;
			}else if (AssigningLearning==LearningItemEnum.Knowledge){
				string klge = Enum.GetName(typeof(Knowledge),AssigningKnowledge);
				SetCounselorSelectorButton(nameDict[AssigningCounselor[AssigningCounselorSlot].type],
				                           new TimeSpan(TrainingTimeTaught * 2,0,0),
				                           KnowledgeDict[KnowledgeOption.knowledge],
				                           (AssigningCounselor[AssigningCounselorSlot].attributes["attributes"]["KnownKnowledge"][klge].AsInt+1)
				                           );
				game.trainings[AssigningCounselorSlot].type = (int)AssigningKnowledge;
			}else if (AssigningLearning==LearningItemEnum.Fighting){

			}
			Debug.Log(game.trainings[AssigningCounselorSlot].ToString());
			Debug.Log (game.trainings[AssigningCounselorSlot].toJSON());
			game.trainings[AssigningCounselorSlot].UpdateObject();
			KnowledgeOption.knowledge = 0;
			KnowledgeOption.knowledgeName ="";
			ResetSettings(false);
		});
		Utilities.Panel.GetCancelButton (ConfirmTraining).onClick.AddListener (() => {
			HidePanel(ConfirmTraining);
			ResetSettings();
		});
	}

	void SetDictionary(){
		academyDescription.Add(LearningItemEnum.IQ, "智商－正常人的智商介乎85~115之間，能夠率領百萬雄獅，智商當然並非一般常人可比，努力訓練謀士，率領百萬雄獅征戰天下。\n自修：於上方點選訓練空間，再選擇欲訓練的謀士即可。\n師承：於上方的訓練空間先選欲訓練的謀士，再選擇謀士作為師傅即可。");
		academyDescription.Add(LearningItemEnum.Commanded, "統率－統率，即是可以率領的士兵數目，戰場上，多一根矛便是多一根矛，殺對手一個片甲不留。\n自修：於上方點選訓練空間，再選擇欲訓練的謀士即可。\n師承：於上方的訓練空間先選欲訓練的謀士，再選擇謀士作為師傅即可。");
		academyDescription.Add(LearningItemEnum.Knowledge, "學問－除了智商、統率之外，學問亦是非常重要。學問可以鍛造各種更好的裝備。工欲善其事，必先利其器，努力學習吧。\n自修：於上方點選訓練空間，再選擇欲訓練的謀士即可。\n師承：於上方的訓練空間先選欲訓練的謀士，再選擇謀士作為師傅即可 。");
		academyDescription.Add(LearningItemEnum.Fighting,"陣法－陣法可以令對方陷入水深火熱之中，亦可以令己方立於不敗之地，善同陣法，可獲長勝。\n自修：於上方點選訓練空間，再選擇欲訓練的謀士即可。\n師承：於上方的訓練空間先選欲訓練的謀士，再選擇謀士作為師傅即可。");
	}

	public static void ShowPanel(GameObject panel){
		DisablePanel.SetActive (true);
		Debug.Log ("ShowPanel");
		panel.SetActive (true);
	}
	
	public static void HidePanel(GameObject panel){
		DisablePanel.SetActive (false);
		Debug.Log ("HidePanel");
		panel.SetActive (false);
	}
	
	public static void ChangePanel(GameObject panel1, GameObject panel2){
		Debug.Log (panel1 + " panel is change to " + panel2);
		panel1.SetActive (false);
		panel2.SetActive (true);
	}


	public void SetCharacters(){
		LoadHeadPic headPic = LoadHeadPic.Instance;
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
	}
}


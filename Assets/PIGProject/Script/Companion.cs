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
	static Button CloseButton;
	static Button BackButton;

	static Transform GeneralsHolder;
	static Transform CounselorsHolder;
	static Button CounselorsButton;
	static Button GeneralsButton;
	public static GameObject CardHolder;

	Dictionary<int,Sprite> imageDict;
	Dictionary<int,string> nameDict;

	public static List<General> cGeneralList;
	public static List<Counselor> cCounselorList;

	Transform CounselorScrollPanel;
	Transform GeneralScrollPanel;

	WsClient wsc;
	Game game;

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

		AssignGameObjectVariable ();

		SetupPrefabList ();
		AddButtonListener ();

		//Get the default Self Study Sprite
//		SelfStudy.defaultSprite = SelfStudyHolder.transform.GetChild (0).GetChild (1).GetChild (0).GetChild (0).GetComponent<Image> ().sprite;

	}

	public void AssignGameObjectVariable(){
		CounselorsHolder = CompanionScreenView.CounselorsHolder.transform;
		GeneralsHolder = CompanionScreenView.GeneralsHolder.transform;
		CardHolder = CompanionScreenView.CardHolder;
		
		CounselorsButton = CompanionScreenView.counselorsButton;
		GeneralsButton = CompanionScreenView.soldiersButton;
		BackButton = CompanionScreenView.backButton;
		CloseButton = CompanionScreenView.closeButton;
		CounselorScrollPanel = CounselorsHolder.GetChild (1).GetChild (1).GetChild (0);
		GeneralScrollPanel = GeneralsHolder.GetChild (1).GetChild (1).GetChild (0);
	}

	public void AddButtonListener(){
		CloseButton.onClick.AddListener (() => {
			foreach (CompanionPrefab co in CompanionPrefab.person) {
				GameObject.DestroyImmediate(co.gameObject);
			}
			CompanionPrefab.person.Clear();
			CounselorsHolder.gameObject.SetActive(true);
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

}


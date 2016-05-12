using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Utilities;

public class AcademyTeach : MonoBehaviour {
	public Image TeacherImage;
	public Image StudentImage;
	public Text TeacherImageText;
	public Text StudentImageText;
	public Text KnowledgeText;
	public Text LeftTimeText;

	public Sprite TeacherPic { get { return TeacherImage.sprite; } set { TeacherImage.sprite = value; } }
	public Sprite StudentPic { get { return StudentImage.sprite; } set { StudentImage.sprite = value; } }

	public DateTime etaTimestamp;

	public static List<AcademyTeach> IQTeach = new List<AcademyTeach>();
	public static List<AcademyTeach> CommandedTeach = new List<AcademyTeach>();
	public static List<AcademyTeach> KnowledgeTeach = new List<AcademyTeach>();
	public static List<AcademyTeach> FightingTeach = new List<AcademyTeach>();
	public int trainerId=0;
	public int targetId=0;
	public int trainingType=0;
	public int targetType = 0;
	public Trainings trainingObject; 
	
	public static Transform commonPanel;

	public DropZone TeacherDropZone;
	public DropZone StudentDropZone;
	public bool isDropZoneEnabled = true; 
	public static int currentIQIndex=0;
	public static int currentCommandedIndex=0;
	public static int currentKnowledgeIndex=0;
	public static int currentFightingIndex=0;
	// Use this for initialization
	void Start () {
		TeacherDropZone = TeacherImage.GetComponent ("DropZone") as DropZone;
		StudentDropZone = StudentImage.GetComponent ("DropZone") as DropZone;
		InvokeRepeating ("OnTrainingCompleted", 0, 1);
		AddButtonListener ();
	}

	void OnTrainingCompleted(){
//		Debug.Log ("OnTrainingCompleted()");
		Game game = Game.Instance;
		if (etaTimestamp < DateTime.Now && trainingObject.status == 1){
			if (trainingObject.id==1){
				Debug.Log ("OnTrainingCompleted()");
				Debug.Log (etaTimestamp);
			}
			TeacherPic = null;
			StudentPic = null;
			KnowledgeText.text = "";
			TeacherImageText.text = "";
			StudentImageText.text = "";
			LeftTimeText.text = "00:00:00";
			int index = game.trainings.FindIndex (x => x.trainerId == trainerId);
			isDropZoneEnabled = true;

//				if (trainingObject.trainerId == 0){
//					return;
//				}
//				Counselor co = game.counselor.Find (x=>x.id == trainingObject.targetId);
//				if (index < 5){ 
//					co.attributes["attributes"]["IQ"].AsFloat = co.attributes["attributes"]["IQ"].AsFloat + 1;
//				}else if (index > 4 && index < 10){
//					co.attributes["attributes"]["Leadership"].AsFloat =co.attributes["attributes"]["IQ"].AsFloat + 1;
//				}else if (index > 9 && index < 15){
//					switch (trainingObject.type){
//					case 2001:
//						co.attributes["attributes"]["KnownKnowledge"]["Woodworker"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Woodworker"].AsInt + 1;
//						break;
//					case 2002:
//						co.attributes["attributes"]["KnownKnowledge"]["MetalFabrication"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["MetalFabrication"].AsInt + 1;
//					break;
//					case 2003:
//						co.attributes["attributes"]["KnownKnowledge"]["ChainSteel"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["ChainSteel"].AsInt + 1;
//					break;					
//					case 2004:
//						co.attributes["attributes"]["KnownKnowledge"]["MetalProcessing"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["MetalProcessing"].AsInt + 1;
//					break;					
//					case 2005:
//						co.attributes["attributes"]["KnownKnowledge"]["Crafts"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Crafts"].AsInt + 1;
//					break;					
//					case 2006:
//						co.attributes["attributes"]["KnownKnowledge"]["Geometry"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Geometry"].AsInt + 1;
//						break;					
//					case 2007:
//						co.attributes["attributes"]["KnownKnowledge"]["Physics"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Physics"].AsInt + 1;
//						break;					
//					case 2008:
//						co.attributes["attributes"]["KnownKnowledge"]["Chemistry"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Chemistry"].AsInt + 1;
//						break;					
//					case 2009:
//						co.attributes["attributes"]["KnownKnowledge"]["PeriodicTable"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["PeriodicTable"].AsInt + 1;
//						break;					
//					case 2010:
//						co.attributes["attributes"]["KnownKnowledge"]["Pulley"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Pulley"].AsInt + 1;
//						break;					
//					case 2011:
//						co.attributes["attributes"]["KnownKnowledge"]["Anatomy"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Anatomy"].AsInt + 1;
//						break;					
//					case 2012:
//						co.attributes["attributes"]["KnownKnowledge"]["Catapult"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Catapult"].AsInt + 1;
//						break;					
//					case 2013:
//						co.attributes["attributes"]["KnownKnowledge"]["GunpowderModulation"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["GunpowderModulation"].AsInt + 1;
//						break;					
//					case 2014:
//						co.attributes["attributes"]["KnownKnowledge"]["Psychology"].AsInt = co.attributes["attributes"]["KnownKnowledge"]["Psychology"].AsInt + 1;
//						break;
//				}
//				}else if (index > 14 && index < 20){
//					
//				}
//				co.UpdateObject();
		} 
	}

	void AddButtonListener(){
		TeacherImage.GetComponent<Button> ().onClick.AddListener (delegate {
			if (trainingObject.etaTimestamp < DateTime.Now){
				Academy.CounselorHolderFunction = "TeacherImage";
				Academy.CounselorHolderButton = TeacherImage.GetComponent<Button>();
				Academy.staticCounselorHolder.SetActive(true);
			}
		});
		StudentImage.GetComponent<Button> ().onClick.AddListener (delegate  {
			if (trainingObject.etaTimestamp < DateTime.Now){
				Academy.CounselorHolderFunction = "StudentImage";
				Academy.CounselorHolderButton = StudentImage.GetComponent<Button>();
				Academy.staticCounselorHolder.SetActive(true);
			}
		 });

	}

	// Update is called once per frame
	void Update () {
//		Debug.Log (Utilities.TimeUpdate.Time (etaTimestamp));
//		LeftTimeText.text = (etaTimestamp > DateTime.Now) ? Utilities.TimeUpdate.Time (etaTimestamp) : "00:00:00";.
		LeftTimeText.text = Utilities.TimeUpdate.Time (etaTimestamp);
	}

	public static void showPanelItems(List<AcademyTeach> items){
		var count = items.Count;
		Utilities.ShowLog.Log ("Count of the Prefabs: " + count);
		for (var i =0; i < count; i++) {
			items[i].transform.SetParent(AcademyTeach.commonPanel);
			RectTransform rTransform = items[i].GetComponent<RectTransform>();
			rTransform.localScale= Vector3.one;
		}
	}

	public static void hidePanelItems(List<AcademyTeach> items){
		var count = items.Count;
		for (var i =0; i < count; i++) {
			items[i].transform.SetParent(null);
		}
	}

	public static void SetCurrentIndex(string panel, int index){
		if (panel == "IQ") {
			currentIQIndex = index;
			currentCommandedIndex = 0;
			currentKnowledgeIndex = 0;
			currentFightingIndex = 0;
		}else 		if (panel == "IQ") {
			currentIQIndex = 0;
			currentCommandedIndex = index;
			currentKnowledgeIndex = 0;
			currentFightingIndex = 0;
		}else 		if (panel == "IQ") {
			currentIQIndex = 0;
			currentCommandedIndex = 0;
			currentKnowledgeIndex = index;
			currentFightingIndex = 0;
		}else 		if (panel == "IQ") {
			currentIQIndex = 0;
			currentCommandedIndex = 0;
			currentKnowledgeIndex = 0;
			currentFightingIndex = index;
		}
	}

//	public static void setDropZone(List<AcademyTeach> items){
//		var count = items.Count;
////		bool isAnyItemEnabled = false;
//		for (var i =0; i < count; i++) {
//			items[i].TeacherDropZone.enabled =  false;
//			items[i].StudentDropZone.enabled = false;
//			items[i].isDropZoneEnabled = false;
//		}
//		for (var i =0; i < count; i++) {
//			if (items[i].LeftTimeText.text != "00:00:00"){
//				items[i].TeacherDropZone.enabled =  false;
//				items[i].StudentDropZone.enabled = false;
//				items[i].isDropZoneEnabled = false;
//			}else{
//				if (!isAnyItemEnabled){
//					items[i].TeacherDropZone.enabled = true;
//					items[i].StudentDropZone.enabled = true;
//					isAnyItemEnabled = true;
//					items[i].isDropZoneEnabled = true;
//				}
//			}
//		}
//		int firstAvailItem = AcademyTeach.findFirstAvailableItem (items);
//		Debug.Log ("First available item: "+firstAvailItem);
//		items[firstAvailItem].TeacherDropZone.enabled = true;
//		items[firstAvailItem].StudentDropZone.enabled = true;
////		isAnyItemEnabled = true;
//		items[firstAvailItem].isDropZoneEnabled = true;
//	}

//	private static int findFirstAvailableItem(List<AcademyTeach> items){
//		var count = items.Count;
//		int i = 0;
//		for (i =0; i < count; i++) {
//			if (items[i].LeftTimeText.text == "00:00:00"){
//				break;
//			}
//		}
//		return i;
//	}
}

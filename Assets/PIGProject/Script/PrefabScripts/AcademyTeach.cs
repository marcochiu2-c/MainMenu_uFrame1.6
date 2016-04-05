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
	public int trainType;
	public int trainerId;
	public int targetId;
	public int trainerType;
	public int targetType;
	
	public static Transform commonPanel;

	public DropZone TeacherDropZone;
	public DropZone StudentDropZone;
	public bool isDropZoneEnabled = false; 
	public static int currentIQIndex=0;
	public static int currentCommandedIndex=0;
	public static int currentKnowledgeIndex=0;
	public static int currentFightingIndex=0;
	// Use this for initialization
	void Start () {
		TeacherDropZone = TeacherImage.GetComponent ("DropZone") as DropZone;
		StudentDropZone = StudentImage.GetComponent ("DropZone") as DropZone;
	}
	
	// Update is called once per frame
	void Update () {
//		Debug.Log (Utilities.TimeUpdate.Time (etaTimestamp));
		LeftTimeText.text = Utilities.TimeUpdate.Time (etaTimestamp);
	}

	public static void showPanelItems(List<AcademyTeach> items){
		var count = items.Count;
		for (var i =0; i < count; i++) {
			items[i].transform.parent = AcademyTeach.commonPanel;
			RectTransform rTransform = items[i].GetComponent<RectTransform>();
			rTransform.localScale= Vector3.one;
		}
	}

	public static void hidePanelItems(List<AcademyTeach> items){
		var count = items.Count;
		for (var i =0; i < count; i++) {
			items[i].transform.parent = null;
		}
	}

	public static void setDropZone(List<AcademyTeach> items){
		var count = items.Count;
//		bool isAnyItemEnabled = false;
		for (var i =0; i < count; i++) {
			items[i].TeacherDropZone.enabled =  false;
			items[i].StudentDropZone.enabled = false;
			items[i].isDropZoneEnabled = false;
		}
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
		int firstAvailItem = AcademyTeach.findFirstAvailableItem (items);
		items[firstAvailItem].TeacherDropZone.enabled = true;
		items[firstAvailItem].StudentDropZone.enabled = true;
//			isAnyItemEnabled = true;
		items[firstAvailItem].isDropZoneEnabled = true;
	}

	private static int findFirstAvailableItem(List<AcademyTeach> items){
		var count = items.Count;
		int i = 0;
		for (i =0; i < count; i++) {
			if (items[i].LeftTimeText.text == "00:00:00"){
				break;
			}
		}
		return i;
	}
}

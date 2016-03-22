using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AcademyTeach : MonoBehaviour {
	public Image TeacherImage;
	public Image StudentImage;
	public Text KnowledgeText;
	public Text LeftTimeText;

	public Sprite TeacherPic { get { return TeacherImage.sprite; } set { TeacherImage.sprite = value; } }
	public Sprite StudentPic { get { return StudentImage.sprite; } set { StudentImage.sprite = value; } }
	public string KnowledgeValue { get { return KnowledgeText.text; } set { KnowledgeText.text = value; } }
	public string LeftTimeValue { get { return LeftTimeText.text; } set { LeftTimeText.text = value; } }

	public DateTime etaTimestamp;

	public static List<AcademyTeach> IQTeach = new List<AcademyTeach>();
	public static List<AcademyTeach> CommandedTeach = new List<AcademyTeach>();
	public static List<AcademyTeach> KnowledgeTeach = new List<AcademyTeach>();
	public static List<AcademyTeach> FightingTeach = new List<AcademyTeach>();

	public static Transform commonPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan t = etaTimestamp.Subtract (DateTime.Now);
		LeftTimeValue = Convert.ToInt32(t.TotalHours) + ":" + t.Minutes.ToString("00") + ":" + t.Seconds.ToString("00");
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
}

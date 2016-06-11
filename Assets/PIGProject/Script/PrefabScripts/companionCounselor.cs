using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Utilities;

public class companionCounselor : MonoBehaviour {
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

	
}

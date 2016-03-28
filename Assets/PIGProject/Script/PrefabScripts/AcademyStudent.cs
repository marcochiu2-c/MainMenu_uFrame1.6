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

	public static Transform commonPanel;
	public int characterType;
	public int characterId;
	public bool isDropped=false;
	GameObject candidatePanel;
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
				drag.placeholderParent.GetComponent<Image>().sprite = StudentPic;
				AcademyTeach at = drag.placeholderParent.parent.GetComponent("AcademyTeach") as AcademyTeach;
				if (drag.placeholderParent.ToString() == "StudentImage (UnityEngine.RectTransform)"){
					if (at.StudentImageText.text != "徒弟"){
						AcademyStudent.reCreateStudentItem(at,false);
					}
					at.targetId = characterId;
					at.targetType = characterType;
					at.StudentImageText.text = StudentImageText.text;
				}else if (drag.placeholderParent.ToString() == "TeacherImage (UnityEngine.RectTransform)"){
					if (at.TeacherImageText.text != "師傅"){
						AcademyStudent.reCreateStudentItem(at,true);
					}
					at.trainerId = characterId;
					at.trainerType = characterType;
					at.TeacherImageText.text = StudentImageText.text;
					string category = Academy.staticTeachHolder.transform.GetChild (0).GetComponent<Text>().text;
					AcademyStudent.showSkillsOptionPanel(category);
				}
				#endregion

				#region OpenTeacherSkillsPanel
// TODO if target object is TeacherImage, show panel for choosing skills/knowledges
// TODO if both TeacherImage and StudentImage filled and choosen skills/knowledges, upload the details to DB.
				GameObject.Destroy(this.gameObject);
				#endregion
				isDropped = true;
			}
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


	public static void reCreateStudentItem(AcademyTeach aTeach, bool isTeacher){
		AcademyStudent obj = Instantiate(Resources.Load("AcademyStudentPrefab") as GameObject).GetComponent<AcademyStudent>();
		if (isTeacher) {
			obj.characterType = aTeach.trainerType;
			obj.characterId = aTeach.trainerId;
			obj.StudentPic = aTeach.TeacherPic;
			obj.StudentImageText.text = aTeach.TeacherImageText.text;
		} else {
			obj.characterType = aTeach.targetType;
			obj.characterId = aTeach.targetId;
			obj.StudentPic = aTeach.StudentPic;
			obj.StudentImageText.text = aTeach.StudentImageText.text;
		}
		obj.transform.parent = AcademyStudent.commonPanel;
		RectTransform rTransform = obj.GetComponent<RectTransform>();
		rTransform.localScale= Vector3.one;
		
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

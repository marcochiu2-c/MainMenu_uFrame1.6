using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum Knowledge {
	Woodworker=2001, MetalFabrication=2002, EasternHistory=2003, WesternHistory=2004, ChainSteel=2005, MetalProcessing=2006, Crafts=2007, Geometry=2008,
	Physics=2009, Chemistry=2010, PeriodicTable=2011, Pulley=2012, Anatomy=2013, Catapult=2014, GunpowderModulation=2015, Psychology=2016, IChing=2017
}

public class KnowledgeOption : MonoBehaviour {
	public static Button Woodworker;
	public static Button MetalFabrication;
	public static Button EasternHistory;
	public static Button WesternHistory;
	public static Button ChainSteel;
	public static Button MetalProcessing;
	public static Button Crafts;
	public static Button Geometry;
	public static Button Physics;
	public static Button Chemistry;
	public static Button PeriodicTable;
	public static Button Pulley;
	public static Button Anatomy;
	public static Button Catapult;
	public static Button GunpowderModulation;
	public static Button Psychology;
	public static Button IChing;
	public static int knowledge;
	public static string knowledgeName;

	public static void AssignButton(GameObject panel){
		Woodworker = panel.transform.GetChild (2).GetChild (0).GetComponent<Button>();
		MetalFabrication = panel.transform.GetChild (2).GetChild (1).GetComponent<Button>();
		EasternHistory = panel.transform.GetChild (2).GetChild (2).GetComponent<Button>();
		WesternHistory = panel.transform.GetChild (2).GetChild (3).GetComponent<Button>();
		ChainSteel = panel.transform.GetChild (2).GetChild (4).GetComponent<Button>();
		MetalProcessing = panel.transform.GetChild (2).GetChild (5).GetComponent<Button>();
		Crafts = panel.transform.GetChild (2).GetChild (6).GetComponent<Button>();
		Geometry = panel.transform.GetChild (2).GetChild (7).GetComponent<Button>();
		Physics = panel.transform.GetChild (2).GetChild (8).GetComponent<Button>();
		Chemistry = panel.transform.GetChild (2).GetChild (9).GetComponent<Button>();
		PeriodicTable = panel.transform.GetChild (2).GetChild (10).GetComponent<Button>();
		Pulley = panel.transform.GetChild (2).GetChild (11).GetComponent<Button>();
		Anatomy = panel.transform.GetChild (2).GetChild (12).GetComponent<Button>();
		Catapult = panel.transform.GetChild (2).GetChild (13).GetComponent<Button>();
		GunpowderModulation = panel.transform.GetChild (2).GetChild (14).GetComponent<Button>();
		Psychology = panel.transform.GetChild (2).GetChild (15).GetComponent<Button>();
		IChing = panel.transform.GetChild (2).GetChild (16).GetComponent<Button>();
	}

	public static void AddButtonListener(GameObject panel,GameObject parent){
		Woodworker.onClick.AddListener (() => {
			knowledge = 2001;
			knowledgeName = "Woodworker";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		MetalFabrication.onClick.AddListener (() => {
			knowledge = 2002;
			knowledgeName = "MetalFabrication";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		EasternHistory.onClick.AddListener (() => {
			knowledge = 2003;
			knowledgeName = "EasternHistory";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		WesternHistory.onClick.AddListener (() => {
			knowledge = 2004;
			knowledgeName = "WesternHistory";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});

		ChainSteel.onClick.AddListener (() => {
			knowledge = 2005;
			knowledgeName = "ChainSteel";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		MetalProcessing.onClick.AddListener (() => {
			knowledge = 2006;
			knowledgeName = "MetalProcessing";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			};
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Crafts.onClick.AddListener (() => {
			knowledge = 2007;
			knowledgeName = "Crafts";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Geometry.onClick.AddListener (() => {
			knowledge = 2008;
			knowledgeName = "Geometry";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Physics.onClick.AddListener (() => {
			knowledge = 2009;
			knowledgeName = "Physics";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Chemistry.onClick.AddListener (() => {
			knowledge = 2010;
			knowledgeName = "Chemistry";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		PeriodicTable.onClick.AddListener (() => {
			knowledge = 2011;
			knowledgeName = "PeriodicTable";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Pulley.onClick.AddListener (() => {
			knowledge = 2012;
			knowledgeName = "Pulley";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Anatomy.onClick.AddListener (() => {
			knowledge = 2013;
			knowledgeName = "Anatomy";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Catapult.onClick.AddListener (() => {
			knowledge = 2014;
			knowledgeName = "Catapult";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		GunpowderModulation.onClick.AddListener (() => {
			knowledge = 2015;
			knowledgeName = "GunpowderModulation";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		Psychology.onClick.AddListener (() => {
			knowledge = 2016;
			knowledgeName = "Psychology";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
		IChing.onClick.AddListener (() => {
			knowledge = 2017;
			knowledgeName = "IChing";
			if (Academy.SelfStudyHolder.activeSelf == true){
//				parent.GetComponent<Academy>().ConfirmTraining.SetActive(true);
				Academy.ConfirmTraining.SetActive(true);
			}else{
//				parent.GetComponent<Academy>().ConfirmTeacherBy.SetActive(true);
				Academy.ConfirmTeacherBy.SetActive(true);
			}
//			parent.GetComponent<Academy>().KnowledgeListHolder.SetActive(false);
			Academy.KnowledgeListHolder.SetActive(false);
		});
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnKnowledgeButtonClicked(Button btn){
		string name = btn.transform.GetChild(0).GetComponent<Text>().text;
		Debug.Log (name);
	}
}

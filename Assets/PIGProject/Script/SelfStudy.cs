using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.Sprites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SelfStudy : MonoBehaviour {
	public static Trainings currentTrainingItem;
	public static List<Trainings> IQTrainingItems = new List<Trainings>{new Trainings(),new Trainings(),new Trainings(),new Trainings(),new Trainings()};
	public static List<Trainings> CommandedTrainingItems = new List<Trainings>{new Trainings(),new Trainings(),new Trainings(),new Trainings(),new Trainings()};
	public static List<Trainings> KnowledgeTrainingItems = new List<Trainings>{new Trainings(),new Trainings(),new Trainings(),new Trainings(),new Trainings()};
	public static List<Trainings> FightingTrainingItems = new List<Trainings>{new Trainings(),new Trainings(),new Trainings(),new Trainings(),new Trainings()};
	public Text ImageText;
	public Image image;
	public DateTime etaTimestamp;
	public int targetId=0;
	public int trainingType=0;
//	public int targetType = 0;
	public bool isDropZoneEnabled = true; 
	public Trainings trainingObject { get; set; }


	public static Sprite defaultSprite;
	// Use this for initialization
	void Start () {

	}

	void OnEnable(){
		InvokeRepeating ("UpdateImageText", 0, 1);
	}
	
	void OnDisable(){
		CancelInvoke ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public static void OnTrainingCompleted(){
		Game game = Game.Instance;
		int type = 0;
		for (int i=20; i<40; i++) {
			if (game.trainings[i].etaTimestamp < DateTime.Now && game.trainings[i].targetId > 0 && game.trainings[i].status==1) {
				Counselor co = game.counselor.Find (x => x.id == game.trainings [i].targetId);
				type = game.trainings [i].type;
				game.trainings [i].status = 3;
				game.trainings [i].trainerId = 0;
				game.trainings [i].targetId = 0;
				game.trainings [i].type = 0;

				Debug.Log ("Training array index: "+i);
				Debug.Log ("Training type: "+type);
				if (i < 25) { 
					co.attributes ["attributes"] ["IQ"].AsFloat = co.attributes ["attributes"] ["IQ"].AsFloat + 1;
				} else if (i > 24 && i < 30) {
					co.attributes ["attributes"] ["Leadership"].AsFloat = co.attributes ["attributes"] ["Leadership"].AsFloat + 1;
				} else if (i > 29 && i < 35) {
					switch (type) {
					case 2001:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Woodworker"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Woodworker"].AsInt + 1;
						Debug.Log(co.attributes ["attributes"] ["KnownKnowledge"] ["Woodworker"].AsInt);
						break;
					case 2002:
						co.attributes ["attributes"] ["KnownKnowledge"] ["MetalFabrication"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["MetalFabrication"].AsInt + 1;
						break;
					case 2003:
						co.attributes ["attributes"] ["KnownKnowledge"] ["ChainSteel"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["ChainSteel"].AsInt + 1;
						break;					
					case 2004:
						co.attributes ["attributes"] ["KnownKnowledge"] ["MetalProcessing"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["MetalProcessing"].AsInt + 1;
						break;					
					case 2005:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Crafts"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Crafts"].AsInt + 1;
						break;					
					case 2006:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Geometry"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Geometry"].AsInt + 1;
						break;					
					case 2007:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Physics"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Physics"].AsInt + 1;
						break;					
					case 2008:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Chemistry"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Chemistry"].AsInt + 1;
						break;					
					case 2009:
						co.attributes ["attributes"] ["KnownKnowledge"] ["PeriodicTable"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["PeriodicTable"].AsInt + 1;
						break;					
					case 2010:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Pulley"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Pulley"].AsInt + 1;
						break;					
					case 2011:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Anatomy"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Anatomy"].AsInt + 1;
						break;					
					case 2012:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Catapult"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Catapult"].AsInt + 1;
						break;					
					case 2013:
						co.attributes ["attributes"] ["KnownKnowledge"] ["GunpowderModulation"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["GunpowderModulation"].AsInt + 1;
						break;					
					case 2014:
						co.attributes ["attributes"] ["KnownKnowledge"] ["Psychology"].AsInt = co.attributes ["attributes"] ["KnownKnowledge"] ["Psychology"].AsInt + 1;
						break;
					}
				} else if (i > 34 && i < 40) {
				
				}
				game.trainings [i].UpdateObject ();
				co.UpdateObject ();

			} 
		}
	}

	void UpdateImageText(){
		Debug.Log ("UpdateImageText()");
		int index = 0;
		Counselor thisCounselor;
		Game game = Game.Instance;
		string title = Academy.staticSelfStudyHolder.transform.GetChild (0).GetChild (0).GetComponent<Text> ().text;
		Transform leftHolder = Academy.staticSelfStudyHolder.transform.GetChild (0).GetChild (1).GetChild (0);
		string[] it = Regex.Split(ImageText.text, "\n");
		if (it[0].Trim () !=""){
			ImageText.text = String.Format("{0}\n{1}",
			                               it[0],
			                               Utilities.TimeUpdate.Time ( etaTimestamp)
			                               );
			isDropZoneEnabled = false;
		}else{
			ImageText.text = "";
			image.sprite = defaultSprite;
			isDropZoneEnabled = true;
		}
		if (it.Length == 2) {
			Debug.Log (it[1]);
			if (it[1] == "00:00:00"){
				ImageText.text = "";
				image.sprite = defaultSprite;
				isDropZoneEnabled = true;
			}
		}
	}

	public static void ShowPanelItems(string panel, Dictionary<int,Sprite> imageDict, Dictionary<int,string> nameDict ){
		Game game = Game.Instance;
		Dictionary<int,string> KnowledgeDict = Utilities.SetDict.Knowledge ();
		Dictionary<int,string> KnowledgeID = Utilities.SetDict.KnowledgeID ();
		Transform leftHolder = Academy.staticSelfStudyHolder.transform.GetChild (0).GetChild (1).GetChild (0);
		SelfStudy p;
		for (int i = 0; i <5; i++){
			p = leftHolder.GetChild(i*2).GetComponent<SelfStudy>();
			Debug.Log (leftHolder.GetChild(0));	
			if (panel == "IQ") {	
				if (game.trainings[i+20].targetId !=0){
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingType = game.trainings[i+20].type;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().targetId = game.trainings[i+20].targetId;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().etaTimestamp = game.trainings[i+20].etaTimestamp;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().ImageText.text = String.Format("{0} 目標：{1}\n{2}",
					                      nameDict[game.counselor.Find(x => x.id == p.targetId).attributes["type"].AsInt],
					                      (game.counselor.Find(x => x.id == p.targetId).attributes["attributes"]["IQ"].AsFloat+1).ToString(),
					                      Utilities.TimeUpdate.Time ( p.etaTimestamp)
					                   );
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = game.trainings[i+20];
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = false;
				}else{
//					Debug.Log (leftHolder.GetChild(i*2).GetComponent<SelfStudy>());
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingType =0 ;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().targetId = 0;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().etaTimestamp = game.trainings[i+20].etaTimestamp;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().ImageText.text = "";
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = new Trainings();
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = true;
				}
				IQTrainingItems[i] = game.trainings[i+20];
			}else if (panel == "Commanded") {
				if (game.trainings[i+25].targetId !=0){
					p.trainingType = game.trainings[i+25].type;
					p.targetId = game.trainings[i+25].targetId;
					p.etaTimestamp = game.trainings[i+25].etaTimestamp;
					SelfStudy.CommandedTrainingItems[i] = game.trainings[i+25];
					p.ImageText.text = String.Format("{0} 目標：{1}\n{2}",
					                      nameDict[game.counselor.Find(x => x.id == p.targetId).attributes["type"].AsInt], 
					                      (game.counselor.Find(x => x.id == p.targetId).attributes["attributes"]["IQ"].AsFloat+1).ToString(),
					                      Utilities.TimeUpdate.Time ( p.etaTimestamp)
					                   );
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = game.trainings[i+25];
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = false;
				}else{
					p.trainingType =0 ;
					p.targetId = 0;
					p.etaTimestamp = game.trainings[i+25].etaTimestamp;
					p.ImageText.text = "";
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = new Trainings();
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = true;
				}
				CommandedTrainingItems[i] = game.trainings[i+25];
			}else if (panel == "Knowledge") {
				Debug.Log ("Target Id: "+game.trainings[30].targetId);
				if(game.trainings[i+30].targetId != 0){
					p.trainingType = game.trainings[i+30].type;
					p.targetId = game.trainings[i+30].targetId;
					p.etaTimestamp = game.trainings[i+30].etaTimestamp;
					Debug.Log (game.trainings[i+30].type);
					p.ImageText.text = String.Format("{0} {1}({2})\n{3}",
					                        nameDict[game.counselor.Find(x => x.id == p.targetId).attributes["type"].AsInt],
					                        KnowledgeDict[game.trainings[i+30].type],
					                        (game.counselor.Find(x => x.id == p.targetId).attributes["attributes"]["KnownKnowledge"][KnowledgeID[game.trainings[i+30].type]].AsInt+1).ToString(),
					                   		Utilities.TimeUpdate.Time ( p.etaTimestamp)
					                   );
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = game.trainings[i+30];
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = false;
				}else{
					p.trainingType =0 ;
					p.targetId = 0;
					p.etaTimestamp = game.trainings[i+30].etaTimestamp;
					p.ImageText.text = "";
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = new Trainings();
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = true;
				}
				KnowledgeTrainingItems[i] = game.trainings[i+30];
			}else if (panel == "Fighting") {
				if(game.trainings[i+35].targetId != 0){
					p.trainingType = game.trainings[i+35].type;
					p.targetId = game.trainings[i+35].targetId;
					p.etaTimestamp = game.trainings[i+35].etaTimestamp;
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = game.trainings[i+35];
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = false;
				}else{
					p.trainingType =0 ;
					p.targetId = 0;
					p.etaTimestamp = game.trainings[i+35].etaTimestamp;
					p.ImageText.text = "";
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().trainingObject = new Trainings();
					leftHolder.GetChild(i*2).GetComponent<SelfStudy>().isDropZoneEnabled = true;
				}
				FightingTrainingItems[i] = game.trainings[i+35];
			}

			if (p.targetId != 0){
				p.image.sprite = imageDict[p.targetId];
			}else {
				p.image.sprite = defaultSprite;
			}

//			if (panel == "IQ") {
//				leftHolder.GetChild(i/2).GetComponent<SelfStudy>() = p;
//			}else if (panel == "Commanded") {
//
//			}else if (panel == "Knowledge") {
//
//			}else if (panel == "Fighting") {
//
//			}
		}
	}
}

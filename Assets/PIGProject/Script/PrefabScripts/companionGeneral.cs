using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using Utilities;

public class companionGeneral : MonoBehaviour {

	Game game;
	WsClient wsc;

	public Image generalImage;
	public Text generalName;

//	public Sprite counselorPic { get { return counselorImage.sprite; } set { counselorImage.sprite = value; } }
	public Sprite generalPic { get { return generalImage.sprite; } set { generalImage.sprite = value; } }


	public static List<companionGeneral> generalList = new List<companionGeneral> ();
	public static Transform commonPanel;

	public GameObject CompanionHead;

	public int characterId, characterType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void showPanelItems(List<companionGeneral> items){
		Game game=Game.Instance;
		WsClient wsc=WsClient.Instance;

		var count = items.Count;
		for (var i=0; i<count; i++) {
			items[i].transform.parent=companionGeneral.commonPanel;
			RectTransform rTransform = items[i].GetComponent<RectTransform>();
			rTransform.localScale=Vector3.one;
		}
	}

	public static void CreateGeneralitem(companionGeneral Generals){
		List<GeneralCards> g=GeneralCards.GetList (1);
		companionGeneral obj=Instantiate (Resources.Load ("GeneralPrefab")as GameObject).GetComponent<companionGeneral>();
		Debug.Log ("CreateGeneralItem()");
		obj.characterId=Generals.characterId;
		obj.characterType=Generals.characterType;
		obj.generalPic=Generals.generalPic;
		obj.generalName.text=g[Generals.characterType-1].Name;
		obj.transform.parent=companionGeneral.commonPanel;
		RectTransform rTransform=obj.GetComponent<RectTransform>();
		rTransform.localScale=Vector3.one;
		generalList.Add (obj);
	}
}

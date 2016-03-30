using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class SchoolField : MonoBehaviour {
	public GameObject DollPanel;
	public GameObject DataPanel;
	public GameObject NewSoldierPanel;
	public GameObject ArmyListHolder;
	public GameObject ArmyQAHolder;
	public GameObject MountListHolder;
	public GameObject MountQAHolder;
	public GameObject ArmorListHolder;
	public GameObject ArmorQAHolder;
	public GameObject TrainingQHolder;
	public GameObject TrainingEquHolder;
	public GameObject TrainingQAHolder;

	Game game;
	WsClient wsc;
	Draggable drag;
	public bool hasTrainingQHolderShown = false;
	float rtx;
	float rty;
	RectTransform rt;
	// Use this for initialization
	void Start () {
		CallSchoolField ();
	}

	void CallSchoolField(){

		game = Game.Instance;
		wsc = WsClient.Instance;
		Debug.Log (TotalSoldiersAvailable());
		InvokeRepeating ("ShowTotalSoldiersAvailableText", 0, 60);
		drag = NewSoldierPanel.transform.GetChild(0).GetComponent ("Draggable") as Draggable;
		rt = (RectTransform)drag.transform;
		rtx = rt.localPosition.x; rty = rt.localPosition.y;
		ShowNewWeaponPanel ();
	}
	
	// Update is called once per frame
	void Update () {
		if (drag.parentToReturnTo != null) {
			if (drag.parentToReturnTo == drag.placeholderParent && !hasTrainingQHolderShown &&
				(drag.placeholderParent.ToString () == "DollPanel (UnityEngine.RectTransform)")) {
				#region DropTheImageAndInfoCopy
//				drag.placeholderParent.GetComponent<Image>().sprite = StudentPic;
				AcademyTeach at = drag.placeholderParent.parent.GetComponent ("AcademyTeach") as AcademyTeach;
				if (drag.placeholderParent.ToString () == "DollPanel (UnityEngine.RectTransform)") {
					drag.transform.SetParent(NewSoldierPanel.transform);
					NewSoldierPanel.transform.GetChild(0).localPosition = new Vector3( rtx,rty,0);
					TrainingQHolder.SetActive(true);
					hasTrainingQHolderShown = true;
				}
				#endregion
			}
			SetHasTrainingQHolderShown();
		}
	}

	#region ShowSoldiersAvailable

	void ShowTotalSoldiersAvailableText(){
//		Text availSoldiersText = NewSoldierPanel.transform.GetChild (0).GetComponent<Text> ();
		Text availSoldiersText = NewSoldierPanel.transform.GetChild (0).GetChild (0).GetComponent<Text> ();
		var i = TotalSoldiersAvailable ();
		if (i > 99999) {
			availSoldiersText.fontSize -= 1;
		} else if (i > 999999) {
			availSoldiersText.fontSize -= 2;
		}
		availSoldiersText.text = "現有兵數：\n"+i;
	}

	int TotalSoldiersAvailable(){
		return TotalSoldierGenerated() - game.login.attributes["TotalDeductedSoldiers"].AsInt;
	}

	int TotalSoldierGenerated(){
		DateTime rt = game.login.registerTime;
		return (int)DateTime.Now.Subtract (rt).TotalMinutes;
	}
	#endregion

	public void OnSetNewSoldierNumber(){
		string s = transform.GetChild (9).GetChild (1).GetChild (0).GetChild (2).GetComponent<Text> ().text;
		s = Regex.Replace(s, "[^0-9]", "");
		if (s != "") {
			int soldiers = Int32.Parse (s);
			if (soldiers > TotalSoldiersAvailable ()) {
				transform.GetChild (9).gameObject.SetActive (true);
				transform.GetChild(9).GetChild(1).GetComponent<Text>().text="<color=red>輸入兵數不能大於現有兵數</color>\n請輸入欲訓練士兵數目";
				Debug.Log ("Number of new soldiers cannot be larger than available, inputed: " + soldiers);
			} else {   //Valid number
				transform.GetChild(9).GetChild(1).GetComponent<Text>().text="請輸入欲訓練士兵數目";
				transform.GetChild (9).gameObject.SetActive (false);
				Debug.Log ("士兵數目："+s);
				game.login.attributes["TotalDeductedSoldiers"].AsInt = game.login.attributes["TotalDeductedSoldiers"].AsInt + soldiers;
				Debug.Log (game.login.attributes["TotalDeductedSoldiers"].AsInt );
				ShowTotalSoldiersAvailableText();
				//wsc.Send ("users","SET",game.login.toJSON());
				if (true) { // TODO get weapon number  // not enough weapon
					ShowIsNeedRecruitArtisanPanel(soldiers);
				}else{

				}
			}
		} else {
			return;
		}
	}

	public void ShowNewWeaponPanel(){
		ProductDict p = new ProductDict ();
		var count = game.weapon.Count;
		Transform panel = ArmyListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate(Resources.Load("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon>();
			obj.transform.localScale = new Vector3(0.51f,0.51f,0.51f);
			obj.SetSchoolFieldWeapon(p.products[game.weapon[i].type].name , 
			                         game.weapon[i].quantity.ToString(), 
			                         p.products[game.weapon[i].type].attributes["Category"] ,
			                         "");

			obj.transform.SetParent(panel);
		}
		ArmyListHolder.SetActive (true);
	}

	public void ShowIsNeedRecruitArtisanPanel(int soldiers){
		string txt = TrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text;
		txt = txt.Replace ("%SQ%", soldiers.ToString ());
		TrainingEquHolder.transform.GetChild (1).GetComponent<Text> ().text = txt;
		TrainingEquHolder.SetActive (true);
	}

	public void SetHasTrainingQHolderShown(){
		hasTrainingQHolderShown = false;
		drag.parentToReturnTo = null;
		drag.placeholderParent = null;
	}
}

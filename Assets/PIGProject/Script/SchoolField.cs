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
	public GameObject ShieldListHolder;
	public GameObject ShieldQAHolder;
	public GameObject TrainingQHolder;
	public GameObject TrainingEquHolder;
	public GameObject TrainingQAHolder;

	public static int AssigningWeaponId;
	public static int AssigningArmorId;
	public static int AssigningShieldId;

	Game game;
	WsClient wsc;
	Draggable drag;
//	public bool hasTrainingQHolderShown = false;
	RectTransform rt;
	// Use this for initialization
	void Start () {
		CallSchoolField ();
	}

	void CallSchoolField(){

		game = Game.Instance;
		wsc = WsClient.Instance;
//		Debug.Log (TotalSoldiersAvailable());
		InvokeRepeating ("ShowTotalSoldiersAvailableText", 0, 60);
	}
	
	// Update is called once per frame
	void Update () {
//		if (drag.parentToReturnTo != null) {
//			if (drag.parentToReturnTo == drag.placeholderParent && !hasTrainingQHolderShown &&
//				(drag.placeholderParent.ToString () == "DollPanel (UnityEngine.RectTransform)")) {
//				#region DropTheImageAndInfoCopy
////				drag.placeholderParent.GetComponent<Image>().sprite = StudentPic;
//				AcademyTeach at = drag.placeholderParent.parent.GetComponent ("AcademyTeach") as AcademyTeach;
//				if (drag.placeholderParent.ToString () == "DollPanel (UnityEngine.RectTransform)") {
//					drag.transform.SetParent(NewSoldierPanel.transform);
//					NewSoldierPanel.transform.GetChild(0).localPosition = new Vector3( rtx,rty,0);
//					TrainingQHolder.SetActive(true);
//					hasTrainingQHolderShown = true;
//				}
//				#endregion
//			}
//		}
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
		string s = TrainingQHolder.transform.GetChild (1).GetChild (0).GetChild (2).GetComponent<Text> ().text;
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
//				if (true) { // TODO get weapon number  // not enough weapon
//					ShowIsNeedRecruitArtisanPanel(soldiers);
//				}else{
//
//				}
			}
		} else {
			return;
		}
	}

	public void ShowNewWeaponPanel(){
		DestroySchoolFieldWeaponPanel(ArmyListHolder.transform);
		ProductDict p = new ProductDict ();
		var count = game.weapon.Count;
		Transform panel = ArmyListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
			obj.SetSchoolFieldWeapon (game.weapon [i].type,
								p.products [game.weapon [i].type].name, 
		                        game.weapon [i].quantity.ToString (), 
		                        p.products [game.weapon [i].type].attributes ["Category"],
		                        "");

			obj.transform.SetParent (panel);
		}

//		ArmyListHolder.SetActive (true);
	}

	public void ShowNewArmorPanel(){
		DestroySchoolFieldWeaponPanel(ArmorListHolder.transform);
		ProductDict p = new ProductDict ();
		var count = game.armor.Count;
		Transform panel = ArmorListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
			obj.SetSchoolFieldWeapon (game.armor [i].type,
			                          p.products [game.armor [i].type].name, 
			                          game.armor [i].quantity.ToString (), 
			                          "",
			                          "");
			
			obj.transform.SetParent (panel);
			
		}
//		ArmyListHolder.SetActive (true);
	}

	public void ShowNewShieldPanel(){
		DestroySchoolFieldWeaponPanel(ShieldListHolder.transform);
		ProductDict p = new ProductDict ();
		var count = game.shield.Count;
		Transform panel = ShieldListHolder.transform.GetChild (1).GetChild (1).GetChild (0);
		for (int i = 0; i < count; i++) {
			SchoolFieldWeapon obj = Instantiate (Resources.Load ("SchoolFieldWeaponPrefab") as GameObject).GetComponent<SchoolFieldWeapon> ();
			obj.transform.localScale = new Vector3 (0.51f, 0.51f, 0.51f);
			obj.SetSchoolFieldWeapon (game.shield [i].type,
			                          p.products [game.shield [i].type].name, 
			                          game.shield [i].quantity.ToString (), 
			                          "",
			                          "");
			
			obj.transform.SetParent (panel);
			
		}
//		ArmyListHolder.SetActive (true);
	}
	
	public void ShowIsNeedRecruitArtisanPanel(int soldiers){
		string txt = TrainingEquHolder.transform.GetChild(1).GetComponent<Text>().text;
		txt = txt.Replace ("%SQ%", soldiers.ToString ());
		TrainingEquHolder.transform.GetChild (1).GetComponent<Text> ().text = txt;
		TrainingEquHolder.SetActive (true);
	}

	public void OnWeaponButtonClicked(){
		ShowNewWeaponPanel ();
	}

	public void OnArmorButtonClicked(){
		ShowNewArmorPanel ();

	}

	public void OnShieldButtonClicked(){
		ShowNewShieldPanel ();

	}
	
	public void OnPanelClose(){
		#region Destroy List of Weapons
		DestroySchoolFieldWeaponPanel(ArmyListHolder.transform);
		DestroySchoolFieldWeaponPanel(ArmorListHolder.transform);
		DestroySchoolFieldWeaponPanel(ShieldListHolder.transform);
		#endregion
	}

	public void DestroySchoolFieldWeaponPanel(Transform panel){
		Transform t = panel.transform.GetChild (1).GetChild (1).GetChild (0);
		int count = t.childCount;
		for (int j = 0; j<count; j++) {
			GameObject.DestroyImmediate(t.GetChild(0).gameObject);
		}
	}
}

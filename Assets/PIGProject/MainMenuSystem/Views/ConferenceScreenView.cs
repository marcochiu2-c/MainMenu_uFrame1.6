using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Services;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class ConferenceScreenView : ConferenceScreenViewBase {

	public Button armyAttack;
	public Button armyGarrison;
	public Button militaryAdviser;
	public Button defensiveLinup;
	public Button standings;
	
	/*
	public Button SetSoldierType;
	public Button SoldierType1;
	public Button SoldierType2;
	public Button SoldierType3;
	public Button SoldierType4;
	public Button SoldierType5;
	public Button SoldierType6;
	public Button SoldierType7;
	public Button SoldierType8;
    */
    
	public GameObject ArmyAttack;
	public GameObject ArmyGarrison;
	public GameObject MilitaryAdviser;
	public GameObject DefensiveLinup;
	public GameObject Standings;
	//public GameObject SoldierType;
	//public GameObject SoldierQuantity;
	
	public Transform GeneralScrollPanel;
	public List<General> GeneralList;
	public Dictionary<int,Sprite> imageDict;
	public Dictionary<int,string> nameDict;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ConferenceScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		Game game = Game.Instance;
		LoadHeadPic headPic = LoadHeadPic.SetCharacters();
		
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
		Debug.Log ("HeadPIG: " + headPic.SuenMo);
		//imageDict = headPic.imageDict;
		
		GeneralList = game.general;
		/*
		for (int j = 0 ; j < game.general.Count; j++){
			GeneralList.Add (new General(game.general[j]));
		}
		*/
		
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.ConferenceScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (armyAttack, () => {
			ArmyAttack.gameObject.SetActive (true);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (false);
			
			for (var i = 0 ; i < GeneralList.Count ; i++){
				CreateSelfLearnItem (GeneralList[i]);
			}
		});

		this.BindButtonToHandler (armyGarrison, () => {
			ArmyAttack.gameObject.SetActive (false);
			ArmyGarrison.gameObject.SetActive (true);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (militaryAdviser, () => {
			ArmyAttack.gameObject.SetActive (false);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (true);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (defensiveLinup, () => {
			ArmyAttack.gameObject.SetActive (false);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (true);
			Standings.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (standings, () => {
			ArmyAttack.gameObject.SetActive (false);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (true);
		});
		
		/*
		this.BindButtonToHandler (SetSoldierType, () => {
			SoldierType.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SoldierType1, () => {
			SoldierType.gameObject.SetActive (false);
			SoldierQuantity.gameObject.SetActive (true);

		});
		*/
    }
    
	public void CreateSelfLearnItem(General character){
		var type = character.type;
		Debug.Log ("Type: " + type);
		AcademySelfLearn ss = Instantiate(Resources.Load("GeneralPrefab") as GameObject).GetComponent<AcademySelfLearn>();
		ss.transform.parent =  GeneralScrollPanel;
		
		ss.characterType = character.type;
		ss.characterId = character.id;
		ss.StudentPic =  imageDict[type];
		ss.StudentImageText.text = nameDict[type];
		//AcademySelfLearn.Students.Add (ss);
		
		ss.transform.localScale = Vector3.one;
		
		Debug.Log ("Gen Genereal from ConfrenceScreen");
	}
}

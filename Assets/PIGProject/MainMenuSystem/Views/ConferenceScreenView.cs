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
	
	public Button SoldierTypeBtn;
	public Button SoldierType1;
	public Button SoldierType2;
	public Button SoldierType3;
	public Button InputConfirmBtn;
	
	public Button General1Btn;
	public Button General2Btn;
	public Button General3Btn;
	public Button General4Btn;
	public Button General5Btn;
	
	
	public InputField SoldierQuantityInput;
	public Text SoldierQuantityText;
	public Text QunatityErrorText;
	public Game game;

	public GameObject ArmyAttack;
	public GameObject ArmyGarrison;
	public GameObject MilitaryAdviser;
	public GameObject DefensiveLinup;
	public GameObject Standings;
	public GameObject SelectSoldierPanel;
	public GameObject SoldierQuantityPanel;
	
	public GameObject GeneralHolder;
	public Transform GeneralListHolder;
	
	/*
	public Image General1;
	public Image General2;
	public Image General3;
	public Image General4;
	public Image General5;
	*/
	
	public Transform General1;
	public Transform General2;
	public Transform General3;
	public Transform General4;
	public Transform General5;
	
	
	public List<General> GeneralList;
	public Dictionary<int,Sprite> imageDict;
	public Dictionary<int,string> nameDict;
	
	private static int _whichGeneral;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ConferenceScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		game = Game.Instance;
		LoadHeadPic headPic = LoadHeadPic.SetCharacters();
		
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
		Debug.Log ("HeadPIG: " + headPic.SuenMo);
		//imageDict = headPic.imageDict;
		
		GeneralList = game.general;
		
		_whichGeneral = 0;
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
		
		this.BindButtonToHandler (SoldierTypeBtn, () => {
			SelectSoldierPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SoldierType1, () => {
			this.ConferenceScreen.Group = 0;
			this.ConferenceScreen.SoldierType = 1;
			//ExecuteSetSoldierData();
			SelectSoldierPanel.gameObject.SetActive (false);
			SoldierTypeBtn.GetComponentInChildren<Text>().text = "兵種一";
			SoldierQuantityPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SoldierType2, () => {
			this.ConferenceScreen.Group = 0;
			this.ConferenceScreen.SoldierType = 2;
			//ExecuteSetSoldierData();
			SelectSoldierPanel.gameObject.SetActive (false);
			SoldierTypeBtn.GetComponentInChildren<Text>().text = "兵種二";
			SoldierQuantityPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SoldierType3, () => {
			this.ConferenceScreen.Group = 0;
			this.ConferenceScreen.SoldierType = 3;
			//ExecuteSetSoldierData();
			SelectSoldierPanel.gameObject.SetActive (false);
			SoldierTypeBtn.GetComponentInChildren<Text>().text = "兵種三";
			SoldierQuantityPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (General1Btn, () => {
		
			//int cslCount = Academy.cSelfLearnList.Count;
			for (var i = 0 ; i < GeneralList.Count ; i++){
				CreateSelfLearnItem (GeneralList[i]);
			}
			
			GeneralHolder.gameObject.SetActive (true);
			
			_whichGeneral = 1;
		});
		
		
		this.BindButtonToHandler (General2Btn, () => {
			
			//int cslCount = Academy.cSelfLearnList.Count;
			for (var i = 0 ; i < GeneralList.Count ; i++){
				CreateSelfLearnItem (GeneralList[i]);
			}
			
			GeneralHolder.gameObject.SetActive (true);
			
			_whichGeneral = 2;
			
		});
		
		this.BindButtonToHandler (General3Btn, () => {
			
			//int cslCount = Academy.cSelfLearnList.Count;
			for (var i = 0 ; i < GeneralList.Count ; i++){
				CreateSelfLearnItem (GeneralList[i]);
			}
			
			GeneralHolder.gameObject.SetActive (true);
			
			_whichGeneral = 3;
			
		});
		
		this.BindButtonToHandler (General4Btn, () => {
			
			//int cslCount = Academy.cSelfLearnList.Count;
			for (var i = 0 ; i < GeneralList.Count ; i++){
				CreateSelfLearnItem (GeneralList[i]);
			}
			
			GeneralHolder.gameObject.SetActive (true);
			
			_whichGeneral = 4;
			
		});
		
		this.BindButtonToHandler (General5Btn, () => {
			
			//int cslCount = Academy.cSelfLearnList.Count;
			for (var i = 0 ; i < GeneralList.Count ; i++){
				CreateSelfLearnItem (GeneralList[i]);
			}
			
			GeneralHolder.gameObject.SetActive (true);
			
			_whichGeneral = 5;
			
		});
		
		
		this.BindButtonToHandler (InputConfirmBtn, () => {
			
			/*
			this.ConferenceScreen.SoldierQuantity = int.Parse(SoldierQuantityInput.text);
			SoldierQuantityText.text = "兵數: " + SoldierQuantityInput.text;
			ExecuteSetSoldierData();
			SoldierQuantityPanel.gameObject.SetActive (false);
			*/
			
			//if(int.Parse(SoldierQuantityInput.text) > game.soldiers[this.ConferenceScreen.SoldierType].quantity)
			if(int.Parse(SoldierQuantityInput.text) > 3000 || int.Parse(SoldierQuantityInput.text) < 0)
			{
				Debug.Log ("Please enter again");
				QunatityErrorText.text = "請重新輸入 (0~3000)";
			}
			
			else
			{
				this.ConferenceScreen.SoldierQuantity = int.Parse(SoldierQuantityInput.text);
				SoldierQuantityText.text = "兵數: " + SoldierQuantityInput.text;
				ExecuteSetSoldierData();
				SoldierQuantityPanel.gameObject.SetActive (false);
			}
			
			
		});
		
    }
    
    public void AssignGeneral (Image icon)
    {
		Image image;
		Text iconText;
		
		switch (_whichGeneral)
		{
			
			case 1:
			General1.transform.FindChild("Image").GetComponent<Image>().sprite = icon.sprite;
			General1.transform.FindChild("Text").GetComponent<Text>().text = "";
			//iconText.text = null;
			break;
			case 2:
			General2.transform.FindChild("Image").GetComponent<Image>().sprite = icon.sprite;
			General2.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			case 3:
			General3.transform.FindChild("Image").GetComponent<Image>().sprite = icon.sprite;
			General3.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			case 4:
			General4.transform.FindChild("Image").GetComponent<Image>().sprite = icon.sprite;
			General4.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			case 5:
			General5.transform.FindChild("Image").GetComponent<Image>().sprite = icon.sprite;
			General5.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			default:
			General1.transform.FindChild("Image").GetComponent<Image>().sprite = icon.sprite;
			General1.transform.FindChild("Text").GetComponent<Text>().text = "";
			break;					  			
		}
		
		GeneralHolder.gameObject.SetActive (false);
    }
    
	public void CreateSelfLearnItem(General character)
	{
		var type = character.type;
		Debug.Log ("Type: " + type);
		AcademySelfLearn ss = Instantiate(Resources.Load("GeneralPrefab") as GameObject).GetComponent<AcademySelfLearn>();
		ss.transform.parent =  GeneralListHolder;
		
		ss.characterType = character.type;
		ss.characterId = character.id;
		ss.StudentPic =  imageDict[type];
		ss.StudentImageText.text = nameDict[type];
		//AcademySelfLearn.Students.Add (ss);
		
		ss.transform.localScale = Vector3.one;
		
		Debug.Log ("Gen Genereal from ConfrenceScreen");
	}
}

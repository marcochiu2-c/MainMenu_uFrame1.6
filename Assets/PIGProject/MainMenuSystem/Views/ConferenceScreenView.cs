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
	
	public Button SelectSoldier1Btn;
	public Button SelectSoldier2Btn;
	public Button SelectSoldier3Btn;
	public Button SelectSoldier4Btn;
	public Button SelectSoldier5Btn;
	
	public Button SoldierType1;
	public Button SoldierType2;
	public Button SoldierType3;
	public Button InputConfirmBtn;
	
	public Button General1Btn;
	public Button General2Btn;
	public Button General3Btn;
	public Button General4Btn;
	public Button General5Btn;
	
	public Button SSCloseBtn;
	public Button SQCloseBtn;
	
	//public Button BattleBtn;
	
	public Button CloseBtn;
	
	public Button GoToSchoolFieldBtn;
	
	public InputField SoldierQuantityInput;
	public Text SoldierQuantityText1;
	public Text SoldierQuantityText2;
	public Text SoldierQuantityText3;
	public Text SoldierQuantityText4;
	public Text SoldierQuantityText5;
	public Text QunatityErrorText;
	public Game game;
	
	public Text TotalSoldierQunatityText;
	public Text TotalGeneralText;
	public Text TotalPlayerText;
	public Text DeadSoldiersText;
	public Text AliveSoldiersText;
	public Text LoseGearText;
	public Text SilverFeatherGainText;
	public Text ResourceGainText;

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
	public UserViewModel LocalUser;	
	public int[] generalIDArray = new int[5];
	public int whichTeam;
	
	private Button _selectSoldier;
	private Text _soldierQuantityText = null;
	
	public int TotalExp = 5010732;
	
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as ConferenceScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		game = Game.Instance;
		LoadHeadPic headPic = LoadHeadPic.SetCharacters();
		LocalUser =  uFrameKernel.Container.Resolve<UserViewModel>("LocalUser");
		
		LocalUser.SetTeam = false;
		imageDict = headPic.imageDict;
		nameDict = headPic.nameDict;
		Debug.Log ("HeadPIG: " + headPic.SuenMo);
		//imageDict = headPic.imageDict;
		
		GeneralList = game.general;
		
		whichTeam = 0;
		
		LocalUser.UserLevel = CharacterPage.UserLevelCalculator(game.login.exp);
		int userLevel = LocalUser.UserLevel;
		
		Debug.Log ("UserLevel: " + userLevel);
		//Testing use
		//int userLevel = 25;
		LocalUser.TotalTeam = 1;
		
		if (userLevel > 10)
		{
			SelectSoldier2Btn.interactable = true;
			General2Btn.interactable = true;
			LocalUser.TotalTeam = 2;
		}
		
		if (userLevel > 20)
		{
			SelectSoldier3Btn.interactable = true;
			General3Btn.interactable = true;
			LocalUser.TotalTeam = 3;
		}
		
		if (userLevel > 30)
		{
			SelectSoldier4Btn.interactable = true;
			General4Btn.interactable = true;
			LocalUser.TotalTeam = 4;
		}
		
		if (userLevel > 40)
		{
			SelectSoldier5Btn.interactable = true;
			General5Btn.interactable = true;
			LocalUser.TotalTeam = 5;
		}
		
		//int expTest = Mathf.RoundToInt(40 * Mathf.Pow(1.1f, LocalUser.UserLevel) / TotalExp * 1000000);
		//Debug.Log ("Add Exp Trial: " + expTest);
		
		Debug.Log (LocalUser.Soldier == null ? "SoldierVM is null" : LocalUser.Soldier.Count.ToString());
		
		/*
		for (int i = 1; i <= LocalUser.TotalTeam; i++)
		{
			LocalUser.Soldier.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			LocalUser.Soldier[i - 1].Max_Health = 0;
			Debug.Log ("SoldierVM added");
			
			//Debug.Log (SoldierVM == null ? "SoldierVM is null" : SoldierVM[0].Movement + " and " + SoldierVM[0].Health + " and " + SoldierVM[0].Action);
		}
		*/
		
		
		for (var i = 0 ; i < GeneralList.Count ; i++){
			CreateSelfLearnItem (GeneralList[i]);
		}
		
		
		for(int i = 0; i < 5; i++)
		{
			if(game.teams[i].status == 1)
			{
				//Set General icon
			
				//Set Soldier Data
				switch (i)
				{
				case 0:
					_soldierQuantityText = SoldierQuantityText1;
					_selectSoldier = SelectSoldier1Btn;
					break;
				case 1:
					_soldierQuantityText = SoldierQuantityText2;
					_selectSoldier = SelectSoldier2Btn;
					break;
				case 2:
					_soldierQuantityText = SoldierQuantityText3;
					_selectSoldier = SelectSoldier3Btn;
					break;
				case 3:
					_soldierQuantityText = SoldierQuantityText4;
					_selectSoldier = SelectSoldier4Btn;					
					break;
				case 4:
					_soldierQuantityText = SoldierQuantityText5;
					_selectSoldier = SelectSoldier5Btn;					
					break;
				default:
					_soldierQuantityText = SoldierQuantityText1;
					_selectSoldier = SelectSoldier1Btn;
					break;
				}
				
				//game.teams[this.ConferenceScreen.Group - 1].soldierQuantity = int.Parse(SoldierQuantityInput.text);
				//game.teams[this.ConferenceScreen.Group - 1].soldierJson["type"].AsInt = this.ConferenceScreen.SoldierType - 1;
				if(game.teams[i].generalJson["type"] != null)
				{
					whichTeam = i + 1;
					AssignGeneral(game.teams[i].generalJson["type"].AsInt);
					whichTeam = 0;
				}
				_soldierQuantityText.text = "兵數: " + game.teams[i].soldierQuantity;
				
				switch (game.teams[i].soldierJson["type"].AsInt)
				{
				case 1:
					_selectSoldier.transform.GetChild(0).GetComponent<Text>().text = "兵種一";
					break;
				case 2:
					_selectSoldier.transform.GetChild(0).GetComponent<Text>().text = "兵種二";
					break;
				case 3:
					_selectSoldier.transform.GetChild(0).GetComponent<Text>().text = "兵種三";
					break;
				default:
					_selectSoldier.transform.GetChild(0).GetComponent<Text>().text = "兵種一";
					break;
				}
				
				//this.ConferenceScreen.SoldierQuantity = game.teams[i].soldierQuantity;
				//this.ConferenceScreen.SoldierType = game.teams[i].soldierJson["type"].AsInt;
				//ExecuteSetTeam();
				//initTeam(i);
			}
		}

		//this.ConferenceScreen.Group = 1;
		//AssignGeneral(GeneralList[0].type);
		//LocalUser.TotalTeam = 0;
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.ConferenceScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
		var evt = new RequestMainMenuScreenCommand();
		
		/*
		this.BindButtonToHandler (BattleBtn, () => {	
			ExecuteSetSoldierData();
		});
		*/
		
		this.BindButtonToHandler (GoToSchoolFieldBtn, () => {
			
			evt.ScreenType = typeof(SchoolFieldScreenViewModel);
			Publish(evt);
			
			QunatityErrorText.text = "";
			
			SelectSoldierPanel.gameObject.SetActive (false);
			SoldierQuantityPanel.gameObject.SetActive (false);
			GoToSchoolFieldBtn.gameObject.SetActive(false);
			
			ArmyAttack.gameObject.SetActive (false);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (false);
			
			GeneralHolder.gameObject.SetActive (false);
			
		});

		this.BindButtonToHandler (armyAttack, () => {
			ArmyAttack.gameObject.SetActive (true);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (false);
			
			GeneralList = game.general;
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
			
			
			TotalSoldierQunatityText.text = LocalUser.TotalSoldierQunatity.ToString();
			TotalGeneralText.text = LocalUser.TotalGeneral.ToString();
			TotalPlayerText.text = LocalUser.TotalPlayer.ToString();
			DeadSoldiersText.text = LocalUser.DeadSoldiers.ToString();
			AliveSoldiersText.text = LocalUser.AliveSoldiers.ToString();
			LoseGearText.text = LocalUser.LoseGear.ToString();
			SilverFeatherGainText.text = LocalUser.SilverFeatherGain.ToString();
			ResourceGainText.text = LocalUser.ResourceGain.ToString();
			
			
		});
		
		this.BindButtonToHandler (SelectSoldier1Btn, () => {
			_selectSoldier = SelectSoldier1Btn;
			_soldierQuantityText = SoldierQuantityText1;
			this.ConferenceScreen.Group = 1;
			
			SelectSoldierPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SelectSoldier2Btn, () => {
			_selectSoldier = SelectSoldier2Btn;
			_soldierQuantityText = SoldierQuantityText2;
			this.ConferenceScreen.Group = 2;
			
			SelectSoldierPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SelectSoldier3Btn, () => {
			_selectSoldier = SelectSoldier3Btn;
			_soldierQuantityText = SoldierQuantityText3;
			this.ConferenceScreen.Group = 3;
			
			SelectSoldierPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SelectSoldier4Btn, () => {
			_selectSoldier = SelectSoldier4Btn;
			_soldierQuantityText = SoldierQuantityText4;
			this.ConferenceScreen.Group = 4;
			
			SelectSoldierPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SelectSoldier5Btn, () => {
			_selectSoldier = SelectSoldier5Btn;
			_soldierQuantityText = SoldierQuantityText5;			
			this.ConferenceScreen.Group = 5;
			
			SelectSoldierPanel.gameObject.SetActive (true);
		});
		
		
		this.BindButtonToHandler (SSCloseBtn, () => {
			_selectSoldier = SelectSoldier1Btn;
			SelectSoldierPanel.gameObject.SetActive (false);
		});
		
		this.BindButtonToHandler (SoldierType1, () => {
			this.ConferenceScreen.SoldierType = 1;
			//ExecuteSetSoldierData();
			SelectSoldierPanel.gameObject.SetActive (false);
			_selectSoldier.GetComponentInChildren<Text>().text = "兵種一";
			SoldierQuantityPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SoldierType2, () => {
			this.ConferenceScreen.SoldierType = 2;
			//ExecuteSetSoldierData();
			SelectSoldierPanel.gameObject.SetActive (false);
			_selectSoldier.GetComponentInChildren<Text>().text = "兵種二";
			SoldierQuantityPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SoldierType3, () => {
			this.ConferenceScreen.SoldierType = 3;
			//ExecuteSetSoldierData();
			SelectSoldierPanel.gameObject.SetActive (false);
			_selectSoldier.GetComponentInChildren<Text>().text = "兵種三";
			SoldierQuantityPanel.gameObject.SetActive (true);
		});
		
		this.BindButtonToHandler (SQCloseBtn, () => {
			SelectSoldierPanel.gameObject.SetActive (true);
			SoldierQuantityPanel.gameObject.SetActive (false);
			
		});
		
		this.BindButtonToHandler (General1Btn, () => {
			
			GeneralHolder.gameObject.SetActive (true);
			
			this.ConferenceScreen.Group = 1;
			whichTeam = 1;
		});
		
		
		this.BindButtonToHandler (General2Btn, () => {
			
			GeneralHolder.gameObject.SetActive (true);
			
			this.ConferenceScreen.Group = 2;
			whichTeam = 2;
		});
		
		this.BindButtonToHandler (General3Btn, () => {
			
			GeneralHolder.gameObject.SetActive (true);
			
			this.ConferenceScreen.Group = 3;
			whichTeam = 3;
		});
		
		this.BindButtonToHandler (General4Btn, () => {
			
			GeneralHolder.gameObject.SetActive (true);
			
			this.ConferenceScreen.Group = 4;
			whichTeam = 4;
		});
		
		this.BindButtonToHandler (General5Btn, () => {
			
			
			GeneralHolder.gameObject.SetActive (true);
			
			this.ConferenceScreen.Group = 5;
			whichTeam = 5;
		});
		
		
		this.BindButtonToHandler (InputConfirmBtn, () => {
			
			/*
			this.ConferenceScreen.SoldierQuantity = int.Parse(SoldierQuantityInput.text);
			SoldierQuantityText.text = "兵數: " + SoldierQuantityInput.text;
			ExecuteSetSoldierData();
			SoldierQuantityPanel.gameObject.SetActive (false);
			*/
			
			//Debug.Log (game.weapon[5019].quantity);
			
			//init
			QunatityErrorText.text = "";
			
			//if(int.Parse(SoldierQuantityInput.text) > game.soldiers[this.ConferenceScreen.SoldierType].quantity)
			if(int.Parse(SoldierQuantityInput.text) > 3000 || int.Parse(SoldierQuantityInput.text) < 0)
			{
				Debug.Log ("Please enter again");
				QunatityErrorText.text = "請重新輸入 (0~3000)";
			}
			
			if(game.soldiers[this.ConferenceScreen.SoldierType - 1].attributes["weapon"].AsInt == 0)
			{
				Debug.Log("Enough Weapons");
			}
			
		    else if(game.weapon.Find ( x => x.type == game.soldiers[this.ConferenceScreen.SoldierType - 1].attributes["weapon"].AsInt).quantity < int.Parse(SoldierQuantityInput.text))	
			{
				Debug.Log ("Please enter again");
				QunatityErrorText.text = "武器數量不足, 可前往校場作為調整";
				GoToSchoolFieldBtn.gameObject.SetActive(true);
				return;
				
			}
			
			
			if(game.soldiers[this.ConferenceScreen.SoldierType - 1].attributes["armor"].AsInt == 0)
			{
				Debug.Log("No Armor");
			}
	
			else if(game.armor.Find ( x => x.type == game.soldiers[this.ConferenceScreen.SoldierType - 1].attributes["armor"].AsInt).quantity < int.Parse(SoldierQuantityInput.text))	
			{
				Debug.Log ("Please enter again");
				QunatityErrorText.text = "防具數量不足, 可前往校場作為調整";
				GoToSchoolFieldBtn.gameObject.SetActive(true);
				return;
			}
			
			
			if(game.soldiers[this.ConferenceScreen.SoldierType - 1].attributes["shield"].AsInt == 0)
			{
				Debug.Log("No Shield");
			}
			
			else if(game.shield.Find ( x => x.type == game.soldiers[this.ConferenceScreen.SoldierType - 1].attributes["shield"].AsInt).quantity	< int.Parse(SoldierQuantityInput.text))
			{
				Debug.Log ("Please enter again");
				QunatityErrorText.text = "盾數量不足, 可前往校場作為調整";
				GoToSchoolFieldBtn.gameObject.SetActive(true);
				return;
				
			}
			
			//need to check condition
			//LocalUser.TotalTeam++;
			/*
			LocalUser.TotalSoldierQunatity += int.Parse(SoldierQuantityInput.text);
			LocalUser.TotalGeneral = 1;
			LocalUser.TotalPlayer = 1;
			*/
			
			this.ConferenceScreen.SoldierQuantity = int.Parse(SoldierQuantityInput.text);
			_soldierQuantityText.text = "兵數: " + SoldierQuantityInput.text;
			ExecuteSetSoldierData();
			SoldierQuantityPanel.gameObject.SetActive (false);
			LocalUser.SetTeam = true;
			
			
			//game.teams[this.ConferenceScreen.Group - 1].generalId = this.ConferenceScreen.;
			//game.teams[this.ConferenceScreen.Group - 1].generalJson["type"].AsInt = ;
			//game.teams[this.ConferenceScreen.Group - 1].generalJson["attributes"] = ;
			game.teams[this.ConferenceScreen.Group - 1].soldierQuantity = int.Parse(SoldierQuantityInput.text);
			game.teams[this.ConferenceScreen.Group - 1].soldierJson["type"].AsInt = this.ConferenceScreen.SoldierType;
			game.teams[this.ConferenceScreen.Group - 1].status = 1;
			game.teams[this.ConferenceScreen.Group - 1].UpdateObject();
			
			Debug.Log ("game.teams updated to server");
		});
		
		this.BindButtonToHandler (CloseBtn, () => {
			
			SelectSoldierPanel.gameObject.SetActive (false);
			SoldierQuantityPanel.gameObject.SetActive (false);
			GoToSchoolFieldBtn.gameObject.SetActive(false);
			
			ArmyAttack.gameObject.SetActive (false);
			ArmyGarrison.gameObject.SetActive (false);
			MilitaryAdviser.gameObject.SetActive (false);
			DefensiveLinup.gameObject.SetActive (false);
			Standings.gameObject.SetActive (false);
			
			GeneralHolder.gameObject.SetActive (false);
			
		});
		
    }
    
    public void AssignGeneral (int type)
    {
		Image image;
		Text iconText;
		
		//if (whichTeam != 0)
		//	this.ConferenceScreen.Group = whichTeam;
			
		switch (whichTeam)
		{
			
			case 1:
			General1.transform.FindChild("Image").GetComponent<Image>().sprite = imageDict[type];
			General1.transform.FindChild("Text").GetComponent<Text>().text = "";
			//iconText.text = null;
			break;
			case 2:
			General2.transform.FindChild("Image").GetComponent<Image>().sprite = imageDict[type];
			General2.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			case 3:
			General3.transform.FindChild("Image").GetComponent<Image>().sprite = imageDict[type];
			General3.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			case 4:
			General4.transform.FindChild("Image").GetComponent<Image>().sprite = imageDict[type];
			General4.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			case 5:
			General5.transform.FindChild("Image").GetComponent<Image>().sprite = imageDict[type];
			General5.transform.FindChild("Text").GetComponent<Text>().text = "";
			  break;
			default:
			General1.transform.FindChild("Image").GetComponent<Image>().sprite = imageDict[type];
			General1.transform.FindChild("Text").GetComponent<Text>().text = "";
			break;					  			
		}			
		
		GeneralHolder.gameObject.SetActive (false);
		
		game.teams[whichTeam - 1].generalJson["type"].AsInt = type;
		game.teams[whichTeam - 1].status = 1;
		game.teams[whichTeam - 1].UpdateObject();
		
		LocalUser.generalImageType[whichTeam - 1] = type ;
    }
    
	public void CreateSelfLearnItem(General character)
	{
		var type = character.type;
		//Debug.Log ("Type: " + type);
		GeneralAssign generalAssign = Instantiate(Resources.Load("GeneralPrefab") as GameObject).GetComponent<GeneralAssign>();
		generalAssign.transform.parent = GeneralListHolder;
		generalAssign.general_id = character.id;
		generalAssign.general_type = character.type;
		generalAssign.generalIcon.sprite = imageDict[type];
        generalAssign.generalName.text = nameDict[type];
        generalAssign.generalIQ.text = "IQ: " + character.attributes["IQ"];
		generalAssign.generalLv.text = "LV: " + character.attributes["Rank"];
		
		generalAssign.transform.localScale = Vector3.one;		
		
	}
	
	public void PopUp()
	{
		Publish(new NotifyCommand()
		{
			Message = "將士已編成到隊伍"
		});
	}
}

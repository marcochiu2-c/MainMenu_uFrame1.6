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
using DG.Tweening;


public class MainGameRootView : MainGameRootViewBase {
    
	public Text gameOverText;
	public GSHexGridManager gSHexGridManager;
	public UserViewModel LocalUser;
	
	public Button InfoButton;
	public Button InfoCloseButton;
	public Button InfoAtkButton;
	public Button InfoMoveButton;
	public Button InfoMissionButton;
	public Button StartBattleButton;
	public Button LeaveButton;
	public Button Leave2Button;

	public Button SlowButton; 
	public Button NormalButton; 
	public Button FastButton;

	public Button AttackButton;
	public Button AssaultButton;
	public Button FeintButton;
	public Button PinButton;
	public Button YawpButton;
	public Button SearchButton;
	public Button AATKButton;
	public Button StandByButton;
	
	public Button DialogueTestButton;

	public GameObject InfoPanel;
	public GameObject BlockPanel;
	public GameObject ExpPanel;
	public GameObject SilverFeatherPanel;
	public GameObject ResourcePanel;
	public TextAsset atkInfo;
	public TextAsset moveInfo;
	public TextAsset missionInfo;
	public Text InfoText;
	
	private Button _copyInfoButton;
	
	public Slider loadingBar;
	public GameObject loadingImage;
	public GameObject beginnerGuide;
	public GameObject guideArrow;
	public Dictionary<int,Sprite> imageDict;
	public Image[] bottomGeneralIcon = new Image[5];
	public SpriteRenderer[] GeneralSprite = new SpriteRenderer[5];
	public Button[] SoldierBtn = new Button[5];
	
	private AsyncOperation _async;

	public List<SoldierViewModel> SoldierVM = new List<SoldierViewModel>();
	public List<SoldierView> SoldierV = new List<SoldierView>();
	
	public Game game;
	public int TotalExp = 5010732;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
		//gameOverText = GameObject.Find("Text_GameOver").GetComponent<Text>();
		gameOverText.gameObject.SetActive(false);
		//TextAsset atkInfo = Resources.Load<TextAsset> ("ATK_Info");
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as MainGameRootViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		InfoText.text = missionInfo.text;
		game = Game.Instance;
		
		gSHexGridManager = GameObject.Find("HexMapGrid").GetComponent<GSHexGridManager>();
		LocalUser =  uFrameKernel.Container.Resolve<UserViewModel>("LocalUser");
		
		//get the ImageDict
		LoadHeadPic headPic = LoadHeadPic.SetCharacters();
		imageDict = headPic.imageDict;
		
		for (int i = 1; i <= LocalUser.TotalTeam ; i++) 
		{
			SoldierVM.Add(uFrameKernel.Container.Resolve<SoldierViewModel>("Soldier" + i));
			SoldierV.Add(uFrameKernel.Container.Resolve<SoldierView>("Soldier" + i));
			
			Debug.Log ("ImageType: " + LocalUser.generalImageType[i - 1]);
			bottomGeneralIcon[i - 1].sprite = imageDict[LocalUser.generalImageType[i - 1]];
			GeneralSprite[i - 1].sprite = imageDict[LocalUser.generalImageType[i - 1]];
			SoldierBtn[i - 1].gameObject.SetActive(true);	
		}
		
		//beginnerGuide = GameObject.Find("BeginnerGuide");
		BeginnerGuide();
	}
	
	public override void Bind() {
        base.Bind();
        // Use this.MainGameRoot to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
        
        //InfoPanel
		this.BindButtonToHandler(InfoButton, () => {
		
			if(InfoPanel.activeSelf == false)
			{
				InfoPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad).OnStart(() =>  InfoPanel.SetActive(true));
				BlockPanel.SetActive(true);
			}
				
			else
			{
				InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
				BlockPanel.SetActive(false);	
			}
			/*
			Publish(new NotifyCommand(){
				Message = atkInfo.text 
		    });
		    */
		});
		
		this.BindButtonToHandler(_copyInfoButton, () => {
			
			if(InfoPanel.activeSelf == false)
			{
				InfoPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad).OnStart(() =>  InfoPanel.SetActive(true));
				BlockPanel.SetActive(true);
			}
			
			else
			{
				InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
				BlockPanel.SetActive(false);	
			}
			
			//beginnerGuide.SetActive(false);
		
		});
		
		this.BindButtonToHandler(InfoCloseButton, () => { 
			InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
			BlockPanel.SetActive(false);
			beginnerGuide.SetActive(false);
		});
		
		this.BindButtonToHandler(StartBattleButton, () => { 
			InfoPanel.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad).OnComplete(() => InfoPanel.SetActive(false));
			BlockPanel.SetActive(false);
			beginnerGuide.SetActive(false);
		});
		
		this.BindButtonToHandler(LeaveButton, () => { 
			var evt = new RequestMainMenuScreenCommand();
			
			/*
			Publish(new UnloadSceneCommand()
			{
				SceneName = "MainGameScene"
			});
			
			Application.LoadLevel("MainMenuScene");
			
			//Publish(new LoadSceneCommand()
			//{
			//	SceneName = "MainMenuScene"
			//});
		    */
			for(int i = 0; i < SoldierVM.Count ; i++)
			{
				SoldierVM[i].Counter = 0;
				SoldierVM[i].playlist.Clear ();
			}
				
			LocalUser.ScreenState = ScreenState.MainGame;
									
			loadingImage.SetActive (true);
			StartCoroutine (LoadLevelWithBar ("MainMenuScene"));
			
			evt.ScreenType = typeof(MenuScreenViewModel);
			Publish(evt);
			
		});
		
		this.BindButtonToHandler(Leave2Button, () => { 
			var evt = new RequestMainMenuScreenCommand();
			
			/*
			Publish(new UnloadSceneCommand()
			{
				SceneName = "MainGameScene"
			});
			
			Application.LoadLevel("MainMenuScene");
			
			//Publish(new LoadSceneCommand()
			//{
			//	SceneName = "MainMenuScene"
			//});
		    */
			for(int i = 0; i < SoldierVM.Count ; i++)
			{
				SoldierVM[i].Counter = 0;
				SoldierVM[i].playlist.Clear ();
			}
			
			LocalUser.ScreenState = ScreenState.MainGame;
			
			loadingImage.SetActive (true);
			StartCoroutine (LoadLevelWithBar ("MainMenuScene"));
			
			evt.ScreenType = typeof(MenuScreenViewModel);
			Publish(evt);
		});

		this.BindButtonToHandler(InfoAtkButton, () => { 
			InfoText.text = atkInfo.text;
		});

		this.BindButtonToHandler(InfoMoveButton, () => { 
			InfoText.text = moveInfo.text;
		});

		this.BindButtonToHandler(InfoMissionButton, () => { 
			InfoText.text = missionInfo.text;
		});

		//Batte Start la
		//MoveStyle
		this.BindButtonToHandler(SlowButton, () => {
			SoldierVM[gSHexGridManager.sNum].Movement = MoveStyle.SLOW;
			//this.Soldier.SoldierState = SoldierState.ATTACK;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});	
		this.BindButtonToHandler(NormalButton, () => {
			SoldierVM[gSHexGridManager.sNum].Movement = MoveStyle.NORMAL;
			//this.Soldier.SoldierState = SoldierState.ATTACK;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});	
		this.BindButtonToHandler(FastButton, () => {
			SoldierVM[gSHexGridManager.sNum].Movement= MoveStyle.FAST;
			//this.Soldier.SoldierState = SoldierState.ATTACK;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});

		//ActionStyle
		this.BindButtonToHandler(AssaultButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.ASSAULT;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			//ExecuteChangeActionStyle();
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});
		this.BindButtonToHandler(AttackButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.ATTACK;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});
		this.BindButtonToHandler(FeintButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.FEINT;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});
		this.BindButtonToHandler(PinButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.PIN;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});

		this.BindButtonToHandler(YawpButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.YAWP;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});
		this.BindButtonToHandler(SearchButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.SEARCH;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});//

		this.BindButtonToHandler(AATKButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.A_ATK;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});

		this.BindButtonToHandler(StandByButton, () => {
			SoldierVM[gSHexGridManager.sNum].Action = ActionStyle.STANDBY;
			//this.Soldier.SoldierState = SoldierState.MOVE;
			gSHexGridManager.selectPoint = true;
			gSHexGridManager.MoveOrAttackPointSelected();
		});
		
		this.BindButtonToHandler(DialogueTestButton, () => {
			//TODO
			Publish(new DialogueCommand()
			        {
				ConversationName = "Ch0_1"
			});
		});
		
    }

    public override void GameStateChanged(GameState gameState) {
    
		if (gameState == GameState.Playing) return;
		
		if (this.MainGameRoot.EnemyCount <= 0)
		{
			Debug.Log ("You Win");
			gameOverText.text = "勝利";
			gameOverText.gameObject.SetActive(true);
			
			ExecuteGameOver();
			
			int expAdd = Mathf.RoundToInt(40 * Mathf.Pow(1.1f, LocalUser.UserLevel - 1) / TotalExp * 1000000);
			game.login.exp += expAdd;
			//SilverFeather
			game.wealth[0].Add(expAdd * 9);
			//Resource
			game.wealth[2].Add (expAdd  * 400);
			game.login.UpdateObject();
			
			ExpPanel.gameObject.SetActive(true);
			ExpPanel.transform.FindChild("Text").GetComponent<Text>().text = "所得經驗： " + expAdd;
			
			SilverFeatherPanel.gameObject.SetActive(true);
			SilverFeatherPanel.transform.FindChild("Text").GetComponent<Text>().text = "所得銀羽： " + expAdd * 9;
			
			ResourcePanel.gameObject.SetActive(true);
			ResourcePanel.transform.FindChild("Text").GetComponent<Text>().text = "所得物資： " + expAdd * 400;
			
			Debug.Log("Add Exp: " + expAdd);
			Debug.Log("User Exp: " + game.login.exp);
			
			
		}
		
		//else if(this.MainGameRoot.SoldierCount == 0)
		else
		{
			Debug.Log ("You Lose");
			gameOverText.text = "落敗";
			gameOverText.gameObject.SetActive(true);
			ExecuteGameOver();
		}
    }
    
	IEnumerator LoadLevelWithBar (string level)
	{
		_async = Application.LoadLevelAsync(level);
		while(!_async.isDone)
		{
			loadingBar.value=_async.progress;
			yield return null;
		}
		
		//UserManagementService.loadDB();
	}
	
	/// <summary>
	/// Init all Behavior in the Grid
	/// </summary>
	public void BeginnerGuide()
	{

		//GameObject guideArrow = GameObject.Find ("GuideArrow");
		guideArrow.transform.localPosition = InfoButton.transform.localPosition + new Vector3(100, 0, 0);
		//Test only
		_copyInfoButton = Instantiate(InfoButton) as Button;
		
		_copyInfoButton.transform.parent = InfoButton.transform.parent; 
		
		_copyInfoButton.transform.localPosition = InfoButton.transform.localPosition ; 
		_copyInfoButton.transform.localRotation = InfoButton.transform.localRotation ;  
		_copyInfoButton.transform.localScale    = InfoButton.transform.localScale; 
		
		_copyInfoButton.transform.parent = beginnerGuide.transform;
		
		guideArrow.transform.DOLocalMoveX(-450f, 0.8f).SetLoops(-1, LoopType.Yoyo);
		
		BlockPanel.SetActive(true);
		
		/*
		GameObject movePanel = GameObject.Find("MovePanel");
		GameObject actionPanel = GameObject.Find ("ActionPanel");
		GameObject flowPanel = GameObject.Find("FlowPanel");
		
		GameObject copyMovePanel = Instantiate(movePanel);
		GameObject copyActionPanel = Instantiate(actionPanel);
		GameObject copyFlowPanel = Instantiate(flowPanel);
		
		copyMovePanel.transform.parent = movePanel.transform.parent; 
		copyActionPanel.transform.parent = movePanel.transform.parent;  
		copyFlowPanel.transform.parent = movePanel.transform.parent;  

		copyMovePanel.transform.localPosition = movePanel.transform.localPosition ;  
		copyMovePanel.transform.localRotation = movePanel.transform.localRotation ;  
		copyMovePanel.transform.localScale    = movePanel.transform.localScale ; 
		
		copyActionPanel.transform.localPosition = actionPanel.transform.localPosition ;  
		copyActionPanel.transform.localRotation = actionPanel.transform.localRotation ;  
		copyActionPanel.transform.localScale    = actionPanel.transform.localScale ;  
		
		copyFlowPanel.transform.localPosition = flowPanel.transform.localPosition ;  
		copyFlowPanel.transform.localRotation = flowPanel.transform.localRotation ;  
		copyFlowPanel.transform.localScale    = flowPanel.transform.localScale ;  
		 
		copyMovePanel.transform.parent = beginnerGuide.transform;
		copyActionPanel.transform.parent = beginnerGuide.transform; 
		copyFlowPanel.transform.parent = beginnerGuide.transform;
		*/ 
	}
}

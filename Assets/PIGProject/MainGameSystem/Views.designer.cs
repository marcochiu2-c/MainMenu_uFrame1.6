// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

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


public class MainGameRootViewBase : uFrame.MVVM.ViewBase {
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public String _State;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public String _HexGridMatching;
    
    [UFToggleGroup("State")]
    [UnityEngine.HideInInspector()]
    public bool _BindState = true;
    
    [UFGroup("State")]
    [UnityEngine.SerializeField()]
    [UnityEngine.HideInInspector()]
    [UnityEngine.Serialization.FormerlySerializedAsAttribute("_StateonlyWhenChanged")]
    protected bool _StateOnlyWhenChanged;
    
    public override string DefaultIdentifier {
        get {
            return "MainGame";
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(MainGameRootViewModel);
        }
    }
    
    public MainGameRootViewModel MainGameRoot {
        get {
            return (MainGameRootViewModel)ViewModelObject;
        }
    }
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as MainGameRootViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        var maingamerootview = ((MainGameRootViewModel)model);
        maingamerootview.State = this._State;
        maingamerootview.HexGridMatching = this._HexGridMatching;
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.MainGameRoot to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
        if (_BindState) {
            this.BindProperty(this.MainGameRoot.StateProperty, this.StateChanged, _StateOnlyWhenChanged);
        }
    }
    
    public virtual void StateChanged(String arg1) {
    }
    
    public virtual void ExecuteGoToMenu() {
        MainGameRoot.GoToMenu.OnNext(new GoToMenuCommand() { Sender = MainGameRoot });
    }
    
    public virtual void ExecutePlay() {
        MainGameRoot.Play.OnNext(new PlayCommand() { Sender = MainGameRoot });
    }
    
    public virtual void ExecuteGameOver() {
        MainGameRoot.GameOver.OnNext(new GameOverCommand() { Sender = MainGameRoot });
    }
    
    public virtual void ExecuteGoToMenu(GoToMenuCommand command) {
        command.Sender = MainGameRoot;
        MainGameRoot.GoToMenu.OnNext(command);
    }
    
    public virtual void ExecutePlay(PlayCommand command) {
        command.Sender = MainGameRoot;
        MainGameRoot.Play.OnNext(command);
    }
    
    public virtual void ExecuteGameOver(GameOverCommand command) {
        command.Sender = MainGameRoot;
        MainGameRoot.GameOver.OnNext(command);
    }
}

public class SoldierViewBase : EntityView {
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public SoldierState _SoldierState;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public PlayList _PlayList;
    
    [UFToggleGroup("SoldierState")]
    [UnityEngine.HideInInspector()]
    public bool _BindSoldierState = true;
    
    [UFGroup("SoldierState")]
    [UnityEngine.SerializeField()]
    [UnityEngine.HideInInspector()]
    [UnityEngine.Serialization.FormerlySerializedAsAttribute("_SoldierStateonlyWhenChanged")]
    protected bool _SoldierStateOnlyWhenChanged;
    
    public override string DefaultIdentifier {
        get {
            return "Soldier1";
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(SoldierViewModel);
        }
    }
    
    public SoldierViewModel Soldier {
        get {
            return (SoldierViewModel)ViewModelObject;
        }
    }
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as SoldierViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        var soldierview = ((SoldierViewModel)model);
        soldierview.SoldierState = this._SoldierState;
        soldierview.PlayList = this._PlayList;
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.Soldier to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
        if (_BindSoldierState) {
            this.BindProperty(this.Soldier.SoldierStateProperty, this.SoldierStateChanged, _SoldierStateOnlyWhenChanged);
        }
    }
    
    public virtual void SoldierStateChanged(SoldierState arg1) {
    }
    
    public virtual void ExecuteChangeActionStyle() {
        Soldier.ChangeActionStyle.OnNext(new ChangeActionStyleCommand() { Sender = Soldier });
    }
    
    public virtual void ExecuteChangeMoveStyle() {
        Soldier.ChangeMoveStyle.OnNext(new ChangeMoveStyleCommand() { Sender = Soldier });
    }
    
    public virtual void ExecuteChangeQuantity() {
        Soldier.ChangeQuantity.OnNext(new ChangeQuantityCommand() { Sender = Soldier });
    }
    
    public virtual void ExecutePlayAction() {
        Soldier.PlayAction.OnNext(new PlayActionCommand() { Sender = Soldier });
    }
    
    public virtual void ExecuteChangeActionStyle(ChangeActionStyleCommand command) {
        command.Sender = Soldier;
        Soldier.ChangeActionStyle.OnNext(command);
    }
    
    public virtual void ExecuteChangeMoveStyle(ChangeMoveStyleCommand command) {
        command.Sender = Soldier;
        Soldier.ChangeMoveStyle.OnNext(command);
    }
    
    public virtual void ExecuteChangeQuantity(ChangeQuantityCommand command) {
        command.Sender = Soldier;
        Soldier.ChangeQuantity.OnNext(command);
    }
    
    public virtual void ExecutePlayAction(PlayActionCommand command) {
        command.Sender = Soldier;
        Soldier.PlayAction.OnNext(command);
    }
}

public class EnemyViewBase : EntityView {
    
    public override string DefaultIdentifier {
        get {
            return "Enemy1";
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(EnemyViewModel);
        }
    }
    
    public EnemyViewModel Enemy {
        get {
            return (EnemyViewModel)ViewModelObject;
        }
    }
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as EnemyViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.Enemy to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
    }
}

public class EntityViewBase : uFrame.MVVM.ViewBase {
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _Health;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _Max_Health;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _AttackSpeed;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public MoveStyle _Movement;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _Power;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Boolean _isAttack;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public ActionStyle _Action;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _MAXROUNDS;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public String _Name;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _Physique;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _HitPoint;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _WeaponProficieny;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _Dodge;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _Hurt;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _Dead;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _InitialMorale;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _Prestige;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Boolean _DEBUG;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _counter;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _Counter;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _UpdatePerRound;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _ElementsPerSecond;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _WarTimeLimitInSecond;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Single _starttime;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Boolean _TimeStarted;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _WeaponProficiency;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public Int32 _moraleStandard;
    
    [UnityEngine.SerializeField()]
    [UFGroup("View Model Properties")]
    [UnityEngine.HideInInspector()]
    public uFrame.MVVM.ViewBase _Opponent;
    
    public override string DefaultIdentifier {
        get {
            return base.DefaultIdentifier;
        }
    }
    
    public override System.Type ViewModelType {
        get {
            return typeof(EntityViewModel);
        }
    }
    
    public EntityViewModel Entity {
        get {
            return (EntityViewModel)ViewModelObject;
        }
    }
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as EntityViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
        var entityview = ((EntityViewModel)model);
        entityview.Health = this._Health;
        entityview.Max_Health = this._Max_Health;
        entityview.AttackSpeed = this._AttackSpeed;
        entityview.Movement = this._Movement;
        entityview.Power = this._Power;
        entityview.isAttack = this._isAttack;
        entityview.Action = this._Action;
        entityview.MAXROUNDS = this._MAXROUNDS;
        entityview.Name = this._Name;
        entityview.Physique = this._Physique;
        entityview.HitPoint = this._HitPoint;
        entityview.WeaponProficieny = this._WeaponProficieny;
        entityview.Dodge = this._Dodge;
        entityview.Hurt = this._Hurt;
        entityview.Dead = this._Dead;
        entityview.InitialMorale = this._InitialMorale;
        entityview.Prestige = this._Prestige;
        entityview.DEBUG = this._DEBUG;
        entityview.counter = this._counter;
        entityview.Counter = this._Counter;
        entityview.UpdatePerRound = this._UpdatePerRound;
        entityview.ElementsPerSecond = this._ElementsPerSecond;
        entityview.WarTimeLimitInSecond = this._WarTimeLimitInSecond;
        entityview.starttime = this._starttime;
        entityview.TimeStarted = this._TimeStarted;
        entityview.WeaponProficiency = this._WeaponProficiency;
        entityview.moraleStandard = this._moraleStandard;
        entityview.Opponent = this._Opponent == null ? null :  ViewService.FetchViewModel(this._Opponent) as EntityViewModel;
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.Entity to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
    }
}

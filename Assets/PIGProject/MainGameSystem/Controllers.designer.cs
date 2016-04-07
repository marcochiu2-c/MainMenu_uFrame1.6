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
using UniRx;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.Serialization;


public class MainGameRootControllerBase : uFrame.MVVM.Controller {
    
    private uFrame.MVVM.IViewModelManager _MainGameRootViewModelManager;
    
    private EnemyViewModel _Enemy1;
    
    private EnemyViewModel _Enemy2;
    
    private EnemyViewModel _Enemy3;
    
    private EnemyViewModel _Enemy4;
    
    private EnemyViewModel _Enemy5;
    
    private SoldierViewModel _Soldier1;
    
    private SoldierViewModel _Soldier2;
    
    private SoldierViewModel _Soldier3;
    
    private SoldierViewModel _Soldier4;
    
    private SoldierViewModel _Soldier5;
    
    private MainGameRootViewModel _MainGameRoot;
    
    [uFrame.IOC.InjectAttribute("MainGameRoot")]
    public uFrame.MVVM.IViewModelManager MainGameRootViewModelManager {
        get {
            return _MainGameRootViewModelManager;
        }
        set {
            _MainGameRootViewModelManager = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy1")]
    public EnemyViewModel Enemy1 {
        get {
            return _Enemy1;
        }
        set {
            _Enemy1 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy2")]
    public EnemyViewModel Enemy2 {
        get {
            return _Enemy2;
        }
        set {
            _Enemy2 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy3")]
    public EnemyViewModel Enemy3 {
        get {
            return _Enemy3;
        }
        set {
            _Enemy3 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy4")]
    public EnemyViewModel Enemy4 {
        get {
            return _Enemy4;
        }
        set {
            _Enemy4 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy5")]
    public EnemyViewModel Enemy5 {
        get {
            return _Enemy5;
        }
        set {
            _Enemy5 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier1")]
    public SoldierViewModel Soldier1 {
        get {
            return _Soldier1;
        }
        set {
            _Soldier1 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier2")]
    public SoldierViewModel Soldier2 {
        get {
            return _Soldier2;
        }
        set {
            _Soldier2 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier3")]
    public SoldierViewModel Soldier3 {
        get {
            return _Soldier3;
        }
        set {
            _Soldier3 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier4")]
    public SoldierViewModel Soldier4 {
        get {
            return _Soldier4;
        }
        set {
            _Soldier4 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier5")]
    public SoldierViewModel Soldier5 {
        get {
            return _Soldier5;
        }
        set {
            _Soldier5 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("MainGameRoot")]
    public MainGameRootViewModel MainGameRoot {
        get {
            return _MainGameRoot;
        }
        set {
            _MainGameRoot = value;
        }
    }
    
    public IEnumerable<MainGameRootViewModel> MainGameRootViewModels {
        get {
            return MainGameRootViewModelManager.OfType<MainGameRootViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeMainGameRoot(((MainGameRootViewModel)(viewModel)));
    }
    
    public virtual MainGameRootViewModel CreateMainGameRoot() {
        return ((MainGameRootViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new MainGameRootViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeMainGameRoot(MainGameRootViewModel viewModel) {
        // This is called when a MainGameRootViewModel is created
        viewModel.GoToMenu.Action = this.GoToMenuHandler;
        viewModel.Play.Action = this.PlayHandler;
        viewModel.GameOver.Action = this.GameOverHandler;
        MainGameRootViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        MainGameRootViewModelManager.Remove(viewModel);
    }
    
    public virtual void GoToMenu(MainGameRootViewModel viewModel) {
    }
    
    public virtual void Play(MainGameRootViewModel viewModel) {
    }
    
    public virtual void GameOver(MainGameRootViewModel viewModel) {
    }
    
    public virtual void GoToMenuHandler(GoToMenuCommand command) {
        this.GoToMenu(command.Sender as MainGameRootViewModel);
    }
    
    public virtual void PlayHandler(PlayCommand command) {
        this.Play(command.Sender as MainGameRootViewModel);
    }
    
    public virtual void GameOverHandler(GameOverCommand command) {
        this.GameOver(command.Sender as MainGameRootViewModel);
    }
}

public class SoldierControllerBase : EntityController {
    
    private uFrame.MVVM.IViewModelManager _SoldierViewModelManager;
    
    [uFrame.IOC.InjectAttribute("Soldier")]
    public uFrame.MVVM.IViewModelManager SoldierViewModelManager {
        get {
            return _SoldierViewModelManager;
        }
        set {
            _SoldierViewModelManager = value;
        }
    }
    
    public IEnumerable<SoldierViewModel> SoldierViewModels {
        get {
            return SoldierViewModelManager.OfType<SoldierViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeSoldier(((SoldierViewModel)(viewModel)));
    }
    
    public virtual SoldierViewModel CreateSoldier() {
        return ((SoldierViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new SoldierViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeSoldier(SoldierViewModel viewModel) {
        // This is called when a SoldierViewModel is created
        viewModel.ChangeActionStyle.Action = this.ChangeActionStyleHandler;
        SoldierViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        SoldierViewModelManager.Remove(viewModel);
    }
    
    public virtual void ChangeActionStyle(SoldierViewModel viewModel) {
    }
    
    public virtual void ChangeActionStyleHandler(ChangeActionStyleCommand command) {
        this.ChangeActionStyle(command.Sender as SoldierViewModel);
    }
}

public class EnemyControllerBase : EntityController {
    
    private uFrame.MVVM.IViewModelManager _EnemyViewModelManager;
    
    [uFrame.IOC.InjectAttribute("Enemy")]
    public uFrame.MVVM.IViewModelManager EnemyViewModelManager {
        get {
            return _EnemyViewModelManager;
        }
        set {
            _EnemyViewModelManager = value;
        }
    }
    
    public IEnumerable<EnemyViewModel> EnemyViewModels {
        get {
            return EnemyViewModelManager.OfType<EnemyViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeEnemy(((EnemyViewModel)(viewModel)));
    }
    
    public virtual EnemyViewModel CreateEnemy() {
        return ((EnemyViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new EnemyViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeEnemy(EnemyViewModel viewModel) {
        // This is called when a EnemyViewModel is created
        EnemyViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        EnemyViewModelManager.Remove(viewModel);
    }
}

public class EntityControllerBase : uFrame.MVVM.Controller {
    
    private uFrame.MVVM.IViewModelManager _EntityViewModelManager;
    
    private EnemyViewModel _Enemy1;
    
    private EnemyViewModel _Enemy2;
    
    private EnemyViewModel _Enemy3;
    
    private EnemyViewModel _Enemy4;
    
    private EnemyViewModel _Enemy5;
    
    private SoldierViewModel _Soldier1;
    
    private SoldierViewModel _Soldier2;
    
    private SoldierViewModel _Soldier3;
    
    private SoldierViewModel _Soldier4;
    
    private SoldierViewModel _Soldier5;
    
    private MainGameRootViewModel _MainGameRoot;
    
    [uFrame.IOC.InjectAttribute("Entity")]
    public uFrame.MVVM.IViewModelManager EntityViewModelManager {
        get {
            return _EntityViewModelManager;
        }
        set {
            _EntityViewModelManager = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy1")]
    public EnemyViewModel Enemy1 {
        get {
            return _Enemy1;
        }
        set {
            _Enemy1 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy2")]
    public EnemyViewModel Enemy2 {
        get {
            return _Enemy2;
        }
        set {
            _Enemy2 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy3")]
    public EnemyViewModel Enemy3 {
        get {
            return _Enemy3;
        }
        set {
            _Enemy3 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy4")]
    public EnemyViewModel Enemy4 {
        get {
            return _Enemy4;
        }
        set {
            _Enemy4 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy5")]
    public EnemyViewModel Enemy5 {
        get {
            return _Enemy5;
        }
        set {
            _Enemy5 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier1")]
    public SoldierViewModel Soldier1 {
        get {
            return _Soldier1;
        }
        set {
            _Soldier1 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier2")]
    public SoldierViewModel Soldier2 {
        get {
            return _Soldier2;
        }
        set {
            _Soldier2 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier3")]
    public SoldierViewModel Soldier3 {
        get {
            return _Soldier3;
        }
        set {
            _Soldier3 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier4")]
    public SoldierViewModel Soldier4 {
        get {
            return _Soldier4;
        }
        set {
            _Soldier4 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier5")]
    public SoldierViewModel Soldier5 {
        get {
            return _Soldier5;
        }
        set {
            _Soldier5 = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("MainGameRoot")]
    public MainGameRootViewModel MainGameRoot {
        get {
            return _MainGameRoot;
        }
        set {
            _MainGameRoot = value;
        }
    }
    
    public IEnumerable<EntityViewModel> EntityViewModels {
        get {
            return EntityViewModelManager.OfType<EntityViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeEntity(((EntityViewModel)(viewModel)));
    }
    
    public virtual EntityViewModel CreateEntity() {
        return ((EntityViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new EntityViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeEntity(EntityViewModel viewModel) {
        // This is called when a EntityViewModel is created
        viewModel.ChangeBattleState.Action = this.ChangeBattleStateHandler;
        viewModel.ChangeHealth.Action = this.ChangeHealthHandler;
        EntityViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        EntityViewModelManager.Remove(viewModel);
    }
    
    public virtual void ChangeBattleState(EntityViewModel viewModel) {
    }
    
    public virtual void ChangeHealth(EntityViewModel viewModel) {
    }
    
    public virtual void ChangeBattleStateHandler(ChangeBattleStateCommand command) {
        this.ChangeBattleState(command.Sender as EntityViewModel);
    }
    
    public virtual void ChangeHealthHandler(ChangeHealthCommand command) {
        this.ChangeHealth(command.Sender as EntityViewModel);
    }
}

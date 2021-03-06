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
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class MainMenuRootControllerBase : uFrame.MVVM.Controller {
    
    private uFrame.MVVM.IViewModelManager _MainMenuRootViewModelManager;
    
    private MainMenuRootViewModel _MainMenuRoot;
    
    [uFrame.IOC.InjectAttribute("MainMenuRoot")]
    public uFrame.MVVM.IViewModelManager MainMenuRootViewModelManager {
        get {
            return _MainMenuRootViewModelManager;
        }
        set {
            _MainMenuRootViewModelManager = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("MainMenuRoot")]
    public MainMenuRootViewModel MainMenuRoot {
        get {
            return _MainMenuRoot;
        }
        set {
            _MainMenuRoot = value;
        }
    }
    
    public IEnumerable<MainMenuRootViewModel> MainMenuRootViewModels {
        get {
            return MainMenuRootViewModelManager.OfType<MainMenuRootViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeMainMenuRoot(((MainMenuRootViewModel)(viewModel)));
    }
    
    public virtual MainMenuRootViewModel CreateMainMenuRoot() {
        return ((MainMenuRootViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new MainMenuRootViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeMainMenuRoot(MainMenuRootViewModel viewModel) {
        // This is called when a MainMenuRootViewModel is created
        MainMenuRootViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        MainMenuRootViewModelManager.Remove(viewModel);
    }
}

public class SubScreenControllerBase : uFrame.MVVM.Controller {
    
    private uFrame.MVVM.IViewModelManager _SubScreenViewModelManager;
    
    private MainMenuRootViewModel _MainMenuRoot;
    
    [uFrame.IOC.InjectAttribute("SubScreen")]
    public uFrame.MVVM.IViewModelManager SubScreenViewModelManager {
        get {
            return _SubScreenViewModelManager;
        }
        set {
            _SubScreenViewModelManager = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute("MainMenuRoot")]
    public MainMenuRootViewModel MainMenuRoot {
        get {
            return _MainMenuRoot;
        }
        set {
            _MainMenuRoot = value;
        }
    }
    
    public IEnumerable<SubScreenViewModel> SubScreenViewModels {
        get {
            return SubScreenViewModelManager.OfType<SubScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeSubScreen(((SubScreenViewModel)(viewModel)));
    }
    
    public virtual SubScreenViewModel CreateSubScreen() {
        return ((SubScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new SubScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeSubScreen(SubScreenViewModel viewModel) {
        // This is called when a SubScreenViewModel is created
        viewModel.Close.Action = this.CloseHandler;
        SubScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        SubScreenViewModelManager.Remove(viewModel);
    }
    
    public virtual void Close(SubScreenViewModel viewModel) {
    }
    
    public virtual void CloseHandler(CloseCommand command) {
        this.Close(command.Sender as SubScreenViewModel);
    }
}

public class LoginScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _LoginScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("LoginScreen")]
    public uFrame.MVVM.IViewModelManager LoginScreenViewModelManager {
        get {
            return _LoginScreenViewModelManager;
        }
        set {
            _LoginScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<LoginScreenViewModel> LoginScreenViewModels {
        get {
            return LoginScreenViewModelManager.OfType<LoginScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeLoginScreen(((LoginScreenViewModel)(viewModel)));
    }
    
    public virtual LoginScreenViewModel CreateLoginScreen() {
        return ((LoginScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new LoginScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeLoginScreen(LoginScreenViewModel viewModel) {
        // This is called when a LoginScreenViewModel is created
        viewModel.Login.Action = this.LoginHandler;
        LoginScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        LoginScreenViewModelManager.Remove(viewModel);
    }
    
    public virtual void Login(LoginScreenViewModel viewModel) {
    }
    
    public virtual void LoginHandler(LoginCommand command) {
        this.Login(command.Sender as LoginScreenViewModel);
    }
}

public class SettingsScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _SettingsScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("SettingsScreen")]
    public uFrame.MVVM.IViewModelManager SettingsScreenViewModelManager {
        get {
            return _SettingsScreenViewModelManager;
        }
        set {
            _SettingsScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<SettingsScreenViewModel> SettingsScreenViewModels {
        get {
            return SettingsScreenViewModelManager.OfType<SettingsScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeSettingsScreen(((SettingsScreenViewModel)(viewModel)));
    }
    
    public virtual SettingsScreenViewModel CreateSettingsScreen() {
        return ((SettingsScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new SettingsScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeSettingsScreen(SettingsScreenViewModel viewModel) {
        // This is called when a SettingsScreenViewModel is created
        viewModel.Apply.Action = this.ApplyHandler;
        viewModel.Default.Action = this.DefaultHandler;
        SettingsScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        SettingsScreenViewModelManager.Remove(viewModel);
    }
    
    public virtual void Apply(SettingsScreenViewModel viewModel) {
    }
    
    public virtual void Default(SettingsScreenViewModel viewModel) {
    }
    
    public virtual void ApplyHandler(ApplyCommand command) {
        this.Apply(command.Sender as SettingsScreenViewModel);
    }
    
    public virtual void DefaultHandler(DefaultCommand command) {
        this.Default(command.Sender as SettingsScreenViewModel);
    }
}

public class LevelSelectScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _LevelSelectScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("LevelSelectScreen")]
    public uFrame.MVVM.IViewModelManager LevelSelectScreenViewModelManager {
        get {
            return _LevelSelectScreenViewModelManager;
        }
        set {
            _LevelSelectScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<LevelSelectScreenViewModel> LevelSelectScreenViewModels {
        get {
            return LevelSelectScreenViewModelManager.OfType<LevelSelectScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeLevelSelectScreen(((LevelSelectScreenViewModel)(viewModel)));
    }
    
    public virtual LevelSelectScreenViewModel CreateLevelSelectScreen() {
        return ((LevelSelectScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new LevelSelectScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeLevelSelectScreen(LevelSelectScreenViewModel viewModel) {
        // This is called when a LevelSelectScreenViewModel is created
        viewModel.SelectLevel.Action = this.SelectLevelHandler;
        LevelSelectScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        LevelSelectScreenViewModelManager.Remove(viewModel);
    }
    
    public virtual void SelectLevelHandler(SelectLevelCommand command) {
        this.SelectLevel(command.Sender as LevelSelectScreenViewModel, command.Argument);
    }
    
    public virtual void SelectLevel(LevelSelectScreenViewModel viewModel, LevelDescriptor arg) {
    }
}

public class MenuScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _MenuScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("MenuScreen")]
    public uFrame.MVVM.IViewModelManager MenuScreenViewModelManager {
        get {
            return _MenuScreenViewModelManager;
        }
        set {
            _MenuScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<MenuScreenViewModel> MenuScreenViewModels {
        get {
            return MenuScreenViewModelManager.OfType<MenuScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeMenuScreen(((MenuScreenViewModel)(viewModel)));
    }
    
    public virtual MenuScreenViewModel CreateMenuScreen() {
        return ((MenuScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new MenuScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeMenuScreen(MenuScreenViewModel viewModel) {
        // This is called when a MenuScreenViewModel is created
        MenuScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        MenuScreenViewModelManager.Remove(viewModel);
    }
}

public class NoticeScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _NoticeScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("NoticeScreen")]
    public uFrame.MVVM.IViewModelManager NoticeScreenViewModelManager {
        get {
            return _NoticeScreenViewModelManager;
        }
        set {
            _NoticeScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<NoticeScreenViewModel> NoticeScreenViewModels {
        get {
            return NoticeScreenViewModelManager.OfType<NoticeScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeNoticeScreen(((NoticeScreenViewModel)(viewModel)));
    }
    
    public virtual NoticeScreenViewModel CreateNoticeScreen() {
        return ((NoticeScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new NoticeScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeNoticeScreen(NoticeScreenViewModel viewModel) {
        // This is called when a NoticeScreenViewModel is created
        viewModel.Sign.Action = this.SignHandler;
        NoticeScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        NoticeScreenViewModelManager.Remove(viewModel);
    }
    
    public virtual void Sign(NoticeScreenViewModel viewModel) {
    }
    
    public virtual void SignHandler(SignCommand command) {
        this.Sign(command.Sender as NoticeScreenViewModel);
    }
}

public class SampleScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _SampleScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("SampleScreen")]
    public uFrame.MVVM.IViewModelManager SampleScreenViewModelManager {
        get {
            return _SampleScreenViewModelManager;
        }
        set {
            _SampleScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<SampleScreenViewModel> SampleScreenViewModels {
        get {
            return SampleScreenViewModelManager.OfType<SampleScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeSampleScreen(((SampleScreenViewModel)(viewModel)));
    }
    
    public virtual SampleScreenViewModel CreateSampleScreen() {
        return ((SampleScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new SampleScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeSampleScreen(SampleScreenViewModel viewModel) {
        // This is called when a SampleScreenViewModel is created
        SampleScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        SampleScreenViewModelManager.Remove(viewModel);
    }
}

public class CardScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _CardScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("CardScreen")]
    public uFrame.MVVM.IViewModelManager CardScreenViewModelManager {
        get {
            return _CardScreenViewModelManager;
        }
        set {
            _CardScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<CardScreenViewModel> CardScreenViewModels {
        get {
            return CardScreenViewModelManager.OfType<CardScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeCardScreen(((CardScreenViewModel)(viewModel)));
    }
    
    public virtual CardScreenViewModel CreateCardScreen() {
        return ((CardScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new CardScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeCardScreen(CardScreenViewModel viewModel) {
        // This is called when a CardScreenViewModel is created
        CardScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        CardScreenViewModelManager.Remove(viewModel);
    }
}

public class SetBattleScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _SetBattleScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("SetBattleScreen")]
    public uFrame.MVVM.IViewModelManager SetBattleScreenViewModelManager {
        get {
            return _SetBattleScreenViewModelManager;
        }
        set {
            _SetBattleScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<SetBattleScreenViewModel> SetBattleScreenViewModels {
        get {
            return SetBattleScreenViewModelManager.OfType<SetBattleScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeSetBattleScreen(((SetBattleScreenViewModel)(viewModel)));
    }
    
    public virtual SetBattleScreenViewModel CreateSetBattleScreen() {
        return ((SetBattleScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new SetBattleScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeSetBattleScreen(SetBattleScreenViewModel viewModel) {
        // This is called when a SetBattleScreenViewModel is created
        SetBattleScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        SetBattleScreenViewModelManager.Remove(viewModel);
    }
}

public class CharPageScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _CharPageScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("CharPageScreen")]
    public uFrame.MVVM.IViewModelManager CharPageScreenViewModelManager {
        get {
            return _CharPageScreenViewModelManager;
        }
        set {
            _CharPageScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<CharPageScreenViewModel> CharPageScreenViewModels {
        get {
            return CharPageScreenViewModelManager.OfType<CharPageScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeCharPageScreen(((CharPageScreenViewModel)(viewModel)));
    }
    
    public virtual CharPageScreenViewModel CreateCharPageScreen() {
        return ((CharPageScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new CharPageScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeCharPageScreen(CharPageScreenViewModel viewModel) {
        // This is called when a CharPageScreenViewModel is created
        CharPageScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        CharPageScreenViewModelManager.Remove(viewModel);
    }
}

public class ConferenceScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _ConferenceScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("ConferenceScreen")]
    public uFrame.MVVM.IViewModelManager ConferenceScreenViewModelManager {
        get {
            return _ConferenceScreenViewModelManager;
        }
        set {
            _ConferenceScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<ConferenceScreenViewModel> ConferenceScreenViewModels {
        get {
            return ConferenceScreenViewModelManager.OfType<ConferenceScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeConferenceScreen(((ConferenceScreenViewModel)(viewModel)));
    }
    
    public virtual ConferenceScreenViewModel CreateConferenceScreen() {
        return ((ConferenceScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new ConferenceScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeConferenceScreen(ConferenceScreenViewModel viewModel) {
        // This is called when a ConferenceScreenViewModel is created
        ConferenceScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        ConferenceScreenViewModelManager.Remove(viewModel);
    }
}

public class ParallelScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _ParallelScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("ParallelScreen")]
    public uFrame.MVVM.IViewModelManager ParallelScreenViewModelManager {
        get {
            return _ParallelScreenViewModelManager;
        }
        set {
            _ParallelScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<ParallelScreenViewModel> ParallelScreenViewModels {
        get {
            return ParallelScreenViewModelManager.OfType<ParallelScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeParallelScreen(((ParallelScreenViewModel)(viewModel)));
    }
    
    public virtual ParallelScreenViewModel CreateParallelScreen() {
        return ((ParallelScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new ParallelScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeParallelScreen(ParallelScreenViewModel viewModel) {
        // This is called when a ParallelScreenViewModel is created
        ParallelScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        ParallelScreenViewModelManager.Remove(viewModel);
    }
}

public class CompanionScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _CompanionScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("CompanionScreen")]
    public uFrame.MVVM.IViewModelManager CompanionScreenViewModelManager {
        get {
            return _CompanionScreenViewModelManager;
        }
        set {
            _CompanionScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<CompanionScreenViewModel> CompanionScreenViewModels {
        get {
            return CompanionScreenViewModelManager.OfType<CompanionScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeCompanionScreen(((CompanionScreenViewModel)(viewModel)));
    }
    
    public virtual CompanionScreenViewModel CreateCompanionScreen() {
        return ((CompanionScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new CompanionScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeCompanionScreen(CompanionScreenViewModel viewModel) {
        // This is called when a CompanionScreenViewModel is created
        CompanionScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        CompanionScreenViewModelManager.Remove(viewModel);
    }
}

public class TechnologyTreeScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _TechnologyTreeScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("TechnologyTreeScreen")]
    public uFrame.MVVM.IViewModelManager TechnologyTreeScreenViewModelManager {
        get {
            return _TechnologyTreeScreenViewModelManager;
        }
        set {
            _TechnologyTreeScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<TechnologyTreeScreenViewModel> TechnologyTreeScreenViewModels {
        get {
            return TechnologyTreeScreenViewModelManager.OfType<TechnologyTreeScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeTechnologyTreeScreen(((TechnologyTreeScreenViewModel)(viewModel)));
    }
    
    public virtual TechnologyTreeScreenViewModel CreateTechnologyTreeScreen() {
        return ((TechnologyTreeScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new TechnologyTreeScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeTechnologyTreeScreen(TechnologyTreeScreenViewModel viewModel) {
        // This is called when a TechnologyTreeScreenViewModel is created
        TechnologyTreeScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        TechnologyTreeScreenViewModelManager.Remove(viewModel);
    }
}

public class StorageScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _StorageScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("StorageScreen")]
    public uFrame.MVVM.IViewModelManager StorageScreenViewModelManager {
        get {
            return _StorageScreenViewModelManager;
        }
        set {
            _StorageScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<StorageScreenViewModel> StorageScreenViewModels {
        get {
            return StorageScreenViewModelManager.OfType<StorageScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeStorageScreen(((StorageScreenViewModel)(viewModel)));
    }
    
    public virtual StorageScreenViewModel CreateStorageScreen() {
        return ((StorageScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new StorageScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeStorageScreen(StorageScreenViewModel viewModel) {
        // This is called when a StorageScreenViewModel is created
        StorageScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        StorageScreenViewModelManager.Remove(viewModel);
    }
}

public class ShopScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _ShopScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("ShopScreen")]
    public uFrame.MVVM.IViewModelManager ShopScreenViewModelManager {
        get {
            return _ShopScreenViewModelManager;
        }
        set {
            _ShopScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<ShopScreenViewModel> ShopScreenViewModels {
        get {
            return ShopScreenViewModelManager.OfType<ShopScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeShopScreen(((ShopScreenViewModel)(viewModel)));
    }
    
    public virtual ShopScreenViewModel CreateShopScreen() {
        return ((ShopScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new ShopScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeShopScreen(ShopScreenViewModel viewModel) {
        // This is called when a ShopScreenViewModel is created
        ShopScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        ShopScreenViewModelManager.Remove(viewModel);
    }
}

public class SchoolFieldScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _SchoolFieldScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("SchoolFieldScreen")]
    public uFrame.MVVM.IViewModelManager SchoolFieldScreenViewModelManager {
        get {
            return _SchoolFieldScreenViewModelManager;
        }
        set {
            _SchoolFieldScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<SchoolFieldScreenViewModel> SchoolFieldScreenViewModels {
        get {
            return SchoolFieldScreenViewModelManager.OfType<SchoolFieldScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeSchoolFieldScreen(((SchoolFieldScreenViewModel)(viewModel)));
    }
    
    public virtual SchoolFieldScreenViewModel CreateSchoolFieldScreen() {
        return ((SchoolFieldScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new SchoolFieldScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeSchoolFieldScreen(SchoolFieldScreenViewModel viewModel) {
        // This is called when a SchoolFieldScreenViewModel is created
        SchoolFieldScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        SchoolFieldScreenViewModelManager.Remove(viewModel);
    }
}

public class AcademyScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _AcademyScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("AcademyScreen")]
    public uFrame.MVVM.IViewModelManager AcademyScreenViewModelManager {
        get {
            return _AcademyScreenViewModelManager;
        }
        set {
            _AcademyScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<AcademyScreenViewModel> AcademyScreenViewModels {
        get {
            return AcademyScreenViewModelManager.OfType<AcademyScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeAcademyScreen(((AcademyScreenViewModel)(viewModel)));
    }
    
    public virtual AcademyScreenViewModel CreateAcademyScreen() {
        return ((AcademyScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new AcademyScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeAcademyScreen(AcademyScreenViewModel viewModel) {
        // This is called when a AcademyScreenViewModel is created
        AcademyScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        AcademyScreenViewModelManager.Remove(viewModel);
    }
}

public class ArtisanScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _ArtisanScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("ArtisanScreen")]
    public uFrame.MVVM.IViewModelManager ArtisanScreenViewModelManager {
        get {
            return _ArtisanScreenViewModelManager;
        }
        set {
            _ArtisanScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<ArtisanScreenViewModel> ArtisanScreenViewModels {
        get {
            return ArtisanScreenViewModelManager.OfType<ArtisanScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeArtisanScreen(((ArtisanScreenViewModel)(viewModel)));
    }
    
    public virtual ArtisanScreenViewModel CreateArtisanScreen() {
        return ((ArtisanScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new ArtisanScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeArtisanScreen(ArtisanScreenViewModel viewModel) {
        // This is called when a ArtisanScreenViewModel is created
        ArtisanScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        ArtisanScreenViewModelManager.Remove(viewModel);
    }
}

public class TrainScreenControllerBase : SubScreenController {
    
    private uFrame.MVVM.IViewModelManager _TrainScreenViewModelManager;
    
    [uFrame.IOC.InjectAttribute("TrainScreen")]
    public uFrame.MVVM.IViewModelManager TrainScreenViewModelManager {
        get {
            return _TrainScreenViewModelManager;
        }
        set {
            _TrainScreenViewModelManager = value;
        }
    }
    
    public IEnumerable<TrainScreenViewModel> TrainScreenViewModels {
        get {
            return TrainScreenViewModelManager.OfType<TrainScreenViewModel>();
        }
    }
    
    public override void Setup() {
        base.Setup();
        // This is called when the controller is created
    }
    
    public override void Initialize(uFrame.MVVM.ViewModel viewModel) {
        base.Initialize(viewModel);
        // This is called when a viewmodel is created
        this.InitializeTrainScreen(((TrainScreenViewModel)(viewModel)));
    }
    
    public virtual TrainScreenViewModel CreateTrainScreen() {
        return ((TrainScreenViewModel)(this.Create(Guid.NewGuid().ToString())));
    }
    
    public override uFrame.MVVM.ViewModel CreateEmpty() {
        return new TrainScreenViewModel(this.EventAggregator);
    }
    
    public virtual void InitializeTrainScreen(TrainScreenViewModel viewModel) {
        // This is called when a TrainScreenViewModel is created
        TrainScreenViewModelManager.Add(viewModel);
    }
    
    public override void DisposingViewModel(uFrame.MVVM.ViewModel viewModel) {
        base.DisposingViewModel(viewModel);
        TrainScreenViewModelManager.Remove(viewModel);
    }
}

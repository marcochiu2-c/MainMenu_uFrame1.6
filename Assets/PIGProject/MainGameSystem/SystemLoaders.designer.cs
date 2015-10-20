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
using uFrame.MVVM;


public class MainGameSystemLoaderBase : uFrame.Kernel.SystemLoader {
    
    private SoldierViewModel _Soldier1;
    
    private SoldierViewModel _Soldier2;
    
    private EnemyViewModel _Enemy1;
    
    private MainGameRootViewModel _MainGame;
    
    private EnemyViewModel _Enemy2;
    
    private EnemyViewModel _Enemy3;
    
    private EnemyViewModel _Enemy4;
    
    private EnemyViewModel _Enemy5;
    
    private SoldierViewModel _Soldier3;
    
    private SoldierViewModel _Soldier4;
    
    private SoldierViewModel _Soldier5;
    
    private MainGameRootController _MainGameRootController;
    
    private SoldierController _SoldierController;
    
    private EnemyController _EnemyController;
    
    private EntityController _EntityController;
    
    [uFrame.IOC.InjectAttribute("Soldier1")]
    public virtual SoldierViewModel Soldier1 {
        get {
            if (this._Soldier1 == null) {
                this._Soldier1 = this.CreateViewModel<SoldierViewModel>( "Soldier1");
            }
            return _Soldier1;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier2")]
    public virtual SoldierViewModel Soldier2 {
        get {
            if (this._Soldier2 == null) {
                this._Soldier2 = this.CreateViewModel<SoldierViewModel>( "Soldier2");
            }
            return _Soldier2;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy1")]
    public virtual EnemyViewModel Enemy1 {
        get {
            if (this._Enemy1 == null) {
                this._Enemy1 = this.CreateViewModel<EnemyViewModel>( "Enemy1");
            }
            return _Enemy1;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("MainGame")]
    public virtual MainGameRootViewModel MainGame {
        get {
            if (this._MainGame == null) {
                this._MainGame = this.CreateViewModel<MainGameRootViewModel>( "MainGame");
            }
            return _MainGame;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy2")]
    public virtual EnemyViewModel Enemy2 {
        get {
            if (this._Enemy2 == null) {
                this._Enemy2 = this.CreateViewModel<EnemyViewModel>( "Enemy2");
            }
            return _Enemy2;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy3")]
    public virtual EnemyViewModel Enemy3 {
        get {
            if (this._Enemy3 == null) {
                this._Enemy3 = this.CreateViewModel<EnemyViewModel>( "Enemy3");
            }
            return _Enemy3;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy4")]
    public virtual EnemyViewModel Enemy4 {
        get {
            if (this._Enemy4 == null) {
                this._Enemy4 = this.CreateViewModel<EnemyViewModel>( "Enemy4");
            }
            return _Enemy4;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Enemy5")]
    public virtual EnemyViewModel Enemy5 {
        get {
            if (this._Enemy5 == null) {
                this._Enemy5 = this.CreateViewModel<EnemyViewModel>( "Enemy5");
            }
            return _Enemy5;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier3")]
    public virtual SoldierViewModel Soldier3 {
        get {
            if (this._Soldier3 == null) {
                this._Soldier3 = this.CreateViewModel<SoldierViewModel>( "Soldier3");
            }
            return _Soldier3;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier4")]
    public virtual SoldierViewModel Soldier4 {
        get {
            if (this._Soldier4 == null) {
                this._Soldier4 = this.CreateViewModel<SoldierViewModel>( "Soldier4");
            }
            return _Soldier4;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute("Soldier5")]
    public virtual SoldierViewModel Soldier5 {
        get {
            if (this._Soldier5 == null) {
                this._Soldier5 = this.CreateViewModel<SoldierViewModel>( "Soldier5");
            }
            return _Soldier5;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute()]
    public virtual MainGameRootController MainGameRootController {
        get {
            if (_MainGameRootController==null) {
                _MainGameRootController = Container.CreateInstance(typeof(MainGameRootController)) as MainGameRootController;;
            }
            return _MainGameRootController;
        }
        set {
            _MainGameRootController = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute()]
    public virtual SoldierController SoldierController {
        get {
            if (_SoldierController==null) {
                _SoldierController = Container.CreateInstance(typeof(SoldierController)) as SoldierController;;
            }
            return _SoldierController;
        }
        set {
            _SoldierController = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute()]
    public virtual EnemyController EnemyController {
        get {
            if (_EnemyController==null) {
                _EnemyController = Container.CreateInstance(typeof(EnemyController)) as EnemyController;;
            }
            return _EnemyController;
        }
        set {
            _EnemyController = value;
        }
    }
    
    [uFrame.IOC.InjectAttribute()]
    public virtual EntityController EntityController {
        get {
            if (_EntityController==null) {
                _EntityController = Container.CreateInstance(typeof(EntityController)) as EntityController;;
            }
            return _EntityController;
        }
        set {
            _EntityController = value;
        }
    }
    
    public override void Load() {
        Container.RegisterViewModelManager<MainGameRootViewModel>(new ViewModelManager<MainGameRootViewModel>());
        Container.RegisterController<MainGameRootController>(MainGameRootController);
        Container.RegisterViewModelManager<SoldierViewModel>(new ViewModelManager<SoldierViewModel>());
        Container.RegisterController<SoldierController>(SoldierController);
        Container.RegisterViewModelManager<EnemyViewModel>(new ViewModelManager<EnemyViewModel>());
        Container.RegisterController<EnemyController>(EnemyController);
        Container.RegisterViewModelManager<EntityViewModel>(new ViewModelManager<EntityViewModel>());
        Container.RegisterController<EntityController>(EntityController);
        Container.RegisterViewModel<SoldierViewModel>(Soldier1, "Soldier1");
        Container.RegisterViewModel<SoldierViewModel>(Soldier2, "Soldier2");
        Container.RegisterViewModel<EnemyViewModel>(Enemy1, "Enemy1");
        Container.RegisterViewModel<MainGameRootViewModel>(MainGame, "MainGame");
        Container.RegisterViewModel<EnemyViewModel>(Enemy2, "Enemy2");
        Container.RegisterViewModel<EnemyViewModel>(Enemy3, "Enemy3");
        Container.RegisterViewModel<EnemyViewModel>(Enemy4, "Enemy4");
        Container.RegisterViewModel<EnemyViewModel>(Enemy5, "Enemy5");
        Container.RegisterViewModel<SoldierViewModel>(Soldier3, "Soldier3");
        Container.RegisterViewModel<SoldierViewModel>(Soldier4, "Soldier4");
        Container.RegisterViewModel<SoldierViewModel>(Soldier5, "Soldier5");
    }
}

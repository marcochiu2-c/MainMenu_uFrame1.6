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
    
    private SoldierViewModel _Soldier;
    
    private SoldierViewModel _Soldier2;
    
    private EnemyViewModel _Enemy;
    
    private MainGameRootViewModel _MainGame;
    
    private EnemyViewModel _Enemy2;
    
    private EnemyViewModel _Enemy3;
    
    private EnemyViewModel _Enemy4;
    
    private EnemyViewModel _Enemy5;
    
    private MainGameRootController _MainGameRootController;
    
    private SoldierController _SoldierController;
    
    private EnemyController _EnemyController;
    
    private EntityController _EntityController;
    
    [uFrame.IOC.InjectAttribute("Soldier")]
    public virtual SoldierViewModel Soldier {
        get {
            if (this._Soldier == null) {
                this._Soldier = this.CreateViewModel<SoldierViewModel>( "Soldier");
            }
            return _Soldier;
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
    
    [uFrame.IOC.InjectAttribute("Enemy")]
    public virtual EnemyViewModel Enemy {
        get {
            if (this._Enemy == null) {
                this._Enemy = this.CreateViewModel<EnemyViewModel>( "Enemy");
            }
            return _Enemy;
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
        Container.RegisterViewModel<SoldierViewModel>(Soldier, "Soldier");
        Container.RegisterViewModel<SoldierViewModel>(Soldier2, "Soldier2");
        Container.RegisterViewModel<EnemyViewModel>(Enemy, "Enemy");
        Container.RegisterViewModel<MainGameRootViewModel>(MainGame, "MainGame");
        Container.RegisterViewModel<EnemyViewModel>(Enemy2, "Enemy2");
        Container.RegisterViewModel<EnemyViewModel>(Enemy3, "Enemy3");
        Container.RegisterViewModel<EnemyViewModel>(Enemy4, "Enemy4");
        Container.RegisterViewModel<EnemyViewModel>(Enemy5, "Enemy5");
    }
}

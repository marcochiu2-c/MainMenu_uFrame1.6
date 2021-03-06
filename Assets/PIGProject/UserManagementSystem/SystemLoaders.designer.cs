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


public class UserManagementSystemLoaderBase : uFrame.Kernel.SystemLoader {
    
    private UserViewModel _LocalUser;
    
    private UserController _UserController;
    
    [uFrame.IOC.InjectAttribute("LocalUser")]
    public virtual UserViewModel LocalUser {
        get {
            if (this._LocalUser == null) {
                this._LocalUser = this.CreateViewModel<UserViewModel>( "LocalUser");
            }
            return _LocalUser;
        }
        set {
        }
    }
    
    [uFrame.IOC.InjectAttribute()]
    public virtual UserController UserController {
        get {
            if (_UserController==null) {
                _UserController = Container.CreateInstance(typeof(UserController)) as UserController;;
            }
            return _UserController;
        }
        set {
            _UserController = value;
        }
    }
    
    public override void Load() {
        Container.RegisterViewModelManager<UserViewModel>(new ViewModelManager<UserViewModel>());
        Container.RegisterController<UserController>(UserController);
        Container.RegisterViewModel<UserViewModel>(LocalUser, "LocalUser");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;


public class MainGameRootController : MainGameRootControllerBase {
    
    public override void InitializeMainGameRoot(MainGameRootViewModel viewModel) {
        base.InitializeMainGameRoot(viewModel);
        // This is called when a MainGameRootViewModel is created
    }
}

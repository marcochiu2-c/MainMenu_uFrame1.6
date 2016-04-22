using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class CompanionScreenController : CompanionScreenControllerBase {
    
    public override void InitializeCompanionScreen(CompanionScreenViewModel viewModel) {
        base.InitializeCompanionScreen(viewModel);
        // This is called when a CompanionScreenViewModel is created
    }
}

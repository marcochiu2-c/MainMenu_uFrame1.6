using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class CharPageScreenController : CharPageScreenControllerBase {
    
    public override void InitializeCharPageScreen(CharPageScreenViewModel viewModel) {
        base.InitializeCharPageScreen(viewModel);
        // This is called when a CharPageScreenViewModel is created
    }
}

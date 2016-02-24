using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class CardScreenController : CardScreenControllerBase {
    
    public override void InitializeCardScreen(CardScreenViewModel viewModel) {
        base.InitializeCardScreen(viewModel);
        // This is called when a CardScreenViewModel is created
    }
}

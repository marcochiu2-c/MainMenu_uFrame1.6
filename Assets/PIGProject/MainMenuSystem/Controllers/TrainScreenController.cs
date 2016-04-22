using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class TrainScreenController : TrainScreenControllerBase {
    
    public override void InitializeTrainScreen(TrainScreenViewModel viewModel) {
        base.InitializeTrainScreen(viewModel);
        // This is called when a TrainScreenViewModel is created
    }
}

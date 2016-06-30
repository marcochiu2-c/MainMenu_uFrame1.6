using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class ParallelScreenController : ParallelScreenControllerBase {
    
    public override void InitializeParallelScreen(ParallelScreenViewModel viewModel) {
        base.InitializeParallelScreen(viewModel);
        // This is called when a ParallelScreenViewModel is created
    }
}

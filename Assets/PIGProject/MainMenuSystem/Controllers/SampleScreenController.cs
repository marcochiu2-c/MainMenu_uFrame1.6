using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using uFrame.Serialization;


public class SampleScreenController : SampleScreenControllerBase {
    
    public override void InitializeSampleScreen(SampleScreenViewModel viewModel) {
        base.InitializeSampleScreen(viewModel);
        // This is called when a SampleScreenViewModel is created
    }
}

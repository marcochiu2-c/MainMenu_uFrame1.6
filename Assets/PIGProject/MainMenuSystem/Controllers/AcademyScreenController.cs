using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class AcademyScreenController : AcademyScreenControllerBase {
    
    public override void InitializeAcademyScreen(AcademyScreenViewModel viewModel) {
        base.InitializeAcademyScreen(viewModel);
        // This is called when a AcademyScreenViewModel is created
    }
}

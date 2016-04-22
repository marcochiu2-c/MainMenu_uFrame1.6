using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class SchoolFieldScreenController : SchoolFieldScreenControllerBase {
    
    public override void InitializeSchoolFieldScreen(SchoolFieldScreenViewModel viewModel) {
        base.InitializeSchoolFieldScreen(viewModel);
        // This is called when a SchoolFieldScreenViewModel is created
    }
}

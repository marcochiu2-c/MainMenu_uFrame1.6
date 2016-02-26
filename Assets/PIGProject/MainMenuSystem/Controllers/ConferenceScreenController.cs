using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class ConferenceScreenController : ConferenceScreenControllerBase {
    
    public override void InitializeConferenceScreen(ConferenceScreenViewModel viewModel) {
        base.InitializeConferenceScreen(viewModel);
        // This is called when a ConferenceScreenViewModel is created
    }
}

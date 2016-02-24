using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class SetBattleScreenController : SetBattleScreenControllerBase {
    
    public override void InitializeSetBattleScreen(SetBattleScreenViewModel viewModel) {
        base.InitializeSetBattleScreen(viewModel);
        // This is called when a SetBattleScreenViewModel is created
    }
}

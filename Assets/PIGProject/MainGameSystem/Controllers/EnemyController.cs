using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;


public class EnemyController : EnemyControllerBase {
    
    public override void InitializeEnemy(EnemyViewModel viewModel) {
        base.InitializeEnemy(viewModel);
        // This is called when a EnemyViewModel is created
    }
}

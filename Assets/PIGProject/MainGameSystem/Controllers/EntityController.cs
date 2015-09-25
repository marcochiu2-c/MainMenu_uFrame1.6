using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;


public class EntityController : EntityControllerBase {
    
    public override void InitializeEntity(EntityViewModel viewModel) {
        base.InitializeEntity(viewModel);
        // This is called when a EntityViewModel is created
    }
}

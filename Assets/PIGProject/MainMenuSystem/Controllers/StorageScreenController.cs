using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class StorageScreenController : StorageScreenControllerBase {
    
    public override void InitializeStorageScreen(StorageScreenViewModel viewModel) {
        base.InitializeStorageScreen(viewModel);
        // This is called when a StorageScreenViewModel is created
    }
}

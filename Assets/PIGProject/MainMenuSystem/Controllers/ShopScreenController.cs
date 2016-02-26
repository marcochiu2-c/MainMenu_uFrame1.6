using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class ShopScreenController : ShopScreenControllerBase {
    
    public override void InitializeShopScreen(ShopScreenViewModel viewModel) {
        base.InitializeShopScreen(viewModel);
        // This is called when a ShopScreenViewModel is created
    }
}

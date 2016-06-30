using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UniRx;
using uFrame.Serialization;
using uFrame.MVVM;


public class ArtisanScreenController : ArtisanScreenControllerBase {
    
    public override void InitializeArtisanScreen(ArtisanScreenViewModel viewModel) {
        base.InitializeArtisanScreen(viewModel);
        // This is called when a ArtisanScreenViewModel is created
    }
}

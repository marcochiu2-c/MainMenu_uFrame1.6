using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Serialization;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using Gamelogic;
using Gamelogic.Grids;

public class MainGameRootController : MainGameRootControllerBase {
    
    public override void InitializeMainGameRoot(MainGameRootViewModel viewModel) {
        base.InitializeMainGameRoot(viewModel);
        // This is called when a MainGameRootViewModel is created
    }

    public override void GoToMenu(MainGameRootViewModel viewModel) {
        base.GoToMenu(viewModel);
    }

    public override void Play(MainGameRootViewModel viewModel) {
        base.Play(viewModel);
    }

    public override void GameOver(MainGameRootViewModel viewModel) {
        base.GameOver(viewModel);
    }
}

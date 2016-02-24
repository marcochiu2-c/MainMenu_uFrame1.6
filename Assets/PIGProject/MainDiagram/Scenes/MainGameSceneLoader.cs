using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.Serialization;
using UnityEngine;


public class MainGameSceneLoader : MainGameSceneLoaderBase {
    
    protected override IEnumerator LoadScene(MainGameScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(MainGameScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.Serialization;


public class MainMenuSceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual MainMenuSceneSettings Settings {
        get {
            return _SettingsObject as MainMenuSceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class MainMenuSceneLoaderBase : SceneLoader<MainMenuScene> {
    
    protected override IEnumerator LoadScene(MainMenuScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(MainMenuScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

public class LevelSceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual LevelSceneSettings Settings {
        get {
            return _SettingsObject as LevelSceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class LevelSceneLoaderBase : SceneLoader<LevelScene> {
    
    protected override IEnumerator LoadScene(LevelScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(LevelScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

public class IntroSceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual IntroSceneSettings Settings {
        get {
            return _SettingsObject as IntroSceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class IntroSceneLoaderBase : SceneLoader<IntroScene> {
    
    protected override IEnumerator LoadScene(IntroScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(IntroScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

public class AssetsLoadingSceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual AssetsLoadingSceneSettings Settings {
        get {
            return _SettingsObject as AssetsLoadingSceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class AssetsLoadingSceneLoaderBase : SceneLoader<AssetsLoadingScene> {
    
    protected override IEnumerator LoadScene(AssetsLoadingScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(AssetsLoadingScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

public class NotificationUISceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual NotificationUISceneSettings Settings {
        get {
            return _SettingsObject as NotificationUISceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class NotificationUISceneLoaderBase : SceneLoader<NotificationUIScene> {
    
    protected override IEnumerator LoadScene(NotificationUIScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(NotificationUIScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

public class DialogueSceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual DialogueSceneSettings Settings {
        get {
            return _SettingsObject as DialogueSceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class DialogueSceneLoaderBase : SceneLoader<DialogueScene> {
    
    protected override IEnumerator LoadScene(DialogueScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(DialogueScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

public class MainGameSceneBase : uFrame.Kernel.Scene {
    
    public override string DefaultKernelScene {
        get {
            return "PIGProjectKernelScene";
        }
    }
    
    public virtual MainGameSceneSettings Settings {
        get {
            return _SettingsObject as MainGameSceneSettings;
        }
        set {
            _SettingsObject = value;
        }
    }
}

public class MainGameSceneLoaderBase : SceneLoader<MainGameScene> {
    
    protected override IEnumerator LoadScene(MainGameScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
    
    protected override IEnumerator UnloadScene(MainGameScene scene, Action<float, string> progressDelegate) {
        yield break;
    }
}

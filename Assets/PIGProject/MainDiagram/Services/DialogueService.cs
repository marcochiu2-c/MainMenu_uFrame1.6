using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UnityEngine;
using UniRx;
using uFrame.MVVM;
using DG.Tweening;
using PixelCrushers.DialogueSystem;

public class DialogueService : DialogueServiceBase {
    
	//UI Container which will hold ui items, while they are shown
	public Transform UIContainer;
	
	//A template prefab for notification ui item
	public GameObject RPGPrefab;

	public override void Setup(){
		base.Setup();
		//Every time GAmeReadyEvent is published, invok LoadNotificationUIScene
		this.OnEvent<GameReadyEvent>().Subscribe(evt => LoadDialogueScene());

		//Will invoke NotificationUISceneLoaded when such scene is loaded
		this.OnEvent<SceneLoaderEvent>()
			.Where (evt=>evt.SceneRoot is NotificationUIScene)
				.Subscribe(evt => DialogueSceneLoaded(evt.SceneRoot as DialogueScene));
	}
	
	protected void DialogueSceneLoaded(DialogueScene scene){
		//UIContainer = scene.UIContainer;
	}
	
	protected void LoadDialogueScene(){
		//Will load NotificationUIScene
		this.Publish(new LoadSceneCommand(){
			SceneName = "DialogueScene"
		});
	}

    public override void DialogueCommandHandler(DialogueCommand data) {
        base.DialogueCommandHandler(data);
        // Process the commands information.  Also, you can publish new events by using the line below.
        // this.Publish(new AnotherEvent())
		ShowDialogue(data);
    }

	public void ShowDialogue(DialogueCommand DialogueData){
		//var uiItem = Instantiate (RPGPrefab) as GameObject;

		DialogueManager.StartConversation(DialogueData.ConversationName);
	}
}

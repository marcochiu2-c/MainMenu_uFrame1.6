using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.IOC;
using uFrame.Kernel;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using uFrame.MVVM;


public class NotificationService : NotificationServiceBase {

	//UI Container which will hold notification ui items, while they are shown
	public Transform UIContainer;

	//A template prefab for notification ui item
	public GameObject NotificationItemPrefab;

	public override void Setup(){
		base.Setup();
		//Every time GAmeReadyEvent is published, invok LoadNotificationUIScene
		this.OnEvent<GameReadyEvent>().Subscribe(evt => LoadNotificationUIScene());

		//Will invoke NotificationUISceneLoaded when such scene is loaded
		this.OnEvent<SceneLoaderEvent>()
			.Where (evt=>evt.SceneRoot as NotificationUIScene)
				.Subscribe(evt => NotificationUISceneLoaded(evt.SceneRoot as NotificationUIScene));
	}

	protected void NotificationUISceneLoaded(NotificationUIScene scene){
		UIContainer = scene.UIContainer;
	}

	protected void LoadNotificationUIScene(){
		//Will load NotificationUIScene
		this.Publish(new LoadSceneCommand(){
			SceneName = "NotificationUIScene"
		});
	}

	public override void NotifyCommandHandler(NotifyCommand data){
		base.NotifyCommandHandler(data);
		//This method exists because we added NotifyComaand in the Handlers section of NotificationService.
		//It will be invoked every time you publish NotifyCommand
		StartCoroutine(ShowNotification(data));
	}

	IEnumerator ShowNotification(NotifyCommand notificationData){
		//Construct prefab and cache uiItemCanvas
		var uiItem = Instantiate (NotificationItemPrefab);
		var uiItemCanvas = uiItem.GetComponent<CanvasGroup>();

		//Set alpha to 0
		uiItemCanvas.alpha = 0;

		//Set text to message
		uiItemCanvas.GetComponentInChildren<Text>().text = notificationData.Message;
		
		//Parent object to the Container
		uiItem.transform.SetParent(UIContainer);
		
		//Reset scale of an object (unity sometimes messes it up when you parent it to the container with layout group
		uiItem.transform.localScale = Vector3.one;
		
		//Fade In
		yield return StartCoroutine(Fade(uiItemCanvas, 1, 0.5f));
		
		//Let it be for 5 seconds
		yield return new WaitForSeconds(1);
		
		//Fade Out
		yield return StartCoroutine(Fade(uiItemCanvas, 0, 0.5f));
		
		//Kill the notification ui item
		Destroy(uiItem);
	}

		IEnumerator Fade(CanvasGroup target, float alpha, float time){
			//Start time
			var start = Time.time;
			while (Math.Abs(target.alpha - alpha) > 0.01f){
				//Time passed from start
				var elapsed = Time.time - start;
				//normalized time from 0..1
				var normalisedTime = Mathf.Clamp((elapsed / time) * Time.deltaTime, 0, 1);
				//assign interpolated value
				target.alpha = Mathf.Lerp(target.alpha, alpha, normalisedTime);
				yield return null;
			}
		}
	}

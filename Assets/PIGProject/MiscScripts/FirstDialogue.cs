using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using uFrame.Kernel;
using uFrame.IOC;
using UniRx;
using uFrame.MVVM;
using uFrame.Serialization;

public class FirstDialogue : uFrameComponent{
	
	public GameObject poem0;
	// Use this for initialization
	
	public override void KernelLoaded()
	{
		base.KernelLoaded();
		StartCoroutine(DialogueIntro());	
	}
	
	private IEnumerator DialogueIntro()
	{
		//Poem0 Alpha 1 > 0
		
		
		//Call First Dialogue
		Publish(new DialogueCommand()
		{
			ConversationName = "Ch0_1"
		});
		
		yield return null;
		
	}
	// Update is called once per frame
	void Update () {
	
	}
}

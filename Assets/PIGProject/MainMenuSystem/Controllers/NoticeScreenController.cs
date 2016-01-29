using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.MVVM;
using uFrame.Kernel;
using uFrame.IOC;
using uFrame.Serialization;
using UniRx;
using UnityEngine;


public class NoticeScreenController : NoticeScreenControllerBase {
    
    public override void InitializeNoticeScreen(NoticeScreenViewModel viewModel) {
        base.InitializeNoticeScreen(viewModel);
        // This is called when a NoticeScreenViewModel is created
    }

	public override void Sign(NoticeScreenViewModel viewModel) {
		base.Sign(viewModel);
		//TODO
		Publish(new DialogueCommand()
		        {
			ConversationName = "daughter"
		});
		
		Debug.Log("You have signed");
	}
}

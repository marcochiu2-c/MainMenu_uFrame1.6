using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Services;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UniRx;
using UnityEngine;
using Gamelogic.Grids;

public class PlayerView : PlayerViewBase {
	//public IMap<FlatHexPoint> Map;
	
	public FlatHexPoint CurrentPointLocation
	{
		get;
		set;
	}
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as PlayerViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
    }
    
    public override void Bind() {
        base.Bind();
        // Use this.Player to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.
    }

	//public void Move(float x, float y){
	//	transform.position = new Vector3(x, y);
	//}

	// try to use UniRx when have time	
	public IEnumerator Move(Vector3 currentPoint, Vector3 endPoint, MoveStyle moveStyle){
		float time = 0;
		const float totalTime = .3f;
		
		//onAction = true;
		while (time < totalTime)
		{
			float x = Mathf.Lerp(currentPoint.x, endPoint.x, time / totalTime);
			float y = Mathf.Lerp(currentPoint.y, endPoint.y, time / totalTime);

			y -= 20;
			
			transform.position = new Vector3(x, y);
			time += Time.deltaTime;
		}
		
		//CurrentPointLocation  = endPoint;
		yield return null;
		

	}

    public override void StateChanged(PlayerState _state) {
    }
}

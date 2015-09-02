using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gamelogic.Grids;
using PixelCrushers.DialogueSystem;

public class MatchingGrid : GridBehaviour<DiamondPoint> 
{

	//need to create a array to save the point and command since it will be run agagin when the battle start
	public Player player;
	private bool onAction = false; 

	// Use this for initialization
	void Start () {
			//player.CurrentPointLocation = DiamondPoint.Zero;
		//player.transform.position = new Vector3(Map[player.CurrentPointLocation].x, Map[player.CurrentPointLocation].y);
		//_playerPoint= startPoint;

		///<c>
		/// player.CurrentPointLocation -> DiamondPoint -> e.g) (1,0)
		/// Map[player.CurrentPointLocation] -> actually position ----->Map[].X, Map[].Y
		/// 
		/// 
		/// </c>
	}

	public void OnClick(DiamondPoint point)
	{	

		if(onAction) return;

		Debug.Log (point.BasePoint);   //return (x,y)

		//if(player.CurrentPointLocation != null)
		//	StartCoroutine(Move (player.CurrentPointLocation , point));
	}

	private IEnumerator Move(DiamondPoint currentPoint, DiamondPoint endPoint){

		float time = 0;
		const float totalTime = .3f;

		//onAction = true;

		while (time < totalTime)
		{
			float x = Mathf.Lerp(Map[currentPoint].x, Map[endPoint].x, time / totalTime);
			float y = Mathf.Lerp(Map[currentPoint].y, Map[endPoint].y, time / totalTime);

			 y  += 30;
			
			//player.transform.position = new Vector3(x, y);
			time += Time.deltaTime;
			
			yield return null;

		}

		//player.CurrentPointLocation  = endPoint;

		CallCommand();
		yield return null;
		//yield return new WaitForSeconds(2);

		onAction = false;
	}

	public void CallCommand(){
		//TODO
		DialogueManager.StartConversation("PiKaChuAction");

	}

	public void OnConversationEnd(Transform actor) {
		Debug.Log ("Converation Stop");
		onAction = false;
	}
}

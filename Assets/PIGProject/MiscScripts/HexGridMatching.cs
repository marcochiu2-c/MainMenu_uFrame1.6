using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gamelogic;
using Gamelogic.Grids;
using PixelCrushers.DialogueSystem;

public class HexGridMatching : GridBehaviour<FlatHexPoint> 
{
	
	//need to create a array to save the point and command since it will be run agagin when the battle start
	public PlayerView player;
	public SpriteCell pathPrefab;
	public GameObject pathRoot;

	private bool onAction = true; 
	private FlatHexPoint start;
	private FlatHexPoint finish;
	
	// Use this for initialization

	void Start () {

		if(player != null && Map != null){
			player.CurrentPointLocation = FlatHexPoint.Zero;
			player._Movement = MoveStyle.SLOW;
			start = FlatHexPoint.Zero;

			player.Move(Map[player.CurrentPointLocation], Map[player.CurrentPointLocation],player._Movement);
		}
		///<c>
		/// player.CurrentPointLocation -> DiamondPoint -> e.g) (1,0)
		/// Map[player.CurrentPointLocation] -> actually position ----->Map[].X, Map[].Y
		/// 
		/// 
		/// </c>
	}
	
	public void OnClick(FlatHexPoint point)
	{	
		//onAction = !onAction; 
		//if(onAction) return;
		
		//Debug.Log (point.BasePoint);   //return (x,y)
		
		if(player != null)
		{
			//StartCoroutine(Move (player.CurrentPointLocation , point));
			start = player.CurrentPointLocation;
			finish = point;
			player.CurrentPointLocation = point;
			var path = Algorithms.AStar(Grid, start, finish);

			StartCoroutine(MovePath(path));
		}
	}


	public IEnumerator MovePath(IEnumerable<FlatHexPoint> path)
	{
		var pathList = path.ToList();
		
		if(pathList.Count < 2) yield break; //Not a valid path

		foreach (var point in path)
		{
			var pathNode = Instantiate(pathPrefab);
			
			pathNode.transform.parent = pathRoot.transform;
			pathNode.transform.localScale = Vector3.one * 0.3f;
			pathNode.transform.localPosition = Map[point];
			
			if (point == start)
			{
				pathNode.Color = ExampleUtils.Colors[1];
			}
			else if (point == finish)
			{
				pathNode.Color = ExampleUtils.Colors[3];
			}
			else
			{
				pathNode.Color = ExampleUtils.Colors[2];
			}

		}
		
		for(int i = 0; i < pathList.Count - 1; i++)
		{
			yield return StartCoroutine(player.Move (Map[pathList[i]], Map[pathList[i+1]], player._Movement));
		}
		CallCommand();
	 }


	/*
	public IEnumerator Move(FlatHexPoint currentPoint, FlatHexPoint endPoint, MoveStyle moveStyle){
		float time = 0;
		const float totalTime = .3f;
		
		//onAction = true;
		
		while (time < totalTime)
		{
			float x = Mathf.Lerp(Map[currentPoint].x, Map[endPoint].x, time / totalTime);
			float y = Mathf.Lerp(Map[currentPoint].y, Map[endPoint].y, time / totalTime);

			player.Move(x, y);
			time += Time.deltaTime;
		}
		
		player.CurrentPointLocation  = endPoint;
		yield return null;

		/*
		if(moveStyle == MoveStyle.SLOW)
			yield return new WaitForSeconds(0.05f);

		else if(moveStyle == MoveStyle.NORMAL)
			yield return new WaitForSeconds(0.1f);

		else if(moveStyle == MoveStyle.FAST)
			yield return new WaitForSeconds(0.2f);

		else
			yield return new WaitForSeconds(0.4f);

		}
		*/
	
	public void CallCommand(){
		//TODO
		DialogueManager.StartConversation("PiKaChuAction");
		
	}
	
	public void OnConversationEnd(Transform actor) {
		Debug.Log ("Converation Stop");
		onAction = false;
	}

	public static class ExampleUtils
	{
		public static Color ColorFromInt(int r, int g, int b)
		{
			return new Color(r/255.0f, g/255.0f, b/255.0f, 0.5f);
		}
		
		public static Color ColorFromInt(int r, int g, int b, int a)
		{
			return new Color(r/255.0f, g/255.0f, b/255.0f, a/255.0f);
		}
		
		public static Color[] Colors =
		{
			ColorFromInt(133, 219, 233),
			ColorFromInt(198, 224, 34),
			ColorFromInt(255, 215, 87),
			ColorFromInt(228, 120, 129),
			
			ColorFromInt(42, 192, 217),
			ColorFromInt(114, 197, 29),
			ColorFromInt(247, 188, 0),
			ColorFromInt(215, 55, 82),
			
			ColorFromInt(205, 240, 246),
			ColorFromInt(229, 242, 154),
			ColorFromInt(255, 241, 153),
			ColorFromInt(240, 182, 187),
			
			ColorFromInt(235, 249, 252),
			ColorFromInt(241, 249, 204),
			ColorFromInt(255, 252, 193),
			ColorFromInt(247, 222, 217),
			
			Color.black
		};
	}

}

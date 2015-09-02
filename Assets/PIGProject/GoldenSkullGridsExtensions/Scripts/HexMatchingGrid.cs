using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gamelogic;
using Gamelogic.Grids;
using PixelCrushers.DialogueSystem;

public class HexMatchingGrid : GridBehaviour<FlatHexPoint> 
{
	
	//need to create a array to save the point and command since it will be run agagin when the battle start
	public Player player;
	public SpriteCell pathPrefab;
	public GameObject pathRoot;

	private bool onAction = true; 
	private FlatHexPoint start;
	private FlatHexPoint finish;
	
	// Use this for initialization

	void Start () {
		player.CurrentPointLocation = FlatHexPoint.Zero;
		player.transform.position = new Vector3(Map[player.CurrentPointLocation].x, Map[player.CurrentPointLocation].y);
		start = player.CurrentPointLocation;
		//_playerPoint= startPoint;
		
		///<c>
		/// player.CurrentPointLocation -> DiamondPoint -> e.g) (1,0)
		/// Map[player.CurrentPointLocation] -> actually position ----->Map[].X, Map[].Y
		/// 
		/// 
		/// </c>
	}
	
	public void OnClick(FlatHexPoint point)
	{	

		onAction = !onAction; 

		//if(onAction) return;
		
		Debug.Log (point.BasePoint);   //return (x,y)
		
		if(player.CurrentPointLocation != null)
		{
			//StartCoroutine(Move (player.CurrentPointLocation , point));
			start = player.CurrentPointLocation;
			finish = point;
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
			yield return StartCoroutine(Move (pathList[i], pathList[i+1]));
		}
		//CallCommand();
	 }



	public IEnumerator Move(FlatHexPoint currentPoint, FlatHexPoint endPoint){
		float time = 0;
		const float totalTime = .3f;
		
		//onAction = true;
		
		while (time < totalTime)
		{
			float x = Mathf.Lerp(Map[currentPoint].x, Map[endPoint].x, time / totalTime);
			float y = Mathf.Lerp(Map[currentPoint].y, Map[endPoint].y, time / totalTime);

			y -= 20;

			player.transform.position = new Vector3(x, y);
			time += Time.deltaTime;
		}
		
		player.CurrentPointLocation  = endPoint;
		//yield return null;
		yield return new WaitForSeconds(0.05f);
	}
	
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
			return new Color(r/255.0f, g/255.0f, b/255.0f);
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

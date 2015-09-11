using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Gamelogic;
using Gamelogic.Grids;
using PixelCrushers.DialogueSystem;

public class HexGridMatching : GridBehaviour<FlatHexPoint> 
{
	
	//need to create a array to save the point and command since it will be run agagin when the battle start
	public PlayerView player;
	public EnemyView target;
	public SpriteCell pathPrefab;
	public GameObject pathRoot;
	public Text myText;

	private bool onAction = true; 
	private FlatHexPoint start;
	private FlatHexPoint finish;
	private Vector3 tempPoint;
	
	// Use this for initialization

	public override void InitGrid()
	{
		//if(player != null && target != null && Map != null){
			Debug.Log ("FuckYou");
			player.CurrentPointLocation = FlatHexPoint.Zero;
			player.transform.position = Map[FlatHexPoint.Zero];
			start = player.CurrentPointLocation;

			target.CurrentPointLocation = new FlatHexPoint(7, 4);
			target.transform.position = Map[new FlatHexPoint(7, 4)];
			InitPosition();
		//}

		///<c>
		/// player.CurrentPointLocation -> DiamondPoint -> e.g) (1,0)
		/// Map[player.CurrentPointLocation] -> actually position ----->Map[].X, Map[].Y
		/// 
		/// 
		/// </c>
	}

	public void InitPosition()
	{	
		//Debug.Log ("InitPosition");
		//player.transform.position = Map[FlatHexPoint.Zero];
		target.CurrentPointLocation = new FlatHexPoint(7, 4);
		target.transform.position = Map[new FlatHexPoint(7, 4)];
	}
	

	public void OnClick(FlatHexPoint point)
	{	
		//onAction = !onAction; 
		//if(onAction) return;
		//Debug.Log (point.BasePoint);   //return (x,y)
		InitPosition();
		if(player != null)
		{	
			if(player._State == PlayerState.MOVE)
			{
				start = player.CurrentPointLocation;
				finish = point;
				player.CurrentPointLocation = point;
				var path = Algorithms.AStar(Grid, start, finish);

					StartCoroutine(MovePath(path));
			}

			else if (player._State == PlayerState.ATTACK)
			{

				// (target.CurrentPointLocation == point && in Grid.GetAllNeighbors(player))
				foreach (var neighbor in Grid.GetAllNeighbors(player.CurrentPointLocation))
				{
					if(neighbor == point && neighbor == target.CurrentPointLocation)
					{
						//FindNearly cell, get the target
						//if point is one of neighbour, target = pointtarget
						Debug.Log ("You can Attack");
						Battle (player, target);
						return;
					}
				}
				Debug.Log ("You can't Attack");
				myText.text = "You can't Attack, Please Move";
				player._State = PlayerState.MOVE;

			}
		}
	}
	
	public void Battle(PlayerView p1, EnemyView p2)
	{
		p1._Quantity -=  p2._Power;
		p2._Quantity -=  p1._Power;

		myText.text =  "Your Quantity: " + p1._Quantity + " Target Quantity: " + p2._Quantity;
		Debug.Log("Player Quantity: " + p1._Quantity);
		Debug.Log("Target Quantity: " + p2._Quantity);

		player._State = PlayerState.MOVE;
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
			yield return StartCoroutine(player.Move (Map[pathList[i]], Map[pathList[i+1]]));
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

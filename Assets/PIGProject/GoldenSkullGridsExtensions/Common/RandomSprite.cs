using UnityEngine;

namespace Gamelogic.Grids.GoldenSkull
{
	/// <summary>
	/// The base of grid behaviour that can select random sprites and stack heights.
	/// </summary>
	/// <typeparam name="TPoint">The type of the point.</typeparam>
	[ExecuteInEditMode]
	public class RandomSprite<TPoint> : GridBehaviour<TPoint> where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// The sprites that can be randomly selected from.
		/// </summary>
		public Sprite[] sprites;

		/// <summary>
		/// The maximum stack height.
		/// </summary>
		public int maxStackHeight = 1;

		/// <summary>
		/// The maximum height offset.
		/// </summary>
		public float maxHeightOffset = 0;

		public void Init()
		{
			foreach (var point in Grid)
			{
				var cell = Grid[point].GetComponent<GSCell>();

				if (sprites.Length > 0)
				{
					cell.Sprite = sprites.RandomItem();
				}

				cell.StackHeight = Random.Range(1, maxStackHeight + 1);
				cell.HeightOffset = Random.value*maxHeightOffset;
			}
		}
	}
}
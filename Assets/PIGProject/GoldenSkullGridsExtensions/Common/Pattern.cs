using UnityEngine;

namespace Gamelogic.Grids.GoldenSkull
{
	/// <summary>
	/// The base of a grid behavoiour that can use a pattern (coloring) 
	/// to select sprites for cells. Requires a GSGridBehaviour of the same 
	/// point type.
	/// </summary>
	/// <typeparam name="TPoint">The type of the point.</typeparam>
	[ExecuteInEditMode]
	public class Pattern<TPoint> : GridBehaviour<TPoint> where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// The sprites to use for the pattern. Must be at least the number of colors of the coloring.
		/// </summary>
		public Sprite[] sprites;

		/// <summary>
		/// The stack heights to use for the pattern.  Must be at least the number of colors of the coloring.
		/// </summary>
		public int[] stackHeights;

		/// <summary>
		/// The coloring to use to make the pattern.
		/// </summary>
		public ColorFunction colorFunction;

		public void Init()
		{
			foreach (var point in Grid)
			{
				var cell = Grid[point].GetComponent<GSCell>();

				int colorIndex = GetColor(point);

				cell.Sprite = sprites[colorIndex];
				cell.StackHeight = stackHeights[colorIndex];
			}
		}

		/// <summary>
		/// Gets the color of the point, using the colorfunction. By defualt returns 0.
		/// </summary>
		protected virtual int GetColor(TPoint point)
		{
			return 0;
		}
	}
}
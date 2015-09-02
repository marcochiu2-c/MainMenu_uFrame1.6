using UnityEngine;

namespace Gamelogic.Grids.GoldenSkull
{
	/// <summary>
	/// The base class of grid behaviours that uses a heightmap to select sprites and stack heights.
	/// </summary>
	/// <typeparam name="TPoint">The type of the point.</typeparam>
	[ExecuteInEditMode]
	public class HeightMap<TPoint> : GridBehaviour<TPoint> where TPoint:IGridPoint<TPoint>
	{
		/// <summary>
		/// The sprites that this behaviour selects from for the grid from highest to lowest.
		/// </summary>
		public Sprite[] sprites;

		/// <summary>
		/// The height map to use to select sprites. This greyscale value of the color
		/// is used. This image should be tileable, it should be set to enable reading / writing, and its wrap mode should be set to repeat.
		/// </summary>
		public Texture2D heightMap;

		/// <summary>
		/// The maximum height
		/// </summary>
		public int maxStackHeight;

		/// <summary>
		/// The frequency to sample the image at.
		/// </summary>
		public float frequency;

		/// <summary>
		/// The height scale to use for offsets.
		/// </summary>
		public float heightScale = 40f;

		public bool flatFirstLevel = true;
		public float firstLevelHeightOffset = 0;

		public void Init()
		{
			var imageMap = GetImageMap();
		
			foreach (var point in Grid)
			{
				var cell = Grid[point].GetComponent<GSCell>();
			
				var imagePoint = imageMap[point];

				var floatLevel = heightMap.GetPixelBilinear(imagePoint.x, imagePoint.y).grayscale;
				var spriteIndex = (int) (floatLevel*(sprites.Length));

				if (spriteIndex > sprites.Length - 1)
				{
					spriteIndex = sprites.Length - 1;
				}

				cell.Sprite = sprites[spriteIndex];

				var height = (int)(floatLevel * (maxStackHeight));

				if (height > maxStackHeight - 1)
				{
					height = maxStackHeight - 1;
				}

				cell.StackHeight = height + 1;

				if (spriteIndex == 0 && flatFirstLevel)
				{
					cell.HeightOffset = firstLevelHeightOffset;
				}
				else
				{
					cell.HeightOffset = floatLevel * heightScale;
				}
			}
		}

		/// <summary>
		/// Gets a 2D map that converts grid points to image points.
		/// </summary>
		virtual protected IMap<TPoint> GetImageMap()
		{
			return null;
		}
	}
}
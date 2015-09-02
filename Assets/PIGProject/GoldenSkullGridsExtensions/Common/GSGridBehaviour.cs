using UnityEngine;

/// <summary>
/// Contains classes for making it possible to use Golden Skull tile art with Gamelogic Grids.
/// </summary>
namespace Gamelogic.Grids.GoldenSkull
{
	/// <summary>
	/// The base class of grid behaviours that can be used on editor grids to use GSCells which exposes
	/// functionality that makes use of the particular aspects of Golden Skull art. One of the base classes
	/// should be used in the editor: GSHexGridBehaviour or GSIsoGridBehaviour.
	/// </summary>
	/// <typeparam name="TPoint">The type of the point.</typeparam>
	public class GSGridBehaviour<TPoint> : GridBehaviour<TPoint> where TPoint : IGridPoint<TPoint>
	{
		public override void InitGrid()
		{
			var sprite = GridBuilder
				.CellPrefab
				.transform.GetChild(0)
				.GetComponent<SpriteRenderer>()
				.sprite;

			foreach (var point in Grid)
			{
				var cell = Grid[point].GetComponent<GSCell>();
				cell.Sprite = sprite;
				cell.SortingLayerOrder = CalcSortingLayerOrder(point);
			}

			//Use this to call InitGrid for other GridBehaviours
			//This *should* be called by the grid builder,
			//but the grid builder at the moment only calls the first InitGrid method.
			//To avoid recursive calls, we use the method Init instead.
			SendMessage("Init", SendMessageOptions.DontRequireReceiver);
		}

		/// <summary>
		/// Calculates the sorting layer order.
		/// </summary>
		/// <param name="point">The point to calculate the layer order for.</param>
		/// <returns>An integer that can be used as the sorting layer order.</returns>
		protected virtual int CalcSortingLayerOrder(TPoint point)
		{
			return 0;
		}
	}
}
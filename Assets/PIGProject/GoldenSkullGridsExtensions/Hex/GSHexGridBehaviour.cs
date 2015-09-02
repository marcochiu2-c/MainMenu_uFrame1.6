namespace Gamelogic.Grids.GoldenSkull
{
	public class GSHexGridBehaviour : GSGridBehaviour<FlatHexPoint>
	{
		protected override int CalcSortingLayerOrder(FlatHexPoint point)
		{
			return -(2*point.Y + point.X);
		}
	}
}
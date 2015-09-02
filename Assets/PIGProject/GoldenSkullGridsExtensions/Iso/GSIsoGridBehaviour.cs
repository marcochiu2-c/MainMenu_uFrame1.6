namespace Gamelogic.Grids.GoldenSkull
{
	public class GSIsoGridBehaviour : GSGridBehaviour<DiamondPoint>
	{
		protected override int CalcSortingLayerOrder(DiamondPoint point)
		{
			return -(point.X + point.Y);
		}
	}
}
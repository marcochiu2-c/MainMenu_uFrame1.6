namespace Gamelogic.Grids.GoldenSkull
{
	public class HexPattern : Pattern<FlatHexPoint>
	{
		protected override int GetColor(FlatHexPoint point)
		{
			return point.GetColor(
				colorFunction.x0,
				colorFunction.x1,
				colorFunction.y1);
		}
	}
}
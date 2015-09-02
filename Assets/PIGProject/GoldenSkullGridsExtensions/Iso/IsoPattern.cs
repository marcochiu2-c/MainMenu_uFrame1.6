namespace Gamelogic.Grids.GoldenSkull
{
	public class IsoPattern : Pattern<DiamondPoint>
	{
		protected override int GetColor(DiamondPoint point)
		{
			return point.GetColor(
				colorFunction.x0,
				colorFunction.x1,
				colorFunction.y1);
		}
	}
}
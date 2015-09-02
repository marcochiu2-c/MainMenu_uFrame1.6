using UnityEngine;

namespace Gamelogic.Grids.GoldenSkull
{
	public class IsoHeightMap : HeightMap <DiamondPoint>
	{
		protected override IMap<DiamondPoint> GetImageMap()
		{
			return new DiamondMap(Vector2.one)
				.Scale(new Vector2(2, 1) * frequency)
				.Translate(Random.value, Random.value);
		}
	}
}
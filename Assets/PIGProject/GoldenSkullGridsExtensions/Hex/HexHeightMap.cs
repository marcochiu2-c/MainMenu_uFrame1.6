using Gamelogic.Grids;
using Gamelogic.Grids.GoldenSkull;
using UnityEngine;

public class HexHeightMap : HeightMap <FlatHexPoint>
{
	protected override IMap<FlatHexPoint> GetImageMap()
	{
		return new FlatHexMap(Vector2.one)
			.Scale(new Vector2(2, 1) * frequency);
			//.Translate(Random.value, Random.value);
	}
}

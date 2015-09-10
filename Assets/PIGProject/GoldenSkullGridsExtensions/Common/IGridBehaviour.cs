using UnityEngine;
using System.Collections;
using Gamelogic;
using Gamelogic.Grids;

public interface IGridBehavior<TPoint> where TPoint : IGridPoint<TPoint>{

	void GridBehavior(TPoint tPoint);
}

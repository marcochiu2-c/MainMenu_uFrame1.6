using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gamelogic.Grids;

public class Player : MonoBehaviour {
	private FlatHexPoint _currentPointLocation;
		
	public FlatHexPoint CurrentPointLocation
	{
			get;
			set;
	}
}

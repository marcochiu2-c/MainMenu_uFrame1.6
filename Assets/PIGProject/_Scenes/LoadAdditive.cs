using UnityEngine;
using System.Collections;

public class LoadAdditive : MonoBehaviour {
	
	public void LoadAddOnClick(int Level)
	{
		Application.LoadLevelAdditive (Level);
	}
}

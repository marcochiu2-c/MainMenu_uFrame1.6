using UnityEngine;
using System.Collections;

namespace Utilities{
	public class GaemObjectSize : MonoBehaviour {

		public static Vector2 Size(GameObject og){
			var rt = og.transform as RectTransform;
			return new Vector2 (rt.rect.width, rt.rect.height);
		}
	}
}
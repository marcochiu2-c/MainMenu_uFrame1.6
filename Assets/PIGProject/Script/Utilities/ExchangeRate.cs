using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Utilities{
	public class ExchangeRate : MonoBehaviour {
		static int resource = 2000;
		static int feather  = 5;

		public static int GetStardustFromResource (int res){
			return Mathf.CeilToInt((float)res /resource);
		}

		public static int GetStardustFromFeather (int f){
			return Mathf.CeilToInt((float)f/feather);
		}
	}
}
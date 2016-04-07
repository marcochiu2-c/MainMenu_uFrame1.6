#define TEST
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utilities{
	public class ShowLog
	{

		public static void Log(string text){
#if TEST
			Debug.Log (text);
#endif
		}

		public static void Log(Object text){
#if TEST
			Debug.Log (text.ToString());
#endif
		}


	}
}
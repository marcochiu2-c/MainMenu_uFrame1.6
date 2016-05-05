#define TEST
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilities{
	public class ShowLog
	{
		
		public static void Log(object message){
			#if TEST
			UnityEngine.Debug.Log (DateTime.Now+": "+message);
			#endif
		}
		
		public static void Log(object message, UnityEngine.Object context){
			#if TEST
			UnityEngine.Debug.Log (DateTime.Now+": "+message, context);
			#endif
		}
		
		
	}
}
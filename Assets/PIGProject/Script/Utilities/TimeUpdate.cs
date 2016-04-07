using System;
using UnityEngine;
using System.Collections;

namespace Utilities{
	public class TimeUpdate : MonoBehaviour {
		
		public static string Time(DateTime eta){

			if (eta.ToString() == "01/01/0001 00:00:00") return "00:00:00";
			TimeSpan t = eta.Subtract (DateTime.Now);
			return Convert.ToInt32(t.TotalHours) + ":" + t.Minutes.ToString("00") + ":" + t.Seconds.ToString("00");
		}
	}
}
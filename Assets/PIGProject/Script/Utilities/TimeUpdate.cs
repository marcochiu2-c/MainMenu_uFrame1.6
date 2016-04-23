using System;
using UnityEngine;
using System.Collections;

namespace Utilities{
	public class TimeUpdate : MonoBehaviour {
		
		public static string Time(DateTime eta){
			if (eta < DateTime.Now) return "00:00:00";
			TimeSpan t = eta.Subtract (DateTime.Now);
			if (t.TotalHours > 99) {
				return ((int)t.TotalDays).ToString () + "日";
			} else {
				return Mathf.FloorToInt((float)t.TotalHours) + ":" + t.Minutes.ToString ("00") + ":" + t.Seconds.ToString ("00");
			}
		}

		public static string Time(TimeSpan ts){
			if (ts.TotalHours > 99) {
				return ((int)ts.TotalDays).ToString () + "日";
			} else {
				return Mathf.FloorToInt((float)ts.TotalHours) + ":" + ts.Minutes.ToString ("00") + ":" + ts.Seconds.ToString ("00");
			}
		}
	}
}
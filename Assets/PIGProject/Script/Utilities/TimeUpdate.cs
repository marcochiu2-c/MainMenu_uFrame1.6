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
				return Convert.ToInt32 (t.TotalHours) + ":" + t.Minutes.ToString ("00") + ":" + t.Seconds.ToString ("00");
			}
		}
	}
}
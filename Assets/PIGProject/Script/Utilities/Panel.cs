using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Utilities{
	public class Panel : MonoBehaviour	{
		
		public static Button GetConfirmButton(GameObject obj){
			return obj.transform.GetChild(2).GetChild(0).GetComponent<Button>();
		}

		public static Button GetCancelButton(GameObject obj){
			return obj.transform.GetChild(2).GetChild(1).GetComponent<Button>();
		}

		public static Button GetButtomFromButtonHolder(GameObject obj, int index){
			return obj.transform.GetChild(2).GetChild(index).GetComponent<Button>();
		}

		public static Text GetHeader(GameObject obj){
			return obj.transform.GetChild(0).GetComponent<Text>();
		}

		public static Text GetMessageText(GameObject obj){
			return obj.transform.GetChild(1).GetComponent<Text>();
		}


	}
}

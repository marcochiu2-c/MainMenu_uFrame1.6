using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NoticeText : MonoBehaviour {

	Text theText;

//	private IEnumerator Check()
//	{
//		WWW w = new WWW(MainScene.noticeURL); // TODO URL have to be finalized.
//		Debug.Log (MainScene.noticeURL);
//		yield return w;
//		if (w.error != null)
//		{
//			Debug.Log("Error .. " +w.error);
//			// for example, often 'Error .. 404 Not Found'
//		}
//		else
//		{
//			Debug.Log("Found ... ==>" +w.text +"<==");
//			webText = w.text;
//			theText.text = webText;
//			// don't forget to look in the 'bottom section'
//			// of Unity console to see the full text of
//			// multiline console messages.
//		}
//		
//		/* example code to separate all that text in to lines:
//         longStringFromFile = w.text
//         List<string> lines = new List<string>(
//             longStringFromFile
//             .Split(new string[] { "\r","\n" },
//             StringSplitOptions.RemoveEmptyEntries) );
//         // remove comment lines...
//         lines = lines
//             .Where(line => !(line.StartsWith("//")
//                             || line.StartsWith("#")))
//             .ToList();
//         */
//		
//	}


	// Use this for initialization
	void Start () {
		Debug.Log ( Regex.Replace(MainScene.noticeText,
		                         @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
		                         "<a target='_blank' href='$1'>$1</a>"));
		theText = transform.GetChild (1).GetChild (0).GetComponent<Text>();
		theText.text = MainScene.noticeText;
	}
}
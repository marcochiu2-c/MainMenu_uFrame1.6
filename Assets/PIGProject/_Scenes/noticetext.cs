using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class noticetext : MonoBehaviour {
	
	public Text theText;
	
	public TextAsset textFile;
	public string[] textLines;
	string webText="";
	
	private IEnumerator Check()
	{
		WWW w = new WWW("http://www.shawnyip.info/Notice.txt"); //TODO: change link
		Debug.Log ("http://www.shawnyip.info/Notice.txt");
		yield return w;
		if (w.error != null)
		{
			Debug.Log("Error .. " +w.error);
			// for example, often 'Error .. 404 Not Found'
		}
		else
		{
			Debug.Log("Found ... ==>" +w.text +"<==");
			webText = w.text;
			theText.text = webText;
			// don't forget to look in the 'bottom section'
			// of Unity console to see the full text of
			// multiline console messages.
		}
		
		/* example code to separate all that text in to lines:
         longStringFromFile = w.text
         List<string> lines = new List<string>(
             longStringFromFile
             .Split(new string[] { "\r","\n" },
             StringSplitOptions.RemoveEmptyEntries) );
         // remove comment lines...
         lines = lines
             .Where(line => !(line.StartsWith("//")
                             || line.StartsWith("#")))
             .ToList();
         */
		
	}
	
	
	// Use this for initialization
	void Start () {
		//theText = GameObject.Find ("/_MainMenuSceneRoot//Canvas/NoticeHolder/NoticeHolder/NoticeMask/Text").GetComponent <Text>();
		StartCoroutine (Check ());
	}
}
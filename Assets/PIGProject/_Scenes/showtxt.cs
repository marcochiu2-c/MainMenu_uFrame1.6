using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using System.IO;

public class showtxt : MonoBehaviour {

	public GameObject showTextarea;

	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	private int t;

	// Use this for initialization
	void Start () 
	{
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}

		print(textFile.text);

		theText.text = (textLines [0] + "\n\n" + textLines [2] + "\n\n" + textLines [3] +"\n\n" + textLines [4] + "\n\n" + textLines [5] + "\n\n" + textLines [6]);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

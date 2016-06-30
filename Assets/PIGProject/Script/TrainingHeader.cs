using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrainingHeader : MonoBehaviour {
/*
	public TextAsset TextFile; 
	public Text text;
	
	void Start() { 
		string[] linesInFile = TextFile.text.Split('\n');
		
		foreach (string line in linesInFile)
		{
			Debug.Log(line);
			text.text = (line);
		}
		
	}
*/

	
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
		
		theText.text = (textLines [0] + "\n\n" + textLines [2]);
		
		
	}

}

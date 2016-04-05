using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrainingEvent : MonoBehaviour {
	
	public Text CourageHeader, CourageEvent1, CourageEvent2, CourageEvent3, CourageEvent4, 
				ForceHeader, ForceEvent1, ForceEvent2, ForceEvent3, ForceEvent4, 
				StrengthHeader, StrengthEvent1, StrengthEvent2, StrengthEvent3, StrengthEvent4;
	
	public TextAsset CourageFile, ForceFile, StrengthFile;
	public string[] CourageTxt, ForceTxt, StrengthTxt;
	
	// Use this for initialization
	void Start () 
	{
		if (CourageFile != null) {
			CourageTxt = (CourageFile.text.Split ('\n'));
		}
		
		print(CourageFile.text);
		
		CourageHeader.text = (CourageTxt [1] + "\n\n" + CourageTxt [3]);
		CourageEvent1.text = (CourageTxt [6] + "\n" + CourageTxt [7] + "\n" + CourageTxt [8]);
		CourageEvent2.text = (CourageTxt [11] + "\n" + CourageTxt [12] + "\n" + CourageTxt [13]);
		CourageEvent3.text = (CourageTxt [16] + "\n" + CourageTxt [17] + "\n" + CourageTxt [18]);
		CourageEvent4.text = (CourageTxt [21] + "\n" + CourageTxt [22] + "\n" + CourageTxt [23]);

		if (ForceFile != null) {
			ForceTxt = (ForceFile.text.Split ('\n'));
		}

		print (ForceFile.text);

		ForceHeader.text = (ForceTxt [1] + "\n\n" + ForceTxt [3]);
		ForceEvent1.text = (ForceTxt [6] + "\n" + ForceTxt [7] + "\n" + ForceTxt [8]);
		ForceEvent2.text = (ForceTxt [11] + "\n" + ForceTxt [12] + "\n" + ForceTxt [13]);
		ForceEvent3.text = (ForceTxt [16] + "\n" + ForceTxt [16] + "\n" + ForceTxt [16]);
		ForceEvent4.text = (ForceTxt [18] + "\n" + ForceTxt [18] + "\n" + ForceTxt [18]);

		if(StrengthFile != null) {
			StrengthTxt = (StrengthFile.text.Split ('\n'));
		}

		print (StrengthFile.text);

		StrengthHeader.text = (StrengthTxt [1] + "\n\n" + StrengthTxt [3]);
		StrengthEvent1.text = (StrengthTxt [6] + "\n" + StrengthTxt [7] + "\n" + StrengthTxt [8]);
		StrengthEvent2.text = (StrengthTxt [11] + "\n" + StrengthTxt [11] + "\n" + StrengthTxt [11]);
		StrengthEvent3.text = (StrengthTxt [13] + "\n" + StrengthTxt [13] + "\n" + StrengthTxt [13]);
		StrengthEvent4.text = (StrengthTxt [15] + "\n" + StrengthTxt [15] + "\n" + StrengthTxt [15]);
	}

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderChangeValue : MonoBehaviour {

	string sliderTextString = "0";
	public Text sliderText; // public is needed to ensure it's available to be assigned in the inspector.
		
	public void textUpdate (float textUpdateNumber)
	{
		sliderTextString = textUpdateNumber.ToString ("0.00") + "%";
		sliderText.text = sliderTextString;
	}
}
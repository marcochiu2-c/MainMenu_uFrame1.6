using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderChangeValue : MonoBehaviour {

	string sliderTextString = "0";
	public Text sliderText; // public is needed to ensure it's available to be assigned in the inspector.

	public Button addValue;

	public void textUpdate (float textUpdateNumber)
	{
		sliderTextString = textUpdateNumber.ToString ("0.00") + "%";
		sliderText.text = sliderTextString;
	}

	public void valueUpdate(float valueUpdateNumber){
		sliderTextString = sliderText.text + 1;
//		sliderText.text = sliderTextString;
	}
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SchoolFieldWeapon : MonoBehaviour {
	public Text Name;
	public Text Quantity;
	public Text Category;
	public Text Value;

	// Use this for initialization
	void Start () {

	}


	public void SetSchoolFieldWeapon(string name, string quantity, string category, string value){
		Name.text = name;
		Quantity.text = quantity;
		Category.text = category;
		Value.text = value;
	}

	// Update is called once per frame
	void Update () {
	
	}
}

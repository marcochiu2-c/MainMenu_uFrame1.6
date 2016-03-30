using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SchoolFieldWeapon : MonoBehaviour {
	public Text Name;
	public Text Quantity;
	public Text Category;
	public Text Value;
	public int weaponId;

	// Use this for initialization
	void Start () {

	}


	public void SetSchoolFieldWeapon(int id, string name, string quantity, string category, string value){
		weaponId = id;
		Name.text = name;
		Quantity.text = quantity;
		Category.text = category;
		Value.text = value;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnPrefabClick(){
		if (weaponId > 5000 && weaponId < 6000) {
			SchoolField.AssigningWeaponId = weaponId;
		} else if (weaponId > 6000 && weaponId < 7000) {
			SchoolField.AssigningArmorId = weaponId;
		} else if (weaponId > 7000 && weaponId < 8000) {
			SchoolField.AssigningShieldId = weaponId;
		}
		Transform t = this.transform.parent;
		t.parent.parent.parent.gameObject.SetActive (false);
	}
}

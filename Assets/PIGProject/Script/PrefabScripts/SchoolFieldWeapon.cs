using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class SchoolFieldWeapon : MonoBehaviour {
	public Text Name;
	public Text Quantity;
	public Text Category;
	public Text Value;
	public int weaponId;
	public int soldier;
	// Use this for initialization
	void Start () {

	}


	public void SetSchoolFieldWeapon(int s, int wid, string name, string quantity, string category, string value){
		soldier = s;
		weaponId = wid;
		Name.text = name;
		Quantity.text = quantity;
		Category.text = category;
		Value.text = value;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void OnPrefabClick(){
		Game game = Game.Instance;
		WsClient wsc = WsClient.Instance;
		string type = "";
		if (weaponId > 5000 && weaponId < 6000) {
			SchoolField.AssigningWeaponId = weaponId;
			type = "weapon";
		} else if (weaponId > 6000 && weaponId < 7000) {
			SchoolField.AssigningArmorId = weaponId;
			type = "armor";
		} else if (weaponId > 7000 && weaponId < 8000) {
			SchoolField.AssigningShieldId = weaponId;
			type = "shield";
		}
		Transform t = this.transform.parent;
		t.parent.parent.parent.gameObject.SetActive (false);
		if (game.soldiers [soldier - 1].attributes [type].AsInt == 0) {
//			if (type == "weapon") {
//				game.soldiers[soldier-1].attributes["weapon"].AsInt = weaponId;
//			}else if (type == "armor") {
//				game.soldiers[soldier-1].attributes["armor"].AsInt = weaponId;
//			}else if (type == "shield") {
//				game.soldiers[soldier-1].attributes["shield"].AsInt = weaponId;
//			}
//			JSONClass json = new JSONClass ();
//			json.Add ("id", new JSONData (game.soldiers [soldier - 1].id));
//			json.Add ("userId", new JSONData (game.login.id));
//			json.Add ("json", game.soldiers [soldier - 1].attributes);
//			wsc.Send ("soldier", "SET", json);
//			Debug.Log (game.soldiers [soldier - 1].attributes);
			SchoolField.CheckArmedEquipmentAvailability();
		} else {
			SchoolField.OnReplaceItem (weaponId,type);
		}

	}
}

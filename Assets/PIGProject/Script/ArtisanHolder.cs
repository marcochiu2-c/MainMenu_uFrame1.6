#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using Utilities;
using System.Collections.Generic;

public class ArtisanHolder : MonoBehaviour {
	Game game;
	public GameObject ArtisanWeaponPanel;
	public GameObject ArtisanArmorPanel; // All protective equipments

	// Use this for initialization
	void Start () {
		CallArtisanHolder ();
	}

	public void CallArtisanHolder(){
		game = Game.Instance;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetPanel(string panel,int quantity){
		List<WeaponMaking> wsi = null;
		List<ArmorMaking> asi = null;  //TODO: change the list type to ArmorMaking once setup
		GameObject sp=null;
		WeaponMaking wobj = null;
		ArmorMaking aobj = null;
		if (panel == "Weapon") {
			wsi = WeaponMaking.Weapons;
			sp = ArtisanWeaponPanel;
			for (var i = 0; i < quantity; i++) { 
				wobj = Instantiate(Resources.Load("WeaponMakingPrefab") as GameObject).GetComponent<WeaponMaking>();
				wobj.transform.parent = sp.transform;
				wobj.ResourceRequire.text = "";

				RectTransform rTransform = wobj.GetComponent<RectTransform>();
				wsi.Add (wobj);

				rTransform.localScale=Vector3.one;
			}
		}else {
			asi = ArmorMaking.Armors;
			sp = ArtisanArmorPanel;
			for (var i = 0; i < quantity; i++) { 
				aobj = Instantiate(Resources.Load("WeaponMakingPrefab") as GameObject).GetComponent<ArmorMaking>();
				aobj.transform.parent = sp.transform;
				aobj.transform.parent = sp.transform;
				RectTransform rTransform = aobj.GetComponent<RectTransform>();
				asi.Add (aobj);

				rTransform.localScale=Vector3.one;
			}
		}
		

		
	}
}

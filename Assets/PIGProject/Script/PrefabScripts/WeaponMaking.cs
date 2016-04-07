#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using SimpleJSON;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class WeaponMaking : MonoBehaviour {
	public Text WeaponName;
	public Text ResourceRequire;
	public Text Quantity;
	public Button Metalsmith;
	public Text TimeRequire;
	public Text Details;
	public int id;
	DateTime eta;
	public DateTime etaTimestamp;
	TimeSpan ts;
	Color color;


	public static List<WeaponMaking> Weapons = new List<WeaponMaking>();
	public static List<WeaponMaking> Armors = new List<WeaponMaking>();
	public static List<WeaponMaking> Shields = new List<WeaponMaking>();
	// Use this for initialization
	void Start () {
		Color.TryParseHexString ("#FFFFF12", out color);
		AddButtonListener ();
	}

	void AddButtonListener(){
		Metalsmith.onClick.AddListener (() => {
			//GameObject ArtisanConfirmPopup = transform.parent.parent.parent.parent.GetChild(5).gameObject;
			//SetArtisanConfirmPopupText(ArtisanConfirmPopup);
			ArtisanHolder.IdEquipmentToBeProduced = id;
			ShowPanel(transform.parent.parent.parent.parent.GetChild(8).gameObject);

		});
	}

	public void SetPanel(Products p,int q, DateTime endTime){
//	public void SetPanel(string wn, int rr, string q, string ms, DateTime endTime, string details){ 
		WeaponName.text = p.name;
		ResourceRequire.text = p.attributes["NumberOfProductionResources"].ToString ();
		Quantity.text = q.ToString();
//		Metalsmith.text = ms;
		eta = endTime;
		InvokeRepeating ("UpdateRemainingTime", 0, 1);
		id = p.id;
//		Details.text = details;
	}

	public void UpdateRemainingTime(){
		if (eta > DateTime.Now) {
			ts = eta.Subtract (DateTime.Now);
			TimeRequire.text = string.Format( "{0:D2}:{1:D2}:{2:D2}", ts.Hours, ts.Minutes, ts.Seconds);
			transform.GetComponent<Image>().color = Color.green;
		} else {
			TimeRequire.text = "00:00:00";
			transform.GetComponent<Image>().color = color;
		}
	}

	// Update is called once per frame
	void Update () {
#if NOTTEST
//		TimeRequire.text = Utilities.TimeUpdate.Time (etaTimestamp); 
#endif
	}

	void ShowPanel(GameObject panel){
		transform.parent.parent.parent.parent.GetChild(4).gameObject.SetActive (true);  //Show Disable Panel mask
		panel.SetActive (true);
	}
}

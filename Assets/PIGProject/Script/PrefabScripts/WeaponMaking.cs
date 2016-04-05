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
	public Text Metalsmith;
	public Text TimeRequire;
	public Text Details;
	DateTime eta;
	public DateTime etaTimestamp;

	public static List<WeaponMaking> Weapons = new List<WeaponMaking>();
	// Use this for initialization
	void Start () {
	
	}

	public void SetPanel(string wn, int rr, string q, string ms, DateTime endTime, string details){ 
		WeaponName.text = wn;
		ResourceRequire.text = rr.ToString ();
		Quantity.text = q;
		Metalsmith.text = ms;
		eta = endTime;
		InvokeRepeating ("UpdateRemainingTime", 0, 1);
		Details.text = details;
	}

	public void UpdateRemainingTime(){
		TimeRequire.text = eta.Subtract (DateTime.Now).ToString();
	}

	// Update is called once per frame
	void Update () {
#if NOTTEST
//		TimeRequire.text = Utilities.TimeUpdate.Time (etaTimestamp); 
#endif
	}
}

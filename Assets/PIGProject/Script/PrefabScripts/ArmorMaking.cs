﻿#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using WebSocketSharp;
using SimpleJSON;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class ArmorMaking : MonoBehaviour {
	public Text ArmorName;
	public Text ResourceRequire;
	public Text Quantity;
	public Text Metalsmith;
	public Text TimeRequire;
	public Text Details;

	public DateTime etaTimestamp;

	public static List<ArmorMaking> Armors = new List<ArmorMaking>();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if NOTTEST
//		TimeRequire.text = Utilities.TimeUpdate.Time (etaTimestamp); 
#endif
	}
}

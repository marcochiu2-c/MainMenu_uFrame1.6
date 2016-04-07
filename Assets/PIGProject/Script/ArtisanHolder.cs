#define TEST

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;
using Utilities;
using System;
using System.Collections.Generic;

public class ArtisanHolder : MonoBehaviour {
	Game game;
	public GameObject ArtisanWeaponPanel;
	public GameObject ArtisanArmorPanel;
	public GameObject ArtisanShieldPanel;
	Button BackButton;
	Button CloseButton;
	static ProductDict p  = new ProductDict();
	static int IdWeaponWhichProducing=0;
	static int IdArmorWhichProducing=0;
	static int IdShieldWhichProducing=0;
	int latestEta = 0;
	// Use this for initialization
	void Start () {
		CallArtisanHolder ();
	}

	public void CallArtisanHolder(){
		game = Game.Instance;
		BackButton = transform.GetChild (1).GetComponent<Button> ();
		CloseButton = transform.GetChild (2).GetComponent<Button> ();
		AddButtonListener ();
		SetPanel (game.weapon);
		SetPanel (game.armor);
		SetPanel (game.shield);
		latestEta = GetLatestEta ();
		InvokeRepeating ("updateProductionEtaTimeText", 0, 1);
	}

	void AddButtonListener(){
		BackButton.onClick.AddListener (() => {
			ArtisanWeaponPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanArmorPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanShieldPanel.transform.parent.parent.gameObject.SetActive(false);
			transform.GetChild(0).gameObject.SetActive(true);
		});
		CloseButton.onClick.AddListener (() => {
			ArtisanWeaponPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanArmorPanel.transform.parent.parent.gameObject.SetActive(false);
			ArtisanShieldPanel.transform.parent.parent.gameObject.SetActive(false);
			gameObject.SetActive(false);
		});
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void SetPanel<T>(List<T> weapon){
		List<WeaponMaking> wsi = null;
		WeaponMaking wobj = null;
		int quantity = weapon.Count;
		Products products;
		string panel = weapon[0].GetType ().ToString ();
		HighlightProceedingJobs ();
		for (var i = 0; i < quantity; i++) {
			wobj = Instantiate(Resources.Load("WeaponMakingPrefab") as GameObject).GetComponent<WeaponMaking>();
			if (panel == "Weapon") {
				wsi = WeaponMaking.Weapons;
				wobj.transform.parent = ArtisanWeaponPanel.transform;
				Weapon x = weapon[i] as Weapon;
				if (IdWeaponWhichProducing== x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[0].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
			}else if (panel == "Armor"){
				wsi = WeaponMaking.Armors;
				wobj.transform.parent = ArtisanArmorPanel.transform;
				Armor x = weapon[i] as Armor;
				if (IdArmorWhichProducing == x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[0].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
			}else if (panel == "Shield"){
				wsi = WeaponMaking.Armors;
				wobj.transform.parent = ArtisanShieldPanel.transform;
				Shield x = weapon[i] as Shield;
				if(IdShieldWhichProducing == x.type){
					wobj.SetPanel(p.products[x.type],x.quantity,game.artisans[0].etaTimestamp);
				}else{
					wobj.SetPanel(p.products[x.type],x.quantity,DateTime.Now);
				}
			}
			RectTransform rTransform = wobj.GetComponent<RectTransform>();
			wsi.Add (wobj);
			rTransform.localScale=Vector3.one;
		}

	}

	void HighlightProceedingJobs(){
		if (game.artisans [0].etaTimestamp > DateTime.Now) {
			IdWeaponWhichProducing = game.artisans[0].targetId;
		}else if (game.artisans [1].etaTimestamp > DateTime.Now) {
			IdArmorWhichProducing = game.artisans[1].targetId;
		}else if (game.artisans [2].etaTimestamp > DateTime.Now) {
		}
	}

	int GetLatestEta(){
		int last = 0;
		if (game.artisans [0].etaTimestamp < game.artisans [1].etaTimestamp) {
			last = 1;
			if (game.artisans [1].etaTimestamp < game.artisans [2].etaTimestamp) {
				last = 2;
			}
		} else if (game.artisans [0].etaTimestamp < game.artisans [2].etaTimestamp){
			last = 2;
		}
		return last;
	}

	void updateProductionEtaTimeText(){
		if (game.artisans [latestEta].etaTimestamp > DateTime.Now) {
			TimeSpan ts = game.artisans [latestEta].etaTimestamp.Subtract (DateTime.Now);
			transform.GetChild (0).GetChild (2).GetChild (3).GetComponent<Text> ().text = string.Format ("生產中 {0:D2}:{1:D2}:{2:D2} 後完成", ts.Hours, ts.Minutes, ts.Seconds);
		} else {
			transform.GetChild (0).GetChild (2).GetChild (3).GetComponent<Text> ().text = "生產中 00:00:00 後完成";
		}
	}
}

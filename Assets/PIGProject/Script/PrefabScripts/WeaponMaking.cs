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
	public Button TimeRequire;
	public Button Details;
	public int id;
	public DateTime eta;
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
			OnMetalsmithClicked();
			ArtisanHolder.IdEquipmentToBeProduced = id;
			
		});
		TimeRequire.onClick.AddListener (() => {
			if (eta>DateTime.Now){
				ArtisanHolder.IdEquipmentToBeProduced = id;
				SetSpeedUpText();
				ShowPanel(ArtisanHolder.staticSpeedUpPopup);
			}
		});
		Details.onClick.AddListener (() => {
			SetDetailText();
			ShowPanel(ArtisanHolder.staticDetailPanel);
		});
	}

	void SetDetailText(){
		ProductDict p = ProductDict.Instance;
		string msg1 = "裝備詳細\n可下拉\n";
		string msg2 = "\n\n對\n主公\n返回最頂吧";
		string details = "";
		string colonAndTab = "\t：\t";
		Text tPanel = ArtisanHolder.staticDetailPanel.transform.GetChild (1).GetChild (0).GetComponent<Text>();
		if (id > 5000 && id < 6000) {
			details += "\n武器名稱"+colonAndTab+p.products[id].name;
			details += "\n等級"+colonAndTab+p.products[id].attributes["Grade"];
			details += "\n所需科技"+colonAndTab+p.products[id].attributes["TechnologyRequired"];
			details += "\n可否裝備盾"+colonAndTab+(p.products[id].attributes["EquipShield"].AsBool ? "可":"不可");
			details += "\n鋒利度"+colonAndTab+p.products[id].attributes["Sharp"];
			details += "\n穿透力"+colonAndTab+p.products[id].attributes["Penetrate"];
			details += "\n重量"+colonAndTab+p.products[id].attributes["Weight"];
			details += "\n體格應用"+colonAndTab+p.products[id].attributes["PhysicalApplications"];
			details += "\n其他命中"+colonAndTab+p.products[id].attributes["OtherHits"];
			details += "\n要害命中"+colonAndTab+p.products[id].attributes["KeyHit"];
			details += "\n致命要害命中"+colonAndTab+p.products[id].attributes["FatalKeyHit"];
			details += "\n製作資源數"+colonAndTab+p.products[id].attributes["NumberOfProductionResources"];
			details += "\n製作時間"+colonAndTab+p.products[id].attributes["ProductionTime"];
		} else if (id > 6000 && id < 7000) {
			details += "\n防具名稱"+colonAndTab+p.products[id].name;
			details += "\n特性"+colonAndTab+p.products[id].attributes["Characteristic"];
			details += "\n等級"+colonAndTab+p.products[id].attributes["Grade"];
			details += "\n所需科技"+colonAndTab+p.products[id].attributes["TechnologyRequired"];
			details += "\n硬度"+colonAndTab+p.products[id].attributes["Hardness"];
			details += "\n重量"+colonAndTab+p.products[id].attributes["Weight"];
			details += "\n其他覆蓋"+colonAndTab+p.products[id].attributes["OtherCover"];
			details += "\n要害覆蓋"+colonAndTab+p.products[id].attributes["KeyCover"];
			details += "\n致命要害覆蓋"+colonAndTab+p.products[id].attributes["FatalKeyCover"];
			details += "\n製作資源數"+colonAndTab+p.products[id].attributes["NumberOfProductionResources"];
			details += "\n製作時間"+colonAndTab+p.products[id].attributes["ProductionTime"];
		} else if (id > 7000 && id < 8000) {
			details += "\n盾名稱"+colonAndTab+p.products[id].name;
//			details += "\n特性"+colonAndTab+p.products[id].attributes["Characteristic"];
			details += "\n等級"+colonAndTab+p.products[id].attributes["Grade"];
			details += "\n所需科技"+colonAndTab+p.products[id].attributes["TechnologyRequired"];
			details += "\n剋制"+colonAndTab+p.products[id].attributes["Restraint"];
			details += "\n硬度"+colonAndTab+p.products[id].attributes["Hardness"];
			details += "\n重量"+colonAndTab+p.products[id].attributes["Weight"];
			details += "\n製作資源數"+colonAndTab+p.products[id].attributes["NumberOfProductionResources"];
			details += "\n製作時間"+colonAndTab+p.products[id].attributes["ProductionTime"];
		}
		tPanel.text = msg1 + details + msg2;
	}

	void OnMetalsmithClicked(){
		Game game = Game.Instance;
		int type = 0;
		if (id > 5000 && id < 6000) {
			type = 0;
		} else if (id > 6000 && id < 7000) {
			type = 1;
		} else if (id > 7000 && id < 8000) {
			type = 2;
		}
		if (game.artisans [type].etaTimestamp <= DateTime.Now) {
			GameObject EquipmentQHolder = ArtisanHolder.staticEquipmentQHolder;
			EquipmentQHolder.SetActive(true);
		} else {
			if (game.artisans [type].targetId == id){
				ArtisanHolder.CancelType = type;
				ArtisanHolder.CancelId   = id;
				SetCancelPanel();
				ShowPanel(ArtisanHolder.staticJobCancelPopup); // Cancel Panel
			}else{
				HidePanel(ArtisanHolder.staticJobCancelPopup); // Cancel Panel
			}
		}
	}

	void SetCancelPanel(){
		Game game = Game.Instance;	
		GameObject JobCancelPopup = ArtisanHolder.staticJobCancelPopup;
		string msg = "主公，裝備製作需時，確定取消嗎？";
		if (game.artisans [ArtisanHolder.CancelType].status == 4) {
			msg = "主公，這項工作不能取消";
			JobCancelPopup.transform.GetChild (2).GetChild (1).gameObject.SetActive (false);
		}else{
			JobCancelPopup.transform.GetChild (2).GetChild (1).gameObject.SetActive (true);
		}
		JobCancelPopup.transform.GetChild (1).GetComponent<Text> ().text = msg;
	}

	void SetSpeedUpText(){  // 10 Stardust for 1 hour
		Game game = Game.Instance;
		DateTime time = DateTime.Now;
		if (id == game.artisans [0].targetId) {
			time = game.artisans[0].etaTimestamp;
		}else if (id == game.artisans [1].targetId) {
			time = game.artisans[1].etaTimestamp;
		}else if (id == game.artisans [2].targetId) {
			time = game.artisans[2].etaTimestamp;
		}
		TimeSpan tdiff = time - DateTime.Now;
		string msg = "主公，使用amount星塵進行加速嗎？";
		ArtisanHolder.IdEquipmentToBeProduced = id;
		msg = msg.Replace ("amount", (((int) (tdiff.TotalHours*10))).ToString ());
		Utilities.Panel.GetMessageText (ArtisanHolder.staticSpeedUpPopup).text = msg;
	}
		                  
	 public void SetPanel(Products p,int q, DateTime endTime){
			//    public void SetPanel(string wn, int rr, string q, string ms, DateTime endTime, string details){ 
		WeaponName.text = p.name;
		ResourceRequire.text = p.attributes["NumberOfProductionResources"].ToString ();
		Quantity.text = q.ToString();
		//        Metalsmith.text = ms;
		eta = endTime;
		InvokeRepeating ("UpdateRemainingTime", 0, 1);
		InvokeRepeating ("UpdateButtonName", 0, 1);
		id = p.id;
		//        Details.text = details;
	}
		
	public void UpdateRemainingTime(){
		ProductDict p = ProductDict.Instance;

		if (eta >= DateTime.Now) { // Job of this prefab ongoing
			ts = eta.Subtract (DateTime.Now);
			TimeRequire.transform.GetChild(0).GetComponent<Text>().text = Utilities.TimeUpdate.Time(ts);
			transform.GetComponent<Image>().color = Color.green;
		} else { // Job not started
			TimeRequire.transform.GetChild(0).GetComponent<Text>().text = p.products[id].attributes["ProductionTime"]+"s";
			transform.GetComponent<Image>().color = color;
		}
	}

	public void UpdateButtonName(){
		ProductDict p = ProductDict.Instance;

		if (eta > DateTime.Now) {

			ts = eta.Subtract (DateTime.Now);
//			Metalsmith.transform.GetChild(0).GetComponent<Text>().text = "取消";
			Utilities.Panel.GetHeader(Metalsmith.gameObject).text = "取消";
		} else {
//			Metalsmith.transform.GetChild(0).GetComponent<Text>().text = "鍛冶";
			Utilities.Panel.GetHeader(Metalsmith.gameObject).text = "鍛冶";
		}
	}
	
	// Update is called once per frame
	void Update () {
		#if NOTTEST
		//        TimeRequire.text = Utilities.TimeUpdate.Time (etaTimestamp); 
		#endif
	}
	
	void ShowPanel(GameObject panel){
		ArtisanHolder.staticDisablePopup.SetActive (true);  //Show Disable Panel mask
		panel.SetActive (true);
	}
	void HidePanel(GameObject panel){
		ArtisanHolder.staticDisablePopup.SetActive (false);  //Show Disable Panel mask
		panel.SetActive (false);
	}
}

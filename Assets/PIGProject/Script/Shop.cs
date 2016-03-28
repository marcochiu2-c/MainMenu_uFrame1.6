using UnityEngine;
using OnePF;
using System.Collections;
using System.Collections.Generic;
//using Soomla;
//using Soomla.Profile;
using SimpleJSON;
using System;
using UnityEngine.UI;


public class ProductDesc{
	public string name;
	public int quantity=0;
	public ProductDesc(string n, int q){
		name = n;
		quantity = q;
	}
}


public class Shop : MonoBehaviour {
	const string STARDUST_250 = "stardust_250";
	const string STARDUST_550 = "stardust_550";
	const string STARDUST_1300 = "stardust_1300";
	const string STARDUST_2625 = "stardust_2625";
	const string STARDUST_4000 = "stardust_4000";
	const string STARDUST_8500 = "stardust_8500";
	const string RESOURCE_250 = "resource_250";
	const string RESOURCE_550 = "resource_550";
	const string RESOURCE_1300 = "resource_1300";
	const string RESOURCE_2625 = "resource_2625";
	const string RESOURCE_4000 = "resource_4000";
	const string RESOURCE_8500 = "resource_8500";
	const string FEATHER_250 = "feather_250";
	const string FEATHER_550 = "feather_550";
	const string FEATHER_1300 = "feather_1300";
	const string FEATHER_2625 = "feather_2625";
	const string FEATHER_4000 = "feather_4000";
	const string FEATHER_8500 = "feather_8500";
	const string FIRST_CHARGE = "first_charge";

	Dictionary<string,ProductDesc> prodIdDict;
	Dictionary<string,int> currencyDict;
	
	const string MONTHLY_SUBSCRIPTION = "monthly_subscription";
	const string SKU="";
	string _label = "";	
	Inventory _inventory = null;
	ProductList prodList = new ProductList();
//	List<Purchase> UnConsumedInventory;
//	bool UnConsumedInventoryNewUpdate=false;
	bool monthlySubscriptionBought = false;
	bool _isInitialized = false;
	Game game;
	WsClient wsc;
	public Button firstChargeButton;
	public Button monthlySubscriptionButton;

	string googlePublicKey ="MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgQiRcQJ6Gv0KaZm3SRQ2qntuyiHgrA2cXhwXsOc3aTF3hzncp86wIAHGmlblZDo6g04YLdSJxuEpqXmEW77utWtRnO8lQI8kbh52VbyhUxeOv1q6vJKjymrCSFsWaIlp31ki7CQDA9YWq7nMGFXlzfV9cMEJwqTfMyzwMoPR2hqtd80zJ9IIP4vIBDb9lPLT/MNgJ6gwDu6wDAUjEyM2Gjs1MfqEzUuoTF02V9u5dT4axDks3uP8fI2PiLOUwvju45faMQP6ujtMqHNzOOHSN6zu8RfcrXFFG/+UgCtUl/YgWNmy81rdnN8Kh2Wk9RW2FXV1vbnBbHWuKO26mIWofQIDAQAB";



	// Use this for initialization
	void Start () {
		CallShop ();
	}

	void CallShop(){
		game = Game.Instance;
		wsc = WsClient.Instance;

		prodIdDict = new Dictionary<string, ProductDesc> () {
			{STARDUST_250,new ProductDesc("stardust",250)} ,{STARDUST_550,new ProductDesc("stardust",550)} ,
			{STARDUST_1300,new ProductDesc("stardust",1300)} , {STARDUST_2625,new ProductDesc("stardust",2500)} ,
			{STARDUST_4000,new ProductDesc("stardust",4000)} , {STARDUST_8500,new ProductDesc("stardust",8500)} ,
			{FEATHER_250,new ProductDesc("feather",250)} , {FEATHER_550,new ProductDesc("feather",550)} ,
			{FEATHER_1300,new ProductDesc("feather",1300)} , {FEATHER_2625,new ProductDesc("feather",2625)} ,
			{FEATHER_4000,new ProductDesc("feather",4000)} , {FEATHER_8500,new ProductDesc("feather",8500)} ,
			{RESOURCE_250,new ProductDesc("resource",250)} , {RESOURCE_550,new ProductDesc("resource",550)} ,
			{RESOURCE_1300,new ProductDesc("resource",1300)} , {RESOURCE_2625,new ProductDesc("resource",2625)} ,
			{RESOURCE_4000,new ProductDesc("resource",4000)} , {RESOURCE_8500,new ProductDesc("resource",8500)} ,
		};

		//Set Currency type
		currencyDict = new Dictionary<string, int> () {
			{"feather",1},{"stardust",2},{"resource",3}
		};
		
		// Map skus for different stores       
		OpenIAB.mapSku(STARDUST_250, OpenIAB_Android.STORE_GOOGLE, STARDUST_250);
		OpenIAB.mapSku(STARDUST_550, OpenIAB_Android.STORE_GOOGLE, STARDUST_550);
		OpenIAB.mapSku(STARDUST_1300, OpenIAB_Android.STORE_GOOGLE, STARDUST_1300);
		OpenIAB.mapSku(STARDUST_2625, OpenIAB_Android.STORE_GOOGLE, STARDUST_2625);
		OpenIAB.mapSku(STARDUST_4000, OpenIAB_Android.STORE_GOOGLE, STARDUST_4000);
		OpenIAB.mapSku(STARDUST_8500, OpenIAB_Android.STORE_GOOGLE, STARDUST_8500);
		OpenIAB.mapSku(RESOURCE_250, OpenIAB_Android.STORE_GOOGLE, RESOURCE_250);
		OpenIAB.mapSku(RESOURCE_550, OpenIAB_Android.STORE_GOOGLE, RESOURCE_550);
		OpenIAB.mapSku(RESOURCE_1300, OpenIAB_Android.STORE_GOOGLE, RESOURCE_1300);
		OpenIAB.mapSku(RESOURCE_2625, OpenIAB_Android.STORE_GOOGLE, RESOURCE_2625);
		OpenIAB.mapSku(RESOURCE_4000, OpenIAB_Android.STORE_GOOGLE, RESOURCE_4000);
		OpenIAB.mapSku(RESOURCE_8500, OpenIAB_Android.STORE_GOOGLE, RESOURCE_8500);
		OpenIAB.mapSku(FEATHER_250, OpenIAB_Android.STORE_GOOGLE, FEATHER_250);
		OpenIAB.mapSku(FEATHER_550, OpenIAB_Android.STORE_GOOGLE, FEATHER_550);
		OpenIAB.mapSku(FEATHER_1300, OpenIAB_Android.STORE_GOOGLE, FEATHER_1300);
		OpenIAB.mapSku(FEATHER_2625, OpenIAB_Android.STORE_GOOGLE, FEATHER_2625);
		OpenIAB.mapSku(FEATHER_4000, OpenIAB_Android.STORE_GOOGLE, FEATHER_4000);
		OpenIAB.mapSku(FEATHER_8500, OpenIAB_Android.STORE_GOOGLE, FEATHER_8500);
		OpenIAB.mapSku(FIRST_CHARGE, OpenIAB_Android.STORE_GOOGLE, FIRST_CHARGE);
		OpenIAB.mapSku(MONTHLY_SUBSCRIPTION, OpenIAB_Android.STORE_GOOGLE, MONTHLY_SUBSCRIPTION);
//		OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_AMAZON, "sku");
//		OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_SAMSUNG, "100000105017/samsung_sku");
//		OpenIAB.mapSku(STARDUST_500, OpenIAB_iOS.STORE, STARDUST_500);



		var options = new OnePF.Options();
//		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
//		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_ONLY_KNOWN;
		options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, googlePublicKey} };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_YANDEX, yandexPublicKey } };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_SLIDEME, slideMePublicKey } };
		options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
		
		// Transmit options and start the service
		OpenIAB.init(options);
		Invoke ("CheckInventory", 5); // check inventory after IAB initialized.
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	private void OnEnable()
	{
		// Listen to all events for illustration purposes
		OpenIABEventManager.billingSupportedEvent += billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent += billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent += queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent += queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent += purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent += purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent += consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent += consumePurchaseFailedEvent;
	}
	private void OnDisable()
	{
		// Remove all event handlers
		OpenIABEventManager.billingSupportedEvent -= billingSupportedEvent;
		OpenIABEventManager.billingNotSupportedEvent -= billingNotSupportedEvent;
		OpenIABEventManager.queryInventorySucceededEvent -= queryInventorySucceededEvent;
		OpenIABEventManager.queryInventoryFailedEvent -= queryInventoryFailedEvent;
		OpenIABEventManager.purchaseSucceededEvent -= purchaseSucceededEvent;
		OpenIABEventManager.purchaseFailedEvent -= purchaseFailedEvent;
		OpenIABEventManager.consumePurchaseSucceededEvent -= consumePurchaseSucceededEvent;
		OpenIABEventManager.consumePurchaseFailedEvent -= consumePurchaseFailedEvent;
	}

	private void billingSupportedEvent()
	{
		_isInitialized = true;
		Debug.Log("billingSupportedEvent");
	}
	private void billingNotSupportedEvent(string error)
	{
		Debug.Log("billingNotSupportedEvent: " + error);
	}
	private void queryInventorySucceededEvent(Inventory inventory)
	{
		Debug.Log("queryInventorySucceededEvent: " + inventory);
		if (inventory != null)
		{
			_label = inventory.ToString();
			_inventory = inventory;
			List<Purchase> prods = inventory.GetAllPurchases();
			
			foreach(Purchase p in prods){
				if (p.Sku !=MONTHLY_SUBSCRIPTION && p.Sku != FIRST_CHARGE){
					OpenIAB.consumeProduct(p);
				}else{
					if (p.Sku == FIRST_CHARGE){
						firstChargeButton.interactable = false;
					}else if (p.Sku ==MONTHLY_SUBSCRIPTION){
						monthlySubscriptionBought = true;
					}
//					UnConsumedInventoryNewUpdate = true;
				}
			}
		}
		
	}
	private void queryInventoryFailedEvent(string error)
	{
		Debug.Log("queryInventoryFailedEvent: " + error);
		_label = error;
	}
	
	private void purchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("purchaseSucceededEvent: " + purchase);
		_label = "PURCHASED:" + purchase.ToString();
		var j = new JSONClass ();
		var json = new JSONClass ();
		var js = new JSONClass ();
		if ((purchase.Sku != MONTHLY_SUBSCRIPTION) && (purchase.Sku != FIRST_CHARGE)) { // For purchased virtual currency
			OpenIAB.consumeProduct (purchase);

			j.Add ("Event", "Event: User, " + game.login.id + " has purchased an item.");
			j ["OriginalJson"] = purchase.OriginalJson;
			j ["OrderId"] = purchase.OrderId;

			json ["data"] = j;
			json ["table"] = "log";
			json ["action"] = "NEW";
			wsc.Send (json.ToString ());  // log purchases in server

			var item = prodIdDict [purchase.Sku].name;
			var quantity = prodIdDict [purchase.Sku].quantity;
			for (int i = 0; i <3; i++) {
				if (game.wealth [i].type == currencyDict [item]) {
					game.wealth [i].value += quantity;
					json ["data"] = game.wealth [i].toJSON ();
				}
			}

			json ["table"] = "wealth";
			json ["action"] = "SET";
			wsc.Send (json.ToString ());  // update corresponance currency
		} else {
			//set gifts.

//			json ["table"] = "storage";
//			json ["action"] = "NEW";
//			wsc.Send (json.ToString ())
		}
		
	}
	private void purchaseFailedEvent(int errorCode, string errorMessage)
	{
		Debug.Log("purchaseFailedEvent: " + errorMessage );
		_label = "Purchase Failed: " + errorMessage;
	}
	private void consumePurchaseSucceededEvent(Purchase purchase)
	{
		Debug.Log("consumePurchaseSucceededEvent: " + purchase);
		_label = "CONSUMED: " + purchase.ToString();
	}
	private void consumePurchaseFailedEvent(string error)
	{
		Debug.Log("consumePurchaseFailedEvent: " + error);
		_label = "Consume Failed: " + error;
	}
	
//	public void OnPurchaseVirtualItem(int item,int quantity=1){
//		//update Game Object
//		int type = prodList.products [item].type;
//		int price = prodList.products [item].price;
//		int currencyType = prodList.products [item].currencyType;
//		
//		int numItems = game.storage.Count; 
//		bool isInStorage = false;
//		for (int i=0; i< numItems; i++) { 
//			
//		}
//		game.storage.Add (new Storage(item, prodList.products [item].type, 1, quantity));
//		
//		//deduct wealth
//		game.wealth [currencyType].value -= price;
//		
//		
//		// update server
//	}

	public void OnPurchaseButtonClicked(string product){
		if (product != MONTHLY_SUBSCRIPTION){
			OpenIAB.purchaseProduct (product);
		} else {
			OpenIAB.purchaseSubscription(product);
		}
	}

	private void CheckInventory(){
		OpenIAB.queryInventory ();
	}

}

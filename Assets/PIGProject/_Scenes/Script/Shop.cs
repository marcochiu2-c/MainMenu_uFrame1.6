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
	const string STARDUST_500 = "stardust_500";
	const string STARDUST_1200 = "stardust_1200";
	const string STARDUST_2500 = "stardust_2500";
	const string STARDUST_6500 = "stardust_6500";
	const string STARDUST_14000 = "stardust_14000";
	const string RESOURCE_500 = "resource_500";
	const string RESOURCE_1200 = "resource_1200";
	const string RESOURCE_2500 = "resource_2500";
	const string RESOURCE_6500 = "resource_6500";
	const string RESOURCE_14000 = "resource_14000";
	const string FEATHER_500 = "feather_500";
	const string FEATHER_1200 = "feather_1200";
	const string FEATHER_2500 = "feather_2500";
	const string FEATHER_6500 = "feather_6500";
	const string FEATHER_14000 = "feather_14000";
	const string FIRST_CHARGE = "first_charge";

	Dictionary<string,ProductDesc> prodIdDict;
	Dictionary<string,int> currencyDict;
	
	const string MONTHLY_SUBSCRIPTION = "monthly_subscription";
	const string SKU="";
	string _label = "";	
	Inventory _inventory = null;
	ProductList prodList = new ProductList();
	bool _isInitialized = false;
	Game game;

	string googlePublicKey ="MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgEEaiFfxugLWAH4CQqXYttXlj3GI2ozlcnWlZDaO2VYkcUhbrAz368FMmw2g40zgIDfyopFqETXf0dMTDw7VH3JOXZID2ATtTfBXaU4hqTf2lSwcY9RXe/Uz0x1nf1oLAf85oWZ7uuXScR747ekzRZB4vb4afm2DsbE30ohZD/WzQ22xByX6583yYE19RdE9yJzFckEPlHuOeMgKOa4WErt11PHB6FTdT5eN96/jjjeEoYhX/NGkOWKW0Y0T0A7CdUC0D4t2xxkzAQHdgLfcRw9+/EIcaysLhncWYiCifJrRBGpqZU1IrNuehrC5FXUN99786c/TwlxNG5nflE6sWwIDAQAB";



	// Use this for initialization
	void Start () {
		CallShop ();
	}

	void CallShop(){
		game = Game.Instance;
		prodIdDict = new Dictionary<string, ProductDesc> () {
			{STARDUST_500,new ProductDesc("stardust",500)} , {STARDUST_1200,new ProductDesc("stardust",1200)} ,
			{STARDUST_2500,new ProductDesc("stardust",2500)} , {STARDUST_6500,new ProductDesc("stardust",6500)} ,
			{STARDUST_14000,new ProductDesc("stardust",14000)} ,
			{FEATHER_500,new ProductDesc("feather",500)} , {FEATHER_1200,new ProductDesc("feather",1200)} ,
			{FEATHER_2500,new ProductDesc("feather",2500)} , {FEATHER_6500,new ProductDesc("feather",6500)} ,
			{FEATHER_14000,new ProductDesc("feather",14000)} ,
			{RESOURCE_500,new ProductDesc("resource",500)} , {RESOURCE_1200,new ProductDesc("resource",1200)} ,
			{RESOURCE_2500,new ProductDesc("resource",2500)} , {RESOURCE_6500,new ProductDesc("resource",6500)} ,
			{RESOURCE_14000,new ProductDesc("resource",14000)} 
		};

		//Set Currency type
		currencyDict = new Dictionary<string, int> () {
			{"stardust",0},{"resource",1},{"feather",2}
		};
		
		// Map skus for different stores       
		OpenIAB.mapSku(STARDUST_500, OpenIAB_Android.STORE_GOOGLE, STARDUST_500);
		OpenIAB.mapSku(STARDUST_1200, OpenIAB_Android.STORE_GOOGLE, STARDUST_1200);
		OpenIAB.mapSku(STARDUST_2500, OpenIAB_Android.STORE_GOOGLE, STARDUST_2500);
		OpenIAB.mapSku(STARDUST_6500, OpenIAB_Android.STORE_GOOGLE, STARDUST_6500);
		OpenIAB.mapSku(STARDUST_14000, OpenIAB_Android.STORE_GOOGLE, STARDUST_14000);
		OpenIAB.mapSku(RESOURCE_500, OpenIAB_Android.STORE_GOOGLE, RESOURCE_500);
		OpenIAB.mapSku(RESOURCE_1200, OpenIAB_Android.STORE_GOOGLE, RESOURCE_1200);
		OpenIAB.mapSku(RESOURCE_2500, OpenIAB_Android.STORE_GOOGLE, RESOURCE_2500);
		OpenIAB.mapSku(RESOURCE_6500, OpenIAB_Android.STORE_GOOGLE, RESOURCE_6500);
		OpenIAB.mapSku(RESOURCE_14000, OpenIAB_Android.STORE_GOOGLE, RESOURCE_14000);
		OpenIAB.mapSku(FEATHER_500, OpenIAB_Android.STORE_GOOGLE, FEATHER_500);
		OpenIAB.mapSku(FEATHER_1200, OpenIAB_Android.STORE_GOOGLE, FEATHER_1200);
		OpenIAB.mapSku(FEATHER_2500, OpenIAB_Android.STORE_GOOGLE, FEATHER_2500);
		OpenIAB.mapSku(FEATHER_6500, OpenIAB_Android.STORE_GOOGLE, FEATHER_6500);
		OpenIAB.mapSku(FEATHER_14000, OpenIAB_Android.STORE_GOOGLE, FEATHER_14000);
		OpenIAB.mapSku(FIRST_CHARGE, OpenIAB_Android.STORE_GOOGLE, FIRST_CHARGE);
		OpenIAB.mapSku(MONTHLY_SUBSCRIPTION, OpenIAB_Android.STORE_GOOGLE, MONTHLY_SUBSCRIPTION);
		//OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_AMAZON, "sku");
		//OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_SAMSUNG, "100000105017/samsung_sku");
		OpenIAB.mapSku(STARDUST_500, OpenIAB_iOS.STORE, STARDUST_500);
		
		var options = new Options();
		options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
		options.checkInventory = false;
		options.verifyMode = OptionsVerifyMode.VERIFY_SKIP;
		options.prefferedStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.availableStoreNames = new string[] { OpenIAB_Android.STORE_GOOGLE };
		options.storeKeys = new Dictionary<string, string> { {OpenIAB_Android.STORE_GOOGLE, googlePublicKey} };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_YANDEX, yandexPublicKey } };
		//options.storeKeys = new Dictionary<string, string> { { OpenIAB_Android.STORE_SLIDEME, slideMePublicKey } };
		options.storeSearchStrategy = SearchStrategy.INSTALLER_THEN_BEST_FIT;
		
		// Transmit options and start the service
		OpenIAB.init(options);

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
				if (p.Sku !=MONTHLY_SUBSCRIPTION)
					OpenIAB.consumeProduct(p);
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
		Debug.Log("purchaseSucceededEvent: " + purchase + "East Sun");
		_label = "PURCHASED:" + purchase.ToString();
		if ((purchase.Sku != MONTHLY_SUBSCRIPTION)&&(purchase.Sku != FIRST_CHARGE)) {
			OpenIAB.consumeProduct (purchase);
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
	
	public void OnPurchaseVirtualItem(int item,int quantity=1){
		//update Game Object
		int type = prodList.products [item].type;
		int price = prodList.products [item].price;
		int currencyType = prodList.products [item].currencyType;
		
		int numItems = game.storage.Count; 
		bool isInStorage = false;
		for (int i=0; i< numItems; i++) { 
			
		}
		game.storage.Add (new Storage(item, prodList.products [item].type, 1, quantity));
		
		//deduct wealth
		game.wealth [currencyType].value -= price;
		
		
		// update server
	}

}

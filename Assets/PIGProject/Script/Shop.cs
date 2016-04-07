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
	const string STARDUST_380 = "stardust_380";
	const string STARDUST_780 = "stardust_780";
	const string STARDUST_1380 = "stardust_1380";
	const string STARDUST_3680 = "stardust_3680";
	const string STARDUST_4980 = "stardust_4980";
	const string STARDUST_7880 = "stardust_7880";
	const string RESOURCE_76W = "resource_760000";
	const string RESOURCE_156W = "resource_1560000";
	const string RESOURCE_276W = "resource_2760000";
	const string RESOURCE_736W = "resource_7360000";
	const string RESOURCE_996W = "resource_9960000";
	const string RESOURCE_1576W = "resource_15760000";
	const string FEATHER_1900 = "feather_1900";
	const string FEATHER_3900 = "feather_3900";
	const string FEATHER_6900 = "feather_6900";
	const string FEATHER_18400 = "feather_18400";
	const string FEATHER_24900 = "feather_24900";
	const string FEATHER_39400 = "feather_39400";
	const string FIRST_CHARGE = "first_charge";
	
	Dictionary<string,ProductDesc> prodIdDict;
	Dictionary<string,int> currencyDict;
	
	const string MONTHLY_SUBSCRIPTION = "monthly_subscription";
	const string SKU="";
	string _label = "";    
	Inventory _inventory = null;
	//    ProductList prodList = new ProductList();
	//    List<Purchase> UnConsumedInventory;
	//    bool UnConsumedInventoryNewUpdate=false;
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
			{STARDUST_380,new ProductDesc("stardust",380)} ,{STARDUST_780,new ProductDesc("stardust",780)} ,
			{STARDUST_1380,new ProductDesc("stardust",1380)} , {STARDUST_3680,new ProductDesc("stardust",3680)} ,
			{STARDUST_4980,new ProductDesc("stardust",4980)} , {STARDUST_7880,new ProductDesc("stardust",7880)} ,
			{FEATHER_1900,new ProductDesc("feather",1900)} , {FEATHER_3900,new ProductDesc("feather",3900)} ,
			{FEATHER_6900,new ProductDesc("feather",6900)} , {FEATHER_18400,new ProductDesc("feather",18400)} ,
			{FEATHER_24900,new ProductDesc("feather",24900)} , {FEATHER_39400,new ProductDesc("feather",39400)} ,
			{RESOURCE_76W,new ProductDesc("resource",760000)} , {RESOURCE_156W,new ProductDesc("resource",1560000)} ,
			{RESOURCE_276W,new ProductDesc("resource",2760000)} , {RESOURCE_736W,new ProductDesc("resource",7360000)} ,
			{RESOURCE_996W,new ProductDesc("resource",9960000)} , {RESOURCE_1576W,new ProductDesc("resource",15760000)} ,
		};
		
		//Set Currency type
		currencyDict = new Dictionary<string, int> () {
			{"feather",1},{"stardust",2},{"resource",3}
		};
		
		// Map skus for different stores       
		OpenIAB.mapSku(STARDUST_380, OpenIAB_Android.STORE_GOOGLE, STARDUST_380);
		OpenIAB.mapSku(STARDUST_780, OpenIAB_Android.STORE_GOOGLE, STARDUST_780);
		OpenIAB.mapSku(STARDUST_1380, OpenIAB_Android.STORE_GOOGLE, STARDUST_1380);
		OpenIAB.mapSku(STARDUST_3680, OpenIAB_Android.STORE_GOOGLE, STARDUST_3680);
		OpenIAB.mapSku(STARDUST_4980, OpenIAB_Android.STORE_GOOGLE, STARDUST_4980);
		OpenIAB.mapSku(STARDUST_7880, OpenIAB_Android.STORE_GOOGLE, STARDUST_7880);
		OpenIAB.mapSku(RESOURCE_76W, OpenIAB_Android.STORE_GOOGLE, RESOURCE_76W);
		OpenIAB.mapSku(RESOURCE_156W, OpenIAB_Android.STORE_GOOGLE, RESOURCE_156W);
		OpenIAB.mapSku(RESOURCE_276W, OpenIAB_Android.STORE_GOOGLE, RESOURCE_276W);
		OpenIAB.mapSku(RESOURCE_736W, OpenIAB_Android.STORE_GOOGLE, RESOURCE_736W);
		OpenIAB.mapSku(RESOURCE_996W, OpenIAB_Android.STORE_GOOGLE, RESOURCE_996W);
		OpenIAB.mapSku(RESOURCE_1576W, OpenIAB_Android.STORE_GOOGLE, RESOURCE_1576W);
		OpenIAB.mapSku(FEATHER_1900, OpenIAB_Android.STORE_GOOGLE, FEATHER_1900);
		OpenIAB.mapSku(FEATHER_3900, OpenIAB_Android.STORE_GOOGLE, FEATHER_3900);
		OpenIAB.mapSku(FEATHER_6900, OpenIAB_Android.STORE_GOOGLE, FEATHER_6900);
		OpenIAB.mapSku(FEATHER_18400, OpenIAB_Android.STORE_GOOGLE, FEATHER_18400);
		OpenIAB.mapSku(FEATHER_24900, OpenIAB_Android.STORE_GOOGLE, FEATHER_24900);
		OpenIAB.mapSku(FEATHER_39400, OpenIAB_Android.STORE_GOOGLE, FEATHER_39400);
		OpenIAB.mapSku(FIRST_CHARGE, OpenIAB_Android.STORE_GOOGLE, FIRST_CHARGE);
		OpenIAB.mapSku(MONTHLY_SUBSCRIPTION, OpenIAB_Android.STORE_GOOGLE, MONTHLY_SUBSCRIPTION);
		//        OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_AMAZON, "sku");
		//        OpenIAB.mapSku(SKU, OpenIAB_Android.STORE_SAMSUNG, "100000105017/samsung_sku");
		//        OpenIAB.mapSku(STARDUST_500, OpenIAB_iOS.STORE, STARDUST_500);
		
		
		
		var options = new OnePF.Options();
		//        options.checkInventoryTimeoutMs = Options.INVENTORY_CHECK_TIMEOUT_MS * 2;
		//        options.discoveryTimeoutMs = Options.DISCOVER_TIMEOUT_MS * 2;
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
					//                    UnConsumedInventoryNewUpdate = true;
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
			game.wealth[currencyDict [item]-1].Add ( prodIdDict [purchase.Sku].quantity );
		} else if (purchase.Sku == FIRST_CHARGE) {
			//set gifts.
			
			//            json ["table"] = "storage";
			//            json ["action"] = "NEW";
			//            wsc.Send (json.ToString ())
			game.wealth[0].Add ( 400 );
			game.wealth[1].Add ( 80 );
			game.wealth[2].Add ( 160000 );
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
	
	//    public void OnPurchaseVirtualItem(int item,int quantity=1){
	//        //update Game Object
	//        int type = prodList.products [item].type;
	//        int price = prodList.products [item].price;
	//        int currencyType = prodList.products [item].currencyType;
	//        
	//        int numItems = game.storage.Count; 
	//        bool isInStorage = false;
	//        for (int i=0; i< numItems; i++) { 
	//            
	//        }
	//        game.storage.Add (new Storage(item, prodList.products [item].type, 1, quantity));
	//        
	//        //deduct wealth
	//        game.wealth [currencyType].value -= price;
	//        
	//        
	//        // update server
	//    }
	
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

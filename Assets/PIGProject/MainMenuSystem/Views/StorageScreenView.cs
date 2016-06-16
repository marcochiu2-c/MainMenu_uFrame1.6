using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Services;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class StorageScreenView : StorageScreenViewBase {

	public static Button weaponButton;
	public static Button armorButton;
	public static Button shieldButton;
	public static Button mountsButton;

	public static GameObject weaponGrid;
	public static GameObject armorGrid;
	public static GameObject shieldGrid;
	public static GameObject mountsGrid;
	public static GameObject panel;
    
    protected override void InitializeViewModel(uFrame.MVVM.ViewModel model) {
        base.InitializeViewModel(model);
        // NOTE: this method is only invoked if the 'Initialize ViewModel' is checked in the inspector.
        // var vm = model as StorageScreenViewModel;
        // This method is invoked when applying the data from the inspector to the viewmodel.  Add any view-specific customizations here.
		AssignGameObjectVariable ();
	}
	
	public void AssignGameObjectVariable(){
		panel = ScreenUIContainer.transform.Find ("Heading").gameObject;
		Transform popupHolder = ScreenUIContainer.transform.Find ("PopupHolder");
		Transform btnGroup = ScreenUIContainer.transform.Find ("ButtonGroup");
		
		Debug.Log("ButtonGroup: " + btnGroup);
		
		weaponGrid = popupHolder.Find ("WeaponGridList").gameObject;
		armorGrid = popupHolder.Find ("ArmorGridList").gameObject;
		shieldGrid = popupHolder.Find ("ShieldGridList").gameObject;
		mountsGrid = popupHolder.Find ("MountsGridList").gameObject;
		
		weaponButton = btnGroup.Find ("WeaponButton").GetComponent<Button>();
		armorButton = btnGroup.Find ("ArmorButton").GetComponent<Button>();
		shieldButton = btnGroup.Find ("SheildButton").GetComponent<Button>();
		mountsButton = btnGroup.Find ("MountsButton").GetComponent<Button>();
	}
    
    public override void Bind() {
        base.Bind();
        // Use this.StorageScreen to access the viewmodel.
        // Use this method to subscribe to the view-model.
        // Any designer bindings are created in the base implementation.

		this.BindButtonToHandler (weaponButton, () => {
			weaponGrid.gameObject.SetActive (true);
			armorGrid.gameObject.SetActive (false);
			shieldGrid.gameObject.SetActive (false);
			mountsGrid.gameObject.SetActive (false);

//			Debug.Log (text);
			Debug.Log ("Coordinate X of panel: "+panel.transform.localPosition.x);
			Debug.Log ("Coordinate Y of panel: "+panel.transform.localPosition.y);
			//		Debug.Log ("Width of panel: "+panel.transform.);
			//		Debug.Log ("Height of panel: "+panel.transform.localPosition.y);
//			return true;
		});

		this.BindButtonToHandler (armorButton, () => {
			weaponGrid.gameObject.SetActive (false);
			armorGrid.gameObject.SetActive (true);
			shieldGrid.gameObject.SetActive (false);
			mountsGrid.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (shieldButton, () => {
			weaponGrid.gameObject.SetActive (false);
			armorGrid.gameObject.SetActive (false);
			shieldGrid.gameObject.SetActive (true);
			mountsGrid.gameObject.SetActive (false);
		});

		this.BindButtonToHandler (mountsButton, () => {
			weaponGrid.gameObject.SetActive (false);
			armorGrid.gameObject.SetActive (false);
			shieldGrid.gameObject.SetActive (false);
			mountsGrid.gameObject.SetActive (true);
		});

    }

	public void SetButtons(string panel){
		Game game = Game.Instance;
//		ProductList pList = new ProductList();

		var count = game.storage.Count;
//		for (var i = 0; i < count ; i++){
//			if (panel == "Weapons" && game.storage[i].type>=5000 && game.storage[i].type<6000) {  // Weapons
//				if(Button(pList.GetProductById(game.storage[i].type).name))
//				{
//
//				}
//			}else if (panel == "Armors" && game.storage[i].type>=6000 && game.storage[i].type<7000){  // Armors
//				if(Button(pList.GetProductById(game.storage[i].type).name))
//				{
//					
//				}
//			}else if (panel == "Shields" && game.storage[i].type>=7000 && game.storage[i].type<8000){  // Shields
//				if(Button(pList.GetProductById(game.storage[i].type).name))
//				{
//					
//				}
//			}
//		}
	}
	
//	private bool Button(string text)
//	{
//		float width = Screen.width / 2.0f - X_OFFSET * 2;
//		float height = (Screen.width >= SMALL_SCREEN_SIZE || Screen.height >= SMALL_SCREEN_SIZE) ? LARGE_HEIGHT : SMALL_HEIGHT;
//		
//		bool click = GUI.Button(new Rect(
//			X_OFFSET + _column * X_OFFSET * 2 + _column * width, 
//			Y_OFFSET + _row * Y_OFFSET + _row * height, 
//			width, height),
//		                        text);
//		
//		++_column;
//		if (_column > 1)
//		{
//			_column = 0;
//			++_row;
//		}
//		
//		return click;

//	}

}

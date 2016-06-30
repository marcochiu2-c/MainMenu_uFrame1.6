using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GeneralDrag  : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
	public Image GeneralIcon;
	
	public Transform parentToReturnTo = null;
	public Transform placeholderParent = null;
	public Transform GeneralList;
	public GameObject panel;
	private Vector3 _imagePosition = Vector3.zero;
	
	GameObject placeholder = null;
	//ConferenceScreenView ConferenceV;
	
	public void OnBeginDrag(PointerEventData eventData) {
		Debug.Log ("OnBeginDrag");
	
		if(!panel.activeSelf)
			panel.SetActive(true);
		
		if(_imagePosition == Vector3.zero)
			_imagePosition = GeneralIcon.transform.localPosition;
		
		placeholder = new GameObject();
		placeholder.transform.SetParent( this.transform.parent );
		
		if(placeholder != GeneralList)
			GeneralIcon.transform.localPosition = _imagePosition;
		
		
		LayoutElement le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;
		
		placeholder.transform.SetSiblingIndex( this.transform.GetSiblingIndex() );
		
		parentToReturnTo = this.transform.parent;
		placeholderParent = parentToReturnTo;
		this.transform.SetParent( this.transform.parent.parent.parent.parent );
		
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		
	}
	
	public void OnDrag(PointerEventData eventData) {
		//Debug.Log ("OnDrag");
		
		this.transform.position = eventData.position;
		
		if(placeholder.transform.parent != placeholderParent)
			placeholder.transform.SetParent(placeholderParent);
		
		int newSiblingIndex = placeholderParent.childCount;
		
		for(int i=0; i < placeholderParent.childCount; i++) {
			if(this.transform.position.x < placeholderParent.GetChild(i).position.x) {
				
				newSiblingIndex = i;
				
				if(placeholder.transform.GetSiblingIndex() < newSiblingIndex)
					newSiblingIndex--;
				
				break;
			}
		}
		
		placeholder.transform.SetSiblingIndex(newSiblingIndex);
		
		
	}

	public void OnEndDrag(PointerEventData eventData) {
		
		Debug.Log ("OnEndDrag2" + placeholder.transform.GetSiblingIndex());
		this.transform.SetParent( parentToReturnTo );
		this.transform.SetSiblingIndex( placeholder.transform.GetSiblingIndex() + 10);
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		
		
		GeneralList = GameObject.Find("GeneralList").transform;
		
		if(parentToReturnTo == GeneralList)
		{
			//GeneralIcon.transform.localPosition = _imagePosition;
		}
		
		else if(parentToReturnTo != GeneralList)
		{
			Transform parentImage = parentToReturnTo.FindChild("Image");
		    Image image = parentImage.GetComponent<Image>();
			GeneralIcon.transform.position = image.transform.position;
			//Debug.Log ("ParentImage: " + parentImage);
			//if(image != null)
				//image.sprite = GeneralIcon.sprite;
				
		}
		

		
		Destroy(placeholder);
		
		if(parentToReturnTo != GeneralList)
			panel.SetActive(false);
	}	
}

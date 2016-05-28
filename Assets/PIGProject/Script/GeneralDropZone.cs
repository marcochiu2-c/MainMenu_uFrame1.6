using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GeneralDropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
	
	public void OnPointerEnter(PointerEventData eventData) {
		//Debug.Log("OnPointerEnter");
		if(eventData.pointerDrag == null)
			return;
		
		GeneralDrag d = eventData.pointerDrag.GetComponent<GeneralDrag>();
		if(d != null) {
			d.placeholderParent = this.transform;
		}
	}
	
	public void OnPointerExit(PointerEventData eventData) {
		//Debug.Log("OnPointerExit");
		if(eventData.pointerDrag == null)
			return;
		
		GeneralDrag d = eventData.pointerDrag.GetComponent<GeneralDrag>();
		if(d != null && d.placeholderParent==this.transform) {
			d.placeholderParent = d.parentToReturnTo;
		}
	}
	
	public void OnDrop(PointerEventData eventData) {
		Debug.Log (eventData.pointerDrag.name + " was dropped on " + gameObject.name);
		
		GeneralDrag d = eventData.pointerDrag.GetComponent<GeneralDrag>();
		if(d != null) {
			d.parentToReturnTo = this.transform;
		}
		
	}
}

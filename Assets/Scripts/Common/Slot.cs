using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler {

    public GameObject item {
        get {
            if (transform.childCount == 1) {
				var obj = transform.GetChild (0);
				return obj.GetComponent<Text>() != null ? null : obj.gameObject;
			} else if(transform.childCount > 1){
				return transform.GetChild (0).gameObject;
			}
            return null;
        }
    }

    public void OnDrop(PointerEventData eventData) {
        if (!item) {
            DragHandler.itemBeingDragged.transform.SetParent(transform);
        }
    }
}

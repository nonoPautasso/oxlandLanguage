using System;
using Assets.Scripts.Levels.OrderWordsDictionary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageTextSlot : MonoBehaviour, IDropHandler {
	public OrderWordsDictionaryView view;

	public void OnDrop(PointerEventData eventData) {
		if(transform.childCount != 1){
			GameObject target = ImageTextDragger.itemBeingDragged;
			target.transform.SetParent (transform);
			ImageTextDragger dragger = target.GetComponent <ImageTextDragger> ();
			dragger.Dropped ();
			Destroy (dragger);
			view.CheckTry ();
		}
	}
}
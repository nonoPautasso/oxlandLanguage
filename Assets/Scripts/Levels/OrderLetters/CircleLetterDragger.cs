using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CircleLetterDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	private bool dropped;
	private Vector3 startPosition;

	public void OnBeginDrag(PointerEventData eventData) {
		//transform.SetAsLastSibling ();
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) {
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		transform.position = startPosition;
		if(dropped){
			gameObject.SetActive (false);
			Dropped (false);
		}
	}

	public void Dropped (bool d) { dropped = d; }
}
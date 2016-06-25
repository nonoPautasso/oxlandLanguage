using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ImageTextDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	public static GameObject itemBeingDragged;
	private Vector3 startPosition;
	private Transform startParent;

	public void OnBeginDrag(PointerEventData eventData) {
		transform.SetAsLastSibling ();
		itemBeingDragged = gameObject;
		startParent = transform.parent;
		startPosition = transform.position;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData) {
		transform.position = Input.mousePosition;
	}

	public void Dropped () {
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if (startParent != transform.parent) {
			transform.position = transform.parent.position;
		}
	}

	public void OnEndDrag(PointerEventData eventData) {
		itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;

		transform.position = startPosition;
	}
}
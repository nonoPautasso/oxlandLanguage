﻿using System;

//It's a dragger lie because it's dragging, but then it returns to the old position magically setting text in the dropped slot.
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.App;


namespace Assets.Scripts.Common.Dragger {
	public class DraggerHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
		public static DraggerHandler itemBeingDragged;
		public bool activeOnDrop;
		private Vector3 startPosition;
		private bool dropped;
		private bool active = true;

		public void SetActive(bool isActive){
			active = isActive;
		}

		public void OnBeginDrag(PointerEventData eventData) {
			if (active) {
				SoundManager.instance.PlayClickSound ();
				itemBeingDragged = this;
				startPosition = transform.position;
				GetComponent<CanvasGroup> ().blocksRaycasts = false;
			}
		}

		public void OnDrag(PointerEventData eventData) {
			if (active) 
				transform.position = Input.mousePosition;
		}

		public void OnEndDrag(PointerEventData eventData) {
			if (active) {
				SoundManager.instance.PlayClickSound ();
				itemBeingDragged = null;
				GetComponent<CanvasGroup> ().blocksRaycasts = true;

				transform.position = startPosition;

				if (dropped && !activeOnDrop) {
					dropped = false;
					gameObject.SetActive (false);
				}
			}
		}

		public void Dropped() {
			dropped = true;
		}
	}
}
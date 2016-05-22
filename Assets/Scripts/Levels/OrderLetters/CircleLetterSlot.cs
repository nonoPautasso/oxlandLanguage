using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Levels.OrderLetters;
using UnityEngine.UI;

public class CircleLetterSlot : MonoBehaviour, IDropHandler, IPointerClickHandler {
	public OrderLettersView view;
	private GameObject target;

	public void OnDrop(PointerEventData eventData) {
		if(GetComponentInChildren <Text>().text == ""){
			target = CircleLetterDragger.itemBeingDragged;
			target.GetComponent <CircleLetterDragger>().Dropped(true);
			GetComponentInChildren <Text> ().text = target.GetComponentInChildren <Text> ().text;
			view.CheckTry ();
		}
	}

	public void OnPointerClick (PointerEventData eventData) {
		if (target != null && view.ClickEnabled ()) {
			target.SetActive (true);
			GetComponentInChildren <Text> ().text = "";
			target = null;
			view.CheckTry ();
		}
	}
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Levels.MayusMin;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour, IDropHandler {
	private MayusMinView view;

	public void OnDrop(PointerEventData eventData) {
		if(transform.GetChild (1).GetComponentInChildren <Text>().text == ""){;
			view.Dropped (this, LetterDragger.itemBeingDragged);
			LetterDragger.itemBeingDragged.GetComponentInChildren<LetterDragger>().OnEndDrag (null);
		}
	}

	public void View(MayusMinView view){
		this.view = view;
	}
}
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Levels.SoundsInWords;
using UnityEngine.UI;

public class CircleLetterPaint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler {
	public SoundsInWordsView view;
	private bool disabled;


	public void OnPointerDown (PointerEventData eventData) {
		view.PaintMode(true);
		PaintMe (view.GetColor ());
	}

	public void OnPointerUp (PointerEventData eventData) {
		view.PaintMode(false);
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if(view.PaintMode()){
			PaintMe (view.GetColor ());
		}
	}

	public void PaintMe (Color c) {
		if(!disabled) GetComponentInChildren<Image>().color = c;
	}

	public void Disabled(bool d){ disabled = d; }
}
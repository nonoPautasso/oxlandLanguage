using UnityEngine;
using System.Collections;
using Assets.Scripts.Levels.CreateSentences;
using Assets.Scripts.Levels.SplitSentences;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TogglePaint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler {
	public SplitSentencesView view;

	public void OnPointerDown (PointerEventData eventData) {
		if (!AmIPainted ()) {
			view.PaintModeOn (this);
			PaintMe (view.GetColor ());
		}
	}

	public void OnPointerUp (PointerEventData eventData) {
		view.PaintModeOff (this);
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (view.PaintMode () && view.IsNextToCurrent (this)) {
			if (AmIPainted ()) {
				//I'm going back!
				if(view.HasSameColor(this)){
					view.UnPaintCurrent ();
					view.PaintModeOn (this);
				}
				else view.PaintModeOff (this);
			}
			else {
				PaintMe (view.GetColor ());
				view.PaintModeOn (this);
			}
		} else
			view.PaintModeOff (this);
	}

	public void PaintMe (Color c) {
		GetComponentInChildren<Image>().color = c;
	}

	public bool AmIPainted () {
		return GetComponentInChildren<Image> ().color != Color.white;
	}

	public Color GetColor(){
		return GetComponentInChildren<Image> ().color;
	}
}
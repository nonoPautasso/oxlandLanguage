using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderView : LevelView {
		public Button nextBtn;
		public List<Image> answers;
		public List<Button> letters;
		private List<Vector3> originalPositions;
		public Image lettersPanel;

		private AbcOrderController controller;

		public void NextChallenge (List<string> modelAnswers, List<string> options, List<bool> helpLetters) {
			SetAnswerLetters (modelAnswers, helpLetters);
			SetOptions (options);
			nextBtn.gameObject.SetActive (false);
		}

		void SetOptions (List<string> options) {
			originalPositions = new List<Vector3> ();
			for (int i = 0; i < letters.Count; i++) {
				originalPositions.Add (letters[i].transform.position);
				letters [i].GetComponentInChildren<Text> ().text = options [i];
				letters [i].gameObject.AddComponent<DragHandler> ();
				letters [i].GetComponent<DragHandler> ().BeginParent (lettersPanel.transform);
			}
		}

		void SetAnswerLetters (List<string> modelAnswers, List<bool> helpLetters) {
			for (int i = 0; i < answers.Count; i++) {
				if(helpLetters[i]){
					answers [i].GetComponentInChildren<Text> ().text = modelAnswers [i];
				} else {
					answers [i].GetComponentInChildren<Text> ().text = "";
					answers [i].gameObject.AddComponent<Slot> ();
				}
			}
		}

		public override void ShowHint () {
			
		}

		public override void EndGame () { }

		public void DropOnLettersPanel(){
			var itemBeingDragged = DragHandler.itemBeingDragged;
			if (itemBeingDragged.transform.parent != lettersPanel.transform) {
				itemBeingDragged.transform.SetParent (lettersPanel.transform);
				itemBeingDragged.transform.position = originalPositions [letters.IndexOf (itemBeingDragged.GetComponent<Button>())];
			}
		}

		public void Controller (AbcOrderController controller) { this.controller = controller; }
	}
}
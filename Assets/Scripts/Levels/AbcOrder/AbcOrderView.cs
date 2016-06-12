using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderView : LevelView {
		public Button nextBtn;
		public List<Image> answers;
		public List<Button> letters;
		private List<Vector3> originalPositions;
		public Image lettersPanel;

		private AbcOrderController controller;

		public void NextChallenge (List<string> modelAnswers, List<string> options, List<bool> helpLetters) {
			EnableHint ();
			SetAnswerLetters (modelAnswers, helpLetters);
			SetOptions (options);
			nextBtn.gameObject.SetActive (false);
		}

		void SetOptions (List<string> options) {
			originalPositions = new List<Vector3> ();
			for (int i = 0; i < letters.Count; i++) {
				letters [i].image.color = Color.white;
				letters [i].interactable = true;
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
					answers [i].gameObject.AddComponent<Slot> ().view = this;
				}
			}
		}

		public void Correct (int index) {
			PlayRightSound ();
			Button letter = answers [index].GetComponent<Slot> ().item.GetComponent<Button> ();
			Destroy (letter.GetComponent<DragHandler>());
			letter.image.color = new Color32 (81, 225, 148, 225);
			if(IsEnded()){
				DisableHint ();
				nextBtn.gameObject.SetActive (true);
				RemoveDragHandlers ();
			}
		}

		void RemoveDragHandlers () {
			foreach (Button letter in letters) {
				RemoveDragHandler (letter);
			}
		}

		private static void RemoveDragHandler (Button letter) {
			DragHandler comp = letter.GetComponent<DragHandler> ();
			if (comp) Destroy (comp);
		}

		public void NextClick(){
			ResetLetters ();
			controller.ChallengeFinish ();
		}

		bool IsEnded () {
			foreach (Image answer in answers) {
				string text = answer.GetComponentInChildren<Text>().text;
				if(text == "") {
					if (!answer.GetComponent<Slot> ().item)
						return false;
				}
			}
			return true;
		}

		public void Wrong (int index) {
			PlayWrongSound ();
			Button letter = answers [index].GetComponent<Slot> ().item.GetComponent<Button> ();
			ResetLetter (letter, originalPositions[letters.IndexOf (letter)]);
			letter.image.color = new Color32 (251, 96, 96, 255);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Try(Image image){
			int index = answers.IndexOf (image);
			string answer = image.GetComponent<Slot>().item.GetComponentInChildren<Text>().text;
			controller.Try (index, answer);
		}

		public override void EndGame () { }

		public void ResetLetters () {
			for (int i = 0; i < letters.Count; i++) {
				ResetLetter (letters[i], originalPositions[i]);
			}
		}

		private void ResetLetter (Button letter, Vector3 originalPosition) {
			letter.transform.SetParent (lettersPanel.transform);
			letter.transform.position = originalPosition;
		}

		public void Hint (List<string> answersLetters) {
			Randomizer letterRandomizer = Randomizer.New (letters.Count - 1);
			while(letterRandomizer.HasNext ()){
				Button letter = letters[letterRandomizer.Next ()];
				if(!answersLetters.Contains (letter.GetComponentInChildren<Text>().text) && letter.IsInteractable ()){
					ResetLetter (letter, originalPositions[letters.IndexOf (letter)]);
					letter.image.color = new Color32 (251, 96, 96, 255);
					RemoveDragHandler (letter);
					letter.interactable = false;
					break;
				}
			}
			CheckHint (answersLetters);
		}

		private void CheckHint (List<string> answersLetters) {
			foreach (Button letter in letters) {
				if(!answersLetters.Contains (letter.GetComponentInChildren<Text>().text) && letter.IsInteractable ()){
					EnableHint ();
				}
			}
		}

		public void Controller (AbcOrderController controller) { this.controller = controller; }
	}
}
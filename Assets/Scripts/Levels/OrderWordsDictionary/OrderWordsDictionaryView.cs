using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.OrderWordsDictionary {
	public class OrderWordsDictionaryView : LevelView {
		public List<Button> draggables;
		public List<Image> objImages;
		public List<Image> slots;
		public Image hint;
		public Button tryBtn;
		public Button nextBtn;
		public Image draggablePanel;

		private List<Vector3> originalPositions;

		private OrderWordsDictionaryController controller;

		public void NextChallenge (List<string> letters, List<Word> words) {
			EnableHint ();
			ActiveButtons (true, false);
			SetWords (words);
			SetHint (letters);
			tryBtn.interactable = false;
			CheckTry ();
		}

		private void SetHint (List<string> letters) {
			hint.gameObject.SetActive (false);
			hint.GetComponentInChildren <Text>().text = string.Join (" ", letters.ToArray ());
		}

		private void SetWords (List<Word> words) {
			List<Word> randomWords = Randomizer.RandomizeList (words);
			for (int i = 0; i < draggables.Count; i++) {
				draggables [i].GetComponentInChildren <Text> ().text = randomWords [i].Name ();
				if(draggables[i].GetComponent <ImageTextDragger>() == null) draggables [i].gameObject.AddComponent <ImageTextDragger> ();
				objImages [i].sprite = randomWords [i].Sprite ();
				draggables [i].enabled = true;
				draggables [i].transform.SetParent (draggablePanel.transform);
				draggables [i].transform.position = originalPositions [i];
			}
		}

		public void ObjectClick(Button draggable){
			draggable.transform.SetParent (draggablePanel.transform);
			draggable.transform.position = originalPositions [draggables.IndexOf (draggable)];
			if(draggable.GetComponent <ImageTextDragger>() == null) draggable.gameObject.AddComponent <ImageTextDragger> ();
			CheckTry ();
		}

		private void ActiveButtons (bool tryB, bool next) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
		}

		public void TryClick(){
			List<string> answer = new List<string>();
			foreach (Image slot in slots) {
				answer.Add (slot.GetComponentInChildren <Button>().GetComponentInChildren <Text>().text);
			}
			controller.Try (answer);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void Wrong () {
			PlayWrongSound ();
			ObjectsDown ();
			CheckTry ();
		}

		private void ObjectsDown () {
			foreach (Button draggable in draggables) {
				ObjectClick (draggable);
			}
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			ActiveButtons (false, true);
			RemoveDraggables ();
		}

		private void RemoveDraggables () {
			for (int i = 0; i < draggables.Count; i++) {
				Destroy(draggables [i].GetComponent <ImageTextDragger> ());
				draggables [i].enabled = false;
			}
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
			Views.SetActiveImage(hint, true);
		}

		public void CheckTry () {
			RemoveHint ();
			foreach (Image slot in slots) {
				if(slot.transform.childCount < 1){
					tryBtn.interactable = false;
					return;
				}
			}
			tryBtn.interactable = true;
		}

		private void RemoveHint () {
			EnableHint ();
			hint.gameObject.SetActive (false);
		}

		public void SetOriginalPositions () {
			if(originalPositions == null){
				originalPositions = new List<Vector3> ();
				foreach (Button draggable in draggables) {
					originalPositions.Add (draggable.transform.position);
				}
			}
		}

		public override void EndGame () { }
		public void Controller (OrderWordsDictionaryController controller) { this.controller = controller; }
	}
}
using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.Syllables {
	public class SyllablesView : LevelView {
		public Image objectImage;
		public Button tryBtn;
		public Image backPanel;
		public List<Button> syllables;
		private List<Vector3> originalPositions;
		private Transform originalParent;

		public static int MAX_SYLLABLES = 7;

		private SyllablesController controller;

		public void TryClick(){
			List<string> result = new List<string> ();
			foreach (Button button in backPanel.GetComponentsInChildren<Button>()) {
				result.Add (button.GetComponentInChildren<Text>().text);
			}
			controller.Try (result);
		}

		public void Correct () {
			Views.ButtonsEnabled (syllables.ToArray (), false);
			tryBtn.enabled = false;
			DisableHint ();
		}

		public void Wrong () {
			PlayWrongSound ();
		}

		public void SyllableClick(int index){
			Button syllableButton = syllables [index];
			if(syllableButton.transform.parent == originalParent){
				MoveToPanel (syllableButton);
			} else {
				MoveFromPanel (syllableButton);
			}
		}

		private void MoveFromPanel (Button button) {
			PlaySoundClic ();
			button.transform.parent = originalParent;
		}

		private void MoveToPanel (Button button) {
			if(backPanel.GetComponentsInChildren<Button>().Length != MAX_SYLLABLES) {
				PlaySoundClic ();
				button.transform.parent = backPanel.transform;
			}
		}

		public override void ShowHint () {
			DisableHint ();
			Views.ButtonsEnabled (syllables.ToArray (), false);
			tryBtn.enabled = false;
			controller.ShowHint ();
		}

		public void HintDone(){
			Views.ButtonsEnabled (syllables.ToArray (), true);
			tryBtn.enabled = true;
		}

		public void NextChallenge (Word word, List<AudioClip> syllableList) {
			ResetView ();
			SetWord (word);
			SetSyllables (syllableList);
		}

		private void SetSyllables (List<AudioClip> syllableList) {
			for (int i = 0; i < syllables.Count; i++) {
				syllables [i].GetComponentInChildren<Text> ().text = syllableList [i].name.ToUpper ();
			}
		}

		private void SetWord (Word word) {
			objectImage.sprite = word.Sprite ();
		}

		public void StartGame () {
			originalPositions = new List<Vector3> ();
			foreach (Button syllable in syllables) {
				originalPositions.Add (syllable.transform.position);
			}
			originalParent = syllables [0].transform.parent;
		}

		public void ResetView () {
			for (int i = 0; i < syllables.Count; i++) {
				syllables [i].transform.parent = originalParent;
				syllables [i].transform.position = originalPositions [i];
			}

			EnableHint ();

			Views.ButtonsEnabled (syllables.ToArray (), true);
			tryBtn.enabled = true;
		}

		public void Controller (SyllablesController controller) { this.controller = controller; }
		public override void EndGame () { }
	}
}
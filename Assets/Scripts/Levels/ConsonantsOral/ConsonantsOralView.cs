using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.ConsonantsOral {
	public class ConsonantsOralView : LevelView {
		public List<Toggle> letters;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;

		private ConsonantsOralController controller;

		public void NextChallenge (Word word, List<string> letters) {
			objImage.sprite = word.Sprite ();
			EnableHint ();
			SetText ("");
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			SetLetters (letters);
		}

		private void SetLetters (List<string> lettersText) {
			Views.TogglesEnabled (letters.ToArray (), true);
			Views.TogglesOff (letters.ToArray ());

			for (int i = 0; i < letters.Count; i++) {
				letters [i].GetComponentInChildren<Text> ().text = lettersText [i];
				letters [i].interactable = true;
			}
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.enabled = sound;
		}

		public void ToggleChange(){
			foreach (Toggle letter in letters) {
				letter.image.color = Color.white;
			}

			foreach (Toggle letter in letters) {
				if(letter.isOn){
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		public void TryClick(){
			int index = -1;
			foreach (Toggle letter in letters) {
				if (letter.isOn) index = letters.IndexOf (letter);
			}
			if(index != -1) controller.Try (index);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			controller.PlayWord ();
		}

		public void Wrong (int index) {
			PlayWrongSound ();
			letters [index].image.color = Color.red;
			tryBtn.interactable = false;
		}

		public void Correct(Word word, int index){
			PlayRightSound ();
			DisableHint ();
			Views.TogglesEnabled (letters.ToArray (), false);
			letters [index].image.color = Color.green;
			SetText (word.Name ().ToUpper ());
			ActiveButtons (false, true, false);
		}

		private void SetText (string w) {
			objImage.GetComponentInChildren <Text>().text = w.Length != 0 ? "<color=green>" + w[0] + "</color>" + w.Remove (0, 1) : "";
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Hint (List<int> disables) {
			foreach (int disable in disables) {
				letters [disable].interactable = false;
			}
		}

		public override void EndGame () { }
		public void Controller (ConsonantsOralController controller) { this.controller = controller; }
	}
}


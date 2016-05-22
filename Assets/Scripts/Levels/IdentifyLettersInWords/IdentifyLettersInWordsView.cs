using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Levels.IdentifyLettersInWords {
	public class IdentifyLettersInWordsView : LevelView {
		public List<Toggle> letters;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;
		
		private IdentifyLettersInWordsController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			objImage.sprite = word.Sprite ();
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			SetWord (word.Name ());
			controller.PlayLetter ();
		}

		void SetWord (string word) {
			Views.TogglesEnabled (letters.ToArray (), true);
			Views.TogglesOff (letters.ToArray ());
			char[] wordChars = word.ToCharArray ();
			for (int i = 0; i < letters.Count; i++) {
				Toggle letter = letters [i];
				if (i < wordChars.Length) {
					Views.SetActiveToggle (letter, true);
					letter.GetComponentInChildren<Text> ().text = wordChars [i].ToString ();
				}
				else Views.SetActiveToggle (letter, false);
			}
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.enabled = sound;
		}

		public void ToggleChange(){
			//TODO TAKE THIS OUT!
			foreach (Toggle letter in letters) {
				letter.image.color = letter.isOn ? Color.grey : Color.white;
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
			List<string> answers = new List<string> ();
			foreach (Toggle letter in letters) {
				if (letter.isOn) answers.Add (letter.GetComponentInChildren<Text> ().text);
			}
			controller.Try (answers);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			controller.PlayLetter ();
		}

		public void Wrong () {
			PlayWrongSound ();
			tryBtn.interactable = false;
			Views.TogglesOff (letters.ToArray ());
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			Views.TogglesEnabled (letters.ToArray (), false);
			ActiveButtons (false, true, false);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public override void EndGame () { }
		public void Controller (IdentifyLettersInWordsController controller) { this.controller = controller; }
	}
}
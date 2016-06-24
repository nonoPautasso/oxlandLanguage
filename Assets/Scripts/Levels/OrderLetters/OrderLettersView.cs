using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

namespace Assets.Scripts.Levels.OrderLetters {
	public class OrderLettersView : LevelView {
		public List<Button> letters;
		public List<Image> slots;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;

		private bool clickEnabled;

		private OrderLettersController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			objImage.sprite = word.Sprite ();
			ActiveButtons (true, false, true);
			SetWord (word);
			controller.PlayWord ();
			CheckTry ();
		}

		private void SetWord (Word word) {
			clickEnabled = true;
			List<char> wordChars = Randomizer.RandomizeList (new List<char> (word.Name ().ToUpper ()));
			for (int i = 0; i < letters.Count; i++) {
				Button letter = letters [i];
				Image slot = slots [i];
				if (i < wordChars.Count) {
					slot.color = Color.white;
					Views.SetActiveButton (letter, true);
					slot.gameObject.SetActive (true);
					letter.GetComponentInChildren <Text>().text = wordChars[i].ToString ();
					slot.GetComponentInChildren <Text>().text = "";
				} else {
					Views.SetActiveButton (letter, false);
					slot.gameObject.SetActive (false);
					slot.GetComponentInChildren <Text>().text = "";
				}
			}
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.interactable = sound;
		}

		public void TryClick(){
			string answer = "";
			foreach (Image slot in slots) {
				answer += slot.GetComponentInChildren<Text> ().text;
			}
			controller.Try (answer);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			soundBtn.enabled = false;
			controller.PlayWord ();
		}

		public void AudioDone () {
			soundBtn.enabled = true;
		}

		public void Wrong (Word word) {
			PlayWrongSound ();
			SetWord (word);
			CheckTry ();
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			ActiveButtons (false, true, false);
			PaintAndDisableCorrect ();
		}

		private void PaintAndDisableCorrect () {
			clickEnabled = false;
			foreach (Image slot in slots) {
				slot.color = new Color32(81,225,148,225);
			}
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void CheckTry () {
			foreach (Image slot in slots) {
				if (slot.GetComponentInChildren<Text>().text.Length > 0) {
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		public bool ClickEnabled(){ return clickEnabled; }

		public override void EndGame () { }
		public void Controller (OrderLettersController controller) { this.controller = controller; }
	}
}
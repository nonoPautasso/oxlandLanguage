using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.SoundsInWords {
	public class SoundsInWordsView : LevelView {
		public List<Toggle> letters;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Toggle rubber;
		public Image objImage;
		public Text hint;

		private static Color DEFAULT_COLOR = Color.cyan;
		private bool paintMode;

		private SoundsInWordsController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			hint.gameObject.SetActive (false);
			objImage.sprite = word.Sprite ();
			TogglesEnabled (letters, false);
			UnPaintLetters ();
			ActiveButtons (true, false, true, false);
			SetLetters (word);
			controller.PlaySyllable ();
			CheckTry ();
		}

		private void TogglesEnabled (List<Toggle> letters, bool disabled) {
			foreach (Toggle letter in letters) {
				letter.GetComponent <CircleLetterPaint>().Disabled (disabled);
			}
		}

		private void SetLetters (Word word) {
			char[] wordChars = word.Name ().ToUpper ().ToCharArray ();
			for (int i = 0; i < letters.Count; i++) {
				Toggle letter = letters [i];
				if (i < wordChars.Length) {
					letter.image.color = Color.white;
					Views.SetActiveToggle (letter, true);
					letter.GetComponentInChildren <Text>().text = wordChars[i].ToString ();
				} else Views.SetActiveToggle (letter, false);
			}
		}

		private void ActiveButtons (bool tryB, bool next, bool sound, bool rub) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.interactable = sound;
			rubber.interactable = rub;
		}

		public void TryClick(){
			string answer = "";
			foreach (Toggle letter in letters) {
				if (letter.image.color == DEFAULT_COLOR)
					answer += letter.GetComponentInChildren<Text> ().text;
			}
			controller.Try (answer);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			controller.PlaySyllable ();
			soundBtn.enabled = false;
		}

		public void AudioDone () {
			soundBtn.enabled = true;
		}

		public void Wrong () {
			PlayWrongSound ();
			UnPaintLetters ();
			CheckTry ();
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			TogglesEnabled (letters, true);
			ActiveButtons (false, true, false, false);
			PaintCorrect ();
		}

		private void PaintCorrect () {
			foreach (Toggle letter in letters) {
				if(letter.image.color == DEFAULT_COLOR){
					letter.image.color = Color.green;
				}
			}
		}

		private void UnPaintLetters () {
			foreach (Toggle letter in letters) {
				letter.image.color = Color.white;
			}
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Hint (List<AudioClip> syllables) {
			hint.gameObject.SetActive (true);
			string h = "";
			foreach (AudioClip syllable in syllables) {
				h += syllable.name.ToUpper () + " ";
			}
			hint.text = h;
		}

		private void CheckTry () {
			foreach (Toggle letter in letters) {
				if (letter.image.color == DEFAULT_COLOR) {
					tryBtn.interactable = true;
					rubber.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
			rubber.isOn = false;
			rubber.interactable = false;
		}

		public void PaintMode(bool p) {
			paintMode = p;
			CheckTry ();
		}
		public bool PaintMode () { return paintMode; }
		public Color GetColor () { return rubber.isOn ? Color.white : DEFAULT_COLOR; }

		public override void EndGame () { }
		public void Controller (SoundsInWordsController controller) { this.controller = controller; }
	}
}
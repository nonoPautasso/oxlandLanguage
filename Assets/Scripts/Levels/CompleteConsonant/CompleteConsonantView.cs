using System;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.CompleteConsonant {
	public class CompleteConsonantView : LevelView {
		public List<Toggle> letters;
		public Button tryBtn, nextBtn, soundBtn;
		public Image objImage;
		public Text txt;

		private CompleteConsonantController controller;

		public void NextChallenge (Word word, List<string> letters, List<string> answer) {
			EnableHint ();
			UnpaintLetters ();
			objImage.sprite = word.Sprite ();
			nextBtn.interactable = true;
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			SetWord (word.Name (), answer);
			SetLetters (letters);
		}

		private void UnpaintLetters () {
			foreach (Toggle letter in letters) {
				letter.image.color = Color.white;
			}
		}

		void SetWord (string word, List<string> answer) {
			Views.TogglesEnabled (letters.ToArray (), true);
			Views.TogglesOff (letters.ToArray ());

			txt.text = GetText (word, answer);
		}

		private string GetText (string word, List<string> answer) {
			string result = word;
			foreach (string letter in answer) {
				result = result.Replace (letter, "_");
			}
			return result;
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
			//PlaySoundClick ();
			UnpaintLetters ();

			foreach (Toggle letter in letters) {
				if(letter.isOn){
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		public void TryClick(){
			List<int> answers = new List<int> ();
			foreach (Toggle letter in letters) {
				if (letter.isOn) answers.Add (letters.IndexOf (letter));
			}
			controller.Try (answers);
		}

		public void NextClick(){
			PlaySoundClick ();
			controller.NextChallenge ();
		}

		public void SoundClick(){
			soundBtn.enabled = false;
			SpeakerScript.instance.PlaySound (txt.text.Length / 3);
			controller.PlayWord ();
		}

		public void SoundFinished () {
			soundBtn.enabled = true;
		}

		public void Wrong (List<int> indexes) {
			PlayWrongSound ();
			tryBtn.interactable = false;
			Views.TogglesOff (letters.ToArray ());
			PaintToggles (indexes, new Color32(251,96,96,255));
		}

		private void PaintToggles (List<int> indexes, Color color) {
			foreach (int index in indexes) {
				letters [index].image.color = color;
			}
		}

		public void Correct(List<int> indexes, string word){
			PlayRightSound ();
			DisableHint ();
			Views.TogglesEnabled (letters.ToArray (), false);
			ActiveButtons (false, true, false);
			Views.TogglesOff (letters.ToArray ());
			PaintToggles (indexes, new Color32(81,225,148,225));
			txt.text = word;
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
		public void Controller (CompleteConsonantController controller) { this.controller = controller; }
	}
}


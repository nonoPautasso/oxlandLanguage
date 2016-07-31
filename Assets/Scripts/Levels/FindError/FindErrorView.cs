using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Levels.FindError {
	public class FindErrorView : LevelView {
		private FindErrorController controller;
		public Button nextBtn;
		public Button tryBtn;
		public Button soundBtn;
		public Image obj;
		public List<Toggle> toggles;

		private bool wrong;

		public void NextChallenge (Word w) {
			EnableHint ();
			SetActiveButtons (true, false, false);
			SetWords (GetWords(w));
			obj.sprite = w.Sprite ();
			Views.TogglesOff (toggles.ToArray ());
			Views.TogglesEnabled (toggles.ToArray (), true);

			toggles.ForEach ((Toggle o) => o.image.color = Color.white);

			CheckTry ();
		}

		private List<string> GetWords (Word w) {
			List<string> result = new List<string> ();
			string name = w.Name ();
			result.Add (name);
			int place = Randomizer.RandomInRange (name.Length - 1);
			string c = name [place].ToString ();

			string letter = null;

			if (Words.IsVowel (c)) {
				letter = Words.RandomVowel ();
			} else if (Words.IsConsonant (c)) {
				letter = Words.RandomConsonant ();
			}

			if (letter == null || letter == c) return GetWords (w);

			result.Add (Replace(name, letter, place));
			return Randomizer.RandomizeList (result);
		}
	
		public string Replace(string word, string ch, int place){
			StringBuilder sb = new StringBuilder(word);
			sb[place] = ch[0];
			return sb.ToString();
		}

		private void SetWords (List<string> words) {
			for (int i = 0; i < toggles.Count; i++) {
				toggles [i].GetComponentInChildren <Text> ().text = words [i];
			}
		}

		private void SetActiveButtons (bool tryB, bool nextB, bool soundB) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, nextB);
			Views.SetActiveButton (soundBtn, soundB);
		}

		public void ToggleChange(){
			toggles.ForEach ((Toggle obj) => obj.image.color = Color.white);
			EnableHint ();
			CheckTry ();
			if(wrong) {
				Views.TogglesEnabled (toggles.ToArray (), true);
				wrong = false;
			}
		}

		private void CheckTry () {
			tryBtn.interactable = SelectedToggle () != null;
		}

		private Toggle SelectedToggle () {
			Views.SetActiveButton (soundBtn, false);
			return toggles.Find ((Toggle obj) => obj.isOn);
		}

		public void TryClick(){
			controller.Try (SelectedToggle ().GetComponentInChildren <Text> ().text);
		}

		public void NextClick(){
			PlaySoundClick ();
			controller.NextChallenge ();
		}

		public void SoundClick(){
			PlaySoundClick ();
			controller.PlaySound ();
			soundBtn.enabled = false;
			tryBtn.interactable = false;
			Views.TogglesEnabled (toggles.ToArray (), false);
		}

		public void AudioDone () {
			soundBtn.enabled = true;
			Views.TogglesEnabled (toggles.ToArray (), true);
			CheckTry ();
		}

		public void Correct(){
			PlayRightSound ();
			Views.TogglesEnabled (toggles.ToArray (), false);
			SetActiveButtons (false, true, false);
			PaintToggle (new Color32 (81, 225, 148, 225));
			DisableHint ();
		}

		public void Wrong(){
			PlayWrongSound ();
			tryBtn.interactable = false;
			SelectedToggle ().enabled = false;
			wrong = true;
			PaintToggle (new Color32 (251, 96, 96, 255));
		}

		private void PaintToggle (Color color) {
			Toggle toggle = SelectedToggle ();
			toggle.isOn = false;
			toggle.image.color = color;
		}

		public override void ShowHint () {
			DisableHint ();
			Views.SetActiveButton (soundBtn, true);
			controller.ShowHint ();
		}

		public override void EndGame () { }
		public void Controller (FindErrorController controller) { this.controller = controller; }
	}
}
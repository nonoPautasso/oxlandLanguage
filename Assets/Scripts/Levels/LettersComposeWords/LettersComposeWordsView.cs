using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.LettersComposeWords {
	public class LettersComposeWordsView : LevelView {
		public List<Button> keyboard;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;
		public List<Button> clams;

		private LettersComposeWordsController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			Views.ButtonsEnabled (keyboard.ToArray (), true);
			Views.ButtonsEnabled (clams.ToArray (), true);
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			objImage.sprite = word.Sprite ();
			controller.PlayWord ();
			EmptyLetters ();
		}

		private void EmptyLetters () {
			RedrawClams ();
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.enabled = sound;
		}

		public void KeyboardClick(Button key){
			KeyboardClick (key.GetComponentInChildren <Text>().text);
		}

		private void RedrawClams (List<string> current = null) {
			Views.SetActiveButtons(clams, false);
			if (current == null) {
				foreach (Button clam in clams) clam.GetComponentInChildren <Text> ().text = "";
				return;
			}

			for (int i = 0; i < clams.Count; i++) {
				var isCurrent = i < current.Count;
				clams [i].GetComponentInChildren <Text> ().text = isCurrent ? current [i] : "";
				Views.SetActiveButton (clams [i], isCurrent);
			}
		}

		public void KeyboardClick(string letter){
			List<string> current = CurrentLetters ();
			if(current.Contains (letter)){
				current.Remove (letter);
				RedrawClams (current);
			} else {
				foreach (Button clam in clams) {
					if(clam.GetComponentInChildren <Text>().text == ""){
						clam.GetComponentInChildren <Text>().text = letter;
						Views.SetActiveButton (clam, true);
						break;
					}
				}
			}

			CheckTryBtn ();
		}

		private List<string> CurrentLetters () {
			List<string> result = new List<string> ();
			foreach (Button clam in clams) {
				string letter = clam.GetComponentInChildren<Text> ().text;
				if (letter != "")
					result.Add (letter);
			}
			return result;
		}

		private void CheckTryBtn () {
			foreach (Button subLetter in clams) {
				if (subLetter.GetComponentInChildren <Text> ().text != "") {
					tryBtn.interactable = true;
					return;
				}
			}
			tryBtn.interactable = false;
		}

		public void ClamClick(Button clam){
			clam.GetComponentInChildren <Text>().text = "";
			RedrawClams ();
			CheckTryBtn ();
		}

		public void TryClick(){
			List<string> answer = new List<string> ();
			foreach (Button clam in clams) {
				string text = clam.GetComponentInChildren<Text> ().text;
				if (text != "") answer.Add (text);
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

		public void Wrong () {
			PlayWrongSound ();
			tryBtn.interactable = false;
			RedrawClams ();
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			Views.ButtonsEnabled (keyboard.ToArray (), false);
			Views.ButtonsEnabled (clams.ToArray (), false);
			ActiveButtons (false, true, false);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Hint (List<string> round) {
			
		}

		private List<string> CharList (string round) {
			List<string> result = new List<string> ();
			foreach (char letter in round) {
				result.Add (letter.ToString ());
			}
			return result;
		}

		void OnGUI() {
			Event e = Event.current;
			if (e.isKey && e.type == EventType.KeyUp) {
				if (!nextBtn.IsActive ()) {
					if (e.keyCode >= KeyCode.A && e.keyCode <= KeyCode.Z) {
						Debug.Log ("Detected key code: " + e.keyCode);
						KeyboardClick (e.keyCode.ToString ());
					} else if (e.keyCode == KeyCode.Return) {
						Debug.Log ("Detected key code: Enter");
						TryClick ();
					}
				} else {
					if (e.keyCode == KeyCode.Return) {
						Debug.Log ("Detected key code: Enter");
						NextClick ();
					}
				}
			}
		}

		public override void EndGame () { }
		public void Controller (LettersComposeWordsController controller) { this.controller = controller; }
	}
}
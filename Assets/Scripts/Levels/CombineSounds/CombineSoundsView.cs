using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.CombineSounds {
	public class CombineSoundsView : LevelView {
		public List<Button> keyboard;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public List<Button> submarineLetters;
		public Button deleteButton;

		private static int HINT_DISABLE = 17;

		private CombineSoundsController controller;

		public void NextChallenge () {
			EnableHint ();
			PaintSubLetters (Color.white);
			Views.ButtonsEnabled (keyboard.ToArray (), true);
			Views.ButtonsEnabled (submarineLetters.ToArray (), true);
			deleteButton.interactable = true;
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			controller.PlayRound ();
			EmptyLetters ();
		}

		private void EmptyLetters () {
			foreach (Button subLetter in submarineLetters) {
				subLetter.GetComponentInChildren <Text>().text = "";
			}

			foreach (Button letter in keyboard) {
				letter.interactable = true;
			}
		}

		private void ActiveButtons (bool tryB, bool next, bool sound) {
			Views.SetActiveButton (tryBtn, tryB);
			Views.SetActiveButton (nextBtn, next);
			soundBtn.enabled = sound;
		}

		public void KeyboardClick(Button key){
			KeyboardClick (key.GetComponentInChildren <Text>().text);
		}

		public void KeyboardClick(string letter){
			PaintSubLetters (Color.white);
			foreach (Button subLetter in submarineLetters) {
				if(subLetter.GetComponentInChildren <Text>().text == ""){
					subLetter.GetComponentInChildren <Text>().text = letter;
					break;
				}
			}
			CheckTryBtn ();
		}

		private void CheckTryBtn () {
			foreach (Button subLetter in submarineLetters) {
				if (subLetter.GetComponentInChildren <Text> ().text == "") {
					tryBtn.interactable = false;
					return;
				}
			}
			tryBtn.interactable = true;
		}

		public void SubLetterClick(Button subLetter){
			PaintSubLetters (Color.white);
			subLetter.GetComponentInChildren <Text>().text = "";
			tryBtn.interactable = false;
		}

		public void DeleteClick(){
			PaintSubLetters (Color.white);
			foreach (Button subLetter in submarineLetters) {
				subLetter.GetComponentInChildren <Text>().text = "";
			}
			tryBtn.interactable = false;
		}

		public void TryClick(){
			string answer = "";
			foreach (Button b in submarineLetters) {
				answer += b.GetComponentInChildren <Text> ().text;
			}
			controller.Try (answer);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			soundBtn.enabled = false;
			controller.PlayRound ();
		}

		public void AudioDone () {
			soundBtn.enabled = true;
		}

		public void Wrong (string round) {
			PlayWrongSound ();
			tryBtn.interactable = false;
			PaintWrong (round);
		}

		private void PaintWrong (string round) {
			for (int i = 0; i < round.Length; i++) {
				if(round[i].ToString () != submarineLetters[i].GetComponentInChildren<Text>().text){
					submarineLetters [i].image.color = new Color32 (251, 96, 96, 255);
				}
			}
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			Views.ButtonsEnabled (keyboard.ToArray (), false);
			Views.ButtonsEnabled (submarineLetters.ToArray (), false);
			deleteButton.interactable = false;
			ActiveButtons (false, true, false);
			PaintSubLetters (new Color32(81,225,148,225));
		}

		private void PaintSubLetters (Color color) {
			foreach (Button subLetter in submarineLetters) {
				subLetter.image.color = color;
			}
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Hint (string round) {
			List<string> charList = CharList(round);
			Randomizer keyboardRandomizer = Randomizer.New (keyboard.Count - 1);
			int count = 0;

			while(count != HINT_DISABLE){
				Button b = keyboard [keyboardRandomizer.Next ()];
				if(!charList.Contains (b.GetComponentInChildren <Text>().text)){
					b.interactable = false;
					count++;
				}
			}
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
					} else if (e.keyCode == KeyCode.Backspace) {
						DeleteClick ();
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
		public void Controller (CombineSoundsController controller) { this.controller = controller; }
	}
}


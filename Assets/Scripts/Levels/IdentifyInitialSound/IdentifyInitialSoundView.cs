using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.IdentifyInitialSound {
	public class IdentifyInitialSoundView : LevelView {
		public List<Button> keyboard;
		public List<Image> objects;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Button deleteButton;
		public Image textBox;

		private IdentifyInitialSoundController controller;

		public void NextChallenge (List<Word> objs) {
			EnableHint ();
			PaintTextBox (Color.white);
			Views.ButtonsEnabled (keyboard.ToArray (), true);
			deleteButton.interactable = true;
			ActiveButtons (true, false, true);
			tryBtn.interactable = false;
			controller.PlayRound ();
			EmptyLetters ();
			SetWordSprites (objs);
		}

		private void SetWordSprites (List<Word> objs) {
			objects.ForEach ((Image obj) => obj.sprite = objs[objects.IndexOf (obj)].Sprite ());
		}

		private void EmptyLetters () {
			textBox.GetComponentInChildren <Text>().text = "";

			foreach (Button letter in keyboard) {
				letter.interactable = true;
			}

			for (int i = 0; i < objects.Count; i++) {
				Text text = objects [i].GetComponentInChildren<Text> (true);
				text.gameObject.SetActive (false);
				text.text = "";
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
			PaintTextBox (Color.white);
			Text t = textBox.GetComponentInChildren<Text> ();
			if(t.text.Length < 2){
				t.text += letter;
			}
			CheckTryBtn ();
		}

		private void CheckTryBtn () {
			tryBtn.interactable = textBox.GetComponentInChildren <Text>().text.Length == 2;
		}

		public void DeleteClick(){
			PaintTextBox (Color.white);
			Text t = textBox.GetComponentInChildren<Text> ();
			if(t.text.Length > 0){
				t.text = t.text.Remove (t.text.Length - 1, 1);
			}
			tryBtn.interactable = false;
		}

		public void TryClick(){
			string answer = textBox.GetComponentInChildren <Text>().text;
			controller.Try (answer);
		}

		public void NextClick(){
			controller.NextChallenge ();
		}

		public void SoundClick(){
			soundBtn.enabled = false;
			tryBtn.interactable = false;
			controller.PlayRound ();
		}

		public void AudioDone () {
			soundBtn.enabled = true;
			CheckTryBtn ();
		}

		public void Wrong () {
			PlayWrongSound ();
			tryBtn.interactable = false;
			PaintTextBox (new Color32 (251, 96, 96, 255));
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			Views.ButtonsEnabled (keyboard.ToArray (), false);
			deleteButton.interactable = false;
			ActiveButtons (false, true, false);
			PaintTextBox (new Color32(81,225,148,225));
		}

		private void PaintTextBox (Color color) {
			textBox.color = color;
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Hint (List<Word> round) {
			for (int i = 0; i < objects.Count; i++) {
				Text text = objects [i].GetComponentInChildren<Text> (true);
				text.gameObject.SetActive (true);
				text.text = round[i].Name ();
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
						if(tryBtn.IsInteractable ()) TryClick ();
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
		public void Controller (IdentifyInitialSoundController controller) { this.controller = controller; }
	}
}
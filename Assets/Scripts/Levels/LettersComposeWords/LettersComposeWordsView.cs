﻿using System;
using Assets.Scripts.App;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Levels.LettersComposeWords {
	public class LettersComposeWordsView : LevelView {
		public List<Toggle> keyboard;
		public Button tryBtn;
		public Button nextBtn;
		public Button soundBtn;
		public Image objImage;
		public List<Button> clams;

		private bool hintEnabled;
		private int roundNumber;

		private LettersComposeWordsController controller;

		public void NextChallenge (Word word) {
			EnableHint ();
			hintEnabled = false;
			Views.TogglesEnabled (keyboard.ToArray (), true);
			//PaintKeys (keyboard, Color.white);
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

		public void KeyboardClick(Toggle key){
			KeyboardClick (key.GetComponentInChildren <Text>().text);
		}



		private void RedrawClams (List<string> current = null) {
			if(!hintEnabled) Views.SetActiveButtons(clams, false);
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
			GameObject myEventSystem = GameObject.Find("EventSystem");
			myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
			List<string> current = CurrentLetters ();
			if(current.Contains (letter)){
				current.Remove (letter);
				RedrawClams (current);
				//PaintKey (letter, Color.white);
			} else {
				foreach (Button clam in clams) {
					if(clam.GetComponentInChildren <Text>().text == ""){
						clam.GetComponentInChildren <Text>().text = letter;
						Views.SetActiveButton (clam, true);
						//PaintKey (letter, Color.cyan);
						break;
					}
				}
			}

			if(hintEnabled) CheckHint ();
			CheckTryBtn ();
		}

		private void PaintKeys (List<Button> letters, Color c) {
			foreach (Button letter in letters) {
				letter.image.color = c;
			}
		}

//		private void PaintKey (string letter, Color c) {
//			foreach (Button letterBtn in keyboard) {
//				if(letterBtn.GetComponentInChildren <Text>().text == letter) {
//					letterBtn.image.color = c;
//					return;
//				}
//			}
//		}

		private void CheckHint () {
			for (int i = 0; i < clams.Count; i++) {
				clams [i].gameObject.SetActive (i < roundNumber);
				if(i >= roundNumber) clams[i].GetComponentInChildren <Text>().text = "";
			}
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
			Toggle keyToggle = GetToggleByLetter (clam.GetComponentInChildren <Text> ().text);
			keyToggle.isOn = false;
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
			ClearClams ();
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
			ClearClams ();
//			PaintKeys (keyboard, Color.white);
		}

		void ClearClams ()
		{
			foreach (Toggle key in keyboard) {
				key.isOn = false;
			}
		}

		public void Correct(){
			PlayRightSound ();
			DisableHint ();
			Views.TogglesEnabled (keyboard.ToArray (), false);
			Views.ButtonsEnabled (clams.ToArray (), false);
			ActiveButtons (false, true, false);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void Hint (List<string> round) {
			hintEnabled = true;
			roundNumber = round.Count;

			for (int i = 0; i < clams.Count; i++) {
				clams [i].gameObject.SetActive (i < roundNumber);
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
						Toggle keyToggle = GetToggleByLetter (e.keyCode.ToString ());
						bool toggleIsOn = keyToggle.isOn;
						keyToggle.isOn = !toggleIsOn;
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

		public Toggle GetToggleByLetter(string letter){
			foreach (Toggle key in keyboard) {
				if (letter == key.GetComponentInChildren <Text> ().text) {
					return key;
				}
			}
			return null;
		}

		public override void EndGame () { }
		public void Controller (LettersComposeWordsController controller) { this.controller = controller; }
	}
}
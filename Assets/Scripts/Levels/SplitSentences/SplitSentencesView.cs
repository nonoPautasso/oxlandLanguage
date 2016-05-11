using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Common;

namespace AssemblyCSharp {
	public class SplitSentencesView : LevelView {
		public List<GameObject> sentenceLines;
		public Image lettersPanel;
		public Image sentencePanel;

		private SplitSentencesController controller;

		public void NextChallenge (string sentence) {
			ResetView ();
			SetSentence (sentence);
		}

		private void SetSentence (string sentence) {
			char[] charArray = sentence.ToCharArray ();
			int charNumber = 0;
			foreach (Toggle letterToggle in lettersPanel.GetComponentsInChildren<Toggle>()) {
				if (charNumber < charArray.Length && charArray [charNumber] == ' ') charNumber++;
				if (charNumber < charArray.Length){
					ResetLetterToggle (letterToggle);
					letterToggle.GetComponentInChildren<Text>().text = charArray[charNumber].ToString ();
					AddEvents (letterToggle);
				} else {
					Views.SetActiveToggle (letterToggle, false);
				}
				charNumber++;
			}
		}

		private void AddEvents (Toggle letterToggle) {
			
		}

		void ResetLetterToggle (Toggle letterToggle) {
			Views.SetActiveToggle (letterToggle, true);
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public void ResetView () {
			
		}

		public void TryClick(){
			
		}

		public void Controller (SplitSentencesController controller) { this.controller = controller; }
		public override void EndGame () { }
	}
}
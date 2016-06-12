using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.ConsonantsVowels {
	public class ConsonantsVowelsView : LevelView {
		private ConsonantsVowelsController controller;
		private List<string> letters;
		private List<string> immutableLetters;
		private List<string> others;
		private Randomizer letterRandomizer;
		private Randomizer otherRandomizer;
		private Randomizer randomRandomizer;

		public List<Text> letterTexts;
		public List<Button> bubbles;

		public void NextChallenge (List<string> letters, List<string> others) {
			this.letters = new List<string> (letters);
			this.others = others;
			immutableLetters = new List<string> (letters);
			randomRandomizer = Randomizer.New (10);
			letterRandomizer = Randomizer.New (letters.Count - 1);
			otherRandomizer = Randomizer.New (others.Count - 1);

			ResetLetters ();
			ResetBubbles ();
			StartBubbles ();
		}

		private void StartBubbles () {
			foreach (Button bubble in bubbles) {
				SetBubble (bubble);
			}
		}

		private void ResetBubbles () {
			foreach (Button bubble in bubbles) {
				bubble.GetComponentInChildren<Text>().text = "";
			}
		}

		public void SetBubble (Button bubble) {
			string letter;
			bubble.interactable = true;
			bubble.enabled = true;
			bubble.image.color = Color.white;
			if (letters.Count == 0) return;
			bubble.GetComponent <Animator>().SetTime (0);
			if (randomRandomizer.Next () <= 3) {
				letter = letters.Count == 1 ? letters [0] : letters [letterRandomizer.Next ()];
			} else letter = others [otherRandomizer.Next ()];

			if (!LetterIsInUse (bubble, letter))
				bubble.GetComponentInChildren<Text> ().text = letter;
			else
				SetBubble (bubble);
		}

		private bool LetterIsInUse (Button bubble, string letter) {
			foreach (Button b in bubbles) {
				if (bubble != b && b.GetComponentInChildren<Text> ().text == letter)
					return true;
			}
			return false;
		}

		private void ResetLetters () {
			foreach (Text letter in letterTexts) letter.text = "";
		}

		public void BubbleClick(int index){
			Button bubble = bubbles [index];
			string letter = bubble.GetComponentInChildren<Text> ().text;
			bool isCorrect = letters.Contains (letter);
			controller.LogAnswer (isCorrect);
			if(isCorrect){
				letters.Remove (letter);
				letterRandomizer = Randomizer.New (letters.Count - 1);
				PlayRightSound ();
				SetCorrect (letter);
				bubble.interactable = false;
				bubble.enabled = false;
				bubble.image.color = new Color32 (81, 225, 148, 225);
				if (letters.Count == 0)
					controller.RoundEnd ();
			} else {
				PlayWrongSound ();
				SetBubble (bubble);
			}
		}

		private void SetCorrect (string letter) {
			letterTexts [immutableLetters.IndexOf (letter)].text = letter;
		}

		public override void ShowHint () {
			DisableHint ();
			controller.ShowHint ();
		}

		public override void EndGame () { }
		public void Controller (ConsonantsVowelsController controller) { this.controller = controller; }
	}
}
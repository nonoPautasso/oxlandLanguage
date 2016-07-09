using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.ConsonantsOral {
	public class ConsonantsOralModel : LevelModel {
		private int currentRound;
		private Word currentWord;
		private List<string> letters;

		public static int ROUNDS = 10;
		public static int LETTERS_QUANTITY = 5;
		public static int HINT_QUANTITY = 3;
		private List<List<string>> incompatibles;
		public List<Word> history;
		private bool isVowels;
		private Randomizer vowelRandomizer;

		public ConsonantsOralModel (bool isVowels) { this.isVowels = isVowels; }

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			if (isVowels) vowelRandomizer = Randomizer.New (Words.GetVowels ().Length - 1);

			history = new List<Word> ();
			SetIncompatibles ();
		}

		void SetIncompatibles () {
			incompatibles = new List<List<string>>();
			incompatibles.Add (new List<string> {"M", "N"});
			incompatibles.Add (new List<string> {"Q", "C", "K"});
			incompatibles.Add (new List<string> {"U", "W"});
			incompatibles.Add (new List<string> {"V", "B"});
		}

		public override void NextChallenge () {
			while (history.Contains (currentWord) || currentWord == null) {
				if (isVowels) {
					currentWord = Words.GetRandomWordFromLetter (Words.GetVowels () [vowelRandomizer.Next ()]);
				} else {
					currentWord = Words.GetRandomWord (false);
				}
			}
			history.Add (currentWord);
			SetLetters (GetCorrect ());
			currentRound++;
		}

		private string GetCorrect () {
			return currentWord.Name () [0].ToString ();
		}

		private void SetLetters (string correct) {
			if(isVowels){
				letters = new List<string>(Words.GetVowels ());
			} else {
				letters = new List<string> ();
				letters.Add (correct);
				while(letters.Count < LETTERS_QUANTITY){
					string letter = Words.RandomLetter ().ToUpper ();
					if (LetterOk (letter)) letters.Add (letter);
				}
				letters = Randomizer.RandomizeList (letters);
			}
		}

		private bool LetterOk (string letter) {
			foreach (List<string> incompatible in incompatibles) {
				int count = 0;
				foreach (string incompatibleLetter in incompatible) {
					if (letters.Contains (incompatibleLetter) || letter == incompatibleLetter) count++;
				}
				if (count > 1) return false;
			}
			
			return !letters.Contains (letter);
		}

		public List<int> GetHint () {
			Randomizer r = Randomizer.New (LETTERS_QUANTITY - 1);
			List<int> result = new List<int> ();
			while(result.Count < HINT_QUANTITY){
				int i = r.Next ();
				if (!IsCorrect (i)) result.Add (i);
			}
			return result;
		}

		public Word GetCurrentWord () { return currentWord; }

		public List<string> GetLetters () {
			return letters;
		}

		public bool IsCorrect (int index) {
			return letters[index].ToUpper () == GetCorrect ().ToUpper ();
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}


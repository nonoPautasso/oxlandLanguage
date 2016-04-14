using System;
using Assets.Scripts.App;
using UnityEngine;
using Assets.Scripts.Common;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.StartWithVowel {
	public class StartWithVowelModel : LevelModel {
		private Dictionary<string, List<Tuple<Word, bool>>> model;
		private int currentPage;
		public static int QUANTITY = 10;
		public static int CORRECT = 5;
		
		public override void StartGame () {
			currentPage = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadModel();
		}

		void LoadModel () {
			model = new Dictionary<string, List<Tuple<Word, bool>>> ();
			foreach(string vowel in Words.GetVowels()) {
				List<Tuple<Word, bool>> l = new List<Tuple<Word, bool>> ();
				List<Word> words = Words.GetRandomWords(QUANTITY, CORRECT, vowel);
				foreach (Word word in words) {
					l.Add (Tuple.Create (word, false));
				}
				model [vowel] = l;
			}
		}

		public void BackButton () {
			currentPage--;
		}

		public void NextButton () {
			currentPage++;
		}

		public bool HasEnded () {
			foreach (string vowel in Words.GetVowels ()) {
				int correctCount = 0;
				foreach (Tuple<Word, bool> word in model [vowel]) {
					if (word.Item2 && IsCorrect (word, vowel)) correctCount++;
				}
				if (correctCount != CORRECT) return false;
			}
			return true;
		}

		public List<Tuple<Word, bool>> GetCurrentPage () {
			return model [Words.GetVowels()[currentPage]];
		}

		public Word GetWord(int index) {
			return GetCurrentPage()[index].Item1;
		}

		public int GetCurrentPageNumber() {
			return currentPage;
		}

		public bool ObjectClick (int index) {
			Tuple<Word, bool> tuple = GetCurrentPage()[index];
			GetCurrentPage () [index] = Tuple.Create (tuple.Item1, true);

			return IsCorrect (index);
		}

		public bool IsCorrect (int index) {
			Tuple<Word, bool> tuple = GetCurrentPage()[index];
			string currentLetter = Words.GetVowels ()[currentPage];
			return IsCorrect (tuple, currentLetter);
		}

		public bool IsCorrect (Tuple<Word, bool> tuple, string currentLetter) {
			string clickedWord = tuple.Item1.Name ();
			return currentLetter == clickedWord.ToUpper ().ToCharArray () [0].ToString ();
		}

		public override void NextChallenge () { }

		public override void RequestHint () { }
		
	}
}
using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.Abc {
	public class AbcModel : LevelModel {
		private Dictionary<string, List<Tuple<Word, bool>>> model;
		private List<string> letters;
		private int currentPage;
		public static int QUANTITY = 9;
		public static int CORRECT = 5;
		public static int ROUNDS = 6;

		public override void StartGame () {
			currentPage = 0;

			minSeconds = 120;
			pointsPerError = 200;
			pointsPerSecond = 100;

			LoadModel();
		}

		void LoadModel () {
			model = new Dictionary<string, List<Tuple<Word, bool>>> ();
			letters = Words.RandomLetters (ROUNDS, false);
			foreach(string letter in letters) {
				List<Tuple<Word, bool>> l = new List<Tuple<Word, bool>> ();
				List<Word> words = Words.GetRandomWords(QUANTITY, CORRECT, letter, true);
				foreach (Word word in words) {
					l.Add (Tuple.Create (word, false));
				}
				model [letter] = l;
			}
		}

		public void BackButton () {
			currentPage--;
		}

		public void NextButton () {
			currentPage++;
		}

		public bool HasEnded () {
			foreach (string letter in letters) {
				int correctCount = 0;
				foreach (Tuple<Word, bool> word in model [letter]) {
					if (word.Item2 && IsCorrect (word, letter)) correctCount++;
				}
				if (correctCount != CORRECT) return false;
			}
			return true;
		}

		public List<Tuple<Word, bool>> GetCurrentPage () {
			return model [letters[currentPage]];
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
			string currentLetter = letters[currentPage];
			return IsCorrect (tuple, currentLetter);
		}

		public bool IsCorrect (Tuple<Word, bool> tuple, string currentLetter) {
			string clickedWord = tuple.Item1.Name ();
			return currentLetter == clickedWord.ToUpper ().ToCharArray () [0].ToString ();
		}

		public List<string> GetLetters () {
			return letters;
		}

		public string GetCurrentLetter () {
			return letters [currentPage];
		}

		public override void NextChallenge () { }

		public override void RequestHint () { }
	}
}
using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.Abc {
	public class AbcModel : LevelModel {
		private Dictionary<string, List<Tuple<Word, bool>>> model;
		private List<string> letters;
		private int currentPage;
		public static int QUANTITY = 10;
		public static int CORRECT = 5;
		public static int ROUNDS = 6;

		public override void StartGame () {
			currentPage = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadModel();
		}

		void LoadModel () {
			model = new Dictionary<string, List<Tuple<Word, bool>>> ();
			letters = Words.RandomLetters (ROUNDS);
			foreach(string letter in letters) {
				List<Tuple<Word, bool>> l = new List<Tuple<Word, bool>> ();
				List<Word> words = Words.GetRandomWords(QUANTITY, CORRECT, letter, true);
				foreach (Word word in words) {
					l.Add (Tuple.Create (word, false));
				}
				model [letter] = l;
			}
		}

		public void NextPage () {
			currentPage++;
		}

		public bool HasEnded () {
			return PageEnded() && currentPage == (ROUNDS - 1);
		}

		public List<Tuple<Word, bool>> GetCurrentPage () {
			return model [GetCurrentLetter ()];
		}

		public Word GetWord(int index) {
			return GetCurrentPage()[index].Item1;
		}

		public string GetCurrentLetter () {
			return letters [currentPage];
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
			string currentLetter = GetCurrentLetter ();
			return IsCorrect (tuple, currentLetter);
		}

		public bool IsCorrect (Tuple<Word, bool> tuple, string currentLetter) {
			string clickedWord = tuple.Item1.Name ();
			return currentLetter == clickedWord.ToUpper ().ToCharArray () [0].ToString ();
		}

		public bool PageEnded () {
			string currentLetter = GetCurrentLetter ();
			List<Tuple<Word, bool>> currentWords = model [currentLetter];
			int correctCount = 0;
			foreach (Tuple<Word, bool> word in currentWords) {
				if (word.Item2 && IsCorrect (word, currentLetter)) correctCount++;
			}
			return correctCount == CORRECT;
		}

		public override void NextChallenge () { }

		public override void RequestHint () { }
	}
}
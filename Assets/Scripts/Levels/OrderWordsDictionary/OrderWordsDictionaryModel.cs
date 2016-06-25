using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.OrderWordsDictionary {
	public class OrderWordsDictionaryModel : LevelModel {
		private int currentRound;
		private List<string> letters;
		private List<Word> words;

		public static int ROUNDS = 4;
		public static int LETTERS = 5;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;
		}

		public override void NextChallenge () {
			currentRound++;
			CreateRound ();
		}

		private void CreateRound () {
			letters = new List<string> ();
			words = new List<Word> ();

			SetLetters ();
			SetWords ();

		}

		private void SetWords () {
			while(words.Count != LETTERS){
				words.Add (Words.GetRandomWordFromLetter (letters[words.Count]));
			}
		}

		private void SetLetters () {
			string letter = Words.RandomLetter (LETTERS);
			letters.Add (letter);

			while(letters.Count != LETTERS){
				var newLetter = Words.NextLetter (letter);
				letters.Add (newLetter);
				letter = newLetter;
			}
		}

		public List<Word> GetWords () { return words; }

		public List<string> GetLetters () { return letters; }

		public bool IsCorrect (List<string> answer) {
			if (answer.Count != words.Count) return false;
			for (int i = 0; i < answer.Count; i++) {
				if (answer [i] != words [i].Name ()) return false;
			}
			return true;
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
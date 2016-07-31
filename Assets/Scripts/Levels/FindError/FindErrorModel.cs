using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.FindError {
	public class FindErrorModel : LevelModel {
		private int currentRound;
		private List<Word> levels;
		private List<string> others;

		private static int ROUNDS = 10;
		private static int EASY = 3;
		private static int EASY_LENGTH = 5;
		private static int HARD_LENGTH = 6;
		private static List<string> prohibitedLetters = new List<string>{ "LL", "RR" };

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			SetRounds ();
		}

		private void SetRounds () {
			levels = new List<Word>();
			List<Word> easy = Words.GetWordsWithLowerLength(EASY_LENGTH);
			Randomizer easyR = Randomizer.New (easy.Count - 1);
			List<Word> hard = Words.GetWordsWithGreaterLength(HARD_LENGTH);
			Randomizer hardR = Randomizer.New (hard.Count - 1);

			while(levels.Count < ROUNDS){
				Word w = levels.Count < EASY ? easy [easyR.Next ()] : hard [hardR.Next ()];
				if (ContainsProhibited (w.Name ())) continue;
				levels.Add (w);
			}
		}

		private bool ContainsProhibited (string word) {
			foreach (string letter in prohibitedLetters) {
				if (word.Contains (letter))
					return true;
			}
			return false;
		}

		public override void NextChallenge () {
			currentRound++;
		}

		public Word GetCurrentRound(){
			return levels [currentRound - 1];
		}

		public bool IsCorrect(string answer){
			return answer == GetCurrentRound ().Name ();
		}

		public bool GameEnded () {
			return currentRound == ROUNDS;
		}

		public override void RequestHint () { }
	}
}
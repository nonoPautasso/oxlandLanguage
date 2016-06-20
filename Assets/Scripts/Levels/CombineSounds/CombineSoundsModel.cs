using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.CombineSounds {
	public class CombineSoundsModel : LevelModel {
		private int currentRound;
		private List<string> rounds;

		public static int ROUNDS = 7;

		private static List<string> CONSONANT_FILTER = new List<string>{ "B", "V", "C", "K", "Q", "X", "Y", "Z", "H" };

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			SetRounds();
		}

		private void SetRounds () {
			rounds = new List<string> ();
			while(rounds.Count != ROUNDS){
				var consonant = Words.RandomConsonant ();
				string round = consonant + Words.RandomVowel();
				if (!rounds.Contains(round) && !CONSONANT_FILTER.Contains(consonant)) rounds.Add (round);
			}
		}

		public override void NextChallenge () {
			currentRound++;
		}

		public string GetCurrentRound(){
			return rounds[currentRound - 1];
		}

		public bool IsCorrect (string answer) {
			return answer == GetCurrentRound();
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}


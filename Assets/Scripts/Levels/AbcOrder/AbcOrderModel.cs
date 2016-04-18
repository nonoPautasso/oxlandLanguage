using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderModel : LevelModel {
		public static int ROUNDS = 6;
		public static int ANSWERS = 5;
		public static int OPTIONS = 9;
		public static int FIRST_LETTER_ROUNDS = 6;
		private int currentRound;
		private List<string> answers;
		private List<string> options;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;
		}

		public override void NextChallenge () {
			LoadAnswers ();
			LoadOptions ();
		}

		private void LoadOptions () {
			options = new List<string> ();
			options.AddRange (answers);
			while (options.Count != OPTIONS){
				string letter = Words.RandomLetter ();
				if (!options.Contains (letter))
					options.Add (letter);
			}
			options = Randomizer.RandomizeList (options);
		}

		private void LoadAnswers () {
			answers = new List<string> ();
			answers.Add (Words.RandomLetter (ANSWERS));
			while (answers.Count != ANSWERS)
				answers.Add (Words.NextLetter (answers [answers.Count - 1]));
		}

		public List<string> GetAnswers () {
			return answers;
		}

		public List<string> GetOptions () {
			return options;
		}

		public int GetCurrentRound () {
			return currentRound;
		}

		public override void RequestHint () {
			
		}
	}
}
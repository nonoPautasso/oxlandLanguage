using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderModel : LevelModel {
		public static int ROUNDS = 6;
		public static int ANSWERS = 5;
		public static int OPTIONS = 9;
		public static int FIRST_LETTER_ROUNDS = 3;
		private int currentRound;
		private List<string> answers;
		private List<string> options;
		private List<bool> helpLetters;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;
		}

		public void RoundFinish () {
			currentRound++;
		}

		public override void NextChallenge () {
			LoadAnswers ();
			LoadHelpLetters ();
			LoadOptions ();
		}

		void LoadHelpLetters () {
			bool isRandom = (AbcOrderModel.FIRST_LETTER_ROUNDS - currentRound) <= 0;
			int help = isRandom ? AbcOrderModel.ROUNDS - currentRound : AbcOrderModel.FIRST_LETTER_ROUNDS - currentRound;
			helpLetters = new List<bool> ();
			for (int i = 0; i < answers.Count; i++) helpLetters.Add (false);
			Randomizer r = Randomizer.New (answers.Count - 1);

			for (int i = 0; i < help; i++) {
				if (isRandom)
					helpLetters [r.Next ()] = true;
				else
					helpLetters [i] = true;
			}
		}

		private void LoadOptions () {
			options = new List<string> ();
			for (int i = 0; i < answers.Count; i++) {
				if (!helpLetters [i]) options.Add (answers [i]);
			}
			while (options.Count != OPTIONS){
				string letter = Words.RandomLetter ();
				if (!options.Contains (letter) && !answers.Contains (letter))
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

		public bool IsCorrect (int index, string answer) {
			return answers [index] == answer;
		}

		public bool HasEnded () {
			return currentRound == ROUNDS;
		}

		public List<string> GetAnswers () {
			return answers;
		}

		public List<bool> GetHelpLetters () {
			return helpLetters;
		}

		public List<string> GetOptions () {
			return options;
		}

		public override void RequestHint () { }
	}
}
using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.FishNet {
	public class FishNetModel : LevelModel {
		private List<Word> words;
		private int active;
		private List<Word> currentPage;

		public static int ANSWER_QUANTITY = 7;
		public static int HINTS = 2;
		public static int ROUNDS = 6;

		public override void StartGame () {
			minSeconds = 60;
			pointsPerError = 1000;
			pointsPerSecond = 100;

			words = new List<Word>();
			string startLetter = Words.RandomLetter (ROUNDS);
			words.Add(Words.GetRandomWordFromLetter (startLetter));
			active = 1;
		}

		public List<Word> ActiveObjects () {
			return words.GetRange (0, active);
		}

		public List<Word> AllObjects () {
			return words;
		}

		public List<Word> CurrentPage () {
			var currentLetter = CurrentLetter ();
			List<Word> result = Words.GetRandomWords (ANSWER_QUANTITY - 1, Randomizer.RandomInRange (4, 1), currentLetter, true);
			if(IsSameWord ()){
				//word has to be the same
				Word sameWord = words [active];
				if(!result.Contains (sameWord)) result.Add (sameWord);
			}

			while(result.Count != ANSWER_QUANTITY){
				Word newWord = Words.GetRandomWord ();
				if (!result.Contains (newWord)) result.Add (newWord);
			}

			currentPage = Randomizer.RandomizeList (result);
			return currentPage;
		}

		string CurrentLetter () {
			string currentLetter;
			if (IsSameWord())
				currentLetter = words [active].StartLetter ();
			else {
				Word lastWord = words [words.Count - 1];
				currentLetter = Words.NextLetter (lastWord.StartLetter ());
			}
			return currentLetter;
		}

		bool IsSameWord () { return active != words.Count; }

		public bool IsCorrect (int index) {
			var currentLetter = CurrentLetter ();
			Word word = currentPage [index];
			Word sameWord = IsSameWord () ? words [active] : null;
			if(sameWord != null){
				return word.Name () == sameWord.Name ();
			} else {
				return word.StartLetter () == currentLetter;
			}
		}

		public string WordText (int index) {
			return currentPage [index].Name ();
		}

		public bool Correct (int index) {
			if (IsSameWord ()) active++;
			else {
				words.Add (currentPage [index]);
				active++;
				return true;
			}
			return false;
		}

		public bool EndGame () {
			return words.Count == ROUNDS;
		}

		public override void NextChallenge () { }

		public override void RequestHint () { }

		public void ResetActive () {
			active = 1;
		}

		public List<int> GetHint () {
			List<int> result = new List<int> ();
			Randomizer r = Randomizer.New (ANSWER_QUANTITY - 1);
			for (int i = 0; i < HINTS; i++) {
				int next = r.Next ();
				while (IsCorrect (next))
					next = r.Next ();

				result.Add (next);
			}
			return result;
		}
	}
}
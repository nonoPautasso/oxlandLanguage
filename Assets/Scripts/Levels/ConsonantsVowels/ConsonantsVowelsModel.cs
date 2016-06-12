using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.ConsonantsVowels {
	public class ConsonantsVowelsModel : LevelModel {
		private bool isVowels;
		private int currentRound, rounds;
		private List<List<string>> levels;
		private List<string> others;

		private static int LETTER_QUANTITY = 5;

		public ConsonantsVowelsModel (bool isVowels) { this.isVowels = isVowels; }

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			SetRounds ();
			SetOthers ();
		}

		private void SetRounds () {
			levels = new List<List<string>>();
			if (isVowels) {
				rounds = 1;
				AddLevel (new List<string> (Words.GetVowels ()));
			} else {
				rounds = 3;
				AddLevel (new List<string> {"C", "D", "G", "L", "M", "P", "S", "T"}, true);
				AddLevel (new List<string> {"B", "F", "J", "N", "R" }, true);
				AddLevel (new List<string> {"H", "K", "Q", "V", "W", "X", "Y", "Z"}, true);
			}
		}

		private void AddLevel (List<string> letters, bool randomize = false) {
			List<string> aux = randomize ? new List<string>() : letters;
			Randomizer r = Randomizer.New (letters.Count - 1);
			while (aux.Count != LETTER_QUANTITY){ aux.Add (letters[r.Next ()]); }
			levels.Add (aux);
		}

		private void SetOthers () {
			others = new List<string> ();
			if(isVowels) others.AddRange (Words.GetConsonants());
			else others = new List<string> (Words.GetVowels ());
			others.AddRange (Words.GetNumbers ());
		}

		public override void NextChallenge () {
			currentRound++;
		}

		public List<string> GetLetters(){
			return levels [currentRound];
		}

		public List<string> GetOthers(){
			return others;
		}

		public bool GameEnded () {
			return currentRound == rounds;
		}

		public override void RequestHint () { }
	}
}
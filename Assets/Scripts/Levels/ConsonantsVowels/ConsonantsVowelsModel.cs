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

		private int audioIndex;

		private static int LETTER_QUANTITY = 5;

		public ConsonantsVowelsModel (bool isVowels) { this.isVowels = isVowels; }

		public override void StartGame () {
			currentRound = 0;

			if (isVowels) {
				minSeconds = 30;
				pointsPerError = 1200;
				pointsPerSecond = 100;
			} else {
				minSeconds = 60;
				pointsPerError = 1500;
				pointsPerSecond = 100;
			}


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
				AddLevel (new List<string> {"M", "P", "L", "S", "N", "D", "F", "B"}, true);
				AddLevel (new List<string> {"M", "P", "L", "S", "N", "D", "F", "B"}, true);
				AddLevel (new List<string> {"T", "R", "C", "Q", "H", "J"}, true);
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

		public void ResetAudioIndex () {
			audioIndex = 0;
		}

		public int AudioIndex () {
			return audioIndex;
		}

		public string AudioLetter(){
			return GetLetters () [audioIndex++];
		}

		public bool AudiosEnded () {
			return audioIndex == GetLetters ().Count;
		}

		public override void NextChallenge () {
			currentRound++;
		}

		public List<string> GetLetters(){
			return levels [currentRound - 1];
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
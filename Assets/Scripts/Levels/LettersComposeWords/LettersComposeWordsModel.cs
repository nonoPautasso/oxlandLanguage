using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using System.Configuration;

namespace Assets.Scripts.Levels.LettersComposeWords {
	public class LettersComposeWordsModel : LevelModel {
		private int currentRound;
		private List<Word> rounds;

		private List<string> prohibited = new List<string> {"QU", "GU", "LL", "RR", "CH"};

		public static int ROUNDS = 7;

		const int MIN_LENGTH = 5;

		const int MAX_ANSWER_LENGTH = 6;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			SetRounds();
		}

		private void SetRounds () {
			rounds = new List<Word> ();
			while(rounds.Count != ROUNDS){
				Word round = Words.GetRandomWord ();
				if (!rounds.Contains(round) && 
					round.Name ().Length >= MIN_LENGTH && 
					GetAnswer (round.Name ()).Count <= MAX_ANSWER_LENGTH && 
					Words.RipSymbols (round.Name ()) == round.Name () &&
					HasNoProhibited(round.Name ())) {
						rounds.Add (round);
				}
			}
		}

		private bool HasNoProhibited (string w) {
			foreach (string pr in prohibited) {
				if (w.Contains (pr)) return false;
			}
			return true;
		}

		public override void NextChallenge () {
			currentRound++;
		}

		public Word GetCurrentWord(){
			return rounds[currentRound - 1];
		}

		public bool IsCorrect (List<string> answer) {
			List<string> actualAnswer = GetAnswer ();
			if (actualAnswer.Count != answer.Count) return false;

			foreach (string letter in answer) {
				if (!actualAnswer.Contains (letter)) return false;
			}

			return true;
		}

		public List<string> GetAnswer () {
			string word = rounds [currentRound - 1].Name ();
			List<string> result = new List<string> ();
			foreach (char letter in word) {
				if(!result.Contains (letter.ToString ())) result.Add (letter.ToString ());
			}
			return result;
		}

		public List<string> GetAnswer(string word){
			List<string> result = new List<string> ();
			foreach (char letter in word) {
				if(!result.Contains (letter.ToString ())) result.Add (letter.ToString ());
			}
			return result;
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using SimpleJSON;
using I18N;

namespace Assets.Scripts.Levels.CompleteConsonant {
	public class CompleteConsonantModel : LevelModel {
		private Randomizer wordRandomizer;
		private int currentRound;
		private Word currentWord;

		public static int ROUNDS = 10;
		public static int EASY_QUANTITY = 5;
		public static int LETTER_QUANTITY = 5;
		public static int HINT_QUANTITY = 2;

		private List<string> words;
		private List<string> answer;
		private List<string> letters;
		private List<List<string>> answers;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadWords("easy");
		}

		void LoadWords (string difficulty) {
			words = new List<string> ();
			answers = new List<List<string>> ();

			TextAsset sentencesAsset = Resources.Load<TextAsset> (I18n.Msg ("completeConsonant.fileName"));
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode word in data[difficulty] as JSONArray) {
				JSONClass w = word as JSONClass;
				words.Add (w["word"].Value);
				AddAnswers (w ["consonants"] as JSONArray);
			}

			wordRandomizer = Randomizer.New (words.Count - 1);
		}

		private void AddAnswers (JSONArray consonants) {
			List<string> r = new List<string> ();
			foreach (JSONNode consonant in consonants) {
				r.Add (consonant.Value.ToUpper ());
			}
			answers.Add (r);
		}

		public override void NextChallenge () {
			if (EASY_QUANTITY == currentRound) LoadWords ("hard");
			var index = wordRandomizer.Next ();
			currentWord = Words.GetWord (words[index]);

			answer = answers [index];
			SetLetters();
			currentRound++;
		}

		private void SetLetters () {
			letters = new List<string> ();
			letters.AddRange (answer);
			while(letters.Count < LETTER_QUANTITY){
				string next = Words.RandomLetter ();
				if (!Words.IsVowel (next) && !letters.Contains (next)) letters.Add (next);
			}
			letters = Randomizer.RandomizeList (letters);
		}

		public List<int> GetHint () {
			Randomizer r = Randomizer.New (LETTER_QUANTITY - 1);
			List<int> result = new List<int> ();
			while(result.Count < HINT_QUANTITY){
				int i = r.Next ();
				if (!IsLetterCorrect (i)) result.Add (i);
			}
			return result;
		}

		public Word GetCurrentWord () { return currentWord; }

		public List<string> GetLetters(){ return letters; }

		public List<string> GetAnswer () {
			return answer;
		}

		public bool IsCorrect (List<int> answers) {
			if (answer.Count != answers.Count) return false;
			foreach (int index in answers) {
				if (!answer.Contains (letters [index])) return false;
			}
			return true;
		}

		private bool IsLetterCorrect (int i) {
			string letter = letters [i];
			return answer.Contains (letter);
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;
using I18N;
using SimpleJSON;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.IdentifyLettersInWords {
	public class IdentifyLettersInWordsModel : LevelModel {
		private Randomizer easyWordRandomizer;
		private Randomizer hardWordRandomizer;
		private int currentRound;
		private Word currentWord;
		private string currentLetter;

		public static int ROUNDS = 7;
		public static int EASY_QUANTITY = 7;

		private List<String> easyWords;
		private List<String> hardWords;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadWords();
		}

		void LoadWords () {
			easyWords = new List<string> ();
			hardWords = new List<string> ();

			TextAsset sentencesAsset = Resources.Load<TextAsset> (I18n.Msg ("identifyLetters.fileName"));
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode word in data["easy"] as JSONArray) {
				easyWords.Add (word.Value);
			}
			foreach (JSONNode word in data["hard"] as JSONArray) {
				hardWords.Add (word.Value);
			}
			easyWordRandomizer = Randomizer.New (easyWords.Count - 1);
			hardWordRandomizer = Randomizer.New (easyWords.Count - 1);
		}

		public override void NextChallenge () {
			string word = currentRound < EASY_QUANTITY ? easyWords [easyWordRandomizer.Next ()] : hardWords [hardWordRandomizer.Next ()];
			currentWord = Words.GetWord (word);
			currentLetter = Words.RipSymbols(LetterFromWord (word)).ToUpper ();
			currentRound++;
		}

		private string LetterFromWord (string word) {
			return word.ToCharArray () [Randomizer.New (word.Length - 1).Next ()].ToString ();
		}

		public Word GetCurrentWord () { return currentWord; }

		public string GetCurrentLetter () { return currentLetter; }

		public bool IsCorrect (List<string> answers) {
			Debug.Log (string.Join (",", answers.ToArray ()));
			return AllEqual(answers) && CorrectAmount(answers);
		}

		bool CorrectAmount (List<string> answers) {
			int amount = 0;
			foreach (char letter in currentWord.Name ().ToCharArray ()) {
				if (currentLetter == Words.RipSymbols (letter.ToString ())) amount++;
			}

			return answers.Count == amount;
		}

		private bool AllEqual (List<string> answers) {
			string answer = Words.RipSymbols (currentLetter.ToString ());
			foreach (string letter in answers) {
				if (letter != answer) return false;
			}
			return true;
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
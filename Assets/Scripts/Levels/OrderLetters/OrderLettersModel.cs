﻿using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.OrderLetters {
	public class OrderLettersModel : LevelModel {
		private Randomizer easyWordRandomizer;
		private Randomizer hardWordRandomizer;
		private int currentRound;
		private Word currentWord;

		public static int ROUNDS = 7;
		public static int EASY_QUANTITY = 2;

		private static List<int> EASY = new List<int>{3, 4};
		private static List<int> HARD = new List<int>{5, 6};
		private static List<string> WORD_FILTER = new List<string>{"H", "LL", "Q", "W", "X", "K", "GUE", "GUI", "RR"};

		private List<String> easyWords;
		private List<String> hardWords;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 90;
			pointsPerError = 600;
			pointsPerSecond = 100;

			LoadWords();
		}

		private void LoadWords () {
			easyWords = new List<string> ();
			hardWords = new List<string> ();

			easyWords.AddRange (FilterWords(Words.GetWordsWithLength(EASY)));
			hardWords.AddRange (FilterWords (Words.GetWordsWithLength (HARD)));

			easyWordRandomizer = Randomizer.New (easyWords.Count - 1);
			hardWordRandomizer = Randomizer.New (easyWords.Count - 1);
		}

		private List<string> FilterWords (List<string> words) {
			List<string> result = new List<string> ();
			foreach (string word in words) {
				bool contains = false;
				foreach (string filter in WORD_FILTER) {
					if(Words.RipSymbols(word).ToUpper().Contains (filter.ToUpper ()) || Words.RipSymbols (word) != word){
						contains = true;
						break;
					}
				}
				if (!contains) result.Add (word);
			}
			return result;
		}

		public override void NextChallenge () {
			string word = currentRound < EASY_QUANTITY ? easyWords [easyWordRandomizer.Next ()] : hardWords [hardWordRandomizer.Next ()];
			currentWord = Words.GetWord (word);
			currentRound++;
		}

		public Word GetCurrentWord () { return currentWord; }

		public bool IsCorrect (string answer) {
			return answer.ToUpper () == currentWord.Name ().ToUpper ();
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
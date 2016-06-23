using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using SimpleJSON;

namespace Assets.Scripts.Levels.MemotestEnding {
	public class MemotestEndingModel : LevelModel {
		private int pairCount;
		private List<Word> pairs;

		public static int PAIRS = 8;
		public static int PAIR_NUMBER = 2;

		public override void StartGame () {
			pairCount = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			SetRounds();
		}

		private void SetRounds () {
			pairs = new List<Word> ();
			List<string> ends = GetEndSounds ();
			Randomizer soundRandomizer = Randomizer.New (ends.Count - 1);
			while(pairs.Count != PAIRS * PAIR_NUMBER){
				string endSound = ends [soundRandomizer.Next ()];
				List<Word> endWords = Words.WordsWithSuffix (endSound);
				if (endWords.Count < PAIR_NUMBER) continue;
				pairs.AddRange(GetRound(endWords));
			}
			pairs = Randomizer.RandomizeList (pairs);
		}

		private List<Word> GetRound (List<Word> startWords) {
			List<Word> result = new List<Word> ();
			Randomizer r = Randomizer.New (startWords.Count - 1);
			while(result.Count < PAIR_NUMBER){
				result.Add (startWords[r.Next ()]);
			}
			return result;
		}

		private List<string> GetEndSounds () {
			List<string> result = new List<string> ();

			TextAsset sentencesAsset = Resources.Load<TextAsset> ("jsons/startEndSounds");
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode sound in data["endingSounds"] as JSONArray) {
				result.Add (sound.Value);
			}

			return result;
		}

		public List<Word> GetPairs(){
			return pairs;
		}

		public bool IsCorrect (int index1, int index2) {
			bool correct = pairs [index1].EndLetters (2) == pairs [index2].EndLetters (2);
			if (correct) pairCount++;
			return correct;
		}

		public bool GameEnded () { return pairCount == PAIRS; }

		public override void NextChallenge () {}
		public override void RequestHint () { }
	}
}
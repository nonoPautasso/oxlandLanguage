using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using SimpleJSON;

namespace Assets.Scripts.Levels.IdentifyInitialSound {
	public class IdentifyInitialSoundModel : LevelModel {
		private int currentRound;
		private List<List<Word>> rounds;

		public static int ROUNDS = 7;
		public static int WORDS_TO_SHOW = 3;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			SetRounds();
		}

		private void SetRounds () {
			rounds = new List<List<Word>> ();
			List<string> starts = GetStartSounds ();
			Randomizer soundRandomizer = Randomizer.New (starts.Count - 1);
			while(rounds.Count != ROUNDS){
				string startSound = starts [soundRandomizer.Next ()];
				List<Word> startWords = Words.WordsWithPrefix (startSound);
				if (startWords.Count < WORDS_TO_SHOW) continue;
				rounds.Add (GetRound(startWords));
			}
		}

		private List<Word> GetRound (List<Word> startWords) {
			List<Word> result = new List<Word> ();
			Randomizer r = Randomizer.New (startWords.Count - 1);
			while(result.Count < WORDS_TO_SHOW){
				result.Add (startWords[r.Next ()]);
			}
			return result;
		}

		private List<string> GetStartSounds () {
			List<string> result = new List<string> ();

			TextAsset sentencesAsset = Resources.Load<TextAsset> ("jsons/startEndSounds");
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode sound in data["startingSounds"] as JSONArray) {
				result.Add (sound.Value);
			}

			return result;
		}

		public override void NextChallenge () {
			currentRound++;
		}

		public List<Word> GetCurrentRound(){
			return rounds[currentRound - 1];
		}

		public bool IsCorrect (string answer) {
			return answer.ToUpper () == GetCurrentRound()[0].StartLetters (2);
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
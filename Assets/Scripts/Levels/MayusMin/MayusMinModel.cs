using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using SimpleJSON;
using I18N;

namespace Assets.Scripts.Levels.MayusMin {
	public class MayusMinModel : LevelModel {
		private Randomizer sentenceRandomizer;
		private int currentRound;
		private int currentIndex;
		public static int ROUNDS = 7;

		private List<String> sentences;
		private List<List<string>> answers;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadData();
		}

		private void LoadData () {
			sentences = new List<string> ();
			answers = new List<List<string>>();

			TextAsset sentencesAsset = Resources.Load<TextAsset> (I18n.Msg ("mayMin.fileName"));
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode s in data["sentences"] as JSONArray) {
				JSONClass sentenceObject = s as JSONClass;
				sentences.Add (sentenceObject["text"].Value);
				AddAnswer (sentenceObject ["letters"] as JSONArray);
			}
			sentenceRandomizer = Randomizer.New (sentences.Count - 1);
		}

		private void AddAnswer (JSONArray letters) {
			List<string> answer = new List<string> ();
			foreach (JSONNode letter in letters) {
				answer.Add (letter.Value);
			}
			answers.Add (answer);
		}

		public override void NextChallenge () {
			currentIndex = sentenceRandomizer.Next ();
			currentRound++;
		}

		public bool IsLetterCorrect(string letter, int slotNumber){
			return answers [currentIndex] [slotNumber] == letter;
		}

		public bool IsCorrect (List<string> currentAnswer) {
			List<string> answer = answers [currentIndex];
			if (currentAnswer.Count != answer.Count) return false;

			for (int i = 0; i < answer.Count; i++) {
				if (answer [i] != currentAnswer [i]) return false;
			}

			return true;
		}

		public string GetSentence () { return sentences [currentIndex]; }

		public List<string> GetLetters(){ return answers [currentIndex]; }

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
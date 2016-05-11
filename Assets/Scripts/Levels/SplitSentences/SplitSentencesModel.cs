using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;
using I18N;
using SimpleJSON;

namespace AssemblyCSharp {
	public class SplitSentencesModel : LevelModel {
		private Randomizer sentenceRandomizer;
		private int currentRound;
		private int currentIndex;
		public static int ROUNDS = 7;

		private List<String> sentences;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadSentences();
		}

		void LoadSentences () {
			sentences = new List<string> ();

			TextAsset sentencesAsset = Resources.Load<TextAsset> (I18n.Msg ("splitSentences.fileName"));
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode sentence in data["sentences"] as JSONArray) {
				sentences.Add (sentence.Value);
			}
			sentenceRandomizer = Randomizer.New (sentences.Count - 1);
		}

		public override void NextChallenge () {
			currentIndex = sentenceRandomizer.Next ();
			currentRound++;
		}

		public string GetSentence () { return sentences [currentIndex]; }

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;
using I18N;
using SimpleJSON;

namespace AssemblyCSharp {
	public class CreateSentencesModel : LevelModel {
		private Randomizer sentenceRandomizer;
		private int currentRound;

		private List<string> sentenceAudios;
		private List<string> answers;
		private List<List<string>> options;
		private int currentIndex;

		public static int ROUNDS = 7;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadSentences();
		}

		private void LoadSentences () {
			sentenceAudios = new List<string>();
			answers = new List<string> ();
			options = new List<List<string>> ();

			TextAsset sentences = Resources.Load<TextAsset> (I18n.Msg ("sentences.fileName"));
			JSONClass data = JSON.Parse (sentences.text) as JSONClass;

			foreach (JSONClass sentence in data["sentences"] as JSONArray) {
				string answer = sentence ["text"].Value;
				answers.Add (answer);
				AddOptions (answer, sentence);
				sentenceAudios.Add (sentence["audio"].Value);
			}
			sentenceRandomizer = Randomizer.New (answers.Count - 1);
		}

		private void AddOptions (string answer, JSONClass sentence) {
			List<string> l = new List<string>(answer.Split(' '));
			foreach (JSONNode extra in sentence["extras"] as JSONArray) {
				l.Add (extra.Value);
			}
			options.Add (Randomizer.RandomizeList (l));
		}

		public override void NextChallenge () {
			currentIndex = sentenceRandomizer.Next ();
			currentRound++;
		}

		public bool IsCorrect (List<string> result) {
			string currentAnswer = answers [currentIndex];
			if (result.Count == 0) return false;
			var triedAnswer = string.Join (" ", result.ToArray ());
			Debug.Log ("Tried: " + triedAnswer);
			Debug.Log ("Current: " + currentAnswer);
			return currentAnswer == triedAnswer;
		}

		public List<string> GetOptions () {
			return options [currentIndex];
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public AudioClip GetSentenceAudio () {
			return Resources.Load<AudioClip> ("Audio/" + I18n.Msg ("words.locale") + "/Sentences/" + sentenceAudios[currentIndex]);
		}

		public override void RequestHint () { }
	}
}
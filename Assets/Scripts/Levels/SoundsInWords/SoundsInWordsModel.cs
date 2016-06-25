using System;
using Assets.Scripts.App;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using Assets.Scripts.Common;
using I18N;
using System.Linq;

namespace Assets.Scripts.Levels.SoundsInWords {
	public class SoundsInWordsModel : LevelModel {
		private int currentRound;
		private Dictionary<string, List<string>> words;

		private Word word;
		private AudioClip syllable;
		private Randomizer wordRandomizer;

		public static int ROUNDS = 10;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadWords();
		}

		private void LoadWords () {
			words = new Dictionary<string, List<string>> ();
			TextAsset JSONstring = Resources.Load<TextAsset> (I18n.Msg ("syllables.fileName"));
			JSONClass data = JSON.Parse (JSONstring.text) as JSONClass;
			foreach (KeyValuePair<string, JSONNode> word in data) {
				List<string> l = new List<string> ();
				foreach (JSONNode syllables in word.Value.AsArray)
					l.Add (syllables.Value);
				words.Add (word.Key, l);
			}
			wordRandomizer = Randomizer.New (words.Count - 1);
		}

		public override void NextChallenge () {
			SetWord ();
			SetRandomSyllable ();
			currentRound++;
		}

		private void SetWord () {
			string wordString = Enumerable.ToList (words.Keys)[wordRandomizer.Next ()];
			word = Words.GetWord (wordString);
		}

		private void SetRandomSyllable () {
			List<string> syllables = words [word.Name ().ToLower ()];
			syllable = Words.SyllableClip (syllables [Randomizer.New(syllables.Count - 1).Next ()]);
		}

		public List<AudioClip> GetWordSyllables () {
			List<AudioClip> result = new List<AudioClip> ();
			foreach (string syllableString in words[word.Name ().ToLower ()]) {
				result.Add (Words.SyllableClip(syllableString));
			}
			return result;
		}

		public Word GetCurrentWord () { return word; }

		public AudioClip GetSyllable () {
			return syllable;
		}

		public bool IsCorrect (string answer) {
			return Words.RipSymbols (answer).ToUpper () == syllable.name.ToUpper ();
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public override void RequestHint () { }
	}
}
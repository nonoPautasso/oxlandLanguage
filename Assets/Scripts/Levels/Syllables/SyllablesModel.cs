using System;
using Assets.Scripts.App;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;
using Assets.Scripts.Common;
using System.Linq;
using System.Configuration;

namespace Assets.Scripts.Levels.Syllables {
	public class SyllablesModel : LevelModel {
		private Dictionary<string, List<string>> words;
		private int currentPage;
		public static int SYLLABLES = 10;
		private List<AudioClip> syllables;
		private Word word;
		private Randomizer wordRandomizer;
		private int currentRound;

		public static int ROUNDS = 7;

		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			words = new Dictionary<string, List<string>> ();
			LoadWordSyllables ();
		}

		private void LoadWordSyllables () {
			TextAsset JSONstring = Resources.Load<TextAsset> ("syllables_es");
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
			syllables = new List<AudioClip> ();
			SetWord ();
			SetSyllables ();
			currentRound++;
		}

		private void SetSyllables () {
			while (syllables.Count < SYLLABLES) {
				AudioClip syllable = Words.RandomSyllable ();
				if (!syllables.Contains (syllable))
					syllables.Add (syllable);
			}
			syllables = Randomizer.RandomizeList (syllables);
		}

		private void SetWord () {
			string wordString = Enumerable.ToList (words.Keys)[wordRandomizer.Next ()];
			word = Words.GetWord (wordString);
			AddWordSyllables(words[wordString]);
		}

		private void AddWordSyllables (List<string> s) {
			syllables.AddRange (GetCorrectSyllables ());
		}

		public bool IsCorrect (List<string> result) {
			List<string> correct = words [word.Name ().ToLower ()];
			if (result.Count != correct.Count)
				return false;

			for (int i = 0; i < correct.Count; i++) {
				if (result [i].ToLower () != correct [i].ToLower ())
					return false;
			}

			return true;
		}

		public List<AudioClip> GetCorrectSyllables () {
			List<AudioClip> result = new List<AudioClip> ();
			foreach (string syllableString in words[word.Name ().ToLower ()]) {
				result.Add (Words.SyllableClip(syllableString));
			}
			return result;
		}

		public bool GameEnded () { return currentRound == ROUNDS; }

		public Word GetWord(){ return word; }

		public List<AudioClip> GetSyllables(){ return syllables; }

		public override void RequestHint () { }
	}
}
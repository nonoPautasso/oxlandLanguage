using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Assets.Scripts.Common {
	public class Words {
		private static string[] vowels = new string[] { "A", "E", "I", "O", "U" };
		private static string[] alphabet = new string[] {"A", "B","C","D","E", "F","G","H","I", "J",
			"K","L","M","N", "Ñ","O", "P","Q","R","S","T","U", "V","W","X","Y","Z"};
		private static Randomizer alphabetRandomizer = Randomizer.New(alphabet.Count() - 1);

		//alphabet letter -> list of tuples including audio clip of word and sprite number. Use AudioClip.name to get word.
		private static Dictionary<string, List<Word>> words;

		static Words(){
			LoadAllWords();
		}

		private Words () { }

		public static void LoadAllWords() {
			words = new Dictionary<string, List<Word>>();
			int count = 0;
			foreach (string letter in alphabet) {
				List<Word> l = new List<Word>();
				List<AudioClip> audioFiles = Resources.LoadAll<AudioClip> ("Audio/Spanish/" + letter + "Words/").ToList ();
				foreach (AudioClip audio in audioFiles) {
					l.Add (new Word(audio, count));
					count++;
				}
				words [letter] = l;
			}
		}

		public static Word GetRandomWord(bool includeVowels = true){
			string letter = alphabet[alphabetRandomizer.Next()];
			while ((!includeVowels && vowels.Contains (letter)) || words[letter].Count == 0) letter = alphabet [alphabetRandomizer.Next ()];
			List<Word> audios = words[letter];
			return audios[Randomizer.RandomInRange(audios.Count - 1)];
		}

		public static List<Word> GetRandomWordsFromLetter(string letter, int quantity){
			List<Word> letterWords = words[letter.ToUpper ()];
			List<Word> result = new List<Word> ();
			Randomizer randomizer = Randomizer.New(letterWords.Count - 1);
			for (int i = 0; i < quantity; i++) {
				result.Add (letterWords[randomizer.Next ()]);
			}
			return result;
		}

		public static List<Word> GetRandomWords (int quantity, int correct, string letter) {
			List<Word> result = GetRandomWordsFromLetter(letter, correct);
			for (int i = 0; i < quantity - correct; i++) {
				Word w = GetRandomWord(false);
				while (result.Contains (w)) w = GetRandomWord (false);
				result.Add(w);
			}

			return Randomizer.RandomizeList(result);
		}

		public static string[] GetVowels(){
			return vowels;
		}

		public static string[] GetAlphabet(){
			return alphabet;
		}

		public static Dictionary<string, List<Word>> GetWords() {
			return words;
		}
	}
}
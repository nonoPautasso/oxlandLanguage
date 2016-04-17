using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;
using System.IO;
using System.Linq;
using UnityEditor;
using I18N;

namespace Assets.Scripts.Common {
	public class Words {
		private static string[] vowels = new string[] { "A", "E", "I", "O", "U" };
		private static string[] alphabet = new string[] {"A", "B","C","D","E", "F","G","H","I", "J",
			"K","L","M","N", "O", "P","Q","R","S","T","U", "V","W","X","Y","Z"};
		private static Randomizer alphabetRandomizer = Randomizer.New(alphabet.Count() - 1);
		static string currentPath;

		//alphabet letter -> list of tuples including audio clip of word and sprite number. Use AudioClip.name to get word.
		private static Dictionary<string, List<Word>> words;

		static Words(){
			LoadAllWords();
		}

		private Words () { }

		private static void LoadAllWords() {
			words = new Dictionary<string, List<Word>>();
			int count = 0;
			currentPath = CurrentPath ();
			foreach (string letter in alphabet) {
				List<Word> l = new List<Word>();
				List<AudioClip> audioFiles = Resources.LoadAll<AudioClip>(currentPath + letter + "Words/").ToList ();
				foreach (AudioClip audio in audioFiles) {
					l.Add (new Word(audio, count));
					count++;
				}
				words [letter] = l;
			}
		}

		static string CurrentPath () {
			return "Audio/" + I18n.Msg ("words.locale") + "/";
		}

		public static Word GetRandomWord(bool includeVowels = true){
			CheckLoadedWords();
			string letter = alphabet[alphabetRandomizer.Next()];
			while ((!includeVowels && vowels.Contains (letter)) || words[letter].Count == 0) letter = alphabet [alphabetRandomizer.Next ()];
			List<Word> audios = words[letter];
			return audios[Randomizer.RandomInRange(audios.Count - 1)];
		}

		public static List<Word> GetRandomWordsFromLetter(string letter, int quantity){
			CheckLoadedWords();
			List<Word> letterWords = words[letter.ToUpper ()];
			List<Word> result = new List<Word> ();
			Randomizer randomizer = Randomizer.New(letterWords.Count - 1);
			for (int i = 0; i < quantity; i++) {
				result.Add (letterWords[randomizer.Next ()]);
			}
			return result;
		}

		public static Word GetRandomWordFromLetter(string letter){
			return GetRandomWordsFromLetter (letter, 1)[0];
		}

		public static List<Word> GetRandomWords (int quantity, int correct, string letter, bool includeVowels) {
			CheckLoadedWords();
			List<Word> result = GetRandomWordsFromLetter(letter, correct);
			for (int i = 0; i < quantity - correct; i++) {
				Word w = GetRandomWord(includeVowels);
				while (result.Contains (w)) w = GetRandomWord (includeVowels);
				result.Add(w);
			}

			return Randomizer.RandomizeList(result);
		}

		public static string[] GetVowels(){
			CheckLoadedWords();
			return vowels;
		}

		public static string[] GetAlphabet(){
			CheckLoadedWords();
			return alphabet;
		}

		public static Dictionary<string, List<Word>> GetWords() {
			CheckLoadedWords();
			return words;
		}

		static void CheckLoadedWords () {
			if (currentPath != CurrentPath()) LoadAllWords();
		}

		public static string RandomLetter (int endOffset = 1, int startOffset = 0) {
			CheckLoadedWords();
			int letterNumber = alphabetRandomizer.Next ();
			while(letterNumber < startOffset || letterNumber > alphabet.Count () - endOffset || IsAnyWordsEmpty(letterNumber, endOffset))
				letterNumber = alphabetRandomizer.Next ();

			return alphabet [letterNumber];
		}

		static bool IsAnyWordsEmpty (int letterNumber, int quantity) {
			for (int i = 0; i < quantity; i++) {
				List<Word> letterWords = words [alphabet [letterNumber + i]];
				if (letterWords.Count == 0) return true;
			}
			return false;
		}

		public static string NextLetter (string letter) {
			return alphabet [Array.IndexOf (alphabet, letter) + 1];
		}

		public static List<string> RandomLetters (int quantity) {
			List<string> result = new List<string> ();
			for (int i = 0; i < quantity; i++) {
				string letter = RandomLetter ();
				while (result.Contains (letter))
					letter = RandomLetter ();
				result.Add (letter);
			}
			return result;
		}
	}
}
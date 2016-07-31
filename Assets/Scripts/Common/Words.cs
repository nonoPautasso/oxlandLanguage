using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.App;
using System.IO;
using System.Linq;
//using UnityEditor;
using I18N;
using System.Text.RegularExpressions;
using System.Text;
using SimpleJSON;

namespace Assets.Scripts.Common {
	public class Words {
		private static string[] vowels = new string[] { "A", "E", "I", "O", "U" };
		private static List<string> numbers = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
		private static string[] alphabet;
		private static Randomizer alphabetRandomizer;
		static string currentPath;

		//alphabet letter -> list of tuples including audio clip of word and sprite number.
		private static Dictionary<string, List<Word>> words;
		private static Dictionary<string, List<Word>> extraWords;
		private static Dictionary<string, List<AudioClip>> syllables;

		static Words(){
			LoadAllWords();
			LoadSyllables ();
			LoadExtraWords ();
		}

		private Words () { }

		private static void LoadAllWords() {
			words = new Dictionary<string, List<Word>>();
			CheckAlphabet ();
			currentPath = CurrentPath ();
			JSONClass allWordsJson = GetAllWordsObject (I18n.Msg ("words.allWordsJson"));
			foreach (string letter in alphabet) {
				int count = 0;
				List<Word> l = new List<Word>();
				List<AudioClip> audioFiles = Resources.LoadAll<AudioClip>(currentPath + letter + "Words/").ToList ();
				foreach (AudioClip audio in audioFiles) {
					if (allWordsJson == null)
						l.Add (new Word (audio, count));
					else
						l.Add (new Word (audio, count, GetRealWordWrite (allWordsJson, audio)));
					count++;
				}
				words [letter] = l;
			}
		}

		private static void LoadExtraWords () {
			extraWords = new Dictionary<string, List<Word>>();
			currentPath = CurrentPath ();
			JSONClass extraWordsJson = GetExtraWordsObject (I18n.Msg ("words.allWordsJson"));

			List<AudioClip> extraWordsClips = Resources.LoadAll<AudioClip>(currentPath + "Pictograms/ExtraWords/").ToList ();
			foreach (AudioClip extraWordAudio in extraWordsClips) {
				string firstLetter = extraWordAudio.name.ToCharArray ()[0].ToString ().ToUpper ();
				if(!extraWords.ContainsKey (firstLetter)){
					extraWords [firstLetter] = new List<Word> ();
				}


				JSONClass word = extraWordsJson [extraWordAudio.name.ToLower ()] as JSONClass;
				if (word == null) {
					Debug.Log ("HOLA GUTE YO NO ESTOY EN EL JSON! : " + extraWordAudio.name);

				}

				extraWords [firstLetter].Add (new Word (extraWordAudio, GetWord(word ["position"].Value).SpriteNumber(), GetRealWordWrite (extraWordsJson, extraWordAudio),word["position"].Value));
			}

		}

		private static void LoadSyllables () {
			syllables = new Dictionary<string, List<AudioClip>>();
			currentPath = CurrentPath ();
			List<AudioClip> syllablesClips = Resources.LoadAll<AudioClip>(currentPath + "Syllables/").ToList ();
			foreach (AudioClip syllable in syllablesClips) {
				string firstLetter = syllable.name.ToCharArray ()[0].ToString ().ToUpper ();
				if(!syllables.ContainsKey (firstLetter)){
					syllables [firstLetter] = new List<AudioClip> ();
				}
				syllables [firstLetter].Add (syllable);
			}
		}


		private static string GetRealWordWrite (JSONClass wordsJson, AudioClip audio) {
			JSONClass word = wordsJson [audio.name.ToLower ()] as JSONClass;
			if (word == null) {
				Debug.Log ("HOLA MARY YO NO ESTOY EN EL JSON! : " + audio.name);
				return null;
			}
			return word ["word"].Value;
		}

		private static JSONClass GetAllWordsObject (string allWords) {
			if(allWords != ""){
				TextAsset json = Resources.Load<TextAsset> (allWords);
				return (JSON.Parse (json.text) as JSONClass)["wordsSpanish"] as JSONClass;
			}
			return null;
		}

		private static JSONClass GetExtraWordsObject (string allWords) {
			if(allWords != ""){
				TextAsset json = Resources.Load<TextAsset> (allWords);
				return (JSON.Parse (json.text) as JSONClass)["extraWords"] as JSONClass;
			}
			return null;
		}



		static void CheckAlphabet () {
			if(I18n.GetLocale () == "en-US"){
				alphabet = new string[] {"A", "B","C","D","E", "F","G","H","I", "J",
					"K","L","M","N", "O", "P","Q","R","S","T","U", "V","W","X","Y","Z"};
			} else {
				alphabet = new string[] {"A", "B","C","D","E", "F","G","H","I", "J",
					"K","L","M","N", "Ñ", "O", "P","Q","R","S","T","U", "V","W","X","Y","Z"};
			}

			alphabetRandomizer = Randomizer.New(alphabet.Length - 1);
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
				while (result.Contains (w) || FulfillsBVCQK(letter, w)) w = GetRandomWord (includeVowels);
				result.Add(w);
			}

			return Randomizer.RandomizeList(result);
		}

		private static bool FulfillsBVCQK (string letter, Word w) {
			if(letter == "B"){
				return w.StartLetter () == "V";
			} else if(letter == "V"){
				return w.StartLetter () == "B";
			} else if(letter == "C"){
				return w.StartLetter () == "Q" || w.StartLetter () == "K";
			} else if(letter == "Q"){
				return w.StartLetter () == "C" || w.StartLetter () == "K";
			} else if(letter == "K"){
				return w.StartLetter () == "Q" || w.StartLetter () == "C";
			}
			return false;
		}

		public static string[] GetVowels(){
			CheckLoadedWords();
			return vowels;
		}

		public static List<string> GetNumbers(){
			return numbers;
		}

		public static string[] GetAlphabet(){
			CheckLoadedWords();
			return alphabet;
		}

		public static List<string> GetConsonants () {
			CheckAlphabet ();
			List<string> aux = new List<string> ();
			foreach (string letter in alphabet) {
				if(Array.IndexOf (vowels, letter) == -1){
					aux.Add (letter);
				}
			}
			return aux;
		}

		public static Dictionary<string, List<Word>> GetWords() {
			CheckLoadedWords();
			return words;
		}

		static void CheckLoadedWords () {
			if (currentPath != CurrentPath ()) {
				LoadAllWords ();
				LoadSyllables ();
				LoadExtraWords ();
			}
		}

		public static string RandomLetter (int endOffset = 1, int startOffset = 0) {
			CheckLoadedWords();
			int letterNumber = alphabetRandomizer.Next ();
			while(letterNumber < startOffset || letterNumber > alphabet.Count () - endOffset || IsAnyWordsEmpty(letterNumber, endOffset))
				letterNumber = alphabetRandomizer.Next ();

			return alphabet [letterNumber];
		}

		public static string RandomConsonant(){
			string letter = RandomLetter ();
			return Array.IndexOf (vowels, letter) != -1 ? RandomConsonant () : letter;
		}

		public static string RandomVowel(){
			return vowels [Randomizer.RandomInRange (vowels.Length - 1)];
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

		public static List<string> RandomLetters (int quantity, bool includeVowels = true) {
			CheckLoadedWords ();
			List<string> result = new List<string> ();
			while(result.Count < quantity) {
				string letter = RandomLetter ();
				while (result.Contains (letter) || (!includeVowels && Array.IndexOf (vowels, letter) != -1)) {
					letter = RandomLetter ();
				}
				result.Add (letter);
			}
			return result;
		}

		public static AudioClip RandomSyllable(){
			CheckLoadedWords ();
			AudioClip result = null;
			while(result == null){
				string letter = alphabet [alphabetRandomizer.Next ()];
				if(syllables.ContainsKey (letter)){
					List<AudioClip> letterSyllables = syllables [letter];
					result = letterSyllables [Randomizer.RandomInRange (letterSyllables.Count - 1)];
				}
			}
			return result;
		}

		//Param is Word's Name
		public static Word GetWord (string wordString) {
			string firstLetter = wordString.ToCharArray () [0].ToString ().ToUpper ();
			foreach (Word word in words[firstLetter]) {
				if (word.Name ().ToLower () == wordString.ToLower ())
					return word;
			}
			return null;
		}

		//Param is Word's Name
		public static Word GetWordOrExtraWord (string wordString) {

			string firstLetter = wordString.ToCharArray () [0].ToString ().ToUpper ();
			foreach (Word word in words[firstLetter]) {
				if (word.Name().ToLower () == wordString.ToLower ())
					return word;
			}
			if (extraWords [firstLetter]!=null) {
				
				foreach (Word extraWord in  extraWords[firstLetter]) {
					if (extraWord.Name().ToLower () == wordString.ToLower ())
						return extraWord;
				}
			}

			return null;
		}

		public static AudioClip SyllableClip (string syllableString) {
			string firstLetter = syllableString.ToCharArray () [0].ToString ().ToUpper ();
			foreach (AudioClip clip in syllables[firstLetter]) {
				if (clip.name.ToLower () == syllableString.ToLower ())
					return clip;
			}
			return null;
		}

		public static void PlayLetter (string letter) {
			var clip = LetterClip (letter);
			SoundManager.instance.PlayClip(clip);
		}

		public static AudioClip LetterClip (string letter) {
			string rippedLetter = RipSymbols (letter);
			return Resources.Load<AudioClip> ("Audio/" + I18n.Msg ("words.locale") + "/Letters/" + rippedLetter);
		}

		public static List<AudioClip> LetterClips (List<string> letters) {
			List<AudioClip> result = new List<AudioClip> ();
			foreach (string letter in letters) {
				result.Add (LetterClip (letter));
			}
			return result;
		}

		public static string RipSymbols(string s){
			string normalized = s.Normalize(NormalizationForm.FormD);
			Regex reg = new Regex("[^a-zA-Z0-9 ]");
			return reg.Replace(normalized, "");
		}

		public static List<string> GetWordsWithLength (List<int> lengths) {
			CheckLoadedWords ();
			List<string> result = new List<string> ();

			foreach (string letter in alphabet) {
				foreach (Word word in words[letter]) {
					if (lengths.Contains (RipSymbols(word.Name ()).Length)) result.Add (word.Name ());
				}
			}
			return result;
		}

		public static List<Word> GetWordsWithGreaterLength (int length) {
			CheckLoadedWords ();
			List<Word> result = new List<Word> ();

			foreach (string letter in alphabet) {
				foreach (Word word in words[letter]) {
					if (RipSymbols(word.Name ()).Length >= length) result.Add (word);
				}
			}
			return result;
		}

		public static List<Word> GetWordsWithLowerLength (int length) {
			CheckLoadedWords ();
			List<Word> result = new List<Word> ();

			foreach (string letter in alphabet) {
				foreach (Word word in words[letter]) {
					if (RipSymbols(word.Name ()).Length <= length) result.Add (word);
				}
			}
			return result;
		}

		public static bool IsConsonant(string letter) {
			return GetConsonants ().Contains (letter);
		}

		public static bool IsVowel (string letter) {
			foreach (string vowel in vowels) {
				if (RipSymbols(letter.ToUpper ()) == vowel.ToUpper ()) return true;
			}
			return false;
		}

		public static AudioClip GetPhonem (string letter) {
			return Resources.Load<AudioClip>("Audio/" + I18n.Msg ("words.locale") + "/Phonemes/" + letter);
		}

		public static List<AudioClip> GetPhonemes (string word) {
			List<AudioClip> audios = new List<AudioClip>();
			foreach (char letter in word) {
				audios.Add (Words.GetPhonem(letter.ToString ()));
			}
			return audios;
		}

		public static List<Word> WordsWithPrefix (string startSound) {
			CheckLoadedWords ();
			List<Word> letterWords = words [startSound [0].ToString ()];
			return letterWords.FindAll ((Word w) => w.StartLetters (2) == startSound.ToUpper ());
		}

		public static List<AudioClip> GetAudios (List<Word> round) {
			return round.ConvertAll ((Word w) => w.GetAudio ());
		}

		//Shit I have to iterate all words!
		public static List<Word> WordsWithSuffix (string endSound) {
			CheckLoadedWords ();
			List<Word> result = new List<Word> ();
			foreach (List<Word> letterWords in words.Values) {
				result.AddRange (letterWords.FindAll ((Word w) => w.EndLetters (2) == endSound.ToUpper ()));
			}
			return result;
		}
	}
}
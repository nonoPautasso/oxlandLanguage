using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using SimpleJSON;
using System.Linq;


namespace Assets.Scripts.Levels.OracionesPictogramas {
	public class OracionesPictogramasModel : LevelModel {
		private Randomizer structureRandomizer;
		private int currentRound;
		private string currentSentence;
		private string currentGender;
	
		private JSONClass currentStructure;

		public static int ROUNDS = 5;
		public static int BUBBLES = 5;
		public static string[] TAGS = new string[]{"characters","places","objects","transport","food","storage","actions"};

		private List<JSONClass> structures;
		private List<Word> answers;
		private List<string> answerSprites;
		private List<Word> allWords;
		private List<string>[] characters, objects,places,transport,food,storage;
		private List<string> actions;

		private List<AudioClip> sentenceAudios;
		private List<AudioClip> extraAudios;





		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadTags ();
			LoadStructures ();
			LoadAudios ();
		}

		public bool IsCorrect (List<string> playerAnswers)
		{
			foreach (string answer in playerAnswers) {
				if (answerSprites.IndexOf (answer) == -1)
					return false;
			}
			return true;
		}

	
		void LoadAudios ()
		{
			extraAudios = Resources.LoadAll<AudioClip> ("Audio/Spanish/Pictograms/SentenceWords/").ToList ();

		}

	

		AudioClip GetAudioByName(string audioName){
			foreach (AudioClip audio in extraAudios) {
				if (audioName == audio.name) {
					return audio;
				}
				Debug.Log ("audioname: "+audio.name);
			}
			return null;
		}

	
		void LoadTags ()
		{
			TextAsset sentencesAsset = Resources.Load<TextAsset> ("jsons/pictogramTags");
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			//MAIN TAG ARRAYS
			characters = parseJSONValues(data["characters"]);
			objects = parseJSONValues(data["objects"]);
			places = parseJSONValues(data["places"]);
			transport = parseJSONValues(data["transport"]);
			food = parseJSONValues(data["food"]);
			storage = parseJSONValues(data["storage"]);
			characters = parseJSONValues(data["characters"]);
			actions = new List<string> ();


			//TEST
			//TestAllWords();


			JSONArray jsonArray = data ["actions"] as JSONArray;
			foreach (JSONNode action in jsonArray) {
				actions.Add (action);
			}
		}

		//Creates TAG arrays with male and female words
		List<string>[] parseJSONValues (JSONNode jSONNode)
		{
			List<string>[] array = new List<string>[2];

			List<string> maleItems = new List<string> ();
			JSONArray jsonArray = jSONNode ["male"] as JSONArray;
			foreach (JSONNode maleItem in jsonArray) {
				maleItems.Add (maleItem);
			}
			array [0] = maleItems;

			List<string> femaleItems = new List<string> ();
			jsonArray = jSONNode ["female"] as JSONArray;
			foreach (JSONNode femaleItem in jsonArray) {
				femaleItems.Add (femaleItem);
			}
			array [1] = femaleItems;

			return array;
		}

		void LoadStructures ()
		{
			structures = new List<JSONClass> ();

			TextAsset sentencesAsset = Resources.Load<TextAsset> ("jsons/pictogramStructures");
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			foreach (JSONNode structure in data["structures"] as JSONArray) {
				JSONClass s = structure as JSONClass;
				structures.Add (s);
			}
				
			structureRandomizer = Randomizer.New (structures.Count - 1);
		}

		public override void NextChallenge () {
			
			var index = structureRandomizer.Next ();
			currentStructure = structures [index];

			BuildSentence ();
			SetWords ();
		
			currentRound++;
		}

		void SetWords ()
		{
			allWords = new List<Word> ();
			int i;
			for (i = 0; i < answers.Count; i++) {
				allWords.Add (answers[i]);
			}
			for (i=answers.Count; i < BUBBLES; i++) {
				Word word = allWords [0];
				while (allWords.IndexOf (word) != -1) {
					 word = Words.GetRandomWord ();
				}
				allWords.Add (word);
			}


		}


		void BuildSentence ()
		{
			currentSentence = "";
			sentenceAudios = new List<AudioClip> ();
			List<string> sentenceStructure = new List<string> ();
			JSONArray array = currentStructure ["structure"] as JSONArray;

			//Start with male||female
			currentGender =(Randomizer.RandomBoolean()) ? "male" : "female";
			answers = new List<Word> ();
			answerSprites = new List<string> ();

			foreach (JSONNode structureWord in array) {
				//sentenceStructure.Add (structureWord.Value);
				string word = getSentenceWord(structureWord.Value);
				sentenceStructure.Add(word);
				currentSentence += (word+" ");
				//Debug.Log (word);
			}

			Debug.Log (currentSentence);
				
		}

		string getSentenceWord (string value)
		{
			int genderIndex = (currentGender == "male") ? 0 : 1;
			List<string> genderWords = new List<string>();

			if (isTag (value)) {
				switch (value) {
				case "characters":
					genderWords = characters [genderIndex];
					break;
				case "objects":
					genderWords = objects [genderIndex];
					break;
				case "places":
					genderWords = places [genderIndex];
					break;
				case "transport":
					genderWords = transport [genderIndex];
					break;
				case "food":
					genderWords = food [genderIndex];
					break;
				case "storage":
					genderWords = storage [genderIndex];
					break;
				case "actions": 
					string action = actions [Randomizer.RandomInRange (actions.Count - 1, 0)];
					Word actionAnswer = Words.GetWordOrExtraWord (action);
					answers.Add (actionAnswer);
					answerSprites.Add (actionAnswer.Sprite ().name);
					sentenceAudios.Add (actionAnswer.GetAudio ());
					return action;
				}

				string word = genderWords [Randomizer.RandomInRange (genderWords.Count, 0)];
				Debug.Log (word);
				Word answer = Words.GetWordOrExtraWord (word);
				answers.Add (answer);
				answerSprites.Add (answer.Sprite ().name);
		
				sentenceAudios.Add (answer.GetAudio ());

				//Change gender
				currentGender = (currentGender=="male") ? "female" : "male";
				return word;
				
			} else if (isConector (value)) {
			//	Debug.Log (currentStructure [value]);
				sentenceAudios.Add(GetAudioByName(currentStructure [value]));
				return currentStructure [value];

			} else {
				//Debug.Log (currentStructure [value] [currentGender]);
				sentenceAudios.Add(GetAudioByName(currentStructure [value][currentGender]));
				return currentStructure [value] [currentGender];
			}
				
		}


		bool isTag (string value)
		{
			return (Array.IndexOf(TAGS, value)!=-1);
		}

		bool isConector (string value)
		{
			return (value == "conector" || value == "verbConector");
		}

		public override void RequestHint () { 
			sentenceAudios = new List<AudioClip> ();
			foreach (Word answer in answers) {
				sentenceAudios.Add (answer.GetAudio ());
			}
		}

		public bool GameEnded () { return currentRound == ROUNDS; }


		public List<AudioClip> SentenceAudios {
			get {
				return sentenceAudios;
			}
		}	

		public List<Word> Answers {
			get {
				return answers;
			}
		}

		public string CurrentSentence {
			get {
				return currentSentence;
			}
		}

		public List<Word> AllWords {
			get {
				return allWords;
			}
		}


		void TestAllWords (int gender)
		{
			foreach (string word in storage[gender]) {
				Debug.Log (word);
				string charact = Words.GetWordOrExtraWord (word).Name();
			}

			foreach (string word in actions) {
				Debug.Log (word);
				string charact = Words.GetWordOrExtraWord (word).Name();
			}

			foreach (string word in transport[gender]) {
				Debug.Log (word);
				string charact = Words.GetWordOrExtraWord (word).Name();
			}

			foreach (string word in places[gender]) {
				Debug.Log (word);
				string charact = Words.GetWordOrExtraWord (word).Name();
			}

			foreach (string word in characters[1]) {
				Debug.Log (word);
				string charact = Words.GetWordOrExtraWord (word).Name();
			}

			foreach (string word in objects[gender]) {
				Debug.Log (word);
				string obj = Words.GetWordOrExtraWord (word).Name();
			}

			foreach (string word in food[gender]) {
				Debug.Log (word);
				string obj = Words.GetWordOrExtraWord (word).Name();
			}
		}


}
}
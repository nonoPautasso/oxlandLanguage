using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;
using SimpleJSON;

namespace Assets.Scripts.Levels.OracionesPictogramas {
	public class OracionesPictogramasModel : LevelModel {
		private Randomizer structureRandomizer;
		private int currentRound;
		private string currentSentence;
	
		private JSONClass currentStructure;

		public static int ROUNDS = 5;
		public static string[] TAGS = new string[]{"characters","places","objects","transport","food","storage","actions"};

		private List<JSONClass> structures;
		private List<Word> answers;
		private List<string>[] characters, objects,places,transport,food,storage;
		private List<string> actions;
	


		public override void StartGame () {
			currentRound = 0;

			minSeconds = 15;
			pointsPerError = 200;
			pointsPerSecond = 13;

			LoadTags ();
			LoadStructures ();
		}
	
		void LoadTags ()
		{
			TextAsset sentencesAsset = Resources.Load<TextAsset> ("jsons/pictogramTags");
			JSONClass data = JSON.Parse (sentencesAsset.text) as JSONClass;

			characters = parseJSONValues(data["characters"]);
			objects = parseJSONValues(data["objects"]);
			places = parseJSONValues(data["places"]);
			transport = parseJSONValues(data["transport"]);
			food = parseJSONValues(data["food"]);
			storage = parseJSONValues(data["storage"]);
			characters = parseJSONValues(data["characters"]);
			actions = new List<string> ();
			JSONArray jsonArray = data ["actions"] as JSONArray;
			foreach (JSONNode action in jsonArray) {
				action.Add (action);
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
			LoadTags ();
			BuildSentence ();
			//answer = answers [index];
			//SetLetters();
			currentRound++;
		}




		void BuildSentence ()
		{
			List<string> sentenceStructure = new List<string> ();
			JSONArray array = currentStructure ["structure"] as JSONArray;

			foreach (JSONNode structureWord in array) {
				//sentenceStructure.Add (structureWord.Value);
				sentenceStructure.Add(getSentenceWord(structureWord.Value));
			}

			//TODO: Una vez que tengo la oracion armada -> agarrar sonidos y sprites?
				
		}

		string getSentenceWord (string value)
		{
			//Start with male||female
			int gender =(Randomizer.RandomBoolean()) ? 0 : 1;
			Randomizer wordsRandomizer;

			if (isTag (value)) {
				
			}

			//TODO: Ver como son las reglas para cambiar de genero, articulo + char
			/*if (value is TAG)
				add to answers list
				add to sentence structure -> return string
			else
				get value from currentStructure["value"]
				--> switch 
						case article
						case location conector
						case bla
				add word to sentence structure -> return string
			*/
			return "";
		}

		bool isTag (string value)
		{
			return (Array.IndexOf(TAGS, value)!=-1);
		}

		public override void RequestHint () { }
}
}
using System;
using Assets.Scripts.App;
using UnityEngine;
using SimpleJSON;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.Syllables {
	public class SyllablesModel : LevelModel {
		private Dictionary<string, List<string>> model;
		private int currentPage;

		public override void StartGame () {
			model = new Dictionary<string, List<string>> ();
			TextAsset JSONstring = Resources.Load<TextAsset>("syllables_es");
			JSONClass data = JSON.Parse(JSONstring.text) as JSONClass;

			foreach (KeyValuePair<string, JSONNode> word in data) {
				List<string> l = new List<string> ();
				foreach (JSONNode syllables in word.Value.AsArray)
					l.Add (syllables.Value);

				model.Add (word.Key, l);
			}
		}

		public override void NextChallenge () {
			
		}

		public override void RequestHint () {
			
		}
	}
}
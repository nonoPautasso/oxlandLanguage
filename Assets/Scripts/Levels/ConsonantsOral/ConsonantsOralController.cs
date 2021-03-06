﻿using System;
using Assets.Scripts.App;

//THIS INCLUDES VOWELS ORAL TOO. MONODEVELOP WONT LET ME CHANGE CLASS NAMES, IT'S SO BROKEN....
namespace Assets.Scripts.Levels.ConsonantsOral {
	public class ConsonantsOralController : LevelController {
		private ConsonantsOralModel model;
		public ConsonantsOralView view;
		public bool isVowels;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord(), model.GetLetters());
		}

		public override void ShowHint () {
			LogHint();
			view.Hint (model.GetHint ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new ConsonantsOralModel(isVowels);
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public string PlayWord () {
			model.GetCurrentWord ().PlayWord ();
			return model.GetCurrentWord ().Name();
		}

		public void Try (int index) {
			bool isCorrect = model.IsCorrect (index);
			LogAnswer (isCorrect);
			if (isCorrect) {
				view.Correct (model.GetCurrentWord (), index);
			} else {
				view.Wrong (index);
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}


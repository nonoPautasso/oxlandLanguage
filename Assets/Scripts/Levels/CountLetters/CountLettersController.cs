using System;
using Assets.Scripts.App;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.CountLetters {
	public class CountLettersController : LevelController {
		private CountLettersModel model;
		public CountLettersView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord());
		}

		public override void ShowHint () {
			LogHint();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new CountLettersModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayWord () {
			model.GetCurrentWord ().PlayWord ();
		}

		public void Try (int answer) {
			var isCorrect = model.IsCorrect (answer);
			LogAnswer (isCorrect);
			if (isCorrect) {
				Correct ();
			} else {
				Wrong ();
			}
		}

		private void Correct () {
			view.Correct ();
		}

		private void Wrong () {
			view.Wrong ();
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}


using System;
using Assets.Scripts.App;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.CompleteConsonant {
	public class CompleteConsonantController : LevelController {
		private CompleteConsonantModel model;
		public CompleteConsonantView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord(), model.GetLetters (), model.GetAnswer());
		}

		public override void ShowHint () {
			LogHint();
			view.Hint (model.GetHint ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new CompleteConsonantModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayWord () {
			model.GetCurrentWord ().PlayWord ();
		}

		public void Try (List<int> answers) {
			var isCorrect = model.IsCorrect (answers);
			LogAnswer (isCorrect);
			if (isCorrect) {
				view.Correct (answers);
			} else {
				view.Wrong (answers);
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
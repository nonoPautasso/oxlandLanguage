using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.FindError {
	public class FindErrorController : LevelController {
		public FindErrorView view;
		private FindErrorModel model;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			else {
				model.NextChallenge ();
				view.NextChallenge (model.GetCurrentRound ());
			}
		}

		public void PlaySound () {
			model.GetCurrentRound ().PlayWord ();
		}

		public override void ShowHint () {
			LogHint ();
		}

		public void Try (string answer) {
			bool isCorrect = model.IsCorrect (answer);
			LogAnswer (isCorrect);
			if (isCorrect)
				view.Correct ();
			else
				view.Wrong ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new FindErrorModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void RoundEnd () {
			NextChallenge ();
		}

		public override void RestartGame () { InitGame (); }

		public void Start(){ InitGame (); }
	}
}


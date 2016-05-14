using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.SplitSentences {
	public class SplitSentencesController : LevelController {
		private SplitSentencesModel model;
		public SplitSentencesView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetSentence());
		}

		public override void ShowHint () {
			LogHint();
			view.PaintNextWord (model.GetSentence ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new SplitSentencesModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void Try (string answer) {
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
			NextChallenge ();
		}

		private void Wrong () {
			view.Wrong ();
		}

		public override void RestartGame () {
			view.ResetView ();
			Start();
		}

		public void Start() { InitGame (); }
	}
}
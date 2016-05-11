using System;
using Assets.Scripts.App;

namespace AssemblyCSharp {
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
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new SplitSentencesModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public override void RestartGame () {
			view.ResetView ();
			Start();
		}

		public void Start() { InitGame (); }
	}
}
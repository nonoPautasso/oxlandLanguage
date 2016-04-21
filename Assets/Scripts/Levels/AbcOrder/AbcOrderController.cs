using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderController : LevelController {
		private AbcOrderModel model;
		public AbcOrderView view;

		public override void NextChallenge () {
			model.NextChallenge ();
			view.NextChallenge (model.GetAnswers (), model.GetOptions (), model.GetHelpLetters ());
		}

		public override void ShowHint () {
			LogHint ();
			view.Hint (model.GetAnswers ());
		}

		public void ChallengeFinish () {
			model.RoundFinish ();
			if (model.HasEnded ())
				EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			NextChallenge ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new AbcOrderModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void Try (int index, string answer) {
			bool isCorrect = model.IsCorrect (index, answer);
			LogAnswer (isCorrect);
			if (isCorrect)
				view.Correct (index);
			else
				view.Wrong (index);
		}

		public override void RestartGame () {
			view.ResetLetters ();
			InitGame ();
		}

		public void Start(){ InitGame (); }
	}
}
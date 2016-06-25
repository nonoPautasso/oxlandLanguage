using System;
using Assets.Scripts.App;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.OrderWordsDictionary {
	public class OrderWordsDictionaryController : LevelController {
		public OrderWordsDictionaryView view;
		private OrderWordsDictionaryModel model;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetLetters (), model.GetWords ());
		}

		public override void ShowHint () { LogHint (); }

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new OrderWordsDictionaryModel();
			view.Controller (this);
			view.SetOriginalPositions ();
			model.StartGame();

			NextChallenge ();
		}

		public void Try (List<string> answer) {
			var isCorrect = model.IsCorrect (answer);
			LogAnswer (isCorrect);
			if (isCorrect) {
				view.Correct ();
			} else {
				view.Wrong ();
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
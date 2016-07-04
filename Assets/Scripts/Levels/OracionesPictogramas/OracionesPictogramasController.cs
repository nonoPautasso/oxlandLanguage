using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.OracionesPictogramas {
	public class OracionesPictogramasController : LevelController {
		private OracionesPictogramasModel model;
		public OracionesPictogramasView view;

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new OracionesPictogramasModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge();
		}

		public override void NextChallenge () {
			//if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			//view.NextChallenge (model.GetCurrentWord(), model.GetLetters (), model.GetAnswer());

		}

		public override void ShowHint () {
			//TODO
		}

		public override void RestartGame () { Start(); }
		public void Start() { InitGame (); }

}
}
using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.AbcOrder {
	public class AbcOrderController : LevelController {
		private AbcOrderModel model;
		public AbcOrderView view;

		public override void NextChallenge () {
			model.NextChallenge ();
			view.NextChallenge (model.GetAnswers (), model.GetOptions (), model.GetCurrentRound ());
		}

		public override void ShowHint () {
			
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new AbcOrderModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public override void RestartGame () {
			
		}
	}
}
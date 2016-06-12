using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.ConsonantsVowels {
	public class ConsonantsVowelsController : LevelController {
		public bool isVowels;
		public ConsonantsVowelsView view;
		private ConsonantsVowelsModel model;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			else {
				view.NextChallenge (model.GetLetters (), model.GetOthers ());
				model.NextChallenge ();
			}
		}

		public override void ShowHint () {
			LogHint ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new ConsonantsVowelsModel(isVowels);
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
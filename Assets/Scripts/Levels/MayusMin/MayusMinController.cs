using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.MayusMin {
	public class MayusMinController : LevelController {
		public MayusMinView view;
		private MayusMinModel model;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetSentence (), model.GetLetters ());
		}

		public override void ShowHint () { LogHint (); }

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new MayusMinModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public bool IsCorrect (string draggedLetter, int letterSlotNumber) {
			bool isCorrect = model.IsLetterCorrect (draggedLetter, letterSlotNumber);
			LogAnswer (isCorrect);
			return isCorrect;
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}
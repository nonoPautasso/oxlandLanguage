using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.Syllables {
	public class SyllablesController : LevelController {
		private SyllablesModel model;
		public SyllablesView view;

		public override void NextChallenge () {
			
		}

		public override void ShowHint () {
			LogHint();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new SyllablesModel();
			view.Controller (this);
			model.StartGame();
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}
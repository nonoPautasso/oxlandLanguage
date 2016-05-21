using System;
using Assets.Scripts.App;

namespace Assets.Scripts.Levels.FishNet {
	public class FishNetController : LevelController {
		private FishNetModel model;
		public FishNetView view;

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new FishNetModel();
			view.Controller (this);
			model.StartGame();
			view.StartGame ();
			NextChallenge ();
		}

		public override void NextChallenge () {
			view.ResetViews ();
			view.SetActiveObjects (model.ActiveObjects());
			view.SetAnswers (model.CurrentPage ());
		}

		public override void ShowHint () {
			LogHint ();
			view.Hint (model.GetHint(), model.AllObjects ());
		}

		public void AnswerClick (int index) {
			if(model.IsCorrect(index)){
				bool resetActive = model.Correct (index);
				LogAnswer (true);
				view.SetActiveObjects (model.ActiveObjects ());
				view.CorrectAnswer(model.ActiveObjects (), index);
				if (resetActive) {
					view.PlayAudios ();
					model.ResetActive ();
				} else
					AudioDone ();
			} else {
				//model.Wrong ();
				LogAnswer (false);
				view.WrongAnswer(index, model.WordText(index));
			}
		}

		public void AudioDone () {
			if (model.EndGame ())
				EndGame(model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			else
				NextChallenge ();
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}
using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.OrderLetters {
	public class OrderLettersController : LevelController {
		public OrderLettersView view;
		private OrderLettersModel model;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord ());
		}

		public override void ShowHint () {
			LogHint ();
			view.Hint (model.GetCurrentWord ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new OrderLettersModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayWord () {
			Word word = model.GetCurrentWord ();
			word.PlayWord ();
			Invoke("AudioDone", word.AudioLength ());
		}

		public void AudioDone(){
			view.AudioDone ();
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
		}

		private void Wrong () {
			view.Wrong (model.GetCurrentWord ());
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
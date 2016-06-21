using System;
using Assets.Scripts.App;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.LettersComposeWords {
	public class LettersComposeWordsController : LevelController {
		private LettersComposeWordsModel model;
		public LettersComposeWordsView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord ());
		}

		public override void ShowHint () {
			LogHint();
			view.Hint (model.GetAnswer());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new LettersComposeWordsModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayWord () {
			model.GetCurrentWord ().PlayWord ();
			Invoke ("AudioDone", model.GetCurrentWord ().AudioLength ());
		}

		public void AudioDone(){
			view.AudioDone ();
		}

		public void Try (List<string> answer) {
			var isCorrect = model.IsCorrect (answer);
			LogAnswer (isCorrect);
			if (isCorrect) {
				view.Correct ();
			} else {
				view.Wrong();
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
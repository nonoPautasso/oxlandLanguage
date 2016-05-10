using System;
using Assets.Scripts.App;
using System.Collections.Generic;

namespace AssemblyCSharp {
	public class CreateSentencesController : LevelController {
		private CreateSentencesModel model;
		public CreateSentencesView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetOptions ());
		}

		public override void ShowHint () {
			LogHint();
			PlaySentence ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new CreateSentencesModel();
			view.Controller (this);
			model.StartGame();
			view.StartGame ();

			NextChallenge ();
		}

		public void Try (List<string> result) {
			if (model.IsCorrect (result)) {
				Correct ();
				NextChallenge ();
			}
			else
				Wrong ();
		}

		private void Correct () {
			LogAnswer (true);
			view.Correct ();
		}

		private void PlaySentence () {
			SoundManager.instance.PlayClip (model.GetSentenceAudio());
		}

		private void Wrong () {
			LogAnswer (false);
			view.Wrong ();
		}

		public override void RestartGame () {
			view.ResetView ();
			Start();
		}

		public void Start() { InitGame (); }
	}
}
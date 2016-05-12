using System;
using Assets.Scripts.App;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.CreateSentences {
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
			}
			else
				Wrong ();
		}

		public void AudioDone(){
			NextChallenge ();
		}

		private void Correct () {
			LogAnswer (true);
			view.Correct ();
			PlayAudioWithCallback ();
		}

		private void PlayAudioWithCallback () {
			SoundManager.instance.ConcatenateAudio(model.GetSentenceAudio (), AudioDone);
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
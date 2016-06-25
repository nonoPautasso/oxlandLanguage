using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;

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

		public void PlaySentence () {
			AudioClip audioClip = model.GetSentenceAudio ();
			SoundManager.instance.PlayClip (audioClip);
			Invoke ("HintAudioDone", audioClip.length);
		}

		public void HintAudioDone(){
			view.AudioDone ();
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
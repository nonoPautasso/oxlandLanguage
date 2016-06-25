using System;
using Assets.Scripts.App;
using UnityEngine;

namespace Assets.Scripts.Levels.SoundsInWords {
	public class SoundsInWordsController : LevelController {
		private SoundsInWordsModel model;
		public SoundsInWordsView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord());
		}

		public override void ShowHint () {
			LogHint();
			view.Hint (model.GetWordSyllables ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new SoundsInWordsModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlaySyllable () {
			AudioClip clip = model.GetSyllable ();
			SoundManager.instance.PlayClip (clip);
			Invoke ("AudioDone", clip.length);
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
			view.Wrong ();
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
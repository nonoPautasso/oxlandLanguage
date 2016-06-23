using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.CombineSounds {
	public class CombineSoundsController : LevelController {
		private CombineSoundsModel model;
		public CombineSoundsView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge ();
		}

		public override void ShowHint () {
			LogHint();
			view.Hint (model.GetCurrentRound ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new CombineSoundsModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayRound () {
			string round = model.GetCurrentRound ();
			List<AudioClip> audios = Words.GetPhonemes (round);
			SoundManager.instance.ConcatenateAudios (audios, AudioDone);
			SpeakerScript.instance.PlaySound (2);
		}

		public void AudioDone(){
			view.AudioDone ();
		}

		public void Try (string answer) {
			var isCorrect = model.IsCorrect (answer);
			LogAnswer (isCorrect);
			if (isCorrect) {
				view.Correct ();
			} else {
				view.Wrong(model.GetCurrentRound ());
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}


using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Levels.IdentifyInitialSound {
	public class IdentifyInitialSoundController : LevelController {
		private IdentifyInitialSoundModel model;
		public IdentifyInitialSoundView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentRound ());
		}

		public override void ShowHint () {
			LogHint();
			view.Hint (model.GetCurrentRound ());
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new IdentifyInitialSoundModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayRound () {
			List<Word> round = model.GetCurrentRound ();
			List<AudioClip> audios = Words.GetAudios (round);
			SoundManager.instance.ConcatenateAudios (audios, AudioDone);
			SpeakerScript.instance.PlaySound (audios.Count*3);
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
				view.Wrong();
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
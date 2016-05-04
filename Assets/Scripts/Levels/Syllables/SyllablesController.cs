using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.Syllables {
	public class SyllablesController : LevelController {
		private SyllablesModel model;
		public SyllablesView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetWord (), model.GetSyllables ());
		}

		public override void ShowHint () {
			LogHint();
			PlayWord (HintDone);
		}

		private void HintDone () {
			view.HintDone ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new SyllablesModel();
			view.Controller (this);
			model.StartGame();
			view.StartGame ();

			NextChallenge ();
		}

		public void Try (List<string> result) {
			if (model.IsCorrect (result))
				Correct ();
			else
				Wrong ();
		}

		private void Correct () {
			LogAnswer (true);
			view.Correct ();
			PlayWord (AudiosDone);
		}

		private void PlayWord (Action a) {
			List<AudioClip> audios = new List<AudioClip> ();
			audios.Add (model.GetWord ().GetAudio ());
			audios.AddRange (model.GetCorrectSyllables ());
			SoundManager.instance.ConcatenateAudios (audios, a);
		}

		public void PlaySyllable (string syllable) {
			SoundManager.instance.PlayClip (Words.SyllableClip (syllable));
		}

		public void AudiosDone(){
			NextChallenge ();
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
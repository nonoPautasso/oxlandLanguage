using System;
using Assets.Scripts.App;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Common;

namespace Assets.Scripts.Levels.OracionesPictogramas {
	public class OracionesPictogramasController : LevelController {
		private OracionesPictogramasModel model;
		public OracionesPictogramasView view;

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new OracionesPictogramasModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge();
		}

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.CurrentSentence,model.Answers,model.AllWords);

		}


		public void Try(List<string> answers) {
			bool correct = model.IsCorrect(answers);
			LogAnswer(correct);
			if(correct){
				view.Correct();
			} else {
				view.Wrong();
			}
		}

		public void PlaySentenceAudios () {
			List<AudioClip> audios = model.SentenceAudios;
			SoundManager.instance.ConcatenateAudios (audios, AudioDone);
			SpeakerScript.instance.PlaySound (audios.Count);
		}

		public void AudioDone(){
			view.AudioDone ();
		}

		public void NextClick(){
			if (model.GameEnded ())
				EndGame(model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			else
				NextChallenge ();
		}

		public override void ShowHint () {
			LogHint ();
			model.RequestHint ();
		}

		public override void RestartGame () { Start(); }
		public void Start() { InitGame (); }

}
}
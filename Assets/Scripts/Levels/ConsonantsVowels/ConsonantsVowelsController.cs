using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.ConsonantsVowels {
	public class ConsonantsVowelsController : LevelController {
		public bool isVowels;
		public ConsonantsVowelsView view;
		private ConsonantsVowelsModel model;

		public override void NextChallenge () {
			if (model.GameEnded ()) {
				EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			}
			else {
				model.NextChallenge ();
				view.NextChallenge (model.GetLetters (), model.GetOthers ());
			}
		}

		public override void ShowHint () {
			LogHint ();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new ConsonantsVowelsModel(isVowels);
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void RoundEnd () {
			view.RoundEndFirst ();
			model.ResetAudioIndex ();
			NextAudio ();
		}

		private void NextAudio() {
			if(model.AudiosEnded()){
				if(isVowels){
					NextChallenge ();
				} else {
					view.RoundEnd ();
				}
			} else {
				view.PaintText (model.AudioIndex());
				AudioClip letter = Words.LetterClip (model.AudioLetter ());
				SoundManager.instance.PlayClip (letter);
				Invoke ("NextAudio", letter.length);
			}
		}

		public override void RestartGame () { InitGame (); }

		public void Start(){ InitGame (); }
	}
}
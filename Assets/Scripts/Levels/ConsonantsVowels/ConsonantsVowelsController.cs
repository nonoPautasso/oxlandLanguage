using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;

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
			if(isVowels){
				SoundManager.instance.ConcatenateAudios (Words.LetterClips(model.GetLetters ()), NextChallenge);
			} else {
				SoundManager.instance.ConcatenateAudios (Words.LetterClips(model.GetLetters ()), view.RoundEnd);
			}
		}

		public override void RestartGame () { InitGame (); }

		public void Start(){ InitGame (); }
	}
}
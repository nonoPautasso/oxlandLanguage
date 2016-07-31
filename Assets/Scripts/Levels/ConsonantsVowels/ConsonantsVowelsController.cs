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
				if(isVowels){
					SoundManager.instance.ConcatenateAudios (Words.LetterClips(Words.GetVowels ()), GameEnd);
				} else GameEnd ();
			}
			else {
				view.NextChallenge (model.GetLetters (), model.GetOthers ());
				model.NextChallenge ();
			}
		}

		private void GameEnd () {
			EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
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
			if(!isVowels) view.RoundEnd ();
			else {
				view.VowelEnd ();
				NextChallenge ();
			}
		}

		public override void RestartGame () { InitGame (); }

		public void Start(){ InitGame (); }
	}
}
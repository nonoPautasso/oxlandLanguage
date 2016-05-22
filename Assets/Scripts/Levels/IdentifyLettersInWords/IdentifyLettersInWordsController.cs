using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using I18N;
using System.Collections.Generic;

namespace Assets.Scripts.Levels.IdentifyLettersInWords {
	public class IdentifyLettersInWordsController : LevelController {
		private IdentifyLettersInWordsModel model;
		public IdentifyLettersInWordsView view;

		public override void NextChallenge () {
			if (model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);

			model.NextChallenge ();
			view.NextChallenge (model.GetCurrentWord());
		}

		public override void ShowHint () {
			LogHint();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new IdentifyLettersInWordsModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void PlayLetter () {
			Words.PlayLetter (model.GetCurrentLetter ());
		}

		public void Try (List<string> answers) {
			var isCorrect = model.IsCorrect (answers);
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
using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Levels.MemotestEnding {
	public class MemotestEndingController : LevelController {
		private MemotestEndingModel model;
		public MemotestEndingView view;

		public override void NextChallenge () {
			view.StartGame (model.GetPairs ());
		}

		public void CheckGameEnded(){
			if(model.GameEnded ()) EndGame (model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
		}

		public override void ShowHint () {
			LogHint();
		}

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new MemotestEndingModel();
			view.Controller (this);
			model.StartGame();

			NextChallenge ();
		}

		public void Try (int index1, int index2) {
			var isCorrect = model.IsCorrect (index1, index2);
			LogAnswer (isCorrect);
			if (isCorrect) {
				view.Correct (index1, index2);
				CheckGameEnded ();
			} else {
				view.Wrong(index1, index2);
			}
		}

		public override void RestartGame () { Start(); }

		public void Start() { InitGame (); }
	}
}
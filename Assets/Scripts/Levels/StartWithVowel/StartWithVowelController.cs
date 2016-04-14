using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Levels.StartWithVowel {
	public class StartWithVowelController : LevelController {
		private StartWithVowelModel model;
		public StartWithVowelView view;

		public override void NextChallenge () { }

		public override void ShowHint () { }

		public override void InitGame () {
			MetricsManager.instance.GameStart();
			model = new StartWithVowelModel();
			view.Controller (this);
			model.StartGame();
			SetCurrentPage ();
		}

		void SetCurrentPage () {
			view.SetCurrentPage (model.GetCurrentPageNumber (), model.GetCurrentPage ());
		}

		public void BackButton () {
			model.BackButton ();
			SetCurrentPage ();
		}

		public void NextButton () {
			model.NextButton ();
			SetCurrentPage ();
		}

		public void ObjectClick (int index) {
			bool correct = model.ObjectClick (index);
			LogAnswer (correct);
			view.Answer(model.GetWord (index), index, correct);

			if(model.HasEnded()){
				EndGame(model.MinSeconds, model.PointsPerSecond, model.PointsPerError);
			}
		}

		public bool IsCorrect (int index) {
			return model.IsCorrect (index);
		}

		public void SubmarineClick () {
			string letter = Words.GetVowels()[model.GetCurrentPageNumber()];
			AudioClip clip = Resources.Load<AudioClip>("Audio/Spanish/Letters/" + letter);
			SoundManager.instance.PlayClip(clip);
		}

		public void SoundButtonClick (int index) {
			model.GetWord(index).PlayWord();
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}
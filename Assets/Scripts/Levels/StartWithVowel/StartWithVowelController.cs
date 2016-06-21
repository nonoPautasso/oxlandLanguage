using System;
using Assets.Scripts.App;
using Assets.Scripts.Common;
using UnityEngine;
using System.Collections.Generic;
using I18N;

namespace Assets.Scripts.Levels.StartWithVowel {
	public class StartWithVowelController : LevelController {
		private StartWithVowelModel model;
		public StartWithVowelView view;

		public override void NextChallenge () { }

		public override void ShowHint () {
			LogHint ();
		}

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
			AudioClip clip = Resources.Load<AudioClip>("Audio/" + I18n.Msg ("words.locale") + "/Phonemes/" + letter);
			SoundManager.instance.PlayClip(clip);
		}

		public void SoundButtonClick (int index) {
			Word word = model.GetWord (index);
			word.PlayWord();
			view.WordPlayed (word.AudioLength ());
		}

		public List<Tuple<Word, bool>> GetCurrentPage () {
			return model.GetCurrentPage ();
		}

		public override void RestartGame () {
			Start();
		}

		public void Start() { InitGame (); }
	}
}